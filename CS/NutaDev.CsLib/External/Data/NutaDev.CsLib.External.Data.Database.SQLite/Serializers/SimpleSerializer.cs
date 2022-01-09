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
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;

namespace NutaDev.CsLib.External.Data.Database.SQLite.Serializers
{
    /// <summary>
    /// Simple object serialization and deserialization designed to work with sqlite and reflection.
    /// </summary>
    public class SimpleSerializer
    {
        /// <summary>
        /// The argument prefix for SQLiteCommand.
        /// </summary>
        private const string ArgPrefix = "@arg";

        /// <summary>
        /// <para>Parses query <paramref name="row"/> (<see cref="QueryRow"/>) to object of type <paramref name="{T}"/>.</para>
        /// <para>The <paramref name="{T}"/> must have public parameterless default constructor.</para>
        /// <para>Table columns names must be equal to class field names.</para>
        /// <para>Class fields must be instance fields.</para>
        /// </summary>
        /// <typeparam name="T">Type of result object.</typeparam>
        /// <param name="row">Data from database.</param>
        /// <returns>Parsed object.</returns>
        public T QueryRowToObject<T>(QueryRow row)
            where T : new()
        {
            if (row == null)
            {
                return default(T);
            }

            T result = new T();

            FieldInfo[] fields = typeof(T).GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (FieldInfo field in fields)
            {
                if (row.ContainsColumn(field.Name))
                {
                    ParseType(row, result, field, field.FieldType);
                }
            }

            return result;
        }

        /// <summary>
        /// <para>Converst object <paramref name="obj"/> of type <paramref name="{T}"/> into <see cref="SQLiteCommand"/>. The resulting command
        /// may be insert, update or delete command, set by <paramref name="queryType"/> parameter. <see cref="QueryType.Update"/>
        /// and <see cref="QueryType.Delete"/> requires the <paramref name="pkName"/> parameter to be set.
        /// For the <see cref="QueryType.Insert"/> the <paramref name="pkName"/> may be null, however it will
        /// be included into column list. The query type may be resolved by method (works only for update and insert).</para>
        /// <para>Table columns names must be equal to class field names.</para>
        /// </summary>
        /// <param name="obj">Object with data.</param>
        /// <param name="table">Target table's name.</param>
        /// <param name="queryType">Type of query.</param>
        /// <param name="pkName">Primary key name.</param>
        /// <returns>Prepared <see cref="SQLiteCommand"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">For invalid <paramref name="queryType"/> value or when cannot resolve the query type.</exception>
        public SQLiteCommand ObjectToCommand(object obj, string table, QueryType queryType, string pkName = null)
        {
            string sql = ObjectToQuery(obj, table, queryType, out _, pkName);

            SQLiteCommand cmd = new SQLiteCommand(sql);
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;

            FieldInfo[] fields = obj.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            if (queryType == QueryType.Delete)
            {
                cmd.Parameters.Add(new SQLiteParameter($"{ArgPrefix}0", fields.First(x => x.Name.Equals(pkName)).GetValue(obj)));
            }
            else
            {
                for (int i = 0; i < fields.Count(f => !f.Name.Equals(pkName)); ++i)
                {
                    cmd.Parameters.Add(new SQLiteParameter($"{ArgPrefix}{i}", fields[i].GetValue(obj)));
                }
            }

            return cmd;
        }

        /// <summary>
        /// <para>Converst object <paramref name="obj"/> of type <paramref name="{T}"/> into SQLite query. The resulting command
        /// may be insert, update or delete command, set by <paramref name="queryType"/> parameter. <see cref="QueryType.Update"/>
        /// and <see cref="QueryType.Delete"/> requires the <paramref name="pkName"/> parameter to be set.
        /// For the <see cref="QueryType.Insert"/> the <paramref name="pkName"/> may be null, however it will
        /// be included into column list. The query type may be resolved by method (works only for update and insert).</para>
        /// <para>Table columns names must be equal to class field names.</para>
        /// </summary>
        /// <param name="obj">Object with data.</param>
        /// <param name="table">Target table's name.</param>
        /// <param name="queryType">Type of query.</param>
        /// <param name="pkName">Primary key name.</param>
        /// <returns>Prepared SQL query.</returns>
        /// <exception cref="ArgumentOutOfRangeException">For invalid <paramref name="queryType"/> value or when cannot resolve the query type.</exception>
        public string ObjectToQuery(object obj, string table, QueryType queryType, string pkName = null)
        {
            return ObjectToQuery(obj, table, queryType, out _, pkName);
        }

