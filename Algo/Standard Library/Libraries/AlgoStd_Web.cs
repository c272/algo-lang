using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Numerics;
using Antlr4.Runtime;

namespace Algo.StandardLibrary
{
    /// <summary>
    /// A simple web server class for Algo.
    /// </summary>
    public class AlgoStd_Web : IFunctionPlugin
    {
        public string Name { get; set; } = "std_web";
        public List<AlgoPluginFunction> Functions { get; set; } = new List<AlgoPluginFunction>()
        {
            new AlgoPluginFunction()
            {
                Function = GET,
                Name = "get",
                ParameterCount = 1
            }
        };

        //Make a GET request to the given URL.
        public static AlgoValue GET(ParserRuleContext context, params AlgoValue[] args)
        {
            //Check the first argument is a string.
            if (args[0].Type != AlgoValueType.String)
            {
                Error.Fatal(context, "The URL to perform a GET request on must be of type string.");
                return null;
            }

            //Attempt to perform a GET request to this URL.
            string res = "";
            string stat = "";
            int code = -1;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create((string)args[0].Value);
                request.AutomaticDecompression = DecompressionMethods.GZip;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    res = reader.ReadToEnd();
                    stat = response.StatusDescription;
                    code = (int)response.StatusCode;
                }

            }
            catch (Exception e)
            {
                Error.Fatal(context, "An error occured when attempting to make a GET request: '" + e.Message + "'.");
                return null;
            }

            //Create the response object.
            AlgoScopeCollection obj = new AlgoScopeCollection();
            obj.AddVariable("content", new AlgoValue()
            {
                Type = AlgoValueType.String,
                Value = res
            });
            obj.AddVariable("status", new AlgoValue()
            {
                Type = AlgoValueType.Integer,
                Value = new BigInteger(code)
            });
            obj.AddVariable("status_desc", new AlgoValue()
            {
                Type = AlgoValueType.String,
                Value = stat
            });

            //Return the response object.
            return new AlgoValue()
            {
                Type = AlgoValueType.Object,
                Value = new AlgoObject()
                {
                    ObjectScopes = obj
                }
            };

        }
    }
}
