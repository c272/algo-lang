using System;
using System.Collections.Generic;
using System.Numerics;
using Antlr4.Runtime;
using ExtendedNumerics;
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
            },

            //json.make();
            new AlgoPluginFunction()
            {
                Name = "make",
                Function = ToJson,
                ParameterCount = 1
            }
        };

        /// <summary>
        /// Converts an Algo object to JSON format (string).
        /// </summary>
        public static AlgoValue ToJson(ParserRuleContext context, params AlgoValue[] args)
        {
    	    //Check the argument given is an object.
    	    if (args[0].Type != AlgoValueType.Object && args[0].Type != AlgoValueType.List)
    	    {
    		    Error.Fatal(context, "Value to serialize to JSON must be an object or list.");
    		    return null;
    	    }

            if (args[0].Type != AlgoValueType.List)
            {
                //Returning the object parsed string.
                return new AlgoValue()
                {
                    Type = AlgoValueType.String,
                    Value = AlgoConversion.ObjToJsonStr(context, args[0])
                };
            }
            else
            {
                //Returning the list parsed string.
                return new AlgoValue()
                {
                    Type = AlgoValueType.String,
                    Value = AlgoConversion.ListToJsonStr(context, args[0])
                };
            }
        }

        /// <summary>
        /// Converts a string in JSON format to an Algo object.
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
                returned = ParseJsonArray(context, deserialized);
            }

            return returned;
        }

        //Turns a JArray deserialized object into an AlgoValue.
        private static AlgoValue ParseJsonArray(ParserRuleContext context, dynamic deserialized)
        {
            //Enumerate over values.
            List<AlgoValue> list = new List<AlgoValue>();
            foreach (JToken token in ((JArray)deserialized).Children())
            {
                switch (token.Type)
                {
                    //Same parsing as below, but for a list.
                    case JTokenType.Integer:
                        list.Add(new AlgoValue()
                        {
                            Type = AlgoValueType.Integer,
                            Value = BigInteger.Parse(token.ToString())
                        });
                        break;
                    case JTokenType.String:
                        list.Add(new AlgoValue()
                        {
                            Type = AlgoValueType.String,
                            Value = token.ToString()
                        });
                        break;
                    case JTokenType.Array:
                        list.Add(ParseJsonArray(context, (JArray)token));
                        break;
                    case JTokenType.Boolean:
                        list.Add(new AlgoValue()
                        {
                            Type = AlgoValueType.Boolean,
                            Value = bool.Parse(token.ToString())
                        });
                        break;
                    case JTokenType.Float:
                        list.Add(new AlgoValue()
                        {
                            Type = AlgoValueType.Float,
                            Value = BigFloat.Parse(token.ToString())
                        });
                        break;
                    case JTokenType.Null:
                        list.Add(AlgoValue.Null);
                        break;
                    case JTokenType.Object:
                        list.Add(ParseJsonObject(context, (JObject)token));
                        break;
                }
            }

            return new AlgoValue()
            {
                Type = AlgoValueType.List,
                Value = list
            };
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
                    //JSON Integer Representation
                    case JTokenType.Integer:
                        obj.ObjectScopes.AddVariable(token.Name, new AlgoValue()
                        {
                            Type = AlgoValueType.Integer,
                            Value = BigInteger.Parse(token.Value.ToString())
                        });
                        break;

                    //JSON String Representation
                    case JTokenType.String:
                        obj.ObjectScopes.AddVariable(token.Name, new AlgoValue()
                        {
                            Type = AlgoValueType.String,
                            Value = token.Value.ToString()
                        });
                        break;

                    //JSON Array Representation
                    case JTokenType.Array:
                        obj.ObjectScopes.AddVariable(token.Name, ParseJsonArray(context, (JArray)token.Value));
                        break;

                    //JSON Boolean Representation
                    case JTokenType.Boolean:
                        obj.ObjectScopes.AddVariable(token.Name, new AlgoValue()
                        {
                            Type = AlgoValueType.Boolean,
                            Value = bool.Parse(token.Value.ToString())
                        });
                        break;

                    //JSON Float Representation
                    case JTokenType.Float:
                        obj.ObjectScopes.AddVariable(token.Name, new AlgoValue()
                        {
                            Type = AlgoValueType.Float,
                            Value = BigFloat.Parse(token.Value.ToString())
                        });
                        break;

                    //JSON Null Representation
                    case JTokenType.Null:
                        obj.ObjectScopes.AddVariable(token.Name, AlgoValue.Null);
                        break;

                    //JSON Object Representation
                    case JTokenType.Object:
                        obj.ObjectScopes.AddVariable(token.Name, ParseJsonObject(context, (JObject)token.Value));
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
