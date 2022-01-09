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
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace NutaDev.CsLib.Collections.Collections.QuadTree.Models
{
    /// <summary>
    /// The quad tree.
    /// </summary>
    /// <typeparam name="TObject"></typeparam>
    public class QuadTree<TObject>
        : ICollection<TObject>
        where TObject : IQuadTreeBounds
    {
        /// <summary>
        /// Items within tree.
        /// </summary>
        private readonly Dictionary<TObject, QuadTreeData<TObject>> _items = new Dictionary<TObject, QuadTreeData<TObject>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="QuadTree"/> class.
        /// </summary>
        /// <param name="bounds">Node bounds.</param>
        public QuadTree(Rectangle bounds)
        {
            Root = new QuadTreeNode<TObject>(bounds);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QuadTree"/> class.
        /// </summary>
        /// <param name="x">Bounds position X.</param>
        /// <param name="y">Bounds position Y.</param>
        /// <param name="width">Width of bounds.</param>
        /// <param name="height">Height of bounds.</param>
        public QuadTree(int x, int y, int width, int height)
        {
            Root = new QuadTreeNode<TObject>(new Rectangle(x, y, width, height));
        }

        /// <summary>
        /// Bounds of <see cref="Root"/>.
        /// </summary>
        public Rectangle Bounds { get { return Root.Bounds; } }

        /// <summary>
        /// Root of treel.
        /// </summary>
        public QuadTreeNode<TObject> Root { get; }

        /// <summary>
        /// The count of items in tree.
        /// </summary>
        public int Count { get { return _items.Count; } }

        /// <summary>
        /// Indicates whether the tree is readonly.
        /// </summary>
        public bool IsReadOnly { get { return false; } }

        /// <summary>
        /// Adds item to tree.
        /// </summary>
        /// <param name="item">Item to add.</param>
        public void Add(TObject item)
        {
            if (item != null)
            {
                QuadTreeData<TObject> wrappedObject = new QuadTreeData<TObject>(item);
                _items.Add(item, wrappedObject);
                Root.Add(wrappedObject);
            }
        }

        /// <summary>
        /// Removes the item from tree.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        /// <returns>True if item has been removed.</returns>
        public bool Remove(TObject item)
        {
            if (item != null && Contains(item))
            {
                Root.Remove(_items[item], true);
                _items.Remove(item);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Clears the tree.
        /// </summary>
        public void Clear()
        {
            _items.Clear();
            Root.Clear();
        }

        /// <summary>
        /// Checks if tree contains the item.
        /// </summary>
        /// <param name="item">Item to check.</param>
        /// <returns>True if item is within the tree.</returns>
        public bool Contains(TObject item)
        {
            return item != null && _items.ContainsKey(item);
        }

        /// <summary>
        /// Copies items to array.
        /// </summary>
        /// <param name="array">Array to copy to.</param>
        /// <param name="arrayIndex">Starting index.</param>
        public void CopyTo(TObject[] array, int arrayIndex)
        {
            _items.Keys.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Returns objects.
        /// </summary>
        /// <returns>THe objects.</returns>
        public List<TObject> GetObjects()
        {
            return new List<TObject>(_items.Keys);
        }

        /// <summary>
        /// Returns objects in bounds of rectangle.
        /// </summary>
        /// <param name="rect">The bounds.</param>
        /// <returns>Found items.</returns>
        public List<TObject> GetObjectsInRectangle(Rectangle rect)
        {
            return Root.GetObjectsInRectangle(rect);
        }

        /// <summary>
        /// Returns objects in bounds of rectangle.
        /// </summary>
        /// <param name="rect">The bounds.</param>
        /// <param name="results">Found items.</param>
        public void GetObjectsInRectangle(Rectangle rect, List<TObject> results)
        {
            Root.GetObjectsInRectangle(rect, results);
        }

        /// <summary>
        /// Moves the item.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Move(TObject item)
        {
            if (Contains(item))
            {
                Root.Move(_items[item]);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Enumerates the tree.
        /// </summary>
        /// <returns>Tree enumerator.</returns>
        public IEnumerator<TObject> GetEnumerator()
        {
            return _items.Keys.GetEnumerator();
        }

        /// <summary>
        /// Enumerates the tree.
        /// </summary>
        /// <returns>Tree enumerator.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
