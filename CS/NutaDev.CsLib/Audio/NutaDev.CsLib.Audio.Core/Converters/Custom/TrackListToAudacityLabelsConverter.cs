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
using System.Text;
using System.Text.RegularExpressions;

namespace NutaDev.CsLib.Audio.Converters.Custom
{
    /// <summary>
    /// Converts list of tracks in format `((?<hour>[0-9]+):)?((?<min>[0-9]+):)(?<sec>[0-9]+) - (?<title>.*)` into audacity labels.
    /// </summary>
    public class TrackListToAudacityLabelsConverter
    {
        /// <summary>
        /// Converts list of tracks into audacity labels.
        /// </summary>
        /// <param name="lines">Tracks to convert.</param>
        /// <returns>Audacity labels.</returns>
        public string Convert(string[] lines)
        {
            Regex regex = new Regex("((?<hour>[0-9]+):)?((?<min>[0-9]+):)(?<sec>[0-9]+) - (?<title>.*)", RegexOptions.Compiled);

            StringBuilder sbOut = new StringBuilder();

            for (int i = 0; i < lines.Length - 1; ++i)
            {
                string curr = lines[i];
                string next = lines[i + 1];

                Tuple<string, TimeSpan> infoCurr = GetTimeSpan(regex, curr);
                Tuple<string, TimeSpan> infoNext = GetTimeSpan(regex, next);

                TimeSpan durationCurr = infoNext.Item2 - infoCurr.Item2;

                sbOut.AppendLine(GetAudacityLine(infoCurr, durationCurr));
            }

            return sbOut.ToString();
        }

        /// <summary>
        /// Returns tuple of title and duration.
        /// </summary>
        /// <param name="regex">Input regex.</param>
        /// <param name="line">Line to parse.</param>
        /// <returns>Title and duration.</returns>
        private Tuple<string, TimeSpan> GetTimeSpan(Regex regex, string line)
        {
            Match match = regex.Match(line);

            string sHour = match.Groups["hour"].Value;
            string sMin = match.Groups["min"].Value;
            string sSec = match.Groups["sec"].Value;
            string title = match.Groups["title"].Value;

            return Tuple.Create(title, new TimeSpan(SafeToInt(sHour), SafeToInt(sMin), SafeToInt(sSec)));
        }

        /// <summary>
        /// Converts to int32 with additional checks.
        /// </summary>
        /// <param name="str">Text value.</param>
        /// <returns>Integer value.</returns>
        private int SafeToInt(string str)
        {
            return string.IsNullOrWhiteSpace(str) ? 0 : System.Convert.ToInt32(str);
        }

        /// <summary>
        /// Creates audacity lables line.
        /// </summary>
        /// <param name="tIn">Title and start.</param>
        /// <param name="inDuration">Duration.</param>
        /// <returns>Audacity label line.</returns>
        private string GetAudacityLine(Tuple<string, TimeSpan> tIn, TimeSpan inDuration)
        {
            TimeSpan inStart = tIn.Item2;
            string inTitle = tIn.Item1;

            decimal startFrames = CalculateDuration(inStart);
            decimal durationFrames = CalculateDuration(inDuration);

            decimal frames = startFrames;

            return $"{frames:0.000000}\t{frames:0.000000}\t{inTitle}";
        }

        /// <summary>
        /// Calculates duration of track for audacity.
        /// </summary>
        /// <param name="inDuration">Duration in seconds.</param>
        /// <returns>Duration in frames.</returns>
        private decimal CalculateDuration(TimeSpan inDuration)
        {
            decimal seconds = (int)inDuration.TotalSeconds;
            decimal miliseconds = (new decimal(inDuration.TotalSeconds) - seconds) * 100m;
            decimal frames = miliseconds / 75m;

            return seconds + frames;
        }
    }
}
