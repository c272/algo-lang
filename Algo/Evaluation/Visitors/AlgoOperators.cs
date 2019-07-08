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
                        
                    };
                }
                else if (base_.Type == AlgoValueType.Rational)
                {
                    return new AlgoValue()
                    {
                        Value = BigRational.Pow((BigRational)base_.Value, powerCasted),
                        Type = AlgoValueType.Rational,
                        
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
                        
                    };
                }
                else if (base_.Type == AlgoValueType.Rational)
                {
                    return new AlgoValue()
                    {
                        Type = AlgoValueType.Rational,
                        Value = BigRational.Pow((BigRational)base_.Value, new BigInteger(powerCasted)),
                        
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
                        
                    };
                }
                else if (base_.Type == AlgoValueType.Integer)
                {
                    return new AlgoValue()
                    {
                        Type = AlgoValueType.Integer,
                        Value = BigInteger.Pow((BigInteger)base_.Value, castedPower),
                        
                    };
                }
            }

            //Did not find value.
            Error.Fatal(context, "Invalid power operation passed to evaluator, cannot power types '" + base_.Type.ToString() + "' and '" + power.Type.ToString() +"'.");
            return null;
        }

        //Modulus one AlgoValue with another.
        public static AlgoValue Mod(algoParser.TermContext context, AlgoValue left, AlgoValue right)
        {
            //Switch over types.
            if (left.Type == AlgoValueType.Integer)
            {
                //INTEGER % something
                switch (right.Type)
                {
                    case AlgoValueType.Integer:
                        return new AlgoValue()
                        {
                            Type = AlgoValueType.Integer,
                            Value = BigInteger.Remainder((BigInteger)left.Value, (BigInteger)right.Value)
                        };

                    case AlgoValueType.Float:
                        return new AlgoValue()
                        {
                            Type = AlgoValueType.Float,
                            Value = BigFloat.Remainder(new BigFloat((BigInteger)left.Value), (BigFloat)right.Value)
                        };

                    case AlgoValueType.Rational:
                        //Get float representation of rational.
                        BigFloat rational_as_float = AlgoConversion.RationalToFloat(right);

                        //Perform float remainder.
                        return new AlgoValue()
                        {
                            Type = AlgoValueType.Float,
                            Value = BigFloat.Remainder(new BigFloat((BigInteger)left.Value), rational_as_float)
                        };
                }
            }
            else if (left.Type == AlgoValueType.Float)
            {
                //FLOAT % something
                switch (right.Type)
                {
                    case AlgoValueType.Integer:
                        return new AlgoValue()
                        {
                            Type = AlgoValueType.Float,
                            Value = BigFloat.Remainder((BigFloat)left.Value, new BigFloat((BigInteger)right.Value))
                        };
                    case AlgoValueType.Float:
                        return new AlgoValue()
                        {
                            Type = AlgoValueType.Float,
                            Value = BigFloat.Remainder((BigFloat)left.Value, (BigFloat)right.Value)
                        };
                    case AlgoValueType.Rational:
                        //Get float representation of rational.
                        BigFloat rational_as_float = AlgoConversion.RationalToFloat(right);

                        return new AlgoValue()
                        {
                            Type = AlgoValueType.Rational,
                            Value = BigFloat.Remainder((BigFloat)left.Value, rational_as_float)
                        };
                }
            }
            else if (left.Type == AlgoValueType.Rational)
            {
                //RATIONAL % something

                //Get float representation of rational.
                BigFloat rational_as_float = AlgoConversion.RationalToFloat(left);
                
                switch (right.Type)
                {
                    case AlgoValueType.Integer:
                        return new AlgoValue()
                        {
                            Type = AlgoValueType.Float,
                            Value = BigFloat.Remainder(rational_as_float, new BigFloat((BigInteger)right.Value))
                        };
                    case AlgoValueType.Float:
                        return new AlgoValue()
                        {
                            Type = AlgoValueType.Float,
                            Value = BigFloat.Remainder(rational_as_float, (BigFloat)right.Value)
                        };
                    case AlgoValueType.Rational:
                        //Get rational as float.
                        BigFloat sec_rational_as_float = AlgoConversion.RationalToFloat(right);
                        return new AlgoValue()
                        {
                            Type = AlgoValueType.Float,
                            Value = BigFloat.Remainder(rational_as_float, sec_rational_as_float)
                        };
                }
            }

            //No return was found, so error out.
            Error.Fatal(context, "Cannot modulus type '" + left.Type.ToString() + "' with type '" + right.Type.ToString() + "'.");
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
                        
                    };
                }

                if (left.Type == AlgoValueType.Integer)
                {
                    return new AlgoValue()
                    {
                        Type = AlgoValueType.Integer,
                        Value = BigInteger.Multiply((BigInteger)left.Value, (BigInteger)right.Value),
                        
                    };
                }

                else if (left.Type == AlgoValueType.Rational)
                {
                    return new AlgoValue()
                    {
                        Type = AlgoValueType.Rational,
                        Value = BigRational.Multiply((BigRational)left.Value, (BigRational)right.Value),
                        
                    };
                }
            }

            //Types aren't the same, see if there are any valid other combinations.

            //Integer combinations.
            if (left.Type == AlgoValueType.Integer)
            {
                //Float.
                if (right.Type == AlgoValueType.Float)
                {
                    return new AlgoValue()
                    {
                        Type = AlgoValueType.Float,
                        Value = BigFloat.Multiply((BigFloat)right.Value, new BigFloat((BigInteger)left.Value)),
                        
                    };
                }

                //Rational.
                else if (right.Type == AlgoValueType.Rational)
                {
                    BigInteger numerator = BigInteger.Multiply(((BigRational)right.Value).FractionalPart.Numerator, (BigInteger)left.Value);
                    BigInteger denominator = ((BigRational)right.Value).FractionalPart.Denominator;

                    return new AlgoValue()
                    {
                        Type = AlgoValueType.Rational,
                        Value = new BigRational(BigInteger.Zero, new Fraction(numerator, denominator)),
                        
                    };
                }
            }

            //Float combinations.
            if (left.Type == AlgoValueType.Float)
            {
                //Integer.
                if (right.Type == AlgoValueType.Integer)
                {
                    return new AlgoValue()
                    {
                        Type = AlgoValueType.Float,
                        Value = BigFloat.Multiply((BigFloat)left.Value, new BigFloat((BigInteger)right.Value)),
                        
                    };
                }

                //Rational.
                else if (right.Type == AlgoValueType.Rational)
                {
                    //Get the rational part as a float.
                    BigFloat rational_as_float = AlgoConversion.RationalToFloat(right);

                    //Returning.
                    return new AlgoValue()
                    {
                        Type = AlgoValueType.Float,
                        Value = BigFloat.Multiply(rational_as_float, (BigFloat)left.Value),
                        
                    };
                }
            }

            //Rational combinations.
            if (left.Type == AlgoValueType.Rational)
            {
                //Integer.
                if (right.Type == AlgoValueType.Integer)
                {
                    BigInteger numerator = BigInteger.Multiply(((BigRational)left.Value).FractionalPart.Numerator, (BigInteger)right.Value);
                    BigInteger denominator = ((BigRational)left.Value).FractionalPart.Denominator;

                    return new AlgoValue()
                    {
                        Type = AlgoValueType.Rational,
                        Value = new BigRational(BigInteger.Zero, new Fraction(numerator, denominator)),
                        
                    };
                }

                //Float.
                else if (right.Type == AlgoValueType.Float)
                {
                    //Get the rational part as a float.
                    BigFloat rational_as_float = AlgoConversion.RationalToFloat(right);

                    //Returning.
                    return new AlgoValue()
                    {
                        Type = AlgoValueType.Float,
                        Value = BigFloat.Multiply(rational_as_float, (BigFloat)right.Value),
                        
                    };
                }
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
                        
                    };
                }
                else if (left.Type == AlgoValueType.Integer)
                {
                    //Special case for integers, since this can return both an integer and a float.

                    //Check whether the numerator is a multiple of the denominator.
                    if (BigInteger.Remainder((BigInteger)left.Value, (BigInteger)right.Value) == new BigInteger(0))
                    {
                        //It is, so can just be returned as an integer.
                        return new AlgoValue()
                        {
                            Type = AlgoValueType.Integer,
                            Value = BigInteger.Divide((BigInteger)left.Value, (BigInteger)right.Value),
                            
                        };
                    } else
                    {
                        //Nope, has to be divided as a float.
                        return new AlgoValue()
                        {
                            Type = AlgoValueType.Float,
                            Value = BigFloat.Divide((BigInteger)left.Value, (BigInteger)right.Value),
                            
                        };
                    }
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
                        
                    };
                }
            }

            //Find other valid type combinations.

            //Float combinations.
            if (left.Type == AlgoValueType.Float)
            {
                //Integer.
                if (right.Type == AlgoValueType.Integer)
                {
                    return new AlgoValue()
                    {
                        Type = AlgoValueType.Float,
                        Value = BigFloat.Divide((BigFloat)left.Value, new BigFloat((BigInteger)right.Value)),
                        
                    };
                }

                //Rational.
                else if (right.Type == AlgoValueType.Rational)
                {
                    //Get the rational part as a float.
                    BigFloat rational_as_float = AlgoConversion.RationalToFloat(right);

                    //Return.
                    return new AlgoValue()
                    {
                        Type = AlgoValueType.Float,
                        Value = BigFloat.Divide((BigFloat)left.Value, rational_as_float),
                        
                    };
                }
            }

            //Integer combinations.
            if (left.Type == AlgoValueType.Integer)
            {
                //Float.
                if (right.Type == AlgoValueType.Float)
                {
                    return new AlgoValue()
                    {
                        Type = AlgoValueType.Float,
                        Value = BigFloat.Divide(new BigFloat((BigInteger)left.Value), (BigFloat)right.Value),
                        
                    };
                }

                //Rational.
                else if (right.Type == AlgoValueType.Rational)
                {
                    //Getting numerator and denominator.
                    BigInteger numerator = (BigInteger)left.Value * ((BigRational)right.Value).FractionalPart.Denominator;
                    BigInteger denominator = ((BigRational)right.Value).FractionalPart.Numerator;

                    return new AlgoValue()
                    {
                        Type = AlgoValueType.Rational,
                        Value = new BigRational(new Fraction(numerator, denominator)),
                        
                    };
                }
            }

            //Rational combinations.
            if (left.Type == AlgoValueType.Rational)
            {
                //Integer.
                if (right.Type == AlgoValueType.Integer)
                {
                    //Getting numerator and denominator.
                    BigInteger numerator = ((BigRational)left.Value).FractionalPart.Numerator;
                    BigInteger denominator = ((BigRational)left.Value).FractionalPart.Denominator * (BigInteger)left.Value;

                    return new AlgoValue()
                    {
                        Type = AlgoValueType.Rational,
                        Value = new BigRational(new Fraction(numerator, denominator)),
                        
                    };
                }

                //Float.
                else if (right.Type == AlgoValueType.Float)
                {
                    //Get the rational part as a float.
                    BigFloat rational_as_float = AlgoConversion.RationalToFloat(right);

                    //Return.
                    return new AlgoValue()
                    {
                        Type = AlgoValueType.Float,
                        Value = BigFloat.Divide(rational_as_float, (BigFloat)right.Value),
                        
                    };
                }
            }
            
            //Could not find a valid combination, exit as fatal.
            Error.Fatal(context, "Invalid types to divide, cannot divide type " + left.Type.ToString() + " by " + right.Type.ToString() + ".");
            return null;
        }

        //Add one AlgoValue to another.
        public static AlgoValue Add(ParserRuleContext context, AlgoValue left, AlgoValue right)
        {
            //Check neither of them are enumerable.
            //Future roadmap, you can use [] + 3 to create an array with a 3 in it, etc.

            //Identical types.
            if (left.Type == right.Type)
            {
                if (left.Type==AlgoValueType.Float)
                {
                    return new AlgoValue()
                    {
                        Type = AlgoValueType.Float,
                        Value = BigFloat.Add((BigFloat)left.Value, (BigFloat)right.Value),
                        
                    };
                }
                else if (left.Type == AlgoValueType.Integer)
                {
                    return new AlgoValue()
                    {
                        Type = AlgoValueType.Integer,
                        Value = BigInteger.Add((BigInteger)left.Value, (BigInteger)right.Value),
                        
                    };
                }
                else if (left.Type == AlgoValueType.Rational)
                {
                    return new AlgoValue()
                    {
                        Type = AlgoValueType.Rational,
                        Value = BigRational.Add((BigRational)left.Value, (BigRational)right.Value),
                        
                    };
                }
                else if (left.Type == AlgoValueType.String)
                {
                    return new AlgoValue()
                    {
                        Type = AlgoValueType.String,
                        Value = (string)left.Value + (string)right.Value,
                        
                    };
                }
            }

            //Not the same, so implicit casting must be used.

            //Float combinations.
            if (left.Type == AlgoValueType.Float)
            {
                //Integer.
                if (right.Type == AlgoValueType.Integer)
                {
                    return new AlgoValue()
                    {
                        Type = AlgoValueType.Float,
                        Value = BigFloat.Add((BigFloat)left.Value, new BigFloat((BigInteger)right.Value)),
                        
                    };
                }

                //Rational.
                else if (right.Type == AlgoValueType.Rational)
                {
                    //Getting float version of rational.
                    BigFloat rational_as_float = AlgoConversion.RationalToFloat(right);

                    //Returning.
                    return new AlgoValue()
                    {
                        Type = AlgoValueType.Float,
                        Value = BigFloat.Add(rational_as_float, (BigFloat)left.Value),
                        
                    };
                }

                //String.
                else if (right.Type == AlgoValueType.String)
                {
                    return new AlgoValue()
                    {
                        Type = AlgoValueType.String,
                        Value = ((BigFloat)left.Value).ToString() + (string)right.Value,
                        
                    };
                }
            }

            //Integer combinations.
            if (left.Type == AlgoValueType.Integer)
            {
                //Float.
                if (right.Type == AlgoValueType.Float)
                {
                    return new AlgoValue()
                    {
                        Type = AlgoValueType.Float,
                        Value = BigFloat.Add((BigFloat)right.Value, new BigFloat((BigInteger)left.Value)),
                        
                    };
                }

                //Rational.
                else if (right.Type == AlgoValueType.Rational)
                {
                    return new AlgoValue()
                    {
                        Type = AlgoValueType.Rational,
                        Value = BigRational.Add(new BigRational((BigInteger)left.Value), (BigRational)right.Value),
                        
                    };
                }

                //String.
                else if (right.Type == AlgoValueType.String)
                {
                    return new AlgoValue()
                    {
                        Type = AlgoValueType.String,
                        Value = ((BigInteger)left.Value).ToString() + (string)right.Value
                    };
                }
            }

            //Rational combinations.
            if (left.Type == AlgoValueType.Rational)
            {
                //Float.
                if (right.Type == AlgoValueType.Float)
                {
                    //Getting float version of rational.
                    BigFloat rational_as_float = AlgoConversion.RationalToFloat(right);

                    //Returning.
                    return new AlgoValue()
                    {
                        Type = AlgoValueType.Float,
                        Value = BigFloat.Add(rational_as_float, (BigFloat)right.Value),
                        
                    };
                }

                //Integer.
                else if (right.Type == AlgoValueType.Integer)
                {
                    return new AlgoValue()
                    {
                        Type = AlgoValueType.Rational,
                        Value = BigRational.Add((BigRational)left.Value, new BigRational((BigInteger)right.Value)),
                        
                    };
                }

                //String.
                else if (right.Type == AlgoValueType.String)
                {
                    return new AlgoValue()
                    {
                        Type = AlgoValueType.String,
                        Value = ((BigRational)left.Value).ToString() + (string)right.Value,
                        
                    };
                }
            }

            //String combinations.
            if (left.Type == AlgoValueType.String) {

                //Float.
                if (right.Type == AlgoValueType.Float)
                {
                    return new AlgoValue()
                    {
                        Type = AlgoValueType.String,
                        Value = (string)left.Value + ((BigFloat)right.Value).ToString(),
                        
                    };
                }
                
                //Integer.
                else if (right.Type == AlgoValueType.Integer)
                {
                    return new AlgoValue()
                    {
                        Type = AlgoValueType.String,
                        Value = (string)left.Value + ((BigInteger)right.Value).ToString(),
                        
                    };
                }

                //Rational.
                else if (right.Type == AlgoValueType.Rational)
                {
                    return new AlgoValue()
                    {
                        Type = AlgoValueType.String,
                        Value = (string)left.Value + ((BigRational)right.Value).ToString(),
                        
                    };
                }
            }

            //Could not find a match, error.
            Error.Fatal(context, "Invalid types to add, cannot add type " + left.Type.ToString() + " and type " + right.Type.ToString() + ".");
            return null;
        }

        //Take one AlgoValue from another.
        public static AlgoValue Sub(ParserRuleContext context, AlgoValue left, AlgoValue right)
        {
            //Are either of them strings?
            if (left.Type == AlgoValueType.String || right.Type == AlgoValueType.String)
            {
                Error.Fatal(context, "Cannot use a subtract operation on a string.");
                return null;
            }

            //Float combinations.
            if (left.Type == AlgoValueType.Float)
            {
                //Float.
                if (right.Type == AlgoValueType.Float)
                {
                    return new AlgoValue()
                    {
                        Type = AlgoValueType.Float,
                        Value = BigFloat.Subtract((BigFloat)left.Value, (BigFloat)right.Value),
                        
                    };
                }

                //Integer.
                else if (right.Type == AlgoValueType.Integer)
                {
                    return new AlgoValue()
                    {
                        Type = AlgoValueType.Float,
                        Value = BigFloat.Subtract((BigFloat)left.Value, new BigFloat((BigInteger)right.Value)),
                        
                    };
                }

                //Rational.
                else if (right.Type == AlgoValueType.Rational)
                {
                    //Casting rational to a float.
                    BigFloat rational_as_float = AlgoConversion.RationalToFloat(right);

                    //Subtracting.
                    return new AlgoValue()
                    {
                        Type = AlgoValueType.Float,
                        Value = BigFloat.Subtract((BigFloat)left.Value, rational_as_float),
                        
                    };
                }
            }

            //Integer combinations.
            if (left.Type == AlgoValueType.Integer)
            {
                //Integer.
                if (right.Type == AlgoValueType.Integer)
                {
                    return new AlgoValue()
                    {
                        Type = AlgoValueType.Integer,
                        Value = BigInteger.Subtract((BigInteger)left.Value, (BigInteger)right.Value),
                        
                    };
                }

                //Float.
                if (right.Type == AlgoValueType.Float)
                {
                    return new AlgoValue()
                    {
                        Type = AlgoValueType.Float,
                        Value = BigFloat.Subtract(new BigFloat((BigInteger)left.Value), (BigFloat)right.Value),
                        
                    };
                }

                //Rational.
                else if (right.Type == AlgoValueType.Rational)
                {
                    return new AlgoValue()
                    {
                        Type = AlgoValueType.Rational,
                        Value = BigRational.Subtract(new BigRational((BigInteger)left.Value), (BigRational)right.Value),
                        
                    };
                }
            }

            //Rational combinations.
            if (left.Type == AlgoValueType.Rational)
            {
                //Float.
                if (right.Type == AlgoValueType.Float)
                {
                    //Casting rational to float.
                    BigFloat rational_as_float = AlgoConversion.RationalToFloat(right);

                    //Returning.
                    return new AlgoValue()
                    {
                        Type = AlgoValueType.Float,
                        Value = BigFloat.Subtract((BigFloat)left.Value, (BigFloat)right.Value),
                        
                    };
                }

                //Integer.
                else if (right.Type == AlgoValueType.Integer)
                {
                    return new AlgoValue()
                    {
                        Type = AlgoValueType.Rational,
                        Value = BigRational.Subtract((BigRational)left.Value, new BigRational((BigInteger)right.Value)),
                        
                    };
                }

                //Rational.
                else if (right.Type == AlgoValueType.Rational)
                {
                    return new AlgoValue()
                    {
                        Type = AlgoValueType.Rational,
                        Value = BigRational.Subtract((BigRational)left.Value, (BigRational)right.Value),
                        
                    };
                }
            }

            //Did not find a combination, error and return.
            Error.Fatal(context, "Invalid types to subtract, cannot subtract type " + left.Type.ToString() + " and type " + right.Type.ToString() + ".");
            return null;
        }
        
        //Round an AlgoValue to a given number of significant figures.
        public static AlgoValue Round(ParserRuleContext context, AlgoValue value, int significant_figures)
        {
            //Is the value to round a float?
            if (value.Type != AlgoValueType.Float)
            {
                //Convert it to a float then.
                value = ConvertType(context, value, AlgoValueType.Float);
            }

            //Attempt to convert that value to a double.
            if ((BigFloat)value.Value > double.MaxValue)
            {
                Error.Fatal(context, "Cannot round this value, it has become too large to process.");
                return null;
            }

            //Can convert, go for it.
            double toRound = double.Parse(((BigFloat)value.Value).ToString());

            //For some reason, significant figure rounding with doubles doesn't work on negatives (go figure).
            //Flip it beforehand, then flip it back. Bit hacky, but okay.
            bool toBeFlipped = false;
            if (toRound < 0)
            {
                toBeFlipped = true;
                toRound = -toRound;
            }
            string rounded = toRound.Trim(significant_figures);
            if (toBeFlipped)
            {
                rounded = "-" + rounded;
            }

            //Return the rounded value.
            return new AlgoValue()
            {
                Type = AlgoValueType.Float,
                Value = BigFloat.Parse(rounded),
                
            };
        }

        //Convert an AlgoValue to a specific type.
        public static AlgoValue ConvertType(ParserRuleContext context, AlgoValue value, AlgoValueType type)
        {
            if (type == AlgoValueType.Boolean)
            {
                return new AlgoValue()
                {
                    Type = AlgoValueType.Boolean,
                    Value = AlgoComparators.GetBooleanValue(value, context),
                    
                };
            }

            //Cast to float.
            else if (type == AlgoValueType.Float)
            {
                switch (value.Type)
                {
                    //BOOLEAN.
                    case AlgoValueType.Boolean:

                        //Get the integer value.
                        int newValue = -1;
                        if ((bool)value.Value)
                        {
                            newValue = 1;
                        } else
                        {
                            newValue = 0;
                        }

                        //Return.
                        return new AlgoValue()
                        {
                            Type = AlgoValueType.Float,
                            Value = new BigFloat(newValue),
                            
                        };
                    
                    //FLOAT.
                    case AlgoValueType.Float:
                        return value;
                    
                    //INTEGER.
                    case AlgoValueType.Integer:
                        return new AlgoValue()
                        {
                            Type = AlgoValueType.Float,
                            Value = new BigFloat((BigInteger)value.Value),
                            
                        };
                    
                    //RATIONAL.
                    case AlgoValueType.Rational:
                        //Casting rational to a float.
                        BigFloat rational_as_float = AlgoConversion.RationalToFloat(value);

                        //Returning.
                        return new AlgoValue()
                        {
                            Type = AlgoValueType.Float,
                            Value = rational_as_float,
                            
                        };

                    default:
                        Error.Fatal(context, "Cannot implicitly convert value of type " + value.Type.ToString() + " to a float.");
                        return null;
                }
            }

            //Cast to integer.
            else if (type == AlgoValueType.Integer)
            {
                switch (value.Type)
                {
                    case AlgoValueType.Float:
                        Error.Warning(context, "Implicitly casting from float to integer will likely cause loss of data.");
                        if ((BigFloat)value.Value > long.MaxValue)
                        {
                            Error.Fatal(context, "Cannot cast this value to integer, as it has become too large.");
                            return null;
                        }

                        BigInteger converted = new BigInteger(long.Parse(((BigFloat)value.Value).ToString()));
                        return new AlgoValue()
                        {
                            Type = AlgoValueType.Integer,
                            Value = converted,
                            
                        };

                    case AlgoValueType.Integer:
                        return value;

                    case AlgoValueType.Rational:
                        Error.Warning(context, "Implicitly casting from rational to intger will likely cause loss of data.");

                        //Casting rational to a float.
                        BigFloat rational_as_float = AlgoConversion.RationalToFloat(value);

                        if (rational_as_float > long.MaxValue)
                        {
                            Error.Fatal(context, "Cannot cast this value to integer, as it has become too large.");
                            return null;
                        }

                        //Returning.
                        BigInteger converted_rt = new BigInteger(long.Parse((rational_as_float).ToString()));
                        return new AlgoValue()
                        {
                            Type = AlgoValueType.Integer,
                            Value = converted_rt,
                            
                        };
                        
                    default:
                        Error.Fatal(context, "Cannot implicitly convert value of type " + value.Type.ToString() + " to an integer.");
                        return null;
                }
            }

            //Cast to rational.
            else if (type == AlgoValueType.Rational)
            {
                switch (value.Type)
                {
                    case AlgoValueType.Integer:
                        return new AlgoValue()
                        {
                            Type = AlgoValueType.Rational,
                            Value = new BigRational((BigInteger)value.Value),
                            
                        };

                    default:
                        Error.Fatal(context, "Cannot implicitly convert value of type " + value.Type.ToString() + " to a rational.");
                        return null;
                }
            }

            //Cast to string.
            else if (type == AlgoValueType.String)
            {
                switch (value.Type)
                {
                    case AlgoValueType.Boolean:
                        return new AlgoValue()
                        {
                            Type = AlgoValueType.String,
                            Value = ((bool)value.Value).ToString(),
                        };

                    case AlgoValueType.Float:
                        return new AlgoValue()
                        {
                            Type = AlgoValueType.String,
                            Value = ((BigFloat)value.Value).ToString(),
                        };

                    case AlgoValueType.Integer:
                        return new AlgoValue()
                        {
                            Type = AlgoValueType.String,
                            Value = ((BigInteger)value.Value).ToString(),
                            
                        };

                    case AlgoValueType.Rational:
                        return new AlgoValue()
                        {
                            Type = AlgoValueType.String,
                            Value = ((BigRational)value.Value).ToString(),
                            
                        };

                    case AlgoValueType.String:
                        return value;

                    default:
                        Error.Fatal(context, "Cannot implicitly convert value of type " + value.Type.ToString() + " to a string.");
                        return null;
                }
            }

            //Cannot implicitly cast to this type.
            else
            {
                Error.Fatal(context, "Cannot implicitly cast to type " + type.ToString() + ".");
                return null;
            }
        }
    }
}