using System;
using System.Collections.Generic;
using System.IO;
using Antlr4.Runtime;

namespace Algo.StandardLibrary
{
    /// <summary>
    /// The Algo standard library for IO. (Input grabbing, outputting to non-terminal devices)
    /// </summary>
    public class AlgoStd_IO : IFunctionPlugin
    {
        public string Name { get; set; } = "std_io";
        public List<AlgoPluginFunction> Functions { get; set; } = new List<AlgoPluginFunction>()
        {
            //input.get();
            new AlgoPluginFunction()
            {
                Name = "input_get",
                Function = GetConsoleInput,
                ParameterCount = 0
            },

            //input.fromFile();
            new AlgoPluginFunction()
            {
                Name = "input_fromFile",
                Function = GetInputFromFile,
                ParameterCount = 1
            },

            //output.toFile();
            new AlgoPluginFunction()
            {
                Name = "output_toFile",
                Function = OutputToFile,
                ParameterCount = 2
            }
        };

        //Getting input directly from the terminal.
        public static AlgoValue GetConsoleInput(ParserRuleContext context, params AlgoValue[] args)
        {
            //Return the string result of the input.
            return new AlgoValue()
            {
                Type = AlgoValueType.String,
                Value = Console.ReadLine()
            };
        }

        //Get input from file.
        //args[0] = file path
        public static AlgoValue GetInputFromFile(ParserRuleContext context, params AlgoValue[] args)
        {
            //Check if the parameter is actually a string.
            if (args[0].Type != AlgoValueType.String)
            {
                Error.Fatal(context, "File name to read from must be a string.");
                return null;
            }

            //Attempt to read from file.
            return new AlgoValue()
            {
                Type = AlgoValueType.String,
                Value = File.ReadAllText((string)args[0].Value)
            };
        }

        //Writing output to file.
        //args[0] = file path
        //args[1] = text to write
        public static AlgoValue OutputToFile(ParserRuleContext context, params AlgoValue[] args)
        {
            //CHeck if both parameters are strings.
            if (args[0].Type != AlgoValueType.String || args[1].Type != AlgoValueType.String)
            {
                Error.Fatal(context, "Invalid file name or text to output (both must be strings).");
                return null;
            }

            //Attempt to write to file.
            File.WriteAllText((string)args[0].Value, (string)args[1].Value);
            return new AlgoValue()
            {
                Type = AlgoValueType.Null,
                Value = null
            };
        }
    }
}
