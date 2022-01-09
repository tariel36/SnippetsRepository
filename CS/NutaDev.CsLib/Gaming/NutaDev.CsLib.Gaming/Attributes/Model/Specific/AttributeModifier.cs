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
    /// The attribute modifier.
    /// </summary>
    public class AttributeModifier
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeModifier"/> class.
        /// </summary>
        public AttributeModifier()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeModifier"/> class.
        /// </summary>
        /// <param name="type">Attribute type.</param>
        /// <param name="source">Attribute modifier source.</param>
        /// <param name="value">Attribute value.</param>
        public AttributeModifier(IAttributeType type, object source, int value)
        {
            Type = type;
            Source = source;
            Value = value;
        }

        /// <summary>
        /// Gets or sets attribute type.
        /// </summary>
        public IAttributeType Type { get; set; }

        /// <summary>
        /// Gets ors sets attribute modifier source.
        /// </summary>
        public object Source { get; set; }

        /// <summary>
        /// Gets or sets the modifier value.
        /// </summary>
        public int Value { get; set; }
    }
}
