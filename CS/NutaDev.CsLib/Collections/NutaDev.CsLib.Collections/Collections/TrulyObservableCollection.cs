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
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NutaDev.CsLib.Collections.Collections
{
    /// <summary>
    /// <see cref="TrulyObservableCollection{T}"/> is an observable collection that raises changed events
    /// when any of its elements changed or collection itself has been modified.
    /// </summary>
    /// <typeparam name="T">Element type.</typeparam>
    public class TrulyObservableCollection<T>
        : ICollection<T>
        , IList<T>
        , IList
        , INotifyPropertyChanged
        , INotifyCollectionChanged
    {
        /// <summary>
        /// Inner list.
        /// </summary>
        private readonly IList<T> _innerList;

        /// <summary>
        /// Initializes a new instance of the <see cref="TrulyObservableCollection{T}"/> class.
        /// </summary>
        public TrulyObservableCollection()
            : this(null)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TrulyObservableCollection{T}"/> class.
        /// </summary>
        /// <param name="items">Items to add. Events are not raised.</param>
        public TrulyObservableCollection(IEnumerable<T> items)
        {
            SyncRoot = new object();

            _innerList = new List<T>(items ?? new List<T>());
            ChangingAddedItems = new List<T>();
            ChangingRemovedItems = new List<T>();
        }

        /// <summary>
        /// Raised when any of the collection's properties change.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raised when any of the collection's elements property change or collection itself has been modified.
        /// </summary>
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        /// <summary>
        /// Indicates whether collection is synchronized.
        /// </summary>
        public bool IsSynchronized { get { return true; } }

        /// <summary>
        /// Indicates whether collection is fixed size.
        /// </summary>
        public bool IsFixedSize { get { return false; } }

        /// <summary>
        /// Gets count of elements.
        /// </summary>
        public int Count { get { return _innerList.Count; } }

        /// <summary>
        /// Indicates whether collection is readonly.
        /// </summary>
        public bool IsReadOnly { get { return _innerList.IsReadOnly; } }

        /// <summary>
        /// Synchronization root.
        /// </summary>
        public object SyncRoot { get; }

        /// <summary>
        /// Indicates whether collection is changing.
        /// </summary>
        public bool IsChanging { get; private set; }

        /// <summary>
        /// List of added items. Used by events.
        /// </summary>
        private List<T> ChangingAddedItems { get; }

        /// <summary>
        /// List of removed items. Used by events.
        /// </summary>
        private List<T> ChangingRemovedItems { get; }

        /// <summary>
        /// Gets or sets elements by index.
        /// </summary>
        /// <param name="index">Index to set.</param>
        /// <returns>Value at specific index.</returns>
        /// <exception cref="IndexOutOfRangeException">If index is out of range.</exception>
        public T this[int index]
        {
            get { return GetItem(index); }
            set { SetItem(index, value); }
        }

        /// <summary>
        /// Gets or sets elements by index.
        /// </summary>
        /// <param name="index">Index to set.</param>
        /// <returns>Value at specific index.</returns>
        /// <exception cref="IndexOutOfRangeException">If index is out of range.</exception>
        /// <exception cref="InvalidOperationException">If value is not <typeparamref name="{T}"/>.</exception>
        object IList.this[int index]
        {
            get { return GetItem(index); }
            set { SetItem(index, value); }
        }

        /// <summary>Returns an enumerator that iterates through the collection.</summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return _innerList.GetEnumerator();
        }

        /// <summary>Returns an enumerator that iterates through the collection.</summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _innerList.GetEnumerator();
        }

        /// <summary>
        /// Adds an item to collection.
        /// </summary>
        /// <param name="item">Item to add.</param>
        /// <exception cref="IndexOutOfRangeException">If index is out of range.</exception>
        /// <exception cref="InvalidOperationException">If value is not <typeparamref name="{T}"/>.</exception>
        public void Add(T item)
        {
            AddItem(item);
        }

        /// <summary>
        /// Adds an item to collection.
        /// </summary>
        /// <param name="value">Item to add.</param>
        /// <exception cref="IndexOutOfRangeException">If index is out of range.</exception>
        /// <exception cref="InvalidOperationException">If value is not <typeparamref name="{T}"/>.</exception>
        /// <returns>Index of new item.</returns>
        public int Add(object value)
        {
            return AddItem(value);
        }

        /// <summary>
        /// Adds items to collection. <see cref="BeginCollectionChanging"/>, <see cref="EndCollectionChanging"/> and <see cref="Add(T)"/> are invoked internaly.
        /// </summary>
        /// <param name="items">Item to add.</param>
        /// <exception cref="IndexOutOfRangeException">If index is out of range.</exception>
        /// <exception cref="InvalidOperationException">If value is not <typeparamref name="{T}"/>.</exception>
        public void AddRange(IEnumerable<T> items)
        {
            if (items == null) { throw ExceptionFactory.ArgumentNullException(nameof(items)); }

            BeginCollectionChanging();

            foreach (T item in items)
            {
                Add(item);
            }

            EndCollectionChanging();
        }

        /// <summary>
        /// Removes all items from the <see cref="TrulyObservableCollection{T}"/>.
        /// </summary>
        public void Clear()
        {
            List<T> oldItems = new List<T>();

            foreach (T item in _innerList)
            {
                TryUnsubscribeItem(item);
                oldItems.Add(item);

                if (IsChanging)
                {
                    ChangingRemovedItems.Add(item);
                }
            }

            _innerList.Clear();

            OnPropertyChanged(nameof(Count));
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, new List<T>(), oldItems, 0));
        }

        /// <summary>
        /// Determines whether the <see cref="TrulyObservableCollection{T}" /> contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="TrulyObservableCollection{T}" />.</param>
        /// <returns><see langword="true" /> if <paramref name="item" /> is found in the <see cref="TrulyObservableCollection{T}" />; otherwise, <see langword="false" />.</returns>
        public bool Contains(T item)
        {
            return ContainsItem(item);
        }

        /// <summary>
        /// Determines whether the <see cref="TrulyObservableCollection{T}" /> contains a specific value.
        /// </summary>
        /// <param name="value">The object to locate in the <see cref="TrulyObservableCollection{T}" />.</param>
        /// <returns><see langword="true" /> if <paramref name="value" /> is found in the <see cref="TrulyObservableCollection{T}" />; otherwise, <see langword="false" />.</returns>
        public bool Contains(object value)
        {
            return ContainsItem(value);
        }

        /// <summary>
        /// Copies the elements of the <see cref="TrulyObservableCollection{T}" /> to an <see cref="Array" />, starting at a particular <see cref="Array" /> index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="Array" /> that is the destination of the elements copied from <see cref="TrulyObservableCollection{T}" />. The <see cref="Array" /> must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
        /// <exception cref="ArgumentNullException"><paramref name="array" /> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="arrayIndex" /> is less than 0.</exception>
        /// <exception cref="ArgumentException">The number of elements in the source <see cref="TrulyObservableCollection{T}" /> is greater than the available space from <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />.</exception>
        public void CopyTo(T[] array, int arrayIndex)
        {
            _innerList.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Copies the elements of the <see cref="TrulyObservableCollection{T}" /> to an <see cref="Array" />, starting at a particular <see cref="Array" /> index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="Array" /> that is the destination of the elements copied from <see cref="TrulyObservableCollection{T}" />. The <see cref="Array" /> must have zero-based indexing.</param>
        /// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
        /// <exception cref="ArgumentNullException"><paramref name="array" /> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index" /> is less than 0.</exception>
        /// <exception cref="ArgumentException">The number of elements in the source <see cref="TrulyObservableCollection{T}" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
        public void CopyTo(Array array, int index)
        {
            if (array == null) { throw ExceptionFactory.ArgumentNullException(nameof(array)); }
            if (index < 0) { throw ExceptionFactory.ArgumentOutOfRangeException(nameof(array)); }

            int space = array.Length - index;

            if (space - index < _innerList.Count) { throw ExceptionFactory.Create<ArgumentException>(Text.NoEnoughSpaceGot_0_Expected_1_, space, _innerList.Count); }

            for (int i = index, j = 0; i < _innerList.Count; ++i, ++j)
            {
                array.SetValue(_innerList[i], j);
            }
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="TrulyObservableCollection{T}" />.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="TrulyObservableCollection{T}" />.</param>
        /// <returns><see langword="true" /> if <paramref name="item" /> was successfully removed from the <see cref="TrulyObservableCollection{T}" />; otherwise, <see langword="false" />. This method also returns <see langword="false" /> if <paramref name="item" /> is not found in the original <see cref="TrulyObservableCollection{T}" />.</returns>
        /// <exception cref="InvalidOperationException">If value is not <typeparamref name="{T}"/>.</exception>
        public bool Remove(T item)
        {
            return RemoveItem(item);
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="TrulyObservableCollection{T}" />.
        /// </summary>
        /// <param name="value">The object to remove from the <see cref="TrulyObservableCollection{T}" />.</param>
        /// <returns><see langword="true" /> if <paramref name="value" /> was successfully removed from the <see cref="TrulyObservableCollection{T}" />; otherwise, <see langword="false" />. This method also returns <see langword="false" /> if <paramref name="value" /> is not found in the original <see cref="TrulyObservableCollection{T}" />.</returns>
        /// <exception cref="InvalidOperationException">If value is not <typeparamref name="{T}"/>.</exception>
        public void Remove(object value)
        {
            RemoveItem(value);
        }

        /// <summary>
        /// Determines the index of a specific item in the <see cref="TrulyObservableCollection{T}" />.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="TrulyObservableCollection{T}" />.</param>
        /// <returns>The index of <paramref name="item" /> if found in the list; otherwise, -1.</returns>
        public int IndexOf(T item)
        {
            return IndexOfItem(item);
        }

        /// <summary>
        /// Determines the index of a specific item in the <see cref="TrulyObservableCollection{T}" />.
        /// </summary>
        /// <param name="value">The object to locate in the <see cref="TrulyObservableCollection{T}" />.</param>
        /// <returns>The index of <paramref name="value" /> if found in the list; otherwise, -1.</returns>
        /// <exception cref="InvalidOperationException">If value is not <typeparamref name="{T}"/>.</exception>
        public int IndexOf(object value)
        {
            return IndexOfItem(value);
        }

        /// <summary>
        /// Inserts an item to the <see cref="TrulyObservableCollection{T}" /> at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which <paramref name="item" /> should be inserted.</param>
        /// <param name="item">The object to insert into the <see cref="TrulyObservableCollection{T}" />.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index" /> is not a valid index in the <see cref="TrulyObservableCollection{T}" />.</exception>
        /// <exception cref="NotSupportedException">The <see cref="TrulyObservableCollection{T}" /> is read-only.</exception>
        public void Insert(int index, T item)
        {
            InsertItem(index, item);
        }

        /// <summary>
        /// Inserts an item to the <see cref="TrulyObservableCollection{T}" /> at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
        /// <param name="value">The object to insert into the <see cref="TrulyObservableCollection{T}" />.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index" /> is not a valid index in the <see cref="TrulyObservableCollection{T}" />.</exception>
        /// <exception cref="NotSupportedException">The <see cref="TrulyObservableCollection{T}" /> is read-only.</exception>
        public void Insert(int index, object value)
        {
            InsertItem(index, value);
        }

        /// <summary>
        /// Removes the <see cref="TrulyObservableCollection{T}" /> item at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the item to remove.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index" /> is not a valid index in the <see cref="TrulyObservableCollection{T}" />.</exception>
        /// <exception cref="NotSupportedException">The <see cref="TrulyObservableCollection{T}" /> is read-only.</exception>
        public void RemoveAt(int index)
        {
            if (IsIndexValid(index))
            {
                T item = _innerList[index];

                TryUnsubscribeItem(_innerList[index]);
                _innerList.RemoveAt(index);

                if (IsChanging)
                {
                    ChangingRemovedItems.Add(item);
                }

                OnPropertyChanged(nameof(Count));
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, index));
            }
        }

        /// <summary>
        /// Beings the collection changing. <see cref="TrulyObservableCollection{T}"/> does not fire <see cref="CollectionChanged"/> event unti <see cref="EndCollectionChanging"/> is called.
        /// </summary>
        public void BeginCollectionChanging()
        {
            if (!IsChanging)
            {
                ChangingAddedItems.Clear();
                ChangingRemovedItems.Clear();

                IsChanging = true;
            }
        }

        /// <summary>
        /// Ends collection chaning and fires <see cref="CollectionChanged"/> event if collection has changed.
        /// </summary>
        public void EndCollectionChanging()
        {
            if (IsChanging)
            {
                IsChanging = false;

                if (ChangingAddedItems.Count > 0 || ChangingRemovedItems.Count > 0)
                {
                    OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, ChangingAddedItems, ChangingRemovedItems));
                }

                ChangingAddedItems.Clear();
                ChangingRemovedItems.Clear();
            }
        }

        /// <summary>
        /// Performs an attempt to subscribe from item's events.
        /// </summary>
        /// <param name="item">Item with events</param>
        private void TrySubscribeItem(T item)
        {
            if (item is INotifyPropertyChanged propChangedItem)
            {
                propChangedItem.PropertyChanged += ItemOnPropertyChanged;
            }
        }

        /// <summary>
        /// Performs an attempt to unsubscribe from item's events.
        /// </summary>
        /// <param name="item">Item with events</param>
        private void TryUnsubscribeItem(T item)
        {
            if (item is INotifyPropertyChanged propChangedItem)
            {
                propChangedItem.PropertyChanged -= ItemOnPropertyChanged;
            }
        }

        /// <summary>
        /// Performs an attempt to toggle subscription to item's events.
        /// </summary>
        /// <param name="item">Item with events</param>
        private void TryToggleItemSubscription(T item)
        {
            TryUnsubscribeItem(item);
            TrySubscribeItem(item);
        }

        /// <summary>
        /// Fires <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">Property that changed.</param>
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (!IsChanging)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Fires <see cref="CollectionChanged"/> event.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        private void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (!IsChanging)
            {
                CollectionChanged?.Invoke(this, e);
            }
        }

        /// <summary>
        /// Fires <see cref="CollectionChanged"/> event if any of items has changed.
        /// </summary>
        /// <param name="sender">Source item.</param>
        /// <param name="propertyChangedEventArgs">Event arguments.</param>
        private void ItemOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        /// <summary>
        /// Set's <paramref name="value"/> specific <paramref name="index"/>.
        /// </summary>
        /// <param name="index">Index to set.</param>
        /// <param name="value">Value to set.</param>
        /// <exception cref="IndexOutOfRangeException">If index is not valid.</exception>
        /// <exception cref="InvalidOperationException">If item's type is not <typeparamref name="T" />.</exception>
        private void SetItem(int index, object value)
        {
            if (!IsIndexValid(index))
            {
                throw ExceptionFactory.Create<IndexOutOfRangeException>(Text.Index_0_IsOutOfRange, index);
            }

            if (!IsTypeValid(value))
            {
                throw ExceptionFactory.Create<InvalidOperationException>(Text.The_0_IsNot_1_, nameof(value), typeof(T).Name);
            }

            T oldItem = _innerList[index];
            _innerList[index] = (T)value;

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, value, oldItem, index));
        }

        /// <summary>
        /// Adds an item to the collection.
        /// </summary>
        /// <param name="value">Value to add.</param>
        /// <returns><paramref name="value"/> index.</returns>
        /// <exception cref="InvalidOperationException">If item's type is not <typeparamref name="T" />.</exception>
        private int AddItem(object value)
        {
            if (!IsTypeValid(value))
            {
                throw ExceptionFactory.Create<InvalidOperationException>(Text.The_0_IsNot_1_, nameof(value), typeof(T).Name);
            }

            T item = (T)value;

            TryToggleItemSubscription(item);

            _innerList.Add(item);

            if (IsChanging)
            {
                ChangingAddedItems.Add(item);
            }

            OnPropertyChanged(nameof(Count));
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, Count - 1));

            return Count - 1;
        }

        /// <summary>
        /// Gets an item at <paramref name="index"/>.
        /// </summary>
        /// <param name="index">Value's index.</param>
        /// <returns>Value at <paramref name="index"/>.</returns>
        /// <exception cref="IndexOutOfRangeException">If index is not valid.</exception>
        private T GetItem(int index)
        {
            if (!IsIndexValid(index))
            {
                throw ExceptionFactory.Create<IndexOutOfRangeException>(Text.Index_0_IsOutOfRange, index);
            }

            return _innerList[index];
        }

        /// <summary>
        /// Determines whether the <see cref="TrulyObservableCollection{T}" /> contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="TrulyObservableCollection{T}" />.</param>
        /// <returns><see langword="true" /> if <paramref name="item" /> is found in the <see cref="TrulyObservableCollection{T}" />; otherwise, <see langword="false" />.</returns>
        private bool ContainsItem(object item)
        {
            if (!IsTypeValid(item))
            {
                throw ExceptionFactory.Create<InvalidOperationException>(Text.The_0_IsNot_1_, nameof(item), typeof(T).Name);
            }

            return _innerList.Contains((T)item);
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="TrulyObservableCollection{T}" />.
        /// </summary>
        /// <param name="value">The object to remove from the <see cref="TrulyObservableCollection{T}" />.</param>
        /// <returns><see langword="true" /> if <paramref name="value" /> was successfully removed from the <see cref="TrulyObservableCollection{T}" />; otherwise, <see langword="false" />. This method also returns <see langword="false" /> if <paramref name="value" /> is not found in the original <see cref="TrulyObservableCollection{T}" />.</returns>
        /// <exception cref="InvalidOperationException">If value is not <typeparamref name="{T}"/>.</exception>
        private bool RemoveItem(object value)
        {
            if (!IsTypeValid(value))
            {
                throw ExceptionFactory.Create<InvalidOperationException>(Text.The_0_IsNot_1_, nameof(value), typeof(T).Name);
            }

            T item = (T)value;

            int index = _innerList.IndexOf(item);

            if (index >= 0)
            {
                TryUnsubscribeItem(item);

                if (IsChanging)
                {
                    ChangingRemovedItems.Add(item);
                }

                _innerList.RemoveAt(index);

                OnPropertyChanged(nameof(Count));
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, index));

                return true;
            }

            return false;
        }

        /// <summary>
        /// Determines the index of a specific item in the <see cref="TrulyObservableCollection{T}" />.
        /// </summary>
        /// <param name="value">The object to locate in the <see cref="TrulyObservableCollection{T}" />.</param>
        /// <returns>The index of <paramref name="value" /> if found in the list; otherwise, -1.</returns>
        /// <exception cref="InvalidOperationException">If value is not <typeparamref name="{T}"/>.</exception>
        private int IndexOfItem(object value)
        {
            if (!IsTypeValid(value))
            {
                throw ExceptionFactory.Create<InvalidOperationException>(Text.The_0_IsNot_1_, nameof(value), typeof(T).Name);
            }

            T item = (T)value;

            return _innerList.IndexOf(item);
        }

        /// <summary>
        /// Inserts an item to the <see cref="TrulyObservableCollection{T}" /> at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
        /// <param name="value">The object to insert into the <see cref="TrulyObservableCollection{T}" />.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index" /> is not a valid index in the <see cref="TrulyObservableCollection{T}" />.</exception>
        /// <exception cref="NotSupportedException">The <see cref="TrulyObservableCollection{T}" /> is read-only.</exception>
        private void InsertItem(int index, object value)
        {
            if (!IsIndexValid(index))
            {
                throw ExceptionFactory.Create<IndexOutOfRangeException>(Text.Index_0_IsOutOfRange, index);
            }

            if (!IsTypeValid(value))
            {
                throw ExceptionFactory.Create<InvalidOperationException>(Text.The_0_IsNot_1_, nameof(value), typeof(T).Name);
            }

            T item = (T)value;

            TryToggleItemSubscription(item);

            _innerList.Insert(index, item);

            if (IsChanging)
            {
                ChangingAddedItems.Add(item);
            }

            OnPropertyChanged(nameof(Count));
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index));
        }

        /// <summary>
        /// Determines whether <paramref name="index"/> is greater or equal to 0 and lower than <see cref="Count"/>.
        /// </summary>
        /// <param name="index">Index to check.</param>
        /// <returns>True if <paramref name="index"/> is greater or equal to 0 and lower than <see cref="Count"/>, otherwise false.</returns>
        private bool IsIndexValid(int index)
        {
            return index >= 0 && index < _innerList.Count;
        }

        /// <summary>
        /// Determines whether <paramref name="value"/> is <typeparamref name="T"/>.
        /// </summary>
        /// <param name="value">Value to check.</param>
        /// <returns>True if <paramref name="value"/> is <typeparamref name="T"/>, otherwise false.</returns>
        private bool IsTypeValid(object value)
        {
            return value is T || Equals(value, default(T));
        }
    }
}
