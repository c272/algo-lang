namespace Algo
{
    //A single value in Algo.
    public struct AlgoValue
    {
        public object Value;
        public AlgoValueType Type;
    }

    public enum AlgoValueType
    {
        String,
        Integer,
        Float,
        Rational
    }
}