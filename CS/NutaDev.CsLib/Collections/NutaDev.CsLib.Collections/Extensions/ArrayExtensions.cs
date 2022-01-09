// The MIT License (MIT)
//
// Copyright (c) 2022 tariel36
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System;
using System.Linq;

namespace NutaDev.CsLib.Collections.Extensions
{
    /// <summary>
    /// Class that provides various <see cref="System.Array"/> extensions.
    /// </summary>
    public static class ArrayExtensions
    {
        /// <summary>
        /// Returns minimum value from the provided values.
        /// </summary>
        /// <typeparam name="T">Type of values.</typeparam>
        /// <param name="values">Values to compare.</param>
        /// <returns>Minimum value.</returns>
        public static T Min<T>(params T[] values)
        {
            return values.Min();
        }

        /// <summary>
        /// Returns maximum value from the provided values.
        /// </summary>
        /// <typeparam name="T">Type of values.</typeparam>
        /// <param name="values">Values to compare.</param>
        /// <returns>Maximum value.</returns>
        public static T Max<T>(params T[] values)
        {
            return values.Max();
        }

        /// <summary>
        /// Indicates whether <paramref name="index"/> is within <paramref name="array"/> bounds.
        /// </summary>
        /// <typeparam name="T">Array type.</typeparam>
        /// <param name="array">Array to check.</param>
        /// <param name="index">Index to check.</param>
        /// <param name="getLowerBound">Returns lower bound to check against.</param>
        /// <param name="getUpperBounds">Returns upper bound to check against.</param>
        /// <returns>True if <paramref name="index"/> is withing bounds.</returns>
        public static bool IndexWithinBounbds<T>(this T[] array, int index, Func<T[], int> getLowerBound, Func<T[], int> getUpperBounds)
        {
            return getLowerBound(array) >= 0 && index < getUpperBounds(array);
        }

        /// <summary>
        /// Indicates whether <paramref name="index"/> is within <paramref name="array"/> bounds. Default bounds are '<paramref name="index"/> &gt;= 0' for lower, and '<paramref name="index"/> &lt; <paramref name="array"/>.Length' for upper.
        /// </summary>
        /// <typeparam name="T">Array type.</typeparam>
        /// <param name="array">Array to check.</param>
        /// <param name="index">Index to check.</param>
        /// <returns>True if <paramref name="index"/> is withing bounds.</returns>
        public static bool IndexWithinBounbds<T>(this T[] array, int index)
        {
            return index >= 0 && index < array.Length;
        }

        /// <summary>
        /// Returns first element in the <paramref name="arr"/> that meets the <paramref name="predicate"/> or default(<paramref name="{T}"/>) if not found.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="arr">Source array.</param>
        /// <param name="predicate">Boolean predicate performed on each element.</param>
        /// <returns>First element that meets the <paramref name="predicate"/> or default(<paramref name="{T}"/>) if not found.</returns>
        public static T FirstOrDefault<T>(this T[,] arr, Func<T, bool> predicate)
        {
            for (int i = 0; i < arr.GetLength(0); ++i)
            {
                for (int j = 0; j < arr.GetLength(1); ++j)
                {
                    if (predicate(arr[i, j]))
                    {
                        return arr[i, j];
                    }
                }
            }

            return default(T);
        }

        /// <summary>
        /// Checks if <paramref name="array"/> is null or empty (contains no elements, length is equal to 0).
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="array">Target array.</param>
        /// <returns>True if <paramref name="array"/> is null or empty, false otherwise.</returns>
        public static bool IsNullOrEmpty<T>(this T[] array)
        {
            return array == null || array.Length == 0;
        }

        /// <summary>
        /// Transposes the provided matrix (2d array).
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="array">Matrix to transpose.</param>
        /// <returns>Transposed matrix, null if <paramref name="array"/> is null or <paramref name="array"/> if length is 0.</returns>
        public static T[][] Transpose<T>(this T[][] array)
        {
            if (array == null)
            {
                return null;
            }

            if (array.Length == 0)
            {
                return array;
            }

            T[][] result = new T[array[0].Length][];

            for (int i = 0; i < array.Length; ++i)
            {
                for (int j = 0; j < array[i].Length; ++j)
                {
                    if (result[j] == null)
                    {
                        result[j] = new T[array.Length];
                    }

                    result[j][i] = array[i][j];
                }
            }

            return result;
        }

        /// <summary>
        /// Treis to get value from <paramref name="array"/> at <paramref name="index"/>.
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <param name="array">Source array.</param>
        /// <param name="index">Object index.</param>
        /// <returns>Object if index and type match, default otherwise.</returns>
        public static T TryGetValue<T>(this Array array, int index)
        {
            if (array == null) { return default(T); }
            if (index >= array.Length) { return default(T); }

            object value = array.GetValue(index);

            if (value is T tValue)
            {
                return tValue;
            }

            return default(T);
        }
    }
}
