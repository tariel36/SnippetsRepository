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
using System.Collections;
using System.Collections.Generic;

namespace NutaDev.CsLib.Math.Matrixes
{
    /// <summary>
    /// Represents the matrix. Implements basic matrix operations. Based on Array&lt;T&gt;.
    /// </summary>
    /// <typeparam name="T">Values type.</typeparam>
    public class GenericMatrix<T>
        : IEnumerable<T>
        , IEnumerable<T[]>
    {
        /// <summary>
        /// Array of elements.
        /// </summary>
        private readonly T[][] _arr;

        /// <summary>
        /// Initializes a new instance of the GenericMatrix class.
        /// </summary>
        /// <param name="columns">Number of columns.</param>
        /// <param name="rows">Number of rows.</param>
        public GenericMatrix(int columns, int rows)
        {
            Columns = columns;
            Rows = rows;

            _arr = new T[Rows][];
            for (int i = 0; i < Rows; ++i)
            {
                _arr[i] = new T[Columns];
            }
        }

        /// <summary>
        /// Initializes a new instance of the GenericMatrix class. Dimensions are based on passed values.
        /// </summary>
        /// <param name="arr">The values.</param>
        public GenericMatrix(T[][] arr)
        {
            Columns = arr[0].Length;
            Rows = arr.Length;

            _arr = new T[Rows][];
            for (int i = 0; i < Rows; ++i)
            {
                _arr[i] = new T[Columns];

                for (int j = 0; j < Columns; ++j)
                {
                    _arr[i][j] = arr[i][j];
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the GenerixMatrix class. Copies values from passed matrix.
        /// </summary>
        /// <param name="other">Matrix where values will be copied from.</param>
        public GenericMatrix(GenericMatrix<T> other)
            : this(other._arr)
        {
        }

        /// <summary>
        /// Number of columns.
        /// </summary>
        public int Columns { get; private set; }

        /// <summary>
        /// Number of rows.
        /// </summary>
        public int Rows { get; private set; }

        /// <summary>
        /// Creates transposed copy of this matrix.
        /// </summary>
        /// <returns>Transposed matrix or this if Rows count is 0.</returns>
        public GenericMatrix<T> Transpose()
        {
            return Transpose(this, new GenericMatrix<T>(Rows, Columns));
        }

        /// <summary>
        /// Access element at [row,column].
        /// </summary>
        /// <param name="row">The row number.</param>
        /// <param name="column">The column number.</param>
        /// <returns>Element at [row,column].</returns>
        public T this[int row, int column]
        {
            get
            {
                return _arr[row][column];
            }
            set
            {
                _arr[row][column] = value;
            }
        }

        /// <summary>
        /// Enumerates the rows of matrix.
        /// </summary>
        /// <returns>Subsequent rows.</returns>
        IEnumerator<T[]> IEnumerable<T[]>.GetEnumerator()
        {
            foreach (T[] arr in _arr)
            {
                yield return arr;
            }
        }

        /// <summary>
        /// Enumerates the elements of matrix. Enumerates each from left to right (0 .. n - 1) row starting at index 0.
        /// </summary>
        /// <returns>Subsequent elements.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            foreach (T[] arr in _arr)
            {
                foreach (T x in arr)
                {
                    yield return x;
                }
            }
        }

        /// <summary>Returns an enumerator that iterates through a collection.</summary>
        /// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
        /// <filterpriority>2</filterpriority>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Converts <see cref="GenericMatrix{T}"/> to <see cref="Array"/> of <typeparamref name="T"/>.
        /// </summary>
        /// <returns>Array of values equal to <see cref="GenericMatrix{T}"/>.</returns>
        public T[][] ToArray()
        {
            T[][] matrix = new T[Rows][];

            for (int i = 0; i < Rows; ++i)
            {
                matrix[i] = new T[Columns];

                for (int j = 0; j < Columns; ++j)
                {
                    matrix[i][j] = _arr[i][j];
                }
            }

            return matrix;
        }

        /// <summary>
        /// Transposes matrix from <paramref name="source"/> to <paramref name="matrix"/>;
        /// </summary>
        /// <typeparam name="TK">Matrix type.</typeparam>
        /// <param name="source">Source matrix.</param>
        /// <param name="matrix">Target matrix.</param>
        /// <returns><paramref name="matrix"/> or <paramref name="source"/> if <see cref="Rows"/> == 0.</returns>
        protected static TK Transpose<TK>(TK source, TK matrix)
            where TK : GenericMatrix<T>
        {
            if (source.Rows == 0)
            {
                return source;
            }

            for (int i = 0; i < matrix.Rows; ++i)
            {
                for (int j = 0; j < matrix.Columns; ++j)
                {
                    matrix[j, i] = source[i, j];
                }
            }

            return matrix;
        }
    }
}
