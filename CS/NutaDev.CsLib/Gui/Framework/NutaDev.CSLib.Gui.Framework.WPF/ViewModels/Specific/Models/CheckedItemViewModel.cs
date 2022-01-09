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

using NutaDev.CSLib.Gui.Framework.WPF.ViewModels.Abstract.Models;
using System;

namespace NutaDev.CSLib.Gui.Framework.WPF.ViewModels.Specific.Models
{
    /// <summary>
    /// View model for check controls, like checkbox or radiobox.
    /// </summary>
    public class CheckedItemViewModel
        : DataViewModel
    {
        /// <summary>
        /// Backing field for <see cref="IsChecked"/> property.
        /// </summary>
        private bool? _isChecked;

        /// <summary>
        /// Backing field for <see cref="Value"/> property.
        /// </summary>
        private object _value;

        /// <summary>
        /// Backing field for <see cref="DisplayStringProvider"/> property.
        /// </summary>
        private Func<object, string> _displayStringProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckedItemViewModel"/> class.
        /// </summary>
        public CheckedItemViewModel()
        {
            IsChecked = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckedItemViewModel"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public CheckedItemViewModel(object value)
            : this()
        {
            Value = value;
        }

        /// <summary>
        /// Gets display string.
        /// </summary>
        public string DisplayString { get { return DisplayStringProvider == null ? Value?.ToString() : DisplayStringProvider(Value); } }

        /// <summary>
        /// Gets or sets display string provider.
        /// </summary>
        public Func<object, string> DisplayStringProvider
        {
            get { return _displayStringProvider; }
            set
            {
                _displayStringProvider = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(DisplayString));
            }
        }

        /// <summary>
        /// Indicates whether item is checked or not.
        /// </summary>
        public bool? IsChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets value.
        /// </summary>
        public object Value
        {
            get { return _value; }
            set
            {
                _value = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(DisplayString));
            }
        }
    }
}
