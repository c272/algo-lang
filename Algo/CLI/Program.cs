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
using Algo.CLI;
using CommandLine;

namespace Algo
{
    class Program
    {
        //The tree traversal class for Algo.
        public static algoVisitor visitor = null;

        //The current version of Algo. (vX.X.BUILD)
        private const int MAJOR_VER = 0;
        private const int MINOR_VER = 4;
        private static CommandLine.Parser CLIParser;

        public static int Main(string[] args)
        {
            //Check that all the necessary directories exist.
            CPFilePath.CreateDefaultDirectories();

            //Create the CLI parser.
            CLIParser = new CommandLine.Parser(with => with.AutoVersion = false);

            //Based on the given command line flags, execute.
            if (args[0] == "pkg")
            {
                return CLIParser.ParseArguments<PackageManagerCLIOptions>(args)
                    .MapResult(
                        (PackageManagerCLIOptions opts) => { new Sharpie(opts); return 0; },
                        errs => HandleCLIParseErrors(errs)
                    );
            }
            else
            {
                return CommandLine.Parser.Default.ParseArguments<InlineCLIOptions>(args)
                    .MapResult(
                        (InlineCLIOptions opts) =>  RunAlgoInline(opts, args),
                        errs => HandleCLIParseErrors(errs)
                    );
            }
        }

        /// <summary>
        /// Runs Algo in "inline" mode (no package manager, etc). Returns an exit code.
        /// </summary>
        private static int RunAlgoInline(InlineCLIOptions opts, string[] originalArgs)
        {
            //Set developer mode on if necessary.
            if (opts.DeveloperMode)
            {
                AlgoRuntimeInformation.DeveloperMode = true;
            }

            //Displaying any generic info and then shutting off?
            if (opts.ShowVersionOnly) { PrintVersionInfo(); return 0; }

            //Compiling a file?
            if (opts.Compile != null)
            {
                //Attempt to compile.
                ALEC.Compile(opts.Compile);
                return 0;
            }

            //Running the script interpreter, or the live interpreter?
            if (opts.ScriptFile == null)
            {
                //Run live.
                //Print version info. If --nohead is on, then the header info for the interpreter is skipped.
                if (!opts.NoHeader)
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
                    if (line == "help") { PrintHelp(); continue; }
                    if (line == "clear") { Console.Clear(); continue; }
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
                    catch (Exception e)
                    {
                        //Internal exception.
                        Error.Internal(e.Message);
                    }
                }

                return 0;
            }
            else
            {
                //Run normal script.
                //Does the given file location exist?
                string fullPath = CPFilePath.GetPlatformFilePath(new string[] { Environment.CurrentDirectory, opts.ScriptFile });
                if (!File.Exists(fullPath))
                {
                    Error.FatalNoContext("No file with the name '" + opts.ScriptFile + "' exists relative to your current directory.");
                    return -1;
                }

                //Loading in the file arguments.
                List<string> args = originalArgs.ToList();
                args.RemoveAll(x => x.StartsWith("-"));
                algoVisitor.SetConsoleArguments(args.Skip(1).ToArray());

                //Read in the input.
                AlgoRuntimeInformation.FileLoaded = opts.ScriptFile;
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

                return 0;
            }
        }

        /// <summary>
        /// Prints the help screen.
        /// </summary>
        private static void PrintHelp()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Handles any CLI parse errors, and then returns an exit code.
        /// </summary>
        private static int HandleCLIParseErrors(IEnumerable<CommandLine.Error> errs)
        {
            List<string> errsToPrint = new List<string>();
            foreach (var err in errs)
            {
                switch (err.Tag)
                {
                    case ErrorType.BadVerbSelectedError:
                        errsToPrint.Add("Invalid verb given, must be 'pkg' or null.");
                        break;
                    case ErrorType.BadFormatConversionError:
                        errsToPrint.Add("Invalid type given for argument, see documentation or 'help' for details.");
                        break;
                    case ErrorType.MissingRequiredOptionError:
                        errsToPrint.Add("A required option is missing. Please see 'help' for the correct command format.");
                        break;
                    case ErrorType.MissingValueOptionError:
                        errsToPrint.Add("Expected a value, but it was missing. See 'help' for the command line format.");
                        break;
                    case ErrorType.RepeatedOptionError:
                        errsToPrint.Add("You've repeated an option in the arguments. See 'help' for the command line format.");
                        break;
                    case ErrorType.SequenceOutOfRangeError:
                        errsToPrint.Add("The provided sequence of parameters is out of range. See 'help' for the command line format.");
                        break;
                    case ErrorType.UnknownOptionError:
                        errsToPrint.Add("The option you provided does not exist. See 'help' for the command line format.");
                        break;
                    case ErrorType.HelpRequestedError:
                        break; //Ignore help requests.
                    case ErrorType.VersionRequestedError:
                        break; //Ignore version requests.
                    default:
                        errsToPrint.Add("Unknown error, please create an issue at github.com/c272/algo-lang.");
                        break;
                }
            }

            if (errsToPrint.Count != 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("There were some errors parsing your console arguments:");
                foreach (var err in errs)
                {
                    Console.WriteLine(err);
                }
                Console.ResetColor();
            }

            return -1;
        }

        //Prints the current version info of Algo.
        private static void PrintVersionInfo()
        {
            string[] verInfo = typeof(Program).Assembly.GetName().Version.ToString().Split('.');
            Console.WriteLine("Algo Language Interpreter v" + MAJOR_VER + "." + MINOR_VER + "." + verInfo[2] + ", build " + verInfo[3] + ". (" +
                                typeof(Program).Assembly.GetName().GetVersionName() + ")");
            Console.WriteLine("(c) Larry Tang, 2019-" + DateTime.Now.Year);
            Console.WriteLine("\nFor information on how to use this interpreter, use the 'help' command.");
        }
    }
}
