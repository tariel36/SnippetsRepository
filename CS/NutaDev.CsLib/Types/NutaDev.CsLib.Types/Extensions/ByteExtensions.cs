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

using System.Text;

namespace NutaDev.CsLib.Types.Extensions
{
    /// <summary>
    /// Provides extension methods for <see cref="byte"/> type.
    /// </summary>
    public static class ByteExtensions
    {
        /// <summary>
        /// Converts byte array into hex string.
        /// </summary>
        /// <param name="arr">Array to convert.</param>
        /// <param name="isUpper">Indicates whether result should be upper or lower case.</param>
        /// <returns>Hex string.</returns>
        public static string ToHexString(this byte[] arr, bool isUpper = true)
        {
            if (arr == null)
            {
                return null;
            }

            if (arr.Length == 0)
            {
                return string.Empty;
            }

            StringBuilder sb = new StringBuilder();

            string format = isUpper
                ? "X2"
                : "x2";

            for (int i = 0; i < arr.Length; ++i)
            {
                sb.Append(arr[i].ToString(format));
            }

            return sb.ToString();
        }
    }
}
