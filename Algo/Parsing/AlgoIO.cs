using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;
using ExtendedNumerics;
using System.Numerics;

namespace Algo
{
    /// <summary>
    /// All I/O handlers for Algo are kept here.
    /// </summary>
    public partial class algoVisitor : algoBaseVisitor<object>
    {
        public override object VisitStat_print([NotNull] algoParser.Stat_printContext context)
        {
            //Evaluate the expression.
            AlgoValue toPrint = (AlgoValue)VisitExpr(context.expr());

            //Depending on the type, print accordingly via. casts.
            switch (toPrint.Type)
            {
                case AlgoValueType.Float:
                    Console.WriteLine(((BigFloat)toPrint.Value).ToString());
                    break;
                case AlgoValueType.Integer:
                    Console.WriteLine(((BigInteger)toPrint.Value).ToString());
                    break;
                case AlgoValueType.Rational:
                    Console.WriteLine(((BigRational)toPrint.Value).ToString());
                    break;
                case AlgoValueType.String:
                    Console.WriteLine((string)toPrint.Value);
                    break;
                default:
                    Error.Fatal(context, "Invalid type to print, cannot print a value of type '" + toPrint.Type + "'.");
                    return null;
            }

            //Return.
            return null;
        }
    }
}
