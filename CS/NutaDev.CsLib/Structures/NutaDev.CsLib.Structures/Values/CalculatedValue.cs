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

using NutaDev.CsLib.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace NutaDev.CsLib.Structures.Values
{
    /// <summary>
    /// Represents value that can be modified by external modificators - <see cref="ValueModifier"/>.
    /// </summary>
    public class CalculatedValue
        : ICloneable<CalculatedValue>
    {
        /// <summary>
        /// Backing value for <see cref="FinalValue"/>.
        /// </summary>
        private int _finalValue;

        /// <summary>
        /// Backing field for <see cref="BaseValue"/>.
        /// </summary>
        private int _baseValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="CalculatedValue"/> class.
        /// </summary>
        public CalculatedValue()
        {
            Modifiers = new List<ValueModifier>();
            IsDirty = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CalculatedValue"/> class.
        /// </summary>
        /// <param name="baseValue">Base value.</param>
        public CalculatedValue(int baseValue)
        {
            BaseValue = baseValue;
            Modifiers = new List<ValueModifier>();
            IsDirty = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CalculatedValue"/> class. This is copy constructor.
        /// </summary>
        /// <param name="other">Source of values.</param>
        public CalculatedValue(CalculatedValue other)
        {
            BaseValue = other.BaseValue;
            Modifiers = other.Modifiers.Select(x => x.Clone()).ToList();
            IsDirty = true;
        }

        /// <summary>
        /// Gets or sets base value.
        /// </summary>
        public int BaseValue
        {
            get { return _baseValue; }
            set
            {
                _baseValue = value;
                IsDirty = true;
            }
        }

        /// <summary>
        /// Gets final value.
        /// </summary>
        public int FinalValue
        {
            get
            {
                if (IsDirty)
                {
                    Calculate();
                }

                return _finalValue;
            }
        }

        /// <summary>
        /// Indicates whether <see cref="FinalValue"/> is dirtu and should be recalculated.
        /// </summary>
        public bool IsDirty { get; private set; }

        /// <summary>
        /// Gets collection of value modifiers.
        /// </summary>
        private List<ValueModifier> Modifiers { get; }

        /// <summary>
        /// Creates clone of the current instance.
        /// </summary>
        /// <returns>Clone of the current instance.</returns>
        public CalculatedValue Clone()
        {
            return new CalculatedValue(this);
        }

        /// <summary>
        /// Adds modifier.
        /// </summary>
        /// <param name="modifier">Modifier to add.</param>
        public void AddModifier(ValueModifier modifier)
        {
            Modifiers.Add(modifier);

            IsDirty = true;
        }

        /// <summary>
        /// Removes modifier.
        /// </summary>
        /// <param name="modifier">Modifier to remove.</param>
        public void RemoveModifier(ValueModifier modifier)
        {
            Modifiers.Remove(modifier);

            IsDirty = true;
        }

        /// <summary>
        /// Clears all modifiers.
        /// </summary>
        public void Clear()
        {
            Modifiers.Clear();

            IsDirty = true;
        }

        /// <summary>
        /// Recalculates <see cref="FinalValue"/>.
        /// </summary>
        private void Calculate()
        {
            _finalValue = BaseValue;

            foreach (ValueModifier modifier in Modifiers)
            {
                _finalValue += modifier.Value;
            }

            IsDirty = false;
        }
    }
}
