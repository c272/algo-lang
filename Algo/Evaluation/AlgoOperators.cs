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
            if (base_.IsEnumerable || power.IsEnumerable)
            {
                Error.Fatal(context, "Cannot perform mathematical powers on arrays.");
                return null;
            }

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
                            Value = BigFloat.Pow((BigFloat)base_.Value, int.Parse(((BigFloat)power.Value).ToString().Split('.')[0])),
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
                            Value = BigInteger.Pow((BigInteger)base_.Value, int.Parse(((BigInteger)power.Value).ToString())),
                            IsEnumerable = false
                        };

                    case AlgoValueType.Rational:

                        //Warn about casting from rational to whole value.
                        Error.Warning(context, "Given power is a rational, so is being cast to integer. Loss of data may occur.");

                        //Casting power to integer.
                        string[] rational = ((BigRational)power.Value).ToString().Split('/');
                        int numerator = int.Parse(rational[0]);
                        int denominator = int.Parse(rational[1]);
                        float rational_as_float = numerator / denominator;
                        int castedPower = int.Parse(rational_as_float.ToString().Split('.')[0]);

                        //Returning operation.
                        return new AlgoValue()
                        {
                            Type = AlgoValueType.Rational,
                            Value = BigRational.Pow((BigRational)base_.Value, new BigInteger(castedPower)),
                            IsEnumerable = false
                        };
                }
            }

            //Types are not identical. List the valid ones here.
            // INTEGER.
            if (power.Type == AlgoValueType.Integer)
            {
                //Warn if integer is too large.
                int powerCasted;
                if ((BigInteger)power.Value > int.MaxValue)
                {
                    Error.Warning(context, "The power given is too large to compute, and has been cast down. This may result in a loss of data.");
                    powerCasted = int.MaxValue;
                } else
                {
                    powerCasted = int.Parse(((BigInteger)power.Value).ToString());
                }
                
                //Switch for base.
                if (base_.Type == AlgoValueType.Float)
                {
                    return new AlgoValue()
                    {
                        Value = BigFloat.Pow((BigFloat)base_.Value, powerCasted),
                        Type = AlgoValueType.Float,
                        IsEnumerable = false
                    };
                }
                else if (base_.Type == AlgoValueType.Rational)
                {
                    return new AlgoValue()
                    {
                        Value = BigRational.Pow((BigRational)base_.Value, powerCasted),
                        Type = AlgoValueType.Rational,
                        IsEnumerable = false
                    };
                }
            }

            // FLOAT.
            if (power.Type == AlgoValueType.Float)
            {
                //Warn the user of data loss.
                Error.Warning(context, "Floats cannot be used as an exponent, the given power has been cast down. This may result in data loss.");

                //Casting.
                int powerCasted = int.Parse(((BigFloat)power.Value).ToString().Split('.')[0]);

                if (base_.Type == AlgoValueType.Integer)
                {
                    return new AlgoValue()
                    {
                        Type = AlgoValueType.Integer,
                        Value = BigInteger.Pow((BigInteger)base_.Value, powerCasted),
                        IsEnumerable = false
                    };
                }
                else if (base_.Type == AlgoValueType.Rational)
                {
                    return new AlgoValue()
                    {
                        Type = AlgoValueType.Rational,
                        Value = BigRational.Pow((BigRational)base_.Value, new BigInteger(powerCasted)),
                        IsEnumerable = false
                    };
                }
            }

            // RATIONAL.
            if (power.Type == AlgoValueType.Rational)
            {
                //Warn the user of data loss.
                Error.Warning(context, "Rationals are not supported as an exponent, the given power has been cast down. This may result in data loss.");
                
                //Casting power to integer.
                string[] rational = ((BigRational)power.Value).ToString().Split('/');
                int numerator = int.Parse(rational[0]);
                int denominator = int.Parse(rational[1]);
                float rational_as_float = numerator / denominator;
                int castedPower = int.Parse(rational_as_float.ToString().Split('.')[0]);

                if (base_.Type == AlgoValueType.Float)
                {
                    return new AlgoValue()
                    {
                        Type = AlgoValueType.Float,
                        Value = BigFloat.Pow((BigFloat)base_.Value, castedPower),
                        IsEnumerable = false
                    };
                }
                else if (base_.Type == AlgoValueType.Integer)
                {
                    return new AlgoValue()
                    {
                        Type = AlgoValueType.Integer,
                        Value = BigInteger.Pow((BigInteger)base_.Value, castedPower),
                        IsEnumerable = false
                    };
                }
            }

            //Did not find value.
            Error.Fatal(context, "Invalid power operation passed to evaluator.");
            return null;
        }

        //Multiply one AlgoValue by another.
        public static AlgoValue Mul(ParserRuleContext context, AlgoValue left, AlgoValue right)
        {
            //Check if either are strings, renders the operation invalid.
            if (left.Type == AlgoValueType.String || right.Type == AlgoValueType.String)
            {
                Error.Fatal(context, "Cannot perform multiplication on strings.");
                return null;
            }

            //Check if either are arrays.
            if (left.IsEnumerable || right.IsEnumerable)
            {
                Error.Fatal(context, "Cannot perform multiplication on arrays.");
                return null;
            }

            //Are the types the same? No explicit casting.
            if (left.Type == right.Type)
            {
                //Yes, multiply.
                if (left.Type == AlgoValueType.Float)
                {
                    return new AlgoValue()
                    {
                        Type = AlgoValueType.Float,
                        Value = BigFloat.Multiply((BigFloat)left.Value, (BigFloat)right.Value),
                        IsEnumerable = false
                    };
                }

                if (left.Type == AlgoValueType.Integer)
                {
                    return new AlgoValue()
                    {
                        Type = AlgoValueType.Integer,
                        Value = BigInteger.Multiply((BigInteger)left.Value, (BigInteger)right.Value),
                        IsEnumerable = false
                    };
                }

                if (left.Type == AlgoValueType.Rational)
                {
                    return new AlgoValue()
                    {
                        Type = AlgoValueType.Rational,
                        Value = BigRational.Multiply((BigRational)left.Value, (BigRational)right.Value),
                        IsEnumerable = false
                    };
                }
            }

            //Types aren't the same, see if there are any valid other combinations.
            if (left.Type == AlgoValueType.Integer && right.Type == AlgoValueType.Float)
            {
                return new AlgoValue()
                {
                    Type = AlgoValueType.Float,
                    Value = BigFloat.Multiply((BigFloat)right.Value, new BigFloat((BigInteger)left.Value)),
                    IsEnumerable = false
                };
            }

            //Float * Integer
            if (left.Type == AlgoValueType.Float && right.Type == AlgoValueType.Integer)
            {
                return new AlgoValue()
                {
                    Type = AlgoValueType.Float,
                    Value = BigFloat.Multiply((BigFloat)left.Value, new BigFloat((BigInteger)right.Value)),
                    IsEnumerable = false
                };
            }

            //Rational * Integer
            if (left.Type == AlgoValueType.Rational && right.Type == AlgoValueType.Integer)
            {
                BigInteger numerator = BigInteger.Multiply(((BigRational)left.Value).FractionalPart.Numerator, (BigInteger)right.Value);
                BigInteger denominator = ((BigRational)left.Value).FractionalPart.Denominator;

                return new AlgoValue()
                {
                   Type = AlgoValueType.Rational,
                   Value = new BigRational(BigInteger.Zero,new Fraction(numerator, denominator)),
                   IsEnumerable = false
                };
            }

            //Integer * Rational
            if (right.Type == AlgoValueType.Integer && right.Type == AlgoValueType.Rational)
            {
                BigInteger numerator = BigInteger.Multiply(((BigRational)right.Value).FractionalPart.Numerator, (BigInteger)left.Value);
                BigInteger denominator = ((BigRational)right.Value).FractionalPart.Denominator;

                return new AlgoValue()
                {
                    Type = AlgoValueType.Rational,
                    Value = new BigRational(BigInteger.Zero, new Fraction(numerator, denominator)),
                    IsEnumerable = false
                };
            }

            //Could not find a valid combination, exit as fatal.
            Error.Fatal(context, "Invalid types to multiply, cannot multiply type " + left.Type.ToString() + " and " + right.Type.ToString() + ".");
            return null;
        }

        //Divide one AlgoValue by another.
        public static AlgoValue Div(ParserRuleContext context, AlgoValue left, AlgoValue right)
        {
            //Check if either are strings, renders the operation invalid.
            if (left.Type == AlgoValueType.String || right.Type == AlgoValueType.String)
            {
                Error.Fatal(context, "Cannot perform division on strings.");
                return null;
            }

            //Check if either are arrays.
            if (left.IsEnumerable || right.IsEnumerable)
            {
                Error.Fatal(context, "Cannot perform division on arrays.");
                return null;
            }

            //Are the types the same? No casting required that way.
            if (left.Type == right.Type)
            {
                //Little casting required.
                if (left.Type == AlgoValueType.Float)
                {
                    return new AlgoValue()
                    {
                        Type = AlgoValueType.Float,
                        Value = BigFloat.Divide((BigFloat)left.Value, (BigFloat)right.Value),
                        IsEnumerable = false
                    };
                }
                else if (left.Type == AlgoValueType.Integer)
                {
                    return new AlgoValue()
                    {
                        Type = AlgoValueType.Integer,
                        Value = BigInteger.Divide((BigInteger)left.Value, (BigInteger)right.Value),
                        IsEnumerable = false
                    };
                }
                else if (left.Type == AlgoValueType.Rational)
                {
                    //Getting the numerator and denominator.
                    BigInteger left_numerator = ((BigRational)left.Value).FractionalPart.Numerator;
                    BigInteger left_denominator = ((BigRational)left.Value).FractionalPart.Denominator;
                    BigInteger right_numerator = ((BigRational)right.Value).FractionalPart.Numerator;
                    BigInteger right_denominator = ((BigRational)right.Value).FractionalPart.Denominator;

                    //Multiply to get new numerator and denominator.
                    BigInteger numerator = left_numerator * right_denominator;
                    BigInteger denominator = left_denominator * right_numerator;

                    return new AlgoValue()
                    {
                        Type = AlgoValueType.Rational,
                        Value = new BigRational(new Fraction(numerator, denominator)),
                        IsEnumerable = false
                    };
                }
            }

            //Find other valid type combinations.

            //Float / Integer
            if (left.Type == AlgoValueType.Float && right.Type == AlgoValueType.Integer)
            {
                return new AlgoValue()
                {
                    Type = AlgoValueType.Float,
                    Value = BigFloat.Divide((BigFloat)left.Value, new BigFloat((BigInteger)right.Value)),
                    IsEnumerable = false
                };
            }
            else if (left.Type == AlgoValueType.Integer && right.Type == AlgoValueType.Float)
            {
                return new AlgoValue()
                {
                    Type = AlgoValueType.Float,
                    Value = BigFloat.Divide(new BigFloat((BigInteger)left.Value), (BigFloat)right.Value),
                    IsEnumerable = false
                };
            }

            //Rational / Integer
            else if (left.Type == AlgoValueType.Rational && right.Type == AlgoValueType.Integer)
            {
                //Getting numerator and denominator.
                BigInteger numerator = ((BigRational)left.Value).FractionalPart.Numerator;
                BigInteger denominator = ((BigRational)left.Value).FractionalPart.Denominator * (BigInteger)left.Value;

                return new AlgoValue()
                {
                    Type = AlgoValueType.Rational,
                    Value = new BigRational(new Fraction(numerator, denominator)),
                    IsEnumerable = false
                };
            }
            else if (left.Type == AlgoValueType.Integer && right.Type == AlgoValueType.Rational)
            {
                //Getting numerator and denominator.
                BigInteger numerator = (BigInteger)left.Value * ((BigRational)right.Value).FractionalPart.Denominator;
                BigInteger denominator = ((BigRational)right.Value).FractionalPart.Numerator;

                return new AlgoValue()
                {
                    Type = AlgoValueType.Rational,
                    Value = new BigRational(new Fraction(numerator, denominator)),
                    IsEnumerable = false
                };
            }
            
            //Rational / Float
            else if (left.Type == AlgoValueType.Rational && right.Type == AlgoValueType.Float)
            {
                //Get the rational part as a float.
                BigInteger numerator = ((BigRational)left.Value).FractionalPart.Numerator;
                BigInteger denominator = ((BigRational)left.Value).FractionalPart.Denominator;
                BigFloat rational_as_float = BigFloat.Divide(new BigFloat(numerator), new BigFloat(denominator));

                //Return.
                return new AlgoValue()
                {
                    Type = AlgoValueType.Float,
                    Value = BigFloat.Divide(rational_as_float, (BigFloat)right.Value),
                    IsEnumerable = false
                };
            }
            else if (left.Type == AlgoValueType.Float && right.Type == AlgoValueType.Rational)
            {
                //Get the rational part as a float.
                BigInteger numerator = ((BigRational)right.Value).FractionalPart.Numerator;
                BigInteger denominator = ((BigRational)right.Value).FractionalPart.Denominator;
                BigFloat rational_as_float = BigFloat.Divide(new BigFloat(numerator), new BigFloat(denominator));

                //Return.
                return new AlgoValue()
                {
                    Type = AlgoValueType.Float,
                    Value = BigFloat.Divide((BigFloat)left.Value, rational_as_float),
                    IsEnumerable = false
                };
            }
            
            //Could not find a valid combination, exit as fatal.
            Error.Fatal(context, "Invalid types to divide, cannot divide type " + left.Type.ToString() + " by " + right.Type.ToString() + ".");
            return null;
        }
    }
}