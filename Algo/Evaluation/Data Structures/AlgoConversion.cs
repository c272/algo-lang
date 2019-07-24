using Antlr4.Runtime;
using ExtendedNumerics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Algo
{
    public static class AlgoConversion
    {
        //Returns the string representation of a given Algo value.
        public static string GetStringRepresentation(ParserRuleContext context, AlgoValue toPrint)
        {
            string printString = "";
            switch (toPrint.Type)
            {
                case AlgoValueType.Null:
                    printString = "null";
                    break;
                case AlgoValueType.Float:
                    printString = ((BigFloat)toPrint.Value).ToString();
                    break;
                case AlgoValueType.Integer:
                    printString = ((BigInteger)toPrint.Value).ToString();
                    break;
                case AlgoValueType.Rational:
                    printString = ((BigRational)toPrint.Value).ToString();
                    break;
                case AlgoValueType.String:
                    printString = (string)toPrint.Value;
                    break;
                case AlgoValueType.List:

                    //Evaluate each of the list values one by one.
                    printString += '[';
                    foreach (var listVal in (List<AlgoValue>)toPrint.Value)
                    {
                        printString += GetStringRepresentation(context, listVal) + ", ";
                    }
                    printString = printString.Substring(0, printString.Length - 2);
                    printString += ']';
                    break;

                case AlgoValueType.Object:
                    printString += "{ ";
                    foreach (var prop in (Dictionary<string, AlgoValue>)((AlgoObject)(toPrint.Value)).ObjectScopes.GetScopes().Last())
                    {
                        printString += prop.Key + ": " + GetStringRepresentation(context, prop.Value) + ", ";
                    }
                    printString = printString.Trim(',', ' ');
                    printString += " }";
                    return printString;

                default:
                    Error.Fatal(context, "Invalid type, cannot convert a value of type '" + toPrint.Type + "' to string.");
                    return null;
            }

            return printString;
        }

        //Converts an Algo Rational to an Algo Float.
        public static BigFloat RationalToFloat(AlgoValue rational)
        {
            BigInteger numerator = ((BigRational)rational.Value).FractionalPart.Numerator;
            BigInteger denominator = ((BigRational)rational.Value).FractionalPart.Denominator;
            return BigFloat.Divide(new BigFloat(numerator), new BigFloat(denominator));
        }
    }
}
