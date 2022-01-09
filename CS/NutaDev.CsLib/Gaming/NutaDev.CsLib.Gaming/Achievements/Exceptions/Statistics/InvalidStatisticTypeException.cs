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
using NutaDev.CsLib.Gaming.Achievements.Model.Statistics.Values.Abstract;
using NutaDev.CsLib.Gaming.Resources;

namespace NutaDev.CsLib.Gaming.Achievements.Exceptions.Statistics
{
    /// <summary>
    /// An exception thrown when types of <see cref="StatisticValue"/> don't match.
    /// </summary>
    public class InvalidStatisticTypeException
        : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidStatisticTypeException"/> class.
        /// </summary>
        /// <param name="expected">Expected type.</param>
        /// <param name="actual">Actual type.</param>
        public InvalidStatisticTypeException(Type expected, Type actual)
            : this(expected, actual, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidStatisticTypeException"/> class.
        /// </summary>
        /// <param name="expected">Expected type.</param>
        /// <param name="actual">Actual type.</param>
        /// <param name="innerException">Inner exception.</param>
        public InvalidStatisticTypeException(Type expected, Type actual, Exception innerException)
            : base(string.Format(Text.ExpectedType_0_ButActualIs_1_, GetTypeName(expected), GetTypeName(actual)), innerException)
        {
        }

        /// <summary>
        /// Get type name shorthand.
        /// </summary>
        /// <param name="type">Type to get name from.</param>
        /// <returns>Name of type.</returns>
        private static string GetTypeName(Type type)
        {
            return type?.Name ?? "<null>";
        }
    }
}
