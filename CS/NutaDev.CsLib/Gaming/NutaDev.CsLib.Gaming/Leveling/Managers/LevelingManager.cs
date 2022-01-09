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

using NutaDev.CsLib.Gaming.Leveling.Formulas.Abstract;

namespace NutaDev.CsLib.Gaming.Leveling.Managers
{
    /// <summary>
    /// Class that provides utility methods to calculate experience values based on provided <see cref="LevelingFormula"/>.
    /// </summary>
    public class LevelingManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LevelingManager"/> class.
        /// </summary>
        /// <param name="levelingFormula"></param>
        public LevelingManager(ILevelingFormula levelingFormula)
        {
            LevelingFormula = levelingFormula;
        }

        /// <summary>
        /// Gets current leveling formula.
        /// </summary>
        public ILevelingFormula LevelingFormula { get; }

        /// <summary>
        /// Checks if level has increased.
        /// </summary>
        /// <param name="previousExp">Previous experience points.</param>
        /// <param name="currentExp">Current experience points.</param>
        /// <returns></returns>
        public bool HasLeveledUp(int previousExp, int currentExp)
        {
            int prevLevel = GetLevel(previousExp);
            int currLevel = GetLevel(currentExp);

            return prevLevel < currLevel;
        }

        /// <summary>
        /// Gets current level.
        /// </summary>
        /// <param name="exp">Current experience.</param>
        /// <returns>Current level.</returns>
        public int GetLevel(int exp)
        {
            return LevelingFormula.GetLevel(exp);
        }

        /// <summary>
        /// Gets minimum experience based on this level.
        /// </summary>
        /// <param name="exp">Current experience.</param>
        /// <returns>Minimum experience.</returns>
        public int GetLevelMinimumExp(int exp)
        {
            int level = GetLevel(exp);
            return LevelingFormula.GetExperience(level);
        }

        /// <summary>
        /// Gets maximum experience on this level.
        /// </summary>
        /// <param name="exp">Current experience.</param>
        /// <returns>Maximum experience.</returns>
        public int GetLevelMaximumExp(int exp)
        {
            int level = GetLevel(exp) + 1;
            return LevelingFormula.GetExperience(level);
        }

        /// <summary>
        /// Gets current progress experience.
        /// </summary>
        /// <param name="exp">Current exp.</param>
        /// <returns>Remaining experience.</returns>
        public int GetCurrentProgressExp(int exp)
        {
            int min = GetLevelMinimumExp(exp);

            return exp - min;
        }

        /// <summary>
        /// Calculates the experience required for next level.
        /// </summary>
        /// <param name="currentLevel">Current level.</param>
        /// <returns>Experience required for level.</returns>
        public int GetExperienceRequiredForNextLevel(int currentLevel)
        {
            int currentLvExp = LevelingFormula.GetExperience(currentLevel);
            int nextLvExp = LevelingFormula.GetExperience(currentLevel + 1);

            return nextLvExp - currentLvExp;
        }
    }
}
