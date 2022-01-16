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

#ifndef NUTADEV_CPPLIB_BINARY_BITOPERATIONS_HPP
#define NUTADEV_CPPLIB_BINARY_BITOPERATIONS_HPP

namespace NutaDev
{
    namespace CppLib
    {
        namespace Binary
        {
            /// <summary>
            /// Sets bit on specified position.
            /// </summary>
            /// <param name="val">The value.</param>
            /// <param name="pos">The position.</param>
            /// <returns>Modified value.</returns>
            inline int setBit(int val, signed char pos)
            {
                return (val | (1 << pos));
            }

            /// <summary>
            /// Clears bit on specified position.
            /// </summary>
            /// <param name="val">The value.</param>
            /// <param name="pos">The position.</param>
            /// <returns>Modified value.</returns>
            inline int clearBit(int val, signed char pos)
            {
                return (val & (~(1 << pos)));
            }

            /// <summary>
            /// Toggles bit on specified position.
            /// </summary>
            /// <param name="val">The value.</param>
            /// <param name="pos">The position.</param>
            /// <returns>Modified value.</returns>
            inline int toggleBit(int val, signed char pos)
            {
                return (val ^ (1 << pos));
            }

            /// <summary>
            /// Checks bit on specified position.
            /// </summary>
            /// <param name="val">The value.</param>
            /// <param name="pos">The position.</param>
            /// <returns>Modified value.</returns>
            inline int checkBit(int val, signed char pos)
            {
                return (val & (1 << pos));
            }

            /// <summary>
            /// Indicates whether bit is equal to 1 on specified position.
            /// </summary>
            /// <param name="val">The value.</param>
            /// <param name="pos">The position.</param>
            /// <returns>Modified value.</returns>
            inline bool isZero(int val, signed char pos)
            {
                return (checkBit(val, pos) == 0);
            }

            /// <summary>
            /// Indicates whether bit us equal to 0 on specified position.
            /// </summary>
            /// <param name="val">The value.</param>
            /// <param name="pos">The position.</param>
            /// <returns>Modified value.</returns>
            inline bool isOne(int val, signed char pos)
            {
                return !isZero(val, pos);
            }

            /// <summary>
            /// Reverses bits in value.
            /// </summary>
            /// <param name="in">The value.</param>
            /// <returns>Modified value.</returns>
            inline int reverse(int in)
            {
                int out = 0;
                signed char size = (sizeof(in) * 8);

                for (signed char i = 0; i < size; ++i)
                {
                    if (isOne(in, i))
                    {
                        out = setBit(out, size - i - 1);
                    }
                }

                return out;
            }

            /// <summary>
            /// Prints binary values.
            /// </summary>
            /// <param name="in">The value to print.</param>
            /// <returns>A string that contains printed value.</returns>
            inline char * printBinary(int in)
            {
                signed char size = (sizeof(in) * 8);
                char * out = new char[size];

                for (char i = size; i > 0; --i)
                {
                    out[size - i] = (isOne(in, i - 1) + '0');
                }

                out[size] = 0;

                return out;
            }
        }
    }
}

#endif
