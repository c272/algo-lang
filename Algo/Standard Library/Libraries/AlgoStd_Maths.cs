using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;

namespace Algo.StandardLibrary
{
    public class AlgoStd_Maths : IFunctionPlugin
    {
        public string Name { get; set; } = "std_maths";
        public List<AlgoPluginFunction> Functions { get; set; } = new List<AlgoPluginFunction>()
        {
            //random.int()
            new AlgoPluginFunction()
            {
                Name = "random_int",
                Function = GenerateRandomInteger,
                ParameterCount = 1
            }
        };

        //Random number generator (given a seed).
        public static AlgoValue GenerateRandomInteger(ParserRuleContext context, params AlgoValue[] args)
        {
            //Check the seed is an int.
            if (args[0].Type != AlgoValueType.Integer)
            {
                Error.Fatal(context, "Random seed must be an integer, cannot be of type " + args[0].Type.ToString() + ".");
                return null;
            }

            //Too large?
            if (new BigInteger(Environment.TickCount) - (BigInteger)args[0].Value < int.MinValue)
            {
                Error.Fatal(context, "Seed is too large, must be within INT32 bounds.");
                return null;
            }

            //It is, generate an integer and return.
            Random rand = new Random(Environment.TickCount - int.Parse((args[0].Value).ToString()));
            return new AlgoValue()
            {
                Type = AlgoValueType.Integer,
                Value = new BigInteger(rand.Next())
            };
        }
    }
}