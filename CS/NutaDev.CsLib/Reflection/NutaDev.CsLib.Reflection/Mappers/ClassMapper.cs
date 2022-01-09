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

namespace NutaDev.CsLib.Reflection.Mappers
{
    /// <summary>
    /// Contains mapping for class operations.
    /// </summary>
    public class ClassMapper
    {
        /// <summary>
        /// Initializes instance of the <see cref="ClassMapper"/> class.
        /// </summary>
        public ClassMapper()
        {
            Properties = new Dictionary<string, Tuple<Func<object>, Action<object>>>();
        }

        /// <summary>
        /// Gets <see cref="Dictionary{TKey,TValue}"/> of get/set mappings.
        /// </summary>
        private Dictionary<string, Tuple<Func<object>, Action<object>>> Properties { get; }

        /// <summary>
        /// Adds get/set mapping accessed by <paramref name="name"/>.
        /// </summary>
        /// <typeparam name="T">Type of value.</typeparam>
        /// <param name="name">Accessor name.</param>
        /// <param name="get">Get delegate.</param>
        /// <param name="set">Set delegate.</param>
        /// <returns>Reference to itself.</returns>
        public ClassMapper Add<T>(string name, Func<T> get, Action<T> set)
        {
            Properties[name] = Tuple.Create(
                get == null ? (Func<object>)null : () => get(),
                set == null ? (Action<object>)null : x => set((T)x));

            return this;
        }

        /// <summary>
        /// Returns value accessed by <paramref name="name"/> or default value.
        /// </summary>
        /// <typeparam name="T">Type of value.</typeparam>
        /// <param name="name">Accessor name.</param>
        /// <returns>Value or default value.</returns>
        public T Get<T>(string name)
        {
            return Properties.ContainsKey(name) && Properties[name].Item1 != null
                ? (T)Properties[name].Item1()
                : default(T);
        }

        /// <summary>
        /// Sets <paramref name="value"/> accessed by <paramref name="name"/>.
        /// </summary>
        /// <typeparam name="T">Type of value.</typeparam>
        /// <param name="name">Accessor name.</param>
        /// <param name="value">Value to set.</param>
        /// <returns>Reference to itself.</returns>
        public ClassMapper Set<T>(string name, T value)
        {
            if (Properties.ContainsKey(name) && Properties[name].Item2 != null)
            {
                Properties[name].Item2(value);
            }

            return this;
        }
    }
}
