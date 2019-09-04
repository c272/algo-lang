using System;
namespace Algo
{
    public static class StringExtensions
    {
        //Reverses a string as an extension method.
        public static string Reverse(this string s)
        {
            char[] cArray = s.ToCharArray();
            string reverse = String.Empty;
            for (int i = cArray.Length - 1; i > -1; i--)
            {
                reverse += cArray[i];
            }
            return reverse;
        }

        //Converts the hex string to a byte array.
        public static byte[] ToByteArray(this string hex_raw)
        {
            //Chop off 0x.
            string hex = hex_raw.Substring(2);

            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }
    }
}
