using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algo.Sharpie
{
    public static class Extensions
    {
        /// <summary>
        /// Get the array slice between the two indexes.
        /// Inclusive for start index, exclusive for end index.
        /// </summary>
        public static T[] Slice<T>(this T[] source, int start, int end)
        {
            // Return a blank array if the start's bigger than the end of the array.
            if (start >= source.Length)
            {
                return new T[0];
            }

            // Handles negative ends.
            if (end <= -1)
            {
                end = source.Length;
            }
            int len = end - start;

            // Return new array.
            T[] res = new T[len];
            for (int i = 0; i < len; i++)
            {
                res[i] = source[i + start];
            }
            return res;
        }
    }
}
