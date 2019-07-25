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
        public static algoVisitor visitor = null;

        //The current version of algo. (vX.X.BUILD)
        private const int MAJOR_VER = 0;
        private const int MINOR_VER = 3;

        public static void Main(string[] args)
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
                //Print version info.
                PrintVersionInfo();
                Console.WriteLine("Starting interpreter...\n");

                //Create a visitor.
                if (visitor == null)
                {
                    visitor = new algoVisitor();
                }

                //Interactive interpreter.
                while (true)
                {
                    Console.Write(">> ");
                    string line = Console.ReadLine();

                    //Catch keywords and null strings.
                    if (line == "quit" || line == "exit") { break; }
                    if (line == "algo help") { PrintHelp(); continue; }
                    if (line == "") { continue; }

                    //Parse line.
                    var s_chars = new AntlrInputStream(line);
                    var s_lexer = new algoLexer(s_chars);
                    var s_tokens = new CommonTokenStream(s_lexer);
                    var s_parse = new algoParser(s_tokens);

                    //Turn on continuous mode.
                    AlgoRuntimeInformation.ContinuousMode = true;

                    //Execute.
                    s_parse.BuildParseTree = true;
                    var s_tree = s_parse.compileUnit();

                    try
                    {
                        visitor.VisitCompileUnit(s_tree);
                    }
                    catch {}
                }

                return;
            }
            else if (args[0] == "-v")
            {
                //Print the version number.
                PrintVersionInfo();
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

        //Prints the current version info of Algo.
        private static void PrintVersionInfo()
        {
            string[] verInfo = typeof(Program).Assembly.GetName().Version.ToString().Split('.');
            Console.WriteLine("Algo Language Interpreter v" + MAJOR_VER + "." + MINOR_VER + "." + verInfo[2] + ", build " + verInfo[3] + ".");
            Console.WriteLine("(c) Larry Tang, 2019-" + DateTime.Now.Year);
            Console.WriteLine("\nFor information on how to use this interpreter, enter 'algo help'.");
        }

        //Prints the help page for Algo.
        private static void PrintHelp()
        {
            //todo
            Console.WriteLine("For detailed help information, please see https://github.com/c272/algo-lang/wiki/.");
        }
    }
}
