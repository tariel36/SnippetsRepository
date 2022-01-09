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

namespace NutaDev.CsLib.Collections.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="Stack{T}"/>.
    /// </summary>
    public static class StackExtensions
    {
        /// <summary>
        /// Pushes range to <paramref name="stack"/>. Items are pushed in <paramref name="collection"/>'s order.
        /// </summary>
        /// <typeparam name="T">Type of data.</typeparam>
        /// <param name="stack">Target stack.</param>
        /// <param name="collection">Source collection.</param>
        /// <returns>Reference to stack.</returns>
        public static Stack<T> PushRange<T>(this Stack<T> stack, IEnumerable<T> collection)
        {
            foreach (T item in collection)
            {
                stack.Push(item);
            }

            return stack;
        }

        /// <summary>
        /// Pops <paramref name="count"/> items from <paramref name="stack"/>.
        /// </summary>
        /// <typeparam name="T">Type of data.</typeparam>
        /// <param name="stack">Target stack.</param>
        /// <param name="count">Items to pop.</param>
        /// <returns>Reference to stack.</returns>
        public static IEnumerable<T> PopRange<T>(this Stack<T> stack, int count)
        {
            List<T> items = new List<T>(count);

            int countToPop = Math.Min(count, stack.Count);

            for (int i = 0; i < countToPop; ++i)
            {
                items.Add(stack.Pop());
            }

            return items;
        }
    }
}
