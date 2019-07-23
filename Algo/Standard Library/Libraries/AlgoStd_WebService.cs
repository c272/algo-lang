using System;
using System.Collections.Generic;

namespace Algo.StandardLibrary
{
    /// <summary>
    /// A simple web server class for Algo.
    /// </summary>
    public class AlgoStd_Web : IFunctionPlugin
    {
        public string Name { get; set; }
        public List<AlgoPluginFunction> Functions { get; set; }
    }
}
