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
            //If there are particles, get the last value.
            AlgoValue currentVal = null; bool isLibraryTopLevelCall = false;
            if (context.IDENTIFIER() != null)
            {
                //Is the identifier a library and has no particles?
                if (!Scopes.VariableExists(context.IDENTIFIER().GetText())
                  && Scopes.LibraryExists(context.IDENTIFIER().GetText())
                  && (context.particle() == null || context.particle().Length == 0))
                {
                    //Library top level call, so don't evaluate like normal particles.
                    isLibraryTopLevelCall = true;

                    //Just get the very last function like a normal variable, but from the library.
                    string libName = context.IDENTIFIER().GetText();
                    string name = context.functionCall_particle().IDENTIFIER().GetText();
                    var lib = Scopes.GetLibrary(libName);

                    if (!lib.VariableExists(name))
                    {
                        Error.Fatal(context, "No variable named '" + name + "' exists in library '" + libName + "'.");
                        return null;
                    }

                    currentVal = lib.GetVariable(name);
                }
                else
                {
                    //Not a library top level call, so execute like a normal particle block.
                    currentVal = Particles.ParseParticleBlock(this, context, context.IDENTIFIER(), context.particle());
                }
            }
            else
            {
                //No particles, just grab the value as an identifier.
                string name = context.functionCall_particle().IDENTIFIER().GetText();
                if (!Scopes.VariableExists(name))
                {
                    Error.Fatal(context, "No function exists with name '" + name + "'.");
                    return null;
                }

                currentVal = Scopes.GetVariable(name);
            }

            //Attempt to execute the final function on the result.
            if (context.IDENTIFIER() == null || isLibraryTopLevelCall)
            {
                //Visit and execute THIS value, not a child value (this was a straight normal call).
                Particles.SetParticleInput(currentVal);
                return VisitFunctionCall_particle(context.functionCall_particle());
            }
            else
            {
                //There were particles, visit and execute CHILD value.
                if (currentVal.Type != AlgoValueType.Object)
                {
                    Error.Fatal(context, "Cannot call a child function on a value with no children (given value was not an object).");
                    return null;
                }

                //Get the child, see if it's a function.
                AlgoObject thisObj = currentVal.Value as AlgoObject;
                string funcName = context.IDENTIFIER().GetText();
                AlgoValue childFunc = thisObj.ObjectScopes.GetVariable(funcName);
                if (childFunc == null || (childFunc.Type != AlgoValueType.Function && childFunc.Type != AlgoValueType.EmulatedFunction))
                {
                    Error.Fatal(context, "No child function exists with name '" + funcName + "'.");
                    return null;
                }

                //Set the particle result as the function value, call the function call particle.
                Particles.SetParticleInput(childFunc);
                return VisitFunctionCall_particle(context.functionCall_particle());
            }
        }

        //When a value is returned from a function.
        public override object VisitStat_return([NotNull] algoParser.Stat_returnContext context)
        {
            //Evaluate the expression to return.
            AlgoValue toReturn;
            if (context.expr() != null)
            {
                toReturn = (AlgoValue)VisitExpr(context.expr());
            } else
            {
                toReturn = new AlgoValue()
                {
                    Type = AlgoValueType.Null,
                    Value = null
                };
            }

            //Return it.
            return toReturn;
        }
    }
}