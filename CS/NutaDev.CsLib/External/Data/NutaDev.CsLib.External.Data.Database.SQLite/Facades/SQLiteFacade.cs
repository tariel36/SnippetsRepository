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

using NutaDev.CsLib.Data.Database.Models;
using NutaDev.CsLib.Maintenance.Exceptions.Factories;
using NutaDev.CsLib.Resources.Text.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace NutaDev.CsLib.External.Data.Database.SQLite.Facades
{
    /// <summary>
    /// Facade between application and SQLite connection.
    /// </summary>
    public class SQLiteFacade
    {
        public const string ArgumentPrefix = "@arg";

        /// <summary>
        /// Initializes a new instance of the <see cref="SQLiteFacade"/> class.
        /// </summary>
        /// <param name="databaseName">Name of the database.</param>
        /// <param name="databasePath">Full path to database.</param>
        /// <param name="version">SQLite version.</param>
        /// <param name="useUtf8Encoding">Whether use UTF8 encoding.</param>
        /// <param name="password">Password to database.</param>
        public SQLiteFacade(string databaseName, string databasePath, string version, bool useUtf8Encoding, string password)
        {
            DatabaseName = databaseName;
            DatabasePath = databasePath;
            Version = version;
            UseUtf8Encoding = useUtf8Encoding;
            DatabasePassword = password;

            ConnectionString = $"Data Source={FullFilePath};Version={Version};UTF8Encoding={(UseUtf8Encoding ? "True" : "False")};" + (string.IsNullOrWhiteSpace(password) ? string.Empty : $"Password={DatabasePassword}");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SQLiteFacade"/> class.
        /// </summary>
        /// <param name="connectionString">SQLite connection string.</param>
        public SQLiteFacade(string connectionString)
        {
            ConnectionString = connectionString;

            string[] strings = connectionString.Split(';');
            foreach (string str in strings)
            {
                if (str.ToUpper().Contains("DATA SOURCE"))
                {
                    string dsStr = str.Substring(str.IndexOf('=') + 1);
                    DatabaseName = Path.GetFileName(dsStr);
                    DatabasePath = dsStr.Substring(0, dsStr.LastIndexOf(DatabaseName, StringComparison.Ordinal) - 1);
                }
                else if (str.ToUpper().Contains("VERSION"))
                {
                    Version = str.Substring(str.IndexOf('=') + 1);
                }
                else if (str.ToUpper().Contains("UTF8ENCODING"))
                {
                    UseUtf8Encoding = str.Substring(str.IndexOf('=') + 1).ToUpper().Contains("TRUE");
                }
                else if (str.ToUpper().Contains("PASSWORD"))
                {
                    DatabasePassword = str.Substring(str.IndexOf('=') + 1);
                }
            }
        }

        /// <summary>
        /// Gets database name.
        /// </summary>
        public string DatabaseName { get; }

        /// <summary>
        /// Gets path to database.
        /// </summary>
        public string DatabasePath { get; }

        /// <summary>
        /// Gets version of database.
        /// </summary>
        public string Version { get; }

        /// <summary>
        /// Indicates whether use UTF8 encoding.
        /// </summary>
        public bool UseUtf8Encoding { get; }

        /// <summary>
        /// Gets password to database.
        /// </summary>
        public string DatabasePassword { get; }

        /// <summary>
        /// Gets connection string.
        /// </summary>
        public string ConnectionString { get; }

        /// <summary>
        /// Gets full path to database's file.
        /// </summary>
        public string FullFilePath
        {
            get { return DatabasePath + "/" + DatabaseName; }
        }

        /// <summary>
        /// Creates a database file.
        /// </summary>
        /// <returns>True if success, false otherwise.</returns>
        public bool CreateDatabase()
        {
            SQLiteConnection.CreateFile(FullFilePath);
            return true;
        }

        /// <summary>
        /// Performs database query that returns no result.
        /// </summary>
        /// <param name="sql">SQL query.</param>
        /// <param name="args">Query arguments.</param>
        public void Query(string sql, params object[] args)
        {
            using (SQLiteConnection connection = OpenConnection())
            {
                Query(connection, sql, args);
            }
        }

        /// <summary>
        /// Performs database query that returns no result.
        /// </summary>
        /// <param name="trans">Transaction object.</param>
        /// <param name="sql">SQL query.</param>
        /// <param name="args">Query arguments.</param>
        public void Query(SQLiteTransaction trans, string sql, params object[] args)
        {
            if (trans == null) { throw ExceptionFactory.ArgumentNullException(nameof(trans)); }

            if (!string.IsNullOrWhiteSpace(sql))
            {
                Query(trans.Connection, sql, args);
            }
        }

        /// <summary>
        /// Performs database query that should return result.
        /// </summary>
        /// <param name="sql">SQL query.</param>
        /// <param name="args">Query arguments.</param>
        /// <returns>The query result or null if error occured.</returns>
        public QueryResult QueryForResult(string sql, params object[] args)
        {
            using (SQLiteConnection connection = OpenConnection())
            {
                return QueryForResult(connection, sql, args);
            }
        }

        /// <summary>
        /// Performs database query that should return result.
        /// </summary>
        /// <param name="trans">Transaction object.</param>
        /// <param name="sql">SQL query.</param>
        /// <param name="args">Query arguments.</param>
        /// <returns>The query result or null if error occured.</returns>
        public QueryResult QueryForResult(SQLiteTransaction trans, string sql, params object[] args)
        {
            if (trans == null) { throw ExceptionFactory.ArgumentNullException(nameof(trans)); }

            QueryResult result = null;

            if (!string.IsNullOrWhiteSpace(sql))
            {
                result = QueryForResult(trans.Connection, sql, args);
            }

            return result;
        }

        /// <summary>
        /// Checks if database exists.
        /// </summary>
        /// <returns>True if database exists.</returns>
        public bool DatabaseExists()
        {
            bool result = false;

            if (File.Exists(FullFilePath))
            {
                using (SQLiteConnection connection = OpenConnection())
                {
                    if (connection != null)
                    {
                        result = true;
                    }

                    CloseConnection(connection);
                }
            }

            return result;
        }

        /// <summary>
        /// Opens transactioned connection.
        /// </summary>
        /// <returns>Object that holds transactioned connection.</returns>
        public Transactions.SQLiteTransaction OpenTransaction()
        {
            SQLiteConnection connection = OpenConnection();
            return new Transactions.SQLiteTransaction(connection);
        }

        /// <summary>
        /// Closes the transactioned connection.
        /// </summary>
        /// <param name="transaction">Transaction to close.</param>
        public void CloseTransaction(Transactions.SQLiteTransaction transaction)
        {
            if (transaction != null)
            {
                transaction.CommitAndClose();
                transaction.Dispose();
            }
        }

        /// <summary>
        /// Performs database query that returns no result.
        /// </summary>
        /// <param name="connection">SQLite connection object.</param>
        /// <param name="sql">SQL query.</param>
        /// <param name="args">Query arguments.</param>
        private void Query(SQLiteConnection connection, string sql, params object[] args)
        {
            if (!string.IsNullOrWhiteSpace(sql))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(sql, connection))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql;

                    if (args != null)
                    {
                        for (int i = 0; i < args.Length; ++i)
                        {
                            cmd.Parameters.Add(CreateParameter(i, args[i]));
                        }
                    }

                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Performs database query that should return result.
        /// </summary>
        /// <param name="connection">SQLite connection object.</param>
        /// <param name="sql">SQL query.</param>
        /// <param name="args">Query arguments.</param>
        /// <returns>The query result or null if error occured.</returns>
        private QueryResult QueryForResult(SQLiteConnection connection, string sql, params object[] args)
        {
            QueryResult result = null;

            if (!string.IsNullOrWhiteSpace(sql))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(sql, connection))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql;

                    if (args != null)
                    {
                        for (int i = 0; i < args.Length; ++i)
                        {
                            cmd.Parameters.Add(CreateParameter(i, args[i]));
                        }
                    }

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        result = ReadResult(reader);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Opens connection with database.
        /// </summary>
        /// <returns>Connection with database.</returns>
        /// <exception cref="InvalidOperationException">If failed to open the connection.</exception>
        private SQLiteConnection OpenConnection()
        {
            SQLiteConnection connection = new SQLiteConnection(ConnectionString).OpenAndReturn();

            if (connection == null)
            {
                throw ExceptionFactory.Create<InvalidOperationException>(Text.FailedToOpenConnection);
            }

            return connection;
        }

        /// <summary>
        /// Closes the connection with database.
        /// </summary>
        /// <param name="connection">Connection to close.</param>
        private void CloseConnection(SQLiteConnection connection)
        {
            if (connection != null)
            {
                connection.Close();
                connection.Dispose();
            }
        }

        /// <summary>
        /// Reads the query result.
        /// </summary>
        /// <param name="reader">The reader to use.</param>
        /// <returns>The <see cref="QueryResult"/> object that contains query result.</returns>
        private QueryResult ReadResult(SQLiteDataReader reader)
        {
            List<QueryRow> rows = new List<QueryRow>();

            while (reader.Read())
            {
                NameValueCollection values = reader.GetValues();
                List<QueryCell> row = new List<QueryCell>();

                for (int i = 0; i < values.Count; ++i)
                {
                    row.Add(new QueryCell(values.GetKey(i), values.Get(i)));
                }

                rows.Add(new QueryRow(row));
            }

            return new QueryResult(rows);
        }

        /// <summary>
        /// Creates <see cref="SQLiteParameter"/> instance.
        /// </summary>
        /// <param name="index">Index of a parameter.</param>
        /// <param name="value">Parameter's value</param>
        /// <returns>Instance of a <see cref="SQLiteParameter"/>.</returns>
        private SQLiteParameter CreateParameter(int index, object value)
        {
            return new SQLiteParameter(ArgumentPrefix + index, value);
        }
    }
}
