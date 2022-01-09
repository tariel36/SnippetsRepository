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

namespace NutaDev.CsLib.Data.Caches
{
    /// <summary>
    /// Simple cache implementation.
    /// </summary>
    public class Cache
    {
        private readonly object _syncObject = new object();
        private readonly Dictionary<Type, Dictionary<CacheKey, object>> _cachedItems;

        /// <summary>
        /// Initializes new instance of the <see cref="Cache"/> class.
        /// </summary>
        public Cache()
        {
            _cachedItems = new Dictionary<Type, Dictionary<CacheKey, object>>();
        }

        #region Manage

        /// <summary>
        /// Sets the <paramref name="item"/>.
        /// </summary>
        /// <param name="key">Item's key.</param>
        /// <param name="item">Item to set.</param>
        public void Set(CacheKey key, object item)
        {
            lock (_syncObject)
            {
                SetInternal(key, item);
            }
        }

        /// <summary>
        /// Gets the items specified by <paramref name="keys"/>.
        /// </summary>
        /// <param name="keys">Items keys.</param>
        /// <returns>List of items.</returns>
        public List<object> Get(params CacheKey[] keys)
        {
            List<object> result = new List<object>();

            lock (_syncObject)
            {
                result.AddRange(GetInternal(keys));
            }

            return result;
        }

        /// <summary>
        /// Gets the item specified by <paramref name="key"/>.
        /// </summary>
        /// <param name="key">Items keys.</param>
        /// <returns>Item if exists, otherwise null.</returns>
        public object Get(CacheKey key)
        {
            object result;

            lock (_syncObject)
            {
                result = GetInternal(key);
            }

            return result;
        }

        /// <summary>
        /// Gets the items specified by <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate">Items predicate.</param>
        /// <returns>List of items.</returns>
        public List<object> Get(CachePredicate predicate)
        {
            List<object> result;

            lock (_syncObject)
            {
                result = GetInternal(predicate);
            }

            return result;
        }

        /// <summary>
        /// Removes the item specified by <paramref name="key"/>.
        /// </summary>
        /// <param name="key">Items keys.</param>
        /// <returns>Item if exists, otherwise null.</returns>
        public object Remove(CacheKey key)
        {
            object result;

            lock (_syncObject)
            {
                result = RemoveInternal(key);
            }

            return result;
        }

        /// <summary>
        /// Removes the items specified by <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate">Items predicate.</param>
        /// <returns>List of items.</returns>
        public List<object> Remove(CachePredicate predicate)
        {
            List<object> result;

            lock (_syncObject)
            {
                result = RemoveInternal(predicate);
            }

            return result;
        }

        /// <summary>
        /// Indicates whether cache contains specified <paramref name="key"/>.
        /// </summary>
        /// <param name="key">Key to check.</param>
        /// <returns>True if cache contains key, otherwise false.</returns>
        public bool Contains(CacheKey key)
        {
            bool result;

            lock (_syncObject)
            {
                result = ContainsInternal(key);
            }

            return result;
        }

        #endregion

        #region Manage internal

        /// <summary>
        /// Sets the <paramref name="item"/>.
        /// </summary>
        /// <param name="key">Item's key.</param>
        /// <param name="item">Item to set.</param>
        private void SetInternal(CacheKey key, object item)
        {
            if (item == null)
            {
                return;
            }

            Type typeObj = item.GetType();
            if (!_cachedItems.ContainsKey(typeObj))
            {
                _cachedItems.Add(typeObj, new Dictionary<CacheKey, object>());
            }

            _cachedItems[typeObj][key] = item;
        }

