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

using NutaDev.CsLib.Collections.Comparers;
using NutaDev.CsLib.Maintenance.Exceptions.Factories;
using NutaDev.CsLib.Resources.Text.Exceptions;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NutaDev.CsLib.Collections.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="IEnumerable{T}"/> interface.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Finds item in <paramref name="enumerable" /> that has lowest value provied by <paramref name="minSelector"/>.
        /// </summary>
        /// <typeparam name="TObject">Objec type.</typeparam>
        /// <typeparam name="TValue">Compared value type.</typeparam>
        /// <param name="enumerable">Enumerable to process.</param>
        /// <param name="minSelector">Value to compare selector.</param>
        /// <returns>Object with minimum value.</returns>
        public static TObject GetByMin<TObject, TValue>(this IEnumerable<TObject> enumerable, Func<TObject, TValue> minSelector)
            where TValue : IComparable
        {
            TObject currentItem = default(TObject);

            foreach (TObject item in enumerable)
            {
                if (Equals(currentItem, default(TObject)))
                {
                    currentItem = item;
                }
                else
                {
                    TValue currentMin = minSelector(currentItem);
                    TValue possibleMin = minSelector(item);

                    if (possibleMin.CompareTo(currentMin) < 0)
                    {
                        currentItem = item;
                    }
                }
            }

            return currentItem;
        }

        /// <summary>
        /// Returns distinct elements from a sequence by using a <see cref="AnonymousComparer{TObject}"/> with <paramref name="compare"/>.
        /// </summary>
        /// <typeparam name="TObject">Object type.</typeparam>
        /// <param name="enumerable">Enumerable to process.</param>
        /// <param name="compare">Comparing function.</param>
        /// <returns>Distinct collection.</returns>
        public static IEnumerable<TObject> Distinct<TObject>(this IEnumerable<TObject> enumerable, Func<TObject, TObject, bool> compare)
        {
            return enumerable.Distinct(new AnonymousComparer<TObject>(compare));
        }

        /// <summary>
        /// Returns distinct elements from a sequence by using a <see cref="AnonymousComparer{TObject}"/> with <paramref name="getHashCode"/>.
        /// </summary>
        /// <typeparam name="TObject">Object type.</typeparam>
        /// <param name="enumerable">Enumerable to process.</param>
        /// <param name="getHashCode">Hashing function.</param>
        /// <returns>Distinct collection.</returns>
        public static IEnumerable<TObject> Distinct<TObject>(this IEnumerable<TObject> enumerable, Func<TObject, int> getHashCode)
        {
            return enumerable.Distinct(new AnonymousComparer<TObject>(getHashCode));
        }

        /// <summary>
        /// Returns distinct elements from a sequence by using a <see cref="AnonymousComparer{TObject}"/> with <paramref name="compare"/> and <paramref name="getHashCode"/>.
        /// </summary>
        /// <typeparam name="TObject">Object type.</typeparam>
        /// <param name="enumerable">Enumerable to process.</param>
        /// <param name="compare">Comparing function.</param>
        /// <param name="getHashCode">Hashing function.</param>
        /// <returns>Distinct collection.</returns>
        public static IEnumerable<TObject> Distinct<TObject>(this IEnumerable<TObject> enumerable, Func<TObject, TObject, bool> compare, Func<TObject, int> getHashCode)
        {
            return enumerable.Distinct(new AnonymousComparer<TObject>(compare, getHashCode));
        }

        /// <summary>
        /// Performs <paramref name="action"/> on every element in the <paramref name="collection"/>.
        /// </summary>
        /// <typeparam name="TObject">Element type.</typeparam>
        /// <param name="collection">Collection of items on which <paramref name="action"/> will be performed.</param>
        /// <param name="action">Action which will be performed on every collection's element.</param>
        public static void ForEach<TObject>(this ObservableCollection<TObject> collection, Action<TObject> action)
        {
            foreach (TObject item in collection)
            {
                action(item);
            }
        }

        /// <summary>
        /// Performs <paramref name="action"/> on every element in the <paramref name="collection"/>.
        /// </summary>
        /// <typeparam name="TObject">Element type.</typeparam>
        /// <param name="collection">Collection of items on which <paramref name="action"/> will be performed.</param>
        /// <param name="action">Action which will be performed on every collection's element.</param>
        /// <returns>IEnumerable to iterate.</returns>
        public static IEnumerable<TObject> Execute<TObject>(this IEnumerable<TObject> collection, Action<TObject> action)
        {
            foreach (TObject item in collection)
            {
                action(item);
                yield return item;
            }
        }

        /// <summary>
        /// Performs <paramref name="action"/> on every element in the <paramref name="collection"/>.
        /// </summary>
        /// <typeparam name="TObject">Element type.</typeparam>
        /// <param name="collection">Collection of items on which <paramref name="action"/> will be performed.</param>
        /// <param name="action">Action which will be performed on every collection's element.</param>
        public static void ForEach<TObject>(this IEnumerable<TObject> collection, Action<TObject> action)
        {
            foreach (TObject item in collection)
            {
                action(item);
            }
        }

        /// <summary>
        /// Performs <paramref name="predicate"/> check over <paramref name="enumerable"/> if <paramref name="condition"/> returns true, otherwise returns <paramref name="enumerable"/>.
        /// </summary>
        /// <typeparam name="TObject">Element type.</typeparam>
        /// <param name="enumerable">Source collection.</param>
        /// <param name="condition">Condition check.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>Collection of items after <paramref name="predicate"/> check, if <paramref name="condition"/> is met, whole collection otherwise.</returns>
        public static IEnumerable<TObject> WhereIf<TObject>(this IEnumerable<TObject> enumerable, Func<bool> condition, Func<TObject, bool> predicate)
        {
            bool predicateCheck = condition();

            foreach (TObject item in enumerable)
            {
                if (predicateCheck)
                {
                    if (predicate(item))
                    {
                        yield return item;
                    }
                }
                else
                {
                    yield return item;
                }
            }
        }

        /// <summary>
        /// Converts <paramref name="collection"/> to <see cref="ObservableCollection{T}"/>.
        /// </summary>
        /// <typeparam name="TObject">Element type.</typeparam>
        /// <param name="collection">Source collection.</param>
        /// <returns><see cref="ObservableCollection{T}"/> that contains elements from <paramref name="collection"/>.</returns>
        public static ObservableCollection<TObject> ToObservableCollection<TObject>(this IEnumerable<TObject> collection)
        {
            ObservableCollection<TObject> result = new ObservableCollection<TObject>();

            foreach (TObject item in collection)
            {
                result.Add(item);
            }

            return result;
        }

        /// <summary>
        /// Returns distinct collection of items using <paramref name="predicate"/> and <see cref="HashSet{T}"/>.
        /// </summary>
        /// <typeparam name="TObject">Item's type.</typeparam>
        /// <typeparam name="TK">Type of compared value.</typeparam>
        /// <param name="collection">Source collection.</param>
        /// <param name="predicate">Predicate function.</param>
        /// <returns>Distinct collection.</returns>
        public static IEnumerable<TObject> Distinct<TObject, TK>(this IEnumerable<TObject> collection, Func<TObject, TK> predicate)
        {
            HashSet<TK> existingItems = new HashSet<TK>();

            foreach (TObject item in collection)
            {
                if (!existingItems.Add(predicate(item)))
                {
                    continue;
                }

                yield return item;
            }
        }

        /// <summary>
        /// Select value returned by <paramref name="selector"/> if <paramref name="predicate"/> is met.
        /// </summary>
        /// <typeparam name="TObject">Data type.</typeparam>
        /// <typeparam name="TResult">Result type.</typeparam>
        /// <param name="collection">Source collection.</param>
        /// <param name="predicate">Predicate to meet.</param>
        /// <param name="selector">Selects value.</param>
        /// <returns>Collection of selected values that met <paramref name="predicate"/>.</returns>
        public static IEnumerable<TResult> SelectWhere<TObject, TResult>(this IEnumerable<TObject> collection, Func<TObject, bool> predicate, Func<TObject, TResult> selector)
        {
            foreach (TObject item in collection)
            {
                if (predicate(item))
                {
                    yield return selector(item);
                }
            }
        }

        /// <summary>
        /// Performs <paramref name="action"/> on every <paramref name="collection"/>'s item.
        /// </summary>
        /// <typeparam name="TObject">Data type.</typeparam>
        /// <param name="collection">Source collection.</param>
        /// <param name="action">Action to perform.</param>
        /// <returns>Modified element.</returns>
        public static IEnumerable<TObject> Perform<TObject>(this IEnumerable<TObject> collection, Action<TObject> action)
        {
            foreach (TObject item in collection)
            {
                action(item);
                yield return item;
            }
        }

        /// <summary>
        /// Converts <paramref name="collection"/> to <see cref="HashSet{T}"/>.
        /// </summary>
        /// <typeparam name="TObject">Data type.</typeparam>
        /// <param name="collection">Source collection.</param>
        /// <param name="throwOnDuplicate">Whether should throw if duplicate element hasn't been added.</param>
        /// <returns><see cref="HashSet{T}"/> of items.</returns>
        public static HashSet<TObject> ToHashSet<TObject>(this IEnumerable<TObject> collection, bool throwOnDuplicate = false)
        {
            HashSet<TObject> result = new HashSet<TObject>();

            foreach (TObject item in collection)
            {
                if (!result.Add(item) && throwOnDuplicate)
                {
                    throw ExceptionFactory.Create<InvalidOperationException>(Text.DuplicateValue_0_, item);
                }
            }

            return result;
        }

        /// <summary>
        /// Converts <paramref name="enumerable"/> to <see cref="Queue{T}"/>.
        /// </summary>
        /// <typeparam name="TObject">Data type.</typeparam>
        /// <param name="enumerable">Source enumerable.</param>
        /// <returns><see cref="Queue{T}"/> of items.</returns>
        public static Queue<TObject> ToQueue<TObject>(this IEnumerable<TObject> enumerable)
        {
            Queue<TObject> queue = new Queue<TObject>();

            foreach (TObject item in enumerable)
            {
                queue.Enqueue(item);
            }

            return queue;
        }

        /// <summary>
        /// Converts <paramref name="enumerable"/> to <see cref="Stack{T}"/>.
        /// </summary>
        /// <typeparam name="TObject">Data type.</typeparam>
        /// <param name="enumerable">Source enumerable.</param>
        /// <returns><see cref="Stack{T}"/> of items.</returns>
        public static Stack<TObject> ToStack<TObject>(this IEnumerable<TObject> enumerable)
        {
            Stack<TObject> stack = new Stack<TObject>();

            foreach (TObject item in enumerable)
            {
                stack.Push(item);
            }

            return stack;
        }
    }
}
