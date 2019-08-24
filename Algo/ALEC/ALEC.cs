using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Antlr4.Runtime;
using System.Text.RegularExpressions;
using System.CodeDom.Compiler;
using System.CodeDom;
using Microsoft.CSharp;
using System.Reflection;
using System.Diagnostics;
using ILRepacking;

namespace Algo
{
    //ALGO LANGUAGE EXECUTABLE COMPILER (ALEC)
    //Compiles Algo scripts into a system executable, cross platform.
    public static class ALEC
    {
        ///////////////////////////////
        /// ALEC PROPERTIES (CONST) ///
        ///////////////////////////////

        //The current version of ALEC. (vX.X.BUILD)
        private const int MAJOR_VER = 0;
        private const int MINOR_VER = 1;

        /////////////////////////////////
        /// STATIC COMPILE PROPERTIES ///
        /////////////////////////////////

        //All scripts imported in all linked scripts to the input file.
        public static string MainScript = "";
        public static Dictionary<string, string> LinkedScripts = new Dictionary<string, string>();

        //The compile start time.
        public static DateTime CompileStartTime;

        //The name of the compiling project.
        public static string ProjectName;

        //The regex to detect an import statement.
        public static Regex ImportRegex = new Regex("import \"[^\"]+\"");

        ////////////////////////////
        /// MAIN COMPILE METHODS ///
        ////////////////////////////

