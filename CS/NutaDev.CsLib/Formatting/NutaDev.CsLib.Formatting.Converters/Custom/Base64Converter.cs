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
using System.Text;

namespace NutaDev.CsLib.Formatting.Converters.Custom
{
    /// <summary>
    /// Provides methods to convert from and to base64.
    /// </summary>
    public class Base64Converter
    {
        /// <summary>
        /// Converts base64 string to normal string, uses <see cref="Encoding.UTF8"/>.
        /// </summary>
        /// <param name="str">Base64 string.</param>
        /// <returns>Decoded string.</returns>
        public string FromBase64(string str)
        {
            return FromBase64(str, Encoding.UTF8);
        }

        /// <summary>
        /// Converts string to base64, uses <see cref="Encoding.UTF8"/>.
        /// </summary>
        /// <param name="str">String to convert.</param>
        /// <returns>Base64 string.</returns>
        public string ToBase64(string str)
        {
            return ToBase64(str, Encoding.UTF8);
        }

        /// <summary>
        /// Converts base64 string to normal string.
        /// </summary>
        /// <param name="str">Base64 string.</param>
        /// <param name="encoding">Encoding to use.</param>
        /// <returns>Decoded string.</returns>
        public string FromBase64(string str, Encoding encoding)
        {
            return new string(encoding.GetChars(Convert.FromBase64String(str)));
        }

        /// <summary>
        /// Converts string to base64.
        /// </summary>
        /// <param name="str">String to convert.</param>
        /// <param name="encoding">Encoding to use.</param>
        /// <returns>Base64 string.</returns>
        public string ToBase64(string str, Encoding encoding)
        {
            return Convert.ToBase64String(encoding.GetBytes(str));
        }
    }
}
