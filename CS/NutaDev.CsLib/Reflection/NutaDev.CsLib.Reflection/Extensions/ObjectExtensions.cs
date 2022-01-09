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
using System.Linq;
using System.Reflection;
using System.Text;

namespace NutaDev.CsLib.Reflection.Extensions
{
    /// <summary>
    /// Provides extension methods for <see cref="object"/> type.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Checks if given object implements <see cref="IEnumerable"/> interface.
        /// </summary>
        /// <param name="o">The object.</param>
        /// <returns>True if object implements the <see cref="IEnumerable"/> interface.</returns>
        public static bool IsEnumerable(this object o)
        {
            return (o?.GetType())?.IsEnumerable() ?? false;
        }

        /// <summary>
        /// Checks if given object implements <see cref="ICollection"/> interface.
        /// </summary>
        /// <param name="o">The object.</param>
        /// <returns>True if object implements the <see cref="ICollection"/> interface.</returns>
        public static bool IsCollection(this object o)
        {
            return (o?.GetType())?.IsCollection() ?? false;
        }

        /// <summary>
        /// Wrapps the <see cref="TypeExtensions.GetFieldType"/> method.
        /// </summary>
        /// <param name="o">The object.</param>
        /// <param name="fieldName">Field name.</param>
        /// <param name="underlyingTypeIfNullable"></param>
        /// <returns>The <see cref="Type"/> of given field or null if <paramref name="o"/> is null.</returns>
        public static Type GetFieldType(this object o, string fieldName, bool underlyingTypeIfNullable)
        {
            if (o == null) { return null; }

            return o.GetType().GetFieldType(fieldName);
        }

        /// <summary>
        /// Sets instance field value.
        /// </summary>
        /// <param name="o">The object.</param>
        /// <param name="fieldName">Field name.</param>
        /// <param name="value">The value to set.</param>
        /// <returns>True if field has been set, false otherwise.</returns>
        public static bool SetFieldValue(this object o, string fieldName, object value)
        {
            if (o == null) { return false; }

            return o.GetType().SetFieldValue(o, fieldName, value);
        }

        /// <summary>
        /// Gets field value.
        /// </summary>
        /// <param name="o">The object.</param>
        /// <param name="fieldName">Field name.</param>
        /// <returns>Field's value from <paramref name="o"/> or null if <paramref name="o"/> is null.</returns>
        public static object GetFieldValue(this object o, string fieldName)
        {
            return o?.GetType().GetFieldValue(o, fieldName);
        }

        /// <summary>
        /// Returns name of the object.
        /// </summary>
        /// <param name="o">The object.</param>
        /// <returns>Type's name of the object or null.</returns>
        public static string GetTypeName(this object o)
        {
            return o?.GetType().Name;
        }

        /// <summary>
        /// Gets instance property value.
        /// </summary>
        /// <param name="o">The object.</param>
        /// <param name="propertyName">Property name.</param>
        /// <returns>Property value.</returns>
        public static object GetPropertyValue(this object o, string propertyName)
        {
            return o?.GetType().GetPropertyValue(o, propertyName);
        }

        /// <summary>
        /// Sets instance property value.
        /// </summary>
        /// <param name="o">The object.</param>
        /// <param name="propertyName">Property name.</param>
        /// <param name="value">The value to set.</param>
        /// <returns>True if property has been set, false otherwise.</returns>
        public static bool SetPropertyValue(this object o, string propertyName, object value)
        {
            return o?.GetType().SetPropertyValue(o, propertyName, value) ?? false;
        }

        /// <summary>
        /// Invokes instance method of given name with given arguments. No validation is performed.
        /// </summary>
        /// <param name="o">The object.</param>
        /// <param name="methodName">Method to invoke.</param>
        /// <param name="args">Method's arguments.</param>
        /// <returns>Method's result.</returns>
        public static object InvokeMethod(this object o, string methodName, object[] args)
        {
            return o?.GetType().GetMethod(methodName)?.Invoke(o, args);
        }

        /// <summary>
        /// Returns dictionary of all instance properties name/value pairs in given object.
        /// </summary>
        /// <param name="o">The object.</param>
        /// <returns>Dictionary of property name/value pairs.</returns>
        public static Dictionary<string, object> ToPropertyNameValueDictionary(this object o)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            if (o != null)
            {
                PropertyInfo[] properties = o.GetType().GetAllInstanceProperties();

                foreach (PropertyInfo property in properties)
                {
                    result.Add(property.Name, o.GetPropertyValue(property.Name));
                }
            }

            return result;
        }

        /// <summary>
        /// Returns names of all instance properties in given object.
        /// </summary>
        /// <param name="o">The object.</param>
        /// <returns><see cref="List{T}"/> of all instance properties.</returns>
        public static List<string> GetAllInstancePropertyNames(this object o)
        {
            return o?.GetType().GetAllInstanceProperties().Select(x => x.Name).ToList();
        }

        /// <summary>
        /// Returns names of all instance fields in given object.
        /// </summary>
        /// <param name="o">The object.</param>
        /// <returns><see cref="List{T}"/> of all instance fields.</returns>
        public static List<string> GetAllInstanceFieldNames(this object o)
        {
            return o?.GetType().GetAllInstanceFields().Select(x => x.Name).ToList();
        }

        /// <summary>
        /// Prints object tree.
        /// </summary>
        /// <param name="obj">Object to print.</param>
        /// <param name="indent">The indentation.</param>
        /// <returns>Printed object.</returns>
        public static string PrintObject(this object obj, string indent = "")
        {
            StringBuilder sb = new StringBuilder();

            if (obj != null)
            {
                Type objType = obj.GetType();

                if (objType.Namespace == "System")
                {
                    return string.Empty;
                }

                sb.AppendLine($"{indent}{objType.Name}");

                foreach (FieldInfo fieldInfo in objType.GetAllStaticFields())
                {
                    object val = fieldInfo.GetValue(obj);
                    sb.AppendLine($"{indent}    Field: {fieldInfo.Name}: {val}");
                    sb.Append(PrintObject(val, indent + "    "));
                }

                foreach (PropertyInfo propertyInfo in objType.GetAllStaticProperties())
                {
                    object val = propertyInfo.GetValue(obj);
                    sb.AppendLine($"{indent}    Property: {propertyInfo.Name}: {val}");
                    sb.Append(PrintObject(val, indent + "    "));
                }

                foreach (FieldInfo fieldInfo in objType.GetAllInstanceFields())
                {
                    object val = fieldInfo.GetValue(obj);
                    sb.AppendLine($"{indent}    Field: {fieldInfo.Name}: {val}");
                    sb.Append(PrintObject(val, indent + "    "));
                }

                foreach (PropertyInfo propertyInfo in objType.GetAllInstanceProperties())
                {
                    object val = propertyInfo.GetValue(obj);
                    sb.AppendLine($"{indent}    Property: {propertyInfo.Name}: {val}");
                    sb.Append(PrintObject(val, indent + "    "));
                }
            }

            return sb.ToString();
        }
    }
}
