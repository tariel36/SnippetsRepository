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

namespace NutaDev.CsLib.Reflection.Extensions
{
    /// <summary>
    /// This class provides extension methods for <see cref="Type"/> class.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Checks if provided type implements <see cref="IEnumerable"/> interface.
        /// </summary>
        /// <param name="type">Type to check.</param>
        /// <returns>True if <see cref="Type"/> implements the <see cref="IEnumerable"/> interface, false otherwise.</returns>
        public static bool IsEnumerable(this Type type)
        {
            return type?.GetInterface("IEnumerable") != null;
        }

        /// <summary>
        /// Checks if provided type implements <see cref="IsCollection"/> interface.
        /// </summary>
        /// <param name="type">Type to check.</param>
        /// <returns>True if <see cref="Type"/> implements the <see cref="ICollection"/> interface, false otherwise.</returns>
        public static bool IsCollection(this Type type)
        {
            return type?.GetInterface("ICollection") != null;
        }

        /// <summary>
        /// Gets default value of the provided <paramref name="type"/>.
        /// </summary>
        /// <param name="type">Type to check.</param>
        /// <returns>Default value of <paramref name="type"/>.</returns>
        public static object GetDefault(this Type type)
        {
            return type != null && type.IsValueType
                    ? Activator.CreateInstance(type)
                    : null
                ;
        }

        /// <summary>
        /// Returns all non static properties. Public and nonpublic. This is an extension method for <see cref="Type"/>.
        /// </summary>
        /// <param name="type">Type to reflect.</param>
        /// <returns>Array of instance properties.</returns>
        public static PropertyInfo[] GetAllInstanceProperties(this Type type)
        {
            return type?.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic) ?? new PropertyInfo[0];
        }

        /// <summary>
        /// Returns all static properties of given type. This is an extension method for <see cref="Type"/>.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>Array of found <see cref="PropertyInfo"/>.</returns>
        public static PropertyInfo[] GetAllStaticProperties(this Type type)
        {
            return type?.GetProperties(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic) ?? new PropertyInfo[0];
        }

        /// <summary>
        /// Returns all properties of given type. This is an extension method for <see cref="Type"/>.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>Array of found <see cref="PropertyInfo"/>.</returns>
        public static PropertyInfo[] GetAllProperties(this Type type)
        {
            return new List<PropertyInfo>(GetAllInstanceProperties(type).ToList().Intersect(GetAllStaticProperties(type).ToList())).ToArray();
        }

        /// <summary>
        /// Returns type of property with given <paramref name="propertyName"/> name. This method checks every type of property. This is an extension method
        /// for <see cref="Type"/>.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="propertyName">Property name to check.</param>
        /// <returns>Property type or default(<see cref="Type"/>).</returns>
        public static Type GetPropertyType(this Type type, string propertyName)
        {
            return type?.GetAllProperties().FirstOrDefault(x => string.Compare(x.Name, propertyName, StringComparison.Ordinal) == 0)?.PropertyType;
        }

        /// <summary>
        /// Returns value of instance property <paramref name="propertyName"/> in <paramref name="obj"/> object. This is an extension property for <see cref="Type"/>.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="obj">Object to check.</param>
        /// <param name="propertyName">Property to check.</param>
        /// <returns>The value (that may be null) or null if property hasn't been found.</returns>
        public static object GetPropertyValue(this Type type, object obj, string propertyName)
        {
            PropertyInfo prop = type?.GetAllInstanceProperties().FirstOrDefault(p => string.Compare(p.Name, propertyName, StringComparison.Ordinal) == 0);

            if (prop != null && obj != null)
            {
                return prop.GetValue(obj);
            }

            return null;
        }

        /// <summary>
        /// Attempts to set instance property value for given object <paramref name="obj"/>. This is an extension method for <see cref="Type"/>.
        /// Property names are compared with <see cref="String.Compare(string,string,StringComparison)"/> method.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="obj">Object where property value will be set.</param>
        /// <param name="propertyName">Property to set.</param>
        /// <param name="value">The value to set.</param>
        /// <returns>True if property has been set, false otherwise.</returns>
        public static bool SetPropertyValue(this Type type, object obj, string propertyName, object value)
        {
            PropertyInfo prop = type?.GetAllInstanceProperties().FirstOrDefault(p => string.Compare(p.Name, propertyName, StringComparison.Ordinal) == 0);

            if (prop != null && prop.SetMethod != null && obj != null)
            {
                prop.SetValue(obj, value);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns all non static fields. Public and nonpublic. This is an extension method for <see cref="Type"/>.
        /// </summary>
        /// <param name="type">Type to reflect.</param>
        /// <returns>Array of instance fields.</returns>
        public static FieldInfo[] GetAllInstanceFields(this Type type)
        {
            return type?.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic) ?? new FieldInfo[0];
        }

        /// <summary>
        /// Returns all static fields of given type. This is an extension method for <see cref="Type"/>.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>Array of found <see cref="FieldInfo"/>.</returns>
        public static FieldInfo[] GetAllStaticFields(this Type type)
        {
            return type?.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic) ?? new FieldInfo[0];
        }

        /// <summary>
        /// Returns all fields of given type. This is an extension method for <see cref="Type"/>.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>Array of found <see cref="FieldInfo"/>.</returns>
        public static FieldInfo[] GetAllFields(this Type type)
        {
            return new List<FieldInfo>(GetAllInstanceFields(type).ToList().Intersect(GetAllStaticFields(type).ToList())).ToArray();
        }

        /// <summary>
        /// Returns type of field with given <paramref name="fieldName"/> name. This method checks every type of field. This is an extension method
        /// for <see cref="Type"/>.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="fieldName">Field name to check.</param>
        /// <returns>Field type or default(<see cref="Type"/>).</returns>
        public static Type GetFieldType(this Type type, string fieldName)
        {
            return type?.GetField(fieldName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)?.FieldType;
        }

        /// <summary>
        /// Returns value of instance field <paramref name="fieldName"/> in <paramref name="obj"/> object. This is an extension field for <see cref="Type"/>.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="obj">Object to check.</param>
        /// <param name="fieldName">Field to check.</param>
        /// <returns>The value (that may be null) or null if field hasn't been found.</returns>
        public static object GetFieldValue(this Type type, object obj, string fieldName)
        {
            FieldInfo field = type.GetAllInstanceFields().FirstOrDefault(p => string.Compare(p.Name, fieldName, StringComparison.Ordinal) == 0);

            if (field != null && obj != null)
            {
                return field.GetValue(obj);
            }

            return type?.GetDefault();
        }

        /// <summary>
        /// Attempts to set instance field value for given object <paramref name="obj"/>. This is an extension method for <see cref="Type"/>.
        /// Field names are compared with <see cref="String.Compare(string,string,StringComparison)"/> method.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="obj">Object where field value will be set.</param>
        /// <param name="fieldName">Field to set.</param>
        /// <param name="value">The value to set.</param>
        /// <returns>True if field has been set, false otherwise.</returns>
        public static bool SetFieldValue(this Type type, object obj, string fieldName, object value)
        {
            FieldInfo field = type.GetAllInstanceFields().FirstOrDefault(p => string.Compare(p.Name, fieldName, StringComparison.Ordinal) == 0);

            if (field != null
                && obj != null
                && ((field.FieldType.IsValueType && value != null && string.Equals(field.FieldType.FullName, value.GetType().FullName))
                    || (!field.FieldType.IsValueType && (value == null || string.Equals(field.FieldType.FullName, value.GetType().FullName)))))
            {
                field.SetValue(obj, value);

                return true;
            }

            return false;
        }
    }
}
