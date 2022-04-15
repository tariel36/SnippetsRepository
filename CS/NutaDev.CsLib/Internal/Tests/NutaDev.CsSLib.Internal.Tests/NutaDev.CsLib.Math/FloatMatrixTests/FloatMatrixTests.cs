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
using NutaDev.CsLib.Math.Matrixes;
using System.Linq;

namespace NutaDev.CsLib.Internal.Tests.NutaDev.CsLib.Math.FloatMatrixTests
{
    [TestFixture]
    public class FloatMatrixTests
    {
        [Test]
        public void Test_001_TestBasicOperations()
        {
            // Arrange
            FloatMatrix matrixLeft = new FloatMatrix(new[] { new[] { 1.0f, 2.0f }, new[] { 3.0f, 4.0f } });
            FloatMatrix matrixRight = new FloatMatrix(new[] { new[] { 1.0f, 2.0f }, new[] { 3.0f, 4.0f } });
            float[] expectedAdd = { 2.0f, 4.0f, 6.0f, 8.0f };
            float[] expectedSub = { 0.0f, 0.0f, 0.0f, 0.0f };
            float[] expectedMul = { 7.0f, 10.0f, 15.0f, 22.0f };

            FloatMatrix addResult;
            FloatMatrix subResult;
            FloatMatrix mulResult;

            // Act
            addResult = matrixLeft + matrixRight;
            subResult = matrixLeft - matrixRight;
            mulResult = matrixLeft * matrixRight;

            // Assert
            Assert.AreEqual(expectedAdd, addResult.Select<float, float>(x => x).ToArray());
            Assert.AreEqual(expectedSub, subResult.Select<float, float>(x => x).ToArray());
            Assert.AreEqual(expectedMul, mulResult.Select<float, float>(x => x).ToArray());
        }

        [Test]
        public void Test_002_Transpose()
        {
            // Arrange
            FloatMatrix inputMatrix = new FloatMatrix(new[] { new[] { 1.0f, 2.0f }, new[] { 3.0f, 4.0f } });
            FloatMatrix outputMatrix;
            float[][] expectedMatrix = { new[] { 1.0f, 3.0f }, new[] { 2.0f, 4.0f } };

            // Act
            outputMatrix = inputMatrix.Transpose();

            // Assert
            Assert.AreEqual(expectedMatrix, outputMatrix.ToArray());
        }
    }
}
