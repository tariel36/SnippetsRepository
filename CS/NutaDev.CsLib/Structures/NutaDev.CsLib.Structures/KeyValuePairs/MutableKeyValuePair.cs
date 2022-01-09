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

namespace NutaDev.CsLib.Structures.KeyValuePairs
{
    /// <summary>
    /// Mutable version of <see cref="KeyValuePair{TKey,TValue}"/> class.
    /// </summary>
    /// <typeparam name="TK">Key type.</typeparam>
    /// <typeparam name="TV">Value type.</typeparam>
    public class MutableKeyValuePair<TK, TV>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MutableKeyValuePair{TK,TV}"/> class.
        /// </summary>
        public MutableKeyValuePair()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MutableKeyValuePair{TK,TV}"/> class.
        /// </summary>
        /// <param name="key">Key's value.</param>
        /// <param name="value">Value's value.</param>
        public MutableKeyValuePair(TK key, TV value)
        {
            Key = key;
            Value = value;
        }

        /// <summary>
        /// The key.
        /// </summary>
        public TK Key { get; set; }

        /// <summary>
        /// The value.
        /// </summary>
        public TV Value { get; set; }

        /// <summary>
        /// Converts this object to <see cref="KeyValuePair{TKey,TValue}"/>.
        /// </summary>
        /// <returns>Object converted to <see cref="KeyValuePair{TKey,TValue}"/>.</returns>
        public KeyValuePair<TK, TV> ToKeyValuePair()
        {
            return new KeyValuePair<TK, TV>(Key, Value);
        }
    }
}
