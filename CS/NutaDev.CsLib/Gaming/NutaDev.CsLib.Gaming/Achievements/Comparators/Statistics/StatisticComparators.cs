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

namespace NutaDev.CsLib.Gaming.Achievements.Comparators.Statistics
{
    /// <summary>
    /// Utility class that provide comparators for <see cref="StatisticValue"/>.
    /// </summary>
    public static class StatisticComparators
    {
        /// <summary>
        /// Equality check.
        /// </summary>
        /// <param name="left">Left argument.</param>
        /// <param name="right">Right argument.</param>
        /// <returns>True if equal, false otherwise.</returns>
        public static bool Equal(StatisticValue left, StatisticValue right)
        {
            return left.IsEqual(right);
        }

        /// <summary>
        /// Inequality check.
        /// </summary>
        /// <param name="left">Left argument.</param>
        /// <param name="right">Right argument.</param>
        /// <returns>True if not equal, false otherwise.</returns>
        public static bool NotEqual(StatisticValue left, StatisticValue right)
        {
            return left.IsNotEqual(right);
        }

        /// <summary>
        /// Greater than check.
        /// </summary>
        /// <param name="left">Left argument.</param>
        /// <param name="right">Right argument.</param>
        /// <returns>True if greater, false otherwise.</returns>
        public static bool Greater(StatisticValue left, StatisticValue right)
        {
            return left.IsGreater(right);
        }

        /// <summary>
        /// lesser than check.
        /// </summary>
        /// <param name="left">Left argument.</param>
        /// <param name="right">Right argument.</param>
        /// <returns>True if lesser, false otherwise.</returns>
        public static bool Lesser(StatisticValue left, StatisticValue right)
        {
            return left.IsLesser(right);
        }

        /// <summary>
        /// Greater or equal check.
        /// </summary>
        /// <param name="left">Left argument.</param>
        /// <param name="right">Right argument.</param>
        /// <returns>True if greater or equal, false otherwise.</returns>
        public static bool GreaterOrEqual(StatisticValue left, StatisticValue right)
        {
            return left.IsGreaterOrEqual(right);
        }

        /// <summary>
        /// Lesser or equal check.
        /// </summary>
        /// <param name="left">Left argument.</param>
        /// <param name="right">Right argument.</param>
        /// <returns>True if , false otherwise.</returns>
        public static bool LesserOrEqual(StatisticValue left, StatisticValue right)
        {
            return left.IsLesserOrEqual(right);
        }
    }
}
