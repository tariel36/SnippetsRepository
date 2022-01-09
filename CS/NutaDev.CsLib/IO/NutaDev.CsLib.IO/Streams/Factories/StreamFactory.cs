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

using System.IO;
using System.Text;

namespace NutaDev.CsLib.IO.Streams.Factories
{
    /// <summary>
    /// Factory that creates various <see cref="Stream"/> instances.
    /// </summary>
    public static class StreamFactory
    {
        /// <summary>
        /// <para>Taken from <see cref="StreamReader.DefaultBufferSize"/> in StreamReader.cs.</para>
        /// <para>Taken from <see cref="StreamWriter.DefaultBufferSize"/> in StreamWriter.cs.</para>
        /// </summary>
        public const int DefaultBufferSize = 1024;

        /// <summary>
        /// <para>Taken from <see cref="StreamWriter.UTF8NoBOM"/> in StreamWriter.cs.</para>
        /// </summary>
        public static readonly Encoding Utf8NoBom = new UTF8Encoding(false, true);

        /// <summary>
        /// Creates default <see cref="StreamReader"/> instance.
        /// </summary>
        /// <param name="stream"><see cref="Stream"/> to read.</param>
        /// <returns>Default <see cref="StreamReader"/> instance.</returns>
        public static StreamReader CreateStreamReader(Stream stream)
        {
            return new StreamReader(stream, Encoding.UTF8, true, DefaultBufferSize, true);
        }

        /// <summary>
        /// Creates default <see cref="StreamWriter"/> instance.
        /// </summary>
        /// <param name="stream"><see cref="Stream"/> to write.</param>
        /// <returns>Default <see cref="StreamWriter"/> instance.</returns>
        public static StreamWriter CreateStreamWriter(Stream stream)
        {
            return new StreamWriter(stream, Utf8NoBom, DefaultBufferSize, true);
        }
    }
}
