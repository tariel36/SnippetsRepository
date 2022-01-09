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

using System;
using NutaDev.CsLib.Maintenance.Exceptions.Factories;
using NutaDev.CsLib.Resources.Text.Exceptions;

namespace NutaDev.CsLib.Structures.Values
{
    /// <summary>
    /// Class that contains value restricted by minimum and maximum value.
    /// </summary>
    public class CarouselValue
    {
        /// <summary>
        /// Backing field for <see cref="CurrentValue"/>.
        /// </summary>
        private int _currentValue;

        /// <summary>
        /// Backing field for <see cref="Wrap"/>.
        /// </summary>
        private bool _wrap;

        /// <summary>
        /// Backing field for <see cref="ExclusiveMinimum"/>.
        /// </summary>
        private bool _exclusiveMinimum;

        /// <summary>
        /// Backing field for <see cref="ExclusiveMaximum"/>.
        /// </summary>
        private bool _exclusiveMaximum;

        /// <summary>
        /// Initializes a new instance of the <see cref="CarouselValue"/> class.
        /// </summary>
        /// <param name="min">Constant minimum value.</param>
        /// <param name="max">Constant maximum value.</param>
        /// <param name="current">Current value.</param>
        /// <param name="wrap">Determines whether current value should be wrapped.</param>
        /// <param name="exclusiveMinimum">Determines whether minimum is an exclusive value.</param>
        /// <param name="exclusiveMaximum">Determines whether maximum is an exclusive value.</param>
        public CarouselValue(int min, int max, int current, bool wrap = true, bool exclusiveMinimum = false, bool exclusiveMaximum = true)
            : this(() => min, () => max, current, wrap, exclusiveMinimum, exclusiveMaximum)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarouselValue"/> class.
        /// </summary>
        /// <param name="minimumSource">Delegate that returns minimum value.</param>
        /// <param name="maximumSource">Delegate that returns maximum value.</param>
        /// <param name="current">Current value.</param>
        /// <param name="wrap">Determines whether current value should be wrapped.</param>
        /// <param name="exclusiveMinimum">Determines whether minimum is an exclusive value.</param>
        /// <param name="exclusiveMaximum">Determines whether maximum is an exclusive value.</param>
        public CarouselValue(Func<int> minimumSource, Func<int> maximumSource, int current, bool wrap = true, bool exclusiveMinimum = false, bool exclusiveMaximum = true)
        {
            _wrap = wrap;
            _exclusiveMinimum = exclusiveMinimum;
            _exclusiveMaximum = exclusiveMaximum;

            MinimumSource = minimumSource;
            MaximumSource = maximumSource;

            CurrentValue = current;
        }

        /// <summary>
        /// Gets maximum value.
        /// </summary>
        public int MaximumValue { get { return MaximumSource(); } }

        /// <summary>
        /// Gets minimum value.
        /// </summary>
        public int MinimumValue { get { return MinimumSource(); } }

        /// <summary>
        /// Indicates whether <see cref="CarouselValue"/> is either <see cref="ExclusiveMinimum"/>, <see cref="ExclusiveMaximum"/> or both.
        /// </summary>
        public bool Exclusive { get { return ExclusiveMaximum || ExclusiveMinimum; } }

        /// <summary>
        /// Gets or sets current value.
        /// </summary>
        public int CurrentValue
        {
            get { return _currentValue; }
            set { EnsureCurrentValue(value); }
        }

        /// <summary>
        /// Indicates whether <see cref="MaximumValue"/> is exclusive.
        /// </summary>
        public bool ExclusiveMaximum
        {
            get { return _exclusiveMaximum; }
            set
            {
                _exclusiveMaximum = value;
                EnsureCurrentValue();
            }
        }

        /// <summary>
        /// Indicates whether <see cref="MinimumValue"/> is exclusive.
        /// </summary>
        public bool ExclusiveMinimum
        {
            get { return _exclusiveMinimum; }
            set
            {
                _exclusiveMinimum = value;
                EnsureCurrentValue();
            }
        }

        /// <summary>
        /// Indicates whether <see cref="CurrentValue"/> should be wrapped.
        /// </summary>
        public bool Wrap
        {
            get { return _wrap; }
            set
            {
                _wrap = value;
                EnsureCurrentValue();
            }
        }

        /// <summary>
        /// Gets delegate that returns minimum value.
        /// </summary>
        private Func<int> MinimumSource { get; }

        /// <summary>
        /// Gets delegate that returns maximum value.
        /// </summary>
        private Func<int> MaximumSource { get; }

        /// <summary>
        /// Sets maximum value.
        /// </summary>
        public void SetMax()
        {
            CurrentValue = MaximumValue;
        }

        /// <summary>
        /// Sets minimum value.
        /// </summary>
        public void SetMin()
        {
            CurrentValue = MinimumValue;
        }

        /// <summary>
        /// Increments current value.
        /// </summary>
        public void Increment()
        {
            CurrentValue++;
        }

        /// <summary>
        /// Decrements current value.
        /// </summary>
        public void Decrement()
        {
            CurrentValue--;
        }

        /// <summary>
        /// Ensures that <see cref="CurrentValue"/> is valid.
        /// </summary>
        private void EnsureCurrentValue()
        {
            EnsureCurrentValue(CurrentValue);
        }

        /// <summary>
        /// Ensures that <paramref name="value"/> is valid.
        /// </summary>
        /// <param name="value">Value to validate.</param>
        private void EnsureCurrentValue(int value)
        {
            if (ExclusiveMaximum && ExclusiveMinimum && MinimumValue == MaximumValue)
            {
                throw ExceptionFactory.Create<InvalidOperationException>(Text.MinimumIsEqualToMaximumWithExlusiveFlagsMinimum_0_Maximum_1_ExclusiveMaximum_2_ExclusiveMinimum_3_, MinimumValue, MaximumValue, ExclusiveMaximum, ExclusiveMinimum);
            }

            if (ExclusiveMaximum)
            {
                if (value >= MaximumValue)
                {
                    if (Wrap)
                    {
                        value = ExclusiveMinimum ? MinimumValue + 1 : MinimumValue;
                    }
                    else
                    {
                        value = MaximumValue - 1;
                    }
                }
            }
            else
            {
                if (value > MaximumValue)
                {
                    if (Wrap)
                    {
                        value = ExclusiveMinimum ? MinimumValue + 1 : MinimumValue;
                    }
                    else
                    {
                        value = MaximumValue;
                    }
                }
            }

            if (ExclusiveMinimum)
            {
                if (value <= MinimumValue)
                {
                    if (Wrap)
                    {
                        value = ExclusiveMaximum ? MaximumValue - 1 : MaximumValue;
                    }
                    else
                    {
                        value = MinimumValue + 1;
                    }
                }
            }
            else
            {
                if (value < MinimumValue)
                {
                    if (Wrap)
                    {
                        value = ExclusiveMaximum ? MaximumValue - 1 : MaximumValue;
                    }
                    else
                    {
                        value = MinimumValue;
                    }
                }
            }

            _currentValue = value;
        }
    }
}
