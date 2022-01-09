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

using NutaDev.CsLib.Data.Abstract.Models;
using System;
using System.Collections.Generic;

namespace NutaDev.CsLib.Data.Abstract.DataSources
{
    /// <summary>
    /// Abstract data source.
    /// </summary>
    public abstract class DataSource
        : IDisposable
    {
        /// <summary>
        /// Deletes specific item.
        /// </summary>
        /// <typeparam name="T">Item type.</typeparam>
        /// <typeparam name="TK">Key type.</typeparam>
        /// <param name="item">Item to delete.</param>
        /// <returns>Deleted items count.</returns>
        public abstract int Delete<T, TK>(T item)
            where T : IObjectId<TK>;

        /// <summary>
        /// Deletes all tiems.
        /// </summary>
        /// <typeparam name="T">Item type.</typeparam>
        /// <typeparam name="TK">Key type.</typeparam>
        /// <returns>Deleted items count.</returns>
        public abstract int DeleteAll<T, TK>()
            where T : IObjectId<TK>;

        /// <summary>
        /// Gets item by specific <paramref name="id"/>.
        /// </summary>
        /// <typeparam name="T">Item type.</typeparam>
        /// <typeparam name="TK">Key type.</typeparam>
        /// <param name="id">Item's id.</param>
        /// <returns>Item specified by <paramref name="id"/>.</returns>
        public abstract T Get<T, TK>(TK id)
            where T : IObjectId<TK>;

        /// <summary>
        /// Gets all items that fullfil <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="T">Item type.</typeparam>
        /// <typeparam name="TK">Key type.</typeparam>
        /// <param name="predicate">Predicate to check items.</param>
        /// <returns></returns>
        public abstract ICollection<T> Get<T, TK>(Func<T, bool> predicate)
            where T : IObjectId<TK>;

        /// <summary>
        /// Adds or updates an item.
        /// </summary>
        /// <typeparam name="T">Item type.</typeparam>
        /// <typeparam name="TK">Key type.</typeparam>
        /// <param name="toSet">Item to add or update.</param>
        /// <returns>True if item has been set, otherwise false.</returns>
        public abstract bool Set<T, TK>(T toSet)
            where T : IObjectId<TK>;

        /// <summary>
        /// Disposes object.
        /// </summary>
        public virtual void Dispose()
        {

        }
    }
}
