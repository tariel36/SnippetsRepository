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

using NutaDev.CsLib.Data.Abstract.DataSources;
using System;
using System.Collections.Generic;

namespace NutaDev.CsLib.Data.DataSources
{
    /// <summary>
    /// <see cref="DataSource"/> that does nothing.
    /// </summary>
    public class NullDataSource
        : DataSource
    {
        /// <summary>
        /// Does nothing.
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <typeparam name="TK">Key type.</typeparam>
        /// <param name="item">Item to delete.</param>
        /// <returns>Always 0.</returns>
        public override int Delete<T, TK>(T item)
        {
            return 0;
        }

        /// <summary>
        /// Does nothing
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <typeparam name="TK">Key type.</typeparam>
        /// <returns>Always 0.</returns>
        public override int DeleteAll<T, TK>()
        {
            return 0;
        }

        /// <summary>
        /// Does nothing.
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <typeparam name="TK">Key type.</typeparam>
        /// <param name="id">Object id.</param>
        /// <returns>Default value of <typeparamref name="T"/>.</returns>
        public override T Get<T, TK>(TK id)
        {
            return default(T);
        }

        /// <summary>
        /// Does nothing.
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <typeparam name="TK">Key type.</typeparam>
        /// <param name="predicate">Predicate to fulfill.</param>
        /// <returns>Empty collection.</returns>
        public override ICollection<T> Get<T, TK>(Func<T, bool> predicate)
        {
            return new List<T>();
        }

        /// <summary>
        /// Does nothing.
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <typeparam name="TK">Key type.</typeparam>
        /// <param name="toSet">Object to set.</param>
        /// <returns>Always false.</returns>
        public override bool Set<T, TK>(T toSet)
        {
            return false;
        }
    }
}
