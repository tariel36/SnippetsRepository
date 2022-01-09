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
using System.Collections.Generic;
using System.Text;

namespace NutaDev.CsLib.External.Data.Database.SQLite.Builders
{
    /// <summary>
    /// SQL query builder for SQLite dbms.
    /// </summary>
    public class SQLiteQueryBuilder
    {
        /// <summary>
        /// The format used to create keys that are used as placeholders until the query is built.
        /// </summary>
        private static readonly string KeyFormat = Guid.NewGuid().ToString().Replace('-', '_') + "__KEY_{0}__";

        /// <summary>
        /// The constructed query.
        /// </summary>
        private readonly StringBuilder _query;

        /// <summary>
        /// Dictionary that hold placeholder keys and coresponding values.
        /// </summary>
        private readonly Dictionary<string, string> _argumentDict;

        /// <summary>
        /// Backing field for the argument prefix used with command parameters for SQLite.
        /// </summary>
        private string _argumentPrefix;

        /// <summary>
        /// Initializes the instance of the <see cref="SQLiteQueryBuilder"/> class.
        /// </summary>
        public SQLiteQueryBuilder()
            : this(string.Empty)
        {
        }

        /// <summary>
        /// Initializes the instance of the <see cref="SQLiteQueryBuilder"/> class.
        /// </summary>
        /// <param name="argumentPrefix">Argument prefix used with command parameters for SQLite.</param>
        public SQLiteQueryBuilder(string argumentPrefix)
        {
            _argumentPrefix = argumentPrefix;

            _query = new StringBuilder();
            _argumentDict = new Dictionary<string, string>();
        }

        /// <summary>
        /// Argument prefix used with command parameters for SQLite.
        /// </summary>
        public string ArgumentPrefix
        {
            get { return _argumentPrefix; }
            set { _argumentPrefix = value; }
        }

        /// <summary>
        /// Appends 'insert' keyword.
        /// </summary>
        public SQLiteQueryBuilder Insert
        {
            get
            {
                _query.Append(" INSERT ");
                return this;
            }
        }

        /// <summary>
        /// Appends 'delete' keyword.
        /// </summary>
        public SQLiteQueryBuilder Delete
        {
            get
            {
                _query.Append(" DELETE ");
                return this;
            }
        }

        /// <summary>
        /// Appends 'ASC' keyword.
        /// </summary>
        public SQLiteQueryBuilder Asc
        {
            get
            {
                _query.Append(" ASC ");
                return this;
            }
        }

        /// <summary>
        /// Appends 'DESC' keyword.
        /// </summary>
        public SQLiteQueryBuilder Desc
        {
            get
            {
                _query.Append(" DESC ");
                return this;
            }
        }

        /// <summary>
        /// Appends custom query.
        /// </summary>
        /// <param name="builder">Query builder.</param>
        /// <param name="query">The custom query that will be appended.</param>
        /// <returns>Reference to <paramref name="builder"/>.</returns>
        public static SQLiteQueryBuilder operator +(SQLiteQueryBuilder builder, string query)
        {
            return builder.AppendCustomQuery(query);
        }

        /// <summary>
        /// I don't know what this is supposed to do.
        /// </summary>
        /// <param name="builder1">The left side builder.</param>
        /// <param name="builder2">The right side builder.</param>
        /// <returns><paramref name="builder1"/> if is equal to <paramref name="builder2"/>, otherwise null.</returns>
        public static SQLiteQueryBuilder operator +(SQLiteQueryBuilder builder1, SQLiteQueryBuilder builder2)
        {
            return builder1 == builder2
                ? builder1
                : null;
        }

        /// <summary>
        /// Appends 'select' keyword with <paramref name="what"/> clause.
        /// </summary>
        /// <param name="what">What will be selected.</param>
        /// <returns>Reference to itself.</returns>
        public SQLiteQueryBuilder Select(string what)
        {
            _query.Append($" SELECT {AppendArgument(what)} ");

            return this;
        }

