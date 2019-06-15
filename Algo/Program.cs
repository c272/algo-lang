using Antlr4;
using Antlr4.Runtime;
using System;
using System.Collections.Generic;
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

           // Console.WriteLine(new BigRational(new Fraction(new BigInteger(64), new BigInteger(32))).ToString());
            
            //Test input string.
            string input = "print \"What a gamer, number is \" + (3 + 1);";
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

            //Walking the tree.
            var visitor = new algoVisitor();
            visitor.VisitCompileUnit(tree);
        }
    }
}
