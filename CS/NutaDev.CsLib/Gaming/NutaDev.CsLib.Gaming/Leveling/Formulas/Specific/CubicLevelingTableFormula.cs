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

using NutaDev.CsLib.Gaming.Leveling.Formulas.Abstract;
using System.Linq;

namespace NutaDev.CsLib.Gaming.Leveling.Formulas.Specific
{
    /// <summary>
    /// Leveling formula based on (50/3)*(x*x*x - 6*x*x + 17*x - 12).
    /// </summary>
    public class CubicLevelingTableFormula
        : LevelingTableFormula
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CubicLevelingTableFormula"/> class.
        /// </summary>
        public CubicLevelingTableFormula()
            : base(Enumerable.Range(1, 500).Select(x => (uint)x).Select(x => (int)(50u * ((((x * x * x) - 6u * (x * x) + 17u * x - 12u)) / 3u))).ToArray())
        {

        }
    }
}
