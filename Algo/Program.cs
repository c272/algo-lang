using Antlr4;
using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ExtendedNumerics;
using System.Numerics;
using Algo.PacMan;

namespace Algo
{
    class Program
    {
        //The tree traversal class for Algo.
        public static algoVisitor visitor;

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
                Console.WriteLine("Algo Language Interpreter v" + typeof(Program).Assembly.GetName().Version.ToString() + ".");
                Console.WriteLine("(c) Larry Tang, 2019-" + DateTime.Now.Year);
                Console.WriteLine("\nFor information on how to use this interpreter, enter 'algo help'.");
                return;
            }
            else if (args[0] == "pkg")
            {
                //Package management.
                Sharpie sharpie = new Sharpie(args.Slice(1, -1));
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
            visitor = new algoVisitor();
            visitor.VisitCompileUnit(tree);

            if (AlgoRuntimeInformation.DeveloperMode)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("\n ------------------\n | END EVALUATION |\n ------------------\n");

                //Print variables.
                ANTLRDebug.PrintScopes();
            }
        }
    }
}
