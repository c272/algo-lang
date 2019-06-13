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
        public static void Print(ParserRuleContext context, string errMessage)
        {
            Console.WriteLine("Algo Runtime Error: Line " + context.Start.Line + ":" + context.Start.StartIndex + " - " + errMessage);
            Environment.Exit(-1);
        }
    }
}