        public static void Compile(string file)
        {
            //Print the compile header.
            PrintCompileHeader();

            //Note the compile start time.
            CompileStartTime = DateTime.Now;

            //Does the file that is being compiled exist?
            if (!File.Exists(file))
            {
                Error.FatalCompile("The file you wish to compile does not exist.");
                return;
            }

            //Get the FileInfo, set the name of the project.
            FileInfo fi = new FileInfo(file);
            if (fi.Name.Contains("."))
            {
                ProjectName = fi.Name.Split('.')[0];
            }
            else
            {
                ProjectName = fi.Name;
            }

            //Yes, read the file into memory and strip all the comments.
            Log("Linking base Algo file '" + file + "'.");
            string toCompile = "";
            try
            {
                toCompile = File.ReadAllText(file);
            }
            catch(Exception e)
            {
                Error.FatalCompile("Could not read from base script file, error '" + e.Message + "'.");
                return;
            }

            //Attaching the "core" library to the start.
            Log("Attaching the 'core' library to the base script.");
            MainScript = "import \"core\";" + MainScript;

            //Strip all comment lines.
            var scriptLines = toCompile.Replace("\r", "").Split('\n');
            foreach (var line in scriptLines)
            {
                if (!line.StartsWith("//"))
                {
                    MainScript += line + "\n";
                }
            }
            Log("Successfully linked main file, linking references...");

            //Get all linked import reference files.
            LinkFile(MainScript, file);
            Log("Successfully linked base and all referenced Algo scripts.", ALECEvent.Success);

            //Replacing import references with their proper scripts.
            Log("Attempting to replace abstract import links...");
            bool success = ReplaceImportReferences();
            if (!success)
            {
                Error.FatalCompile("Failed to replace import links with script references. Do you have a circular import loop?");
                return;
            }

            //That's done, now convert it to a literal string.
            Log("Converting script into literal form for compilation...");
            MainScript = MainScript.Replace("\"", "\"\"");
            Log("Successfully converted into literal form.");

            //Create the compiler (with arguments).
            CSharpCodeProvider provider = new CSharpCodeProvider();
            CompilerParameters cp = new CompilerParameters();
            cp.GenerateExecutable = true;
            cp.OutputAssembly = ProjectName + ".exe";
            cp.GenerateInMemory = false;

            //Reference the main Algo assembly (this one) when compiling.
            Assembly entryasm = Assembly.GetEntryAssembly();
            cp.ReferencedAssemblies.Add(entryasm.Location);
            cp.ReferencedAssemblies.Add(CPFilePath.GetPlatformFilePath(new string[] { AppDomain.CurrentDomain.BaseDirectory, "Antlr4.Runtime.dll" }));

            //Attempt to compile.
            string finalScript = ALECTemplates.ALECEntryPoint.Replace("[CUSTOM-CODE-HERE]", MainScript);
            CompilerResults results = provider.CompileAssemblyFromSource(cp, finalScript);

            if (results.Errors.HasErrors)
            {
                //Uh oh, failed.
                //Collect the errors.
                string final = "Attempting to compile returned some errors:\n";
                List<string> errors = new List<string>();
                foreach (CompilerError error in results.Errors)
                {
                    errors.Add(error.ErrorText + " (Line " + error.Line + ", column " + error.Column + ").");
                }

                for (int i=0; i<errors.Count; i++)
                {
                    final += "[" + (i + 1) + "] - " + errors[i] + "\n";
                }

                //Log.
                Error.FatalCompile(final);
                return;
            }

            //Successfully compiled, try to output to file.
            Log("Successfully compiled the Algo script into assembly.", ALECEvent.Success);
            Log("Output has been saved in '" + ProjectName + ".exe'.");

            //If Linux, MKBundle.
            if (AlgoPlatformInfo.IsLinux)
            {
                //Attempt to run MKBundle.
                Log("Linux detected as the operating system, attempting to create a native binary...");
                Log("MAKE SURE YOU HAVE MKBUNDLE INSTALLED, AND HAVE A MONO 'machine.config' AT /etc/mono/4.5/machine.config.");
                Process proc = new Process();
                proc.StartInfo.FileName = "/bin/bash";
                proc.StartInfo.Arguments = "-c \" mkbundle -o " + ProjectName + " --simple " + cp.OutputAssembly + " --machine-config /etc/mono/4.5/machine.config --no-config --nodeps *.dll Algo.exe \"";
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.Start();

                while (!proc.StandardOutput.EndOfStream)
                {
                    Log(proc.StandardOutput.ReadLine());
                }

                Log("MKBundle has finished executing.");

                //Delete the main executable.
                Log("Attempting to clean up...");
                try
                {
                    File.Delete(ProjectName + ".exe");
                }
                catch(Exception e)
                {
                    Error.WarningCompile("Failed to clean up Windows executable, given error '" + e.Message + "'.");
                }
            }
            else if (AlgoPlatformInfo.IsWindows)
            {
                //It's Windows, use ILRepack instead.
                Log("Windows detected as the operating system, attempting to create a native binary...");
                Log("Attempting to bundle dependencies into packed executable...");
                RepackOptions opt = new RepackOptions();
                opt.OutputFile = ProjectName + "_packed.exe";
                opt.SearchDirectories = new string[] { AppDomain.CurrentDomain.BaseDirectory, Environment.CurrentDirectory };

                //Setting input assemblies.
                string[] files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll");
                opt.InputAssemblies = new string[] { ProjectName + ".exe", entryasm.Location }.Concat(files).ToArray();
                
                try
                {
                    //Merging.
                    ILRepack pack = new ILRepack(opt);
                    pack.Repack();
                    Log("Successfully merged all dependencies with the output executable.", ALECEvent.Success);

                    //Replacing the depending executable with the new one.
                    Log("Cleaning up build files...");
                    try
                    {
                        File.Delete(ProjectName + ".exe");
                        File.Move(ProjectName + "_packed.exe", ProjectName + ".exe");
                    }
                    catch(Exception e)
                    {
                        Error.WarningCompile("File cleanup failed with error '" + e.Message + "'.");
                        Error.WarningCompile("Failed to clean up build files, the executable to use is named '" + ProjectName + "_packed.exe' rather than '" + ProjectName + ".exe'.");
                    }
                }
                catch(Exception e)
                {
                    Error.WarningCompile("Packing the executable's dependencies failed, with error '" + e.Message + "'. You will need to include algo.exe and all it's dependencies along with the built executable for it to run.");
                }
            }
            else
            {
                Error.FatalCompile("Could not detect the operating system to compile native binary.");
                return;
            }

            //Print the compile footer.
            PrintCompileFooter();
            return;
        }