        /// <summary>
        /// Gets the items specified by <paramref name="keys"/>.
        /// </summary>
        /// <param name="keys">Items keys.</param>
        /// <returns>List of items.</returns>
        private List<object> GetInternal(params CacheKey[] keys)
        {
            List<object> result = new List<object>(keys?.Length ?? 0);

            if (keys != null)
            {
                foreach (CacheKey key in keys)
                {
                    object item = GetInternal(key);
                    if (item != null)
                    {
                        result.Add(item);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the item specified by <paramref name="key"/>.
        /// </summary>
        /// <param name="key">Items keys.</param>
        /// <returns>Item if exists, otherwise null.</returns>
        private object GetInternal(CacheKey key)
        {
            object result = null;

            if (ContainsInternal(key))
            {
                result = _cachedItems[key.ObjectType][key];
            }

            return result;
        }

        /// <summary>
        /// Gets the items specified by <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate">Items predicate.</param>
        /// <returns>List of items.</returns>
        private List<object> GetInternal(CachePredicate predicate)
        {
            List<object> result = null;

            if (ContainsInternal(predicate.ObjectType))
            {
                result = _cachedItems[predicate.ObjectType].Values.Where(predicate.Predicate).ToList();
            }

            return result;
        }

        /// <summary>
        /// Removes the item specified by <paramref name="key"/>.
        /// </summary>
        /// <param name="key">Items keys.</param>
        /// <returns>Item if exists, otherwise null.</returns>
        private object RemoveInternal(CacheKey key)
        {
            object result = null;

            if (ContainsInternal(key))
            {
                result = GetInternal(key);
                _cachedItems[key.ObjectType].Remove(key);
            }

            return result;
        }

        /// <summary>
        /// Removes the items specified by <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate">Items predicate.</param>
        /// <returns>List of items.</returns>
        private List<object> RemoveInternal(CachePredicate predicate)
        {
            List<object> result = new List<object>();

            if (ContainsInternal(predicate.ObjectType))
            {
                Dictionary<CacheKey, object> typeDict = _cachedItems[predicate.ObjectType];
                List<KeyValuePair<CacheKey, object>> cachedItems = _cachedItems[predicate.ObjectType].Where(x => predicate.Predicate(x.Value)).ToList();

                foreach (KeyValuePair<CacheKey, object> cachedItem in cachedItems)
                {
                    typeDict.Remove(cachedItem.Key);
                    result.Add(cachedItem.Value);
                }
            }

            return result;
        }

        /// <summary>
        /// Indicates whether cache contains specified <paramref name="key"/>.
        /// </summary>
        /// <param name="key">Key to check.</param>
        /// <returns>True if cache contains key, otherwise false.</returns>
        private bool ContainsInternal(CacheKey key)
        {
            return ContainsInternal(key.ObjectType) && _cachedItems[key.ObjectType].ContainsKey(key);
        }

        /// <summary>
        /// Indicates whether cache contains specified <paramref name="keyType"/>.
        /// </summary>
        /// <param name="keyType">Type of key to check.</param>
        /// <returns>True if cache contains key type, otherwise false.</returns>
        private bool ContainsInternal(Type keyType)
        {
            return keyType != null && _cachedItems.ContainsKey(keyType);
        }

        #endregion

        #region CachePredicate

        /// <summary>
        /// Cache predicate.
        /// </summary>
        public class CachePredicate
        {
            /// <summary>
            /// Type of object that predicate is associated with.
            /// </summary>
            internal Type ObjectType { get; }

            /// <summary>
            /// Actual predicate.
            /// </summary>
            internal Func<object, bool> Predicate { get; }

            /// <summary>
            /// Initializes a new instance of the <see cref="CachePredicate"/> class.
            /// </summary>
            /// <param name="objType">Type of object that predicate is associated with</param>
            /// <param name="predicate">Actual predicate</param>
            internal CachePredicate(Type objType, Func<object, bool> predicate)
            {
                ObjectType = objType;
                Predicate = predicate;
            }

            /// <summary>
            /// Creates a new instance of the <see cref="CachePredicate"/> class.
            /// </summary>
            /// <typeparam name="T">Object type.</typeparam>
            /// <param name="predicate">Actual predicate</param>
            /// <returns><see cref="CachePredicate"/> instance.</returns>
            public static CachePredicate Create<T>(Func<T, bool> predicate)
            {
                return new CachePredicate(typeof(T), o => predicate((T)o));
            }
        }

        #endregion

        #region CacheKey

        /// <summary>
        /// Base class for cache key.
        /// </summary>
        public abstract class CacheKey
        {
            /// <summary>
            /// Gets actual key value.
            /// </summary>
            public object Key { get; }

            /// <summary>
            /// Gets object type.
            /// </summary>
            internal Type ObjectType { get; }

            /// <summary>
            /// Initializes a new instance of the <see cref="CachePredicate"/> class.
            /// </summary>
            /// <param name="objType"></param>
            /// <param name="key"></param>
            protected CacheKey(Type objType, object key)
            {
                ObjectType = objType;
                Key = key;
            }

            /// <summary>
            /// Serves as the default hash function.
            /// </summary>
            /// <returns>A hash code for the current object.</returns>
            public override int GetHashCode()
            {
                return Key != null && ObjectType != null
                    ? Key.GetHashCode() ^ ObjectType.GetHashCode()
                    : 0;
            }

            /// <summary>Determines whether the specified object is equal to the current object.
            /// </summary>
            /// <param name="obj">The object to compare with the current object.</param>
            /// <returns><see langword="true" /> if the specified object  is equal to the current object; otherwise, <see langword="false" />.</returns>
            public override bool Equals(object obj)
            {
                return Equals(obj as CacheKey);
            }

            /// <summary>Determines whether the specified object is equal to the current object.
            /// </summary>
            /// <param name="other">The object to compare with the current object.</param>
            /// <returns><see langword="true" /> if the specified object  is equal to the current object; otherwise, <see langword="false" />.</returns>
            public bool Equals(CacheKey other)
            {
                return other != null && Equals(Key, other.Key);
            }
        }

        /// <summary>
        /// Generic cache key.
        /// </summary>
        /// <typeparam name="T">Key type.</typeparam>
        public class CacheKey<T>
            : CacheKey
        {
            /// <summary>
            /// Gets actual key value.
            /// </summary>
            public new T Key { get { return (T)base.Key; } }

            /// <summary>
            /// Initializes a new instance of the <see cref="CachePredicate"/> class.
            /// </summary>
            /// <param name="objType">Object type.</param>
            /// <param name="key">Actual key value.</param>
            internal CacheKey(Type objType, T key)
                : base(objType, key)
            {

            }

            /// <summary>
            /// Creates a new instance of the <see cref="CachePredicate"/> class.
            /// </summary>
            /// <typeparam name="TObject">Object type.</typeparam>
            /// <param name="key">Actual key.</param>
            /// <returns><see cref="CacheKey{T}"/> instance.</returns>
            public static CacheKey Create<TObject>(T key)
            {
                return new CacheKey<T>(typeof(TObject), key);
            }
        }

        #endregion
    }
}
