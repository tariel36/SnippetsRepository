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

namespace NutaDev.CsLib.Internal.Tests.NutaDev.CsLib.Math.DoubleMatrixTests
{
    [TestFixture]
    public class DoubleMatrixTests
    {
        [Test]
        public void Test_001_TestBasicOperations()
        {
            // Arrange
            DoubleMatrix matrixLeft = new DoubleMatrix(new[] { new[] { 1.0, 2.0 }, new[] { 3.0, 4.0 } });
            DoubleMatrix matrixRight = new DoubleMatrix(new[] { new[] { 1.0, 2.0 }, new[] { 3.0, 4.0 } });
            double[] expectedAdd = { 2.0, 4.0, 6.0, 8.0 };
            double[] expectedSub = { 0.0, 0.0, 0.0, 0.0 };
            double[] expectedMul = { 7.0, 10.0, 15.0, 22.0 };

            DoubleMatrix addResult;
            DoubleMatrix subResult;
            DoubleMatrix mulResult;

            // Act
            addResult = matrixLeft + matrixRight;
            subResult = matrixLeft - matrixRight;
            mulResult = matrixLeft * matrixRight;

            // Assert
            Assert.AreEqual(expectedAdd, addResult.Select<double, double>(x => x).ToArray());
            Assert.AreEqual(expectedSub, subResult.Select<double, double>(x => x).ToArray());
            Assert.AreEqual(expectedMul, mulResult.Select<double, double>(x => x).ToArray());
        }

        [Test]
        public void Test_002_Transpose()
        {
            // Arrange
            DoubleMatrix inputMatrix = new DoubleMatrix(new[] { new[] { 1.0, 2.0 }, new[] { 3.0, 4.0 } });
            DoubleMatrix outputMatrix;
            double[][] expectedMatrix = { new[] { 1.0, 3.0 }, new[] { 2.0, 4.0 } };

            // Act
            outputMatrix = inputMatrix.Transpose();

            // Assert
            Assert.AreEqual(expectedMatrix, outputMatrix.ToArray());
        }
    }
}
