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
            //If there's an exit function, grab it.
            AlgoValue exitFunc = null;
            if (context.IDENTIFIER() != null)
            {
                //Evaluate the value to use as the exit function.
                exitFunc = Particles.ParseParticleBlock(this, context, context.IDENTIFIER(), context.particle());

                //Valid function?
                if (exitFunc.Type != AlgoValueType.EmulatedFunction && exitFunc.Type != AlgoValueType.Function)
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
            }

            //Get the function parameters for the called function, and pass in.
            List<AlgoValue> args = new List<AlgoValue>();
            if (context.stat_functionCall().functionCall_particle().literal_params() != null)
            {
                foreach (var param in context.stat_functionCall().functionCall_particle().literal_params().expr())
                {
                    var result = VisitExpr(param);
                    if (result == null)
                    {
                        Error.Fatal(context, "Failed to parse parameter of function, expression returned no value.");
                        return null;
                    }

                    args.Add((AlgoValue)result);
                }
            }

            //Run the async function.
            Task.Run(() =>
            {
                executeAsyncFunction(new AsyncCallWrapper()
                {
                    ExitFunction = exitFunc,
                    FuncCall = context.stat_functionCall(),
                    FunctionCtx = context,
                    Arguments = args,
                    CurrentScopes = Scopes
                });
            });

            //Return.
            return null;
        }

        /// <summary>
        /// Executes the provided Algo asynchronous function.
        /// </summary>
        private void executeAsyncFunction(AsyncCallWrapper args)
        {
            //Create the async function visitor.
            algoVisitor asyncVisitor = new algoVisitor();

            //Copy over the existing scope.
            asyncVisitor.Scopes = args.CurrentScopes;

            //Visit and execute.
            var result = asyncVisitor.VisitStat_functionCall(args.FuncCall);

            //Get result to pass to exit function.
            if (result == null)
            {
                args.AsyncValue = null;
            }
            else
            {
                args.AsyncValue = (AlgoValue)result;
            }

            //FUNCTION DONE!
            //If it exists, attempt to call the exit function.
            if (args.ExitFunction == null) { return; }
            if (args.ExitFunction.Type == AlgoValueType.EmulatedFunction)
            {
                var func = (AlgoPluginFunction)args.ExitFunction.Value;

                //If no params, can call straight away.
                if (func.ParameterCount == 0) { func.Function(args.FunctionCtx); }

                //Call with results.
                func.Function(args.FunctionCtx, args.AsyncValue);
            }
            else
            {
                //Normal function, execute as normal.
                var func = (AlgoFunction)args.ExitFunction.Value;

                //Create a new scope, add the parameter to it (if required)
                Scopes.AddScope();
                if (func.Parameters.Count > 0)
                {
                    Scopes.AddVariable(func.Parameters[0], args.AsyncValue, args.FunctionCtx);
                }

                //Running the function's body.
                foreach (var statement in func.Body)
                {
                    VisitStatement(statement);
                }

                //Remove the scope.
                Scopes.RemoveScope();
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
        public List<AlgoValue> Arguments;
        public AlgoScopeCollection CurrentScopes;
    }
}
