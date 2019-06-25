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
            if (context.abstract_params() != null)
            {
                foreach (var param in context.abstract_params().IDENTIFIER())
                {
                    //Check if param already exists.
                    if (params_.Contains(param.GetText()))
                    {
                        Error.Fatal(context, "The parameter with name '" + param.GetText() + "' is already defined in the function.");
                        return null;
                    }

                    params_.Add(param.GetText());
                }
            }

            //No, it doesn't exist. Define it.
            AlgoFunction func = new AlgoFunction(context.statement().ToList(), params_, context.IDENTIFIER().GetText());
            AlgoValue funcValue = new AlgoValue()
            {
                Type = AlgoValueType.Function,
                Value = func
            };

            //Add to scope.
            Scopes.AddVariable(context.IDENTIFIER().GetText(), funcValue);
            return null;
        }

        //When a function is called.
        public override object VisitStat_functionCall([NotNull] algoParser.Stat_functionCallContext context)
        {
            //The function to call.
            AlgoValue funcToCall = null;

            //The library scope (if applicable).
            AlgoScopeCollection scopes_local = null;

            //Check if it's a nested variable.
            bool isVariable = false;
            AlgoScopeCollection objScope = null;
            if (context.obj_access() != null && Scopes.VariableExists(context.obj_access().IDENTIFIER()[0].GetText()))
            {
                //Yes, it's a variable.
                isVariable = true;

                //Stitch the access string together, and get value.
                string objStr = context.obj_access().GetText();
                AlgoValue value = Scopes.GetVariable(objStr);

                //Setting function to call.
                funcToCall = value;
            }

            //No, it's a library.
            else {

                //Getting the correct scope to grab the function from.
                //Is it just the current one?
                if (context.IDENTIFIER() != null)
                {
                    scopes_local = Scopes;

                    //Check if a function variable exists with this name.
                    if (!scopes_local.VariableExists(context.IDENTIFIER().GetText()))
                    {
                        Error.Fatal(context, "No function with name '" + context.IDENTIFIER().GetText() + "' exists.");
                        return null;
                    }

                    //Get the variable.
                    AlgoValue value = scopes_local.GetVariable(context.IDENTIFIER().GetText());


                    //Set function to call.
                    funcToCall = value;
                }
                else
                {
                    //Getting the correct scope.
                    scopes_local = Scopes.GetScopeFromLibAccess(context.obj_access());

                    //Checking if a function variable exists in this scope with the right name.
                    string varname = context.obj_access().IDENTIFIER().Last().GetText();
                    if (!scopes_local.VariableExists(varname))
                    {
                        Error.Fatal(context, "No variable exists with the name '" + varname + "'.");
                        return null;
                    }

                    //Get the variable.
                    AlgoValue funcValue = scopes_local.GetVariable(varname);

                    //Set the function to call.
                    funcToCall = funcValue;
                }
            }

            //Check if the value is a normal function or an emulated function.
            if (funcToCall.Type == AlgoValueType.Function)
            {
                //Get the function.
                AlgoFunction func = (AlgoFunction)funcToCall.Value;

                //Getting parameter length.
                int paramLength = 0;
                if (context.literal_params() != null)
                {
                    paramLength = context.literal_params().expr().Length;
                }

                //Check the parameter length is the same.
                if (paramLength != func.Parameters.Count)
                {
                    Error.Fatal(context, paramLength + " parameters passed to function " + func.Name + ", which expects " + func.Parameters.Count + ".");
                    return null;
                }

                //Parse all the parameters.
                List<AlgoValue> paramvalues = new List<AlgoValue>();
                if (context.literal_params() != null)
                {
                    foreach (var param in context.literal_params().expr())
                    {
                        AlgoValue evaluated = (AlgoValue)VisitExpr(param);
                        paramvalues.Add(evaluated);
                    }
                }

                //If the function is a library, swap out the current scope for the library's scope
                AlgoScopeCollection oldScope = null;
                if (!isVariable)
                {
                    oldScope = Scopes;
                    Scopes = scopes_local;
                }
                if (objScope != null)
                {
                    Scopes.AddScope(objScope.Scopes.First());
                }

                //Adding a scope, and creating the parameters inside it.
                Scopes.AddScope();
                for (int i = 0; i < paramvalues.Count; i++)
                {
                    Scopes.AddVariable(func.Parameters[i], paramvalues[i]);
                }

                //Running the function's body.
                foreach (var statement in func.Body)
                {
                    AlgoValue returned = (AlgoValue)VisitStatement(statement);
                    if (returned != null)
                    {
                        //Remove the function's scope, return.
                        Scopes.RemoveScope();
                        return returned;
                    }
                }

                //Remove the function's scope, we're done!
                Scopes.RemoveScope();
                if (objScope != null)
                {
                    Scopes.RemoveScope();
                }

                //If it was a library, return to old scope.
                if (!isVariable)
                {
                    Scopes = oldScope;
                }
            }

            //It wasn't a normal function, is it an emulated function?
            else if (funcToCall.Type == AlgoValueType.EmulatedFunction)
            {
                //Emulated function, so just return the result of the delegate.
                //First, have to grab parameters though.
                AlgoPluginFunction func = (AlgoPluginFunction)funcToCall.Value;

                //Evaluate parameters.
                //Getting parameter length.
                int paramLength = 0;
                if (context.literal_params() != null)
                {
                    paramLength = context.literal_params().expr().Length;
                }

                //Check the parameter length is the same.
                if (paramLength != func.ParameterCount)
                {
                    Error.Fatal(context, paramLength + " parameters passed to function " + func.Name + ", which expects " + func.ParameterCount + ".");
                    return null;
                }

                //Parse all the parameters.
                List<AlgoValue> paramvalues = new List<AlgoValue>();
                if (context.literal_params() != null)
                {
                    foreach (var param in context.literal_params().expr())
                    {
                        AlgoValue evaluated = (AlgoValue)VisitExpr(param);
                        paramvalues.Add(evaluated);
                    }
                }

                //Return the result of the delegate.
                return func.Function(paramvalues.ToArray());
            }
            else
            {
                //Not a function.
                Error.Fatal(context, "The given function does not exist, so cannot call.");
            }

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