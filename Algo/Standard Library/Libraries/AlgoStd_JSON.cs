using System;
using System.Collections.Generic;
using System.Numerics;
using Antlr4.Runtime;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Algo.StandardLibrary
{
    public class AlgoStd_JSON : IFunctionPlugin
    {
        public string Name { get; set; } = "std_json";
        public List<AlgoPluginFunction> Functions { get; set; } = new List<AlgoPluginFunction>()
        {
            //json.parse();
            new AlgoPluginFunction()
            {
                Name = "parse",
                Function = FromJson,
                ParameterCount = 1
            }
        };

        /// <summary>
        /// Converts an Algo value to JSON format (string).
        /// </summary>
        public static AlgoValue ToJson(ParserRuleContext context, params AlgoValue[] args)
        {
            return null;
        }

        /// <summary>
        /// Converts a string in JSON format to an Algo value.
        /// </summary>
        public static AlgoValue FromJson(ParserRuleContext context, params AlgoValue[] args)
        {
            //Check the JSON being parsed is of type string.
            if (args[0].Type != AlgoValueType.String)
            {
                Error.Fatal(context, "Invalid JSON to parse, must be a string.");
                return null;
            }

            dynamic deserialized = null;
            try
            {
                if (((string)args[0].Value).StartsWith("{")) 
                {
                    deserialized = JObject.Parse((string)args[0].Value);
                }
                else
                {
                    deserialized = JArray.Parse((string)args[0].Value);
                }
            }
            catch(Exception e)
            {
                Error.Fatal(context, "Error occured when parsing JSON, '" + e.Message + "'.");
                return null;
            }

            //Enumerate over properties and parse.
            AlgoValue returned = null;
            if (((string)args[0].Value).StartsWith("{"))
            {
                returned = ParseJsonObject(context, deserialized);
            }
            else
            {
                returned = ParseJsonArray(deserialized);
            }

            return returned;
        }

        //Turns a JArray deserialized object into an AlgoValue.
        private static AlgoValue ParseJsonArray(dynamic deserialized)
        {
            throw new NotImplementedException();
        }

        //Turns a JObject deserialized object into an AlgoValue.
        private static AlgoValue ParseJsonObject(ParserRuleContext context, dynamic deserialized)
        {
            //Enumerate over properties.
            AlgoObject obj = new AlgoObject();
            foreach (JProperty token in ((JObject)deserialized).Properties())
            {
                switch (token.Value.Type)
                {
                    case JTokenType.Integer:
                        obj.ObjectScopes.AddVariable(token.Name, new AlgoValue()
                        {
                            Type = AlgoValueType.Integer,
                            Value = BigInteger.Parse(token.Value.ToString())
                        });
                        break;

                    case JTokenType.String:
                        obj.ObjectScopes.AddVariable(token.Name, new AlgoValue()
                        {
                            Type = AlgoValueType.String,
                            Value = token.Value.ToString()
                        });
                        break;

                    default:
                        Error.Fatal(context, "Invalid type '" + token.Value.Type.ToString() + "' to parse from JSON.");
                        return null;
                }
            }

            //Return the finished object.
            return new AlgoValue()
            {
                Type = AlgoValueType.Object,
                Value = obj
            };
        }
    }
}