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

using Microsoft.WindowsAPICodePack.Dialogs;
using NutaDev.CSLib.Gui.Framework.Gui.Dialogs.Models;
using NutaDev.CSLib.Gui.Framework.Gui.Dialogs.Services.Abstract;
using System.Windows.Forms;

namespace NutaDev.CSLib.Gui.Framework.WinForms.Services.DialogService.Specific
{
    /// <summary>
    /// Contains methods to work with dialogs.
    /// </summary>
    public class DialogService
        : IDialogService
    {
        /// <summary>
        /// Returns reference to active window.
        /// </summary>
        public IWin32Window CurrentWindow { get { return Form.ActiveForm; } }

        /// <summary>
        /// Shows <see cref="MessageBox"/>.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        /// <param name="owner"><see cref="MessageBox"/> owner. If not provided <see cref="CurrentWindow"/> is used.</param>
        /// <param name="buttons">Buttons to show.</param>
        /// <param name="icon">Icon to show.</param>
        /// <returns><see cref="DialogResult"/> value.</returns>
        public DialogResult Show(string title,
            string message,
            IWin32Window owner = null,
            MessageBoxButtons buttons = MessageBoxButtons.OK,
            MessageBoxIcon icon = MessageBoxIcon.None)
        {
            return MessageBox.Show(owner ?? CurrentWindow, message, title, buttons, icon);
        }

        /// <summary>
        /// Opens directory picker dialog.
        /// </summary>
        /// <param name="path">Picked path or <see cref="string.Empty"/>.</param>
        /// <returns>True if pick has been confirmed, otherwise false.</returns>
        public bool OpenDirectoryPickerDialog(out string path)
        {
            using (CommonOpenFileDialog dlg = new CommonOpenFileDialog())
            {
                dlg.IsFolderPicker = true;
                dlg.EnsureFileExists = false;
                dlg.EnsurePathExists = false;
                dlg.EnsureValidNames = false;
                dlg.Multiselect = false;

                if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    path = dlg.FileName;

                    return true;
                }
            }

            path = string.Empty;

            return false;
        }

        /// <summary>
        /// File picker dialog.
        /// </summary>
        /// <param name="path">Picked path or <see cref="string.Empty"/>.</param>
        /// <param name="filters">File extension filters.</param>
        /// <returns>True if pick has been confirmed, otherwise false.</returns>
        public bool OpenGetFileDialog(out string path, DialogFilters filters = null)
        {
            using (CommonOpenFileDialog dlg = new CommonOpenFileDialog())
            {
                SetDialogFilters(dlg, filters);

                if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    path = dlg.FileName;

                    return true;
                }

                path = string.Empty;

                return false;
            }
        }

        /// <summary>
        /// File save dialog.
        /// </summary>
        /// <param name="path">Picked path or <see cref="string.Empty"/>.</param>
        /// <param name="filters">File extension filters.</param>
        /// <returns>True if pick has been confirmed, otherwise false.</returns>
        public bool OpenSaveFileDialog(out string filePath, DialogFilters filters = null)
        {
            return OpenSaveFileDialog(out filePath, null, filters);
        }

        /// <summary>
        /// File save dialog.
        /// </summary>
        /// <param name="filePath">Picked path or <see cref="string.Empty"/>.</param>
        /// <param name="defaultFileName">Default file name.</param>
        /// <param name="filters">File extension filters.</param>
        /// <returns>True if pick has been confirmed, otherwise false.</returns>
        public bool OpenSaveFileDialog(out string filePath, string defaultFileName = null, DialogFilters filters = null)
        {
            using (CommonSaveFileDialog dlg = new CommonSaveFileDialog())
            {
                dlg.EnsureFileExists = false;
                dlg.AlwaysAppendDefaultExtension = true;
                dlg.DefaultFileName = defaultFileName;

                SetDialogFilters(dlg, filters);

                if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    filePath = dlg.FileName;

                    return true;
                }

                filePath = string.Empty;

                return false;
            }
        }

        /// <summary>
        /// Sets filters to dialog.
        /// </summary>
        /// <param name="dlg">Dialog to setup.</param>
        /// <param name="filters">Filters to set.</param>
        private void SetDialogFilters(CommonFileDialog dlg, DialogFilters filters)
        {
            if (dlg == null || filters == null)
            {
                return;
            }

            foreach (DialogFilter filter in filters.Filters)
            {
                dlg.Filters.Add(new CommonFileDialogFilter(filter.Text, string.Join(";", filter.Extensions)));
            }
        }
    }
}
