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

using NutaDev.CsLib.Gaming.Achievements.Model.Statistics.Values.Abstract;
using System;

namespace NutaDev.CsLib.Gaming.Achievements.Model.Statistics.Values.Specific
{
    /// <summary>
    /// Statistic that represents collection of strings.
    /// </summary>
    public class StatisticValueStringCollection
        : StatisticValue<string>
    {
        /// <summary>
        /// Separator character.
        /// </summary>
        private const string ConcatenationString = ";";

        /// <summary>
        /// Initializes a new instance of the <see cref="StatisticValueStringCollection"/> class.
        /// </summary>
        public StatisticValueStringCollection()
            : this(string.Empty)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StatisticValueStringCollection"/> class.
        /// </summary>
        /// <param name="value"></param>
        public StatisticValueStringCollection(string value)
        {
            RawValue = value;
        }

        /// <summary>
        /// Returns new instance with default value.
        /// </summary>
        /// <returns>New instance with default value.</returns>
        public static StatisticValue DefaultValue()
        {
            return new StatisticValueStringCollection();
        }

        /// <summary>
        /// Decrements value.
        /// </summary>
        /// <returns>Reference to itself.</returns>
        public override StatisticValue Increment()
        {
            return this;
        }

        /// <summary>
        /// Gets value.
        /// </summary>
        /// <returns>Reference to itself.</returns>
        public override StatisticValue Decrement()
        {
            return this;
        }

        /// <summary>
        /// Adds <paramref name="value"/> to value.
        /// </summary>
        /// <param name="value">Value to add.</param>
        /// <returns>Reference to itself.</returns>
        public override StatisticValue AddValue(StatisticValue value)
        {
            RawValue = string.Join(ConcatenationString, RawValue, ExtractValue(this, value));

            return this;
        }

        /// <summary>
        /// Substracts <paramref name="value"/> from value.
        /// </summary>
        /// <param name="value">Value to substract.</param>
        /// <returns>Reference to itself.</returns>
        public override StatisticValue SubstractValue(StatisticValue value)
        {
            RawValue = RawValue.Replace(ExtractValue(this, value), string.Empty)
                .Replace(ConcatenationString + ConcatenationString, string.Empty);

            return this;
        }

        /// <summary>
        /// Checks if <paramref name="value"/> is greater.
        /// </summary>
        /// <param name="value">Value to check against.</param>
        /// <returns>True if value is greater.</returns>
        public override bool IsGreater(StatisticValue value)
        {
            string right = ExtractValue(this, value);

            return string.Compare(RawValue, right, StringComparison.Ordinal) > 0;
        }

        /// <summary>
        /// Checks if <paramref name="value"/> is lesser.
        /// </summary>
        /// <param name="value">Value to check against.</param>
        /// <returns>True if value is lesser.</returns>
        public override bool IsLesser(StatisticValue value)
        {
            string right = ExtractValue(this, value);

            return string.Compare(RawValue, right, StringComparison.Ordinal) < 0;
        }
    }
}
