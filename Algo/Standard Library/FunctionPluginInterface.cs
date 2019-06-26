using Antlr4.Runtime;
using System;
using System.Collections.Generic;

namespace Algo
{
    //An interface for classes that represent a plugin for Algo.
    public interface IFunctionPlugin
    {
        //The name of the plugin.
        string Name { get; set; }

        //A list of functions the plugin provides.
        List<AlgoPluginFunction> Functions { get; set; }
    }

    //The function delegate to be followed by all plugin functions.
    public delegate AlgoValue AlgoFunctionDelegate(ParserRuleContext context, params AlgoValue[] args);

    //Representation of a single plugin function.
    public struct AlgoPluginFunction
    {
        //The function delegate.
        public AlgoFunctionDelegate Function;

        //The amount of parameters the function has.
        public int ParameterCount;

        //The name of the function.
        public string Name;
    }
}