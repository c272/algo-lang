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

            //Depending on the type, set string accordingly via. casts.
            string printString = "";
            printString = AlgoConversion.GetStringRepresentation(context, toPrint);

            //Check if a rounding expression is present.
            if (context.rounding_expr() != null)
            {
                //Evaluate the rounding expression.
                AlgoValue roundingNum = (AlgoValue)VisitExpr(context.rounding_expr().expr());

                //Check if it's an integer.
                if (roundingNum.Type != AlgoValueType.Integer)
                {
                    Error.Warning(context, "Given rounding expression is not an integer, so can't round. Rounding was ignored.");
                } else
                {
                    //Check if the rounding value is too large.
                    if ((BigInteger)roundingNum.Value > int.MaxValue)
                    {
                        Error.Warning(context, "Given rounding number is too large to process, so rounding was ignored.");
                    }
                    else
                    {
                        //Round the actual value.
                        int roundingInt = int.Parse(((BigInteger)roundingNum.Value).ToString());
                        toPrint = AlgoOperators.Round(context, toPrint, roundingInt);

                        //Set print string.
                        printString = AlgoConversion.GetStringRepresentation(context, toPrint);
                    }
                }
            }

            //Print and return.
            Console.WriteLine(printString);
            return null;
        }
    }
}
