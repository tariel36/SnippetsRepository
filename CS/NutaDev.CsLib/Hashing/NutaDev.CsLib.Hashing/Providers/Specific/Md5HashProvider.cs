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

using NutaDev.CsLib.Maintenance.Exceptions.Abstract;
using NutaDev.CsLib.Maintenance.Exceptions.Delegates;
using NutaDev.CsLib.Types.Extensions;
using System;
using System.Text;

namespace NutaDev.CsLib.Hashing.Providers.Specific
{
    /// <summary>
    /// Class that provides MD5 generation methods.
    /// </summary>
    public class Md5HashProvider
        : IDisposable
        , IExceptionHandler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Md5HashProvider"/> class.
        /// </summary>
        public Md5HashProvider()
        {
            Md5 = System.Security.Cryptography.MD5.Create();
        }

        /// <summary>
        /// Gets or sets the exception handler.
        /// </summary>
        public ExceptionHandlerDelegate ExceptionHandler { get; set; }

        /// <summary>
        /// Gets or sets Md5.
        /// </summary>
        private System.Security.Cryptography.MD5 Md5 { get; set; }

        /// <summary>
        /// Converts <paramref name="input"/> to Md5 hash using <see cref="Encoding.ASCII"/>.
        /// </summary>
        /// <param name="input">Input to convert.</param>
        /// <returns>Md5 hash.</returns>
        public static string GetOnce(string input)
        {
            return GetOnce(input, Encoding.ASCII);
        }

        /// <summary>
        /// Converts <paramref name="input"/> to Md5 hash.
        /// </summary>
        /// <param name="input">Input to convert.</param>
        /// <param name="encoding">Encoding to use.</param>
        /// <returns>Md5 hash.</returns>
        public static string GetOnce(string input, Encoding encoding)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = encoding.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                return hashBytes.ToHexString();
            }
        }

        /// <summary>
        /// Converts <paramref name="input"/> to Md5 hash using <see cref="Encoding.ASCII"/>.
        /// </summary>
        /// <param name="input">Input to convert.</param>
        /// <returns>Md5 hash.</returns>
        public string Get(string input)
        {
            return Get(input, Encoding.ASCII);
        }

        /// <summary>
        /// Converts <paramref name="input"/> to Md5 hash.
        /// </summary>
        /// <param name="input">Input to convert.</param>
        /// <param name="encoding">Encoding to use.</param>
        /// <returns>Md5 hash.</returns>
        public string Get(string input, Encoding encoding)
        {
            byte[] inputBytes = encoding.GetBytes(input);
            byte[] hashBytes = Md5.ComputeHash(inputBytes);

            return hashBytes.ToHexString();
        }

        /// <summary>
        /// Disposes this object.
        /// </summary>
        public void Dispose()
        {
            try
            {
                Md5?.Dispose();
            }
            catch (Exception ex)
            {
                ExceptionHandler?.Invoke(ex);
            }
            finally
            {
                Md5 = null;
            }
        }
    }
}
