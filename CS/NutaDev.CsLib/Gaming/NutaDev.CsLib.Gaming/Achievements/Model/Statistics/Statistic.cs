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
using NutaDev.CsLib.Gaming.Achievements.Comparators.Statistics;

namespace NutaDev.CsLib.Gaming.Achievements.Model.Statistics
{
    /// <summary>
    /// The statistic class.
    /// </summary>
    public class Statistic
    {
        /// <summary>
        /// Delegate used for <see cref="StatisticComparators"/>.
        /// </summary>
        /// <param name="left">Left argument.</param>
        /// <param name="right">Right argument.</param>
        /// <returns>Comparison result.</returns>
        public delegate bool CompareDelegate(StatisticValue left, StatisticValue right);

        /// <summary>
        /// Initializes a new instance of the <see cref="Statistic"/> class.
        /// </summary>
        /// <param name="metaData">The metadata.</param>
        /// <param name="initialValue">Initial value.</param>
        /// <param name="currentValue">Current value.</param>
        /// <param name="requiredValue">Required value.</param>
        /// <param name="comparator">The comparator.</param>
        public Statistic(StatisticMetaData metaData,
            StatisticValue initialValue,
            StatisticValue currentValue,
            StatisticValue requiredValue,
            CompareDelegate comparator)
        {
            MetaData = metaData;
            InitialValue = initialValue;
            CurrentValue = currentValue;
            RequiredValue = requiredValue;
            Comparator = comparator;
        }

        public string Type { get { return MetaData.Type; } }

        /// <summary>
        /// Related API name.
        /// </summary>
        public string ApiName { get { return MetaData.ApiName; } }

        /// <summary>
        /// Gets the initial value.
        /// </summary>
        public StatisticValue InitialValue { get; }

        /// <summary>
        /// Gets the current value.
        /// </summary>
        public StatisticValue CurrentValue { get; }

        /// <summary>
        /// Gets the required value.
        /// </summary>
        public StatisticValue RequiredValue { get; }

        /// <summary>
        /// Gets statistic comparator.
        /// </summary>
        public CompareDelegate Comparator { get; }

        /// <summary>
        /// Gets statistic metadata.
        /// </summary>
        private StatisticMetaData MetaData { get; }

        /// <summary>
        /// Indicates whether comparison by <see cref="Comparator"/> and <see cref="CurrentValue"/>, <see cref="RequiredValue"/> values is fulfuilled.
        /// </summary>
        /// <returns>True if check is fulfilled, false otherwise.</returns>
        public bool IsFulfilled()
        {
            return Comparator(CurrentValue, RequiredValue);
        }

        /// <summary>
        /// Sets value to <see cref="InitialValue"/>.
        /// </summary>
        public void Clear()
        {
            CurrentValue.SetValue(InitialValue);
        }

        /// <summary>
        /// Adds value to current value.
        /// </summary>
        /// <param name="value">Value to add.</param>
        public void AddValue(StatisticValue value)
        {
            if (string.Equals(MetaData.Comparator, nameof(StatisticComparators.Equal)))
            {
                if (value.IsEqual(RequiredValue))
                {
                    CurrentValue.AddValue(value);
                }
            }
            else
            {
                CurrentValue.AddValue(value);
            }
        }
    }
}
