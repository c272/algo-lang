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
    }
}
