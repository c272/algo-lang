using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Numerics;

namespace Algo.StandardLibrary
{
    //Methods for asynchronous operation that are not supported by Algo's core functionality.
    //Commonly used by other libraries for event handling, sleeping, multithreading.
    public class AlgoStd_Async : IFunctionPlugin
    {
        public string Name { get; set; } = "std_async";
        public List<AlgoPluginFunction> Functions { get; set; } = new List<AlgoPluginFunction>()
        {
            //async.sleep(ms)
            new AlgoPluginFunction()
            {
                Name = "sleep",
                ParameterCount = 1,
                Function = Sleep
            }
        };

        //Sleeps the current thread for a given amount of milliseconds.
        private static AlgoValue Sleep(ParserRuleContext context, AlgoValue[] args)
        {
            if (args[0].Type != AlgoValueType.Integer)
            {
                Error.Fatal(context, "Time to sleep the thread must be given as an integer (ms).");
                return null;
            }

            //Too large?
            if (((BigInteger)args[0].Value) > int.MaxValue)
            {
                Error.Fatal(context, "Cannot sleep a thread for that long, must be within the INT32 integer limit.");
                return null;
            }

            //Sleep the thread.
            Thread.Sleep((int)((BigInteger)args[0].Value));
            return null;
        }
    }
}
