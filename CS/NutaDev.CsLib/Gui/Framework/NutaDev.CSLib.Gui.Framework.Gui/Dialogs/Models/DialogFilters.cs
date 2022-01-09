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

using System.Collections.Generic;

namespace NutaDev.CSLib.Gui.Framework.Gui.Dialogs.Models
{
    /// <summary>
    /// Class that defines dialog filters.
    /// </summary>
    public class DialogFilters
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DialogFilters"/> class.
        /// </summary>
        public DialogFilters()
        {
            InnerFilters = new List<DialogFilter>();
            Filters = InnerFilters;
        }

        /// <summary>
        /// Gets filters collection.
        /// </summary>
        public IReadOnlyCollection<DialogFilter> Filters { get; }

        /// <summary>
        /// Gets backing property for <see cref="Filters"/>.
        /// </summary>
        private List<DialogFilter> InnerFilters { get; }

        /// <summary>
        /// Creates new <see cref="DialogFilters"/> class instance and adds filter to its collection.
        /// </summary>
        /// <param name="text">Filter text.</param>
        /// <param name="extensions">Filter extensions.</param>
        /// <returns>Reference to new object.</returns>
        public static DialogFilters Create(string text, params string[] extensions)
        {
            return new DialogFilters().Add(text, extensions);
        }

        /// <summary>
        /// Adds filter to collection.
        /// </summary>
        /// <param name="text">Filter text.</param>
        /// <param name="extensions">Filter extensions.</param>
        /// <returns>Reference to itself.</returns>
        public DialogFilters Add(string text, params string[] extensions)
        {
            InnerFilters.Add(new DialogFilter(text, extensions));

            return this;
        }

        /// <summary>
        /// Adds filter to collection.
        /// </summary>
        /// <param name="filter">Filter to add.</param>
        /// <returns>Reference to itself.</returns>
        public DialogFilters Add(DialogFilter filter)
        {
            InnerFilters.Add(filter);

            return this;
        }
    }
}
