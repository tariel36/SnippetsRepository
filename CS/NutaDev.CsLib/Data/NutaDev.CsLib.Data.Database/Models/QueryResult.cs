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
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace NutaDev.CsLib.Data.Database.Models
{
    /// <summary>
    /// Result of database query.
    /// </summary>
    [DebuggerDisplay("Count = {" + nameof(Count) + "}")]
    public class QueryResult
        : IEnumerable
    {
        /// <summary>
        /// List of all query rows.
        /// </summary>
        private readonly List<QueryRow> _rows;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryResult"/> class.
        /// </summary>
        /// <param name="rows">Collection of QueryColumn objects.</param>
        public QueryResult(IEnumerable<QueryRow> rows)
        {
            _rows = rows?.ToList() ?? new List<QueryRow>();
        }

        /// <summary>
        /// Gets count of rows.
        /// </summary>
        public int Count
        {
            get { return _rows.Count; }
        }

        /// <summary>
        /// Gets row under selected index.
        /// </summary>
        /// <param name="idx">Index of row.</param>
        /// <returns>Row under selected index.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Index is less than 0 or equal or greater than Count.</exception>
        public QueryRow this[int idx]
        {
            get
            {
                if (idx < 0 || idx >= _rows.Count)
                {
                    throw ExceptionFactory.ArgumentOutOfRangeException(nameof(idx));
                }

                return _rows[idx];
            }
        }

        /// <summary>
        /// Gets column's value from selected row and column.
        /// </summary>
        /// <param name="idx">Index of row.</param>
        /// <param name="columnName">Name of column.</param>
        /// <returns>Value of column from selected row.</returns>
        /// <exception cref="ArgumentNullException">ColumnName is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Index is less than 0 or equal or greater than Count.</exception>
        public string this[int idx, string columnName]
        {
            get
            {
                if (string.IsNullOrWhiteSpace(columnName)) { throw ExceptionFactory.ArgumentNullException(nameof(columnName)); }

                if (idx < 0 || idx >= _rows.Count)
                {
                    throw ExceptionFactory.ArgumentOutOfRangeException(nameof(idx));
                }

                return _rows[idx][columnName];
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through collection of rows.
        /// </summary>
        /// <returns>Enumerator that iterates through collection of rows.</returns>
        public IEnumerator GetEnumerator()
        {
            return _rows.GetEnumerator();
        }

        /// <summary>
        /// Returns new <see cref="List{QueryRow}"/>.
        /// </summary>
        /// <returns>List of cloned query rows.</returns>
        public List<QueryRow> ToList()
        {
            return _rows.ToList();
        }
    }
}
