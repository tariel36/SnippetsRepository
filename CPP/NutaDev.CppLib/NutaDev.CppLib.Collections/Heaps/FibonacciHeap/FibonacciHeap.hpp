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

#ifndef NUTADEV_CPPLIB_COLLECTIONS_HEAPS_FIBONACCIHEAP_FIBONACCIHEAP_HPP
#define NUTADEV_CPPLIB_COLLECTIONS_HEAPS_FIBONACCIHEAP_FIBONACCIHEAP_HPP

#include <cmath>
#include <limits>

#include "HeapItem.hpp"

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
                    /// The fibbonacci heap.
                    /// </summary>
                    template<class T>
                    class FibonacciHeap
                    {
                    public:
                        /// <summary>
                        /// Joins two heaps.
                        /// </summary>
                        /// <param name="left">Left heap.</param>
                        /// <param name="right">Right heap.</param>
                        /// <returns>New heap.</returns>
                        static FibonacciHeap<T> JoinHeaps(FibonacciHeap<T> & left, FibonacciHeap<T> & right)
                        {
                            FibonacciHeap<T> merged = FibonacciHeap<T>();

                            merged._root = left._root;

                            if (merged._root != nullptr && right._root != nullptr)
                            {
                                merged._root->Right->Left = right._root->Left;
                                right._root->Left->Right = merged._root->Right;
                                merged._root->Right = right._root;
                                right._root->Left = merged._root;
                            }

                            if (left._root == nullptr || (right._root != nullptr && right._root->Key < left._root->Key))
                            {
                                merged._root = right._root;
                            }

                            right._root = nullptr;

                            merged._size = left._size + right._size;

                            return merged;
                        }

                        /// <summary>
                        /// Joins two heaps.
                        /// </summary>
                        /// <param name="left">Left heap.</param>
                        /// <param name="right">Right heap.</param>
                        /// <returns>New heap.</returns>
                        static std::shared_ptr<FibonacciHeap<T>> JoinHeaps(std::shared_ptr<FibonacciHeap<T>> left, std::shared_ptr<FibonacciHeap<T>> right)
                        {
                            std::shared_ptr<FibonacciHeap<T>> merged = std::shared_ptr<FibonacciHeap<T>>(new FibonacciHeap<T>());

                            merged->_root = left->_root;

                            if (merged->_root != nullptr && right->_root != nullptr)
                            {
                                merged->_root->Right->Left = right->_root->Left;
                                right->_root->Left->Right = merged->_root->Right;
                                merged->_root->Right = right->_root;
                                right->_root->Left = merged->_root;
                            }

                            if (left->_root == nullptr || (right->_root != nullptr && right->_root->Key < left->_root->Key))
                            {
                                merged->_root = right->_root;
                            }

                            right->_root = nullptr;

                            merged->_size = left->_size + right->_size;

                            return merged;
                        }

                        /// <summary>
                        /// Links two nodes.
                        /// </summary>
                        /// <param name="leftNode"></param>
                        /// <param name="rightNode"></param>
                        static void LinkHeapNodes(std::shared_ptr<HeapItem<T>> leftNode, std::shared_ptr<HeapItem<T>> rightNode)
                        {
                            std::shared_ptr<HeapItem<T>> tempRight = leftNode->Right;
                            std::shared_ptr<HeapItem<T>> tempLeft = leftNode->Left;

                            leftNode->Left->Right = tempRight;
                            leftNode->Right->Left = tempLeft;

                            if (rightNode->Child != nullptr)
                            {
                                rightNode->Child->Left->Right = leftNode;
                                leftNode->Left = rightNode->Child->Left;
                                rightNode->Child->Left = leftNode;
                                leftNode->Right = rightNode->Child;
                            }
                            else
                            {
                                rightNode->Child = leftNode;
                                leftNode->Right = leftNode;
                                leftNode->Left = leftNode;
                            }

                            leftNode->Parent = rightNode;

                            ++(rightNode->Rank);

                            leftNode->Mark = false;
                        }

                        /// <summary>
                        /// Initializes a new instance of this class.
                        /// </summary>
                        FibonacciHeap()
                            : _size (0)
                            , _root (nullptr)
                        {
                        }

                        /// <summary>
                        /// Destructs the instance of this class.
                        /// </summary>
                        ~FibonacciHeap()
                        {
                            Clear(_root);
                        }

                        /// <summary>
                        /// Erases the node.
                        /// </summary>
                        /// <param name="node">Node to erase.</param>
                        void EraseNode(std::shared_ptr<HeapItem<T>> node)
                        {
                            LowerKey(node, std::numeric_limits<T>::min());

                            std::shared_ptr<HeapItem<T>> fn = EraseMinimum();
                        }

                        /// <summary>
                        /// Indicates whether heap is empty.
                        /// </summary>
                        /// <returns>True if heap is empty, false otherwise.</returns>
                        bool Empty() const
                        {
                            return _size == 0;
                        }

                        /// <summary>
                        /// Returns top node.
                        /// </summary>
                        /// <returns>The node.</returns>
                        std::shared_ptr<HeapItem<T>> TopNode()
                        {
                            return Minimum();
                        }

                        /// <summary>
                        /// Returns top value.
                        /// </summary>
                        /// <returns>The value of top.</returns>
                        T Top()
                        {
                            return Minimum()->Key;
                        }

                        /// <summary>
                        /// Pops the top node.
                        /// </summary>
                        void Pop()
                        {
                            if (Empty())
                            {
                                return;
                            }

                            std::shared_ptr<HeapItem<T>> item = EraseMinimum();
                        }

                        /// <summary>
                        /// Pushes the key.
                        /// </summary>
                        /// <param name="key"></param>
                        /// <returns>Pushed item.</returns>
                        std::shared_ptr<HeapItem<T>> Push(T key)
                        {
                            std::shared_ptr<HeapItem<T>> item = std::shared_ptr<HeapItem<T>>(new HeapItem<T>(key));
                            Insert(item);
                            return item;
                        }

                        /// <summary>
                        /// Returns heap size.
                        /// </summary>
                        /// <returns>The size.</returns>
                        unsigned int Size() const noexcept
                        {
                            return _size;
                        }

                        /// <summary>
                        /// Returns minimum.
                        /// </summary>
                        /// <returns>The minimum item.</returns>
                        std::shared_ptr<HeapItem<T>> Minimum()
                        {
                            return _root;
                        }

                        /// <summary>
                        /// Clears whole heap.
                        /// </summary>
                        void Clear()
                        {
                            Clear(_root);
                        }

                        /// <summary>
                        /// Clears the node.
                        /// </summary>
                        /// <param name="ndoe">The node.</param>
                        void Clear(std::shared_ptr<HeapItem<T>> node)
                        {
                            if (node == nullptr)
                            {
                                return;
                            }

                            std::shared_ptr<HeapItem<T>> current = node;

                            while (true)
                            {
                                if (current->Left != nullptr && current->Left != node)
                                {
                                    std::shared_ptr<HeapItem<T>> temp = current;

                                    current = current->Left;

                                    if (temp->Child != nullptr)
                                    {
                                        Clear(temp->Child);
                                    }
                                }
                                else
                                {
                                    if (current->Child != nullptr)
                                    {
                                        Clear(current->Child);
                                    }

                                    break;
                                }
                            }
                        }

                        /// <summary>
                        /// Inserts the node.
                        /// </summary>
                        /// <param name="item">Node to insert.</param>
                        void Insert(std::shared_ptr<HeapItem<T>> item)
                        {
                            item->Rank = 0;
                            item->Parent = nullptr;
                            item->Child = nullptr;
                            item->Mark = false;

                            if (_root == nullptr)
                            {
                                _root = item;
                                item->Left = item;
                                item->Right = item;
                            }
                            else
                            {
                                _root->Left->Right = item;
                                item->Left = _root->Left;
                                _root->Left = item;
                                item->Right = _root;

                                if (item->Key < _root->Key)
                                {
                                    _root = item;
                                }
                            }

                            ++_size;
                        }

                        /// <summary>
                        /// Erases minimum node.
                        /// </summary>
                        /// <returns>The erased node.</returns>
                        std::shared_ptr<HeapItem<T>> EraseMinimum()
                        {
                            std::shared_ptr<HeapItem<T>> min;
                            std::shared_ptr<HeapItem<T>> child;
                            std::shared_ptr<HeapItem<T>> next;
                            std::shared_ptr<std::shared_ptr<HeapItem<T>>> childList;

                            min = _root;

                            if (min != nullptr)
                            {
                                child = min->Child;

                                if (child != nullptr)
                                {
                                    childList = Make1dArray(min->Rank);
                                    next = child;

                                    for (int i = 0; i < min->Rank; i++)
                                    {
                                        childList.get()[i]    = next;
                                        next = next->Right;
                                    }

                                    for (int i = 0; i < min->Rank; i++)
                                    {
                                        child = childList.get()[i];
                                        _root->Left->Right = child;
                                        child->Left = _root->Left;
                                        _root->Left = child;
                                        child->Right = _root;
                                        child->Parent = nullptr;
                                    }
                                }

                                min->Left->Right = min->Right;
                                min->Right->Left = min->Left;

                                if (min == min->Right)
                                {
                                    _root = nullptr;
                                }
                                else
                                {
                                    _root = min->Right;
                                    Merge();
                                }

                                _size--;
                            }

                            return min;
                        }

                        /// <summary>
                        /// Merges heap.
                        /// </summary>
                        void Merge()
                        {
                            std::shared_ptr<HeapItem<T>> nodeInRootList;
                            std::shared_ptr<HeapItem<T>> next;
                            std::shared_ptr<std::shared_ptr<HeapItem<T>>> newArray;
                            std::shared_ptr<std::shared_ptr<HeapItem<T>>> rootList;

                            unsigned rootSize;
                            int maxDegreePlus2;
                            int maxDegree;

                            maxDegree = static_cast<int>(floor(log(static_cast<double>(_size)) / log(static_cast<double>(1 + sqrt(static_cast<double>(5))) / 2)));
                            maxDegreePlus2 = maxDegree + 2;
                            newArray = Make1dArray(maxDegreePlus2);

                            for (int i = 0; i < maxDegreePlus2; ++i)
                            {
                                newArray.get()[i] = nullptr;
                            }

                            nodeInRootList = _root;
                            rootSize = 0;
                            next = nodeInRootList;

                            do
                            {
                                ++rootSize;
                                next = next->Right;
                            }
                            while (next != nodeInRootList);

                            rootList = Make1dArray(rootSize);

                            for (unsigned i = 0; i < rootSize; i++)
                            {
                                rootList.get()[i]    = next;
                                next = next->Right;
                            }

                            for (unsigned i = 0; i < rootSize; i++)
                            {
                                nodeInRootList = rootList.get()[i];

                                std::shared_ptr<HeapItem<T>> nodeInRootTemp = nodeInRootList;
                                int tempRank = nodeInRootTemp->Rank;

                                while (newArray.get()[tempRank] != nullptr)
                                {
                                    std::shared_ptr<HeapItem<T>> newArrayNode = newArray.get()[tempRank];

                                    if (nodeInRootTemp->Key > newArrayNode->Key)
                                    {
                                        std::shared_ptr<HeapItem<T>> temp = nodeInRootTemp;

                                        nodeInRootTemp = newArrayNode;
                                        newArrayNode = temp;
                                    }

                                    LinkHeapNodes(newArrayNode, nodeInRootTemp);

                                    newArray.get()[tempRank] = nullptr;

                                    ++tempRank;
                                }

                                newArray.get()[tempRank] = nodeInRootTemp;
                            }

                            _root = nullptr;

                            for (int i = 0; i < maxDegreePlus2; i++)
                            {
                                if (newArray.get()[i] != nullptr)
                                {
                                    if (_root == nullptr)
                                    {
                                        _root = newArray.get()[i];
                                        newArray.get()[i]->Left = newArray.get()[i];
                                        newArray.get()[i]->Right = newArray.get()[i];
                                    }
                                    else
                                    {
                                        _root->Left->Right = newArray.get()[i];
                                        newArray.get()[i]->Left = _root->Left;
                                        _root->Left = newArray.get()[i];
                                        newArray.get()[i]->Right = _root;

                                        if (newArray.get()[i]->Key < _root->Key)
                                        {
                                            _root = newArray.get()[i];
                                        }
                                    }
                                }
                            }
                        }

                        /// <summary>
                        /// Lowers the key.
                        /// </summary>
                        /// <param name="node">Node to modify.</param>
                        /// <param name="key">The new key.</param>
                        void LowerKey(std::shared_ptr<HeapItem<T>> node, int key)
                        {
                            if (key > node->Key)
                            {
                                throw std::exception("Current key is lower than new key.");
                            }

                            node->Key = key;

                            std::shared_ptr<HeapItem<T>> temp = node->Parent;

                            if (temp != nullptr && node->Key < temp->Key)
                            {
                                Slice(node, temp);
                                RecurrentSlice(temp);
                            }

                            if (node->Key < _root->Key)
                            {
                                _root = node;
                            }
                        }

                        /// <summary>
                        /// Slices the tree.
                        /// </summary>
                        /// <param name="leftNode">The left node.</param>
                        /// <param name="rightNode">The right node.</param>
                        void Slice(std::shared_ptr<HeapItem<T>> leftNode, std::shared_ptr<HeapItem<T>> rightNode)
                        {
                            if (leftNode->Right == leftNode)
                            {
                                rightNode->Child = nullptr;
                            }
                            else
                            {
                                leftNode->Right->Left = leftNode->Left;
                                leftNode->Left->Right = leftNode->Right;

                                if (rightNode->Child == leftNode)
                                {
                                    rightNode->Child = leftNode->Right;
                                }
                            }

                            --(rightNode->Rank);

                            _root->Right->Left = leftNode;
                            leftNode->Right = _root->Right;
                            _root->Right = leftNode;
                            leftNode->Left = _root;        
                            leftNode->Parent = nullptr;        
                            leftNode->Mark = false;
                        }

                        /// <summary>
                        /// Slices the tree.
                        /// </summary>
                        /// <param name="node">The node.</param>
                        void RecurrentSlice(std::shared_ptr<HeapItem<T>> node)
                        {
                            std::shared_ptr<HeapItem<T>> temp;

                            temp = node->Parent;

                            if (temp != nullptr)
                            {

                                if (node->Mark == false)
                                {
                                    node->Mark = true;
                                }
                                else
                                {
                                    Slice(node, temp);
                                    RecurrentSlice(temp);
                                }
                            }
                        }

                    private:
                        /// <summary>
                        /// Tree size.
                        /// </summary>
                        unsigned int _size;

                        /// <summary>
                        /// Root pointer.
                        /// </summary>
                        std::shared_ptr<HeapItem<T>> _root;

                        /// <summary>
                        /// Makes 2d array.
                        /// </summary>
                        /// <param name="rows">The size.</param>
                        /// <param name="cols">The size.</param>
                        /// <returns>The 2d array.</returns>
                        static std::shared_ptr<std::shared_ptr<HeapItem<T>>> Make2dArray(unsigned rows, unsigned cols)
                        {
                            std::shared_ptr<std::shared_ptr<HeapItem<T>>> arr =
                                std::shared_ptr<std::shared_ptr<HeapItem<T>>>(new std::shared_ptr<HeapItem<T>>[rows],
                                                                              [](std::shared_ptr<HeapItem<T>> * p) -> void { delete[] p; });

                            for (unsigned i = 0; i < rows; ++i)
                            {
                                arr.get()[i] = std::shared_ptr<HeapItem<T>>(new HeapItem<T>[cols],
                                                                            [](HeapItem<T> * p) -> void { delete[] p; });
                            }

                            return arr;
                        }

                        /// <summary>
                        /// Makes 1d array of tree.
                        /// </summary>
                        /// <param name="rows">The size.</param>
                        /// <returns>The 1d arary.</returns>
                        static std::shared_ptr<std::shared_ptr<HeapItem<T>>> Make1dArray(unsigned rows)
                        {
                            return std::shared_ptr<std::shared_ptr<HeapItem<T>>>(new std::shared_ptr<HeapItem<T>>[rows],
                                                                                 [](std::shared_ptr<HeapItem<T>> * arr) -> void { delete[] arr; });
                        }
                    };
                }
            }
        }
    }
}

#endif
