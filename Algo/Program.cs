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
        public static string FileLoaded = "";

        static void Main(string[] args)
        {
            //Check that all the necessary directories exist.
            CPFilePath.CreateDefaultDirectories();

            //Check the command line arguments are valid.
            if (args.Length < 1)
            {
                //Nah, just print the version number.
                //...

                return;
            }
            else if (args[0] == "pkg")
            {
                //Package management.
                //...
                return;
            }

            //Test input string.
            FileLoaded = args[0];
            string input = File.ReadAllText(CPFilePath.GetPlatformFilePath(new string[] { Environment.CurrentDirectory, args[0] }));
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
