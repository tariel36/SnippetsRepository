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

namespace NutaDev.CsLib.Random.Providers.Specific
{
    /// <summary>
    /// Default implementation of random number provider. Uses <see cref="System.Random"/> internally.
    /// </summary>
    public class DefaultRandomProvider
        : IRandomProvider
    {
        /// <summary>
        /// Initializes a new insntance of the <see cref="DefaultRandomProvider"/> class.
        /// </summary>
        public DefaultRandomProvider()
        {
            Random = new System.Random();
        }

        /// <summary>
        /// Initializes a new insntance of the <see cref="DefaultRandomProvider"/> class.
        /// </summary>
        /// <param name="seed">Starting seed.</param>
        public DefaultRandomProvider(int seed)
        {
            Random = new System.Random(seed);
        }

        /// <summary>
        /// Gets <see cref="System.Random"/> instance.
        /// </summary>
        private System.Random Random { get; }

        /// <summary>
        /// Returns a random integer.
        /// </summary>
        /// <returns>Random integer.</returns>
        public int Next()
        {
            return Random.Next();
        }

        /// <summary>
        /// Returns a random integer that is within a specified range.
        /// </summary>
        /// <param name="maxExclusive">The exclusive upper bound of the random number returned.</param>
        /// <returns>A 32-bit signed integer that's less than <paramref name="maxExclusive" />.</returns>
        public int Next(int maxExclusive)
        {
            return Random.Next(maxExclusive);
        }

        /// <summary>
        /// Returns a random integer that is within a specified range.
        /// </summary>
        /// <param name="minInclusive">The inclusive lower bound of the random number returned.</param>
        /// <param name="maxExclusive">The exclusive upper bound of the random number returned.</param>
        /// <returns>A 32-bit signed integer greater than or equal to <paramref name="minInclusive" /> and less than <paramref name="maxExclusive" />.</returns>
        public int Next(int minInclusive, int maxExclusive)
        {
            return Random.Next(minInclusive, maxExclusive);
        }
    }
}