        //Turns a normal string input into a literal output string (eg. \n becomes \\n)
        private static string ConvertToLiteral(string input)
        {
            using (var writer = new StringWriter())
            {
                using (var provider = CodeDomProvider.CreateProvider("CSharp"))
                {
                    provider.GenerateCodeFromExpression(new CodePrimitiveExpression(input), writer, null);
                    return writer.ToString();
                }
            }
        }

        //Replace import references in the base file and all linked source files, up to n times.
        //where n = the number of linked files.
        //If it goes over n, there is a high likelihood of a circular reference.
        private static bool ReplaceImportReferences()
        {
            int n = LinkedScripts.Count + 1;
            for (int i = 0; i <= n; i++)
            {
                Log("Beginning pass " + (i + 1) + " of AIL replacement...");

                //Replace the base script's references.
                foreach (var link in LinkedScripts)
                {
                    MainScript = MainScript.Replace("import \"" + link.Key + "\";", link.Value);
                }
                
                //Done replacing, check if there are any import statements left.
                bool importFound = false;
                if (ImportRegex.IsMatch(MainScript))
                {
                    Log("Import still exists in base script.");
                    importFound = true;
                }
                
                //Was a remaining import found? If so, keep going.
                if (!importFound)
                {
                    Log("Successfully replaced all abstract import links with the loaded linked scripts.", ALECEvent.Success);
                    return true;
                }

                Log("Imports still remain after pass " + (i + 1) + ", " + (n - i) + " remaining before fail.");
            }

            return false;
        }

