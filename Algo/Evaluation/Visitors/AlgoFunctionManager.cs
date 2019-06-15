using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;

namespace Algo
{
    /// <summary>
    /// Contains all the function management visitor nodes.
    /// </summary>
    public partial class algoVisitor : algoBaseVisitor<object>
    {
        //When a function is defined.
        public override object VisitStat_functionDef([NotNull] algoParser.Stat_functionDefContext context)
        {
            //Does a variable with this name already exist?
            if (Scopes.VariableExistsLowest(context.IDENTIFIER().GetText()))
            {
                Error.Fatal(context, "A variable with this name already exists, cannot create function with this name.");
                return null;
            }

            //Getting parameters.
            List<string> params_ = new List<string>();
            foreach (var param in context.abstract_params().IDENTIFIER())
            {
                params_.Add(param.GetText());
            }

            //No, it doesn't exist. Define it.
            AlgoFunction func = new AlgoFunction(context.statement().ToList(), params_, context.IDENTIFIER().GetText());
            AlgoValue funcValue = new AlgoValue()
            {
                Type = AlgoValueType.Function,
                Value = func,
                IsEnumerable = false
            };

            //Add to scope.
            Scopes.AddVariable(context.IDENTIFIER().GetText(), funcValue);
            return null;
        }

        //When a function is called.
        public override object VisitStat_functionCall([NotNull] algoParser.Stat_functionCallContext context)
        {
            //Check if a function variable exists with this name.
            if (!Scopes.VariableExists(context.IDENTIFIER().GetText()))
            {
                Error.Fatal(context, "No function with name '" + context.IDENTIFIER().GetText() + "' exists.");
                return null;
            }

            //Get the variable, check if it's a function.
            AlgoValue value = Scopes.GetVariable(context.IDENTIFIER().GetText());
            if (value.Type != AlgoValueType.Function)
            {
                Error.Fatal(context, "The variable with name '" + context.IDENTIFIER().GetText() + "' is not a function, so can't be called like one.");
                return null;
            }

            //It is, get the function.
            AlgoFunction funcToCall = (AlgoFunction)value.Value;

            //Getting parameter length.
            int paramLength = 0;
            if (context.literal_params() != null)
            {
                paramLength = context.literal_params().expr().Length;
            }

            //Check the parameter length is the same.
            if (paramLength != funcToCall.Parameters.Count)
            {
                Error.Fatal(context, paramLength + " parameters passed to function " + funcToCall.Name + ", which expects " + funcToCall.Parameters.Count);
                return null;
            }

            //Parse all the parameters.
            List<AlgoValue> paramvalues = new List<AlgoValue>();
            foreach (var param in context.literal_params().expr())
            {
                AlgoValue evaluated = (AlgoValue)VisitExpr(param);
                paramvalues.Add(evaluated);
            }

            //Adding a scope, and creating the parameters inside it.
            Scopes.AddScope();
            for (int i=0; i<paramvalues.Count; i++)
            {
                Scopes.AddVariable(funcToCall.Parameters[i], paramvalues[i]);
            }

            //Running the function's body.
            foreach (var statement in funcToCall.Body)
            {
                AlgoValue returned = (AlgoValue)VisitStatement(statement);
                if (returned != null)
                {
                    return returned;
                }
            }

            //Remove the function's scope, we're done!
            Scopes.RemoveScope();
            return null;
        }

        //When a value is returned from a function.
        public override object VisitStat_return([NotNull] algoParser.Stat_returnContext context)
        {
            //Evaluate the expression to return.
            AlgoValue toReturn = (AlgoValue)VisitExpr(context.expr());

            //Return it.
            return toReturn;
        }
    }
}