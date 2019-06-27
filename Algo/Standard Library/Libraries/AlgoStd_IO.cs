using System;
using System.Collections.Generic;
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
    }
}
