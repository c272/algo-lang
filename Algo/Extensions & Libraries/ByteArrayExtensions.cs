using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algo
{
    public static class ByteArrayExtensions
    {
        //Byte array to string extension.
        public static string ToHexString(this byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return "0x" + hex.ToString().ToUpper();
        }

        //Add another byte array, return result.
        public static byte[] SumWith(this byte[] first, byte[] second)
        {
            var i1 = BitConverter.ToInt64(first, 0);
            var i2 = BitConverter.ToInt64(second, 0);
            var sum = i1 + i2;

            return BitConverter.GetBytes(sum);
        }
    }
}
