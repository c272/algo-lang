using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algo.Tests.Conversion
{
    /// <summary>
    /// Tests conversions between types. This only tests IMPLICIT conversion.
    /// </summary>
    [TestFixture]
    public class ImplicitConversionTests
    {
        [Test]
        public void NullConversion()
        {
            //Create a test conversion for all of types from null.
            AlgoValue nullVal = AlgoValue.Null;
            ANTLRDebug.EnterTestMode();

            //Test all conversions (these are expected to throw).
            for (int i = 0; i < Enum.GetNames(typeof(AlgoValueType)).Length; i++)
            {
                try
                {
                    var attemptConv = AlgoOperators.ConvertType(null, nullVal, (AlgoValueType)i);
                }
                catch { Assert.Pass(); }
            }

            Assert.Fail();
        }

        [Test]
        public void StringConversions()
        {
            //Create test conversions for strings.
            AlgoValue stringVal = new AlgoValue()
            {
                Type = AlgoValueType.String,
                Value = "the quick brown fox jumps over the lazy dog"
            };
            ANTLRDebug.EnterTestMode();

            //Values that should pass.
            AlgoValue testVal;
            try
            {
                testVal = AlgoOperators.ConvertType(null, stringVal, AlgoValueType.String);
            }
            catch 
            {
                Assert.Fail();
            }

            //Values that should fail.
            for (int i=0; i<Enum.GetNames(typeof(AlgoValueType)).Length; i++)
            {
                if ((AlgoValueType)i == AlgoValueType.String) { continue; }

                try
                {
                    testVal = AlgoOperators.ConvertType(null, stringVal, (AlgoValueType)i);
                    Assert.Fail();
                }
                catch { }
            }

            Assert.Pass();
        }
    }
}
