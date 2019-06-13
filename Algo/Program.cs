using Antlr4;
using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algo
{
    class Program
    {
        static void Main(string[] args)
        {
            //Test input string.
            string input = "let f() = { }";
            var chars = new AntlrInputStream(input);
            var lexer = new algoLexer(chars);
            var tokens = new CommonTokenStream(lexer);

            //Debug print.
            ANTLRDebug.PrintTokens(lexer);

            //Debug print tree.
            var parser = new algoParser(tokens);
            ANTLRDebug.PrintParseList(parser);

            //Getting tree.
            parser.BuildParseTree = true;
            var tree = parser.compileUnit();
        }
    }
}
