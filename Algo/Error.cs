using Antlr4;
using Antlr4.Runtime;
using System;

namespace Algo
{
    /// <summary>
    /// The Algo error handling class.
    /// </summary>
    public class Error
    {
        //Fatal error, with token context.
        public static void Fatal(ParserRuleContext context, string errMessage)
        {
            //Is error catching on?
            if (AlgoRuntimeInformation.CatchExceptions)
            {
                //Don't actually perform any error tasks, just set the caught message.
                AlgoRuntimeInformation.SetExceptionMessage(errMessage);
                throw new Exception("Error caught, you shouldn't be seeing this message.");
            }
            
            //Set console colours.
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Red;

            //Check the loaded file is right.
            if (AlgoRuntimeInformation.FileLoaded == "") { AlgoRuntimeInformation.FileLoaded = "No File"; }

            //Print the actual error message.
            if (context == null)
            {
                Console.WriteLine("Algo Runtime Error: " + AlgoRuntimeInformation.FileLoaded + ", NOCONTEXT - " + errMessage);
            }
            else
            {
                Console.WriteLine("Algo Runtime Error: " + AlgoRuntimeInformation.FileLoaded + ", Line " + context.Start.Line + ":" + context.Start.StartIndex + " - " + errMessage);
            }
            Console.ResetColor();

            //Only print the scopes in developer mode.
            if (AlgoRuntimeInformation.DeveloperMode)
            {
                ANTLRDebug.PrintScopes();
            }

            //Test mode? Throw instead of exit.
            if (AlgoRuntimeInformation.UnitTestMode)
            {
                throw new Exception(errMessage);
            }

            //Check if we're in continuous mode. If so, just restart (no header).
            if (AlgoRuntimeInformation.ContinuousMode)
            {
                //If in continuous mode, just keep going and run the interpreter again.
                Program.Main(new string[] { "--nohead" });
                Environment.Exit(0);
            }
            else
            {
                //Normal mode, just exit.
                Environment.Exit(-1);
            }
        }

        //Fatal error, with no token context. Deprecated, now a wrapper for Fatal(null, msg).
        public static void FatalNoContext(string errMessage)
        {
            Fatal(null, errMessage);
        }

        //Internal fatal error.
        public static void Internal(string errMessage)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            if (AlgoRuntimeInformation.DeveloperMode)
            {
                Console.WriteLine("Internal Language Error - " + errMessage);
                Console.WriteLine("Please submit this with a detailed description as an issue at github.com/c272/algo/issues.");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("Internal language crash, Enable developer mode for further debugging details.");
                Console.ResetColor();
            }
            
            //If in continuous mode, just restart.
            if (AlgoRuntimeInformation.ContinuousMode)
            {
                Program.Main(new string[] { "--nohead" });
                Environment.Exit(0);
                return;
            }

            //Test mode? Throw instead of exit.
            if (AlgoRuntimeInformation.UnitTestMode)
            {
                throw new Exception(errMessage);
            }

            Environment.Exit(-1);
        }

        //Warning.
        public static void Warning(ParserRuleContext context, string errMessage)
        {
            //Check context isn't broken before attempting to use it, don't want the error message throwing an error.
            if (context == null) { FatalNoContext(errMessage); return; }

            if (AlgoRuntimeInformation.FileLoaded == "") { AlgoRuntimeInformation.FileLoaded = "No File"; }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Algo Warning: " + AlgoRuntimeInformation.FileLoaded + ", line " + context.Start.Line + ":" + context.Start.StartIndex + " - " + errMessage);
            Console.ResetColor();
        }

        //Warning, but where rule contexts are unavailable.
        public static void WarningNoContext(string errMessage)
        {
            if (AlgoRuntimeInformation.FileLoaded == "") { AlgoRuntimeInformation.FileLoaded = "No File"; }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Algo Warning: " + AlgoRuntimeInformation.FileLoaded + ", NOCONTEXT - " + errMessage);
            Console.ResetColor();
        }

        //A fatal compile message (the compiler has failed).
        public static void FatalCompile(string msg)
        {
            //Send the error message.
            Console.ForegroundColor = ConsoleColor.Red;
            ALEC.Log("FATAL - " + msg, ALECEvent.Fatal);
            Console.ResetColor();

            //Print the compile footer.
            ALEC.PrintCompileFooter();

            //Test mode? Throw instead of exit.
            if (AlgoRuntimeInformation.UnitTestMode)
            {
                throw new Exception(msg);
            }

            Environment.Exit(-1);
        }

        //A warning compile message.
        public static void WarningCompile(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            ALEC.Log("WARN - " + msg, ALECEvent.Warning);
            Console.ResetColor();
        }
    }
}