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
using System.Data.SQLite;

namespace NutaDev.CsLib.External.Data.Database.SQLite.Transactions
{
    /// <summary>
    /// Wrapper for <see cref="System.Data.SQLite.SQLiteTransaction"/>.
    /// </summary>
    public class SQLiteTransaction
        : IDisposable
    {
        /// <summary>
        /// Connection to SQLite.
        /// </summary>
        private readonly SQLiteConnection _sqliteConnection;

        /// <summary>
        /// The <see cref="System.Data.SQLite.SQLiteTransaction"/> object.
        /// </summary>
        private readonly System.Data.SQLite.SQLiteTransaction _transaction;

        /// <summary>
        /// Initializes instance of the <see cref="SQLiteTransaction"/> class.
        /// </summary>
        /// <param name="sqliteConnection">Connection to SQLite.</param>
        internal SQLiteTransaction(SQLiteConnection sqliteConnection)
        {
            _sqliteConnection = sqliteConnection;
            _transaction = _sqliteConnection.BeginTransaction();
        }

        /// <summary>
        /// Gets connection to SQLite.
        /// </summary>
        internal SQLiteConnection Connection
        {
            get { return _sqliteConnection; }
        }

        /// <summary>
        /// Commits transaction and closes connection with SQLite.
        /// </summary>
        internal void CommitAndClose()
        {
            _transaction.Commit();
            _sqliteConnection.Close();
        }

        /// <summary>
        /// Disposes object instance.
        /// </summary>
        public void Dispose()
        {
            _transaction.Dispose();
            _sqliteConnection.Dispose();
        }
    }
}
