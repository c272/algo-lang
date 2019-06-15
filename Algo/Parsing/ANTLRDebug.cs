using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4;
using Antlr4.Runtime;

namespace Algo
{
    public static class ANTLRDebug
    {
        public static void PrintTokens(Lexer lexer)
        {
            //Getting tokens.
            var tokens = lexer.GetAllTokens();

            //Getting lexer vocabulary.
            var vocab = lexer.Vocabulary;

            //Printing, for each token.
            Console.WriteLine("ANTLR Lexed Tokens:");
            foreach (var tok in tokens)
            {
                Console.WriteLine("[" + vocab.GetSymbolicName(tok.Type) + ", " + tok.Text + ", channel=" + tok.Channel + "]");
            }
            Console.WriteLine("");

            lexer.Reset();
        }

        public static void PrintParseList(algoParser.CompileUnitContext tree, algoParser parser)
        {
            //Printing debug string.
            Console.WriteLine("ANTLR Debug Parse:");
            Console.WriteLine(tree.ToInfoString(parser));
            Console.WriteLine("");

            //Printing parse tree.
            Console.WriteLine("ANTLR Parse Tree:");
            Console.WriteLine(tree.ToStringTree(parser));
            Console.WriteLine(tree.block().statement().Length);
        }
    }
}