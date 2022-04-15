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

using NutaDev.CsLib.Maintenance.Exceptions.Factories;
using NutaDev.CsLib.Random.Providers.Abstract;
using NutaDev.CsLib.Resources.Text.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace NutaDev.CsLib.Internal.Tests.Utility.Mocks
{
    /// <summary>
    /// Provides pseudo random generator that returns values based on provided sequence.
    /// </summary>
    public class SequenceRandomProvider
        : IRandomProvider
    {
        /// <summary>
        /// Initializes a new instnace of the <see cref="SequenceRandomProvider"/> class.
        /// </summary>
        /// <param name="sequence">Sequence to use.</param>
        public SequenceRandomProvider(params int[] sequence)
        {
            Index = 0;
            Sequence = new ReadOnlyCollection<int>(sequence.ToList());
        }

        /// <summary>
        /// Provided sequence.
        /// </summary>
        public IReadOnlyCollection<int> Sequence { get; }

        /// <summary>
        /// Gets or sets current index.
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Indicates whether sequence should wrap or not.
        /// </summary>
        public bool Wrap { get; set; }

        /// <summary>
        /// Indicates whether sequence should return last element instead of throwing an exception.
        /// </summary>
        public bool AlwaysLast { get; set; }

        /// <summary>
        /// Returns next element.
        /// </summary>
        /// <returns>Next sequence element.</returns>
        public int Next()
        {
            return GetNext();
        }

        /// <summary>
        /// Returns next element.
        /// </summary>
        /// <param name="maxExclusive">Not used.</param>
        /// <returns></returns>
        public int Next(int maxExclusive)
        {
            return GetNext();
        }

        /// <summary>
        /// Returns next element.
        /// </summary>
        /// <param name="minInclusive">Not used.</param>
        /// <param name="maxExclusive">Not used.</param>
        /// <returns></returns>
        public int Next(int minInclusive, int maxExclusive)
        {
            return GetNext();
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <param name="content">Not used.</param>
        /// <returns>Nothing, not implemented.</returns>
        public int Roll(string content)
        {
            throw ExceptionFactory.NotImplementedException(nameof(SequenceRandomProvider));
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <param name="messageContent">Not used.</param>
        /// <returns>Nothing, not implemented.</returns>
        public bool IsValidRoll(string messageContent)
        {
            throw ExceptionFactory.NotImplementedException(nameof(SequenceRandomProvider));
        }

        /// <summary>
        /// Returns next element.
        /// </summary>
        /// <returns>Next sequence element.</returns>
        private int GetNext()
        {
            if (Index < Sequence.Count)
            {
                return Sequence.ElementAt(Index++);
            }

            if (Wrap)
            {
                Index = 0;
                return Sequence.ElementAt(Index);
            }

            if (AlwaysLast)
            {
                return Sequence.Last();
            }

            throw ExceptionFactory.InvalidOperationException(Text.NoMoreElementsInSequence);
        }
    }
}
