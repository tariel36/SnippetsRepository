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
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace NutaDev.CsLib.Gui.Framework.Gui.Converters
{
    /// <summary>
    /// Converts <see cref="Enum"/> value to <see cref="Visibility"/>.
    /// </summary>
    public class VisibilityByEnumConverter
        : IValueConverter
    {
        /// <summary>
        /// Converts <see cref="Enum"/> value to <see cref="Visibility"/>.
        /// </summary>
        /// <param name="value">Enum value.</param>
        /// <param name="targetType">Not used.</param>
        /// <param name="parameter">Expected value.</param>
        /// <param name="culture">Not used.</param>
        /// <returns><see cref="Visibility.Visible"/> if <paramref name="value"/> is equal to <paramref name="parameter"/>, otherwise <see cref="Visibility.Collapsed"/>.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Enum enumValue && parameter is Enum parameterValue && Equals(enumValue, parameterValue))
            {
                return Visibility.Visible;
            }

            return Visibility.Collapsed;
        }

        /// <summary>
        /// Converts <see cref="Visibility"/> to <see cref="Enum"/>. This method is not implemented.
        /// </summary>
        /// <param name="value">Not used.</param>
        /// <param name="targetType">Not used.</param>
        /// <param name="parameter">Not used.</param>
        /// <param name="culture">Not used.</param>
        /// <returns>Nothing.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw ExceptionFactory.NotImplementedException(nameof(VisibilityByEnumConverter));
        }
    }
}
