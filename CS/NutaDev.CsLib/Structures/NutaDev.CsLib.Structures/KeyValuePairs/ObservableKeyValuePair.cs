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

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NutaDev.CsLib.Structures.KeyValuePairs
{
    /// <summary>
    /// <see cref="MutableKeyValuePair{TKey,TValue}"/> that implements <see cref="INotifyPropertyChanged"/> interface.
    /// </summary>
    /// <typeparam name="TKey">Key type.</typeparam>
    /// <typeparam name="TValue">Element type.</typeparam>
    public class ObservableKeyValuePair<TKey, TValue>
        : MutableKeyValuePair<TKey, TValue>,
          INotifyPropertyChanged
    {
        /// <summary>
        /// Key's backing field.
        /// </summary>
        private TKey _key;

        /// <summary>
        /// Value's backing field.
        /// </summary>
        private TValue _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableKeyValuePair{TK,TV}"/> class.
        /// </summary>
        public ObservableKeyValuePair()
        {
        }

        /// <summary>
        /// Initializes object with provided values.
        /// </summary>
        /// <param name="key">Key's value.</param>
        /// <param name="value">Value's value.</param>
        public ObservableKeyValuePair(TKey key, TValue value)
            : base(key, value)
        {
        }

        /// <summary>
        /// PropertyChanged event. Fired whether <see cref="Key"/> or <see cref="Value"/> has changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The key.
        /// </summary>
        public new TKey Key
        {
            get { return _key; }
            set
            {
                _key = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The value.
        /// </summary>
        public new TValue Value
        {
            get { return _value; }
            set
            {
                _value = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// PropertyChanged event handler.
        /// </summary>
        /// <param name="propertyName">Property name that changed.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
