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

using System.Runtime.CompilerServices;

namespace NutaDev.CsLib.Types.Extensions
{
    /// <summary>
    /// This class provides extension methods for integer types, like <see cref="int"/> structure.
    /// </summary>
    public static class IntegerExtensions
    {
        /// <summary>
        /// Converts int value to boolean.
        /// </summary>
        /// <param name="value">Int value.</param>
        /// <returns>False if 0, true otherwise.</returns>
        public static bool ToBool(this int value)
        {
            return value != 0;
        }

        /// <summary>
        /// Sets <see cref="value"/> within <see cref="min"/> and <see cref="max"/> range, inclusive.
        /// </summary>
        /// <param name="value">The value to set.</param>
        /// <param name="min">Minimum value.</param>
        /// <param name="max">Maximum value.</param>
        /// <returns>The corrected value.</returns>
        public static int SetInRange(int value, int min, int max)
        {
            return (value > max ? max : (value < min ? min : value));
        }

        /// <summary>
        /// Gets the sign of <see cref="value"/>.
        /// </summary>
        /// <param name="value">Value to check.</param>
        /// <returns>The 1 if value is positive, -1 if value is negative.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetSign(int value)
        {
            return value < 0 ? -1 : 1;
        }
    }
}
