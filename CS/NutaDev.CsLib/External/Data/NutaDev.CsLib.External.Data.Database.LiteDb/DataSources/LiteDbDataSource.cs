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

using LiteDB;
using NutaDev.CsLib.Data.Abstract.DataSources;
using NutaDev.CsLib.Maintenance.Exceptions.Delegates;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NutaDev.CsLib.External.Data.Database.LiteDb.DataSources
{
    /// <summary>
    /// <see cref="DataSource"/> that works with LiteDB.
    /// </summary>
    public class LiteDbDataSource
        : DataSource
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LiteDbDataSource"/> class.
        /// </summary>
        /// <param name="dbPath">Path to databse.</param>
        public LiteDbDataSource(string dbPath, ExceptionHandlerDelegate exceptionHandler = null)
        {
            Database = new LiteDatabase(dbPath);
            ExceptionHandler = exceptionHandler;
        }

        /// <summary>
        /// Gets or sets <see cref="LiteDatabase"/> instance.
        /// </summary>
        private LiteDatabase Database { get; set; }

        /// <summary>
        /// Gets the exception handler.
        /// </summary>
        private ExceptionHandlerDelegate ExceptionHandler { get; }

        /// <summary>
        /// Deletes object from database.
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <typeparam name="TK">Key type.</typeparam>
        /// <param name="item">Item to delete.</param>
        /// <returns>Count of deleted items.</returns>
        public override int Delete<T, TK>(T item)
        {
            return Database.GetCollection<T>(typeof(T).Name).DeleteMany(x => x.Id.Equals(item.Id));
        }

        /// <summary>
        /// Deletes all objects of given type from database.
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <typeparam name="TK">Key type.</typeparam>
        /// <returns>Count of deleted items.</returns>
        public override int DeleteAll<T, TK>()
        {
            int count = Database.GetCollection<T>(typeof(T).Name).Count();
            Database.DropCollection(typeof(T).Name);
            return count;
        }

        /// <summary>
        /// Gets item with <paramref name="id"/>.
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <typeparam name="TK">Key type.</typeparam>
        /// <param name="id">Object id.</param>
        /// <returns>Object with <paramref name="id"/>.</returns>
        public override T Get<T, TK>(TK id)
        {
            return Database.GetCollection<T>(typeof(T).Name).FindOne(x => x.Id.Equals(id));
        }

        /// <summary>
        /// Gets objects that fulfill <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <typeparam name="TK">Key type.</typeparam>
        /// <param name="predicate">Predicate to fulfill.</param>
        /// <returns>Objects that fullfilled <paramref name="predicate"/>.</returns>
        public override ICollection<T> Get<T, TK>(Func<T, bool> predicate)
        {
            return Database.GetCollection<T>(typeof(T).Name).FindAll().Where(predicate).ToList();
        }

        /// <summary>
        /// Sets object in database.
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <typeparam name="TK">Key type.</typeparam>
        /// <param name="toSet">Object to set.</param>
        /// <returns>True if object has been set, otherwise fdalse.</returns>
        public override bool Set<T, TK>(T toSet)
        {
            return Database.GetCollection<T>(typeof(T).Name).Upsert(toSet);
        }

        /// <summary>
        /// Disposes instance of the <see cref="LiteDbDataSource"/>.
        /// </summary>
        public override void Dispose()
        {
            try
            {
                if (Database != null)
                {
                    Database.Dispose();
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler?.Invoke(ex);
            }
            finally
            {
                Database = null;
            }
        }
    }
}
