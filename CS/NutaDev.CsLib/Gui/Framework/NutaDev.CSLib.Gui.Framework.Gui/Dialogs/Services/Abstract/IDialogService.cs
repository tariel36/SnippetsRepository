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

using NutaDev.CSLib.Gui.Framework.Gui.Dialogs.Models;

namespace NutaDev.CSLib.Gui.Framework.Gui.Dialogs.Services.Abstract
{
    /// <summary>
    /// Contains methods to work with dialogs.
    /// </summary>
    public interface IDialogService
    {
        /// <summary>
        /// Opens directory picker dialog.
        /// </summary>
        /// <param name="path">Picked path or <see cref="string.Empty"/>.</param>
        /// <returns>True if pick has been confirmed, otherwise false.</returns>
        bool OpenDirectoryPickerDialog(out string path);

        /// <summary>
        /// File picker dialog.
        /// </summary>
        /// <param name="filePath">Picked path or <see cref="string.Empty"/>.</param>
        /// <param name="filters">File extension filters.</param>
        /// <returns>True if pick has been confirmed, otherwise false.</returns>
        bool OpenGetFileDialog(out string filePath, DialogFilters filters = null);

        /// <summary>
        /// File save dialog.
        /// </summary>
        /// <param name="filePath">Picked path or <see cref="string.Empty"/>.</param>
        /// <param name="defaultFileName">Default file name for file.</param>
        /// <param name="filters">File extension filters.</param>
        /// <returns>True if pick has been confirmed, otherwise false.</returns>
        bool OpenSaveFileDialog(out string filePath, string defaultFileName = null, DialogFilters filters = null);
    }
}
