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
using NutaDev.CsLib.Data.Database.Models;
using NutaDev.CsLib.External.Data.Database.SQLite.Serializers;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Globalization;
using System.Text;

namespace NutaDev.CsSLib.Internal.Tests.NutaDev.CsLib.External.Data.Database.SQLite.SimpleSerializerTests
{
    [TestFixture]
    public class SimpleSerializerTests
    {

        [Test]
        public void Test_001_CreateInsertQuery()
        {
            // Arrange
            int pkValue = 0;
            int intValue = 123;
            double doubleValue = 321.0;
            string strValue = "str";
            DateTime dtValue = new DateTime(1993, 5, 1);
            SimpleSerializer ss = new SimpleSerializer();
            TestObject to = new TestObject(pkValue, intValue, doubleValue, strValue, dtValue);
            string tableName = "TESTS_OBJECT";
            SimpleSerializer.QueryType queryType = SimpleSerializer.QueryType.Insert;
            string pkName = "PK_FIELD";

            string expectedQuery = $"INSERT INTO {tableName} (DB_INT_FIELD,DB_DOUBLE_FIELD,DB_STRING_FIELD,DB_DATETIME_FIELD) VALUES (@arg0,@arg1,@arg2,@arg3);";

            // Act
            SQLiteCommand cmd = ss.ObjectToCommand(to, tableName, queryType, pkName);

            // Assert
            Assert.AreEqual(expectedQuery, cmd.CommandText);
        }

        [Test]
        public void Test_002_CreateUpdatetQuery()
        {
            // Arrange
            int pkValue = 0;
            int intValue = 123;
            double doubleValue = 321.0;
            string strValue = "str";
            DateTime dtValue = new DateTime(1993, 5, 1);
            SimpleSerializer ss = new SimpleSerializer();
            TestObject to = new TestObject(pkValue, intValue, doubleValue, strValue, dtValue);
            string tableName = "TESTS_OBJECT";
            SimpleSerializer.QueryType queryType = SimpleSerializer.QueryType.Update;
            string pkName = "PK_FIELD";

            string expectedQuery = $"UPDATE {tableName} SET DB_INT_FIELD = @arg0,DB_DOUBLE_FIELD = @arg1,DB_STRING_FIELD = @arg2,DB_DATETIME_FIELD = @arg3 WHERE {pkName} = @arg4;";

            // Act
            SQLiteCommand cmd = ss.ObjectToCommand(to, tableName, queryType, pkName);

            // Assert
            Assert.AreEqual(expectedQuery, cmd.CommandText);
        }

        [Test]
        public void Test_003_CreateDeleteQuery()
        {
            // Arrange
            int pkValue = 0;
            int intValue = 123;
            double doubleValue = 321.0;
            string strValue = "str";
            DateTime dtValue = new DateTime(1993, 5, 1);
            SimpleSerializer ss = new SimpleSerializer();
            TestObject to = new TestObject(pkValue, intValue, doubleValue, strValue, dtValue);
            string tableName = "TESTS_OBJECT";
            SimpleSerializer.QueryType queryType = SimpleSerializer.QueryType.Delete;
            string pkName = "PK_FIELD";

            string expectedQuery = $"DELETE FROM {tableName} WHERE {pkName} = @arg0;";

            // Act
            SQLiteCommand cmd = ss.ObjectToCommand(to, tableName, queryType, pkName);

            // Assert
            Assert.AreEqual(expectedQuery, cmd.CommandText);
        }

        [Test]
        public void Test_004_CreateResolveInsertQuery()
        {
            // Arrange
            int pkValue = 0;
            int intValue = 123;
            double doubleValue = 321.0;
            string strValue = "str";
            DateTime dtValue = new DateTime(1993, 5, 1);
            SimpleSerializer ss = new SimpleSerializer();
            TestObject to = new TestObject(pkValue, intValue, doubleValue, strValue, dtValue);
            string tableName = "TESTS_OBJECT";
            SimpleSerializer.QueryType queryType = SimpleSerializer.QueryType.Resolve;
            string pkName = "PK_FIELD";

            string expectedQuery = $"INSERT INTO {tableName} (DB_INT_FIELD,DB_DOUBLE_FIELD,DB_STRING_FIELD,DB_DATETIME_FIELD) VALUES (@arg0,@arg1,@arg2,@arg3);";

            // Act
            SQLiteCommand cmd = ss.ObjectToCommand(to, tableName, queryType, pkName);

            // Assert
            Assert.AreEqual(expectedQuery, cmd.CommandText);
        }

