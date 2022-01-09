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

namespace NutaDev.CsLib.Gaming.Achievements.Model.Statistics
{
    public class StatisticMetaData
    {
        /// <summary>
        /// Gets or sets statistic id.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets value type name.
        /// </summary>
        public string ValueType { get; set; }

        /// <summary>
        /// Gets or sets API name.
        /// </summary>
        public string ApiName { get; set; }

        /// <summary>
        /// Gets or sets set source.
        /// </summary>
        public object SetBy { get; set; }

        /// <summary>
        /// Indicates whether statistic can be incremented only.
        /// </summary>
        public bool IsIncrementOnly { get; set; }

        /// <summary>
        /// Gets or sets maximum change value.
        /// </summary>
        public string MaxChange { get; set; }

        /// <summary>
        /// Gets or sets minimum value.
        /// </summary>
        public string MinValue { get; set; }

        /// <summary>
        /// Gets or sets maximum value.
        /// </summary>
        public string MaxValue { get; set; }

        /// <summary>
        /// Gets or sets default value.
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// Indicates whether statistic is aggregated.
        /// </summary>
        public bool IsAggregated { get; set; }

        /// <summary>
        /// Gets or sets display name.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets time window.
        /// </summary>
        public object Window { get; set; }

        /// <summary>
        /// Gets or sets statistic type name.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets comparator name.
        /// </summary>
        public string Comparator { get; set; }
    }
}