        //Recursively grabs all import references through linked files in scripts, strips comments.
        private static void LinkFile(string toCompile, string fileName)
        {
            //Strip all comment lines.
            List<string> lines = toCompile.Replace("\r", "").Split('\n').ToList();
            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].StartsWith("//"))
                {
                    lines.RemoveAt(i);
                    i--;
                }
            }

            //Join it back together, split with line end instead.
            string source = string.Join("", lines);
            lines = source.Split(';').ToList();

            //Get all the import lines.
            List<string> imports = new List<string>();
            foreach (var line in lines)
            {
                if (ImportRegex.IsMatch(line))
                {
                    imports.Add(ImportRegex.Match(line).Value);
                }
            }

            //Check if there are actually any imports in this file.
            if (imports.Count == 0)
            {
                Log("No imports detected, skipping linking.");
                return;
            }
            Log("Detected " + imports.Count + " referenced external scripts.");

            //For each of those, check contain a valid import (regex).
            foreach (var line in imports)
            {
                if (!ImportRegex.IsMatch(line))
                {
                    //Uh oh, failed the link.
                    Error.FatalCompile("An invalid import statement was found in script '" + fileName + "', \"" + line.Substring(0,30) + "...\".");
                    return;
                }

                //Matches, get the substring out.
                string symbolicName = line.Substring("import \"".Length).TrimEnd('"');
                string referencedFile = symbolicName;
                if (!referencedFile.EndsWith(".ag")) { referencedFile += ".ag"; }

                //Has the file been linked already?
                if (LinkedScripts.Keys.Contains(symbolicName))
                {
                    Log("Script '" + symbolicName + "' has already been linked, skipping.");
                    continue;
                }

                //Is the file in std or packages?
                string stdPath = CPFilePath.GetPlatformFilePath(DefaultDirectories.StandardLibDirectory.Concat(referencedFile.Split('/')).ToArray());
                string pkgPath = CPFilePath.GetPlatformFilePath(DefaultDirectories.PackagesDirectory.Concat(referencedFile.Split('/')).ToArray());
                if (File.Exists(stdPath))
                {
                    Log("'" + referencedFile + "' discovered as a standard library file, attempting to read...");
                    referencedFile = stdPath;
                }
                else if (File.Exists(pkgPath))
                {
                    Log("'" + referencedFile + "' discovered as a package, attempting to read...");
                    referencedFile = pkgPath;
                }
                else
                {
                    Log("Discovered external reference to '" + referencedFile + "', attempting to read...");
                }

                //Not linked yet, trying to read the file into LinkedScripts.
                try
                {
                    //Add the symbolic name rather than the filename, easier to substitute into the file that way.
                    LinkedScripts.Add(symbolicName, File.ReadAllText(referencedFile));
                }
                catch(Exception e)
                {
                    Error.FatalCompile("Failed to read file '" + referencedFile + "', with error '" + e.Message + "'.");
                    return;
                }

                //Read in.
                Log("Successfully read and linked file '" + referencedFile + "'.", ALECEvent.Success);

                //Link files for all linked scripts (recursively).
                for (int i=0; i<LinkedScripts.Count; i++)
                {
                    var file = LinkedScripts.ElementAt(i);
                    LinkFile(file.Value, file.Key);
                }
            }
        }

        ///////////////////////
        /// LOGGING METHODS ///
        ///////////////////////

        //Prints the compile header.
        public static void PrintCompileHeader()
        {
            string[] verInfo = typeof(Program).Assembly.GetName().Version.ToString().Split('.');
            Console.WriteLine(@"  ______     __         ______     ______    
 /\  __ \   /\ \       /\  ___\   /\  ___\   
 \ \  __ \  \ \ \____  \ \  __\   \ \ \____  
  \ \_\ \_\  \ \_____\  \ \_____\  \ \_____\ 
   \/_/\/_/   \/_____/   \/_____/   \/_____/
");
            Console.WriteLine("ALEC (Algo Executable Language Compiler) v" + MAJOR_VER + "." + MINOR_VER + "." + verInfo[2] + ", build " + verInfo[3]);
            Console.WriteLine("Framework: .NET Framework ENV " + Environment.Version);
            Console.WriteLine("Operating System: " + Environment.OSVersion);
            if (Environment.Is64BitProcess)
            {
                Console.WriteLine("Build Mode: 64 bit");
            }
            else
            {
                Console.WriteLine("Build Mode: 32 bit");
            }
            Console.WriteLine();
        }

        //Prints the "compile finished" footer.
        public static void PrintCompileFooter()
        {
            Console.WriteLine("\n  --------------");
            if (Events.FindIndex(x => x.Item1 == ALECEvent.Fatal) == -1)
            {
                Console.WriteLine(" Build succeeded.");
            }
            else
            {
                Console.WriteLine(" Build failed. :(");
            }

            //Print the amount of events.
            Console.WriteLine("  " + Events.Where(x => x.Item1 == ALECEvent.Fatal).Count() + " fatal errors.");
            Console.WriteLine("    " + Events.Where(x => x.Item1 == ALECEvent.Warning).Count() + " warnings.");
            Console.WriteLine("    " + Events.Where(x => x.Item1 == ALECEvent.Notice).Count() + " notices.");
            Console.WriteLine("  --------------");
        }

        //Logs an event to the current ALEC runtime.
        public static List<Tuple<ALECEvent, string>> Events = new List<Tuple<ALECEvent, string>>();
        public static void Log(string msg, ALECEvent event_=ALECEvent.Notice)
        {
            if (event_ == ALECEvent.Success)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            Console.WriteLine("[" + DateTime.Now.ToString("HH:mm:ss") + "] " + msg);
            Console.ResetColor();
            Events.Add(new Tuple<ALECEvent, string>(event_, msg));
        }
    }

    //Types of events that can happen during compile.
    public enum ALECEvent
    {
        Fatal,
        Warning,
        Notice,
        Success
    }
}