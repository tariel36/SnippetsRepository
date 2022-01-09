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

namespace NutaDev.CsLib.Types.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="object"/>.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Performs <paramref name="action"/> on object and returns object.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="obj">Object instance.</param>
        /// <param name="action">Action to perform.</param>
        /// <returns>The object.</returns>
        public static T Modify<T>(this T obj, Action<T> action)
        {
            action(obj);
            return obj;
        }

        /// <summary>
        /// Performs <paramref name="action"/> on the <paramref name="obj"/> and returns the IEnumerable&gt;T&lt; containing this object.
        /// </summary>
        /// <typeparam name="T">The type of object.</typeparam>
        /// <param name="obj">Object instance.</param>
        /// <param name="action">Action to perform.</param>
        /// <returns>IEnumerable&gt;T&lt; instance that contains <paramref name="obj"/>.</returns>
        public static IEnumerable<T> ToEnumerable<T>(this T obj, Action<T> action)
        {
            action(obj);
            return new[] { obj };
        }

        /// <summary>
        /// Returns IEnumerable&gt;T&lt; containing provided object.
        /// </summary>
        /// <typeparam name="T">The type of object.</typeparam>
        /// <param name="obj">Object instance.</param>
        /// <returns>IEnumerable&gt;T&lt; instance that contains <paramref name="obj"/>.</returns>
        public static IEnumerable<T> ToEnumerable<T>(this T obj)
        {
            return new[] { obj };
        }
    }
}