        /// <summary>
        /// Appends 'from' keyword.
        /// </summary>
        /// <param name="where">The tables names.</param>
        /// <returns>Reference to itself.</returns>
        public SQLiteQueryBuilder From(string where)
        {
            _query.Append($" FROM {AppendArgument(where)} ");

            return this;
        }

        /// <summary>
        /// Appends 'where' keyword.
        /// </summary>
        /// <param name="whereClause">The 'where' clause.</param>
        /// <returns>Reference to itself.</returns>
        public SQLiteQueryBuilder Where(string whereClause)
        {
            _query.Append($" WHERE {AppendArgument(whereClause)} ");

            return this;
        }

        /// <summary>
        /// Appends 'into' keyword.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="where">The list of columns.</param>
        /// <returns>Reference to itself.</returns>
        public SQLiteQueryBuilder Into(string table, params string[] where)
        {
            _query.Append($" INTO {table} ({AppendArgument(string.Join(", ", where))}) ");

            return this;
        }

        /// <summary>
        /// Appends 'values' keyword and batch fo values.
        /// </summary>
        /// <param name="args">The values to append.</param>
        /// <returns>Reference to itself.</returns>
        public SQLiteQueryBuilder Values(params string[] args)
        {
            _query.Append($" VALUES ({AppendArgument(string.Join(", ", args))}) ");

            return this;
        }

        /// <summary>
        /// Appends 'values' keyword and batch fo values.
        /// </summary>
        /// <param name="args">The values to append.</param>
        /// <returns>Reference to itself.</returns>
        public SQLiteQueryBuilder ValuesBatch(params string[] args)
        {
            _query.Append($" VALUES ({AppendArgument(string.Join(", ", args))}), ");

            return this;
        }

        /// <summary>
        /// Appends 'update' keyword.
        /// </summary>
        /// <param name="what">The tables to update.</param>
        /// <returns>Reference to itself.</returns>
        public SQLiteQueryBuilder Update(string what)
        {
            _query.Append($" UPDATE {AppendArgument(what)} ");

            return this;
        }

        /// <summary>
        /// Appends 'set' keyword. The columns and values must be provided in pairs, ex. [0] column1, [1] value1.
        /// </summary>
        /// <param name="args">The collection of column/value pairs.</param>
        /// <returns>Reference to itself.</returns>
        /// <exception cref="ArgumentException">If no arguments have been provided or there's key/value mismatch.</exception>
        public SQLiteQueryBuilder Set(params KeyValuePair<string, object>[] args)
        {
            if (args.Length == 0)
            {
                return this;
            }

            _query.Append($" SET ");

            for (int i = 0; i < args.Length; ++i)
            {
                _query.Append($"{AppendArgument(args[i].Key)} = {AppendArgument(args[i].Value.ToString())}");

                if ((i + 1) < args.Length)
                {
                    _query.Append(", ");
                }
            }

            _query.Append(" ");

            return this;
        }

        /// <summary>
        /// Appends 'in' keyword.
        /// </summary>
        /// <param name="args">The collection of values.</param>
        /// <returns>Reference to itself.</returns>
        public SQLiteQueryBuilder In(params string[] args)
        {
            _query.Append($" IN ({AppendArgument(string.Join(", ", args))})");

            return this;
        }

        /// <summary>
        /// Appends 'join' keyword.
        /// </summary>
        /// <param name="what">The join clause.</param>
        /// <returns>Reference to itself.</returns>
        public SQLiteQueryBuilder Join(string what)
        {
            _query.Append($" JOIN {AppendArgument(what)} ");

            return this;
        }

        /// <summary>
        /// Appends 'left join' keywords.
        /// </summary>
        /// <param name="what">The join clause.</param>
        /// <returns>Reference to itself.</returns>
        public SQLiteQueryBuilder LeftJoin(string what)
        {
            _query.Append($" LEFT JOIN {AppendArgument(what)} ");

            return this;
        }

        /// <summary>
        /// Appends 'right join' keywords.
        /// </summary>
        /// <param name="what">The join clause.</param>
        /// <returns>Reference to itself.</returns>
        public SQLiteQueryBuilder RightJoin(string what)
        {
            _query.Append($" RIGHT JOIN {AppendArgument(what)} ");

            return this;
        }

