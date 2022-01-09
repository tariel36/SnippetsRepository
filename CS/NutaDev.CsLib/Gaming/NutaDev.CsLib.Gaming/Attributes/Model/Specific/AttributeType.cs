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

using NutaDev.CsLib.Gaming.Attributes.Model.Abstract;

namespace NutaDev.CsLib.Gaming.Attributes.Model.Specific
{
    /// <summary>
    /// A class that represents attribute type.
    /// </summary>
    /// <typeparam name="T">Actual type of attribute type.</typeparam>
    public class AttributeType<T>
        : IAttributeType
        where T: struct
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeType{T}"/> class.
        /// </summary>
        /// <param name="value">Actual value that indicates the attribute type.</param>
        public AttributeType(T value)
        {
            Value = value;
        }

        /// <summary>
        /// Gets or sets the actual value that indicates the attribute type.
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// Checks equality.
        /// </summary>
        /// <param name="type">Another type.</param>
        /// <returns>True if types are equal.</returns>
        public bool Equals(IAttributeType type)
        {
            if (type is AttributeType<T> tType)
            {
                return Equals(tType.Value, Value);
            }

            return false;
        }

        /// <summary>
        /// Creates copy of instance.
        /// </summary>
        /// <returns>The copy.</returns>
        public IAttributeType Copy()
        {
            return new AttributeType<T>(Value);
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">Object to compare.</param>
        /// <returns>True if object is equal.</returns>
        public override bool Equals(object obj)
        {
            if (obj is IAttributeType type)
            {
                return Equals(type);
            }

            return base.Equals(obj);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>Object hashcode.</returns>
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        /// <summary>
        /// Returns the fully qualified type name of this instance.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
