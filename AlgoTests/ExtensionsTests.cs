using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algo;
using System.Numerics;

namespace Algo.Tests.Extensions
{
    /// <summary>
    /// Tests the byte utility extensions for Algo.
    /// </summary>
    [TestFixture]
    public class ByteExtensionsTests
    {
        [Test]
        public void BytesToHexString_Expected()
        {
            //Setup.
            byte[] bytes = new byte[] { 0x00, 0xFF };
            string byteString = bytes.ToHexString();

            //Assert.
            Assert.That(byteString == "0x00FF");
        }

        [Test]
        public void AddByteArraysNumeric_Expected()
        {
            //Setup.
            BigInteger first = new BigInteger(10);
            BigInteger second = new BigInteger(5);

            //Act.
            byte[] added = first.ToByteArray().SumWith(second.ToByteArray());
            BigInteger addedBigInt = new BigInteger(added);

            //Assert.
            Assert.That(addedBigInt == 15);
        }
    }

    /// <summary>
    /// Tests the string utility extensions for Algo.
    /// </summary>
    [TestFixture]
    public class StringExtensionsTests
    {
        [Test]
        public void ReverseString_Expected()
        {
            //Setup.
            string original = "thisisateststring";
            string reversed = "gnirtstsetasisiht";

            Assert.That(original.Reverse() == reversed);
        }

        [Test]
        public void StringHexToBytes_Expected()
        {
            //Convert a valid hex string to bytes.
            string validHex = "0x0F";
            byte realByte = 0x0F;

            //Assert.
            byte[] bytes = validHex.ToByteArray();
            Assert.That(bytes[0] == realByte);
        }

        [Test]
        public void StringHexToBytes_Unexpected()
        {
            //Try to convert an invalid hex string to bytes.
            string invalidHex = "0xFG";
            
            //Assert.
            try
            {
                byte[] bytes = invalidHex.ToByteArray();
                Assert.Fail();
            }
            catch
            {
                Assert.Pass();
            }
        }
    }
}
