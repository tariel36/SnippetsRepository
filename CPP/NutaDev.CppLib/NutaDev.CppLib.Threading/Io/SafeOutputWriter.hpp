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

#ifndef NUTADEV_CPPLIB_THREADING_IO_SAFEOUTPUTWRITER_HPP
#define NUTADEV_CPPLIB_THREADING_IO_SAFEOUTPUTWRITER_HPP

#include <mutex>
#include <iostream>
#include <sstream>

#include "../Types/Types.hpp"

namespace NutaDev
{
    namespace CppLib
    {
        namespace Threading
        {
            namespace Io
            {
                /// <summary>
                /// Thread safe standard output writer.
                /// </summary>
                class SafeOutputWriter
                {
                private:
                    /// <summary>
                    /// Synchronization context.
                    /// </summary>
                    static std::mutex _mutex;

                public:
                    /// <summary>
                    /// Write to standard output.
                    /// </summary>
                    /// <param name="value">Value to write.</param>
                    /// <param name="args">Arguments.</param>
                    template<typename T, typename... Args>
                    static void Write(T value, Args... args)
                    {
                        NutaDev::CppLib::Threading::Types::LockGuardMutex lock(_mutex);

                        std::cout << StringConverter::ToString(value, args ...);
                    }
                };
            }
        }
    }
}

#endif
