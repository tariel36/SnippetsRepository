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

using NutaDev.CsLib.Gaming.Achievements.Collections.Statistics;
using NutaDev.CsLib.Gaming.Achievements.Context;
using NutaDev.CsLib.Gaming.Achievements.Trackers;
using System.Collections.Generic;

namespace NutaDev.CsLib.Gaming.Achievements.Model.Achievements
{
    /// <summary>
    /// The achievement.
    /// </summary>
    public class Achievement
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="metaData"></param>
        /// <param name="statistics"></param>
        /// <param name="isUnlocked"></param>
        public Achievement(AchievementMetaData metaData, StatisticCollection statistics, bool isUnlocked)
        {
            MetaData = metaData;
            Statistics = statistics;
            IsUnlocked = isUnlocked;
        }

        /// <summary>
        /// Gets or sets achievement id.
        /// </summary>
        public long Id { get { return MetaData.Id; } }

        /// <summary>
        /// Gets or sets related API name.
        /// </summary>
        public string ApiName { get { return MetaData.ApiName; } }

        /// <summary>
        /// Gets or sets display name.
        /// </summary>
        public string DisplayName { get { return MetaData.DisplayName; } }

        /// <summary>
        /// Gets or sets achievement description.
        /// </summary>
        public string Description { get { return MetaData.Description; } }

        /// <summary>
        /// Gets or sets icon for unlocked achievement.
        /// </summary>
        public string AchievedIcon { get { return MetaData.AchievedIcon; } }

        /// <summary>
        /// Gets or sets icon for locked achievement.
        /// </summary>
        public string UnachievedIcon { get { return MetaData.UnachievedIcon; } }

        /// <summary>
        /// Indicates whether achievement has hidden details.
        /// </summary>
        public bool IsHidden { get { return MetaData.IsHidden; } }

        /// <summary>
        /// Indicates whether it's a meta (100%) achievement or not.
        /// </summary>
        public bool IsMeta { get { return MetaData.IsMeta; } }

        /// <summary>
        /// Gets types of tracked statistics.
        /// </summary>
        public ICollection<string> TrackedStatistics { get { return Statistics.Types; } }

        /// <summary>
        /// Gets or sets achievement unlocked event handler.
        /// </summary>
        public AchievementTracker.AchievementUnlockedDelegate AchievementUnlocked { get; set; }

        /// <summary>
        /// Indicates whether the achievement has been unlocked.
        /// </summary>
        public bool IsUnlocked { get; private set; }

        /// <summary>
        /// Gets the achievement's metadata.
        /// </summary>
        protected AchievementMetaData MetaData { get; }

        /// <summary>
        /// Gets the achievement's colloection of statistics.
        /// </summary>
        protected StatisticCollection Statistics { get; }

        /// <summary>
        /// If all requirements are fulfilled then unlocks the achievement.
        /// </summary>
        /// <returns>True if achievement has been unlocked.</returns>
        public bool TryUnlock()
        {
            return Unlock(Statistics.AreFulfilled());
        }

        /// <summary>
        /// Unlocks the achievement while ignoring the requirements.
        /// </summary>
        /// <returns>True if achievement has been unlocked.</returns>
        public bool ForceUnlock()
        {
            return Unlock(true);
        }

        /// <summary>
        /// Sets <see cref="IsUnlocked"/> flag to <see cref="true"/> but does not trigger the actual unlock.
        /// </summary>
        /// <param name="isUnlocked">Value to set.</param>
        public void SetUnlocked(bool isUnlocked)
        {
            IsUnlocked = isUnlocked;
        }

        /// <summary>
        /// 
        /// </summary>
        private void OnAchievementUnlocked()
        {
            if (AchievementUnlocked != null)
            {
                AchievementUnlockedContext ctx = new AchievementUnlockedContext(this);

                AchievementUnlocked(ctx);
            }
        }

        /// <summary>
        /// Tries to unlock the achievement.
        /// </summary>
        /// <param name="unlockStatus">Value to set.</param>
        /// <returns>Trrue if achievement has been unlocked, false otherwise.</returns>
        private bool Unlock(bool unlockStatus)
        {
            if (!IsUnlocked)
            {
                IsUnlocked = unlockStatus;

                if (IsUnlocked)
                {
                    OnAchievementUnlocked();
                }
            }

            return IsUnlocked;
        }
    }
}