        /// <summary>
        /// <para>Converst object <paramref name="obj"/> of type <paramref name="{T}"/> into SQLite query. The resulting command
        /// may be insert, update or delete command, set by <paramref name="queryType"/> parameter. <see cref="QueryType.Update"/>
        /// and <see cref="QueryType.Delete"/> requires the <paramref name="pkName"/> parameter to be set.
        /// For the <see cref="QueryType.Insert"/> the <paramref name="pkName"/> may be null, however it will
        /// be included into column list. The query type may be resolved by method (works only for update and insert).</para>
        /// <para>Table columns names must be equal to class field names.</para>
        /// </summary>
        /// <param name="obj">Object with data.</param>
        /// <param name="table">Target table's name.</param>
        /// <param name="queryType">Type of query.</param>
        /// <param name="fieldValues">Field values.</param>
        /// <param name="pkName">Primary key name.</param>
        /// <returns>Prepared SQL query.</returns>
        /// <exception cref="ArgumentOutOfRangeException">For invalid <paramref name="queryType"/> value or when cannot resolve the query type.</exception>
        public string ObjectToQuery(object obj, string table, QueryType queryType, out object[] fieldValues, string pkName = null)
        {
            string sql;

            FieldInfo[] fields = obj.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            if (queryType == QueryType.Resolve)
            {
                if (pkName != null)
                {
                    queryType = Convert.ToUInt64(fields.FirstOrDefault(f => f.Name.Equals(pkName))?.GetValue(obj) ?? 0) == 0
                              ? QueryType.Insert
                              : QueryType.Update;
                }
            }

            List<object> values = new List<object>();

            switch (queryType)
            {
                case QueryType.Insert:
                {
                    sql = $"INSERT INTO {table} ({string.Join(",", fields.Where(f => !f.Name.Equals(pkName)).Select(x => { values.Add(x.GetValue(obj)); return x.Name; }))}) VALUES ({string.Join(",", Enumerable.Range(0, fields.Count(f => !f.Name.Equals(pkName))).Select(x => $"{ArgPrefix}{x}"))});";
                    break;
                }
                case QueryType.Update:
                {
                    sql = $"UPDATE {table} SET {string.Join(",", Enumerable.Range(1, fields.Length - 1).Select(x => $"{fields[x].Name} = {ArgPrefix}{x - 1}"))} WHERE {pkName} = {ArgPrefix}{fields.Length - 1};";
                    values.AddRange(fields.Skip(1).Select(x => x.GetValue(obj)).Concat(new List<object>() { fields.First().GetValue(obj) }));
                    break;
                }
                case QueryType.Delete:
                {
                    sql = $"DELETE FROM {table} WHERE {pkName} = {ArgPrefix}0;";
                    values.Add(fields.First(x => x.Name.Equals(pkName)).GetValue(obj));
                    break;
                }
                default: throw ExceptionFactory.ArgumentOutOfRangeException(nameof(queryType));
            }

            fieldValues = values.ToArray();

            return sql;
        }

        /// <summary>
        /// Parses column value to provided type.
        /// </summary>
        /// <param name="row">The analyzed query row.</param>
        /// <param name="result">Object where field will be set.</param>
        /// <param name="field">Field to set.</param>
        /// <param name="type">Value type.</param>
        private void ParseType(QueryRow row, object result, FieldInfo field, Type type)
        {
            if (type.GetInterface(typeof(IConvertible).Name) != null)
            {
                if (type == typeof(bool))
                {
                    bool fieldValue = false;
                    string colValue = row[field.Name];
                    if (!string.IsNullOrWhiteSpace(colValue))
                    {
                        if (colValue.Any(c => !char.IsDigit(c)))
                        {
                            fieldValue = bool.Parse(colValue);
                        }
                        else
                        {
                            fieldValue = int.Parse(colValue) != 0;
                        }
                    }

                    field.SetValue(result, fieldValue);
                }
                else
                {
                    field.SetValue(result, Convert.ChangeType(row[field.Name], type));
                }
            }
            else if (type == typeof(Guid))
            {
                field.SetValue(result, Guid.Parse(row[field.Name]));
            }
            else if (type.IsGenericType)
            {
                if (string.IsNullOrEmpty(row[field.Name]))
                {
                    field.SetValue(result, null);
                }
                else
                {
                    ParseType(row, result, field, type.GenericTypeArguments[0]);
                }
            }
        }

        /// <summary>
        /// SQL query type.
        /// </summary>
        public enum QueryType
        {
            /// <summary>
            /// The method will try to resolve query type.
            /// </summary>
            Resolve = 0,

            /// <summary>
            /// The insert query.
            /// </summary>
            Insert = 1,

            /// <summary>
            /// The update query.
            /// </summary>
            Update = 2,

            /// <summary>
            /// The delete query.
            /// </summary>
            Delete = 3
        }
    }
}
