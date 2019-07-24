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

        //Converts an Algo Object to JSON string.
        public static string ObjToJsonStr(ParserRuleContext context, AlgoValue objVal)
        {
            //Enumerate over object members, and add them to the string.
            string json = "{ ";
            foreach (var prop in ((AlgoObject)objVal.Value).ObjectScopes.GetDeepestScope())
            {
                json += "\"" + prop.Key + "\": ";
                AppendValueString(context, prop.Value, ref json);
                json += ", ";
            }
            json = json.Trim(',', ' ') + " }";

            //Fix empty objects having a double space.
            if (json == "{  }") { json = "{}"; }

            //Return.
            return json;
        }

        public static void AppendValueString(ParserRuleContext context, AlgoValue prop, ref string json)
        {
            switch (prop.Type)
            {
                case AlgoValueType.Boolean:
                    json += ((bool)prop.Value).ToString();
                    break;
                case AlgoValueType.Float:
                    json += ((BigFloat)prop.Value).ToString();
                    break;
                case AlgoValueType.Integer:
                    json += ((BigInteger)prop.Value).ToString();
                    break;
                case AlgoValueType.List:
                    json += ListToJsonStr(context, prop);
                    break;
                case AlgoValueType.Null:
                    json += "null";
                    break;
                case AlgoValueType.Object:
                    json += ObjToJsonStr(context, prop);
                    break;
                case AlgoValueType.Rational:
                    AlgoValue rational_as_float = new AlgoValue()
                    {
                        Type = AlgoValueType.Float,
                        Value = RationalToFloat(prop)
                    };
                    json += rational_as_float.ToString();
                    break;
                case AlgoValueType.String:
                    json += "\"" + (string)prop.Value + "\"";
                    break;
            }
        }

        //Convert an Algo list to JSON.
        public static string ListToJsonStr(ParserRuleContext context, AlgoValue listVal)
        {
            //Enumerate over object members, and add them to the string.
            string json = "[";
            foreach (var prop in (List<AlgoValue>)listVal.Value)
            {
                AppendValueString(context, prop, ref json);
                json += ", ";
            }
            json = json.Trim(',', ' ') + "]";
            return json;
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
