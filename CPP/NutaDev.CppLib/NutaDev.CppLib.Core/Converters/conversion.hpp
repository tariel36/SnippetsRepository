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

#ifndef NUTADEV_CPPLIB_CORE_CONVERTERS_CONVERSION_HPP
#define NUTADEV_CPPLIB_CORE_CONVERTERS_CONVERSION_HPP

#include <string>
#include <sstream>

namespace NutaDev
{
    namespace CppLib
    {
        namespace Core
        {
            namespace Converters
            {
                /// <summary>
                /// Attempts to convert the <paramref name="str"/> string's content to <paramref name="{T}"/> type.
                /// </summary>
                /// <typeparam name="T">Target type.</typeparam>
                /// <param name="str">The string with value.</param>
                /// <returns>Converted value.</returns>
                template<class T>
                inline T toType(const std::string & str)
                {
                    std::istringstream ss(str);
                    T val;
                    ss >> val;

                    return val;
                }

                /// <summary>
                /// Attempts to convert the <paramref name="{T}"/> value to the string.
                /// </summary>
                /// <typeparam name="T">Target type.</typeparam>
                /// <param name="str">The string with value.</param>
                /// <returns>Converted value.</returns>
                template<class T>
                inline std::string toString(T val)
                {
                    std::ostringstream ss;
                    ss << val;

                    return ss.str();
                }
            }
        }
    }
}

#endif
