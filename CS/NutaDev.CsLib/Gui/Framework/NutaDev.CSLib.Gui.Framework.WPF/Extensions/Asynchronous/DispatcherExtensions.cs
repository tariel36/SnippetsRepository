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
using System.Windows.Threading;

namespace NutaDev.CsLib.Gui.Framework.Gui.Extensions
{
    /// <summary>
    /// Provides extension methods for <see cref="Dispatcher"/> class.
    /// </summary>
    public static class DispatcherExtensions
    {
        /// <summary>
        /// Shorthand for getting value through <see cref="Dispatcher.Invoke(Action)"/>.
        /// </summary>
        /// <typeparam name="T">Value type.</typeparam>
        /// <param name="dispatcher">The dispatcher.</param>
        /// <param name="getter">Getter function.</param>
        /// <returns>The value or default.</returns>
        public static T DispatcherGet<T>(this Dispatcher dispatcher, Func<T> getter)
        {
            T value = default(T);
            dispatcher.Invoke(() => value = getter());
            return value;
        }
    }
}
