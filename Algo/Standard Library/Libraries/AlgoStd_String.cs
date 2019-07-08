using Antlr4.Runtime;
using ExtendedNumerics;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace Algo.StandardLibrary
{
    //Additional methods required for the "string" standard library.
    public class AlgoStd_String : IFunctionPlugin
    {
        public string Name { get; set; } = "std_string";
        public List<AlgoPluginFunction> Functions { get; set; } = new List<AlgoPluginFunction>
        {
            //ToCharArray
            new AlgoPluginFunction()
            {
                Name = "toCharArray",
                ParameterCount = 1,
                Function = ToCharArray
            },

            //Contains
            new AlgoPluginFunction()
            {
                Name = "contains",
                ParameterCount = 2,
                Function = ContainsString
            },

            //Split
            new AlgoPluginFunction()
            {
                Name = "split",
                ParameterCount = 2,
                Function = SplitString
            },

            //Replace
            new AlgoPluginFunction()
            {
                Name = "replace",
                ParameterCount = 3,
                Function = ReplaceSubstring
            },

            //Substring.
            new AlgoPluginFunction()
            {
                Name = "substring",
                ParameterCount = 3,
                Function = GetSubstring
            },

            //EndsWith
            new AlgoPluginFunction()
            {
                Name = "endsWith",
                ParameterCount = 2,
                Function = StringEndsWith
            },

            //IsInteger
            new AlgoPluginFunction()
            {
                Name = "isInteger",
                ParameterCount = 1,
                Function = IsInteger
            },

            //IsFloat
            new AlgoPluginFunction()
            {
                Name = "isFloat",
                ParameterCount = 1,
                Function = IsFloat
            }
        };

        //Returns a character array of the string.
        public static AlgoValue ToCharArray(ParserRuleContext context, params AlgoValue[] args)
        {
            //Get the first argument, check the value is a string.
            if (args[0].Type != AlgoValueType.String)
            {
                Error.Fatal(context, "Cannot convert a non-string to a character array.");
                return null;
            }

            //Get the string, convert to character array.
            char[] chars = ((string)args[0].Value).ToCharArray();

            //Convert back to string array.
            List<string> str_chars = new List<string>();
            foreach (var char_ in chars)
            {
                str_chars.Add(new string(new char[] { char_ }));
            }

            //Represent this as a list of Algo values.
            List<AlgoValue> algo_chars = new List<AlgoValue>();
            foreach (var str in str_chars)
            {
                algo_chars.Add(new AlgoValue()
                {
                    Type = AlgoValueType.String,
                    Value = str
                });
            }

            //Return a list of characters.
            return new AlgoValue()
            {
                Type = AlgoValueType.List,
                Value = algo_chars
            };
        }

        //Returns whether the given string containsa specific substring.
        public static AlgoValue ContainsString(ParserRuleContext context, params AlgoValue[] args)
        {
            //Get the base string and the substring.
            string baseString = AlgoConversion.GetStringRepresentation(context, args[0]);
            string substring = AlgoConversion.GetStringRepresentation(context, args[1]);

            //Return a bool.
            return new AlgoValue()
            {
                Type = AlgoValueType.Boolean,
                Value = baseString.Contains(substring)
            };
        }

        //Splits a given string into an Algo array given a separator.
        public static AlgoValue SplitString(ParserRuleContext context, params AlgoValue[] args)
        {
            //Extract the string from the first value.
            if (args[0].Type != AlgoValueType.String)
            {
                Error.Fatal(context, "Cannot perform a string split on a non-string value.");
                return null;
            }
            string toSplit = (string)args[0].Value;

            //Get the split argument.
            if (args[1].Type != AlgoValueType.String)
            {
                Error.Fatal(context, "Cannot split by a separator that's not a string.");
                return null;
            }
            string separator = (string)args[1].Value;
            if (separator.Length != 1)
            {
                Error.Fatal(context, "Separator must be 1 character long.");
                return null;
            }

            //Splitting.
            string[] split = toSplit.Split(separator[0]);

            //For every string, create an algovalue and add to list.
            List<AlgoValue> strings = new List<AlgoValue>();
            foreach (var part in split)
            {
                strings.Add(new AlgoValue()
                {
                    Type = AlgoValueType.String,
                    Value = part
                });
            }

            //Return the list.
            return new AlgoValue()
            {
                Type = AlgoValueType.List,
                Value = strings
            };
        }

        //Replaces a given substring inside an Algo string with another.
        public static AlgoValue ReplaceSubstring(ParserRuleContext context, params AlgoValue[] args)
        {
            //Check the inputs are all strings.
            if (args[0].Type != AlgoValueType.String || args[1].Type != AlgoValueType.String || args[2].Type != AlgoValueType.String)
            {
                Error.Fatal(context, "All parameters to the 'replace' function must be of the type String.");
                return null;
            }

            //Replace.
            string original = (string)args[1].Value;
            string replaceWith = (string)args[2].Value;

            args[0].Value = ((string)args[0].Value).Replace(original, replaceWith);
            return args[0];
        }


        //Returns a substring of a string.
        public static AlgoValue GetSubstring(ParserRuleContext context, params AlgoValue[] args)
        {
            //Check argument is a string.
            if (args[0].Type != AlgoValueType.String)
            {
                Error.Fatal(context, "Cannot get a substring from a non-string value.");
                return null;
            }
            if (args[1].Type != AlgoValueType.Integer || args[2].Type != AlgoValueType.Integer)
            {
                Error.Fatal(context, "Bounds for the substring must be an integer.");
                return null;
            }

            //Check the bounds are above zero, and within bounds.
            BigInteger lowerBound = (BigInteger)args[1].Value;
            BigInteger len = (BigInteger)args[2].Value;
            string val = (string)args[0].Value;

            if (lowerBound < 0 || len < 0)
            {
                Error.Fatal(context, "Start index and length for the substring must be greater than zero.");
                return null;
            }
            if (lowerBound >= val.Length || len >= val.Length || lowerBound > int.MaxValue || len > int.MaxValue)
            {
                Error.Fatal(context, "Start index and length for the substring must be smaller than the length of the string, and less than max 32 bit integer limit.");
                return null;
            }

            //Return substring.
            return new AlgoValue()
            {
                Type = AlgoValueType.String,
                Value = val.Substring((int)lowerBound, (int)len)
            };
        }

        //Returns whether a string value ends with a specific substring.
        public static AlgoValue StringEndsWith(ParserRuleContext context, params AlgoValue[] args)
        {
            //Arguments are strings?
            if (args[0].Type != AlgoValueType.String || args[1].Type != AlgoValueType.String)
            {
                Error.Fatal(context, "Source and substring must both be of type String.");
                return null;
            }

            string source = (string)args[0].Value;
            string end = (string)args[1].Value;
            return new AlgoValue()
            {
                Type = AlgoValueType.Boolean,
                Value = source.EndsWith(end)
            };
        }

        //Check if a string is a valid integer.
        public static AlgoValue IsInteger(ParserRuleContext context, params AlgoValue[] args)
        {
            //Check input type.
            if (args[0].Type == AlgoValueType.Integer) { return AlgoValue.True; }
            if (args[0].Type != AlgoValueType.String) { return AlgoValue.False; }

            //It's a string, check if it can be parsed to an integer.
            try
            {
                BigInteger b = BigInteger.Parse((string)args[0].Value);
                return AlgoValue.True;
            } catch
            {
                return AlgoValue.False;
            }
        }

        //Check if a string is a valid float.
        public static AlgoValue IsFloat(ParserRuleContext context, params AlgoValue[] args)
        {
            //Check input type.
            if (args[0].Type == AlgoValueType.Float) { return AlgoValue.True; }
            if (args[0].Type != AlgoValueType.String) { return AlgoValue.False; }

            //It's a string, check if it can be parsed to an integer.
            try
            {
                BigFloat b = BigFloat.Parse((string)args[0].Value);
                return AlgoValue.True;
            }
            catch
            {
                return AlgoValue.False;
            }
        }
    }
}
