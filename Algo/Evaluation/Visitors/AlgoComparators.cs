using Antlr4.Runtime;
using ExtendedNumerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Algo
{
    public class AlgoComparators
    {
        //Binary AND.
        public static bool AND(ParserRuleContext context, AlgoValue left, AlgoValue right)
        {
            //Get value for left and right type.
            bool leftBV = GetBooleanValue(left, context);
            bool rightBV = GetBooleanValue(right, context);

            //Check if these are both true.
            return (leftBV && rightBV);
        }

        //Binary OR.
        public static bool OR(ParserRuleContext context, AlgoValue left, AlgoValue right)
        {
            //Get value for left and right type.
            bool leftBV = GetBooleanValue(left, context);
            bool rightBV = GetBooleanValue(right, context);

            //Check if one of these are true.
            return (leftBV || rightBV);
        }

        //Binary equals.
        public static bool _Equals(ParserRuleContext context, AlgoValue left, AlgoValue right)
        {
            return left._Equals(right);
        }

        //Greater than.
        public static bool GreaterThan(ParserRuleContext context, AlgoValue left, AlgoValue right, bool equalTo)
        {
            //Get the values for both left and right in a specific format.
            //(Statements must be in order of casting heirarchy, top down).
            if (left.Type == AlgoValueType.Float || right.Type == AlgoValueType.Float)
            {
                //Convert both to float.
                AlgoValue leftConverted = AlgoOperators.ConvertType(context, left, AlgoValueType.Float);
                AlgoValue rightConverted = AlgoOperators.ConvertType(context, right, AlgoValueType.Float);

                //Check if the left is greater than the right.
                if (equalTo)
                {
                    return ((BigFloat)left.Value >= (BigFloat)right.Value);
                }
                else
                {
                    return ((BigFloat)left.Value > (BigFloat)right.Value);
                }
            }
            else if (left.Type == AlgoValueType.Rational || right.Type == AlgoValueType.Rational)
            {
                //Convert both to rational.
                AlgoValue leftConverted = AlgoOperators.ConvertType(context, left, AlgoValueType.Rational);
                AlgoValue rightConverted = AlgoOperators.ConvertType(context, right, AlgoValueType.Rational);

                //Check if the left is greater than the right.
                if (equalTo)
                {
                    return ((BigRational)left.Value >= (BigRational)right.Value);
                }
                else
                {
                    return ((BigRational)left.Value > (BigRational)right.Value);
                }
            }
            else if (left.Type == AlgoValueType.Integer || right.Type == AlgoValueType.Integer)
            {
                //Convert both to integer.
                AlgoValue leftConverted = AlgoOperators.ConvertType(context, left, AlgoValueType.Integer);
                AlgoValue rightConverted = AlgoOperators.ConvertType(context, right, AlgoValueType.Integer);

                //Check if the left is greater than the right.
                if (equalTo)
                {
                    return ((BigInteger)left.Value >= (BigInteger)right.Value);
                }
                else
                {
                    return ((BigInteger)left.Value > (BigInteger)right.Value);
                }
            }
            else
            {
                Error.Fatal(context, "Cannot compare values with type " + left.Type.ToString() + " and " + right.Type.ToString() + ".");
                return false;
            }
        }

        //Less than.
        public static bool LessThan(ParserRuleContext context, AlgoValue left, AlgoValue right, bool equalTo)
        {
            //Get the values for both left and right in a specific format.
            //(Statements must be in order of casting heirarchy, top down).
            if (left.Type == AlgoValueType.Float || right.Type == AlgoValueType.Float)
            {
                //Convert both to float.
                AlgoValue leftConverted = AlgoOperators.ConvertType(context, left, AlgoValueType.Float);
                AlgoValue rightConverted = AlgoOperators.ConvertType(context, right, AlgoValueType.Float);

                //Check if the left is less than the right.
                if (equalTo)
                {
                    return ((BigFloat)left.Value <= (BigFloat)right.Value);
                }
                else
                {
                    return ((BigFloat)left.Value < (BigFloat)right.Value);
                }
            }
            else if (left.Type == AlgoValueType.Rational || right.Type == AlgoValueType.Rational)
            {
                //Convert both to rational.
                AlgoValue leftConverted = AlgoOperators.ConvertType(context, left, AlgoValueType.Rational);
                AlgoValue rightConverted = AlgoOperators.ConvertType(context, right, AlgoValueType.Rational);

                //Check if the left is less than the right.
                if (equalTo)
                {
                    return ((BigRational)left.Value <= (BigRational)right.Value);
                }
                else
                {
                    return ((BigRational)left.Value < (BigRational)right.Value);
                }
            }
            else if (left.Type == AlgoValueType.Integer || right.Type == AlgoValueType.Integer)
            {
                //Convert both to integer.
                AlgoValue leftConverted = AlgoOperators.ConvertType(context, left, AlgoValueType.Integer);
                AlgoValue rightConverted = AlgoOperators.ConvertType(context, right, AlgoValueType.Integer);

                //Check if the left is less than the right.
                if (equalTo)
                {
                    return ((BigInteger)left.Value <= (BigInteger)right.Value);
                }
                else
                {
                    return ((BigInteger)left.Value < (BigInteger)right.Value);
                }
            }
            else
            {
                Error.Fatal(context, "Cannot compare values with type " + left.Type.ToString() + " and " + right.Type.ToString() + ".");
                return false;
            }
        }

        //Get boolean value for the given AlgoValue.
        public static bool GetBooleanValue(AlgoValue value, ParserRuleContext context)
        {
            switch (value.Type)
            {
                case AlgoValueType.Boolean:
                    return (bool)value.Value;

                case AlgoValueType.Integer:
                    if ((BigInteger)value.Value == 1)
                    {
                        return true;
                    } else if ((BigInteger)value.Value == 0)
                    {
                        return false;
                    } else
                    {
                        Error.Fatal(context, "Cannot implicitly cast an integer that is not one or zero to boolean.");
                        return false;
                    }

                default:
                    Error.Fatal(context, "Cannot implicitly cast type " + value.Type.ToString() + " to a boolean.");
                    return false;
            }
        }
    }
}
