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
using System.Windows.Input;

namespace NutaDev.CSLib.Gui.Framework.WPF.Extensions.Input
{
    /// <summary>
    /// This class provides helper methods for <see cref="InputBindingCollection"/> and similiar types.
    /// </summary>
    public static class InputBindingCollectionExtensions
    {
        /// <summary>
        /// FirstOrDefault implementation InputBindingCollection.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="match">The match.</param>
        /// <returns>First element that meets the match or default value of InputBinding.</returns>
        public static InputBinding FirstOrDefault(this InputBindingCollection collection, Predicate<InputBinding> match)
        {
            foreach (InputBinding item in collection)
            {
                if (match(item))
                {
                    return item;
                }
            }

            return default(InputBinding);
        }
    }
}
