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
using NutaDev.CSLib.Gui.Framework.WPF.ViewModels.Abstract.Base;
using NutaDev.CSLib.Gui.Framework.WPF.ViewModels.Abstract.View.Base;
using NutaDev.CSLib.Gui.Framework.WPF.ViewModels.Abstract.View.Windows;
using NutaDev.CSLib.Gui.Framework.WPF.Views.Dialogs.Abstract;
using NutaDev.CSLib.Gui.Framework.WPF.Views.Windows.Abstract;
using System;
using System.Windows;

namespace NutaDev.CSLib.Gui.Framework.WPF.Views.Windows.Specific.UniversalDialogWindow
{
    /// <summary>
    /// View model for <see cref="UniversalDialog"/> window.
    /// </summary>
    /// <seealso cref="UniversalDialog"/>
    /// <seealso cref="WindowViewModel"/>
    /// <seealso cref="GuiViewModel"/>
    /// <seealso cref="BaseViewModel"/>
    public class UniversalDialogViewModel
        : WindowViewModel
    {
        #region Consts

        /// <summary>
        /// Grid length equal to 0.
        /// </summary>
        private static readonly GridLength HiddenGridLength = new GridLength(0.0);

        /// <summary>
        /// Default column width.
        /// </summary>
        private static readonly GridLength DefaultColumnDefinition1Width = new GridLength(50.0);

        /// <summary>
        /// Default column width.
        /// </summary>
        private static readonly GridLength DefaultColumnDefinition2Width = new GridLength(50.0);

        /// <summary>
        /// Default column width.
        /// </summary>
        private static readonly GridLength DefaultColumnDefinition3Width = new GridLength(246.0, GridUnitType.Star);

        /// <summary>
        /// Default column width.
        /// </summary>
        private static readonly GridLength DefaultColumnDefinition4Width = new GridLength(118.0, GridUnitType.Star);

        /// <summary>
        /// Default column width.
        /// </summary>
        private static readonly GridLength DefaultColumnDefinition5Width = new GridLength(119.0, GridUnitType.Star);

        /// <summary>
        /// Default row height.
        /// </summary>
        private static readonly GridLength DefaultRowDefinition1Height = new GridLength(28);

        /// <summary>
        /// Default row height.
        /// </summary>
        private static readonly GridLength DefaultRowDefinition2Height = new GridLength(50);

        /// <summary>
        /// Default row height.
        /// </summary>
        private static readonly GridLength DefaultRowDefinition3Height = new GridLength(50);

        /// <summary>
        /// Default row height.
        /// </summary>
        private static readonly GridLength DefaultRowDefinition4Height = new GridLength(33);

        /// <summary>
        /// Default row height.
        /// </summary>
        private static readonly GridLength DefaultRowDefinition5Height = new GridLength(28);

        #endregion

        #region Backing fields

        /// <summary>
        /// Backing field for <see cref="Title"/>.
        /// </summary>
        private string _title;

        /// <summary>
        /// Backing field for <see cref="Header"/>.
        /// </summary>
        private string _header;

        /// <summary>
        /// Backing field for <see cref="Text"/>.
        /// </summary>
        private string _text;

        /// <summary>
        /// Backing field for <see cref="ButtonOkText"/>.
        /// </summary>
        private string _buttonOkText;

        /// <summary>
        /// Backing field for <see cref="ButtonCancelText"/>.
        /// </summary>
        private string _buttonCancelText;

        /// <summary>
        /// Backing field for <see cref="ButtonYesText"/>.
        /// </summary>
        private string _buttonYesText;

        /// <summary>
        /// Backing field for <see cref="ButtonNoText"/>.
        /// </summary>
        private string _buttonNoText;

        /// <summary>
        /// Backing field for <see cref="InputText"/>.
        /// </summary>
        private string _inputText;

        /// <summary>
        /// Backing field for <see cref="ComboBoxDisplayMemberPath"/>.
        /// </summary>
        private string _comboBoxDisplayMemberPath;

        /// <summary>
        /// Backing field for <see cref="ComboBoxItemsSource"/>.
        /// </summary>
        private object _comboBoxItemsSource;

        /// <summary>
        /// Backing field for <see cref="SelectedItem"/>.
        /// </summary>
        private object _selectedItem;

        /// <summary>
        /// Backing field for <see cref="Image"/>.
        /// </summary>
        private object _image;

        /// <summary>
        /// Backing field for <see cref="Icon"/>.
        /// </summary>
        private object _icon;

        /// <summary>
        /// Backing field for <see cref="ComboBoxVisibility"/>.
        /// </summary>
        private Visibility _comboBoxVisibility;

        /// <summary>
        /// Backing field for <see cref="HeaderVisibility"/>.
        /// </summary>
        private Visibility _headerVisibility;

        /// <summary>
        /// Backing field for <see cref="CancelButtonVisibility"/>.
        /// </summary>
        private Visibility _cancelButtonVisibility;

        /// <summary>
        /// Backing field for <see cref="OkButtonVisibility"/>.
        /// </summary>
        private Visibility _okButtonVisibility;

        /// <summary>
        /// Backing field for <see cref="NoButtonVisibility"/>.
        /// </summary>
        private Visibility _noButtonVisibility;

        /// <summary>
        /// Backing field for <see cref="YesButtonVisibility"/>.
        /// </summary>
        private Visibility _yesButtonVisibility;

        /// <summary>
        /// Backing field for <see cref="InputTextVisibility"/>.
        /// </summary>
        private Visibility _inputTextVisibility;

        /// <summary>
        /// Backing field for <see cref="ImageVisibility"/>.
        /// </summary>
        private Visibility _imageVisibility;

        /// <summary>
        /// Backing field for <see cref="TextVisibility"/>.
        /// </summary>
        private Visibility _textVisibility;

        /// <summary>
        /// Backing field for <see cref="ColumnDefinition5Width"/>.
        /// </summary>
        private GridLength _columnDefinition5Width;

        /// <summary>
        /// Backing field for <see cref="ColumnDefinition4Width"/>.
        /// </summary>
        private GridLength _columnDefinition4Width;

        /// <summary>
        /// Backing field for <see cref="ColumnDefinition3Width"/>.
        /// </summary>
        private GridLength _columnDefinition3Width;

        /// <summary>
        /// Backing field for <see cref="ColumnDefinition2Width"/>.
        /// </summary>
        private GridLength _columnDefinition2Width;

        /// <summary>
        /// Backing field for <see cref="ColumnDefinition1Width"/>.
        /// </summary>
        private GridLength _columnDefinition1Width;

        /// <summary>
        /// Backing field for <see cref="RowDefinition5Height"/>.
        /// </summary>
        private GridLength _rowDefinition5Height;

        /// <summary>
        /// Backing field for <see cref="RowDefinition4Height"/>.
        /// </summary>
        private GridLength _rowDefinition4Height;

        /// <summary>
        /// Backing field for <see cref="RowDefinition3Height"/>.
        /// </summary>
        private GridLength _rowDefinition3Height;

        /// <summary>
        /// Backing field for <see cref="RowDefinition2Height"/>.
        /// </summary>
        private GridLength _rowDefinition2Height;

        /// <summary>
        /// Backing field for <see cref="RowDefinition1Height"/>.
        /// </summary>
        private GridLength _rowDefinition1Height;

        /// <summary>
        /// Backing field for <see cref="ShowGridLines"/>.
        /// </summary>
        private bool _showGridLines;

        /// <summary>
        /// Backing field for <see cref="CmdNoButton"/>.
        /// </summary>
        private RelayCommand _cmdNoButton;

        /// <summary>
        /// Backing field for <see cref="CmdYesButton"/>.
        /// </summary>
        private RelayCommand _cmdYesButton;

        /// <summary>
        /// Backing field for <see cref="CmdCancelButton"/>.
        /// </summary>
        private RelayCommand _cmdCancelButton;

        /// <summary>
        /// Backing field for <see cref="CmdOkButton"/>.
        /// </summary>
        private RelayCommand _cmdOkButton;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="UniversalDialogViewModel"/> class.
        /// </summary>
        public UniversalDialogViewModel()
        {
            ColumnDefinition1Width = DefaultColumnDefinition1Width;
            ColumnDefinition2Width = DefaultColumnDefinition2Width;
            ColumnDefinition3Width = DefaultColumnDefinition3Width;
            ColumnDefinition4Width = DefaultColumnDefinition4Width;
            ColumnDefinition5Width = DefaultColumnDefinition5Width;

            RowDefinition1Height = DefaultRowDefinition1Height;
            RowDefinition2Height = DefaultRowDefinition1Height;
            RowDefinition3Height = DefaultRowDefinition1Height;
            RowDefinition4Height = DefaultRowDefinition1Height;
            RowDefinition5Height = DefaultRowDefinition1Height;
        }

        #endregion

        #region Basic

        /// <summary>
        /// Gets or sets title.
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { Set(ref _title, value); }
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
        /// Gets or sets text.
        /// </summary>
        public string Text
        {
            get { return _text; }
            set { Set(ref _text, value); }
        }

        #endregion

        #region Button text

        /// <summary>
        /// Gets or sets `ok` button text.
        /// </summary>
        public string ButtonOkText
        {
            get { return _buttonOkText; }
            set { Set(ref _buttonOkText, value); }
        }

        /// <summary>
        /// Gets or sets `cancel` button text.
        /// </summary>
        public string ButtonCancelText
        {
            get { return _buttonCancelText; }
            set { Set(ref _buttonCancelText, value); }
        }

        /// <summary>
        /// Gets or sets `yes` button text.
        /// </summary>
        public string ButtonYesText
        {
            get { return _buttonYesText; }
            set { Set(ref _buttonYesText, value); }
        }

        /// <summary>
        /// Gets or sets `no` button text.
        /// </summary>
        public string ButtonNoText
        {
            get { return _buttonNoText; }
            set { Set(ref _buttonNoText, value); }
        }

        #endregion

        #region InputText

        /// <summary>
        /// Gets or sets input text.
        /// </summary>
        public string InputText
        {
            get { return _inputText; }
            set { Set(ref _inputText, value); }
        }

        #endregion

        #region ComboBox

        /// <summary>
        /// Gets or sets combobox item display member path.
        /// </summary>
        public string ComboBoxDisplayMemberPath
        {
            get { return _comboBoxDisplayMemberPath; }
            set { Set(ref _comboBoxDisplayMemberPath, value); }
        }

        /// <summary>
        /// Gets or sets combobox items source.
        /// </summary>
        public object ComboBoxItemsSource
        {
            get { return _comboBoxItemsSource; }
            set { Set(ref _comboBoxItemsSource, value); }
        }

        /// <summary>
        /// Gets or sets selected item.
        /// </summary>
        public object SelectedItem
        {
            get { return _selectedItem; }
            set { Set(ref _selectedItem, value); }
        }

        #endregion

        #region Images

        /// <summary>
        /// Gets or sets image.
        /// </summary>
        public object Image
        {
            get { return _image; }
            set { Set(ref _image, value); }
        }

        /// <summary>
        /// Gets or sets icon.
        /// </summary>
        public object Icon
        {
            get { return _icon; }
            set { Set(ref _icon, value); }
        }

        #endregion

        #region Visibility

        /// <summary>
        /// Gets or sets combobox visibility.
        /// </summary>
        public Visibility ComboBoxVisibility
        {
            get { return _comboBoxVisibility; }
            set
            {
                Set(ref _comboBoxVisibility, value);
                SetupVisibility();
            }
        }

        /// <summary>
        /// Gets or sets header visibility.
        /// </summary>
        public Visibility HeaderVisibility
        {
            get { return _headerVisibility; }
            set
            {
                Set(ref _headerVisibility, value);
                SetupVisibility();
            }
        }

        /// <summary>
        /// Gets or sets text visibility.
        /// </summary>
        public Visibility TextVisibility
        {
            get { return _textVisibility; }
            set
            {
                Set(ref _textVisibility, value);
                SetupVisibility();
            }
        }

        /// <summary>
        /// Gets or sets image visibility.
        /// </summary>
        public Visibility ImageVisibility
        {
            get { return _imageVisibility; }
            set
            {
                Set(ref _imageVisibility, value);
                SetupVisibility();
            }
        }

        /// <summary>
        /// Gets or sets input text visibility.
        /// </summary>
        public Visibility InputTextVisibility
        {
            get { return _inputTextVisibility; }
            set
            {
                Set(ref _inputTextVisibility, value);
                SetupVisibility();
            }
        }

        /// <summary>
        /// Gets or sets yes button visibility.
        /// </summary>
        public Visibility YesButtonVisibility
        {
            get { return _yesButtonVisibility; }
            set { Set(ref _yesButtonVisibility, value); }
        }

        /// <summary>
        /// Gets or sets no button visibility.
        /// </summary>
        public Visibility NoButtonVisibility
        {
            get { return _noButtonVisibility; }
            set { Set(ref _noButtonVisibility, value); }
        }

        /// <summary>
        /// Gets or sets ok button visibility.
        /// </summary>
        public Visibility OkButtonVisibility
        {
            get { return _okButtonVisibility; }
            set { Set(ref _okButtonVisibility, value); }
        }

        /// <summary>
        /// Gets or sets cancel button visibility.
        /// </summary>
        public Visibility CancelButtonVisibility
        {
            get { return _cancelButtonVisibility; }
            set { Set(ref _cancelButtonVisibility, value); }
        }

        #endregion

        #region Row heights

        /// <summary>
        /// Gets or sets row height.
        /// </summary>
        public GridLength RowDefinition1Height
        {
            get { return _rowDefinition1Height; }
            private set { Set(ref _rowDefinition1Height, value); }
        }

        /// <summary>
        /// Gets or sets row height.
        /// </summary>
        public GridLength RowDefinition2Height
        {
            get { return _rowDefinition2Height; }
            private set { Set(ref _rowDefinition2Height, value); }
        }

        /// <summary>
        /// Gets or sets row height.
        /// </summary>
        public GridLength RowDefinition3Height
        {
            get { return _rowDefinition3Height; }
            private set { Set(ref _rowDefinition3Height, value); }
        }

        /// <summary>
        /// Gets or sets row height.
        /// </summary>
        public GridLength RowDefinition4Height
        {
            get { return _rowDefinition4Height; }
            private set { Set(ref _rowDefinition4Height, value); }
        }

        /// <summary>
        /// Gets or sets row height.
        /// </summary>
        public GridLength RowDefinition5Height
        {
            get { return _rowDefinition5Height; }
            private set { Set(ref _rowDefinition5Height, value); }
        }

        #endregion

        #region Column widths

        /// <summary>
        /// Gets or sets column width.
        /// </summary>
        public GridLength ColumnDefinition1Width
        {
            get { return _columnDefinition1Width; }
            private set { Set(ref _columnDefinition1Width, value); }
        }

        /// <summary>
        /// Gets or sets column width.
        /// </summary>
        public GridLength ColumnDefinition2Width
        {
            get { return _columnDefinition2Width; }
            private set { Set(ref _columnDefinition2Width, value); }
        }

        /// <summary>
        /// Gets or sets column width.
        /// </summary>
        public GridLength ColumnDefinition3Width
        {
            get { return _columnDefinition3Width; }
            private set { Set(ref _columnDefinition3Width, value); }
        }

        /// <summary>
        /// Gets or sets column width.
        /// </summary>
        public GridLength ColumnDefinition4Width
        {
            get { return _columnDefinition4Width; }
            private set { Set(ref _columnDefinition4Width, value); }
        }

        /// <summary>
        /// Gets or sets column width.
        /// </summary>
        public GridLength ColumnDefinition5Width
        {
            get { return _columnDefinition5Width; }
            private set { Set(ref _columnDefinition5Width, value); }
        }

        #endregion

        #region Debug

        /// <summary>
        /// Indicates whether grid lines should be visible.
        /// </summary>
        public bool ShowGridLines
        {
            get { return _showGridLines; }
            set { Set(ref _showGridLines, value); }
        }

        #endregion

        #region Commands

        /// <summary>
        /// Gets or sets `ok` button command.
        /// </summary>
        public RelayCommand CmdOkButton
        {
            get { return _cmdOkButton; }
            private set { Set(ref _cmdOkButton, value); }
        }

        /// <summary>
        /// Gets or sets `cancel` button command.
        /// </summary>
        public RelayCommand CmdCancelButton
        {
            get { return _cmdCancelButton; }
            private set { Set(ref _cmdCancelButton, value); }
        }

        /// <summary>
        /// Gets or sets `yes` button command.
        /// </summary>
        public RelayCommand CmdYesButton
        {
            get { return _cmdYesButton; }
            private set { Set(ref _cmdYesButton, value); }
        }

        /// <summary>
        /// Gets or sets `no` button command.
        /// </summary>
        public RelayCommand CmdNoButton
        {
            get { return _cmdNoButton; }
            private set { Set(ref _cmdNoButton, value); }
        }

        #endregion

        #region Commands methods

        /// <summary>
        /// Initializes commands.
        /// </summary>
        public void InitializeCommands()
        {
            Action<object> succes = o =>
            {
                if (o is IDialog) { (o as IDialog).DialogResult = true; }
                (o as ICloseable)?.Close();
            };

            Action<object> fail = o =>
            {
                if (o is IDialog) { (o as IDialog).DialogResult = false; }
                (o as ICloseable)?.Close();
            };

            CmdOkButton = new RelayCommand(succes);
            CmdCancelButton = new RelayCommand(fail);

            CmdYesButton = new RelayCommand(succes);
            CmdNoButton = new RelayCommand(fail);
        }

        #endregion

        #region Visibility methods

        /// <summary>
        /// Setups row and column size.
        /// </summary>
        private void SetupVisibility()
        {
            RowDefinition1Height = HeaderVisibility == Visibility.Visible
                    ? DefaultRowDefinition1Height
                    : HiddenGridLength
                ;

            if (TextVisibility != Visibility.Visible && ImageVisibility != Visibility.Visible)
            {
                RowDefinition2Height = HiddenGridLength;
                RowDefinition3Height = HiddenGridLength;
            }
            else if (TextVisibility == Visibility.Visible || ImageVisibility == Visibility.Visible)
            {
                RowDefinition2Height = DefaultRowDefinition2Height;
                RowDefinition3Height = DefaultRowDefinition3Height;
            }

            if (InputTextVisibility != Visibility.Visible
                && ComboBoxVisibility != Visibility.Visible)
            {
                RowDefinition4Height = HiddenGridLength;
            }
            else
            {
                RowDefinition4Height = DefaultRowDefinition4Height;
            }

            if (TextVisibility != Visibility.Visible && ImageVisibility != Visibility.Visible)
            {
                ColumnDefinition1Width = HiddenGridLength;
                ColumnDefinition2Width = HiddenGridLength;
                ColumnDefinition3Width = HiddenGridLength;
                ColumnDefinition4Width = HiddenGridLength;
                ColumnDefinition5Width = HiddenGridLength;
            }

            if (InputTextVisibility != Visibility.Visible)
            {
                ColumnDefinition1Width = HiddenGridLength;
                ColumnDefinition2Width = HiddenGridLength;
                ColumnDefinition3Width = HiddenGridLength;
            }

            if (ComboBoxVisibility != Visibility.Visible)
            {
                ColumnDefinition4Width = HiddenGridLength;
                ColumnDefinition5Width = HiddenGridLength;
            }

            if (ImageVisibility == Visibility.Visible)
            {
                ColumnDefinition1Width = DefaultColumnDefinition1Width;
                ColumnDefinition2Width = DefaultColumnDefinition2Width;
            }

            if (InputTextVisibility == Visibility.Visible)
            {
                ColumnDefinition1Width = DefaultColumnDefinition1Width;
                ColumnDefinition2Width = DefaultColumnDefinition2Width;
                ColumnDefinition3Width = DefaultColumnDefinition3Width;
            }

            if (TextVisibility == Visibility.Visible)
            {
                ColumnDefinition3Width = DefaultColumnDefinition3Width;
                ColumnDefinition4Width = DefaultColumnDefinition4Width;
                ColumnDefinition5Width = DefaultColumnDefinition5Width;
            }

            if (ComboBoxVisibility == Visibility.Visible)
            {
                ColumnDefinition4Width = DefaultColumnDefinition4Width;
                ColumnDefinition5Width = DefaultColumnDefinition5Width;
            }
        }

        #endregion
    }
}
