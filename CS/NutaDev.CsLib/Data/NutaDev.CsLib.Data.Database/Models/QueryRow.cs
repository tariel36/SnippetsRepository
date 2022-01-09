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
    /// Row of database query result.
    /// </summary>
    [DebuggerDisplay("Count = {" + nameof(Count) + "}")]
    public class QueryRow
        : IEnumerable
    {
        /// <summary>
        /// List of all columns (with values or not) in row.
        /// </summary>
        private readonly List<QueryCell> _cells;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryRow"/> class.
        /// </summary>
        /// <param name="cells">Collection of all columns in row.</param>
        public QueryRow(IEnumerable<QueryCell> cells)
        {
            _cells = cells?.ToList() ?? new List<QueryCell>();
        }

        /// <summary>
        /// Gets cells count.
        /// </summary>
        public int Count
        {
            get { return _cells.Count; }
        }

        /// <summary>
        /// Gets value of the column with name equal to provided key.
        /// </summary>
        /// <param name="column">Column's name.</param>
        /// <returns>Value of named column.</returns>
        /// <exception cref="ArgumentNullException">Key is null.</exception>
        /// <exception cref="InvalidOperationException">Key doesn't exist.</exception>
        public string this[string column]
        {
            get { return _cells.FirstOrDefault(x => ColumnNamesEqual(x.ColumnName, column))?.Value; }
        }

        /// <summary>
        /// Gets value of the column with selected index.
        /// </summary>
        /// <param name="idx">Selected index.</param>
        /// <returns>Value of chosen column.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Index is out of range.</exception>
        public string this[int idx]
        {
            get
            {
                if (idx < 0 || idx >= _cells.Count)
                {
                    throw ExceptionFactory.ArgumentOutOfRangeException(nameof(idx));
                }

                return _cells[idx].Value;
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through collection of columns.
        /// </summary>
        /// <returns>Enumerator that iterates through collection of columns.</returns>
        public IEnumerator GetEnumerator()
        {
            return _cells.GetEnumerator();
        }

        /// <summary>
        /// Checks if row contains column of provided name.
        /// </summary>
        /// <param name="name">Name of provided name.</param>
        /// <returns>True if row contains column. False otherwise.</returns>
        public bool ContainsColumn(string name)
        {
            return _cells.Any(x => ColumnNamesEqual(x.ColumnName, name));
        }

        /// <summary>
        /// Compares two column names with invariant culture and ignoring case.
        /// </summary>
        /// <param name="left">First name.</param>
        /// <param name="right">Second name.</param>
        /// <returns>True if names are equal, otherwise false.</returns>
        private bool ColumnNamesEqual(string left, string right)
        {
            return string.Equals(left, right, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
