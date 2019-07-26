using Antlr4.Runtime;
using ExtendedNumerics;
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
            },

            //str(x)
            new AlgoPluginFunction()
            {
                Name = "str",
                ParameterCount = 1,
                Function = ConvertString
            },

            //int(x)
            new AlgoPluginFunction()
            {
                Name = "int",
                ParameterCount = 1,
                Function = ConvertInt
            },

            //flt(x)
            new AlgoPluginFunction()
            {
                Name = "flt",
                ParameterCount = 1,
                Function = ConvertFlt
            },

            //rat(x)
            new AlgoPluginFunction()
            {
                Name = "rat",
                ParameterCount = 1,
                Function = ConvertRat
            },

            //bool(x)
            new AlgoPluginFunction()
            {
                Name = "bool",
                ParameterCount = 1,
                Function = ConvertBool
            },

            //var(x)
            new AlgoPluginFunction()
            {
                Name = "var",
                ParameterCount = 1,
                Function = GetVarByName
            },

            //type(x)
            new AlgoPluginFunction()
            {
                Name = "get_type",
                ParameterCount = 1,
                Function = GetType
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
            //Return converted string representation of the value.
            return new AlgoValue()
            {
                Type = AlgoValueType.String,
                Value = AlgoConversion.GetStringRepresentation(context, args[0])
            };
        }

        //Integer.
        public static AlgoValue ConvertInt(ParserRuleContext context, params AlgoValue[] args)
        {
            //Is it a string?
            if (args[0].Type == AlgoValueType.String)
            {
                //Return a parsed bigint.
                return new AlgoValue()
                {
                    Type = AlgoValueType.Integer,
                    Value = BigInteger.Parse((string)args[0].Value)
                };
            }

            //Nah, then just cast.
            return AlgoOperators.ConvertType(context, args[0], AlgoValueType.Integer);
        }

        //Float.
        public static AlgoValue ConvertFlt(ParserRuleContext context, params AlgoValue[] args)
        {
            //Is it a string?
            if (args[0].Type == AlgoValueType.String)
            {
                //Return a parsed bigfloat.
                return new AlgoValue()
                {
                    Type = AlgoValueType.Float,
                    Value = BigFloat.Parse((string)args[0].Value)
                };
            }

            //Nah, then just cast.
            return AlgoOperators.ConvertType(context, args[0], AlgoValueType.Float);
        }

        //Rational.
        public static AlgoValue ConvertRat(ParserRuleContext context, params AlgoValue[] args)
        {
            //Is it a string?
            if (args[0].Type == AlgoValueType.String)
            {
                //Return a parsed bigint.
                return new AlgoValue()
                {
                    Type = AlgoValueType.Rational,
                    Value = BigRational.Parse((string)args[0].Value)
                };
            }

            //Nah, then just cast.
            return AlgoOperators.ConvertType(context, args[0], AlgoValueType.Rational);
        }

        //Bool.
        public static AlgoValue ConvertBool(ParserRuleContext context, params AlgoValue[] args)
        {
            if (args[0].Type == AlgoValueType.String)
            {
                if ((string)args[0].Value != "true" && (string)args[0].Value != "false")
                {
                    throw new Exception("Invalid string given to convert to boolean.");
                }

                return new AlgoValue()
                {
                    Type = AlgoValueType.Boolean,
                    Value = bool.Parse((string)args[0].Value)
                };
            }

            return new AlgoValue()
            {
                Type = AlgoValueType.Boolean,
                Value = AlgoComparators.GetBooleanValue(args[0], context)
            };
        }

        //Var
        public static AlgoValue GetVarByName(ParserRuleContext context, params AlgoValue[] args)
        {
            //Check variable name is a string.
            if (args[0].Type != AlgoValueType.String)
            {
                Error.Fatal(context, "Cannot get variable with name of type '" + args[0].Type.ToString() + "', must be of type String.");
                return null;
            }

            //Check it exists.
            if (!algoVisitor.Scopes.VariableExists((string)args[0].Value))
            {
                //It doesn't, return null.
                return AlgoValue.Null;
            }

            //It does, return the variable.
            return algoVisitor.Scopes.GetVariable((string)args[0].Value);
        }

        //Returns the type of the variable.
        public static AlgoValue GetType(ParserRuleContext context, params AlgoValue[] args)
        {
            //Enums in Algo are represented by objects with integer members.
            return new AlgoValue()
            {
                Type = AlgoValueType.Integer,
                Value = new BigInteger((int)args[0].Type)
            };
        }
    }
}
