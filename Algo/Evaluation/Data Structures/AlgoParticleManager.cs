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
    public class ParticleManager
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
        public static void Reset()
        {
            particleResult = null;
        }
    }
}
