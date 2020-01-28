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

        //The current version of Algo. (vX.X.BUILD)
        private const int MAJOR_VER = 0;
        private const int MINOR_VER = 4;

        public static void Main(string[] args)
        {
            //Check that all the necessary directories exist.
            CPFilePath.CreateDefaultDirectories();

            //Setting all flags based on console arguments.
            if (args.Contains("--dev"))
            {
                AlgoRuntimeInformation.DeveloperMode = true;
            }

            //Check the command line arguments.
            if (args.Length < 1 || (args.Length == 1 && args[0] == "--nohead"))
            {
                //Print version info. If --nohead is on, then the header info for the interpreter is skipped.
                if (args.Length < 1)
                {
                    PrintVersionInfo();
                    Console.WriteLine("Starting interpreter...\n");
                }

                //Create a visitor.
                if (visitor == null)
                {
                    visitor = new algoVisitor();

                    //Load core library.
                    visitor.LoadCoreLibrary();
                }

                //Interactive interpreter.
                while (true)
                {
                    Console.Write(">> ");
                    string line = Console.ReadLine();

                    //Catch keywords and null strings.
                    if (line == "quit" || line == "exit" || line == "stop") { break; }
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
                    catch (Exception e) {
                        //Internal exception.
                        Error.Internal(e.Message);
                    }
                }
                
                return;
            }
            else if (args[0] == "-v")
            {
                //Print the version number.
                PrintVersionInfo();
                return;
            }
            else if (args[0] == "-c")
            {
                //Compile instruction. Args correct?
                if (args.Length != 2)
                {
                    Error.FatalNoContext("Invalid amount of arguments given for compile. Should be in format \"algo -c [file]\".");
                    return;
                }

                //Compile.
                ALEC.Compile(args[1]);
                return;
            }
            else if (args[0] == "pkg")
            {
                //Package management.
                Sharpie sharpie = new Sharpie(args.Slice(1, -1));
                return;
            }
            else if (args[0] == "help")
            {
                //Print help, with the rest of the arguments.
                if (args.Length > 1)
                {
                    PrintHelp(args.Slice(1, args.Length));
                }
                else
                {
                    PrintHelp();
                }
                return;
            }

            //Does the given file location exist?
            string fullPath = CPFilePath.GetPlatformFilePath(new string[] { Environment.CurrentDirectory, args[0] });
            if (!File.Exists(fullPath)) 
            {
                Error.FatalNoContext("No file with the name '" + args[0] + "' exists relative to your current directory.");
                return;
            }

            //Loading in the file arguments.
            algoVisitor.SetConsoleArguments(args.Skip(1).ToArray());
            
            //Read in the input.
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
            visitor.LoadCoreLibrary();
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
            Console.WriteLine("Algo Language Interpreter v" + MAJOR_VER + "." + MINOR_VER + "." + verInfo[2] + ", build " + verInfo[3] + ". (" +
                                typeof(Program).Assembly.GetName().GetVersionName() + ")");
            Console.WriteLine("(c) Larry Tang, 2019-" + DateTime.Now.Year);
            Console.WriteLine("\nFor information on how to use this interpreter, enter 'algo help'.");
        }

        //Prints the help page for Algo.
        private static void PrintHelp(string[] args = null)
        {
            //If there's an argument, just pass along and return.
            if (args != null && args.Length > 0)
            {
                switch (args[0])
                {
                    case "1":
                        break;
                    case "2":
                        Console.WriteLine("For detailed information on a command, visit \"algo man [cmd]\".");
                        Console.WriteLine("------------");
                        Console.WriteLine("BASIC SYNTAX");
                        Console.WriteLine("let x = y; - Sets the variable x to value y.");
                        Console.WriteLine("x = y; - Changes the existing value of x to y.");
                        Console.WriteLine("disregard [x | *]; - Deletes one or all variables from memory.");
                        Console.WriteLine("print x; - Prints the value of x to console.");
                        Console.WriteLine("x++; - Increments the value of x.");
                        Console.WriteLine("x--; - Decrements the value of x.");
                        Console.WriteLine("x(); - Call the function x.");
                        Console.WriteLine("let x() = { ... } - Creates a function x, running what's inside the braces.");
                        Console.WriteLine("let x = enum { val1, val2, ... } - Creates an enum x, with set values.");
                        Console.WriteLine("let x = object { }; - Create an object with values defined inside the braces.");
                        Console.WriteLine("------------");
                        Console.WriteLine("THIS IS NOT A COMPLETE LIST OF SYNTAX!");
                        Console.WriteLine("SEE THE WIKI BELOW FOR A COMPREHENSIVE GUIDE.");
                        Console.WriteLine("http://github.com/c272/algo-lang/wiki/");
                        return;
                    default:
                        Console.WriteLine("Invalid page number for help.");
                        return;
                }
            }

            //Print the default help page.
            Console.WriteLine("For detailed information on a command, visit \"algo man [cmd]\".");
            Console.WriteLine("------------");
            Console.WriteLine("CLI FLAGS");
            Console.WriteLine("[I] = Independent (Must be used on its own)");
            Console.WriteLine();
            Console.WriteLine("--nohead : [I] Removes the version information when running the interpreter.");
            Console.WriteLine("-v : Displays the version information for this build of Algo.");
            Console.WriteLine("------------");
            Console.WriteLine("PACKAGE MANAGER");
            Console.WriteLine();
            Console.WriteLine("pkg : Displays the package manager's version information."); 
            Console.WriteLine("pkg add [x] : Installs a package for global use.");
            Console.WriteLine("pkg remove [x] : Removes an installed package from global use.");
            Console.WriteLine("pkg sources : Lists the current package sources.");
            Console.WriteLine("pkg sources add [x] : Adds a package source to the master list.");
            Console.WriteLine("pkg sources remove [x] : Removes a package source from the master list.");
            Console.WriteLine("------------");
            Console.WriteLine("To display more pages, use algo help [num].");
        }

        //Prints the manual for a specific command.
        private static void PrintManual(string man)
        {
            Console.WriteLine("The manual for " + man + " is not currently available.");
        }
    }
}
