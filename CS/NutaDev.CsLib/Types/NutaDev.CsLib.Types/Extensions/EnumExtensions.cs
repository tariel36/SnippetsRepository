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
using System.Collections.Generic;
using System.Linq;

namespace NutaDev.CsLib.Types.Extensions
{
    /// <summary>
    /// This class provides helper and extension methods for <see cref="Enum"/> class.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Returns list of enum values.
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <returns>List of <paramref name="{T}"/> values.</returns>
        /// <exception cref="ArgumentException">If <typeparamref name="T"/> is not an enum.</exception>
        public static List<T> EnumValuesToList<T>()
        {
            if (!typeof(T).IsEnum)
            {
                throw ExceptionFactory.Create<ArgumentException>(Text.Type_0_IsNotAnEnum, typeof(T).Name);
            }

            return Enum.GetValues(typeof(T)).Cast<T>().ToList();
        }

        /// <summary>
        /// Returns <see cref="IEnumerable{T}"/> of flags except <paramref name="excludedItems"/>.
        /// </summary>
        /// <param name="input"><see cref="Enum"/> to check</param>
        /// <param name="excludedItems">Collection of excluded items.</param>
        /// <returns><see cref="IEnumerable{T}"/> of flags except <paramref name="excludedItems"/>.</returns>
        public static IEnumerable<Enum> GetFlags(this Enum input, params Enum[] excludedItems)
        {
            foreach (Enum value in Enum.GetValues(input.GetType()))
            {
                if (input.HasFlag(value) && (excludedItems == null || !excludedItems.Contains(value)))
                {
                    yield return value;
                }
            }
        }

        /// <summary>
        /// Returns <see cref="IEnumerable{T}"/> of flag names except <paramref name="excludedItems"/>.
        /// </summary>
        /// <param name="input"><see cref="Enum"/> to check</param>
        /// <param name="excludedItems">Collection of excluded items.</param>
        /// <returns><see cref="IEnumerable{T}"/> of flag names except <paramref name="excludedItems"/>.</returns>
        public static string[] GetFlagNames(this Enum input, params Enum[] excludedItems)
        {
            return input.GetFlags(excludedItems).Select(x => x.ToString()).ToArray();
        }
    }
}
