using System;
using System.Collections.Generic;
using System.IO;
using Antlr4.Runtime;

namespace Algo
{
    //ALGO LANGUAGE EXECUTABLE COMPILER (ALEC)
    //Compiles Algo scripts into a system executable, cross platform.
    public static class ALEC
    {
        public static void Compile(string file)
        {
            //Does the file that is being compiled exist?
            if (!File.Exists(file))
            {
                Error.FatalCompile("The file you wish to compile does not exist.");
                return;
            }

            //Yes, it does. What platform is the compile target?

        }

        //Prints the "compile finished" footer.
        public static void PrintCompileFooter()
        {
            throw new NotImplementedException();
        }

        //Logs an event to the current ALEC runtime.
        public static List<Tuple<ALECEvent, string>> Events = new List<Tuple<ALECEvent, string>>();
        public static void Log(ALECEvent event_, string msg)
        {
            Events.Add(new Tuple<ALECEvent, string>(event_, msg));
        }

        //Types of events that can happen during compile.
        public enum ALECEvent
        {
            Fatal,
            Warning,
            Notice
        }
    }
}