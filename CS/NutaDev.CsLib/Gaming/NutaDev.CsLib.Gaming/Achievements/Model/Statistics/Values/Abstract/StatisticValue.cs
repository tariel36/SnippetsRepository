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

using NutaDev.CsLib.Gaming.Achievements.Exceptions.Statistics;
using NutaDev.CsLib.Gaming.Resources;
using NutaDev.CsLib.Maintenance.Exceptions.Factories;
using System;
using System.Globalization;

namespace NutaDev.CsLib.Gaming.Achievements.Model.Statistics.Values.Abstract
{
    /// <summary>
    /// Base class for statistic values.
    /// </summary>
    public abstract class StatisticValue
    {
        /// <summary>
        /// Converts string value into raw value and sets it as statistic's value.
        /// </summary>
        /// <param name="rawValue">The raw value.</param>
        public abstract void FromString(string rawValue);

        /// <summary>
        /// Increments value.
        /// </summary>
        /// <returns>Reference to itself.</returns>
        public abstract StatisticValue Increment();

        /// <summary>
        /// Decrements value.
        /// </summary>
        /// <returns>Reference to itself.</returns>
        public abstract StatisticValue Decrement();

        /// <summary>
        /// Gets value.
        /// </summary>
        /// <returns>Reference to itself.</returns>
        public abstract StatisticValue GetValue();

        /// <summary>
        /// Sets value.
        /// </summary>
        /// <param name="value">New value.</param>
        /// <returns>Reference to itself.</returns>
        public abstract StatisticValue SetValue(StatisticValue value);

        /// <summary>
        /// Adds <paramref name="value"/> to value.
        /// </summary>
        /// <param name="value">Value to add.</param>
        /// <returns>Reference to itself.</returns>
        public abstract StatisticValue AddValue(StatisticValue value);

        /// <summary>
        /// Substracts <paramref name="value"/> from value.
        /// </summary>
        /// <param name="value">Value to substract.</param>
        /// <returns>Reference to itself.</returns>
        public abstract StatisticValue SubstractValue(StatisticValue value);

        /// <summary>
        /// Equality check.
        /// </summary>
        /// <param name="value">Value to check against.</param>
        /// <returns>True if values are equal, false otherwise.</returns>
        public abstract bool IsEqual(StatisticValue value);

        /// <summary>
        /// Tests for value inequality.
        /// </summary>
        /// <param name="value">Value to check against.</param>
        /// <returns>True if values are not equal, false otherwise.</returns>
        public abstract bool IsNotEqual(StatisticValue value);

        /// <summary>
        /// Checks if <paramref name="value"/> is greater.
        /// </summary>
        /// <param name="value">Value to check against.</param>
        /// <returns>True if value is greater.</returns>
        public abstract bool IsGreater(StatisticValue value);

        /// <summary>
        /// Checks if <paramref name="value"/> is lesser.
        /// </summary>
        /// <param name="value">Value to check against.</param>
        /// <returns>True if value is lesser.</returns>
        public abstract bool IsLesser(StatisticValue value);

        /// <summary>
        /// Checks if <paramref name="value"/> is lesser or equal.
        /// </summary>
        /// <param name="value">Value to check against.</param>
        /// <returns>True if value is lesser or equal.</returns>
        public abstract bool IsLesserOrEqual(StatisticValue value);

        /// <summary>
        /// Checks if <paramref name="value"/> is greater or equal.
        /// </summary>
        /// <param name="value">Value to check against.</param>
        /// <returns>True if value is greater or equal.</returns>
        public abstract bool IsGreaterOrEqual(StatisticValue value);
    }

    /// <summary>
    /// Statistic class with specialized value type.
    /// </summary>
    /// <typeparam name="TRawValue">Raw value type.</typeparam>
    public abstract class StatisticValue<TRawValue>
        : StatisticValue
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StatisticValue{TRawValue}"/> class.
        /// </summary>
        public StatisticValue()
        {
            FormatProvider = CultureInfo.InvariantCulture;
        }

        /// <summary>
        /// Gets or sets the raw value.
        /// </summary>
        protected TRawValue RawValue { get; set; }

        /// <summary>
        /// Gets or sets the format provider.
        /// </summary>
        protected IFormatProvider FormatProvider { get; set; }

        /// <summary>
        /// Converts string value into raw value and sets it as statistic's value.
        /// </summary>
        /// <param name="rawValue">The raw value.</param>
        public override void FromString(string rawValue)
        {
            RawValue = (TRawValue) Convert.ChangeType(rawValue, typeof(TRawValue), FormatProvider);
        }

        /// <summary>
        /// Converts value to string.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString()
        {
            return (RawValue is IConvertible convertibleValue
                ? convertibleValue?.ToString(FormatProvider)
                : RawValue?.ToString()
                ) ?? string.Empty;
        }

        /// <summary>
        /// Gets value.
        /// </summary>
        /// <returns>Reference to itself.</returns>
        public override StatisticValue GetValue()
        {
            return this;
        }

        /// <summary>
        /// Sets value.
        /// </summary>
        /// <param name="value">New value.</param>
        /// <returns>Reference to itself.</returns>
        public override StatisticValue SetValue(StatisticValue value)
        {
            RawValue = ExtractValue(this, value);

            return this;
        }

        /// <summary>
        /// Equality check.
        /// </summary>
        /// <param name="value">Value to check against.</param>
        /// <returns>True if values are equal, false otherwise.</returns>
        public override bool IsEqual(StatisticValue value)
        {
            TRawValue right = ExtractValue(this, value);

            return Equals(RawValue, right);
        }

        /// <summary>
        /// Tests for value inequality.
        /// </summary>
        /// <param name="value">Value to check against.</param>
        /// <returns>True if values are not equal, false otherwise.</returns>
        public override bool IsNotEqual(StatisticValue value)
        {
            return !IsEqual(value);
        }

        /// <summary>
        /// Checks if <paramref name="value"/> is lesser or equal.
        /// </summary>
        /// <param name="value">Value to check against.</param>
        /// <returns>True if value is lesser or equal.</returns>
        public override bool IsLesserOrEqual(StatisticValue value)
        {
            return IsLesser(value) || IsEqual(value);
        }

        /// <summary>
        /// Checks if <paramref name="value"/> is greater or equal.
        /// </summary>
        /// <param name="value">Value to check against.</param>
        /// <returns>True if value is greater or equal.</returns>
        public override bool IsGreaterOrEqual(StatisticValue value)
        {
            return IsGreater(value) || IsEqual(value);
        }

        /// <summary>
        /// Extracts raw value from statistic.
        /// </summary>
        /// <typeparam name="TStatisticValue">Specialized statistic type.</typeparam>
        /// <param name="that">Reference to itself used for type check.</param>
        /// <param name="value">Value to extract.</param>
        /// <returns>Extracted raw value</returns>
        /// <exception cref="InvalidStatisticTypeException">If types don't match.</exception>
        protected TRawValue ExtractValue<TStatisticValue>(TStatisticValue that, StatisticValue value)
            where TStatisticValue : StatisticValue<TRawValue>
        {
            TStatisticValue fValue = value as TStatisticValue ?? throw ExceptionFactory.Create<InvalidStatisticTypeException>(Text.ExpectedType_0_ButActualIs_1_, typeof(TStatisticValue), value?.GetType());
            return fValue.RawValue;
        }
    }
}
