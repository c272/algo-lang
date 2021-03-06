﻿using Antlr4;
using Antlr4.Runtime;
using System;

namespace Algo
{
    /// <summary>
    /// The Algo error handling class.
    /// </summary>
    public class ExternalError
    {
        //Fatal error, with token context.
        public static void Fatal(ParserRuleContext context, string errMessage)
        {
            //Check context isn't broken before attempting to use it, don't want the error message throwing an error.
            if (context == null) { FatalNoContext(errMessage); return; }

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("Algo Runtime Error: " + AlgoRuntimeInformation.FileLoaded + ", Line " + context.Start.Line + ":" + context.Start.StartIndex + " - " + errMessage);
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Environment.Exit(-1);
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
            Environment.Exit(-1);
        }

        //Warning.
        public static void Warning(ParserRuleContext context, string errMessage)
        {
            //Check context isn't broken before attempting to use it, don't want the error message throwing an error.
            if (context == null) { FatalNoContext(errMessage); return; }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Algo Warning: Line " + context.Start.Line + ":" + context.Start.StartIndex + " - " + errMessage);
            Console.ForegroundColor = ConsoleColor.White;
        }

        //Warning, but where rule contexts are unavailable.
        public static void WarningNoContext(string errMessage)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Algo Warning: NOCONTEXT - " + errMessage);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}