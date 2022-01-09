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

using NutaDev.CsLib.Types.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NutaDev.CsLib.Types.Extensions
{
    /// <summary>
    /// This class provides extension methods for <see cref="DateTime"/> structure.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Week length.
        /// </summary>
        private const int WeekLength = 7;

        /// <summary>
        /// Checks if the date is a working day.
        /// </summary>
        /// <param name="date">Date to check.</param>
        /// <param name="holidays">List of holidays.</param>
        /// <param name="offDays">Days of week that are considered off days. If null then <see cref="DateTimes.DefaultOffDays"/> are used.</param>
        /// <returns>True if date is workday, false otherwise.</returns>
        public static bool IsWorkday(this DateTime date, List<DateTime> holidays = null, List<DayOfWeek> offDays = null)
        {
            if (offDays == null)
            {
                offDays = DateTimeHelper.DefaultOffDays.ToList();
            }

            bool isHoliday = false;
            if (holidays != null)
            {
                isHoliday = holidays.Any(x => x.Year == date.Year && x.Month == date.Month && x.Day == date.Day);
            }

            return !offDays.Contains(date.DayOfWeek) && !isHoliday;
        }

        /// <summary>
        /// Calculates <see cref="DateTime"/> with start of week.
        /// </summary>
        /// <param name="dateTime">Source <see cref="DateTime"/>.</param>
        /// <param name="startOfWeek">Day that's a start of week.</param>
        /// <returns><see cref="DateTime"/> with start of a week.</returns>
        public static DateTime StartOfWeek(this DateTime dateTime, DayOfWeek startOfWeek)
        {
            return AddDays(dateTime, -1, (WeekLength + (dateTime.DayOfWeek - startOfWeek)) % WeekLength);
        }

        /// <summary>
        /// Calculates <see cref="DateTime"/> with end of week.
        /// </summary>
        /// <param name="dateTime">Source <see cref="DateTime"/>.</param>
        /// <param name="endOfWeek">Day that's a end of week.</param>
        /// <returns><see cref="DateTime"/> with end of a week.</returns>
        public static DateTime EndOfWeek(this DateTime dateTime, DayOfWeek endOfWeek)
        {
            return AddDays(dateTime, 1, (WeekLength - ((int)dateTime.DayOfWeek + (int)endOfWeek)) % WeekLength);
        }

        /// <summary>
        /// Adds days to date.
        /// </summary>
        /// <param name="dateTime">Base datetime.</param>
        /// <param name="mod">Day's modifier.</param>
        /// <param name="days">Days to add.</param>
        /// <returns>New date.</returns>
        private static DateTime AddDays(DateTime dateTime, int mod, int days)
        {
            return dateTime.AddDays(mod * days).Date;
        }
    }
}