        [Test]
        public void Test_005_CreateResolveUpdateQuery()
        {
            // Arrange
            int pkValue = 1;
            int intValue = 123;
            double doubleValue = 321.0;
            string strValue = "str";
            DateTime dtValue = new DateTime(1993, 5, 1);
            SimpleSerializer ss = new SimpleSerializer();
            TestObject to = new TestObject(pkValue, intValue, doubleValue, strValue, dtValue);
            string tableName = "TESTS_OBJECT";
            SimpleSerializer.QueryType queryType = SimpleSerializer.QueryType.Resolve;
            string pkName = "PK_FIELD";

            string expectedQuery = $"UPDATE {tableName} SET DB_INT_FIELD = @arg0,DB_DOUBLE_FIELD = @arg1,DB_STRING_FIELD = @arg2,DB_DATETIME_FIELD = @arg3 WHERE {pkName} = @arg4;";

            // Act
            SQLiteCommand cmd = ss.ObjectToCommand(to, tableName, queryType, pkName);

            // Assert
            Assert.AreEqual(expectedQuery, cmd.CommandText);
        }

        [Test]
        public void Test_006_SelectObject()
        {
            // Arrange
            const string dateTimeFormat = "yyyy-MM-dd HH\\:mm\\:ss\\.fff";
            int pkValue = 1;
            int intValue = 123;
            double doubleValue = 321.0;
            string strValue = "str";
            DateTime dtValue = new DateTime(1993, 5, 1);
            SimpleSerializer ss = new SimpleSerializer();
            TestObject expectedObject = new TestObject(pkValue, intValue, doubleValue, strValue, dtValue);
            TestObject resultObject;
            QueryRow qRow = new QueryRow(new[]
            {
                new QueryCell("PK_FIELD", pkValue.ToString()),
                new QueryCell("DB_INT_FIELD", intValue.ToString()),
                new QueryCell("DB_DOUBLE_FIELD", doubleValue.ToString(CultureInfo.InvariantCulture)),
                new QueryCell("DB_STRING_FIELD", strValue),
                new QueryCell("DB_DATETIME_FIELD", dtValue.ToString(dateTimeFormat)),
            });

            // Act
            resultObject = ss.QueryRowToObject<TestObject>(qRow);

            // Assert
            Assert.AreEqual(expectedObject.PK_FIELD, resultObject.PK_FIELD);
            Assert.AreEqual(expectedObject.DB_DATETIME_FIELD, resultObject.DB_DATETIME_FIELD);
            Assert.AreEqual(expectedObject.DB_DOUBLE_FIELD, resultObject.DB_DOUBLE_FIELD);
            Assert.AreEqual(expectedObject.DB_INT_FIELD, resultObject.DB_INT_FIELD);
            Assert.AreEqual(expectedObject.DB_STRING_FIELD, resultObject.DB_STRING_FIELD);
        }

        private class TestObject
        {
            public int PK_FIELD;
            public int DB_INT_FIELD;
            public double DB_DOUBLE_FIELD;
            public string DB_STRING_FIELD;
            public DateTime DB_DATETIME_FIELD;

            public TestObject()
            {
            }

            public TestObject(int pk, int intField, double doubleField, string strField, DateTime dateTime)
            {
                PK_FIELD = pk;
                DB_INT_FIELD = intField;
                DB_DOUBLE_FIELD = doubleField;
                DB_STRING_FIELD = strField;
                DB_DATETIME_FIELD = dateTime;
            }
        }
    }
}
