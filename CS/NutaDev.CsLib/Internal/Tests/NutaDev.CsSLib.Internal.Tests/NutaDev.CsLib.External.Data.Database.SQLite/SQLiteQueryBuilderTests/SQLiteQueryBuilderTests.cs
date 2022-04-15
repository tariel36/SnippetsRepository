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
using NutaDev.CsLib.External.Data.Database.SQLite.Builders;
using System.Collections.Generic;

namespace NutaDev.CsLib.Internal.Tests.NutaDev.CsLib.External.Data.Database.SQLite.SQLiteQueryBuilderTests
{
    [TestFixture]
    public class SQLiteQueryBuilderTests
    {
        [Test]
        public void Test_001_SimpleSelectQueryTest()
        {
            // Arrange
            string expectedQuery = "SELECT * FROM MY_TABLE WHERE ID_ITEM = 10";
            string resultQuery;
            SQLiteQueryBuilder qb = new SQLiteQueryBuilder();

            // Act
            resultQuery = qb.Select("*").From("MY_TABLE").Where("ID_ITEM = 10").ToString();

            // Assert
            Assert.AreEqual(expectedQuery, resultQuery);
        }

        [Test]
        public void Test_002_MediumSelectQueryTest()
        {
            // Arrange
            string expectedQuery = "SELECT * FROM MY_TABLE WHERE ID_ITEM = 10";
            string resultQuery;
            SQLiteQueryBuilder qb = new SQLiteQueryBuilder();

            // Act
            resultQuery = qb.Select("*").From("MY_TABLE").Where("ID_ITEM = 10").ToString();

            // Assert
            Assert.AreEqual(expectedQuery, resultQuery);
        }

        [Test]
        public void Test_003_InsertQueryTest()
        {
            // Arrange
            string expectedQuery = @"INSERT INTO MY_TABLE (ID_ITEM, ITEM_VALUE) VALUES (10, 'test')";
            string resultQuery;
            SQLiteQueryBuilder qb = new SQLiteQueryBuilder();

            // Act
            resultQuery = qb.Insert.Into("MY_TABLE", "ID_ITEM", "ITEM_VALUE").Values("10", "'test'").ToString();

            // Assert
            Assert.AreEqual(expectedQuery, resultQuery);
        }

        [Test]
        public void Test_004_UpdateQueryTest()
        {
            // Arrange
            string expectedQuery = "UPDATE MY_TABLE SET ITEM_VALUE = 'ABC' WHERE ID_ITEM = 10";
            string resultQuery;
            SQLiteQueryBuilder qb = new SQLiteQueryBuilder();

            // Act
            resultQuery = qb.Update("MY_TABLE").Set(new KeyValuePair<string, object>("ITEM_VALUE", "'ABC'")).Where("ID_ITEM = 10").ToString();

            // Assert
            Assert.AreEqual(expectedQuery, resultQuery);
        }

        [Test]
        public void Test_005_DeleteQueryTest()
        {
            // Arrange
            string expectedQuery = "DELETE FROM MY_TABLE WHERE ID_ITEM = 10";
            string resultQuery;
            SQLiteQueryBuilder qb = new SQLiteQueryBuilder();

            // Act
            resultQuery = qb.Delete.From("MY_TABLE").Where("ID_ITEM = 10").ToString();

            // Assert
            Assert.AreEqual(expectedQuery, resultQuery);
        }
    }
}
