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

using System.Collections.Generic;

namespace NutaDev.CsLib.Random.Services.Abstract
{
    /// <summary>
    /// Interface for service that handles random number generation.
    /// </summary>
    public interface IRandomService
    {
        /// <summary>
        /// Returns a random integer that is within a specified range.
        /// </summary>
        /// <param name="minInclusive">The inclusive lower bound of the random number returned.</param>
        /// <param name="maxExclusive">The exclusive upper bound of the random number returned.</param>
        /// <returns>A 32-bit signed integer greater than or equal to <paramref name="minInclusive" /> and less than <paramref name="maxExclusive" />.</returns>
        int Next(int minInclusive, int maxExclusive);

        /// <summary>
        /// Returns a random integer that is within a specified range.
        /// </summary>
        /// <param name="maxExclusive">The exclusive upper bound of the random number returned.</param>
        /// <returns>A 32-bit signed integer that's less than <paramref name="maxExclusive" />.</returns>
        int Next(int maxExclusive);

        /// <summary>
        /// Rolls multiple dice with <paramref name="sides"/>.
        /// </summary>
        /// <param name="diceCount">Number of dice.</param>
        /// <param name="sides">Side of each dice.</param>
        /// <returns>Collection of results.</returns>
        ICollection<int> Roll(int diceCount, int sides);

        /// <summary>
        /// Rolls single 20 sided dice.
        /// </summary>
        /// <returns>Result of roll.</returns>
        int Roll20();

        /// <summary>
        /// Rolls single dice with <paramref name="diceSides"/>.
        /// </summary>
        /// <param name="diceSides">Sides of dice.</param>
        /// <returns>Result of roll.</returns>
        int Roll(int diceSides);
    }
}
