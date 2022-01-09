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

using System.Collections.Generic;

namespace NutaDev.CsLib.Audio.Formats.Cue
{
    /// <summary>
    /// Cue track.
    /// </summary>
    public class CueTrack
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CueTrack"/> class.
        /// </summary>
        public CueTrack()
        {
            Indexes = new List<CueIndex>();
        }

        /// <summary>
        /// Gets or sets track title.
        /// </summary>
        public string Title { get; internal set; }

        /// <summary>
        /// Gets or sets track performer.
        /// </summary>
        public string Performer { get; internal set; }

        /// <summary>
        /// Gets or sets track composer.
        /// </summary>
        public string Composer { get; internal set; }

        /// <summary>
        /// Gets or sets ISRC.
        /// </summary>
        public string ISRC { get; internal set; }

        /// <summary>
        /// Gets or sets track type.
        /// </summary>
        public string Type { get; internal set; }

        /// <summary>
        /// Gets or sets track index.
        /// </summary>
        public int Index { get; internal set; }

        /// <summary>
        /// Gets or sets the collection of indexes.
        /// </summary>
        public IReadOnlyCollection<CueIndex> Indexes { get; internal set; }

        /// <summary>
        /// Gets or sets the collection of properties.
        /// </summary>
        public IReadOnlyCollection<CueCustomProperty> Properties { get; internal set; }
    }
}
