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
using System.Collections.ObjectModel;

namespace NutaDev.CsLib.Collections.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="ObservableCollection{T}"/> class.
    /// </summary>
    public static class ObservableCollectionExtensions
    {
        /// <summary>
        /// Adds elements of the <paramref name="items"/> to <paramref name="collection"/>.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="collection">Target collection.</param>
        /// <param name="items">Items to add.</param>
        public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> items)
        {
            if (items == null)
            {
                return;
            }

            foreach (T item in items)
            {
                collection.Add(item);
            }
        }

        /// <summary>
        /// Removes all elements that meets <paramref name="predicate"/> condition.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="collection">Source collection.</param>
        /// <param name="predicate">Boolean check if element should be removed.</param>
        public static void RemoveWhere<T>(this ObservableCollection<T> collection, Func<T, bool> predicate)
        {
            for (int i = 0; i < collection.Count; ++i)
            {
                if (predicate(collection[i]))
                {
                    collection.RemoveAt(i--);
                }
            }
        }

        /// <summary>
        /// Replaces <paramref name="oldValue"/> with <paramref name="newValue"/>. Comprasion is based on IndexOf.
        /// </summary>
        /// <typeparam name="TSource">Source type.</typeparam>
        /// <param name="source">Source of elements.</param>
        /// <param name="oldValue">Value to be replaced.</param>
        /// <param name="newValue">New value.</param>
        public static void Replace<TSource>(this ObservableCollection<TSource> source, TSource oldValue, TSource newValue)
        {
            int oldIdx = source.IndexOf(oldValue);
            if (oldIdx >= 0)
            {
                source.RemoveAt(oldIdx);
                source.Insert(oldIdx, newValue);
            }
        }
    }
}
