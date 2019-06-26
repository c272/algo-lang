using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Algo.StandardLibrary
{
    //Additional methods for std_core, commonly used functions that aren't part of the inhrent feature set of Algo.
    //eg. Not print, since that technically isn't a function.
    public class AlgoStd_Core : IFunctionPlugin
    {
        public string Name { get; set; } = "std_core";

        public List<AlgoPluginFunction> Functions { get; set; } = new List<AlgoPluginFunction>()
        {
            //*.len
            new AlgoPluginFunction()
            {
                Name = "len",
                ParameterCount = 1,
                Function = Length
            }
        };

        //Returns the length of a list or string.
        public static AlgoValue Length(ParserRuleContext context, params AlgoValue[] args)
        {
            //Is the value a list or a string?
            if (args[0].Type != AlgoValueType.List && args[0].Type != AlgoValueType.String)
            {
                Error.Fatal(context, "Cannot get the length of values with type '" + args[0].Type.ToString() + "'.");
                return null;
            }

            //It's one of those, return depending on the type.
            if (args[0].Type == AlgoValueType.List)
            {
                return new AlgoValue()
                {
                    Type = AlgoValueType.Integer,
                    Value = new BigInteger(((List<AlgoValue>)args[0].Value).Count)
                };
            } else
            {
                return new AlgoValue()
                {
                    Type = AlgoValueType.Integer,
                    Value = new BigInteger(((string)args[0].Value).Length)
                };
            }
        }

        //Convert values with static casts.
        //String.
        public static AlgoValue ConvertString(ParserRuleContext context, params AlgoValue[] args)
        {
            return null;
        }

        //Integer.
        public static AlgoValue ConvertInt(ParserRuleContext context, params AlgoValue[] args)
        {
            return null;
        }

        //Float.
        public static AlgoValue ConvertFlt(ParserRuleContext context, params AlgoValue[] args)
        {
            return null;
        }

        //Rational.
        public static AlgoValue ConvertRat(ParserRuleContext context, params AlgoValue[] args)
        {
            return null;
        }

        //Bool.
        public static AlgoValue ConvertBool(ParserRuleContext context, params AlgoValue[] args)
        {
            return null;
        }
    }
}
