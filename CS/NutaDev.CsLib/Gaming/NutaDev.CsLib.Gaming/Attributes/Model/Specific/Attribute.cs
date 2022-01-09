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
using System.Collections.Generic;
using System.Linq;

namespace NutaDev.CsLib.Gaming.Attributes.Model.Specific
{
    /// <summary>
    /// Represents the modifiable attribute.
    /// </summary>
    public class Attribute
    {
        /// <summary>
        /// Cached actual value.
        /// </summary>
        private int _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="Attribute"/> class.
        /// </summary>
        /// <param name="type">Attribute type.</param>
        /// <param name="baseValue">Attribute base value.</param>
        public Attribute(IAttributeType type, int baseValue)
            : this(type, baseValue, int.MinValue)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Attribute"/> class.
        /// </summary>
        /// <param name="type">Attribute type.</param>
        /// <param name="baseValue">Attribute base value.</param>
        /// <param name="minimumValue">Attribute minimum value.</param>
        public Attribute(IAttributeType type, int baseValue, int minimumValue)
        {
            Modifiers = new List<AttributeModifier>();

            Type = type;
            BaseValue = baseValue;
            MinimumValue = minimumValue;
            MaximumValue = int.MaxValue;
        }

        /// <summary>
        /// Gets the attribute type.
        /// </summary>
        public IAttributeType Type { get; }

        /// <summary>
        /// Gets  the actual value. Recalculates it if needed.
        /// </summary>
        public int Value { get { return IsDirty ? Recalculate() : _value; } }

        /// <summary>
        /// Gets ors sets the modifiers count.
        /// </summary>
        public int Count { get { return Modifiers.Count; } }

        /// <summary>
        /// Gets or sets the base value.
        /// </summary>
        public int BaseValue { get; set; }

        /// <summary>
        /// Gets or sets the minimum value.
        /// </summary>
        public int MinimumValue { get; set; }

        /// <summary>
        /// Gets or sets maximum value.
        /// </summary>
        public int MaximumValue { get; set; }

        /// <summary>
        /// Indicates whether attribute needs a recalculation.
        /// </summary>
        private bool IsDirty { get; set; }

        /// <summary>
        /// Gets the collection of modifiers.
        /// </summary>
        private List<AttributeModifier> Modifiers { get; }

        /// <summary>
        /// Adds modifier.
        /// </summary>
        /// <param name="modifier">The modifier to add.</param>
        public void AddModifier(AttributeModifier modifier)
        {
            Modifiers.Add(modifier);
            IsDirty = true;
        }

        /// <summary>
        /// Removes modifier.
        /// </summary>
        /// <param name="modifier">The modifier to remove.</param>
        public void RemoveModifier(AttributeModifier modifier)
        {
            Modifiers.Remove(modifier);
            IsDirty = true;
        }

        /// <summary>
        /// Removes all modifiers based on source.
        /// </summary>
        /// <param name="source">The source.</param>
        public void RemoveModifier(object source)
        {
            Modifiers.RemoveAll(x => Equals(x.Source, source));
            IsDirty = true;
        }

        /// <summary>
        /// Recalculates the final value of the attribute.
        /// </summary>
        /// <returns>Actual value.</returns>
        private int Recalculate()
        {
            _value = BaseValue + Modifiers.Sum(x => x.Value);

            if (_value < MinimumValue)
            {
                _value = MinimumValue;
            }
            else if (_value > MaximumValue)
            {
                _value = MaximumValue;
            }

            IsDirty = false;

            return _value;
        }
    }
}
