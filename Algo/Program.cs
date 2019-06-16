using Antlr4;
using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtendedNumerics;
using System.Numerics;

namespace Algo
{
    class Program
    {
        //The currently loaded file.
        public static string FileLoaded = "debugscript.txt";

        static void Main(string[] args)
        {
            //Test input string.
            string input = File.ReadAllText(FileLoaded);
            var chars = new AntlrInputStream(input);
            var lexer = new algoLexer(chars);
            var tokens = new CommonTokenStream(lexer);

            //Debug print.
            ANTLRDebug.PrintTokens(lexer);

            //Debug print tree.
            var parser = new algoParser(tokens);
            parser.BuildParseTree = true;
            var tree = parser.compileUnit();
            ANTLRDebug.PrintParseList(tree, parser);

            //Add a gap.
            Console.WriteLine(" --------------------\n | BEGIN EVALUATION |\n --------------------\n");

            //Walking the tree.
            var visitor = new algoVisitor();
            visitor.VisitCompileUnit(tree);

            Console.WriteLine("\n ------------------\n | END EVALUATION |\n ------------------\n");

            //Print variables.
            ANTLRDebug.PrintScopes();
        }
    }
}
