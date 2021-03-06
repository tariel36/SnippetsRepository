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

#ifndef NUTADEV_CPPLIB_MAINTENANCE_LOGGING_LOGLEVEL_HPP
#define NUTADEV_CPPLIB_MAINTENANCE_LOGGING_LOGLEVEL_HPP

namespace NutaDev
{
    namespace CppLib
    {
        namespace Maintenance
        {
            namespace Logging
            {
                enum LogLevel
                {
                    /// <summary>
                    /// No logging.
                    /// </summary>
                    None = 0,
                    /// <summary>
                    /// Only critical logs.
                    /// </summary>
                    Critical = (1 << 1),
                    /// <summary>
                    /// Error logs and higher.
                    /// </summary>
                    Error = (1 << 2),
                    /// <summary>
                    /// Warning logs and higher.
                    /// </summary>
                    Warning = (1 << 3),
                    /// <summary>
                    /// Info logs and higher.
                    /// </summary>
                    Info = (1 << 4),
                    /// <summary>
                    /// All logs.
                    /// </summary>
                    All = (1 << 5)
                };
            }
        }
    }
}

#endif
