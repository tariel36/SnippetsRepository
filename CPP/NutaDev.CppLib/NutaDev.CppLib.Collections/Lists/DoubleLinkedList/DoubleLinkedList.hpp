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

#ifndef NUTADEV_CPPLIB_COLLECTIONS_LISTS_DOUBLELINKEDLIST_DOUBLELINKEDLIST_HPP
#define NUTADEV_CPPLIB_COLLECTIONS_LISTS_DOUBLELINKEDLIST_DOUBLELINKEDLIST_HPP

#include <memory>

#include "DoubleLinkedListItem.hpp"

namespace NutaDev
{
    namespace CppLib
    {
        namespace Collections
        {
            namespace Lists
            {
                namespace DoubleLinkedList
                {
                    /// <summary>
                    /// Double linked lsit.
                    /// </summary>
                    template <typename T>
                    class DoubleLinkedList
                    {
                    public:

                        /// <summary>
                        /// Joins two lists.
                        /// </summary>
                        /// <param name="left">First list.</param>
                        /// <param name="right">Right list.</param>
                        /// <returns>New list.</returns>
                        static DoubleLinkedList<T> JoinLists(DoubleLinkedList<T> & left, DoubleLinkedList<T> & right)
                        {
                            DoubleLinkedList<T> result;

                            unsigned size = left._size > right._size ? left._size : right._size;

                            for (unsigned i = 0; i < size; ++i)
                            {
                                if (i < left._size)
                                {
                                    result.Add(left.Get(i));
                                }
                                if (i < right._size)
                                {
                                    result.Add(right.Get(i));
                                }
                            }

                            return result;
                        }

                        /// <summary>
                        /// Joins two lists.
                        /// </summary>
                        /// <param name="left">First list.</param>
                        /// <param name="right">Right list.</param>
                        /// <returns>New list.</returns>
                        static std::shared_ptr<DoubleLinkedList<T>> JoinLists(std::shared_ptr<DoubleLinkedList<T>> left, std::shared_ptr<DoubleLinkedList<T>> right)
                        {
                            std::shared_ptr<DoubleLinkedList<T>> result = std::shared_ptr<DoubleLinkedList<T>>(new DoubleLinkedList<T>());

                            unsigned size = left->_size > right->_size ? left->_size : right->_size;
                            for (unsigned i = 0; i < size; ++i)
                            {
                                if (i < left->_size)
                                {
                                    result->Add(left->Get(i));
                                }
                                if (i < right->_size)
                                {
                                    result->Add(right->Get(i));
                                }
                            }

                            return result;
                        }

                        /// <summary>
                        /// Adds element to list.
                        /// </summary>
                        /// <param name="value">Value to add.</param>
                        /// <returns>Reference to itself.</returns>
                        DoubleLinkedList<T> & Add(T & value)
                        {
                            if (_root == nullptr)
                            {
                                _root = std::shared_ptr<ListItem<T>>(new ListItem<T>());
                                _last = _root;
                                _root->Value = value;
                            }
                            else
                            {
                                std::shared_ptr<ListItem<T>> newElem = std::shared_ptr<ListItem<T>>(new ListItem<T>());
                                newElem->Value = value;
                                newElem->Next = nullptr;
                                newElem->Prev = _last;
                                _last->Next = newElem;
                                _last = newElem;
                            }

                            _size++;

                            return *this;
                        }

                        /// <summary>
                        /// Adds element to list.
                        /// </summary>
                        /// <param name="value">Value to add.</param>
                        /// <returns>Reference to itself.</returns>
                        DoubleLinkedList<T> & Add(T && value)
                        {
                            if (_root == nullptr)
                            {
                                _root = std::shared_ptr<ListItem<T>>(new ListItem<T>());
                                _last = _root;
                                _root->Value = value;
                            }
                            else
                            {
                                std::shared_ptr<ListItem<T>> newElem = std::shared_ptr<ListItem<T>>(new ListItem<T>());
                                newElem->Value = value;
                                newElem->Next = nullptr;
                                newElem->Prev = _last;
                                _last->Next = newElem;
                                _last = newElem;
                            }

                            _size++;

                            return *this;
                        }

