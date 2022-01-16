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

#ifndef NUTADEV_CPPLIB_COLLECTIONS_QUEUES_QUEUE_HPP
#define NUTADEV_CPPLIB_COLLECTIONS_QUEUES_QUEUE_HPP

#include <mutex>

#include "../Lists/DoubleLinkedList/DoubleLinkedList.hpp"

namespace NutaDev
{
    namespace CppLib
    {
        namespace Collections
        {
            namespace Queues
            {
                /// <summary>
                /// Queue structure.
                /// </summary>
                template<typename T>
                class Queue
                {
                public:
                    /// <summary>
                    /// Tries to enqueue element from queue.
                    /// </summary>
                    /// <param name="item">Item to enqueue.</param>
                    /// <returns>True if item has been enqueued.</returns>
                    bool TryEnqueue(T && item)
                    {
                        if (_synch.try_lock())
                        {
                            _queue.Add(item);

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
                    bool TryEnqueue(T & item)
                    {
                        if (_synch.try_lock())
                        {
                            _queue.Add(item);

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
                    bool TryEnqueue(const T & item)
                    {
                        if (_synch.try_lock())
                        {
                            _queue.Add(item);

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
                    bool TryDequeue(T & item)
                    {
                        bool result = false;

                        if (_synch.try_lock())
                        {
                            if (Size() > 0)
                            {
                                std::swap(item, _queue.Get(0));

                                _queue.Remove(0);

                                result = true;
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
                    bool JoinQueue(Queue<T> & other)
                    {
                        if (_synch.try_lock())
                        {
                            _queue = Lists::List<T>::JoinLists(_queue, other._queue);

                            _synch.unlock();
                            return true;
                        }

                        return false;
                    }

                    /// <summary>
                    /// Splits the queue into two queues.
                    /// </summary>
                    /// <returns>New queue with half of the elements.</returns>
                    Queue<T> Split()
                    {
                        _synch.lock();
                        Queue<T> result;

                        if (_queue.GetSize() > 1)
                        {
                            unsigned size = _queue.GetSize();
                            size = ((size % 2 == 0) ? (size / 2) : ((size - 1) / 2));
                            for (unsigned i = 0; i < size; ++i)
                            {
                                result.TryEnqueue(_queue.Pop(0));
                            }
                        }

                        _synch.unlock();
                        return result;
                    }

                    /// <summary>
                    /// Peeks at top element of the queue.
                    /// </summary>
                    /// <returns>Top element of queue.</returns>
                    const T & Peek() const
                    {
                        if (Size() > 0)
                        {
                            return _queue.Get(0);
                        }

                        throw std::exception("You can't peek empty queue.");
                    }

                    /// <summary>
                    /// Gets size of queue.
                    /// </summary>
                    /// <returns>Size of queue.</returns>
                    unsigned Size() const noexcept
                    {
                        return _queue.GetSize();
                    }

                    /// <summary>
                    /// Initializes a new instance of this class.
                    /// </summary>
                    Queue()
                    {
                    }

                    /// <summary>
                    /// Initializes a new instance of this class.
                    /// </summary>
                    /// <param name="other"></param>
                    Queue(const Queue<T> & other)
                        : _queue(other._queue)
                    {

                    }

                    /// <summary>
                    /// Initializes a new instance of this class.
                    /// </summary>
                    /// <param name="other"></param>
                    Queue(Queue<T> && other) noexcept
                        : _queue(std::move(other._queue))
                    {

                    }

                    /// <summary>
                    /// Assigns another queue.
                    /// </summary>
                    /// <param name="other">Other queue.</param>
                    /// <returns>Reference to itself.</returns>
                    Queue<T> & operator=(const Queue<T> & other)
                    {
                        this->_queue = other._queue;

                        return *this;
                    }

                    /// <summary>
                    /// Assigns another queue.
                    /// </summary>
                    /// <param name="other">Other queue.</param>
                    /// <returns>Reference to itself.</returns>
                    Queue<T> & operator=(Queue<T> && other)
                    {
                        if (this != &other)
                        {
                            _queue = std::move(other._queue);
                        }

                        return *this;
                    }

                    /// <summary>
                    /// Destructs instance of this class.
                    /// </summary>
                    virtual ~Queue()
                    {
                    }

                protected:
                    /// <summary>
                    /// Synchronization context.
                    /// </summary>
                    std::mutex _synch;

                    /// <summary>
                    /// Internal collection.
                    /// </summary>
                    Lists::List<T> _queue;

                };
            }
        }
    }
}

#endif
