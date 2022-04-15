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

using NUnit.Framework;
using NutaDev.CsLib.Gaming.Leveling.Formulas.Specific;

namespace NutaDev.CsLib.Internal.Tests.NutaDev.CsLib.Gaming.CubicLevelingTableFormulaTests
{
    [TestFixture]
    public class CubicLevelingTableFormulaTests
    {
        [Test]
        [TestCase(1, 0)]
        [TestCase(2, 100)]
        [TestCase(10, 9300)]
        [TestCase(100, 15694800)]
        [TestCase(500, int.MaxValue)]
        public void TestGetExperience(int level, int expectedExp)
        {
            // Arrange
            CubicLevelingTableFormula formula = new CubicLevelingTableFormula();

            // Act
            int actualExp = formula.GetExperience(level);

            // Assert
            Assert.AreEqual(expectedExp, actualExp);
        }

        [Test]
        [TestCase(0, 1)]
        [TestCase(50, 1)]
        [TestCase(9300, 10)]
        [TestCase(15694800, 100)]
        [TestCase(2058474800, 500)]
        public void TestGetLevel(int exp, int expectedLevel)
        {
            // Arrange
            CubicLevelingTableFormula formula = new CubicLevelingTableFormula();

            // Act
            int actualLevel = formula.GetLevel(exp);

            // Assert
            Assert.AreEqual(expectedLevel, actualLevel);
        }
    }
}
