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
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NutaDev.CsLib.Gui.Framework.WPF.ViewModels.Abstract.Base
{
    /// <summary>
    /// Base class for all view models. Implements <see cref="INotifyPropertyChanged"/> and <see cref="INotifyPropertyChanging"/> interfaces.
    /// </summary>
    public abstract class BaseViewModel
        : INotifyPropertyChanged
        , INotifyPropertyChanging
    {
        /// <summary>
        /// <see cref="PropertyChangedEventHandler"/> event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// <see cref="PropertyChangingEventHandler"/> event.
        /// </summary>
        public event PropertyChangingEventHandler PropertyChanging;

        /// <summary>
        /// Sets property if old value is not equal to new value and fires <see cref="OnPropertyChanged"/>.
        /// </summary>
        /// <typeparam name="T">Value type.</typeparam>
        /// <param name="field">Property's backing field reference.</param>
        /// <param name="value">Value to set.</param>
        /// <param name="propertyName">Property name.</param>
        /// <returns>True if property has changed.</returns>
        protected bool Set<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (string.IsNullOrWhiteSpace(propertyName) || Equals(field, value)) { return false; }

            field = value;

            OnPropertyChanged(propertyName);

            return true;
        }

        /// <summary>
        /// Sets property if old value is not equal to new value and fires <see cref="OnPropertyChanged"/>.
        /// </summary>
        /// <typeparam name="T">Value type.</typeparam>
        /// <param name="getter">Value getter.</param>
        /// <param name="setter">Value setter.</param>
        /// <param name="value">Value to set.</param>
        /// <param name="propertyName">Property name.</param>
        /// <returns>True if property has changed.</returns>
        protected bool Set<T>(Func<T> getter, Action<T> setter, T value, [CallerMemberName] string propertyName = null)
        {
            if (getter == null || setter == null || string.IsNullOrWhiteSpace(propertyName) || Equals(getter(), value)) { return false; }

            setter(value);

            OnPropertyChanged(propertyName);

            return true;
        }

        /// <summary>
        /// Fires the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">The name of property that changed.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Fires the <see cref="PropertyChanging"/> event.
        /// </summary>
        /// <param name="propertyName">The name of property that changes.</param>
        protected virtual void OnPropertyChanging([CallerMemberName] string propertyName = null)
        {
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }
    }
}
