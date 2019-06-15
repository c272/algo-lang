using System;
using System.Collections.Generic;

namespace Algo
{
    //A single function as represented in Algo.
    public class AlgoFunction
    {
        public AlgoFunction(List<algoParser.StatementContext> body, List<string> parameters, string name)
        {
            Body = body;
            Parameters = parameters;
            Name = name;
        }

        public List<algoParser.StatementContext> Body;
        public List<string> Parameters;
        public string Name;
    }
}