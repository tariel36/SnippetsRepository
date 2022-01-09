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

using NutaDev.CsLib.Maintenance.Exceptions.Factories;
using NutaDev.CsLib.Resources.Text.Exceptions;
using System;
using System.Text.RegularExpressions;

namespace NutaDev.CsLib.Formatting.Converters.Custom
{
    /// <summary>
    /// Provides conversion methods between <see cref="DateTime"/> and <see cref="string"/> representation of date and time in JSON file with format used by Microsoft.
    /// </summary>
    public class MicrosoftJsonDateTimeConverter
    {
        /// <summary>
        /// Regular expression for Microsoft JSON DateTime format.
        /// </summary>
        public static readonly Regex MicrosoftJsonDateTimeRegex = new Regex(@"\\?\/?DATE\((?'TICKS'[0-9]+)((?'SIGN'(\+|\-))(?'OFFSET'[0-9]+))?\)\\?\/?", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        /// <summary>
        /// Converts Microsfot JSON DateTime to <see cref="DateTime"/>.
        /// </summary>
        /// <param name="json">JSON to convert.</param>
        /// <returns>Converted <see cref="DateTime"/>.</returns>
        /// <seealso cref="MicrosoftJsonDateTimeRegex"/>
        public static DateTime FromMicrosoftJson(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                throw ExceptionFactory.ArgumentNullException(nameof(json));
            }

            Match match = MicrosoftJsonDateTimeRegex.Match(json);
            string sTicks = match.Groups["TICKS"].Value;
            string sSign = match.Groups["SIGN"].Value;
            string sOffset = match.Groups["OFFSET"].Value;

            if (string.IsNullOrWhiteSpace(sTicks))
            {
                throw ExceptionFactory.Create<InvalidOperationException>(Text.MissingTicks);
            }

            long ticks = long.Parse(sTicks);
            ticks = (ticks * 10000L) + 621355968000000000L;

            TimeSpan offSet = TimeSpan.Zero;

            if ((string.IsNullOrWhiteSpace(sSign) && !string.IsNullOrWhiteSpace(sOffset))
                || (!string.IsNullOrWhiteSpace(sSign) && string.IsNullOrWhiteSpace(sOffset)))
            {
                throw ExceptionFactory.Create<InvalidOperationException>(Text.InvalidOffset);
            }

            if (!string.IsNullOrWhiteSpace(sSign) && !string.IsNullOrWhiteSpace(sOffset))
            {
                string sHours = "0";
                string sMinutes = "0";

                if (sOffset.Length == 2)
                {
                    sHours = sOffset.Substring(0, 2);
                }
                else if (sOffset.Length == 4)
                {
                    sMinutes = sOffset.Substring(2, 2);
                }
                else
                {
                    throw ExceptionFactory.Create<InvalidOperationException>(Text.InvalidOffset);
                }

                int sign = sSign == "-"
                    ? -1
                    : 1;

                offSet = new TimeSpan(sign * int.Parse(sHours), int.Parse(sMinutes), 0);
            }

            ticks += offSet.Ticks;

            return new DateTime(ticks);
        }

        /// <summary>
        /// Converts <see cref="DateTime"/> to Microsfot JSON DateTime format.
        /// </summary>
        /// <param name="dt">DateTime to convert.</param>
        /// <returns>Converted <see cref="DateTime"/> to JSON.</returns>
        public static string ToMicrosoftJson(DateTime dt)
        {
            TimeSpan offset = TimeZone.CurrentTimeZone.GetUtcOffset(dt);

            long ticks = dt.Ticks;
            long offsetTicks = offset.Ticks;

            if (dt.Kind == DateTimeKind.Local)
            {
                ticks = ticks - offsetTicks;
            }

            ticks = (ticks - 621355968000000000L) / 10000L;

            return $"/Date({ticks}{(offsetTicks >= 0 ? "+" : "-")}{Math.Abs(offset.Hours):00}:{Math.Abs(offset.Minutes):00})/";
        }
    }
}