        /// <summary>
        /// Appends the 'on' keyword.
        /// </summary>
        /// <param name="onClause">The 'on' clause.</param>
        /// <returns>Reference to itself.</returns>
        public SQLiteQueryBuilder On(string onClause)
        {
            _query.Append($" ON {AppendArgument(onClause)} ");

            return this;
        }

        /// <summary>
        /// Appends the 'ORDER BY' keyword.
        /// </summary>
        /// <param name="column">The column name.</param>
        /// <returns>Reference to itself.</returns>
        public SQLiteQueryBuilder OrderBy(string column)
        {
            _query.Append($" ORDER BY {AppendArgument(column)} ");

            return this;
        }

        /// <summary>
        /// Appends the 'LIMIT' keyword.
        /// </summary>
        /// <param name="limit">The limit.</param>
        /// <returns>Reference to itself.</returns>
        public SQLiteQueryBuilder Limit(int limit)
        {
            _query.Append($" LIMIT {AppendArgument(limit.ToString())} ");

            return this;
        }

        /// <summary>
        /// Appends the <paramref name="query"/> if <paramref name="func"/> returns true.
        /// </summary>
        /// <param name="func">The logic check function.</param>
        /// <param name="query">The query to append.</param>
        /// <returns>Reference to itself.</returns>
        public SQLiteQueryBuilder AppendQueryIf(Func<bool> func, string query)
        {
            if (func())
            {
                _query.Append($" {AppendArgument(query)} ");
            }

            return this;
        }

        /// <summary>
        /// Flushes the builder.
        /// </summary>
        public void Clear()
        {
            _query.Clear();
        }

        /// <summary>
        /// Returns contents of this builder as string.
        /// </summary>
        /// <param name="clear">If true, the builder will be flushed.</param>
        /// <returns>Built query.</returns>
        public string ToString(bool clear)
        {
            string value = RemoveRedundantWhiteCharacters(_query).ToString().Trim();

            if (clear)
            {
                Clear();
            }

            value = ReplaceArugmentsWithValues(value);

            return value;
        }

        /// <summary>
        /// Returns contents of this builder as string. The builder is not flushed.
        /// </summary>
        /// <returns>Built query.</returns>
        public override string ToString()
        {
            return ToString(false);
        }

        /// <summary>
        /// Replaces argument placeholders with coresponding values.
        /// </summary>
        /// <param name="query">The query where placeholders will be replaced.</param>
        /// <returns>The query with actual values.</returns>
        private string ReplaceArugmentsWithValues(string query)
        {
            foreach (KeyValuePair<string, string> arg in _argumentDict)
            {
                query = query.Replace(arg.Key, arg.Value);
            }

            return query;
        }

        /// <summary>
        /// Appends custom query to builder.
        /// </summary>
        /// <param name="query">The custom query.</param>
        /// <returns>Reference to itself.</returns>
        private SQLiteQueryBuilder AppendCustomQuery(string query)
        {
            _query.Append($" {AppendArgument(query)} ");

            return this;
        }

        /// <summary>
        /// Appends argument to dictionary.
        /// </summary>
        /// <param name="value">Value to append.</param>
        /// <returns>The new key.</returns>
        private string AppendArgument(string value)
        {
            string key = string.Format(KeyFormat, _argumentDict.Count);

            _argumentDict.Add(key, value);

            return key;
        }

        /// <summary>
        /// Removes redundant white characters from provided <see cref="StringBuilder"/>.
        /// </summary>
        /// <param name="sb"><see cref="StringBuilder"/> from where redundant white characters will be removed.</param>
        /// <returns>Reference to <paramref name="sb"/>.</returns>
        private StringBuilder RemoveRedundantWhiteCharacters(StringBuilder sb)
        {
            for (int i = 0; i < sb.Length; ++i)
            {
                if (sb[i] <= 32 || sb[i] >= 127)
                {
                    ++i;
                    while (i < sb.Length && (sb[i] <= 32 || sb[i] >= 127))
                    {
                        sb.Remove(i++, 1);
                    }
                }
            }

            return sb;
        }
    }
}
