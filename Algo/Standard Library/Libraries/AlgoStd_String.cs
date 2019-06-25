using System;
using System.Collections.Generic;

namespace Algo.StandardLibrary
{
    //Additional methods required for the "string" standard library.
    public class AlgoStd_String : IFunctionPlugin
    {
        public string Name { get; set; } = "String";
        public List<AlgoPluginFunction> Functions { get; set; } = new List<AlgoPluginFunction>
        {

        };

        //Returns a character array of the string.
        public AlgoValue ToCharArray(params AlgoValue[] args)
        {
            //Get the first argument, check the value is a string.
            if (args[0].Type != AlgoValueType.String)
            {
                Error.FatalNoContext("Cannot convert a non-string to a character array.");
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
    }
}
