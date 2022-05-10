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

using NutaDev.CsLib.Gui.Framework.WPF.Views.Windows.Services.Abstract;
using System.Windows;

namespace NutaDev.CsLib.Gui.Framework.WPF.Views.Windows.Specific.UniversalDialogWindow
{
    /// <summary>
    /// Contains methods to work with <see cref="UniversalDialog"/>.
    /// </summary>
    public sealed class UniversalDialogService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UniversalDialogService"/> class.
        /// </summary>
        /// <param name="windowService">Window service.</param>
        public UniversalDialogService(IWindowService windowService)
        {
            WindowService = windowService;
        }

        /// <summary>
        /// Gets instance of the window service.
        /// </summary>
        private IWindowService WindowService { get; }

        /// <summary>
        /// Opens input text dialog.
        /// </summary>
        /// <param name="title">Dialog title.</param>
        /// <param name="header">Message header.</param>
        /// <param name="inputText">User's input text.</param>
        /// <param name="ok">Confirmation text.</param>
        /// <param name="cancel">Cancellation text.</param>
        /// <returns>True if dialog has been confirmed, false or null otherwise.</returns>
        public bool? OpenUniversalDialog(string title, string header, ref string inputText, string ok = null, string cancel = null)
        {
            object obj = null;

            return UniversalDialog.ShowDialog(WindowService.ActiveWindow, title, header, string.Empty, Visibility.Visible, Visibility.Visible, null, null, Visibility.Collapsed,
                ref inputText, Visibility.Visible, ok, cancel, null, null, Visibility.Visible, Visibility.Visible, Visibility.Collapsed, Visibility.Collapsed,
                null, null, Visibility.Collapsed, ref obj);
        }

        /// <summary>
        /// Opens error dialog.
        /// </summary>
        /// <param name="title">Dialog title.</param>
        /// <param name="header">Message header.</param>
        /// <param name="message">Message content</param>
        /// <returns>True if dialog has been confirmed, false or null otherwise.</returns>
        public bool? OpenErrorDialog(string title, string header, string message)
        {
            return UniversalDialog.ShowOkDialog(WindowService.ActiveWindow, title, header, message);
        }
    }
}
