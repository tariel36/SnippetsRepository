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

using NutaDev.CsLib.Math.Calculator.Tokens;
using System.Collections;
using System.Collections.Generic;

namespace NutaDev.CsLib.Internal.Tests.Utility.Comparers
{
    /// <summary>
    /// Compares two <see cref="Token"/>
    /// </summary>
    public class TokenComparer
        : IComparer<Token>
        , IComparer
    {
        /// <summary>
        /// Compares two <see cref="Token"/> objects.
        /// </summary>
        /// <param name="x">First object.</param>
        /// <param name="y">Second object.</param>
        /// <returns>The result of comparison - 1, 0 or -1.</returns>
        public int Compare(Token x, Token y)
        {
            return (x == y && x == null) || (x != null && y != null && x.Type == y.Type && string.Equals(x.Value, y.Value))
                ? 0
                : -1;
        }

        /// <summary>
        /// Compares two <see cref="Token"/> objects.
        /// </summary>
        /// <param name="x">First object.</param>
        /// <param name="y">Second object.</param>
        /// <returns>The result of comparison - 1, 0 or -1.</returns>
        public int Compare(object x, object y)
        {
            return Compare(x as Token, y as Token);
        }
    }
}
