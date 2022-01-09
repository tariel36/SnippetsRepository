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

namespace NutaDev.CsLib.IO.Serialization.Abstract
{
    /// <summary>
    /// Serializes object to bytes and deserializes bytes into object.
    /// </summary>
    /// <typeparam name="T">Object type.</typeparam>
    public interface IBytesSerializer<T>
    {
        /// <summary>
        /// Serializes <paramref name="obj"/> to byte array.
        /// </summary>
        /// <param name="obj">Object to serialize.</param>
        /// <returns>Byte array or null.</returns>
        byte[] ToBytes(object obj);

        /// <summary>
        /// Deserializes <paramref name="bytes"/> to object.
        /// </summary>
        /// <param name="bytes">Bytes to deserialize.</param>
        /// <returns>Object instance or default value.</returns>
        T Deserialize(byte[] bytes);
    }
}
