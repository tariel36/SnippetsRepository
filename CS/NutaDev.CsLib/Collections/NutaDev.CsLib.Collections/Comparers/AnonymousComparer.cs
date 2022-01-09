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

namespace NutaDev.CsLib.Collections.Comparers
{
    /// <summary>
    /// The anonymous comparer. Uses provided equality and hash functions or <see cref="object.Equals(object)"/> and <see cref="object.GetHashCode"/> functions.
    /// </summary>
    /// <typeparam name="TObject"></typeparam>
    public class AnonymousComparer<TObject>
        : IEqualityComparer<TObject>
    {
        /// <summary>
        /// Equals delegate field.
        /// </summary>
        private readonly Func<TObject, TObject, bool> _equals;

        /// <summary>
        /// GetHashCode delegate field.
        /// </summary>
        private readonly Func<TObject, int> _getHashCode;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnonymousComparer{T}"/> class.
        /// </summary>
        public AnonymousComparer()
            : this(null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnonymousComparer{T}"/> class.
        /// </summary>
        /// <param name="equalsFunc">Delegate that will be used as Equals function.</param>
        public AnonymousComparer(Func<TObject, TObject, bool> equalsFunc)
            : this(equalsFunc, null)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnonymousComparer{T}"/> class.
        /// </summary>
        /// <param name="getHashCodeFunc">Delegate that will be used as GetHashCode function.</param>
        public AnonymousComparer(Func<TObject, int> getHashCodeFunc = null)
            : this(null, getHashCodeFunc)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnonymousComparer{T}"/> class.
        /// </summary>
        /// <param name="equalsFunc">Delegate that will be used as Equals function.</param>
        /// <param name="getHashCodeFunc">Delegate that will be used as GetHashCode function.</param>
        public AnonymousComparer(Func<TObject, TObject, bool> equalsFunc, Func<TObject, int> getHashCodeFunc = null)
        {
            _equals = equalsFunc;
            _getHashCode = getHashCodeFunc;
        }

        /// <summary>
        /// Determines whether the specified object instances are considered equal.
        /// </summary>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        /// <returns><see langword="true" /> if the objects are considered equal; otherwise, <see langword="false" />. If both <paramref name="x" /> and <paramref name="y" /> are null, the method returns <see langword="true" /></returns>
        public bool Equals(TObject x, TObject y)
        {
            return _equals == null
                ? object.Equals(x, y)
                : _equals(x, y)
                ;
        }

        /// <summary>
        /// Serves as the hash function.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>A hash code for the <paramref name="obj"/> object.</returns>
        public int GetHashCode(TObject obj)
        {
            return _getHashCode == null
                ? obj.GetHashCode()
                : _getHashCode(obj)
                ;
        }
    }
}
