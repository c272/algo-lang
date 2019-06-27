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
        static void Main(string[] args)
        {
            //Check that all the necessary directories exist.
            CPFilePath.CreateDefaultDirectories();

            //Setting all flags based on console arguments.
            if (args.Contains("--dev"))
            {
                AlgoRuntimeInformation.DeveloperMode = true;
            }

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

            //Does the given file location exist?
            string fullPath = CPFilePath.GetPlatformFilePath(new string[] { Environment.CurrentDirectory, args[0] });
            if (!File.Exists(fullPath)) 
            {
                Error.FatalNoContext("No file with the name '" + args[0] + "' exists relative to your current directory.");
                return;
            }
            
            //Test input string.
            AlgoRuntimeInformation.FileLoaded = args[0];
            string input = File.ReadAllText(fullPath);
            var chars = new AntlrInputStream(input);
            var lexer = new algoLexer(chars);
            var tokens = new CommonTokenStream(lexer);

            //Debug print.
            if (AlgoRuntimeInformation.DeveloperMode)
            {
                ANTLRDebug.PrintTokens(lexer);
            }

            //Debug print tree.
            var parser = new algoParser(tokens);
            parser.BuildParseTree = true;
            var tree = parser.compileUnit();
            if (AlgoRuntimeInformation.DeveloperMode)
            {
                ANTLRDebug.PrintParseList(tree, parser);

                //Add a gap.
                Console.WriteLine(" --------------------\n | BEGIN EVALUATION |\n --------------------\n");
            }

            //Walking the tree.
            var visitor = new algoVisitor();
            visitor.VisitCompileUnit(tree);

            if (AlgoRuntimeInformation.DeveloperMode)
            {
                Console.WriteLine("\n ------------------\n | END EVALUATION |\n ------------------\n");

                //Print variables.
                ANTLRDebug.PrintScopes();
            }
        }
    }
}
