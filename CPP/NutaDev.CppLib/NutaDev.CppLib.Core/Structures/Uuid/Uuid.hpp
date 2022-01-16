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

#ifndef NUTADEV_CPPLIB_CORE_STRUCTURES_UUID_UUID_HPP
#define NUTADEV_CPPLIB_CORE_STRUCTURES_UUID_UUID_HPP

#include <string>

namespace NutaDev
{
    namespace CppLib
    {
        namespace Core
        {
            namespace Structures
            {
                namespace Uuid
                {
                    class UUID
                    {
                    public:
                        /// <summary>
                        /// Generates a new UUID object.
                        /// </summary>
                        /// <returns>A UUID object.</returns>
                        static UUID Generate();

                        /// <summary>
                        /// Initializes a new instance of the class.
                        /// </summary>
                        /// <param name="data1">First quarter.</param>
                        /// <param name="data2">Second quarter.</param>
                        /// <param name="data3">Third quarter.</param>
                        /// <param name="data4">Fourth quarter.</param>
                        UUID(int data1, int data2, int data3, int data4);

                        /// <summary>
                        /// Initializes a new instance of the class.
                        /// </summary>
                        /// <param name="str">String representation of a UUID.</param>
                        UUID(const std::string & str);

                        /// <summary>
                        /// Compares UUID to another UUID.
                        /// </summary>
                        /// <param name="uuid">Another UUID.</param>
                        /// <returns>A value indicating comarison result, -1, 0, or 1.</returns>
                        int Compare(const UUID & uuid) const;

                        /// <summary>
                        /// Indicates whether this object is smaller than another one.
                        /// </summary>
                        /// <param name="right">Another UUID.</param>
                        /// <returns>True if it's smaller, false otherwise.</returns>
                        bool operator <(const UUID & right) const;

                        /// <summary>
                        /// Converts UUID into string.
                        /// </summary>
                        /// <returns>A string representation.</returns>
                        std::string ToString() const;

                    private:
                        /// <summary>
                        /// First quarter.
                        /// </summary>
                        int _data1 : 32;

                        /// <summary>
                        /// Second quarter.
                        /// </summary>
                        int _data2 : 32;

                        /// <summary>
                        /// Third quarter.
                        /// </summary>
                        int _data3 : 32;

                        /// <summary>
                        /// Fourth quarter.
                        /// </summary>
                        int _data4 : 32;
                    };
                }
            }
        }
    }
}

#endif
