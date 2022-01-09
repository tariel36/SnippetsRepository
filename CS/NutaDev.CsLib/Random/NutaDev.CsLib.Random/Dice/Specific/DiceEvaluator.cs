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
using NutaDev.CsLib.Random.Dice.Abstract;
using NutaDev.CsLib.Random.Services.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace NutaDev.CsLib.Random.Dice.Specific
{
    /// <summary>
    /// Dice evaluator class.
    /// </summary>
    public class DiceEvaluator
        : IDiceEvaluator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DiceEvaluator"/> class.
        /// </summary>
        /// <param name="service">Random service instance.</param>
        public DiceEvaluator(IRandomService service)
        {
            RandomService = service;
        }

        /// <summary>
        /// Random service instance.
        /// </summary>
        private IRandomService RandomService { get; }

        /// <summary>
        /// Returns collection of dice rolls.
        /// </summary>
        /// <param name="count">Number of dices.</param>
        /// <param name="side">Number of side.</param>
        /// <returns>Collection of reults.</returns>
        public ICollection<int> Evaluate(int count, int side)
        {
            return RandomService.Roll(count, side);
        }

        /// <summary>
        /// Returns collection of dice rolls.
        /// </summary>
        /// <param name="definitions">Definitions to roll.</param>
        /// <returns>Collection of reults.</returns>
        public ICollection<int> Evaluate(params DiceDefinition[] definitions)
        {
            if (definitions == null) { throw ExceptionFactory.ArgumentNullException(nameof(definitions)); }

            return definitions.Select(x => RandomService.Roll(1, x.Sides).First() + x.Modifier).ToList();
        }
    }
}
