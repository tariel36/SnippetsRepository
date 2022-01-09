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

namespace NutaDev.CsLib.Structures.Wrappers
{
    /// <summary>
    /// This class allows to use any type as reference type.
    /// </summary>
    /// <typeparam name="T">Target value.</typeparam>
    public class ValueWrapper<T>
    {
        /// <summary>
        /// Backing field to <see cref="Value"/>.
        /// </summary>
        private T _value;

        /// <summary>
        /// The wrapped value.
        /// </summary>
        public T Value
        {
            get { return _value; }
            set { _value = value; }
        }

        /// <summary>
        /// Conversion between <see cref="ValueWrapper{T}"/> and {T}.
        /// </summary>
        /// <param name="value"><see cref="ValueWrapper{T}"/> to convert.</param>
        public static implicit operator T(ValueWrapper<T> value)
        {
            return value.Value;
        }

        /// <summary>
        /// Conversion between <see cref="ValueWrapper{T}"/> and {T}.
        /// </summary>
        /// <param name="value">{T} to convert.</param>
        public static implicit operator ValueWrapper<T>(T value)
        {
            return new ValueWrapper<T> { Value = value };
        }
    }
}
