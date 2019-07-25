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
            //Check context isn't broken before attempting to use it, don't want the error message throwing an error.
            if (context == null) { FatalNoContext(errMessage); return; }

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Red;
            if (AlgoRuntimeInformation.FileLoaded == "") { AlgoRuntimeInformation.FileLoaded = "No File"; }
            Console.WriteLine("Algo Runtime Error: " + AlgoRuntimeInformation.FileLoaded + ", Line " + context.Start.Line + ":" + context.Start.StartIndex + " - " + errMessage);
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;

            //Only print the scopes in developer mode.
            if (AlgoRuntimeInformation.DeveloperMode)
            {
                ANTLRDebug.PrintScopes();
            }

            if (!AlgoRuntimeInformation.ContinuousMode)
            {
                Environment.Exit(-1);
            }
        }

        //Fatal error, with no token context.
        public static void FatalNoContext(string errMessage)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Red;
            if (AlgoRuntimeInformation.FileLoaded == "") { AlgoRuntimeInformation.FileLoaded = "No File"; }
            Console.WriteLine("Algo Runtime Error: " + AlgoRuntimeInformation.FileLoaded + ", NOCONTEXT - " + errMessage);
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;

            //Only print scopes if we're in developer mode.
            if (AlgoRuntimeInformation.DeveloperMode)
            {
                ANTLRDebug.PrintScopes();
            }

            if (!AlgoRuntimeInformation.ContinuousMode)
            {
                Environment.Exit(-1);
            }
            else
            {
                Program.Main(new string[] { });
            }
        }

        //Warning.
        public static void Warning(ParserRuleContext context, string errMessage)
        {
            //Check context isn't broken before attempting to use it, don't want the error message throwing an error.
            if (context == null) { FatalNoContext(errMessage); return; }

            if (AlgoRuntimeInformation.FileLoaded == "") { AlgoRuntimeInformation.FileLoaded = "No File"; }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Algo Warning: " + AlgoRuntimeInformation.FileLoaded + ", line " + context.Start.Line + ":" + context.Start.StartIndex + " - " + errMessage);
            Console.ForegroundColor = ConsoleColor.White;
        }

        //Warning, but where rule contexts are unavailable.
        public static void WarningNoContext(string errMessage)
        {
            if (AlgoRuntimeInformation.FileLoaded == "") { AlgoRuntimeInformation.FileLoaded = "No File"; }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Algo Warning: " + AlgoRuntimeInformation.FileLoaded + ", NOCONTEXT - " + errMessage);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}