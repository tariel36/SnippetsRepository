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

namespace NutaDev.CsLib.Gaming.Achievements.Model.Achievements
{
    /// <summary>
    /// Achievement metadata.
    /// </summary>
    public sealed class AchievementMetaData
    {
        /// <summary>
        /// Gets or sets achievement id.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets related API name.
        /// </summary>
        public string ApiName { get; set; }

        /// <summary>
        /// Gets or sets a statistic that indicates progress. This is somehow unique for some APIs.
        /// </summary>
        public string ProgressStat { get; set; }

        /// <summary>
        /// Gets or sets display name.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets achievement description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the source setter.
        /// </summary>
        public object SetBy { get; set; }

        /// <summary>
        /// Indicates whether achievement has hidden details.
        /// </summary>
        public bool IsHidden { get; set; }

        /// <summary>
        /// Indicates whether it's a meta (100%) achievement or not.
        /// </summary>
        public bool IsMeta { get; set; }

        /// <summary>
        /// Gets or sets icon for unlocked achievement.
        /// </summary>
        public string AchievedIcon { get; set; }

        /// <summary>
        /// Gets or sets icon for locked achievement.
        /// </summary>
        public string UnachievedIcon { get; set; }

        /// <summary>
        /// Gets or sets the value of achievement.
        /// </summary>
        public string Experience { get; set; }

        /// <summary>
        /// Gets or sets unit of <see cref="Experience"/>.
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// Gets or sets related statistics.
        /// </summary>
        public string[] StatisticApiName { get; set; }
    }
}
