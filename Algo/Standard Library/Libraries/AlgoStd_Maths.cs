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
                ParameterCount = 0
            },

            //random.intRange(x, y)
            new AlgoPluginFunction()
            {
                Name = "random_intRange",
                Function = RandomIntegerRanged,
                ParameterCount = 2
            },
            
            //maths.modPow()
            new AlgoPluginFunction()
            {
                Name = "modPow",
                Function = ModPow,
                ParameterCount = 3
            }
        };

        //Random number generator, returns an integer.
        public static AlgoValue GenerateRandomInteger(ParserRuleContext context, params AlgoValue[] args)
        {
            //Generate an integer and return.
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            return new AlgoValue()
            {
                Type = AlgoValueType.Integer,
                Value = new BigInteger(rand.Next())
            };
        }

        //Random number generator that returns an integer from a range.
        public static AlgoValue RandomIntegerRanged(ParserRuleContext context, params AlgoValue[] args)
        {
            //Check the range is valid.
            if (args[0].Type != args[1].Type || args[0].Type != AlgoValueType.Integer)
            {
                Error.Fatal(context, "Range must be given in integer form.");
                return null;
            }
            
            if ((BigInteger)args[0].Value >= (BigInteger)args[1].Value)
            {
                Error.Fatal(context, "Lower bound for the range must be smaller than the upper bound.");
                return null;
            }

            if ((BigInteger)args[0].Value < 1)
            {
                Error.Fatal(context, "Lower bound cannot be zero or below.");
                return null;
            }

            if ((BigInteger)args[1].Value > int.MaxValue)
            {
                Error.Fatal(context, "Upper bound cannot be larger than the INT32 max.");
                return null;
            }

            //Generate an integer and return.
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            return new AlgoValue()
            {
                Type = AlgoValueType.Integer,
                Value = new BigInteger(rand.Next(int.Parse(((BigInteger)args[0].Value).ToString()), int.Parse(((BigInteger)args[1].Value).ToString())))
            };
        }

        //Performs ModPow on two large numbers, useful when powers are too large to process.
        //args[0] = base
        //args[1] = exponent
        //args[2] = modulus
        public static AlgoValue ModPow(ParserRuleContext context, params AlgoValue[] args)
        {
            //Check arguments.
            if (args[0].Type != args[1].Type || args[1].Type != args[2].Type || args[0].Type != AlgoValueType.Integer)
            {
                Error.Fatal(context, "Arguments for ModPow must all be integers.");
                return null;
            }

            //Computing.
            return new AlgoValue()
            {
                Type = AlgoValueType.Integer,
                Value = BigInteger.ModPow((BigInteger)args[0].Value, (BigInteger)args[1].Value, (BigInteger)args[2].Value)
            };
        }
    }
}