using System;
using ExtendedNumerics;
using System.Numerics;
using Antlr4;
using Antlr4.Runtime;

namespace Algo
{
    public class AlgoOperators
    {
        //Power one AlgoValue by another. Takes the AlgoValueType of the power if required.
        public static AlgoValue Pow(ParserRuleContext context, AlgoValue base_, AlgoValue power)
        {
            //Check neither are strings, that just doesn't work.
            if (base_.Type == AlgoValueType.String || power.Type == AlgoValueType.String)
            {
                Error.Fatal(context, "Cannot perform mathematical powers on strings.");
                return null;
            }

            //Are either of them enumerable? Those can't be powered.


            //Identical sets.
            if (base_.Type == power.Type)
            {
                switch (base_.Type)
                {
                    case AlgoValueType.Float:

                        //Only integer powers are permitted for BigFloat.
                        Error.Warning(context, "Implicit type casting of float to integer, could result in loss of data. Only integer powers are permitted.");

                        //Returning value.
                        return new AlgoValue()
                        {
                            Type = AlgoValueType.Float,
                            Value = BigFloat.Pow((BigFloat)base_.Value, int.Parse(((BigFloat)power.Value).ToString())),
                            IsEnumerable = false
                        };

                    case AlgoValueType.Integer:

                        //Check if the given BigInt is larger than the maximum normal integer.
                        if ((BigInteger)power.Value > int.MaxValue)
                        {
                            Error.Warning(context, "Given power is too large to process normally, so has to be truncated. This may result in loss of data.");
                            power.Value = new BigInteger(int.MaxValue);
                        }

                        //All good, no warnings.
                        return new AlgoValue()
                        {
                            Type = AlgoValueType.Integer,
                            Value = BigInteger.Pow()
                        };

                }
            }
        }
    }
}