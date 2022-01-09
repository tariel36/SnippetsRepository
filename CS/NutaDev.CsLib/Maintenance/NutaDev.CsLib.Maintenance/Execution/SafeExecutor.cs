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
using System.Collections.Generic;

namespace NutaDev.CsLib.Maintenance.Execution
{
    /// <summary>
    /// Safe execution context.
    /// </summary>
    public class SafeExecutor
    {
        /// <summary>
        /// Executes action (<see cref="deleg"/>) in safe context.
        /// </summary>
        /// <param name="deleg">Action to perform.</param>
        /// <returns>True if action ended successfully, otherwise false.</returns>
        public bool TryExecute(Action deleg)
        {
            return TryExecute(deleg, 1, out AggregateException _);
        }

        /// <summary>
        /// Executes action (<see cref="deleg"/>) in safe context.
        /// </summary>
        /// <param name="deleg">Action to perform.</param>
        /// <param name="maxTryCount">Retry count.</param>
        /// <param name="aggregateException"><see cref="AggregateException"/> object that aggregates all exceptions.</param>
        /// <returns>True if action ended successfully, otherwise false.</returns>
        public bool TryExecute(Action deleg, int maxTryCount, out AggregateException aggregateException)
        {
            return TryExecute(deleg, maxTryCount, out aggregateException, false);
        }

        /// <summary>
        /// Executes action (<see cref="deleg"/>) in safe context.
        /// </summary>
        /// <param name="deleg">Action to perform.</param>
        /// <param name="maxTryCount">Retry count.</param>
        /// <param name="aggregateException"><see cref="AggregateException"/> object that aggregates all exceptions.</param>
        /// <param name="throwOnException">If set to true, throws <paramref name="aggregateException"/>.</param>
        /// <returns>True if action ended successfully, otherwise false.</returns>
        public bool TryExecute(Action deleg, int maxTryCount, out AggregateException aggregateException, bool throwOnException)
        {
            aggregateException = null;

            List<Exception> exceptions = new List<Exception>();

            for (int i = 0; i < maxTryCount; ++i)
            {
                try
                {
                    deleg();
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }
            }

            if (exceptions.Count > 0)
            {
                aggregateException = new AggregateException(exceptions);

                if (throwOnException)
                {
                    throw aggregateException;
                }
            }

            return exceptions.Count == 0;
        }
    }
}
