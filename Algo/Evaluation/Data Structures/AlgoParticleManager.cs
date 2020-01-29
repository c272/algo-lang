using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algo
{
    /// <summary>
    /// Aids in the evaluation of particles in expressions.
    /// </summary>
    public class Particles
    {
        //The result from the last particle evaluation.
        private static AlgoValue particleResult = null;
        public static AlgoScopeCollection funcArgumentScopes { get; private set; } = null;

        /// <summary>
        /// Sets the scopes to evaluate function arguments for particles.
        /// </summary>
        public static void SetFunctionArgumentScopes(AlgoScopeCollection scopes)
        {
            funcArgumentScopes = scopes;
        }

        /// <summary>
        /// Deletes the scopes to evaluate function arguments for particles (uses current instead).
        /// </summary>
        public static void ResetFunctionArgumentScopes()
        {
            funcArgumentScopes = null;
        }

        /// <summary>
        /// Sets the result of a particle being evaluated, to carry over.
        /// </summary>
        public static void SetParticleInput(AlgoValue value)
        {
            particleResult = value;
        }

        /// <summary>
        /// Gets the result of the previous particle evaluation in the chain.
        /// </summary>
        public static AlgoValue GetParticleResult()
        {
            if (particleResult == null)
            {
                Error.Internal("Could not fetch particle result for chain, no chain is in progress.");
                return null;
            }

            return particleResult;
        }

        /// <summary>
        /// Ends this chain of particle results, since the evaluation has finished.
        /// </summary>
        public static void ResetParticleInput()
        {
            particleResult = null;
        }

        /// <summary>
        /// Get a result from this particle block (null if none).
        /// </summary>
        public static AlgoValue ParseParticleBlock(algoVisitor visitor, ParserRuleContext ctx, ITerminalNode firstIdentifier, algoParser.ParticleContext[] particleContext)
        {
            //Is it a user created variable?
            if (algoVisitor.Scopes.VariableExists(firstIdentifier.GetText()))
            {
                //Yes, get the value and evaluate the particles.
                var value = algoVisitor.Scopes.GetVariable(firstIdentifier.GetText());
                if (particleContext != null)
                {
                    //Set particle input as the current value.
                    SetParticleInput(value);

                    foreach (var particle in particleContext)
                    {
                        //Evaluate all the particles, get the final result.
                        visitor.VisitParticle(particle);
                    }

                    value = GetParticleResult();
                }

                //Return the value.
                return value;
            }
            //Is it a library?
            else if (algoVisitor.Scopes.LibraryExists(firstIdentifier.GetText()))
            {
                //If it's a library, you've got to have a particle access.
                if (particleContext == null || particleContext.Length == 0)
                {
                    Error.Fatal(ctx, "Cannot use a library directly as a value.");
                    return null;
                }

                //It's a library, prepare for a scope switch.
                var scopes_local = algoVisitor.Scopes.GetLibrary(firstIdentifier.GetText());
                AlgoScopeCollection oldScope = algoVisitor.Scopes;

                //First, get the first particle to set as the particle input.
                var toEval = particleContext[0];
                var particleVal = scopes_local.GetVariable(toEval.IDENTIFIER().GetText());
                if (particleVal == null)
                {
                    Error.Fatal(toEval, "No variable exists in library '" + firstIdentifier.GetText() + "' named '" + toEval.IDENTIFIER().GetText() + "'.");
                    return null;
                }

                //Swap out the scopes, set the argument evaluation scope.
                SetFunctionArgumentScopes(algoVisitor.Scopes);
                SetParticleInput(particleVal);
                algoVisitor.Scopes = scopes_local;

                //Execute all the particles past the first one.
                for (int i = 1; i < particleContext.Length; i++)
                {
                    visitor.VisitParticle(particleContext[i]);
                    particleVal = GetParticleResult();
                }

                //Switch back to the original scope, reset the arg eval scope.
                ResetFunctionArgumentScopes();
                ResetParticleInput();
                algoVisitor.Scopes = oldScope;

                return particleVal;
            }
            else
            {
                Error.Fatal(ctx, "No variable or library exists with name '" + firstIdentifier.GetText() + "'.");
                return null;
            }
        }

        /// <summary>
        /// Stitches a variable name out of a base identifier, and a load of particles.
        /// Assumes all particles are of type identifier.
        /// </summary>
        public static string StitchVariableName(ParserRuleContext ctx, string firstIdentifier, algoParser.ParticleContext[] particleContext)
        {
            string toRet = firstIdentifier;
            if (particleContext != null)
            {
                foreach (var particle in particleContext)
                {
                    //Type MUST be Identifier.
                    if (particle.IDENTIFIER() == null)
                    {
                        Error.Fatal(ctx, "Invalid variable name given, must only be an object reference or identifier.");
                        return null;
                    }

                    //Add as identifier.
                    toRet += "." + particle.IDENTIFIER().GetText();
                }
            }

            return toRet;
        }
    }
}
