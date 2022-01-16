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

#ifndef NUTADEV_CPPLIB_COLLECTIONS_QUEUES_PRIORITYQUEUE_PRIORITYQUEUEITEM_HPP
#define NUTADEV_CPPLIB_COLLECTIONS_QUEUES_PRIORITYQUEUE_PRIORITYQUEUEITEM_HPP

namespace NutaDev
{
    namespace CppLib
    {
        namespace Collections
        {
            namespace Queues
            {
                namespace PriorityQueue
                {
                    /// <summary>
                    /// Priority queue item.
                    /// </summary>
                    template<typename T>
                    class PriorityQueueItem
                    {
                    public:
                        /// <summary>
                        /// Item priority.
                        /// </summary>
                        unsigned Priority;

                        /// <summary>
                        /// The item value.
                        /// </summary>
                        T Item;

                        /// <summary>
                        /// Compares element to this instance.
                        /// </summary>
                        /// <param name="right">Another element.</param>
                        /// <returns>True if item is lower than another.</returns>
                        bool operator < (const PriorityQueueItem & right)
                        {
                            return Priority < right.Priority;
                        }

                        /// <summary>
                        /// Compares element to this instance.
                        /// </summary>
                        /// <param name="right">Another element.</param>
                        /// <returns>True if item is greater than another.</returns>
                        bool operator > (const PriorityQueueItem & right)
                        {
                            return Priority > right.Priority;
                        }

                        /// <summary>
                        /// Initializes a new instance of this class.
                        /// </summary>
                        PriorityQueueItem()
                            : Priority(-1)
                        {

                        }

                        /// <summary>
                        /// Initializes a new instance of this class.
                        /// </summary>
                        /// <param name="item">Item to copy.</param>
                        /// <param name="priority">The priority.</param>
                        PriorityQueueItem(T item, unsigned priority)
                            : Priority(priority), Item(item)
                        { }

                        /// <summary>
                        /// Initializes a new instance of this class.
                        /// </summary>
                        /// <param name="copy">Item to copy.</param>
                        PriorityQueueItem(const PriorityQueueItem<T> & copy)
                            : Priority(copy.Priority), Item(copy.Item)
                        { }

                        /// <summary>
                        /// Initializes a new instance of this class.
                        /// </summary>
                        /// <param name="move">Item to move.</param>
                        PriorityQueueItem(PriorityQueueItem && move)
                            : Priority(move.Priority), Item(move.Item)
                        { }

                        /// <summary>
                        /// Assigns another queue.
                        /// </summary>
                        /// <returns>Reference to itself.</returns>
                        PriorityQueueItem & operator= (const PriorityQueueItem & right)
                        {
                            PriorityQueueItem tmp(right);
                            *this = std::move(tmp);
                            return *this;
                        }

                        /// <summary>
                        /// Assigns another queue.
                        /// </summary>
                        /// <returns>Reference to itself.</returns>
                        PriorityQueueItem & operator= (PriorityQueueItem && right) noexcept
                        {
                            Item = right.Item;
                            Priority = right.Priority;
                            return *this;
                        }

                        /// <summary>
                        /// Destructs an instance of this class.
                        /// </summary>
                        ~PriorityQueueItem() noexcept {}
                    };
                }
            }
        }
    }
}

#endif
