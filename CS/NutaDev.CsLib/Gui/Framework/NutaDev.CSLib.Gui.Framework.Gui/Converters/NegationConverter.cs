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
using System.Globalization;
using System.Windows.Data;

namespace NutaDev.CsLib.Gui.Framework.Gui.Converters
{
    /// <summary>
    /// Converter that negates boolean value.
    /// </summary>
    public class NegationConverter
        : IValueConverter
    {
        /// <summary>
        /// If provided <paramref name="value"/> is boolean, then negation is returned, value otherwise.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <param name="targetType">Unused parameter.</param>
        /// <param name="parameter">Unused parameter.</param>
        /// <param name="culture">Unused parameter.</param>
        /// <returns>Negated <paramref name="value"/> or value.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Negate(value);
        }

        /// <summary>
        /// If provided <paramref name="value"/> is boolean, then negation is returned, value otherwise.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <param name="targetType">Unused parameter.</param>
        /// <param name="parameter">Unused parameter.</param>
        /// <param name="culture">Unused parameter.</param>
        /// <returns>Negated <paramref name="value"/> or value.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Negate(value);
        }

        /// <summary>
        /// If provided <paramref name="value"/> is boolean type, then negation is returned, value otherwise.
        /// </summary>
        /// <param name="value">Value to negate.</param>
        /// <returns>Negation of <paramref name="value"/> or value.</returns>
        private object Negate(object value)
        {
            if (value is bool bValue)
            {
                return !bValue;
            }

            return value;
        }
    }
}
