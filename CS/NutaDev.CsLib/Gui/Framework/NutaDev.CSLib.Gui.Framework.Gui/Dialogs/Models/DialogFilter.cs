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
    /// Filter class that contains filter text and extensions.
    /// </summary>
    public class DialogFilter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DialogFilter"/> class.
        /// </summary>
        /// <param name="text">Filter text.</param>
        /// <param name="extensions">Filter extensions.</param>
        public DialogFilter(string text, params string[] extensions)
        {
            Text = text;
            Extensions = extensions;
        }

        /// <summary>
        /// Gets filter text.
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// Gets filter extensions.
        /// </summary>
        public IReadOnlyCollection<string> Extensions { get; }
    }
}
