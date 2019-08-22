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
        public static Dictionary<string, string> LinkedScripts = new Dictionary<string, string>();
        
        ////////////////////////////
        /// MAIN COMPILE METHODS ///
        ////////////////////////////
        
        public static void Compile(string file)
        {
            //Does the file that is being compiled exist?
            if (!File.Exists(file))
            {
                Error.FatalCompile("The file you wish to compile does not exist.");
                return;
            }

            //Yes, read the file into memory and get all linked import references.
            string toCompile = File.ReadAllText(file);

            //Get all linked import reference files.
            Log("Linking base Algo file '" + file + "'.");
            LinkFile(toCompile, file);
            Log("Successfully linked all referenced Algo scripts.", ALECEvent.Success);
            Console.WriteLine("todo");
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
            foreach (var line in lines)
            {
                if (!importReg.IsMatch(line))
                {
                    //Uh oh, failed the link.
                    Error.FatalCompile("An invalid import statement was found in script '" + fileName + "'.");
                    return;
                }

                //Matches, get the substring out.
                string referencedFile = line.Substring("import \"".Length).TrimEnd('"');
                Log("Discovered external reference to '" + referencedFile + "', attempting to read...");

                //Has the file been linked already?
                if (LinkedScripts.Keys.Contains(referencedFile))
                {
                    Log("This script has already been referenced, skipping.");
                    continue;
                }

                //Not linked yet, trying to read the file into LinkedScripts.
                try
                {
                    LinkedScripts.Add(referencedFile, File.ReadAllText(referencedFile));
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
            throw new NotImplementedException();
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