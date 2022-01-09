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
using NutaDev.CsLib.Resources.Text.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace NutaDev.CsLib.Maintenance.Exceptions.Factories
{
    /// <summary>
    /// Static class that allows easy creation of popular exceptions.
    /// </summary>
    public static class ExceptionFactory
    {
        /// <summary>
        /// Creates <see cref="System.NotSupportedException"/>.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="args">Message arguments.</param>
        /// <returns>Exception object.</returns>
        public static Exception NotSupportedException(string message, params object[] args)
        {
            return Create<NotSupportedException>(message, args);
        }
        /// <summary>
        /// Creates <see cref="System.InvalidOperationException"/>.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="args">Message arguments.</param>
        /// <returns>Exception object.</returns>
        public static Exception InvalidOperationException(string message, params object[] args)
        {
            return Create<InvalidOperationException>(message, args);
        }

        /// <summary>
        /// Creates <see cref="System.IO.FileNotFoundException"/>.
        /// </summary>
        /// <param name="filePath">Path to file.</param>
        /// <returns>Instance of <see cref="System.FileNotFoundException"/>.</returns>
        public static Exception FileNotFoundException(string filePath)
        {
            return Create<FileNotFoundException>(Text.File_0_NotFound, filePath);
        }

        /// <summary>
        /// Gets or sets the exception handler.
        /// </summary>
        public static ExceptionHandlerDelegate ExceptionHandler { get; set; }

        /// <summary>
        /// Creates <see cref="System.AggregateException"/>.
        /// </summary>
        /// <param name="exceptions">Exceptions to aggregate.</param>
        /// <returns>Instance of <see cref="System.AggregateException"/>.</returns>
        public static Exception AggregateException(IEnumerable<Exception> exceptions)
        {
            return new AggregateException(exceptions);
        }

        /// <summary>
        /// Creates <see cref="System.AggregateException"/>.
        /// </summary>
        /// <param name="exceptions">Exceptions to aggregate.</param>
        /// <returns>Instance of <see cref="System.AggregateException"/>.</returns>
        public static Exception AggregateException(params Exception[] exceptions)
        {
            return new AggregateException(exceptions);
        }

        /// <summary>
        /// Creates <see cref="System.NotImplementedException"/>.
        /// </summary>
        /// <param name="caller">Caller name.</param>
        /// <param name="callerMember">Caller member name.</param>
        /// <returns>Exception object.</returns>
        public static Exception NotImplementedException(string caller, [CallerMemberName] string callerMember = null)
        {
            return Create<NotImplementedException>(Text._0_IsNotImplemented, $"{caller}.{callerMember}");
        }

        /// <summary>
        /// Creates <see cref="System.ArgumentNullException"/>.
        /// </summary>
        /// <param name="argumentName">Name of argument that's null.</param>
        /// <returns>Exception object.</returns>
        public static Exception ArgumentNullException(string argumentName)
        {
            return Create<ArgumentNullException>(Text.Argument_0_IsNull, argumentName);
        }

        /// <summary>
        /// Creates <see cref="System.ArgumentOutOfRangeException"/>.
        /// </summary>
        /// <param name="argumentName">Name of argument that's out of null.</param>
        /// <returns>Exception object.</returns>
        public static Exception ArgumentOutOfRangeException(string argumentName)
        {
            return Create<ArgumentOutOfRangeException>(Text.Argument_0_IsOutOfRange, argumentName);
        }

        /// <summary>
        /// Creates an <typeparamref name="T"/> object.
        /// </summary>
        /// <typeparam name="T">Exception type.</typeparam>
        /// <param name="message">Exception message.</param>
        /// <param name="args">Message arguments.</param>
        /// <returns>Exception object.</returns>
        public static T Create<T>(string message, params object[] args)
            where T : Exception
        {
            return (T)Create(typeof(T), message, null, args);
        }

        /// <summary>
        /// Creates an <typeparamref name="T"/> object.
        /// </summary>
        /// <typeparam name="T">Exception type.</typeparam>
        /// <param name="message">Exception message.</param>
        /// <param name="innerException">Inner exception.</param>
        /// <param name="args">Message arguments.</param>
        /// <returns>Exception object.</returns>
        public static T Create<T>(string message, Exception innerException, params object[] args)
            where T : Exception
        {
            return (T)Create(typeof(T), message, innerException, args);
        }

        /// <summary>
        /// Creates an <see cref="Exception"/> object.
        /// </summary>
        /// <param name="type">Exception type.</param>
        /// <param name="message">Exception message.</param>
        /// <param name="innerException">Inner exception.</param>
        /// <param name="args">Message arguments.</param>
        /// <returns>Exception object.</returns>
        public static Exception Create(Type type, string message, Exception innerException, params object[] args)
        {
            Exception instance;

            if (!typeof(Exception).IsAssignableFrom(type))
            {
                type = typeof(Exception);
            }

            bool hasDefaultCtor = type.GetConstructors().Any(x => x.GetParameters().Length == 0);
            bool hasMessageCtor = type.GetConstructors().Any(x => x.GetParameters().Length > 0 && x.GetParameters()[0].ParameterType == typeof(string));
            bool hasMessageInnerExceptionCtor = type.GetConstructors().Any(x => x.GetParameters().Length > 1 && x.GetParameters()[0].ParameterType == typeof(string) && x.GetParameters()[1].ParameterType == typeof(Exception));

            if (!hasMessageCtor)
            {
                message = null;
            }

            if (string.IsNullOrWhiteSpace(message))
            {
                if (!hasDefaultCtor)
                {
                    type = typeof(Exception);
                }

                instance = (Exception)Activator.CreateInstance(type);
            }
            else
            {
                if (args?.Length > 0)
                {
                    try
                    {
                        message = string.Format(message, args);
                    }
                    catch (Exception ex)
                    {
                        ExceptionHandler?.Invoke(ex);
                    }
                }

                if (hasMessageInnerExceptionCtor)
                {
                    instance = (Exception)Activator.CreateInstance(type, message, innerException);
                }
                else
                {
                    instance = (Exception)Activator.CreateInstance(type, message);
                }
            }

            return instance;
        }
    }
}
