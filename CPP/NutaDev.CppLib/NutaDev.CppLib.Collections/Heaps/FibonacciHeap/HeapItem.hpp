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

#ifndef NUTADEV_CPPLIB_COLLECTIONS_HEAPS_FIBONACCIHEAP_HEAPITEM_HPP
#define NUTADEV_CPPLIB_COLLECTIONS_HEAPS_FIBONACCIHEAP_HEAPITEM_HPP

#include <cmath>
#include <memory>

namespace NutaDev
{
    namespace CppLib
    {
        namespace Collections
        {
            namespace Heaps
            {
                namespace FibonacciHeap
                {
                    /// <summary>
                    /// Element of heap.
                    /// </summary>
                    template <typename T>
                    class HeapItem
                    {
                    public:
                        /// <summary>
                        /// Initializes a new instance of this class.
                        /// </summary>
                        /// <param name="key">Item key</param>
                        HeapItem(T key)
                            : Key(key)
                            , Mark(false)
                            , Parent(nullptr)
                            , Left(nullptr)
                            , Right(nullptr)
                            , Child(nullptr)
                            , Rank(-1)
                        {
                        }

                        /// <summary>
                        /// Destructs instance of this class.
                        /// </summary>
                        ~HeapItem()
                        {
                        }

                        /// <summary>
                        /// The key.
                        /// </summary>
                        T Key;

                        /// <summary>
                        /// Indicates whether item is marked.
                        /// </summary>
                        bool Mark;

                        /// <summary>
                        /// Parent item.
                        /// </summary>
                        std::shared_ptr<HeapItem<T>> Parent;

                        /// <summary>
                        /// Left item.
                        /// </summary>
                        std::shared_ptr<HeapItem<T>> Left;

                        /// <summary>
                        /// Right item.
                        /// </summary>
                        std::shared_ptr<HeapItem<T>> Right;

                        /// <summary>
                        /// The child.
                        /// </summary>
                        std::shared_ptr<HeapItem<T>> Child;

                        /// <summary>
                        /// The rank.
                        /// </summary>
                        int Rank;
                    };
                }
            }
        }
    }
}

#endif
