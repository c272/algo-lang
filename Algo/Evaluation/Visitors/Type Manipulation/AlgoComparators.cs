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

        //Checks whether two lists are equal to each other.
        public static bool ListsEqual(AlgoValue leftval, AlgoValue rightval)
        {
            //Casting.
            var left = (List<AlgoValue>)leftval.Value;
            var right = (List<AlgoValue>)rightval.Value;

            //Simple length check.
            if (left.Count != right.Count)
            {
                return false;
            }

            //Loop over, check.
            for (int i = 0; i < left.Count; i++)
            {
                if (!left[i]._Equals(right[i]))
                {
                    return false;
                }
            }

            return true;
        }

        //Checks whether two objects are equal to each other.
        public static bool ObjectsEqual(AlgoValue leftval, AlgoValue rightval)
        {
            //Casting.
            var left = (AlgoObject)leftval.Value;
            var right = (AlgoObject)rightval.Value;

            //Simple length check.
            if (left.ObjectScopes.GetScopes()[0].Count != right.ObjectScopes.GetScopes()[0].Count)
            {
                return false;
            }

            //Loop over child properties.
            List<string> checkedProps = new List<string>();
            foreach (var prop in left.ObjectScopes.GetScopes()[0])
            {
                //Check whether it exists in the other object.
                bool found = false;
                foreach (var c_prop in right.ObjectScopes.GetScopes()[0])
                { 
                    if (prop.Key == c_prop.Key)
                    {
                        found = true;

                        //Found it! Check the values are equal.
                        if (!prop.Value._Equals(c_prop.Value))
                        {
                            //Nope.
                            return false;
                        }

                        checkedProps.Add(prop.Key);
                        break;
                    }
                }

                if (!found) { return false; }
            }

            //Check remaining properties.
            foreach (var prop in right.ObjectScopes.GetScopes()[0])
            {
                //Don't check already checked properties.
                if (checkedProps.Contains(prop.Key))
                {
                    continue;
                }

                bool found = false;
                foreach (var c_prop in left.ObjectScopes.GetScopes()[0])
                {
                    if (prop.Key == c_prop.Key)
                    {
                        found = true;

                        //Found it, are the values equal?
                        if (!prop.Value._Equals(c_prop.Value))
                        {
                            //Nope.
                            return false;
                        }

                        break;
                    }
                }

                if (!found) { return false; }
            }

            //Return true, checks passed.
            return true;
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
