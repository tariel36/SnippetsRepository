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

namespace NutaDev.CsLib.Gaming.Achievements.Model.Statistics.Values.Specific
{
    /// <summary>
    /// Integer statistic.
    /// </summary>
    public class StatisticValueInt
        : StatisticValue<int>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StatisticValueInt"/> class.
        /// </summary>
        public StatisticValueInt()
            : this(0)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StatisticValueInt"/> class.
        /// </summary>
        /// <param name="value"></param>
        public StatisticValueInt(int value)
        {
            RawValue = value;
        }

        /// <summary>
        /// Returns new instance with default value.
        /// </summary>
        /// <returns>New instance with default value.</returns>
        public static StatisticValue DefaultValue()
        {
            return new StatisticValueInt();
        }

        /// <summary>
        /// Increments value.
        /// </summary>
        /// <returns>Reference to itself.</returns>
        public override StatisticValue Increment()
        {
            RawValue += 1;
            return this;
        }

        /// <summary>
        /// Decrements value.
        /// </summary>
        /// <returns>Reference to itself.</returns>
        public override StatisticValue Decrement()
        {
            RawValue -= 1;
            return this;
        }

        /// <summary>
        /// Adds <paramref name="value"/> to value.
        /// </summary>
        /// <param name="value">Value to add.</param>
        /// <returns>Reference to itself.</returns>
        public override StatisticValue AddValue(StatisticValue value)
        {
            RawValue += ExtractValue(this, value);

            return this;
        }

        /// <summary>
        /// Substracts <paramref name="value"/> from value.
        /// </summary>
        /// <param name="value">Value to substract.</param>
        /// <returns>Reference to itself.</returns>
        public override StatisticValue SubstractValue(StatisticValue value)
        {
            RawValue -= ExtractValue(this, value);

            return this;
        }

        /// <summary>
        /// Checks if <paramref name="value"/> is greater.
        /// </summary>
        /// <param name="value">Value to check against.</param>
        /// <returns>True if value is greater.</returns>
        public override bool IsGreater(StatisticValue value)
        {
            int right = ExtractValue(this, value);

            return RawValue > right;
        }

        /// <summary>
        /// Checks if <paramref name="value"/> is lesser.
        /// </summary>
        /// <param name="value">Value to check against.</param>
        /// <returns>True if value is lesser.</returns>
        public override bool IsLesser(StatisticValue value)
        {
            int right = ExtractValue(this, value);

            return RawValue < right;
        }
    }
}
