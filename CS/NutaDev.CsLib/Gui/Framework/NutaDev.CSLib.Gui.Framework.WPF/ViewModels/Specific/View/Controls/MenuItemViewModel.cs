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

using NutaDev.CsLib.Gui.Framework.Gui.Commands;
using NutaDev.CsLib.Gui.Framework.WPF.ViewModels.Abstract.View.Controls;
using System;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;

namespace NutaDev.CsLib.Gui.Framework.WPF.ViewModels.Specific.View.Controls
{
    /// <summary>
    /// MenuItem view model.
    /// </summary>
    public class MenuItemViewModel
        : ControlViewModel
    {
        /// <summary>
        /// Backing field for <see cref="Header"/>.
        /// </summary>
        private string _header;

        /// <summary>
        /// Backing field for <see cref="Image"/>.
        /// </summary>
        private BitmapImage _image;

        /// <summary>
        /// Backing field for <see cref="IsImageVisible"/>.
        /// </summary>
        private bool _isImageVisible;

        /// <summary>
        /// Backing field for <see cref="Tag"/>.
        /// </summary>
        private object _tag;

        /// <summary>
        /// Backing field for <see cref="IsChecked"/>.
        /// </summary>
        private bool _isChecked;

        /// <summary>
        /// Backing field for <see cref="IsCheckable"/>.
        /// </summary>
        private bool _isCheckable;

        /// <summary>
        /// Backing field for <see cref="Command"/>.
        /// </summary>
        private RelayCommand _command;

        /// <summary>
        /// Backing field for <see cref="Items"/>.
        /// </summary>
        private ObservableCollection<MenuItemViewModel> _items;

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuItemViewModel"/> class.
        /// </summary>
        public MenuItemViewModel()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuItemViewModel"/> class.
        /// </summary>
        /// <param name="header">Menu item header.</param>
        public MenuItemViewModel(string header)
        {
            Header = header;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuItemViewModel"/> class.
        /// </summary>
        /// <param name="header">Menu item header.</param>
        /// <param name="filePath">Path to image.</param>
        public MenuItemViewModel(string header, string filePath)
        {
            Header = header;

            Image = new BitmapImage(new Uri(filePath));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuItemViewModel"/> class.
        /// </summary>
        /// <param name="header">Menu item header.</param>
        /// <param name="img">Image instance.</param>
        public MenuItemViewModel(string header, BitmapImage img)
        {
            Header = header;
            Image = img;
        }

        /// <summary>
        /// Gets or sets command.
        /// </summary>
        public RelayCommand Command
        {
            get { return _command; }
            set { Set(ref _command, value); }
        }

        /// <summary>
        /// Indicates whether menu item is checkable.
        /// </summary>
        public bool IsCheckable
        {
            get { return _isCheckable; }
            set { Set(ref _isCheckable, value); }
        }

        /// <summary>
        /// Indicates whether menu item is checked.
        /// </summary>
        public bool IsChecked
        {
            get { return _isChecked; }
            set { Set(ref _isChecked, value); }
        }

        /// <summary>
        /// Gets or sets inner items collection.
        /// </summary>
        public ObservableCollection<MenuItemViewModel> Items
        {
            get { return _items; }
            set { Set(ref _items, value); }
        }

        /// <summary>
        /// Gets or sets header.
        /// </summary>
        public string Header
        {
            get { return _header; }
            set { Set(ref _header, value); }
        }

        /// <summary>
        /// Gets or sets image.
        /// </summary>
        public BitmapImage Image
        {
            get { return _image; }
            set { Set(ref _image, value); }
        }

        /// <summary>
        /// Indicates whether <see cref="Image"/> should be visible.
        /// </summary>
        public bool IsImageVisible
        {
            get { return _isImageVisible; }
            set { Set(ref _isImageVisible, value); }
        }

        /// <summary>
        /// Gets or sets tag.
        /// </summary>
        public object Tag
        {
            get { return _tag; }
            set { Set(ref _tag, value); }
        }
    }
}