                        /// <summary>
                        /// Adds element to list.
                        /// </summary>
                        /// <param name="value">Value to add.</param>
                        /// <returns>Reference to itself.</returns>
                        DoubleLinkedList<T> & Add(const T & value)
                        {
                            if (_root == nullptr)
                            {
                                _root = std::shared_ptr<ListItem<T>>(new ListItem<T>());
                                _last = _root;
                                _root->Value = value;
                            }
                            else
                            {
                                std::shared_ptr<ListItem<T>> newElem = std::shared_ptr<ListItem<T>>(new ListItem<T>());
                                newElem->Value = value;
                                newElem->Next = nullptr;
                                newElem->Prev = _last;
                                _last->Next = newElem;
                                _last = newElem;
                            }

                            _size++;

                            return *this;
                        }

                        /// <summary>
                        /// Removes element from list and returns it.
                        /// </summary>
                        /// <param name="idx">Index of element.</param>
                        /// <returns>The removed element.</returns>
                        T Pop(unsigned idx)
                        {
                            if (idx > GetSize())
                            {
                                throw std::exception("Index out of bounds.");
                            }

                            T result;

                            std::shared_ptr<ListItem<T>> element = _root;
                            for (unsigned i = 0; i < _size; ++i)
                            {
                                if (i == idx)
                                {
                                    if (element->Prev != nullptr)
                                    {
                                        element->Prev->Next = element->Next;
                                    }

                                    if (element->Next != nullptr)
                                    {
                                        element->Next->Prev = element->Prev;
                                    }

                                    result = element->Value;

                                    if (idx == 0)
                                    {
                                        _root = element->Next;
                                    }

                                    break;
                                }
                                element = element->Next;
                            }

                            if (_size == 1)
                            {
                                _root = nullptr;
                                _last = nullptr;
                            }

                            _size--;

                            return result;
                        }

                        /// <summary>
                        /// Removes node on specified index.
                        /// </summary>
                        /// <param name="idx">Idx</param>
                        /// <returns>Reference to itself.</returns>
                        DoubleLinkedList<T> & Remove(unsigned idx)
                        {
                            if (idx > GetSize())
                            {
                                throw std::exception("Index out of bounds.");
                            }

                            std::shared_ptr<ListItem<T>> element = _root;
                            for (unsigned i = 0; i < _size; ++i)
                            {
                                if (i == idx)
                                {
                                    if (element->Prev != nullptr)
                                    {
                                        element->Prev->Next = element->Next;
                                    }

                                    if (element->Next != nullptr)
                                    {
                                        element->Next->Prev = element->Prev;
                                    }

                                    if (idx == 0)
                                    {
                                        _root = element->Next;
                                    }

                                    break;
                                }
                                element = element->Next;
                            }

                            if (_size == 1)
                            {
                                _root = nullptr;
                                _last = nullptr;
                            }

                            _size--;

                            return *this;
                        }

                        /// <summary>
                        /// Gets element on specified index.
                        /// </summary>
                        /// <param name="idx">Element index.</param>
                        /// <returns>Element on provided index.</returns>
                        T & Get(unsigned idx)
                        {
                            if (idx > GetSize())
                            {
                                throw std::exception("Index out of bounds.");
                            }

                            std::shared_ptr<ListItem<T>> element = _root;
                            for (unsigned i = 0; i < _size; ++i)
                            {
                                if (i == idx)
                                {
                                    break;
                                }
                                element = element->Next;
                            }

                            return element->Value;
                        }

                        /// <summary>
                        /// Gets element on specified index.
                        /// </summary>
                        /// <param name="idx">Element index.</param>
                        /// <returns>Element on provided index.</returns>
                        const T & Get(unsigned idx) const
                        {
                            if (idx > GetSize())
                            {
                                throw std::exception("Index out of bounds.");
                            }

                            std::shared_ptr<ListItem<T>> element = _root;
                            for (unsigned i = 0; i < _size; ++i)
                            {
                                if (i == idx)
                                {
                                    break;
                                }
                                element = element->Next;
                            }

                            return element->Value;
                        }

                        /// <summary>
                        /// Gets list size.
                        /// </summary>
                        /// <returns>List size.</returns>
                        unsigned GetSize() const noexcept
                        {
                            return _size;
                        }

                        /// <summary>
                        /// Initializes a new instance of this class.
                        /// </summary>
                        DoubleLinkedList()
                            : _root(nullptr)
                            , _last(nullptr)
                            , _size(0)
                        {

                        }

                    private:
                        /// <summary>
                        /// Frist node.
                        /// </summary>
                        std::shared_ptr<ListItem<T>> _root;

                        /// <summary>
                        /// Last node.
                        /// </summary>
                        std::shared_ptr<ListItem<T>> _last;

                        /// <summary>
                        /// List size.
                        /// </summary>
                        unsigned _size;
                    };
                }
            }
        }
    }
}

#endif
