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
using NutaDev.CsLib.Resources.Text.Exceptions;
using NutaDev.CsLib.Structures.Wrappers;
using System;
using System.Diagnostics;
using System.Threading;

namespace NutaDev.CsLib.Maintenance.Performance
{
    /// <summary>
    /// Provides high resolution execution measure.
    /// </summary>
    public class ExecutionTime
        : IDisposable
    {
        /// <summary>
        /// Multipler for miliseconds.
        /// </summary>
        public const long Mili = 1000L;

        /// <summary>
        /// Multipler for microseconds.
        /// </summary>
        public const long Micro = 1000L * 1000L;

        /// <summary>
        /// Multipler for nanoseconds.
        /// </summary>
        public const long Nano = 1000L * 1000L * 1000L;

        /// <summary>
        /// Stopwatch that measures execution time.
        /// </summary>
        private readonly Stopwatch _stopWatch = new Stopwatch();

        /// <summary>
        /// Multipler for <see cref="RawValue"/>.
        /// </summary>
        private readonly long _multipler;

        /// <summary>
        /// Elapsed time as <see cref="TimeSpan"/>. Backing field for <see cref="ElapsedTime"/>.
        /// </summary>
        private ValueWrapper<TimeSpan> _elapsedTime;

        /// <summary>
        /// Elapsed time as <see cref="long"/> (raw value). Backing field for <see cref="RawValue"/>.
        /// </summary>
        private ValueWrapper<long> _rawValue;

        /// <summary>
        /// Initializes a <see cref="ExecutionTime"/> class instance.
        /// Measurement starts immediately.
        /// </summary>
        public ExecutionTime()
            : this(new ValueWrapper<TimeSpan>(), new ValueWrapper<long>(), Mili)
        {

        }

        /// <summary>
        /// Initializes a <see cref="ExecutionTime"/> class instance.
        /// </summary>
        /// <param name="instantStart">Indicates whether time measurement should start immediatly.</param>
        public ExecutionTime(bool instantStart)
            : this(new ValueWrapper<TimeSpan>(), new ValueWrapper<long>(), Mili, instantStart)
        {

        }

        /// <summary>
        /// Initializes a <see cref="ExecutionTime"/> class instance.
        /// </summary>
        /// <param name="timeWrapper"><see cref="ValueWrapper{T}"/> for elapsed time.</param>
        public ExecutionTime(ValueWrapper<TimeSpan> timeWrapper)
            : this(timeWrapper, new ValueWrapper<long>(), Mili)
        {

        }

        /// <summary>
        /// Initializes a <see cref="ExecutionTime"/> class instance.
        /// </summary>
        /// <param name="timeWrapper"><see cref="ValueWrapper{T}"/> for elapsed time.</param>
        /// <param name="multipler">Multipler for <see cref="RawValue"/>.</param>
        public ExecutionTime(ValueWrapper<TimeSpan> timeWrapper, long multipler)
            : this(timeWrapper, new ValueWrapper<long>(), multipler)
        {

        }

        /// <summary>
        /// Initializes a <see cref="ExecutionTime"/> class instance.
        /// </summary>
        /// <param name="timeWrapper"><see cref="ValueWrapper{T}"/> for elapsed time.</param>
        /// <param name="rawValue"><see cref="ValueWrapper{T}"/> for raw value.</param>
        /// <param name="multipler">Multipler for <see cref="RawValue"/>.</param>
        /// <param name="instantStart">Indicates whether time measurement should start immediatly.</param>
        public ExecutionTime(ValueWrapper<TimeSpan> timeWrapper, ValueWrapper<long> rawValue, long multipler, bool instantStart = true)
        {
            if (!Stopwatch.IsHighResolution)
            {
                throw ExceptionFactory.Create<NotSupportedException>(Text.HighResolutionNotSupported);
            }

            _multipler = multipler;

            long seed = Environment.TickCount;

            Process.GetCurrentProcess().ProcessorAffinity = new IntPtr(0x0001);
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
            Thread.CurrentThread.Priority = ThreadPriority.Highest;

            ElapsedTime = timeWrapper;
            RawValue = rawValue;

            if (instantStart)
            {
                Start();
            }
        }

        /// <summary>
        /// Gets current elapsed time or null if not set. Sets elapsed time.
        /// </summary>
        public ValueWrapper<TimeSpan> ElapsedTime
        {
            get
            {
                if (_elapsedTime != null)
                {
                    _elapsedTime.Value = new TimeSpan(0, 0, 0, 0, (int)(_stopWatch.ElapsedTicks * 1000L / Stopwatch.Frequency));
                }

                return _elapsedTime;
            }
            set { _elapsedTime = value; }
        }

        /// <summary>
        /// Gets current elapsed time raw value or null if not set. Sets raw value.
        /// </summary>
        public ValueWrapper<long> RawValue
        {
            get
            {
                if (_rawValue != null)
                {
                    _rawValue = _stopWatch.ElapsedTicks * _multipler / Stopwatch.Frequency;
                }

                return _rawValue;
            }
            set { _rawValue = value; }
        }

        /// <summary>
        /// Starts time measurement.
        /// </summary>
        public void Start()
        {
            if (!_stopWatch.IsRunning)
            {
                _stopWatch.Start();
            }
        }

        /// <summary>
        /// Stops time measurement.
        /// </summary>
        public void Stop()
        {
            if (_stopWatch.IsRunning)
            {
                _stopWatch.Stop();
            }
        }

        /// <summary>
        /// Stops time measurement and disposes the object.
        /// </summary>
        public void Dispose()
        {
            Stop();
        }
    }
}
