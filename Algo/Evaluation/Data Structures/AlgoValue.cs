using System;
using System.Collections.Generic;
using System.Numerics;
using ExtendedNumerics;

namespace Algo
{
    //A single value in Algo.
    public class AlgoValue
    {
        public object Value;
        public AlgoValueType Type;

        //Override for checking whether AlgoValues are equal.
        public bool _Equals(AlgoValue obj)
        {
            //Check if value types are the same.
            if (Type != obj.Type)
            {
                //No.
                return false;
            }

            //Are the actual values themselves the same?
            var objVal = obj.Value;
            switch (Type)
            {
                case AlgoValueType.String:
                    return (string)Value == (string)objVal;
                case AlgoValueType.Boolean:
                    return (bool)Value == (bool)objVal;
                case AlgoValueType.Float:
                    return ((BigFloat)Value).Equals((BigFloat)objVal);
                case AlgoValueType.Integer:
                    return ((BigInteger)Value).Equals((BigInteger)objVal);
                case AlgoValueType.List:
                    return AlgoComparators.ListsEqual(this, obj);
                case AlgoValueType.Null:
                    return true;
                case AlgoValueType.Object:
                    return AlgoComparators.ObjectsEqual(this, obj);
                case AlgoValueType.Rational:
                    return ((BigRational)Value).Equals((BigRational)objVal);
                default:
                    Error.FatalNoContext("Cannot compare whether two " + Type.ToString() + " values are equal.");
                    return false;
            }
        }

        //The Null AlgoValue.
        public static AlgoValue Null = new AlgoValue()
        {
            Type = AlgoValueType.Null,
            Value = null
        };

        //True and false.
        public static AlgoValue True = new AlgoValue()
        {
            Type = AlgoValueType.Boolean,
            Value = true
        };
        public static AlgoValue False = new AlgoValue()
        {
            Type = AlgoValueType.Boolean,
            Value = false
        };

        //Break value.
        public static AlgoValue Break = new AlgoValue()
        {
            Type = AlgoValueType.Break,
            Value = null
        };

        //Continue value.
        public static AlgoValue Continue = new AlgoValue()
        {
            Type = AlgoValueType.Continue,
            Value = null
        };
    }

    public enum AlgoValueType
    {
        String,
        Integer,
        Float,
        Rational,
        Boolean,
        List,
        Function,
        EmulatedFunction,
        Object,
        Break,
        Continue,
        Null,
        Bytes
    }
}