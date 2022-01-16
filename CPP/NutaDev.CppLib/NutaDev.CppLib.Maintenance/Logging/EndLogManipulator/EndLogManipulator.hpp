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

#ifndef NUTADEV_CPPLIB_MAINTENANCE_LOGGING_ENDLOGMANIPULATOR_ENDLOGMANIPULATOR_HPP
#define NUTADEV_CPPLIB_MAINTENANCE_LOGGING_ENDLOGMANIPULATOR_ENDLOGMANIPULATOR_HPP

#include <string>
#include <sstream>

#include "../ForwardDeclarations.hpp"
#include "../LogLevel.hpp"

namespace NutaDev
{
    namespace CppLib
    {
        namespace Maintenance
        {
            namespace Logging
            {
                namespace EndLogManipulator
                {
                    /// <summary>
                    /// Manipulator that indicates end of log.
                    /// </summary>
                    class EndLogManipulator
                    {
                    public:

                        /// <summary>
                        /// Initializes a new instance of this class.
                        /// </summary>
                        /// <param name="messageLevel">Log level.</param>
                        EndLogManipulator(LogLevel messageLevel);

                        /// <summary>
                        /// Parentheses operator.
                        /// </summary>
                        /// <param name="val">Stream reference.</param>
                        void operator() (Log::Log & stream) const;

                    private:
                        /// <summary>
                        /// Current log level.
                        /// </summary>
                        LogLevel _level;
                    };
                }
            }
        }
    }
}

#endif
