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

using NutaDev.CSLib.Gui.Framework.Gui.Commands;
using NutaDev.CSLib.Gui.Framework.WPF.ViewModels.Abstract.Models;
using System;
using System.Windows;

namespace NutaDev.CSLib.Gui.Framework.WPF.Views.Controls.Specific.GuidsGeneratorControl
{
    /// <summary>
    /// Single line with guid.
    /// </summary>
    public class GuidGeneratorItemViewModel
        : DataViewModel
    {
        /// <summary>
        /// Backing field for command.
        /// </summary>
        private RelayCommand _cmdCopyGuid;

        /// <summary>
        /// Backing field for guid.
        /// </summary>
        private Guid _guid;

        /// <summary>
        /// Initializes a new instance of the <see cref="GuidGeneratorItemViewModel"/>.
        /// </summary>
        public GuidGeneratorItemViewModel()
        {
            Guid = Guid.NewGuid();
            CmdCopyGuid = new RelayCommand(CopyGuid);
        }

        /// <summary>
        /// Gets or sets guid.
        /// </summary>
        public Guid Guid
        {
            get { return _guid; }
            set { _guid = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// Gets or sets copy command.
        /// </summary>
        public RelayCommand CmdCopyGuid
        {
            get { return _cmdCopyGuid; }
            set { _cmdCopyGuid = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// Copies guid to clipboard.
        /// </summary>
        private void CopyGuid()
        {
            Clipboard.SetText(Guid.ToString());
        }
    }
}
