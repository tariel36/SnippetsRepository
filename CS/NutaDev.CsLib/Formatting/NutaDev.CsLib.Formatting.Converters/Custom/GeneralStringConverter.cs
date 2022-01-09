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

namespace NutaDev.CsLib.Formatting.Converters.Custom
{
    /// <summary>
    /// A converter that provides methods to convert between <see cref="string"/> and various basic types.
    /// </summary>
    public class GeneralStringConverter
    {
        /// <summary>
        /// Converts string to given type.
        /// </summary>
        /// <param name="value">String to convert.</param>
        /// <param name="type">Target type.</param>
        /// <param name="culture">Conversion culture. If not provided <see cref="CultureInfo.InvariantCulture"/> is used.</param>
        /// <returns>Converted string or null.</returns>
        public object ToType(string value, Type type, CultureInfo culture = null)
        {
            if (type == typeof(string) || type == typeof(object)) { return value; }

            if (culture == null) { culture = CultureInfo.InvariantCulture; }

            if (type == typeof(byte)) { return Convert.ToByte(value, culture); }
            if (type == typeof(bool)) { return Convert.ToBoolean(value, culture); }
            if (type == typeof(char)) { return Convert.ToChar(value, culture); }
            if (type == typeof(decimal)) { return Convert.ToDecimal(value, culture); }
            if (type == typeof(float)) { return Convert.ToSingle(value, culture); }
            if (type == typeof(double)) { return Convert.ToDouble(value, culture); }
            if (type == typeof(int)) { return Convert.ToInt32(value, culture); }
            if (type == typeof(long)) { return Convert.ToInt64(value, culture); }
            if (type == typeof(sbyte)) { return Convert.ToSByte(value, culture); }
            if (type == typeof(short)) { return Convert.ToInt16(value, culture); }
            if (type == typeof(uint)) { return Convert.ToUInt32(value, culture); }
            if (type == typeof(ulong)) { return Convert.ToUInt64(value, culture); }
            if (type == typeof(ushort)) { return Convert.ToUInt16(value, culture); }

            if (type == typeof(void)) { return null; }

            return null;
        }

        /// <summary>
        /// Attempts to convert string to given type by type's name.
        /// </summary>
        /// <param name="typeName"><see cref="Type"/>'s name.</param>
        /// <param name="value">The value to convert.</param>
        /// <exception cref="InvalidOperationException">If <paramref name="typeName"/> not found.</exception>
        /// <returns>The converted value.</returns>
        public object ToString(string typeName, string value)
        {
            if (string.IsNullOrWhiteSpace(typeName))
            {
                return null;
            }

            Type type = Type.GetType(typeName);

            if (type == null)
            {
                throw ExceptionFactory.Create<InvalidOperationException>(Text._0_NotFound, typeName);
            }

            return Convert.ChangeType(value, type);
        }
    }
}
