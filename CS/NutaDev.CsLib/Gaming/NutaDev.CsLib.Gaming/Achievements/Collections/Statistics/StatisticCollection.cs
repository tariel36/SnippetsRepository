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

using NutaDev.CsLib.Gaming.Achievements.Model.Statistics;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NutaDev.CsLib.Gaming.Achievements.Collections.Statistics
{
    /// <summary>
    /// Collection of statistics.
    /// </summary>
    public class StatisticCollection
        : IEnumerable<Statistic>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StatisticCollection"/>.
        /// </summary>
        public StatisticCollection()
        {
            StatisticsByType = new Dictionary<string, List<Statistic>>();
        }

        /// <summary>
        /// Gets statistic types.
        /// </summary>
        public ICollection<string> Types { get { return StatisticsByType.Keys; } }

        /// <summary>
        /// Get statistics grouped by their type.
        /// </summary>
        private Dictionary<string, List<Statistic>> StatisticsByType { get; }

        /// <summary>
        /// Adds multiple statistics.
        /// </summary>
        /// <param name="stats">Statistics to add.</param>
        /// <returns>Reference to itself.</returns>
        public StatisticCollection AddRange(IEnumerable<Statistic> stats)
        {
            if (stats != null)
            {
                foreach (Statistic stat in stats)
                {
                    Add(stat);
                }
            }

            return this;
        }

        /// <summary>
        /// Add statistic from collection.
        /// </summary>
        /// <param name="stat">Statistic to add.</param>
        /// <returns>Reference to itself.</returns>
        public StatisticCollection Add(Statistic stat)
        {
            if (stat != null)
            {
                EnsureKey(stat.Type);

                StatisticsByType[stat.Type].Add(stat);
            }

            return this;
        }

        /// <summary>
        /// Removes statistic from collection.
        /// </summary>
        /// <param name="stat">Statistic to remove.</param>
        /// <returns>Reference to itself.</returns>
        public StatisticCollection Remove(Statistic stat)
        {
            if (stat != null)
            {
                if (ContainsKey(stat.Type))
                {
                    StatisticsByType[stat.Type].Remove(stat);
                }
            }

            return this;
        }

        /// <summary>
        /// Indicates whether all statistis are fulfilled.
        /// </summary>
        /// <returns></returns>
        public bool AreFulfilled()
        {
            return GetValues().All(x => x.IsFulfilled());
        }

        /// <summary>
        /// Gets enumerator.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator<Statistic> GetEnumerator()
        {
            return GetValues().GetEnumerator();
        }

        /// <summary>
        /// Gets enumerator.
        /// </summary>
        /// <returns>The enumerator.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Ensures that key/value pair exists.
        /// </summary>
        /// <param name="type">Statistic type.</param>
        private void EnsureKey(string type)
        {
            if (!StatisticsByType.ContainsKey(type))
            {
                StatisticsByType[type] = new List<Statistic>();
            }
        }

        /// <summary>
        /// Indicates whether collection contains <see cref="type"/>.
        /// </summary>
        /// <param name="type">Type to check.</param>
        /// <returns>True if contains key, false otherwise.</returns>
        private bool ContainsKey(string type)
        {
            return StatisticsByType.ContainsKey(type);
        }

        /// <summary>
        /// Gets statistics in the collection.
        /// </summary>
        /// <returns>Statistics in the collection.</returns>
        private IEnumerable<Statistic> GetValues()
        {
            return StatisticsByType.Values.SelectMany(x => x);
        }
    }
}
