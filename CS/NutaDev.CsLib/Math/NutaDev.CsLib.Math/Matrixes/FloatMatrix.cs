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

using NutaDev.CsLib.Maintenance.Exceptions.Factories;
using NutaDev.CsLib.Resources.Text.Exceptions;
using System;

namespace NutaDev.CsLib.Math.Matrixes
{
    /// <summary>
    /// Template specjalization for <see cref="GenericMatrix{T}"/> class template.
    /// </summary>
    public class FloatMatrix
        : GenericMatrix<float>
    {
        /// <summary>
        /// Initializes a new instance of the GenericMatrix class.
        /// </summary>
        /// <param name="columns">Number of columns.</param>
        /// <param name="rows">Number of rows.</param>
        public FloatMatrix(int columns, int rows)
            : base(columns, rows)
        {
        }

        /// <summary>
        /// Initializes a new instance of the GenericMatrix class. Dimensions are based on passed values.
        /// </summary>
        /// <param name="arr">The values.</param>
        public FloatMatrix(float[][] arr)
            : base(arr)
        {
        }

        /// <summary>
        /// Initializes a new instance of the GenerixMatrix class. Copies values from passed matrix.
        /// </summary>
        /// <param name="other">Matrix where values will be copied from.</param>
        public FloatMatrix(FloatMatrix other)
            : base(other)
        {
        }

        /// <summary>
        /// Creates transposed copy of this matrix.
        /// </summary>
        /// <returns>Transposed matrix or this if Rows count is 0.</returns>
        public new FloatMatrix Transpose()
        {
            return Transpose(this, new FloatMatrix(Rows, Columns));
        }

        /// <summary>
        /// Overrides the + operator.
        /// </summary>
        /// <param name="m1">Left operand.</param>
        /// <param name="m2">Right operand.</param>
        /// <returns>New GenericMatrix which contains result of addition.</returns>
        /// <exception cref="ArgumentException">If matrixes have different dimensions.</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="m1"/> or <paramref name="m2"/> is null.</exception>
        public static FloatMatrix operator +(FloatMatrix m1, FloatMatrix m2)
        {
            if (m1 == null) { throw ExceptionFactory.ArgumentNullException(nameof(m1)); }
            if (m2 == null) { throw ExceptionFactory.ArgumentNullException(nameof(m2)); }

            if (m1.Columns != m2.Columns || m1.Rows != m2.Rows)
            {
                throw ExceptionFactory.Create<ArgumentException>(Text.BothMatrixesMustHaveEqualSize_0_x_1_And_2_x_3_, m1.Columns, m1.Rows, m2.Columns, m2.Rows);
            }

            FloatMatrix result = new FloatMatrix(m1.Columns, m2.Rows);

            for (int i = 0; i < m1.Rows; ++i)
            {
                for (int j = 0; j < m1.Columns; ++j)
                {
                    result[i, j] = m1[i, j] + m2[i, j];
                }
            }

            return result;
        }

        /// <summary>
        /// Overrides the - operator.
        /// </summary>
        /// <param name="m1">Left operand.</param>
        /// <param name="m2">Right operand.</param>
        /// <returns>New GenericMatrix which contains result of subtraction.</returns>
        /// <exception cref="ArgumentException">If matrixes have different dimensions.</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="m1"/> or <paramref name="m2"/> is null.</exception>
        public static FloatMatrix operator -(FloatMatrix m1, FloatMatrix m2)
        {
            if (m1 == null) { throw ExceptionFactory.ArgumentNullException(nameof(m1)); }
            if (m2 == null) { throw ExceptionFactory.ArgumentNullException(nameof(m2)); }

            if (m1.Columns != m2.Columns || m1.Rows != m2.Rows)
            {
                throw ExceptionFactory.Create<ArgumentException>(Text.BothMatrixesMustHaveEqualSize_0_x_1_And_2_x_3_, m1.Columns, m1.Rows, m2.Columns, m2.Rows);
            }

            FloatMatrix result = new FloatMatrix(m1.Columns, m2.Rows);

            for (int i = 0; i < m1.Rows; ++i)
            {
                for (int j = 0; j < m1.Columns; ++j)
                {
                    result[i, j] = m1[i, j] - m2[i, j];
                }
            }

            return result;
        }

        /// <summary>
        /// Overrides the * operator.
        /// </summary>
        /// <param name="m1">Left operand.</param>
        /// <param name="m2">Right operand.</param>
        /// <returns>New GenericMatrix which contains result of multiplication.</returns>
        /// <exception cref="ArgumentException">If matrixes don't meet the m1.Rows == m2.Columns requirement.</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="m1"/> or <paramref name="m2"/> is null.</exception>
        public static FloatMatrix operator *(FloatMatrix m1, FloatMatrix m2)
        {
            if (m1 == null) { throw ExceptionFactory.ArgumentNullException(nameof(m1)); }
            if (m2 == null) { throw ExceptionFactory.ArgumentNullException(nameof(m2)); }

            if (m1.Rows != m2.Columns)
            {
                throw ExceptionFactory.Create<ArgumentException>(Text.Matrix_0_ColumnCount_1_MustBeEqualToMatrix_2_RowCount_3_, nameof(m2), m1.Columns, nameof(m1), m1.Rows);
            }

            FloatMatrix result = new FloatMatrix(m1.Rows, m2.Columns);

            for (int row = 0; row < m1.Rows; row++)
            {
                for (int col = 0; col < m1.Rows; col++)
                {
                    result[row, col] = 0.0f;

                    for (int inner = 0; inner < m1.Columns; inner++)
                    {
                        result[row, col] = result[row, col] + (m1[row, inner] * m2[inner, col]);
                    }
                }
            }

            return result;
        }
    }
}
