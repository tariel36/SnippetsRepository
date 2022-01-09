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
using System.Dynamic;

namespace NutaDev.CsLib.Reflection.Structures
{
    /// <summary>
    /// <para>This class works similar way to anonymous types (ex. var type = { A = 10, B = 12 }). It allows you to add and remove properties on runtime
    /// without recompiling the source. The properties work like normal properties, so if you use the dynamic keyword you can easily bind
    /// to them in WPF.</para>
    /// <para></para>
    /// <para>dynamic myObject = AnonymousObject.Create();</para>
    /// <para>myObject.RandomProperty1 = 10;</para>
    /// <para>myObject.RandomProperty2 = myObject.RandomProperty1;</para>
    /// </summary>
    public class AnonymousObject
        : DynamicObject
    {
        /// <summary>
        /// Dictionary of properties and their values.
        /// </summary>
        private readonly Dictionary<string, object> _dynamicProperties;

        /// <summary>
        /// Initializes instance of the <see cref="AnonymousObject"/> class.
        /// </summary>
        private AnonymousObject()
            : this(new Dictionary<string, object>())
        {
        }

        /// <summary>
        /// Initializes instance of the <see cref="AnonymousObject"/> class.
        /// </summary>
        /// <param name="dynamicProperties">Dictionary of properties.</param>
        private AnonymousObject(Dictionary<string, object> dynamicProperties)
        {
            _dynamicProperties = dynamicProperties;
        }

        /// <summary>
        /// Creates <see cref="AnonymousObject"/> object and returns it as dynamic type.
        /// </summary>
        /// <returns>The <see cref="AnonymousObject"/> class instance.</returns>
        public static dynamic Create()
        {
            return Create(new Dictionary<string, object>());
        }

        /// <summary>
        /// Creates <see cref="AnonymousObject"/> object and returns it as dynamic type.
        /// </summary>
        /// <param name="dynamicProperties">Dictionary of properties.</param>
        /// <returns>The <see cref="AnonymousObject"/> class instance.</returns>
        public static dynamic Create(Dictionary<string, object> dynamicProperties)
        {
            return new AnonymousObject(dynamicProperties);
        }

        /// <summary>
        /// Enumerates the registered property names.
        /// </summary>
        /// <returns><see cref="IEnumerable{T}"/> of property names.</returns>
        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return _dynamicProperties.Keys;
        }

        /// <summary>
        /// Tries to add the property if not exists yet and returns it's value.
        /// </summary>
        /// <param name="binder">The binder.</param>
        /// <param name="result">The value.</param>
        /// <returns>True.</returns>
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return TryGetProperty(binder.Name, out result, binder.ReturnType, false, null);
        }

        /// <summary>
        /// Tries to get the property with provided <paramref name="name"/>. If property does not exists, adds it.
        /// </summary>
        /// <param name="name">Property name.</param>
        /// <param name="value">Value to get.</param>
        /// <returns>True if value has been set, false otherwise.</returns>
        public bool TryGetMember(string name, out object value)
        {
            return TryGetProperty(name, out value, typeof(object), false, null);
        }

        /// <summary>
        /// Tries to get the property with provided <paramref name="name"/>. If property does not exists, adds it.
        /// </summary>
        /// <typeparam name="TValue">Value type.</typeparam>
        /// <param name="name">Property name.</param>
        /// <returns>Actual value if exists and is valid type, otherwise default value.</returns>
        public TValue TryGetValue<TValue>(string name)
        {
            return TryGetValue(name, default(TValue), true);
        }

        /// <summary>
        /// Tries to get the property with provided <paramref name="name"/>. If property does not exists, adds it.
        /// </summary>
        /// <typeparam name="TValue">Value type.</typeparam>
        /// <param name="name">Property name.</param>
        /// <param name="defaultValue">Default value.</param>
        /// <param name="tryConvert">Indicates whether type conversion should be performed.</param>
        /// <returns>Actual value if exists and is valid type, otherwise default value.</returns>
        public TValue TryGetValue<TValue>(string name, TValue defaultValue, bool tryConvert)
        {
            object value;

            TryGetProperty(name, out value, typeof(TValue), tryConvert, defaultValue);

            return (TValue)value;
        }

        /// <summary>
        /// Adds and sets the value of the property.
        /// </summary>
        /// <param name="binder">The binder.</param>
        /// <param name="value">The value.</param>
        /// <returns>True.</returns>
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            return TrySetMember(binder.Name, value);
        }

        /// <summary>
        /// Tries to set the property with provided <paramref name="name"/>. If property does not exists, adds it.
        /// </summary>
        /// <param name="name">Property name.</param>
        /// <param name="value">Value to set.</param>
        /// <returns>True if value has been set, false otherwise.</returns>
        public bool TrySetMember(string name, object value)
        {
            TryAddProperty(name);

            _dynamicProperties[name] = value;

            return true;
        }

        /// <summary>
        /// Removes property and returns its value.
        /// </summary>
        /// <param name="name">Property to remove.</param>
        /// <returns>Property value.</returns>
        public object RemoveMember(string name)
        {
            object value = null;

            if (_dynamicProperties.ContainsKey(name))
            {
                value = _dynamicProperties[name];
                _dynamicProperties.Remove(name);
            }

            return value;
        }

        /// <summary>
        /// Tries to add property to dictionary. If property already exists, it's not added and value is not replaced.
        /// </summary>
        /// <param name="name">Property name.</param>
        /// <param name="value">Property value.</param>
        private void TryAddProperty(string name, object value = null)
        {
            if (!_dynamicProperties.ContainsKey(name))
            {
                _dynamicProperties.Add(name, value);
            }
        }

        /// <summary>
        /// Tries to get the property with provided <paramref name="name"/>. If property does not exists, adds it.
        /// </summary>
        /// <param name="name">Property name.</param>
        /// <param name="value">Value to get.</param>
        /// <param name="type">Value type.</param>
        /// <param name="tryConvert">Indicates whether type conversion should be performed.</param>
        /// <param name="defaultValue">Default value.</param>
        /// <returns>Actual value if exists and is valid type, otherwise default value.</returns>
        private bool TryGetProperty(string name, out object value, Type type, bool tryConvert, object defaultValue)
        {
            bool result = false;

            TryAddProperty(name);
            value = _dynamicProperties[name];

            if (tryConvert)
            {
                try
                {
                    value = Convert.ChangeType(value, type);
                }
                catch
                {
                    value = defaultValue;
                }
            }
            else
            {
                result = true;
            }

            return result;
        }
    }
}
