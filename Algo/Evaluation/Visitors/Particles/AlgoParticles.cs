using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;

namespace Algo
{
    /// <summary>
    /// Contains all particle referencing and handling for Algo.
    /// </summary>
    public partial class algoVisitor : algoBaseVisitor<object>
    {
        /// <summary>
        /// Evaluates a given particle, and returns the result.
        /// </summary>
        public override object VisitParticle([NotNull] algoParser.ParticleContext context)
        {
            //Get the last result of the particle before this.
            AlgoValue current = ParticleManager.GetParticleResult();
            if (current == null)
            {
                Error.Internal("Particle chain started without setting an initial value.");
                return null;
            }

            //Prepare an "old" scope for swapping.
            AlgoScopeCollection oldScope = null;

            //What type of particle is it?

            //CHILD VARIABLE
            if (context.IDENTIFIER() != null)
            {
                //Sub-variable. Attempt to get from the object.
                if (current.Type != AlgoValueType.Object)
                {
                    Error.Fatal(context, "Cannot access children when given variable is not an object, and has no children to access.");
                    return null;
                }

                //Does the sub-variable exist?
                AlgoObject curObj = (AlgoObject)current.Value;
                AlgoValue newObj = curObj.ObjectScopes.GetVariable(context.IDENTIFIER().GetText());
                if (newObj == null)
                {
                    Error.Fatal(context, "No child of the given object exists with name '" + context.IDENTIFIER().GetText() + "'.");
                    return null;
                }

                //Yes, set result and return.
                ParticleManager.SetParticleInput(newObj);
                return null;
            }

            //ARRAY ACCESS
            else if (context.array_access_particle() != null)
            {
                var aap = context.array_access_particle();

                //Indexing.
                //For each index, iterate over.
                AlgoValue currentLV = current;
                foreach (var index in aap.literal_params().expr())
                {
                    //Is the current list value a list?
                    if (currentLV.Type != AlgoValueType.List)
                    {
                        Error.Fatal(context, "Cannot index into variable that is not a list or indexable object.");
                        return null;
                    }

                    //Need to switch scopes for evaluating indexes?
                    if (ParticleManager.funcArgumentScopes != null)
                    {
                        oldScope = Scopes;
                        Scopes = ParticleManager.funcArgumentScopes;
                    }

                    //Evaluate the expression.
                    AlgoValue indexVal = (AlgoValue)VisitExpr(index);

                    //Back to normal.
                    if (ParticleManager.funcArgumentScopes != null)
                    {
                        Scopes = oldScope;
                    }

                    //Valid?
                    if (indexVal.Type != AlgoValueType.Integer)
                    {
                        Error.Fatal(context, "Index value provided is not an integer. Indexes must be whole integer values.");
                        return null;
                    }

                    //Is the index outside of the bounds of the array?
                    if (((List<AlgoValue>)currentLV.Value).Count <= (BigInteger)indexVal.Value)
                    {
                        Error.Fatal(context, "Index provided is outside the bounds of the array.");
                        return null;
                    }

                    //Index into it, set the current value.
                    currentLV = ((List<AlgoValue>)currentLV.Value)[(int)((BigInteger)indexVal.Value)];
                }

                //Set result.
                ParticleManager.SetParticleInput(currentLV);
                return null;
            }

            //CHILD FUNCTION
            else if (context.functionCall_particle() != null)
            {
                //Function call.
                if (current.Type != AlgoValueType.Object)
                {
                    Error.Fatal(context, "Cannot call a child function on a value with no children (given value was not an object).");
                    return null;
                }

                //Get the child, see if it's a function.
                AlgoObject thisObj = current.Value as AlgoObject;
                string funcName = context.IDENTIFIER().GetText();
                AlgoValue childFunc = thisObj.ObjectScopes.GetVariable(funcName);
                if (childFunc == null || (childFunc.Type != AlgoValueType.Function && childFunc.Type != AlgoValueType.EmulatedFunction))
                {
                    Error.Fatal(context, "No child function exists with name '" + funcName + "'.");
                    return null;
                }

                //Set the particle result as the function value, call the function call particle.
                ParticleManager.SetParticleInput(childFunc);
                var result = VisitFunctionCall_particle(context.functionCall_particle());
                if (result == null)
                { 
                    ParticleManager.Reset(); //Returned no result. 
                }
                else
                {
                    //A result came back, set input for the next particle.
                    ParticleManager.SetParticleInput((AlgoValue)result);
                }

                return null;
            }
            else
            {
                //Unrecognized!
                Error.Internal("Unrecognized particle type to parse, implementation either unfinished or incorrect.");
                return null;
            }
        }

