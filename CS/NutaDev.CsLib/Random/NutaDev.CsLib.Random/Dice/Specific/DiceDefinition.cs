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

namespace NutaDev.CsLib.Random.Dice.Specific
{
    /// <summary>
    /// Represents dice definition.
    /// </summary>
    public class DiceDefinition
        : ICloneable<DiceDefinition>
        , IDisplayString
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DiceDefinition"/> class. This is a copy constructor.
        /// </summary>
        /// <param name="other">The <see cref="DiceDefinition"/> to copy from.</param>
        public DiceDefinition(DiceDefinition other)
            : this(other.Sides, other.Modifier)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DiceDefinition"/> class. 
        /// </summary>
        /// <param name="sides">Number of sides.</param>
        /// <param name="modifier">The modifier.</param>
        public DiceDefinition(int sides, int modifier)
        {
            Sides = sides;
            Modifier = modifier;
            DisplayString = $"1d{Sides}" + (Modifier != 0 ? (Modifier > 0 ? $"+{Modifier}" : $"-{Modifier}") : string.Empty);
        }

        /// <summary>
        /// Gets number of sides.
        /// </summary>
        public int Sides { get; }

        /// <summary>
        /// Gets value modifier.
        /// </summary>
        public int Modifier { get; }

        /// <summary>
        /// Gets human readable value.
        /// </summary>
        public string DisplayString { get; }

        /// <summary>
        /// Creates new instance of the <see cref="DiceDefinition"/> class thats clone of current instance.
        /// </summary>
        /// <returns></returns>
        public DiceDefinition Clone()
        {
            return new DiceDefinition(this);
        }
    }
}
