using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algo
{
    public static class AlgoRuntimeInformation
    {
        //The currently loaded script file.
        public static string FileLoaded = "";

        //Whether the current runtime is in developer mode or not.
        public static bool DeveloperMode = false;

        //Whether the current runtime should ignore fatal errors and continue running.
        public static bool ContinuousMode = false;

        //Whether the current runtime should catch fatal errors and return up the heirarchy.
        public static bool CatchExceptions = false;
        
        //Whether the current run time should suppress termination because of unit tests, and "throw" instead.
        public static bool UnitTestMode = false;

        //The caught exception message.
        private static string ExceptionMessage = "";

        //Checks whether an exception has been caught.
        public static bool ExceptionCaught()
        {
            if (ExceptionMessage == "") { return false; }
            return true;
        }

        //Gets the exception message that was caught. Wipes the exception afterward.
        public static string GetExceptionMessage()
        {
            string caught = ExceptionMessage;
            ExceptionMessage = "";
            return caught;
        }

        //Sets the exception message.
        public static void SetExceptionMessage(string msg)
        {
            ExceptionMessage = msg;
        }
    }
}
