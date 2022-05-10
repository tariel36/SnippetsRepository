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

using System.Windows;
using System.Windows.Media;

namespace NutaDev.CsLib.Gui.Framework.WPF.Views.Helpers
{
    /// <summary>
    /// This class provides helper methods to operate on WPF visual tree.
    /// </summary>
    public static class WPFVisualTreeHelper
    {
        /// <summary>
        /// <para>Gets the visual child of selected type.</para>
        /// </summary>
        /// <typeparam name="T">Type of visual child.</typeparam>
        /// <param name="parent">Parent of visual child.</param>
        /// <returns>Visual child.</returns>
        public static T GetVisualChild<T>(Visual parent)
            where T : Visual
        {
            T child = default(T);

            if (parent != null)
            {
                int numVisuals = VisualTreeHelper.GetChildrenCount(parent);

                for (int i = 0; i < numVisuals; ++i)
                {
                    Visual visual = VisualTreeHelper.GetChild(parent, i) as Visual;

                    child = visual as T;

                    if (child == null)
                    {
                        child = GetVisualChild<T>(visual);
                    }

                    if (child != null)
                    {
                        break;
                    }
                }
            }

            return child;
        }

        /// <summary>
        /// Get the visual parent of the selected type.
        /// </summary>
        /// <typeparam name="T">Type of visual parent.</typeparam>
        /// <param name="child">Parent of visual parent.</param>
        /// <returns>Visual parent or null.</returns>
        public static T GetVisualParent<T>(DependencyObject child)
            where T : DependencyObject
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            if (parentObject == null)
            {
                return null;
            }

            T parent = parentObject as T;

            return parent ?? GetVisualParent<T>(parentObject);
        }
    }
}
