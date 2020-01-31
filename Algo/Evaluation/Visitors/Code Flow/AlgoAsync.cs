using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;

namespace Algo
{
    /// <summary>
    /// Contains all visitor nodes that handle asynchronous operations.
    /// These asynchronous operations are quite unstable, so please don't use in production until they're polished up.
    /// </summary>
    public partial class algoVisitor : algoBaseVisitor<object>
    {
        /// <summary>
        /// When a function is called as an asynchronous fire and forget.
        /// </summary>
        public override object VisitStat_asyncFunctionCall([NotNull] algoParser.Stat_asyncFunctionCallContext context)
        {
            //Evaluate the value to use as the exit function.
            AlgoValue exitFunc = Particles.ParseParticleBlock(this, context, context.IDENTIFIER(), context.particle());

            //Valid function?
            if (exitFunc.Type != AlgoValueType.EmulatedFunction || exitFunc.Type != AlgoValueType.Function)
            {
                Error.Fatal(context, "Invalid value specified as exit function, value was not a function.");
                return null;
            }

            //Valid amount of parameters?
            if (exitFunc.Type == AlgoValueType.EmulatedFunction)
            {
                var func = (AlgoPluginFunction)exitFunc.Value;
                if (func.ParameterCount > 1)
                {
                    Error.Fatal(context, "Invalid parameter count, async exit functions can only have one or zero parameters.");
                    return null;
                }
            }
            else
            {
                var func = (AlgoFunction)exitFunc.Value;
                if (func.Parameters.Count > 1)
                {
                    Error.Fatal(context, "Invalid parameter count, async exit functions can only have one or zero parameters.");
                    return null;
                }
            }

            //Use a backgroundWorker to call the normal function.
            var bw = new BackgroundWorker();
            bw.DoWork += executeAsyncFunction;
            bw.RunWorkerCompleted += executeAsyncExitFunc;
            bw.RunWorkerAsync(new AsyncCallWrapper()
            {
                ExitFunction = exitFunc,
                AsyncValue = null,
                FuncCall = context.stat_functionCall(),
                FunctionCtx = context
            });
        }

        /// <summary>
        /// Executes the provided Algo asynchronous function.
        /// </summary>
        private void executeAsyncFunction(object sender, DoWorkEventArgs e)
        {
            var args = (AsyncCallWrapper)e.Argument;

            //Visit and execute.
            var result = VisitStat_functionCall(args.FuncCall);

            //Return result.
            if (result == null)
            {
                args.AsyncValue = null;
            }
            else
            {
                args.AsyncValue = (AlgoValue)result;
            }
        }

        /// <summary>
        /// Executes when an async function is completed, 
        /// </summary>
        private void executeAsyncExitFunc(object sender, RunWorkerCompletedEventArgs e)
        {
            //Call exit function from e.Result.
            var args = (AsyncCallWrapper)e.Result;
            
            //Attempt to call exit function.
            if (args.ExitFunction.Type == AlgoValueType.EmulatedFunction)
            {
                var func = (AlgoPluginFunction)args.ExitFunction.Value;
                if (func.ParameterCount == 0) { func.Function(args.FunctionCtx); }
                //todo
            }
        }
        
    }

    /// <summary>
    /// Asynchronous function call data wrapper.
    /// </summary>
    public struct AsyncCallWrapper
    {
        public algoParser.Stat_functionCallContext FuncCall;
        public AlgoValue ExitFunction;
        public AlgoValue AsyncValue;
        public ParserRuleContext FunctionCtx;
    }
}
