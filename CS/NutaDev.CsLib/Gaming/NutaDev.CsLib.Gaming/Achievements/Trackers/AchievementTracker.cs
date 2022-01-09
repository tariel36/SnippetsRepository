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

using NutaDev.CsLib.Gaming.Achievements.Context;
using NutaDev.CsLib.Gaming.Achievements.Model.Achievements;
using NutaDev.CsLib.Gaming.Achievements.Model.Statistics;
using NutaDev.CsLib.Gaming.Resources;
using NutaDev.CsLib.Maintenance.Exceptions.Factories;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace NutaDev.CsLib.Gaming.Achievements.Trackers
{
    /// <summary>
    /// The achievement tracker.
    /// </summary>
    public class AchievementTracker
    {
        /// <summary>
        /// Delegate used for unlock event.
        /// </summary>
        /// <param name="ctx"></param>
        public delegate void AchievementUnlockedDelegate(AchievementUnlockedContext ctx);

        /// <summary>
        /// Sync object for asynchronous operations.
        /// </summary>
        private static readonly object SyncObject = new object();

        /// <summary>
        /// Gets achievements grouped by statistic type.
        /// </summary>
        private Dictionary<string, List<Achievement>> Achievements { get; }

        /// <summary>
        /// Gets statistics grouped by statistic type.
        /// </summary>
        private Dictionary<string, List<Statistic>> Statistics { get; }

        /// <summary>
        /// Gets or sets statistics event queue.
        /// </summary>
        private ConcurrentQueue<StatisticChangedContext> EventQueue { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AchievementTracker"/> class.
        /// </summary>
        public AchievementTracker()
        {
            EventQueue = new ConcurrentQueue<StatisticChangedContext>();

            Achievements = new Dictionary<string, List<Achievement>>();
            Statistics = new Dictionary<string, List<Statistic>>();
        }

        /// <summary>
        /// Gets or sets achievement unlocked event handler.
        /// </summary>
        public AchievementUnlockedDelegate AchievementUnlocked { get; set; }

        /// <summary>
        /// Indicates whether tracker is locked.
        /// </summary>
        private bool IsLocked { get; set; }

        /// <summary>
        /// Gets or sets source of lock.
        /// </summary>
        private object LockSource { get; set; }

        /// <summary>
        /// Sets the collection of tracked statistics.
        /// </summary>
        /// <param name="statistics">Collection of statistics.</param>
        public void SetStatistics(ICollection<Statistic> statistics)
        {
            Statistics.Clear();

            Statistics[string.Empty] = new List<Statistic>();

            foreach (Statistic stat in statistics)
            {
                string statType = stat.Type;

                if (!Statistics.ContainsKey(statType))
                {
                    Statistics[statType] = new List<Statistic>();
                }

                Statistics[statType].Add(stat);
            }
        }

        /// <summary>
        /// Sets the collection of tracked achievements.
        /// </summary>
        /// <param name="achievements">Collection of achievements.</param>
        public void SetAchievements(ICollection<Achievement> achievements)
        {
            Achievements.Clear();

            foreach (Achievement achi in achievements)
            {
                if (achi.TrackedStatistics == null)
                {
                    string statType = string.Empty;

                    if (!Achievements.ContainsKey(statType))
                    {
                        Achievements[statType] = new List<Achievement>();
                    }

                    achi.AchievementUnlocked = OnAchievementUnlocked;

                    Achievements[statType].Add(achi);
                }
                else
                {
                    foreach (string statType in achi.TrackedStatistics)
                    {
                        if (!Achievements.ContainsKey(statType))
                        {
                            Achievements[statType] = new List<Achievement>();
                        }

                        achi.AchievementUnlocked = OnAchievementUnlocked;

                        Achievements[statType].Add(achi);
                    }
                }
            }
        }

        /// <summary>
        /// Gets tracked statistics.
        /// </summary>
        /// <returns>Collection of tracked statistics.</returns>
        public IEnumerable<Statistic> GetStatistics()
        {
            if (!IsLocked)
            {
                throw ExceptionFactory.InvalidOperationException(Text.YouHaveToLockTrackerToEnumerateStatistics);
            }

            ProcessQueue();

            return EnumerateStatistics();
        }

        /// <summary>
        /// Gets tracked achievements.
        /// </summary>
        /// <returns>Collection of tracked achievements.</returns>
        public IEnumerable<Achievement> GetAchievements()
        {
            if (!IsLocked)
            {
                throw ExceptionFactory.InvalidOperationException(Text.YouHaveToLockTrackerToEnumerateAchievements);
            }

            ProcessQueue();

            return EnumerateAchievements();
        }

        /// <summary>
        /// Processes the queue.
        /// </summary>
        public void Update()
        {
            string source = nameof(Update);

            if (Lock(source))
            {
                ProcessQueue();

                Unlock(source);
            }
        }

        /// <summary>
        /// Locks the asynchronous access.
        /// </summary>
        /// <param name="lockSource">Source of lock.</param>
        /// <returns>True if lock has been set.</returns>
        public bool Lock(object lockSource)
        {
            lock (SyncObject)
            {
                if (IsLocked)
                {
                    return false;
                }

                LockSource = lockSource;

                IsLocked = true;

                return true;
            }
        }

        /// <summary>
        /// Unlocks the asynchronous access.
        /// </summary>
        /// <param name="lockSource">Source of lock.</param>
        /// <returns>True if lock has been lifted.</returns>
        public bool Unlock(object lockSource)
        {
            lock (SyncObject)
            {
                if (!IsLocked)
                {
                    return false;
                }

                if (LockSource != lockSource)
                {
                    return false;
                }

                LockSource = null;

                IsLocked = false;

                return true;
            }
        }

        /// <summary>
        /// Enqueues statistic change event.
        /// </summary>
        /// <param name="ctx">Statistic change context.</param>
        public void EnqueueEvent(StatisticChangedContext ctx)
        {
            EventQueue.Enqueue(ctx);

            if (Lock(this))
            {
                ProcessQueue();

                Unlock(this);
            }
        }

        /// <summary>
        /// Gets tracked achievements.
        /// </summary>
        /// <returns>Collection of tracked achievements.</returns>
        private IEnumerable<Achievement> EnumerateAchievements()
        {
            return Achievements.SelectMany(x => x.Value);
        }

        /// <summary>
        /// Gets tracked statistics.
        /// </summary>
        /// <returns>Collection of tracked statistics.</returns>
        private IEnumerable<Statistic> EnumerateStatistics()
        {
            return Statistics.SelectMany(x => x.Value);
        }

        /// <summary>
        /// Processes statistic change events.
        /// </summary>
        private void ProcessQueue()
        {
            while (EventQueue.TryDequeue(out StatisticChangedContext eventInfo))
            {
                if (Statistics.ContainsKey(eventInfo.Type))
                {
                    foreach (Statistic stat in Statistics[eventInfo.Type])
                    {
                        stat.AddValue(eventInfo.Value);
                    }

                    foreach (Achievement achi in Achievements[eventInfo.Type])
                    {
                        achi.TryUnlock();
                    }
                }
            }
        }

        /// <summary>
        /// Achievement unlocked event handler.
        /// </summary>
        /// <param name="ctx">Unlock context.</param>
        private void OnAchievementUnlocked(AchievementUnlockedContext ctx)
        {
            AchievementUnlocked?.Invoke(ctx);
        }
    }
}
