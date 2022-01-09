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
using System.Collections.Generic;
using System.Linq;

namespace NutaDev.CsLib.Types.Extensions
{
    /// <summary>
    /// This class provides extension methods for <see cref="string"/> type.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Splits the string with provided split options based on split string.
        /// </summary>
        /// <param name="str">String to split.</param>
        /// <param name="splitStr">Splitting string.</param>
        /// <param name="splitOptions">Split options.</param>
        /// <returns>Splitted string.</returns>
        public static IEnumerable<string> Separate(this string str, string pattern, StringSplitOptions splitOptions = StringSplitOptions.None)
        {
            int i;
            int j;
            string substring;

            for (i = 0, j = 0; i < str.Length; ++i)
            {
                int idx = str.IndexOf(pattern, i);

                if (idx >= 0)
                {
                    substring = str.Subtext(i, idx);

                    if (string.Equals(substring, pattern)
                        || splitOptions == StringSplitOptions.RemoveEmptyEntries && string.Equals(substring, string.Empty))
                    {
                        continue;
                    }

                    yield return substring;

                    i = idx + pattern.Length - 1;
                    j = i;
                }
            }

            substring = str.Subtext(j + 1, i);

            if (!string.Equals(substring, pattern)
                && (splitOptions == StringSplitOptions.None || (splitOptions == StringSplitOptions.RemoveEmptyEntries && !string.Equals(substring, string.Empty))))
            {
                yield return substring;
            }
        }

        /// <summary>
        /// Splits the string with provided split options based on split string.
        /// </summary>
        /// <param name="str">String to split.</param>
        /// <param name="splitStr">Splitting string.</param>
        /// <param name="splitOptions">Split options.</param>
        /// <returns>Splitted string.</returns>
        public static string[] Split(this string str, string splitStr, StringSplitOptions splitOptions = StringSplitOptions.None)
        {
            return str.Split(new[] { splitStr }, splitOptions);
        }

        /// <summary>
        /// Returns substring from between <paramref name="startIdx"/> and <paramref name="endIdx"/>.
        /// </summary>
        /// <param name="str">Source string.</param>
        /// <param name="startIdx">Substring start index.</param>
        /// <param name="endIdx">Substring end index.</param>
        /// <returns>Substring instance.</returns>
        public static string Subtext(this string str, int startIdx, int endIdx)
        {
            return str.Substring(startIdx, endIdx - startIdx);
        }

        /// <summary>
        /// Returns indexed length of the string which is equal to last index.
        /// </summary>
        /// <param name="str">Source string.</param>
        /// <returns>String length.</returns>
        public static int IndexedLength(this string str)
        {
            return string.IsNullOrEmpty(str) ? 0 : str.Length - 1;
        }

        /// <summary>
        /// Counts all appearances of given character.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="c">The character to count.</param>
        /// <returns>Count of <paramref name="c"/>.</returns>
        public static int Count(this string str, char c)
        {
            return str.Count(x => x == c);
        }

        /// <summary>
        /// Inserts string at given position counting from the end of the string.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="pos">The position counted from the end.</param>
        /// <param name="insertStr">String which will be inserted.</param>
        /// <returns>The merged string.</returns>
        public static string InsertBack(this string str, int pos, string insertStr)
        {
            string str1 = str.Substring(0, str.Length - pos);
            string str2 = str.Substring(str.Length - pos);

            return str1 + insertStr + str2;
        }

        /// <summary>
        /// Joins <paramref name="strings"/> with <paramref name="separator"/>.
        /// </summary>
        /// <param name="strings">Strings to join.</param>
        /// <param name="separator">Separator that joins strings.</param>
        /// <returns><paramref name="strings"/> joined with <paramref name="separator"/>.</returns>
        public static string Join(this string[] strings, string separator = ", ")
        {
            return string.Join(separator, strings);
        }

        /// <summary>
        /// Performs equality check using <see cref="StringComparison.InvariantCultureIgnoreCase"/>.
        /// </summary>
        /// <param name="left">First string to compare.</param>
        /// <param name="right">Second string to compare.</param>
        /// <returns>True if the value of the <paramref name="left"/> parameter is equal to the value of the <paramref name="right"/> parameter; otherwise, false.</returns>
        public static bool InvariantEquals(this string left, string right)
        {
            return string.Equals(left, right, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
