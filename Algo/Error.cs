using Antlr4;
using Antlr4.Runtime;
using System;

namespace Algo
{
    /// <summary>
    /// The Algo error handling class.
    /// </summary>
    internal class Error
    {
        //Fatal error, with token context.
        public static void Fatal(ParserRuleContext context, string errMessage)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("Algo Runtime Error: Line " + context.Start.Line + ":" + context.Start.StartIndex + " - " + errMessage);
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Environment.Exit(-1);
        }

        //Fatal error, with no token context.
        public static void FatalNoContext(string errMessage)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("Algo Runtime Error: NOCONTEXT - " + errMessage);
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        //Warning.
        public static void Warning(ParserRuleContext context, string errMessage)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Algo Warning: Line " + context.Start.Line + ":" + context.Start.StartIndex + " - " + errMessage);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}