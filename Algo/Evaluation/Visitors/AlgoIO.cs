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

                //Check it's an integer.
                if (roundingNum.Type != AlgoValueType.Integer)
                {
                    Error.Warning(context, "Rounding value given wasn't an integer, so it was ignored.");
                }
                else if ((BigInteger)roundingNum.Value <= 0)
                {
                    Error.Warning(context, "Rounding value given less than one, so it was ignored.");
                }
                else if (toPrint.Type == AlgoValueType.String)
                {
                    //Cannot round strings.
                    Error.Warning(context, "Cannot round a string, so rounding was ignored.");
                }
                else
                {
                    //Print rounded value.
                    //todo: make this take account of rounding.
                    int amtSF = 0;
                    bool pointReached = false;
                    foreach (char c in printString)
                    {
                        if (c != '.' && c != '0' && c != '-')
                        {
                            amtSF++;
                        } else if (c=='.')
                        {
                            pointReached = true;
                        }
                        Console.Write(c);

                        //Reached the required SF.
                        if (amtSF == (BigInteger)roundingNum.Value)
                        {
                            if (!pointReached)
                            {
                                Console.Write('0');
                            } else
                            {
                                break;
                            }
                        }
                    }

                    //Return.
                    return null;
                }
            }

            //Print and return.
            Console.WriteLine(printString);
            return null;
        }
    }
}
