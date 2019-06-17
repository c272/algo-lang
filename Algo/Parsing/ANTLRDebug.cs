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

        //Print all scopes available in Algo.
        public static void PrintScopes()
        {
            //Printing variable scopes.
            Console.WriteLine("\nGlobal Namespace");
            Console.WriteLine("SCOPES:");
            var scopes = algoVisitor.Scopes.GetScopes();
            for (int i = 0; i < scopes.Count; i++)
            {
                Console.WriteLine("Scope " + (i + 1) + "\n---");
                foreach (var variable in scopes[i])
                {
                    Console.WriteLine(variable.Key + " of type " + variable.Value.Type.ToString() + ", value " + variable.Value.Value.ToString() + ".");
                }
                Console.WriteLine("");
            }

            //Printing library scopes.
            var libs = algoVisitor.Scopes.GetLibraries();
            foreach (var lib in libs)
            {
                Console.WriteLine("LIBRARY '" + lib.Key + "'");
                var libscopes = lib.Value.GetScopes();
                for (int i = 0; i < libscopes.Count; i++)
                {
                    Console.WriteLine("Scope " + (i + 1) + "\n---");
                    foreach (var variable in libscopes[i])
                    {
                        Console.WriteLine(variable.Key + " of type " + variable.Value.Type.ToString() + ", value " + variable.Value.Value.ToString() + ".");
                    }
                    Console.WriteLine("");
                }
            }
        }

        public static void PrintParseList(algoParser.CompileUnitContext tree, algoParser parser)
        {
            //Printing parse tree.
            Console.WriteLine("ANTLR Parse Tree:");
            Console.WriteLine(tree.ToStringTree(parser));
            Console.WriteLine("-\nStatement Length: " + tree.block().statement().Length);
            Console.WriteLine("");
        }
    }
}