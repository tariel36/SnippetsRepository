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

using NutaDev.CsLib.Collections.Collections.QuadTree.Abstract;
using NutaDev.CsLib.Maintenance.Exceptions.Factories;
using NutaDev.CsLib.Resources.Text.Exceptions;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace NutaDev.CsLib.Collections.Collections.QuadTree.Models
{
    /// <summary>
    /// Node of <see cref="Models.QuadTree{TObject}"/>
    /// </summary>
    /// <typeparam name="TObject"></typeparam>
    public class QuadTreeNode<TObject>
        where TObject : IQuadTreeBounds
    {
        /// <summary>
        /// Threshold before division.
        /// </summary>
        private const int NodeObjectThreshold = 2;

        /// <summary>
        /// Initial size of collection.
        /// </summary>
        private const int ItemsInitialSize = 64;

        /// <summary>
        /// The top left child.
        /// </summary>
        private QuadTreeNode<TObject> _topLeftChild;

        /// <summary>
        /// The top right child.
        /// </summary>
        private QuadTreeNode<TObject> _topRightChild;

        /// <summary>
        /// The bottom left child.
        /// </summary>
        private QuadTreeNode<TObject> _bottomLeftChild;

        /// <summary>
        /// The bottom right child.
        /// </summary>
        private QuadTreeNode<TObject> _bottomRightChild;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuadTreeNode"/> class.
        /// </summary>
        /// <param name="bounds">Node bounds.</param>
        public QuadTreeNode(Rectangle bounds)
        {
            Bounds = bounds;

            Items = new List<QuadTreeData<TObject>>(ItemsInitialSize);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QuadTreeNode"/> class.
        /// </summary>
        /// <param name="x">Bounds position X.</param>
        /// <param name="y">Bounds position Y.</param>
        /// <param name="width">Width of bounds.</param>
        /// <param name="height">Height of bounds.</param>
        public QuadTreeNode(int x, int y, int width, int height)
            : this(new Rectangle(x, y, width, height))
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QuadTreeNode"/> class.
        /// </summary>
        /// <param name="parent">Parent node.</param>
        /// <param name="bounds">Bounds of node.</param>
        private QuadTreeNode(QuadTreeNode<TObject> parent, Rectangle bounds)
            : this(bounds)
        {
            Parent = parent;
        }

        /// <summary>
        /// Gets bounds.
        /// </summary>
        public Rectangle Bounds { get; }

        /// <summary>
        /// Gets parent.
        /// </summary>
        public QuadTreeNode<TObject> Parent { get; }

        /// <summary>
        /// Gets items.
        /// </summary>
        public List<QuadTreeData<TObject>> Items { get; }

        /// <summary>
        /// Indicates whether node is empty or not.
        /// </summary>
        public bool IsEmpty { get { return Count == 0 && _topLeftChild == null; } }

        /// <summary>
        /// Gets count of items.
        /// </summary>
        public int Count
        {
            get
            {
                int count = Items.Count;

                count += _topLeftChild?.Count ?? 0;
                count += _topRightChild?.Count ?? 0;
                count += _bottomLeftChild?.Count ?? 0;
                count += _bottomRightChild?.Count ?? 0;

                return count;
            }
        }

        /// <summary>
        /// Adds item to tree.
        /// </summary>
        /// <param name="item">Item to add.</param>
        public void Add(QuadTreeData<TObject> item)
        {
            if (!Bounds.Contains(item.Value.ObjectBounds))
            {
                if (Parent != null)
                {
                    throw ExceptionFactory.InvalidOperationException(Text.TriedToInserIntoRootNodeButNodeIsNotARoot);
                }

                if (Parent == null)
                {
                    Insert(item);
                }
                else
                {
                    return;
                }
            }

            if (_topLeftChild == null && (Items.Count + 1) <= NodeObjectThreshold)
            {
                Insert(item);
            }
            else
            {
                if (_topLeftChild == null)
                {
                    Divide();
                }

                QuadTreeNode<TObject> targetNode = GetTargetNode(item);

                if (targetNode == this)
                {
                    Insert(item);
                }
                else
                {
                    targetNode.Add(item);
                }
            }
        }

        /// <summary>
        /// Removes item.
        /// </summary>
        /// <param name="item">Item to remove.</param>
        /// <param name="clean">Indicates whether should clean upwards.</param>
        public void Remove(QuadTreeData<TObject> item, bool clean)
        {
            if (item.Node != null)
            {
                if (item.Node == this)
                {
                    Delete(item);

                    if (clean)
                    {
                        ClearUpwards();
                    }
                }
                else
                {
                    item.Node.Remove(item, clean);
                }
            }
        }

        /// <summary>
        /// Clears the node.
        /// </summary>
        public void Clear()
        {
            _topLeftChild?.Clear();
            _topRightChild?.Clear();
            _bottomLeftChild?.Clear();
            _bottomRightChild?.Clear();

            Items.Clear();

            _topLeftChild = null;
            _topRightChild = null;
            _bottomLeftChild = null;
            _bottomRightChild = null;
        }

        /// <summary>
        /// Search objects within bounds.
        /// </summary>
        /// <param name="searchRect">Bounds to search.</param>
        /// <returns>The collection of results.</returns>
        public List<TObject> GetObjectsInRectangle(Rectangle searchRect)
        {
            List<TObject> results = new List<TObject>(ItemsInitialSize);

            GetObjectsInRectangle(searchRect, results);

            return results;
        }

        /// <summary>
        /// Find objects within bounds.
        /// </summary>
        /// <param name="searchRect">Bounds to search.</param>
        /// <param name="results">The collection of results.</param>
        public void GetObjectsInRectangle(Rectangle searchRect, List<TObject> results)
        {
            if (searchRect.Contains(Bounds))
            {
                GetObjects(results);
            }
            else if (searchRect.IntersectsWith(Bounds))
            {
                for (int i = 0; i < Items.Count; ++i)
                {
                    if (searchRect.IntersectsWith(Items[i].Value.ObjectBounds))
                    {
                        results.Add(Items[i].Value);
                    }
                }

                _topLeftChild?.GetObjectsInRectangle(searchRect, results);
                _topRightChild?.GetObjectsInRectangle(searchRect, results);
                _bottomLeftChild?.GetObjectsInRectangle(searchRect, results);
                _bottomRightChild?.GetObjectsInRectangle(searchRect, results);
            }
        }

        /// <summary>
        /// Moves the item.
        /// </summary>
        /// <param name="item"></param>
        public void Move(QuadTreeData<TObject> item)
        {
            if (item.Node != null)
            {
                item.Node.InternalMove(item);
            }
            else
            {
                InternalMove(item);
            }
        }

        /// <summary>
        /// Inserts the item.
        /// </summary>
        /// <param name="item">Item within tree.</param>
        private void Insert(QuadTreeData<TObject> item)
        {
            item.Node = this;
            Items.Add(item);
        }

        /// <summary>
        /// Deletes the node.
        /// </summary>
        /// <param name="item">Item within tree.</param>
        private void Delete(QuadTreeData<TObject> item)
        {
            int itemIndex = Items.IndexOf(item);

            if (itemIndex >= 0)
            {
                Items[itemIndex] = Items[Items.Count - 1];
                Items.RemoveAt(Items.Count - 1);
            }
        }

        /// <summary>
        /// Divides the tree.
        /// </summary>
        private void Divide()
        {
            Point halfSize = new Point(Bounds.Width / 2, Bounds.Height / 2);
            Point middlePoint = new Point(Bounds.X + halfSize.X, Bounds.Y + halfSize.Y);

            _topLeftChild = new QuadTreeNode<TObject>(this, new Rectangle(Bounds.Left, Bounds.Top, halfSize.X, halfSize.Y));
            _topRightChild = new QuadTreeNode<TObject>(this, new Rectangle(middlePoint.X, Bounds.Top, halfSize.X, halfSize.Y));
            _bottomLeftChild = new QuadTreeNode<TObject>(this, new Rectangle(Bounds.Left, middlePoint.Y, halfSize.X, halfSize.Y));
            _bottomRightChild = new QuadTreeNode<TObject>(this, new Rectangle(middlePoint.X, middlePoint.Y, halfSize.X, halfSize.Y));

            for (int i = 0; i < Items.Count; ++i)
            {
                QuadTreeNode<TObject> targetNode = GetTargetNode(Items[i]);

                if (targetNode != this)
                {
                    targetNode.Add(Items[i]);

                    Delete(Items[i]);

                    --i;
                }
            }
        }

        /// <summary>
        /// Gets node of the item.
        /// </summary>
        /// <param name="item">Item to test.</param>
        /// <returns>Node of item.</returns>
        private QuadTreeNode<TObject> GetTargetNode(QuadTreeData<TObject> item)
        {
            QuadTreeNode<TObject> targetNode = this;

            if (_topLeftChild.Bounds.Contains(item.Value.ObjectBounds))
            {
                targetNode = _topLeftChild;
            }
            else if (_topRightChild.Bounds.Contains(item.Value.ObjectBounds))
            {
                targetNode = _topRightChild;
            }
            else if (_bottomLeftChild.Bounds.Contains(item.Value.ObjectBounds))
            {
                targetNode = _bottomLeftChild;
            }
            else if (_bottomRightChild.Bounds.Contains(item.Value.ObjectBounds))
            {
                targetNode = _bottomRightChild;
            }

            return targetNode;
        }

        /// <summary>
        /// Gets the objects of the node and children.
        /// </summary>
        /// <param name="results"></param>
        private void GetObjects(List<TObject> results)
        {
            for (int i = 0; i < Items.Count; ++i)
            {
                results.Add(Items[i].Value);
            }

            _topLeftChild?.GetObjects(results);
            _topRightChild?.GetObjects(results);
            _bottomLeftChild?.GetObjects(results);
            _bottomRightChild?.GetObjects(results);
        }

        /// <summary>
        /// Moves the node.
        /// </summary>
        /// <param name="item"></param>
        private void InternalMove(QuadTreeData<TObject> item)
        {
            if (Bounds.Contains(item.Value.ObjectBounds))
            {
                if (_topLeftChild != null)
                {
                    QuadTreeNode<TObject> targetNode = GetTargetNode(item);

                    if (item.Node != targetNode)
                    {
                        QuadTreeNode<TObject> previousOwner = item.Node;

                        Remove(item, false);

                        targetNode.Add(item);

                        previousOwner.ClearUpwards();
                    }
                }
            }
            else
            {
                if (Parent != null)
                {
                    Parent.InternalMove(item);
                }
            }
        }

        /// <summary>
        /// Clears the node upwards.
        /// </summary>
        private void ClearUpwards()
        {
            if (_topLeftChild != null)
            {
                if (_topLeftChild.IsEmpty &&
                    _topRightChild.IsEmpty &&
                    _bottomLeftChild.IsEmpty &&
                    _bottomRightChild.IsEmpty)
                {
                    _topLeftChild = null;
                    _topRightChild = null;
                    _bottomLeftChild = null;
                    _bottomRightChild = null;

                    if (Parent != null && Count == 0)
                    {
                        Parent.ClearUpwards();
                    }
                }
            }
            else
            {
                if (Parent != null && Count == 0)
                {
                    Parent.ClearUpwards();
                }
            }
        }
    }
}
