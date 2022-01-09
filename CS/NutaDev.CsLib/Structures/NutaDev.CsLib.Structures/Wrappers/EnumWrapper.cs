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

namespace NutaDev.CsLib.Structures.Wrappers
{
    /// <summary>
    /// Wrapps the enum value to provide human-readable value. This class uses function delegate to convert enum value to text.
    /// </summary>
    /// <typeparam name="T">Enum type.</typeparam>
    public class EnumWrapper<T>
        where T : struct, IConvertible
    {
        /// <summary>
        /// Converts {T} to text.
        /// </summary>
        /// <param name="item">Enum to convert.</param>
        /// <returns>Converted value to text.</returns>
        public delegate string ConvertFunctionDelegate(T item);

        /// <summary>
        /// Initializes new instance of the <see cref="EnumWrapper{T}"/> class.
        /// </summary>
        /// <param name="value">Enum value to wrapp.</param>
        /// <param name="func">Conversion function delegate.</param>
        public EnumWrapper(T value, ConvertFunctionDelegate func)
        {
            if (!typeof(T).IsEnum)
            {
                throw ExceptionFactory.Create<ArgumentException>(CsLib.Resources.Text.Exceptions.Text.Type_0_IsNotAnEnum, typeof(T).Name);
            }

            EnumValue = value;
            ConvertFunction = func;
        }

        /// <summary>
        /// Gets or sets enum value.
        /// </summary>
        public T EnumValue { get; set; }

        /// <summary>
        /// Gets human-readable enum value.
        /// </summary>
        public string Text { get { return ConvertFunction == null ? EnumValue.ToString(CultureInfo.InvariantCulture) : ConvertFunction(EnumValue); } }

        /// <summary>
        /// Gets or sets conversion function delegate.
        /// </summary>
        public ConvertFunctionDelegate ConvertFunction { get; set; }

        /// <summary>
        /// Wrapps the <see cref="Text"/> property.
        /// </summary>
        /// <returns>Human-readable enum value.</returns>
        public override string ToString()
        {
            return Text;
        }
    }
}
