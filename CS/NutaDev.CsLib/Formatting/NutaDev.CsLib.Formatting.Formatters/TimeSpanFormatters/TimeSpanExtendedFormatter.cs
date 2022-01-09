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

namespace NutaDev.CsLib.Formatting.Formatters.TimeSpanFormatters
{
    /// <summary>
    /// Formatter that formats <see cref="TimeSpan"/> into text representation of <see cref="TimeSpan.TotalSeconds"/>.
    /// </summary>
    public class TimeSpanExtendedFormatter
        : IFormatProvider
        , ICustomFormatter
    {
        /// <summary>
        /// Returns an object that provides formatting services for the specified type.
        /// </summary>
        /// <param name="formatType">Type of formatter.</param>
        /// <returns>Formatter if is supported, null otherwise.</returns>
        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
            {
                return this;
            }

            return null;
        }

        /// <summary>
        /// Converts the value of a specified object to an equivalent string representation using specified format and culture-specific formatting information.
        /// </summary>
        /// <param name="format">Format to use.</param>
        /// <param name="arg">Format argument.</param>
        /// <param name="formatProvider">Formatter to use.</param>
        /// <returns>Formatted value.</returns>
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (!Equals(formatProvider))
            {
                return null;
            }

            if (arg is TimeSpan ts)
            {
                if (string.Equals(format, "ts", StringComparison.InvariantCulture))
                {
                    return $"{ts.TotalSeconds}";
                }
            }

            return arg.ToString();
        }
    }
}
