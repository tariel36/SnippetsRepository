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

using NutaDev.CsLib.Audio.Formats.Cue;
using NutaDev.CsLib.Maintenance.Exceptions.Factories;
using NutaDev.CsLib.Resources.Text.Exceptions;
using System;
using System.Linq;
using System.Text;

namespace NutaDev.CsLib.Audio.Converters.Specific
{
    /// <summary>
    /// Converts from <see cref="CueFile"/> to audacity labels.
    /// </summary>
    public class CueToAudacityLabelsConverter
    {
        /// <summary>
        /// Converts <paramref name="cue"/> into audacity labels.
        /// </summary>
        /// <param name="cue">Cue to convert.</param>
        /// <returns>Audacity labels.</returns>
        public string Convert(CueFile cue)
        {
            StringBuilder sb = new StringBuilder();

            if (cue.Discs.Count > 0)
            {
                CueDisc cueFile = cue.Discs.First();

                if (cueFile.Tracks.Count == 0)
                {
                    throw ExceptionFactory.Create<InvalidOperationException>(Text.NoTracksInCueFile_0_, cueFile.DiscName);
                }

                foreach (CueTrack track in cueFile.Tracks)
                {
                    CueIndex lastIndex = track.Indexes.OrderBy(x => x.Time).Last();

                    decimal seconds = (int)lastIndex.Time.TotalSeconds;
                    decimal miliseconds = (new decimal(lastIndex.Time.TotalSeconds) - seconds) * 100m;
                    decimal frames = miliseconds / 75m;

                    decimal duration = seconds + frames;

                    sb.AppendLine($"{duration:0.000000}\t{duration:0.000000}\t{track.Title}");
                }
            }
            else
            {
                throw ExceptionFactory.Create<InvalidOperationException>(Text.NoDiscsInCue_0_, cue.Path);
            }

            return sb.ToString();
        }
    }
}
