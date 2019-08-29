using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algo
{
    //A collection of all templates required for compiling with ALEC.
    public static class ALECTemplates
    {
        public static string ALECEntryPoint = @"
            using System;
            using Algo;
            using Antlr4;
            using Antlr4.Runtime;

            namespace ALECProgram
            {
                class Program
                {
                    public static void Main(string[] args)
                    {
                        //Parse the script.
                        var s_chars = new AntlrInputStream(@""[CUSTOM-CODE-HERE]"");
                        var s_lexer = new algoLexer(s_chars);
                        var s_tokens = new CommonTokenStream(s_lexer);
                        var s_parse = new algoParser(s_tokens);
                        var visitor = new algoVisitor();

                        //Load console arguments.
                        algoVisitor.SetConsoleArguments(args);

                        //Execute.
                        s_parse.BuildParseTree = true;
                        var s_tree = s_parse.compileUnit();
                        visitor.VisitCompileUnit(s_tree);
                    }
                }
            }
        ";
    }
}
