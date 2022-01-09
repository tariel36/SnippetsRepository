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

using System.Collections.Generic;

namespace NutaDev.CsLib.Collections.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="IList{T}"/> interface.
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// Indicates whether <paramref name="list"/> is null or empty.
        /// </summary>
        /// <typeparam name="T">Type of element.</typeparam>
        /// <param name="list">Object to check.</param>
        /// <returns>True if null or empty.</returns>
        public static bool IsNullOrEmpty<T>(this IList<T> list)
        {
            return list == null || list.Count == 0;
        }

        /// <summary>
        /// Inverts the order of the elements in the list.
        /// </summary>
        /// <typeparam name="T">Type of element.</typeparam>
        /// <param name="list">List to invert.</param>
        /// <returns>List with inverted order of the elements.</returns>
        public static IList<T> Reverse<T>(this IList<T> list)
        {
            int count = (list.Count % 2 == 0 ? list.Count / 2 : (list.Count - 1) / 2);

            for (int i = 0; i < count; ++i)
            {
                T tmp = list[i];
                list[i] = list[list.Count - 1 - i];
                list[list.Count - 1 - i] = tmp;
            }

            return list;
        }

        /// <summary>
        /// Returns index of the element in the collection using reference comparison.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="list">Source list.</param>
        /// <param name="target">Searched element.</param>
        /// <returns>Index of element or -1 if not found.</returns>
        public static int IndexOfByReference<T>(IList<T> list, T target)
        {
            if (target == null || list == null || list.Count == 0) { return -1; }

            for (int idx = 0; idx < list.Count; ++idx)
            {
                if (ReferenceEquals(list[idx], target))
                {
                    return idx;
                }
            }

            return -1;
        }
    }
}
