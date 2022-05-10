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
    /// Converts <see cref="TimeSpan"/> to <see cref="Visibility"/>.
    /// </summary>
    public class TimeSpanToVisibilityConverter
        : IValueConverter
    {
        /// <summary>
        /// Converts <see cref="TimeSpan"/> to <see cref="Visibility"/>.
        /// </summary>
        /// <param name="value"><see cref="TimeSpan"/> value.</param>
        /// <param name="targetType">Not used.</param>
        /// <param name="parameter">Not used.</param>
        /// <param name="culture">Not used.</param>
        /// <returns>
        /// <see cref="Visibility.Collapsed"/> if <paramref name="value"/> is not <see cref="TimeSpan"/>
        /// or is any of <see cref="TimeSpan.MinValue"/>, <see cref="TimeSpan.MaxValue"/>, <see cref="TimeSpan.Zero"/>,
        /// otherwise <see cref="Visibility.Visible"/>.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TimeSpan timeSpan)
            {
                return timeSpan == TimeSpan.MinValue || timeSpan == TimeSpan.MaxValue || timeSpan == TimeSpan.Zero
                        ? Visibility.Collapsed
                        : Visibility.Visible
                    ;
            }

            return Visibility.Collapsed;
        }

        /// <summary>
        /// Converts <see cref="Visibility"/> to <see cref="TimeSpan"/>. This method is not implemented.
        /// </summary>
        /// <param name="value">Not used.</param>
        /// <param name="targetType">Not used.</param>
        /// <param name="parameter">Not used.</param>
        /// <param name="culture">Not used.</param>
        /// <returns>Nothing.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw ExceptionFactory.NotImplementedException(nameof(TimeSpanToVisibilityConverter));
        }
    }
}
