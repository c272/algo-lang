using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Antlr4.Runtime;

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
            LinkFile(toCompile);
        }

        //Recursively grabs all import references through linked files in scripts, strips comments.
        private static void LinkFile(string toCompile)
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

            //Get all the import lines.
            List<string> imports = new List<string>();
            foreach (var line in lines)
            {
                if (line.StartsWith("import "))
                {
                    imports.Add(line);
                }
            }
            Log("Detected " + imports.Count + " referenced external scripts.");

            //For each of those, check they're a valid import (regex).
            //..
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
            Console.WriteLine("[" + DateTime.Now.ToString("HH:mm:ss") + "] " + msg);
            Events.Add(new Tuple<ALECEvent, string>(event_, msg));
        }
    }

    //Types of events that can happen during compile.
    public enum ALECEvent
    {
        Fatal,
        Warning,
        Notice
    }
}