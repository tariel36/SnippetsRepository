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
using NutaDev.CSLib.Gui.Framework.Gui.Dialogs.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NutaDev.CSLib.Gui.Framework.WPF.Views.Controls.Specific.FilePathPickerControl
{
    /// <summary>
    /// Interaction logic for FilePathPicker.xaml
    /// </summary>
    public partial class FilePathPicker
        : UserControl
    {
        /// <summary>
        /// File path property.
        /// </summary>
        public static readonly DependencyProperty FilePathProperty = DependencyProperty.Register(nameof(FilePath),
            typeof(string),
            typeof(FilePathPicker),
            new FrameworkPropertyMetadata(default(string), PropertyChangedCallback) { DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, BindsTwoWayByDefault = true });

        /// <summary>
        /// Label text property.
        /// </summary>
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof(Text),
            typeof(string),
            typeof(FilePathPicker),
            new PropertyMetadata(default(string), PropertyChangedCallback));

        /// <summary>
        /// Dialog filters property.
        /// </summary>
        public static readonly DependencyProperty FiltersProperty = DependencyProperty.Register(nameof(Filters),
            typeof(DialogFilters),
            typeof(FilePathPicker),
            new PropertyMetadata(default(DialogFilters), PropertyChangedCallback));

        /// <summary>
        /// Select button text property.
        /// </summary>
        public static readonly DependencyProperty SelectButtonTextProperty = DependencyProperty.Register(nameof(SelectButtonText),
            typeof(string),
            typeof(FilePathPicker),
            new PropertyMetadata(default(string), PropertyChangedCallback));

        /// <summary>
        /// Select button icon property.
        /// </summary>
        public static readonly DependencyProperty SelectButtonIconProperty = DependencyProperty.Register(nameof(SelectButtonIcon),
            typeof(BitmapImage),
            typeof(FilePathPicker),
            new PropertyMetadata(default(BitmapImage), PropertyChangedCallback));

        /// <summary>
        /// View service property.
        /// </summary>
        public static readonly DependencyProperty ViewServiceProperty = DependencyProperty.Register(nameof(ViewService),
            typeof(IDialogService),
            typeof(FilePathPicker),
            new PropertyMetadata(default(IDialogService), PropertyChangedCallback));

        /// <summary>
        /// Initializes a new instance of the <see cref="FilePathPicker"/> class.
        /// </summary>
        public FilePathPicker()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets label text.
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        /// <summary>
        /// Gets or sets file path.
        /// </summary>
        public string FilePath
        {
            get { return (string)GetValue(FilePathProperty); }
            set { SetValue(FilePathProperty, value); }
        }

        /// <summary>
        /// Gets or sets file dialog filters.
        /// </summary>
        public DialogFilters Filters
        {
            get { return (DialogFilters)GetValue(FiltersProperty); }
            set { SetValue(FiltersProperty, value); }
        }

        /// <summary>
        /// Gets or sets view service object.
        /// </summary>
        public IDialogService ViewService
        {
            get { return (IDialogService)GetValue(ViewServiceProperty); }
            set { SetValue(ViewServiceProperty, value); }
        }

        /// <summary>
        /// Gets or sets select button text.
        /// </summary>
        public string SelectButtonText
        {
            get { return (string)GetValue(SelectButtonTextProperty); }
            set { SetValue(SelectButtonTextProperty, value); }
        }

        /// <summary>
        /// Gets or sets select button image.
        /// </summary>
        public BitmapImage SelectButtonIcon
        {
            get { return (BitmapImage)GetValue(SelectButtonIconProperty); }
            set { SetValue(SelectButtonIconProperty, value); }
        }

        /// <summary>
        /// Handles property updates.
        /// </summary>
        /// <param name="dependencyObject">Event sender.</param>
        /// <param name="args">Event args.</param>
        private static void PropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            if (dependencyObject is FilePathPicker ctrl && !string.IsNullOrWhiteSpace(args.Property?.Name))
            {
                switch (args.Property.Name)
                {
                    case nameof(FilePath):
                        ctrl.FilePathTextBox.Text = args.NewValue.ToString();
                        break;
                    case nameof(Text):
                        ctrl.TextLabel.Content = args.NewValue.ToString();
                        break;
                    case nameof(SelectButtonText):
                    case nameof(SelectButtonIcon):
                    {
                        if (ctrl.ChooseButton.Content is Image && args.NewValue is string)
                        {
                            return;
                        }

                        if (args.NewValue is string)
                        {
                            ctrl.ChooseButton.Content = args.NewValue;
                        }
                        else
                        {
                            ctrl.ChooseButton.Content = new Image()
                            {
                                Source = (BitmapImage)args.NewValue
                            };
                        }

                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Opens get file dialog.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="args">Event args.</param>
        private void Select_OnClick(object sender, RoutedEventArgs args)
        {
            string path = null;

            if (ViewService?.OpenGetFileDialog(out path, Filters) == true)
            {
                FilePathTextBox.Text = path;
                FilePath = path;
                GetBindingExpression(FilePathProperty)?.UpdateTarget();
                GetBindingExpression(FilePathProperty)?.UpdateSource();
            }
        }
    }
}
