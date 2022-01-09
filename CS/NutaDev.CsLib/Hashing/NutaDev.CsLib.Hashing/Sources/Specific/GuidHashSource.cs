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

using NutaDev.CsLib.Hashing.Sources.Abstract;
using System;
using System.Linq;

namespace NutaDev.CsLib.Hashing.Sources.Specific
{
    /// <summary>
    /// Provides <see cref="string"/> hash value based on <see cref="Guid"/>.
    /// </summary>
    public class GuidHashSource
        : IHashSource<Guid>
    {
        /// <summary>
        /// Returns hash based on particular <see cref="Guid"/>.
        /// </summary>
        /// <param name="value">Guid that is base for hash generation.</param>
        /// <returns>Generated hash.</returns>
        public string Get(Guid value)
        {
            return value.ToString().Split('-').First();
        }

        /// <summary>
        /// Returns hash based on random <see cref="Guid"/>.
        /// </summary>
        /// <returns>Generated hash.</returns>
        public string Get()
        {
            return Get(Guid.NewGuid());
        }
    }
}
