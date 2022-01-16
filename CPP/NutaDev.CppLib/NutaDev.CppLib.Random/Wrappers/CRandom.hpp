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

#ifndef NUTADEV_CPPLIB_RANDOM_WRAPPERS_CRANDOM_HPP
#define NUTADEV_CPPLIB_RANDOM_WRAPPERS_CRANDOM_HPP

#include <ctime>
#include <cstdlib>

namespace NutaDev
{
    namespace CppLib
    {
        namespace Random
        {
            namespace Wrappers
            {
                /// <summary>
                /// Set's the seed. If no seed provided the time based seed is set. (Call the cstdlib srand function.)
                /// </summary>
                /// <param name="seed">Seed to set.</param>
                inline void setSeed(unsigned seed = static_cast<unsigned>(time(nullptr)))
                {
                    srand(seed);
                }

                /// <summary>
                /// Returns random integer (based on cstdlib rand() function) from provided [<paramref name="min"/>, <paramref name="max"/>] range (both side inclusive).
                /// </summary>
                /// <param name="min">The minimum value.</param>
                /// <param name="max">The maximum value.</param>
                /// <returns>Random integer.</returns>
                inline int nextInt(int min, int max)
                {
                    return rand() % (max - min + 1) + min;
                }

                /// <summary>
                /// Returns random integer (based on cstdlib rand() function) from provided [0, <paramref name="max"/>] range (both side inclusive).
                /// </summary>
                /// <param name="max">The maximum value.</param>
                /// <returns>Random integer.</returns>
                inline int nextInt(int max = RAND_MAX)
                {
                    return nextInt(0, max);
                }

                /// <summary>
                /// Calls the cstdlib rand() function.
                /// </summary>
                /// <returns>Random integer.</returns>
                inline int nextInt()
                {
                    return rand();
                }
            }
        }
    }
}

#endif
