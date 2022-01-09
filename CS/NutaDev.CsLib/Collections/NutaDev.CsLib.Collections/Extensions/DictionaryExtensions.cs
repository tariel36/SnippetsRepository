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

namespace NutaDev.CsLib.Collections.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="IDictionary{TKey,TValue}"/> interface.
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Performs <paramref name="predicate"/> on each element of the collection.
        /// </summary>
        /// <typeparam name="TKey">Key type.</typeparam>
        /// <typeparam name="TValue">Value type.</typeparam>
        /// <param name="dict">Target dictionary.</param>
        /// <param name="predicate">Predicate to perform.</param>
        public static void ForEach<TKey, TValue>(this IDictionary<TKey, TValue> dict, Action<TValue> predicate)
        {
            foreach (TValue item in dict.Values)
            {
                predicate(item);
            }
        }

        /// <summary>
        /// Returns value assiociated with the key or default <typeparamref name="TValue"/> value.
        /// </summary>
        /// <typeparam name="TKey">Key type.</typeparam>
        /// <typeparam name="TValue">Value type.</typeparam>
        /// <param name="dict">Target dictionary.</param>
        /// <param name="key">Key value.</param>
        /// <returns>Value or default.</returns>
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key)
        {
            TValue val;
            dict.TryGetValue(key, out val);
            return val;
        }

        /// <summary>
        /// Returns value assiociated with the key or provided default value if dictionary does not contains the key.
        /// </summary>
        /// <typeparam name="TKey">Key type.</typeparam>
        /// <typeparam name="TValue">Value type.</typeparam>
        /// <param name="dict">Target dictionary.</param>
        /// <param name="key">Key value.</param>
        /// <param name="defaultValue">Default value to return.</param>
        /// <returns>Value from dictionary or provided <paramref name="defaultValue"/>.</returns>
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue defaultValue)
        {
            TValue val = defaultValue;

            if (dict.ContainsKey(key))
            {
                val = dict[key];
            }

            return val;
        }

        /// <summary>
        /// Ensures that key exists. Adds the key to dictionary with provided value if not exists. Nothing otherwise.
        /// </summary>
        /// <typeparam name="TKey">Key type.</typeparam>
        /// <typeparam name="TValue">Value type.</typeparam>
        /// <param name="dict">Target dictionary.</param>
        /// <param name="key">Key to check.</param>
        /// <param name="value">Value to add.</param>
        /// <returns>Reference to itself.</returns>
        public static IDictionary<TKey, TValue> EnsureKeyExists<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            if (!dict.ContainsKey(key))
            {
                dict.Add(key, value);
            }

            return dict;
        }

        /// <summary>
        /// Ensures that key exists. Adds the key to dictionary with default value if not exists. Nothing otherwise.
        /// </summary>
        /// <typeparam name="TKey">Key type.</typeparam>
        /// <typeparam name="TValue">Value type.</typeparam>
        /// <param name="dict">Target dictionary.</param>
        /// <param name="key">Key to check.</param>
        /// <returns>Reference to itself.</returns>
        public static IDictionary<TKey, TValue> EnsureKeyExists<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key)
        {
            return EnsureKeyExists(dict, key, default(TValue));
        }

        /// <summary>
        /// Removes all entries that fulfills the predicate.
        /// </summary>
        /// <typeparam name="TKey">Key type.</typeparam>
        /// <typeparam name="TValue">Value type.</typeparam>
        /// <param name="dict">Target dictionary.</param>
        /// <param name="predicate">Predicate to use on every entry.</param>
        /// <returns>Reference to itself.</returns>
        public static IDictionary<TKey, TValue> RemoveAll<TKey, TValue>(this IDictionary<TKey, TValue> dict, Func<KeyValuePair<TKey, TValue>, bool> predicate)
        {
            List<TKey> keys = dict.Keys.ToList();

            for (int i = 0; i < keys.Count; ++i)
            {
                if (dict.ContainsKey(keys[i]) && predicate(dict.First(x => x.Key.Equals(keys[i]))))
                {
                    dict.Remove(keys[i--]);
                }
            }

            return dict;
        }
    }
}
