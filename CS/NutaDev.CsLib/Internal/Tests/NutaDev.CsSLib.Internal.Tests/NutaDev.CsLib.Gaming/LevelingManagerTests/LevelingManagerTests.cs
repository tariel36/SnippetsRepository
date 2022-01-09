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
using NutaDev.CsLib.Gaming.Leveling.Managers;

namespace NutaDev.CsSLib.Internal.Tests.NutaDev.CsLib.Gaming.LevelingManagerTests
{
    [TestFixture]
    public class LevelingManagerTests
    {
        [Test]
        [TestCase(0, 50, false)]
        [TestCase(0, 150, true)]
        public void TestHasLeveledUp(int prev, int curr, bool expected)
        {
            // Arrange
            LevelingManager manager = new LevelingManager(new CubicLevelingTableFormula());

            // Act
            bool actual = manager.HasLeveledUp(prev, curr);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase(0, 1)]
        [TestCase(50, 1)]
        [TestCase(150, 2)]
        [TestCase(200, 3)]
        [TestCase(205, 3)]
        public void TestGetLevel(int exp, int expected)
        {
            // Arrange
            LevelingManager manager = new LevelingManager(new CubicLevelingTableFormula());

            // Act
            int actual = manager.GetLevel(exp);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase(50, 0)]
        [TestCase(150, 100)]
        [TestCase(250, 200)]
        public void TestGetLevelMinimumExp(int exp, int expected)
        {
            // Arrange
            LevelingManager manager = new LevelingManager(new CubicLevelingTableFormula());

            // Act
            int actual = manager.GetLevelMinimumExp(exp);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase(50, 100)]
        [TestCase(150, 200)]
        [TestCase(6450, 9300)]
        public void TestGetLevelMaximumExp(int exp, int expected)
        {
            // Arrange
            LevelingManager manager = new LevelingManager(new CubicLevelingTableFormula());

            // Act
            int actual = manager.GetLevelMaximumExp(exp);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase(50, 50)]
        [TestCase(150, 50)]
        [TestCase(6450, 50)]
        public void TestGetCurrentProgressExp(int exp, int expected)
        {
            // Arrange
            LevelingManager manager = new LevelingManager(new CubicLevelingTableFormula());

            // Act
            int actual = manager.GetCurrentProgressExp(exp);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase(1, 100)]
        [TestCase(2, 100)]
        [TestCase(9, 2900)]
        public void TestGetExperienceRequiredForNextLevel(int currLevel, int expected)
        {
            // Arrange
            LevelingManager manager = new LevelingManager(new CubicLevelingTableFormula());

            // Act
            int actual = manager.GetExperienceRequiredForNextLevel(currLevel);

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
