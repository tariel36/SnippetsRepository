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

#ifndef NUTADEV_CPPLIB_TYPES_HELPERS_STRING_STRING_HPP
#define NUTADEV_CPPLIB_TYPES_HELPERS_STRING_STRING_HPP

#include <vector>

namespace NutaDev
{
    namespace CppLib
    {
        namespace Types
        {
            namespace Helpers
            {
                namespace String
                {
                    /// <summary>The typedef for std::vector&lt;std::string&gt;</summary>
                    typedef std::vector<std::string> VectorString;

                    /// <summary>The typedef for std::vector&lt;char&gt;</summary>
                    typedef std::vector<char> VectorChar;

                    /// <summary>
                    /// Removes the whitespace characters from the beginning and end of <paramref name="str"/> string.
                    /// </summary>
                    /// <param name="str">String that will be trimmed.</param>
                    /// <returns>Trimmed copy of the string.</returns>
                    std::string trim(const std::string & str);

                    /// <summary>
                    /// Removes the occurrences of provided <paramref name="chars"/> characters from the beginning and end of <paramref name="str"/> string.
                    /// </summary>
                    /// <param name="str">String that will be trimmed.</param>
                    /// <param name="chars>Characters that will be trimmed.</param>
                    /// <returns>Trimmed copy of the string.</returns>
                    std::string trim(const std::string & str, const VectorChar & chars);

                    /// <summary>
                    /// Replaces all letters with their lowercase equivalent.
                    /// </summary>
                    /// <param name="str">Source string.</param>
                    /// <returns>Copy of the source string with lowercase letters.</returns>
                    std::string toLower(const std::string & str);

                    /// <summary>
                    /// Replaces all letters with their uppercase equivalent.
                    /// </summary>
                    /// <param name="str">Source string.</param>
                    /// <returns>Copy of the source string with uppercase letters.</returns>
                    std::string toUpper(const std::string & str);

                    /// <summary>
                    /// Splits the <paramref name="str"/> string with provied <paramref name="delim"/> character.
                    /// </summary>
                    /// <param name="str">Analyzed string.</param>
                    /// <param name="delim">The splitting character.</param>
                    /// <returns>Vector of substrings.</returns>
                    VectorString split(const std::string & str, char delim);

                    /// <summary>
                    /// Pads on the left provided string <paramref name="str"/> with whitespace character to match the <paramref name="totalWidth"/> width.
                    /// </summary>
                    /// <param name="str">Source string.</param>
                    /// <param name="totalWidth">Minimum string width.</param>
                    /// <returns>Padded copy of the <paramref name="str"/>.</returns>
                    std::string padLeft(const std::string & str, int totalWidth);

                    /// <summary>
                    /// Pads on the left provided string <paramref name="str"/> with provided <paramref name="paddingChar"/> character to match the <paramref name="totalWidth"/> width.
                    /// </summary>
                    /// <param name="str">Source string.</param>
                    /// <param name="totalWidth">Minimum string width.</param>
                    /// <param name="paddingChar">The character to pad with.</param>
                    /// <returns>Padded copy of the <paramref name="str"/>.</returns>
                    std::string padLeft(const std::string & str, int totalWidth, char paddingChar);

                    /// <summary>
                    /// Pads on the right provided string <paramref name="str"/> with whitespace character to match the <paramref name="totalWidth"/> width.
                    /// </summary>
                    /// <param name="str">Source string.</param>
                    /// <param name="totalWidth">Minimum string width.</param>
                    /// <returns>Padded copy of the <paramref name="str"/>.</returns>
                    std::string padRight(const std::string & str, int totalWidth);

                    /// <summary>
                    /// Pads on the right provided string <paramref name="str"/> with provided <paramref name="paddingChar"/> character to match the <paramref name="totalWidth"/> width.
                    /// </summary>
                    /// <param name="str">Source string.</param>
                    /// <param name="totalWidth">Minimum string width.</param>
                    /// <param name="paddingChar">The character to pad with.</param>
                    /// <returns>Padded copy of the <paramref name="str"/>.</returns>
                    std::string padRight(const std::string & str, int totalWidth, char paddingChar);

                    /// <summary>
                    /// Replaces the <paramref name="oldChar"/> character with the <paramref name="newChar"/> character.
                    /// </summary>
                    /// <param name="str">Analyzed string.</param>
                    /// <param name="oldChar">Character to be replaced.</param>
                    /// <param name="newChar">Character to be replaced with.</param>
                    /// <returns>Copy of the <paramref name="str"> string with replaced characters.</returns>
                    std::string replace(const std::string & str, char oldChar, char newChar);

                    /// <summary>
                    /// Returns index of the first occurrence of the <paramref name="searchedCharr"/> character.
                    /// </summary>
                    /// <param name="str">Analyzed string.</param>
                    /// <param name="searchedChar">The searched character.</param>
                    /// <returns>Index of std::string::npos if not found.</returns>
                    size_t indexOf(const std::string & str, char searchedChar);

                    /// <summary>
                    /// Returns index of the last occurrence of the <paramref name="searchedCharr"/> character.
                    /// </summary>
                    /// <param name="str">Analyzed string.</param>
                    /// <param name="searchedChar">The searched character.</param>
                    /// <returns>Index of std::string::npos if not found.</returns>
                    size_t lastIndexOf(const std::string & str, char searchedChar);

                    /// <summary>
                    /// Returns first index of the any provided character from <paramref name="chars"/>.
                    /// </summary>
                    /// <param name="str">Analyzed string.</param>
                    /// <param name="chars">Vector of analyzed characters.</param>
                    /// <returns>Index of character or std::string::npos if not found.</returns>
                    size_t indexOfAny(const std::string & str, const VectorChar & chars);

                    /// <summary>
                    /// Returns last index of the any provided character from <paramref name="chars"/>.
                    /// </summary>
                    /// <param name="str">Analyzed string.</param>
                    /// <param name="chars">Vector of analyzed characters.</param>
                    /// <returns>Index of character or std::string::npos if not found.</returns>
                    size_t lastIndexOfAny(const std::string & str, const VectorChar & chars);

                    /// <summary>
                    /// Inserts <paramref name="count"/> whitespace characters at the beginning of the <paramref name="str"/> string.
                    /// </summary>
                    /// <param name="str">Source string.</param>
                    /// <param name="count">Number of whitespaces to insert.</param>
                    /// <returns>Copy of the <paramref name="str"/>.</returns>
                    std::string moveRight(const std::string & str, int count);

                    /// <summary>
                    /// Inserts <paramref name="count"/> <paramref name="charToInsert"/> characters at the beginning of the <paramref name="str"/> string.
                    /// </summary>
                    /// <param name="str">Source string.</param>
                    /// <param name="count">Number of characters to insert.</param>
                    /// <param name="charToInsert">Character to insert.</param>
                    /// <returns>Copy of the <paramref name="str"/>.</returns>
                    std::string moveRight(const std::string & str, int count, char charToInsert);
                }
            }
        }
    }
}

#endif
