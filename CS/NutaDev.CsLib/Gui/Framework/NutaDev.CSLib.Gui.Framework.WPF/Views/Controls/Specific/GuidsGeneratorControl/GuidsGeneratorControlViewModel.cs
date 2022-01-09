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
using NutaDev.CSLib.Gui.Framework.WPF.ViewModels.Abstract.View.Controls;
using System.Collections.ObjectModel;

namespace NutaDev.CSLib.Gui.Framework.WPF.Views.Controls.Specific.GuidsGeneratorControl
{
    /// <summary>
    /// View model for <see cref="GuidsGeneratorControl"/>.
    /// </summary>
    public class GuidsGeneratorControlViewModel
        : ControlViewModel
    {
        private ObservableCollection<GuidGeneratorItemViewModel> _guids;

        /// <summary>
        /// Initializes a new instance of the <see cref="GuidsGeneratorControlViewModel"/> class.
        /// </summary>
        public GuidsGeneratorControlViewModel()
        {
            CmdGenerate = new RelayCommand(Generate);

            Guids = new ObservableCollection<GuidGeneratorItemViewModel>();
        }

        /// <summary>
        /// Collection of generated guids.
        /// </summary>
        public ObservableCollection<GuidGeneratorItemViewModel> Guids
        {
            get { return _guids; }
            set { _guids = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// The command that triggers generation.
        /// </summary>
        public RelayCommand CmdGenerate { get; set; }

        /// <summary>
        /// Generates guids.
        /// </summary>
        private void Generate()
        {
            Guids.Clear();

            for (int i = 0; i < 20; ++i)
            {
                Guids.Add(new GuidGeneratorItemViewModel());
            }
        }
    }
}