        /// <summary>
        /// Evaluates a single function call particle, and returns the result as a value (NOT using particle manager).
        /// </summary>
        public override object VisitFunctionCall_particle([NotNull] algoParser.FunctionCall_particleContext context)
        {
            //Set up an old scope.
            AlgoScopeCollection oldScope = null;

            //Get the previous particle output.
            var current = ParticleManager.GetParticleResult();

            //Right length of parameters?
            var paramCtx = context.literal_params();
            if (current.Type == AlgoValueType.EmulatedFunction)
            {
                var func = ((AlgoPluginFunction)current.Value);
                int paramNum = func.ParameterCount;
                if (paramCtx.expr().Length != paramNum)
                {
                    Error.Fatal(context, "Invalid number of parameters for function '" + func.Name + "' (Expected " + func.ParameterCount + ", got " + paramCtx.expr().Length + ").");
                    return null;
                }
            }
            else if (current.Type == AlgoValueType.Function)
            {
                var func = ((AlgoFunction)current.Value);
                int paramNum = func.Parameters.Count;
                if (paramCtx.expr().Length != paramNum)
                {
                    Error.Fatal(context, "Invalid number of parameters for function '" + func.Name + "' (Expected " + func.Parameters.Count + ", got " + paramCtx.expr().Length + ").");
                    return null;
                }
            }

            //Evaluate all the parameters, in the paramScope if necessary.
            if (ParticleManager.funcArgumentScopes != null)
            {
                oldScope = Scopes;
                Scopes = ParticleManager.funcArgumentScopes;
            }

            List<AlgoValue> params_ = new List<AlgoValue>();
            foreach (var paramToEval in paramCtx.expr())
            {
                params_.Add((AlgoValue)VisitExpr(paramToEval));
            }

            //Switch back scopes.
            if (ParticleManager.funcArgumentScopes != null)
            {
                Scopes = oldScope;
            }

            //Depending on the type of function, execute.
            if (current.Type == AlgoValueType.Function)
            {
                //STANDARD FUNCTION
                var func = (AlgoFunction)current.Value;

                //Create a new scope, add the parameters to it.
                Scopes.AddScope();
                for (int i = 0; i < params_.Count; i++)
                {
                    Scopes.AddVariable(func.Parameters[i], params_[i], context);
                }
                
                //Running the function's body.
                foreach (var statement in func.Body)
                {
                    AlgoValue returned = (AlgoValue)VisitStatement(statement);
                    if (returned != null)
                    {
                        //Normal function, remove scope and return.
                        Scopes.RemoveScope();

                        return returned;
                    }
                }

                //Remove the scope.
                Scopes.RemoveScope();

                //Return nothing, no result.
                return null;
            }
            else if (current.Type == AlgoValueType.EmulatedFunction)
            {
                //EMULATED FUNCTION
                var func = (AlgoPluginFunction)current.Value;

                //Attempt to evaluate, and return result.
                AlgoValue returned = null;
                try
                {
                    returned = func.Function(context, params_.ToArray());
                }
                catch (Exception e)
                {
                    Error.Fatal(context, "External function returned an error, " + e.Message);
                    return null;
                }

                return returned;
            }
            else
            {
                //Unsupported uncaught function type.
                Error.Internal("Unsupported uncaught function type detected (" + current.Type.ToString() + "), cannot execute.");
                return null;
            }
        }
    }
}
