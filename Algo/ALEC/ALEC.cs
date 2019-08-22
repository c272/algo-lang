using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Antlr4.Runtime;
using System.Text.RegularExpressions;

namespace Algo
{
    //ALGO LANGUAGE EXECUTABLE COMPILER (ALEC)
    //Compiles Algo scripts into a system executable, cross platform.
    public static class ALEC
    {
        /////////////////////////////////
        /// STATIC COMPILE PROPERTIES ///
        /////////////////////////////////

        //All scripts imported in all linked scripts to the input file.
        public static string MainScript = "";
        public static Dictionary<string, string> LinkedScripts = new Dictionary<string, string>();
        public static DateTime CompileStartTime;
        
        ////////////////////////////
        /// MAIN COMPILE METHODS ///
        ////////////////////////////
        
        public static void Compile(string file)
        {
            //Note the compile start time.
            CompileStartTime = DateTime.Now;

            //Does the file that is being compiled exist?
            if (!File.Exists(file))
            {
                Error.FatalCompile("The file you wish to compile does not exist.");
                return;
            }

            //Yes, read the file into memory and get all linked import references.
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
            MainScript = toCompile;
            Log("Successfully linked main file, linking references...");

            //Get all linked import reference files.
            LinkFile(toCompile, file);
            Log("Successfully linked base and all referenced Algo scripts.", ALECEvent.Success);
            
            //

            return;
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
                if (line.StartsWith("import "))
                {
                    imports.Add(line);
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
            Regex importReg = new Regex("import \"[^\"]+\"");
            foreach (var line in imports)
            {
                if (!importReg.IsMatch(line))
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
                if (LinkedScripts.Keys.Contains(referencedFile))
                {
                    Log("This script has already been referenced, skipping.");
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
                foreach (var file in LinkedScripts)
                {
                    LinkFile(file.Value, file.Key);
                }
            }
        }

        ///////////////////////
        /// LOGGING METHODS ///
        ///////////////////////

        //Prints the "compile finished" footer.
        public static void PrintCompileFooter()
        {
            Console.WriteLine("this is a compile footer");
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