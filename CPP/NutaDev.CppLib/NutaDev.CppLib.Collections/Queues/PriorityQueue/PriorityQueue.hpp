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

#ifndef NUTADEV_CPPLIB_COLLECTIONS_QUEUES_PRIORITYQUEUE_PRIORITYQUEUE_HPP
#define NUTADEV_CPPLIB_COLLECTIONS_QUEUES_PRIORITYQUEUE_PRIORITYQUEUE_HPP

#include <mutex>

#include "../../Heaps/FibonacciHeap/FibonacciHeap.hpp"
#include "PriorityQueueItem.hpp"

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
                    /// The structure of priority queue.
                    /// </summary>
                    /// <returns></returns>
                    template<typename T>
                    class PriorityQueue
                    {
                    public:
                        /// <summary>
                        /// Tries to enqueue element from queue.
                        /// </summary>
                        /// <param name="item">Item to enqueue.</param>
                        /// <returns>True if item has been enqueued.</returns>
                        bool TryEnqueue(T && value, unsigned priority)
                        {
                            if (_synch.try_lock())
                            {
                                Enqueue(value, priority);

                                _synch.unlock();
                                return true;
                            }

                            return false;
                        }

                        /// <summary>
                        /// Tries to enqueue element from queue.
                        /// </summary>
                        /// <param name="item">Item to enqueue.</param>
                        /// <returns>True if item has been enqueued.</returns>
                        bool TryEnqueue(T & value, unsigned priority)
                        {
                            if (_synch.try_lock())
                            {
                                Enqueue(value, priority);

                                _synch.unlock();
                                return true;
                            }

                            return false;
                        }

                        /// <summary>
                        /// Tries to enqueue element from queue.
                        /// </summary>
                        /// <param name="item">Item to enqueue.</param>
                        /// <returns>True if item has been enqueued.</returns>
                        bool TryEnqueue(const T & value, unsigned priority)
                        {
                            if (_synch.try_lock())
                            {
                                Enqueue(value, priority);

                                _synch.unlock();
                                return true;
                            }

                            return false;
                        }

                        /// <summary>
                        /// Tries to dequeue element from queue.
                        /// </summary>
                        /// <param name="item">Item to dequeue.</param>
                        /// <returns>True if item has been dequeued.</returns>
                        bool TryDequeue(T & value)
                        {
                            bool result = false;

                            if (_synch.try_lock())
                            {
                                if (Size() > 0)
                                {
                                    std::shared_ptr<Heaps::HeapItem<PriorityQueueItem<T>>> minItem = _heap.EraseMinimum();
                                    std::swap(value, minItem->Key.Item);
                                    result = true;
                                    _size--;
                                }

                                _synch.unlock();
                            }

                            if (Size() == 0)
                            {
                                result = false;
                            }

                            return result;
                        }

                        /// <summary>
                        /// Joins another queue into this queue.
                        /// </summary>
                        /// <param name="other">Another queue to join.</param>
                        /// <returns>True if queues have been joined.</returns>
                        bool JoinWith(PriorityQueue<T> & other)
                        {
                            if (_synch.try_lock())
                            {
                                _heap = Heaps::FibonacciHeap<PriorityQueueItem<T>>::JoinHeaps(_heap, other._heap);
                                _size = _heap.Size();

                                _synch.unlock();
                                return true;
                            }

                            return false;
                        }

                        /// <summary>
                        /// Clears the collection.
                        /// </summary>
                        void Clear()
                        {
                            _synch.lock();
                            _heap.Clear();
                            _synch.unlock();
                        }

                        /// <summary>
                        /// Splits the queue into two queues.
                        /// </summary>
                        /// <returns>New queue with half of the elements.</returns>
                        PriorityQueue<T> Split()
                        {
                            PriorityQueue<T> result;

                            _synch.lock();

                            if (_size > 1)
                            {
                                for (unsigned i = 0, size = _size / 2; i < size; ++i)
                                {
                                    std::shared_ptr<Heaps::HeapItem<PriorityQueueItem<T>>> minItem = _heap.EraseMinimum();
                                    _size--;

                                    result.TryEnqueue(minItem->Key.Item, minItem->Key.Priority);
                                }
                            }

                            _synch.unlock();
                            return result;
                        }

                        /// <summary>
                        /// Gets size of queue.
                        /// </summary>
                        /// <returns>Size of queue.</returns>
                        unsigned Size() const noexcept
                        {
                            return _size;
                        }

                        /// <summary>
                        /// Peeks at top element of the queue.
                        /// </summary>
                        /// <returns>Top element of queue.</returns>
                        const T & Peek() const
                        {
                            if (Size() > 0)
                            {
                                return _heap.Minimum();
                            }

                            throw std::exception("You can't peek empty queue.");
                        }

                        /// <summary>
                        /// Initializes a new instance of this class.
                        /// </summary>
                        PriorityQueue()
                            : _size(0)
                        {
                        }

                        /// <summary>
                        /// Initializes a new instance of this class.
                        /// </summary>
                        /// <param name="other">Another queue.</param>
                        PriorityQueue(const PriorityQueue<T> & other)
                            : _heap(other._heap)
                            , _size(other._size)
                        {

                        }

                        /// <summary>
                        /// Initializes a new instance of this class.
                        /// </summary>
                        /// <param name="other">Another queue.</param>
                        PriorityQueue(PriorityQueue<T> && other) noexcept
                            : _heap(std::move(other._heap))
                            , _size(std::move(other._size))
                        {

                        }

                        /// <summary>
                        /// Assigns another queue.
                        /// </summary>
                        /// <param name="other">Another queue.</param>
                        /// <returns>Reference to itself.</returns>
                        PriorityQueue<T> & operator=(const PriorityQueue<T> & other)
                        {
                            this->_heap = other._heap;
                            this->_size = other._size;

                            return *this;
                        }

                        /// <summary>
                        /// Assigns another queue.
                        /// </summary>
                        /// <param name="other">Another queue.</param>
                        /// <returns>Reference to itself.</returns>
                        PriorityQueue<T> & operator=(PriorityQueue<T> && other)
                        {
                            if (this != &other)
                            {
                                _heap = std::move(other._heap);
                                _size = std::move(other._size);
                            }

                            return *this;
                        }

                        /// <summary>
                        /// Destructs an instance of this class.
                        /// </summary>
                        ~PriorityQueue()
                        {
                        }

                    private:
                        /// <summary>
                        /// Internal structure.
                        /// </summary>
                        Heaps::FibonacciHeap<PriorityQueueItem<T>> _heap;

                        /// <summary>
                        /// Synchronization context.
                        /// </summary>
                        std::mutex _synch;

                        /// <summary>
                        /// Size of queue.
                        /// </summary>
                        unsigned _size;

                        /// <summary>
                        /// Enqueue new element.
                        /// </summary>
                        /// <param name="value">The value.</param>
                        /// <param name="priority">Element priority.</param>
                        void Enqueue(T value, unsigned priority)
                        {
                            _heap.Push(PriorityQueueItem<T>(value, priority));
                            _size++;
                        }
                    };
                }
            }
        }
    }
}

#endif
