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
using System.Globalization;
using System.Windows.Data;

namespace NutaDev.CsLib.Gui.Framework.Gui.Converters
{
    /// <summary>
    /// Converts <see cref="TimeSpan"/> with provided format.
    /// </summary>
    public class TimeSpanToStringConverter
        : IValueConverter
    {
        /// <summary>
        /// Conversion from <see cref="TimeSpan"/> to <see cref="string"/> with format provided in <paramref name="parameter"/>.
        /// </summary>
        /// <param name="value">Value to format.</param>
        /// <param name="targetType">The target type, should be <see cref="string"/>.</param>
        /// <param name="parameter">The format.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>Formatted string.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType == typeof(string))
            {
                if (value is TimeSpan)
                {
                    return ((TimeSpan)value).ToString(parameter?.ToString(), culture);
                }
            }

            throw ExceptionFactory.NotSupportedException(Text.ConvertTargetType_0_, targetType?.Name);
        }

        /// <summary>
        /// Convers from <see cref="string"/> to <see cref="TimeSpan"/> with format provided in <paramref name="parameter"/>.
        /// </summary>
        /// <param name="value">Value to format.</param>
        /// <param name="targetType">The target type, should be <see cref="TimeSpan"/>.</param>
        /// <param name="parameter">The format.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>Parsed <see cref="TimeSpan"/>.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType == typeof(TimeSpan))
            {
                if (value is string)
                {
                    return TimeSpan.Parse(value.ToString(), culture);
                }
            }

            throw ExceptionFactory.NotSupportedException(Text.ConvertBackTargetType_0_, targetType?.Name);
        }
    }
}
