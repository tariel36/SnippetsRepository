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

#ifndef NUTADEV_CPPLIB_TYPES_HELPERS_TIME_TIME_HPP
#define NUTADEV_CPPLIB_TYPES_HELPERS_TIME_TIME_HPP

#include <string>
#include <chrono>
#include <ctime>

namespace NutaDev
{
    namespace CppLib
    {
        namespace Types
        {
            namespace Helpers
            {
                namespace Time
                {
                    /// <summary>
                    /// Returns string representation of the current date and time with provided format or default format.
                    /// </summary>
                    /// <param name="format">The format.</param>
                    /// <returns>String representation of current date and time.</returns>
                    inline std::string getCurrentDateTime(const char * format = nullptr)
                    {
                        std::chrono::time_point<std::chrono::system_clock> nowChrono = std::chrono::system_clock::now();
                        std::time_t nowTime = std::chrono::system_clock::to_time_t(nowChrono);
                        struct tm nowTm;
                        localtime_s(&nowTm, &nowTime);

                        char out[1024];

                        if (format == nullptr)
                        {
                            const char * defaultFormat = "%d.%m.%Y %H:%M:%S";
                            format = defaultFormat;
                        }

                        strftime(out, 1024, format, &nowTm);

                        return std::string(out);
                    }
                }
            }
        }
    }
}

#endif
