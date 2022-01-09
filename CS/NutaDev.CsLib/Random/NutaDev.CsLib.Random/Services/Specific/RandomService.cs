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

using NutaDev.CsLib.Random.Providers.Abstract;
using NutaDev.CsLib.Random.Providers.Specific;
using NutaDev.CsLib.Random.Services.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace NutaDev.CsLib.Random.Services.Specific
{
    /// <summary>
    /// Service with various random number generation methods.
    /// </summary>
    public class RandomService
        : IRandomService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RandomService"/> class with <see cref="DefaultRandomProvider"/>.
        /// </summary>
        public RandomService()
            : this(new DefaultRandomProvider())
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomService"/> class.
        /// </summary>
        /// <param name="randomProvider"></param>
        public RandomService(IRandomProvider randomProvider)
        {
            RandomProvider = randomProvider;
        }

        /// <summary>
        /// Gets implementation of <see cref="IRandomProvider"/>.
        /// </summary>
        public IRandomProvider RandomProvider { get; }

        /// <summary>
        /// Returns a random integer that is within a specified range.
        /// </summary>
        /// <param name="minInclusive">The inclusive lower bound of the random number returned.</param>
        /// <param name="maxExclusive">The exclusive upper bound of the random number returned.</param>
        /// <returns>A 32-bit signed integer greater than or equal to <paramref name="minInclusive" /> and less than <paramref name="maxExclusive" />.</returns>
        public int Next(int minInclusive, int maxExclusive)
        {
            return RandomProvider.Next(minInclusive, maxExclusive);
        }

        /// <summary>
        /// Returns a random integer that is within a specified range.
        /// </summary>
        /// <param name="maxExclusive">The exclusive upper bound of the random number returned.</param>
        /// <returns>A 32-bit signed integer that's less than <paramref name="maxExclusive" />.</returns>
        public int Next(int maxExclusive)
        {
            return RandomProvider.Next(maxExclusive);
        }

        /// <summary>
        /// Rolls multiple dice with <paramref name="sides"/>.
        /// </summary>
        /// <param name="diceCount">Number of dice.</param>
        /// <param name="sides">Side of each dice.</param>
        /// <returns>Collection of results.</returns>
        public ICollection<int> Roll(int diceCount, int sides)
        {
            return Enumerable.Range(0, diceCount).Select(x => Roll(sides)).ToArray();
        }

        /// <summary>
        /// Rolls single 20 sided dice.
        /// </summary>
        /// <returns>Result of roll.</returns>
        public int Roll20()
        {
            return RandomProvider.Next(1, 21);
        }

        /// <summary>
        /// Rolls single dice with <paramref name="diceSides"/>.
        /// </summary>
        /// <param name="diceSides">Sides of dice.</param>
        /// <returns>Result of roll.</returns>
        public int Roll(int diceSides)
        {
            return RandomProvider.Next(1, diceSides + 1);
        }
    }
}
