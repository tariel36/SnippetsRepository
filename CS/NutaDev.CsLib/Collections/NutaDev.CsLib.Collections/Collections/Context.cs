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

using NutaDev.CsLib.Maintenance.Exceptions.Factories;
using NutaDev.CsLib.Resources.Text.Exceptions;
using System;
using System.Collections.Generic;

namespace NutaDev.CsLib.Collections.Collections
{
    /// <summary>
    /// Universal dictionary like container.
    /// </summary>
    /// <typeparam name="TKey">Type of key.</typeparam>
    public class Context<TKey>
    {
        /// <summary>
        /// Inner dictionary.
        /// </summary>
        private readonly Dictionary<TKey, object> _items;

        /// <summary>
        /// Initializes a new instance of the <see cref="Context{TKey}"/> class.
        /// </summary>
        public Context()
        {
            _items = new Dictionary<TKey, object>();
        }

        /// <summary>
        /// Adds new element.
        /// </summary>
        /// <param name="key">Item's key.</param>
        /// <param name="value">Value to add.</param>
        /// <param name="replace">Indicates whether value should be replaced if key already exists.</param>
        /// <exception cref="InvalidOperationException">If key already exists and <paramref name="replace"/> is false.</exception>
        public void Add(TKey key, object value, bool replace = false)
        {
            if (_items.ContainsKey(key) && !replace)
            {
                throw ExceptionFactory.Create<InvalidOperationException>(Text.Key_0_AlreadyExistsAndReplacingIsNotAllowed, key);
            }

            _items[key] = value;
        }

        /// <summary>
        /// Removes element associated with <paramref name="key"/>.
        /// </summary>
        /// <param name="key">Element's key.</param>
        /// <returns>Removed value or null.</returns>
        public object Remove(TKey key)
        {
            object tmp = null;

            if (_items.ContainsKey(key))
            {
                tmp = _items[key];
            }

            _items.Remove(key);

            return tmp;
        }

        /// <summary>
        /// Removes element associated with <paramref name="key"/>.
        /// </summary>
        /// <param name="key">Element's key.</param>
        public void Delete(TKey key)
        {
            _items.Remove(key);
        }

        /// <summary>
        /// Returns value associated with <paramref name="key"/>.
        /// </summary>
        /// <param name="key">Element's key.</param>
        /// <returns>Value.</returns>
        /// <exception cref="InvalidOperationException">If key does not exist.</exception>
        public object Get(TKey key)
        {
            return InnerGet(key, true);
        }

        /// <summary>
        /// Returns value associated with <paramref name="key"/> or default value if key does not exist.
        /// </summary>
        /// <param name="key">Element's key.</param>
        /// <param name="defaultValue">Default value.</param>
        /// <returns>Value associated with <paramref name="key"/> or default value.</returns>
        public T TryGet<T>(TKey key, T defaultValue = default(T))
        {
            T result = defaultValue;

            object tmp = InnerGet(key, false);
            if (tmp is T)
            {
                result = (T)tmp;
            }

            return result;
        }

        /// <summary>
        /// Indicates whether <paramref name="key"/> exists in <see cref="Context{TKey}"/>.
        /// </summary>
        /// <param name="key">Key to check.</param>
        /// <returns>True if <see cref="Context{TKey}"/> contains <paramref name="key"/>.</returns>
        public bool ContainsKey(TKey key)
        {
            return _items.ContainsKey(key);
        }

        /// <summary>
        /// Removes all elements from <see cref="Context{TKey}"/>.
        /// </summary>
        public void Clear()
        {
            _items.Clear();
        }

        /// <summary>
        /// Returns an element from inner collection or default value if <paramref name="throwException"/> is set to false, throws an exception otherwise.
        /// </summary>
        /// <param name="key">Key to check.</param>
        /// <param name="throwException">Indicates whether an exception should be thrown.</param>
        /// <returns>Value associated with <paramref name="key"/> or default value.</returns>
        /// <exception cref="InvalidOperationException">If <paramref name="throwException"/> is set to true and key does not exist.</exception>
        private object InnerGet(TKey key, bool throwException)
        {
            if (ContainsKey(key))
            {
                return _items[key];
            }

            if (throwException)
            {
                throw ExceptionFactory.Create<InvalidOperationException>(Text.Key_0_DoesNotExist, key);
            }

            return default(object);
        }
    }
}
