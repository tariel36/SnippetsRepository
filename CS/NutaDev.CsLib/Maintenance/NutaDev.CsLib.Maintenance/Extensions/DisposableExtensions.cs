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

using NutaDev.CsLib.Maintenance.Exceptions.Delegates;
using System;

namespace NutaDev.CsLib.Maintenance.Extensions
{
    /// <summary>
    /// Provides extension methods for <see cref="IDisposable"/> type.
    /// </summary>
    public static class DisposableExtensions
    {
        /// <summary>
        /// Performs safe dispose on object.
        /// </summary>
        /// <param name="disposable">Disposable to dispose.</param>
        /// <param name="exceptionHandler">Exception handler callback.</param>
        /// <returns>True if object has been disposes, false otherwise.</returns>
        public static bool SafeDispose(this IDisposable disposable, ExceptionHandlerDelegate exceptionHandler = null)
        {
            if (disposable == null) { return false; }
            if (disposable is Core.Interfaces.IDisposable flagDisposable && flagDisposable.IsDisposed) { return true; }

            try
            {
                disposable.Dispose();
            }
            catch (Exception ex)
            {
                exceptionHandler?.Invoke(ex);

                return false;
            }

            return true;
        }
    }
}
