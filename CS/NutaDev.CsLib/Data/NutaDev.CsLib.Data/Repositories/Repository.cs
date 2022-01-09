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
using NutaDev.CsLib.Data.Abstract.Models;
using NutaDev.CsLib.Data.Caches;
using NutaDev.CsLib.Maintenance.Exceptions.Delegates;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NutaDev.CsLib.Data.Repositories
{
    /// <summary>
    /// Simple repository class.
    /// </summary>
    public class Repository
        : IDisposable
    {
        /// <summary>
        /// Synchronization object.
        /// </summary>
        private readonly object _syncLock = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository"/> class.
        /// </summary>
        /// <param name="cache">Cache instance.</param>
        /// <param name="dataSource">Data source instance.</param>
        public Repository(Cache cache, DataSource dataSource, ExceptionHandlerDelegate exceptionHandler = null)
        {
            Cache = cache;
            Source = dataSource;
            ExceptionHandler = exceptionHandler;

            InvocatorsDict = new Dictionary<Type, ISourceInvocator>();
        }

        /// <summary>
        /// Cache instance.
        /// </summary>
        public Cache Cache { get; }

        /// <summary>
        /// Data source.
        /// </summary>
        public DataSource Source { get; }

        /// <summary>
        /// Collection of invocators.
        /// </summary>
        private Dictionary<Type, ISourceInvocator> InvocatorsDict { get; }

        /// <summary>
        /// Gets the exception handler.
        /// </summary>
        private ExceptionHandlerDelegate ExceptionHandler { get; }

        #region IDisposable

        /// <summary>
        /// Dispose <see cref="Repository"/>.
        /// </summary>
        public void Dispose()
        {
            TryDispose(Source);
        }

        /// <summary>
        /// Tries to dispose object.
        /// </summary>
        /// <param name="disposable">Object to dispose.</param>
        private void TryDispose(IDisposable disposable)
        {
            if (disposable != null)
            {
                try
                {
                    disposable.Dispose();
                }
                catch (Exception ex)
                {
                    ExceptionHandler.Invoke(ex);
                }
            }
        }

        #endregion

        #region Invocators

        /// <summary>
        /// Adds invocator.
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <param name="invocator">Invocator to add.</param>
        public void AddInvocator<T>(ISourceInvocator<T> invocator)
        {
            lock (_syncLock)
            {
                InvocatorsDict[typeof(T)] = invocator;
            }
        }

        /// <summary>
        /// Adds invcator.
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <typeparam name="TK">Key type.</typeparam>
        public void AddInvocator<T, TK>()
            where T : IObjectId<TK>
        {
            AddInvocator(new SourceInvocator<T, TK>(Source));
        }

        /// <summary>
        /// Removes invocator.
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        public void RemoveInvocator<T>()
        {
            lock (_syncLock)
            {
                InvocatorsDict.Remove(typeof(T));
            }
        }

        #endregion

        #region SetItem | TryGetItem | DeleteItem

        /// <summary>
        /// Deletes all items.
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <returns>Count of deleted items.</returns>
        public int DeleteAll<T>()
        {
            lock (_syncLock)
            {
                ISourceInvocator<T> invocator;

                if (TryGetInvocator(out invocator))
                {
                    Cache.Remove(Cache.CachePredicate.Create<T>(x => true));
                    return invocator.DeleteAll();
                }

                return 0;
            }
        }

        /// <summary>
        /// Deletes item.
        /// </summary>
        /// <typeparam name="T">Item type.</typeparam>
        /// <typeparam name="TK">Key type.</typeparam>
        /// <param name="item">Item to delete.</param>
        /// <returns>Count of deleted items.</returns>
        public int DeleteItem<T, TK>(T item)
            where T : IObjectId<TK>
        {
            lock (_syncLock)
            {
                ISourceInvocator<T> invocator;

                if (TryGetInvocator(out invocator))
                {
                    Cache.Remove(Cache.CacheKey<TK>.Create<T>(item.Id));
                    return invocator.Delete(item);
                }

                return 0;
            }
        }

        /// <summary>
        /// Sets item.
        /// </summary>
        /// <typeparam name="T">Item type.</typeparam>
        /// <typeparam name="TK">Key type.</typeparam>
        /// <param name="item">Item to set.</param>
        /// <returns>True if item has been set, false otherwise.</returns>
        public bool SetItem<T, TK>(T item)
            where T : IObjectId<TK>
        {
            lock (_syncLock)
            {
                return InnerSetItem<T, TK>(item);
            }
        }

        /// <summary>
        /// Try to get items specified by <paramref name="ids"/>.
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <typeparam name="TK">Key type.</typeparam>
        /// <param name="ids">Predicate to check.</param>
        /// <returns>List of items that with specified <paramref name="ids"/>.</returns>
        public List<T> TryGetItem<T, TK>(TK[] ids)
            where T : IObjectId<TK>
        {
            lock (_syncLock)
            {
                List<T> result = new List<T>();

                ISourceInvocator<T> invocator;

                if (TryGetInvocator(out invocator))
                {
                    if (ids != null && ids.Length > 0)
                    {
                        ids = ids.Distinct().ToArray();

                        result = Cache.Get(ids.Select(x => x).Select(Cache.CacheKey<TK>.Create<T>).ToArray())
                            .Where(x => x is T)
                            .Cast<T>()
                            .ToList();

                        if (result.Count != ids.Length)
                        {
                            TK[] toGet = ids.Except(result.Select(x => x.Id)).ToArray();

                            if (toGet.Length > 0)
                            {
                                ICollection<T> items = invocator.Get(o => toGet.Contains(o.Id));
                                foreach (T item in items)
                                {
                                    Cache.Set(Cache.CacheKey<TK>.Create<T>(item.Id), item);
                                    result.Add(item);
                                }
                            }
                        }
                    }
                }

                return result;
            }
        }

        /// <summary>
        /// Try to get items specified by <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <typeparam name="TK">Key type.</typeparam>
        /// <param name="predicate">Predicate to check.</param>
        /// <returns>List of items that fulfilled <paramref name="predicate"/>.</returns>
        public List<T> TryGetItems<T, TK>(Func<T, bool> predicate)
            where T : IObjectId<TK>
        {
            lock (_syncLock)
            {
                List<T> result = new List<T>();

                if (predicate != null)
                {
                    ISourceInvocator<T> invocator;

                    if (TryGetInvocator(out invocator))
                    {
                        ICollection<T> items = invocator.Get(predicate);

                        if (items != null && items.Count > 0)
                        {
                            foreach (T item in items)
                            {
                                SetCacheItem<T, TK>(item);
                                result.Add(item);
                            }
                        }
                    }
                }

                return result;
            }
        }

        #endregion

        #region Inner GET | SET | DELETE

        /// <summary>
        /// Sets item.
        /// </summary>
        /// <typeparam name="T">Item type.</typeparam>
        /// <typeparam name="TK">Key type.</typeparam>
        /// <param name="item">Item to set.</param>
        /// <returns>True if item has been set, false otherwise.</returns>
        private bool InnerSetItem<T, TK>(T item)
            where T : IObjectId<TK>
        {
            bool result = false;

            ISourceInvocator<T> invocator;

            if (TryGetInvocator(out invocator))
            {
                result = invocator.Set(item);

                if (result)
                {
                    SetCacheItem<T, TK>(item);
                }
            }

            return result;
        }

        private void SetCacheItem<T, TK>(T item)
            where T : IObjectId<TK>
        {
            Cache.Set(Cache.CacheKey<TK>.Create<T>(item.Id), item);
        }

        #endregion

        #region Invocator GET | SET

        /// <summary>
        /// Try to get invocator.
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <param name="invocator">Invocator's value.</param>
        /// <returns>True if <paramref name="invocator"/> has been filled properly, false otherwise.</returns>
        private bool TryGetInvocator<T>(out ISourceInvocator<T> invocator)
        {
            Type type = typeof(T);

            invocator = default(ISourceInvocator<T>);

            if (InvocatorsDict.ContainsKey(type))
            {
                invocator = InvocatorsDict[type] as ISourceInvocator<T>;
            }

            return invocator != default(ISourceInvocator<T>);
        }

        #endregion

        #region SourceInvocator

        /// <summary>
        /// Base interface for source invocators.
        /// </summary>
        public interface ISourceInvocator
        {

        }

        /// <summary>
        /// Generic interface for source invocators.
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        public interface ISourceInvocator<T>
            : ISourceInvocator
        {
            /// <summary>
            /// Get object collection delegate.
            /// </summary>
            Func<Func<T, bool>, ICollection<T>> Get { get; set; }

            /// <summary>
            /// Set object delegate.
            /// </summary>
            Func<T, bool> Set { get; set; }

            /// <summary>
            /// Delete object delegate.
            /// </summary>
            Func<T, int> Delete { get; set; }

            /// <summary>
            /// DeleteAll objects delegate.
            /// </summary>
            Func<int> DeleteAll { get; set; }
        }

        /// <summary>
        /// Source invocator implementation.
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <typeparam name="TK">Key type.</typeparam>
        public class SourceInvocator<T, TK>
            : ISourceInvocator<T>
            where T : IObjectId<TK>
        {
            /// <summary>
            /// Initializes new instance of the <see cref="SourceInvocator{T,TK}"/> class.
            /// </summary>
            public SourceInvocator()
            {

            }

            /// <summary>
            /// Initializes new instance of the <see cref="SourceInvocator{T,TK}"/> class.
            /// </summary>
            /// <param name="ds">Data source.</param>
            public SourceInvocator(DataSource ds)
                : this(ds.Get<T, TK>, ds.Set<T, TK>, ds.Delete<T, TK>, ds.DeleteAll<T, TK>)
            {
            }

            /// <summary>
            /// Initializes new instance of the <see cref="SourceInvocator{T,TK}"/> class.
            /// </summary>
            /// <param name="get">Get delegate.</param>
            /// <param name="set">Set delegate.</param>
            /// <param name="delete">Delete delegate.</param>
            /// <param name="deleteAll">Delete all delegate.</param>
            public SourceInvocator(Func<Func<T, bool>, ICollection<T>> get, Func<T, bool> set, Func<T, int> delete, Func<int> deleteAll)
            {
                Get = get;
                Set = set;
                Delete = delete;
                DeleteAll = deleteAll;
            }

            /// <summary>
            /// Get object collection delegate.
            /// </summary>
            public Func<Func<T, bool>, ICollection<T>> Get { get; set; }

            /// <summary>
            /// Set object delegate.
            /// </summary>
            public Func<T, bool> Set { get; set; }

            /// <summary>
            /// Delete object delegate.
            /// </summary>
            public Func<T, int> Delete { get; set; }

            /// <summary>
            /// DeleteAll objects delegate.
            /// </summary>
            public Func<int> DeleteAll { get; set; }
        }

        #endregion
    }
}
