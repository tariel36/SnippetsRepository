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

namespace NutaDev.CsLib.Audio.Formats.Cue
{
    /// <summary>
    /// Single disc within CUE file.
    /// </summary>
    public class CueDisc
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CueDisc"/> class.
        /// </summary>
        public CueDisc()
        {
            Tracks = new ArraySegment<CueTrack>();
        }

        /// <summary>
        /// Gets or sets name of disc.
        /// </summary>
        public string DiscName { get; set; }

        /// <summary>
        /// Gets ors sets type of disc.
        /// </summary>
        public string DiscType { get; set; }

        /// <summary>
        /// Gets or sets track's on disc.
        /// </summary>
        public IReadOnlyCollection<CueTrack> Tracks { get; internal set; }

        /// <summary>
        /// Gets or sets track's properties.
        /// </summary>
        public IReadOnlyCollection<CueCustomProperty> Properties { get; internal set; }
    }
}
