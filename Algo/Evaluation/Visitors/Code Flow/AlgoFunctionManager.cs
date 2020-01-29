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
            //DEFINING LOCALS
            //The library scope (if applicable).
            AlgoScopeCollection scopes_local = null, oldScope = null;
            bool isVariable = false; //Defines whether the func being called is a library or not.
            AlgoValue currentValue = null;

            //Evaluate the first identifier, check if it's a library or a normal variable.
            string initialIdentifier = null;
            if (context.IDENTIFIER() != null)
            {
                //Nested properties/particles.
                initialIdentifier = context.IDENTIFIER().GetText();
            }
            else
            {
                //Simple function call.
                initialIdentifier = context.functionCall_particle().IDENTIFIER().GetText();
            }
            
            //Normal variable?
            if (Scopes.VariableExists(initialIdentifier))
            {
                //Normal variable for base.
                isVariable = true;
                currentValue = Scopes.GetVariable(initialIdentifier);
            }
            else if (Scopes.LibraryExists(initialIdentifier))
            {
                //Library, needs scope switching.
                isVariable = false;
                scopes_local = Scopes.GetLibrary(initialIdentifier);
            }

            //Switch scopes as necessary.
            if (!isVariable)
            {
                ParticleManager.SetFunctionArgumentScopes(Scopes); //Make sure arguments are still evaluated on this scope.
                oldScope = Scopes;
                Scopes = scopes_local;
            }

            //Loop through the particles (if any), and evaluate each, starting from the initial identifier.
            ParticleManager.SetParticleInput(currentValue);
            if (context.particle() != null)
            {
                foreach (var particle in context.particle())
                {
                    VisitParticle(particle);
                }
            }

            //Switch scopes back.
            if (!isVariable)
            {
                Scopes = oldScope;
                ParticleManager.ResetFunctionArgumentScopes();
            }

            //Get the result back.
            currentValue = ParticleManager.GetParticleResult();
            if (currentValue == null)
            {
                Error.Fatal(context, "No value returned to call final function on.");
                return null;
            }

            //Attempt to execute the final function on the result.
            if (context.IDENTIFIER() == null)
            {
                //Visit and execute THIS value, not a child value.
                ParticleManager.SetParticleInput(currentValue);
                return VisitFunctionCall_particle(context.functionCall_particle());
            }
            else
            {
                //There were particles, visit and execute CHILD value.
                if (currentValue.Type != AlgoValueType.Object)
                {
                    Error.Fatal(context, "Cannot call a child function on a value with no children (given value was not an object).");
                    return null;
                }

                //Get the child, see if it's a function.
                AlgoObject thisObj = currentValue.Value as AlgoObject;
                string funcName = context.IDENTIFIER().GetText();
                AlgoValue childFunc = thisObj.ObjectScopes.GetVariable(funcName);
                if (childFunc == null || (childFunc.Type != AlgoValueType.Function && childFunc.Type != AlgoValueType.EmulatedFunction))
                {
                    Error.Fatal(context, "No child function exists with name '" + funcName + "'.");
                    return null;
                }

                //Set the particle result as the function value, call the function call particle.
                ParticleManager.SetParticleInput(childFunc);
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