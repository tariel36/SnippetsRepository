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
using System.Linq;

namespace NutaDev.CsLib.Types.Parsers
{
    public class EnumParser
    {
        /// <summary>
        /// Parses any value to enum value if possible. Also ensures that value is defined within enum.
        /// </summary>
        /// <typeparam name="T">Enum type.</typeparam>
        /// <param name="toParse">Value to parse</param>
        /// <param name="value">Parsed value.</param>
        /// <returns>True if parse operation succeeded, false otherwise.</returns>
        public bool TryParseEnum<T>(object toParse, out T value)
            where T : struct
        {
            value = default(T);

            if (!typeof(T).IsEnum)
            {
                return false;
            }

            string sValue = toParse?.ToString();

            if (string.IsNullOrWhiteSpace(sValue))
            {
                return false;
            }

            bool tryParseResult = Enum.TryParse(sValue, out value);
            T parsedValue = value;

            bool isDefined = tryParseResult && Enum.GetValues(typeof(T)).Cast<T>().Any(x => Equals(x, parsedValue));

            if (!isDefined)
            {
                value = default(T);
            }

            return isDefined;
        }
    }
}
