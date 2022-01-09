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

namespace NutaDev.CsLib.Gaming.Leveling.Formulas.Specific
{
    /// <summary>
    /// Simple leveling formula based on accumulation of 1000.
    /// </summary>
    public class SimpleLevelingFormula
        : ILevelingFormula
    {
        /// <summary>
        /// Gets level based on experience.
        /// </summary>
        /// <param name="exp">Experience value.</param>
        /// <returns>The level related to experienced.</returns>
        public int GetLevel(int exp)
        {
            return (exp / 1000) + 1;
        }

        /// <summary>
        /// Gets the experience required for <paramref name="level"/>.
        /// </summary>
        /// <param name="level">The level to check.</param>
        /// <returns>Required experience.</returns>
        public int GetExperience(int level)
        {
            return (level - 1) * 1000;
        }
    }
}
