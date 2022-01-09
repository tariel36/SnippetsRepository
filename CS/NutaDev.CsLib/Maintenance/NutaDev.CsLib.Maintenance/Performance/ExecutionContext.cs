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

using NutaDev.CsLib.Types.Extensions;
using System;

namespace NutaDev.CsLib.Maintenance.Performance
{
    /// <summary>
    /// Method execution context class.
    /// </summary>
    public class ExecutionContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExecutionContext"/> class.
        /// </summary>
        /// <param name="methodName">Method name.</param>
        public ExecutionContext(string methodName)
        {
            Name = methodName;
            ExecutionTime = new ExecutionTime(false);
            Hash = Guid.NewGuid().Hash();
        }

        /// <summary>
        /// Gets method name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets context hash.
        /// </summary>
        public string Hash { get; }

        /// <summary>
        /// Gets <see cref="Time"/> string.
        /// </summary>
        public string TimeString { get { return Time.ToString("G"); } }

        /// <summary>
        /// Gets current execution time.
        /// </summary>
        public TimeSpan Time { get { return ExecutionTime.ElapsedTime.Value; } }

        /// <summary>
        /// Gets <see cref="ExecutionTime"/> instance.
        /// </summary>
        private ExecutionTime ExecutionTime { get; }

        /// <summary>
        /// Starts execution time measurement.
        /// </summary>
        /// <returns></returns>
        public ExecutionTime Start()
        {
            ExecutionTime.Start();
            return ExecutionTime;
        }

        /// <summary>
        /// Stops execution time measurement.
        /// </summary>
        public void Stop()
        {
            ExecutionTime.Stop();
        }
    }
}
