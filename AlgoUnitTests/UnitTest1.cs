using System;
using System.Numerics;
using Algo;
using ExtendedNumerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlgoUnitTests
{
    [TestClass]
    public class OperatorTests
    {
        [TestMethod]
        //Tests the AlgoOperators addition algorithm.
        public void AdditionTest()
        {
            //Artificially create some values.
            AlgoValue Positive = new AlgoValue()
            {
                Type = AlgoValueType.Integer,
                Value = new BigInteger(21)
            };
            AlgoValue Negative = new AlgoValue()
            {
                Type = AlgoValueType.Float,
                Value = new BigFloat(-34.323423)
            };

            //Check whether the value is correct.
            AlgoValue added = AlgoOperators.Add(null, Positive, Negative);

            //Check the value was cast correctly.
            Assert.IsTrue(added.Type == AlgoValueType.Float, "A value was not properly casted when adding.");

            //Check the value is correct.
            Assert.IsTrue((BigFloat)added.Value == new BigFloat(-13.323423), "A value was not properly calculated when adding.");
        }

        [TestMethod]
        public void SubtractionTest()
        {

        }
    }
}
