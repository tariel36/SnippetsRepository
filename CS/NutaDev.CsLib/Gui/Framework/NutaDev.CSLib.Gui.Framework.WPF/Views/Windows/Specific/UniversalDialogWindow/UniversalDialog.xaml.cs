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

using NutaDev.CSLib.Gui.Framework.WPF.Views.Dialogs.Abstract;
using NutaDev.CSLib.Gui.Framework.WPF.Views.Windows.Abstract;
using System.Windows;

namespace NutaDev.CSLib.Gui.Framework.WPF.Views.Windows.Specific.UniversalDialogWindow
{
    /// <summary>
    /// <para>This window is a universal dialog. Contains set of often used controls.</para>
    /// <para>Interaction logic for UniversalDialog.xaml</para>
    /// </summary>
    public partial class UniversalDialog
        : Window
        , ICloseable
        , IDialog
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UniversalDialog"/> class.
        /// </summary>
        public UniversalDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Shows dialog with properties based on passed parameters.
        /// </summary>
        /// <param name="owner">Dialog owner.</param>
        /// <param name="title">Window title.</param>
        /// <param name="header">Header to show.</param>
        /// <param name="text">Text to show.</param>
        /// <param name="yesButtonText">`Yes` button text.</param>
        /// <param name="noButtonText">`No` button text.</param>
        /// <returns>Dialog result - true, false or null.</returns>
        public static bool? ShowYesNoDialog(Window owner, string title, string header, string text, string yesButtonText = "Yes", string noButtonText = "No")
        {
            string input = null;
            object sel = null;

            return ShowDialog(owner, title, header, text, Visibility.Visible, Visibility.Visible, null, null, Visibility.Collapsed, ref input, Visibility.Collapsed,
                null, null, yesButtonText, noButtonText, Visibility.Collapsed, Visibility.Collapsed, Visibility.Visible, Visibility.Visible, null, null, Visibility.Collapsed, ref sel);
        }

        /// <summary>
        /// Shows dialog with properties based on passed parameters.
        /// </summary>
        /// <param name="owner">Dialog owner.</param>
        /// <param name="title">Window title.</param>
        /// <param name="header">Header to show.</param>
        /// <param name="text">Text to show.</param>
        /// <param name="okButtonText">`Ok` button text.</param>
        /// <param name="cancelButtonText">`Cancel` button text.</param>
        public static bool? ShowOkCancelDialog(Window owner, string title, string header, string text, string okButtonText = "OK", string cancelButtonText = "Cancel")
        {
            string input = null;
            object sel = null;

            return ShowDialog(owner, title, header, text, Visibility.Visible, Visibility.Visible, null, null, Visibility.Collapsed, ref input, Visibility.Collapsed,
                okButtonText, cancelButtonText, null, null, Visibility.Visible, Visibility.Visible, Visibility.Collapsed, Visibility.Collapsed, null, null, Visibility.Collapsed, ref sel);
        }

        /// <summary>
        /// Shows dialog with properties based on passed parameters.
        /// </summary>
        /// <param name="owner">Dialog owner.</param>
        /// <param name="title">Window title.</param>
        /// <param name="header">Header to show.</param>
        /// <param name="text">Text to show.</param>
        /// <param name="okButtonText">`Ok` button text.</param>
        /// <returns>Dialog result - true, false or null.</returns>
        public static bool? ShowOkDialog(Window owner, string title, string header, string text, string okButtonText = "OK")
        {
            string input = null;
            object sel = null;

            return ShowDialog(owner, title, header, text, Visibility.Visible, Visibility.Visible, null, null, Visibility.Collapsed, ref input, Visibility.Collapsed,
                okButtonText, null, null, null, Visibility.Visible, Visibility.Collapsed, Visibility.Collapsed, Visibility.Collapsed, null, null, Visibility.Collapsed, ref sel);
        }

        /// <summary>
        /// Shows dialog with properties based on passed parameters.
        /// </summary>
        /// <param name="owner">Dialog owner.</param>
        /// <param name="title">Window title.</param>
        /// <param name="header">Header to show.</param>
        /// <param name="text">Text to show.</param>
        /// <param name="inputText">Input text.</param>
        /// <param name="okButtonText">`Ok` button text.</param>
        /// <param name="cancelButtonText">`Cancel` button text.</param>
        /// <returns>Dialog result - true, false or null.</returns>
        public static bool? ShowInputDialog(Window owner, string title, string header, string text, ref string inputText, string okButtonText = "OK", string cancelButtonText = "Cancel")
        {
            object sel = null;

            return ShowDialog(owner, title, header, text, Visibility.Visible, Visibility.Visible, null, null, Visibility.Collapsed, ref inputText, Visibility.Visible,
                okButtonText, cancelButtonText, null, null, Visibility.Visible, Visibility.Visible, Visibility.Collapsed, Visibility.Collapsed, null, null, Visibility.Collapsed, ref sel);
        }

        /// <summary>
        /// Shows dialog with properties based on passed parameters.
        /// </summary>
        /// <param name="owner">Dialog owner.</param>
        /// <param name="title">Window title.</param>
        /// <param name="header">Header to show.</param>
        /// <param name="text">Text to show.</param>
        /// <param name="headerVisibility">Header visibility.</param>
        /// <param name="textVisibility">Text visibility.</param>
        /// <param name="icon">Icon to show.</param>
        /// <param name="image">Image to show.</param>
        /// <param name="imageVisibility">Image visibility.</param>
        /// <param name="inputText">Input text.</param>
        /// <param name="inputTextVisibility">Input text vsibility.</param>
        /// <param name="okButtonText">`Ok` button text.</param>
        /// <param name="cancelButtonText">`Cancel` button text.</param>
        /// <param name="yesButtonText">`Yes` button text.</param>
        /// <param name="noButtonText">`No` button text.</param>
        /// <param name="okButtonVisibility">`Ok` button visibility.</param>
        /// <param name="cancelButtonVisibility">`Cancel` button visibility.</param>
        /// <param name="yesButtonVisibility">`Yes` button visibility.</param>
        /// <param name="noButtonVisibility">`No` button visibility.</param>
        /// <param name="cbxDisplayMemberPath">Combobox display member path.</param>
        /// <param name="cbxItemsSource">Combobox items source.</param>
        /// <param name="cbxVisibility">Combobox visibility.</param>
        /// <param name="cbxSelectedItem">Selected item in combobox.</param>
        /// <returns>Dialog result - true, false or null.</returns>
        public static bool? ShowDialog(
            Window owner,
            string title, string header, string text,
            Visibility headerVisibility, Visibility textVisibility,
            object icon,
            object image, Visibility imageVisibility,
            ref string inputText, Visibility inputTextVisibility,
            string okButtonText, string cancelButtonText, string yesButtonText, string noButtonText,
            Visibility okButtonVisibility, Visibility cancelButtonVisibility, Visibility yesButtonVisibility, Visibility noButtonVisibility,
            string cbxDisplayMemberPath, object cbxItemsSource, Visibility cbxVisibility, ref object cbxSelectedItem)
        {
            UniversalDialogViewModel vm = new UniversalDialogViewModel();

            vm.Title = title;
            vm.Header = header;
            vm.Text = text;
            vm.Icon = icon;

            vm.Image = image;
            vm.InputText = inputText;

            vm.ButtonOkText = okButtonText;
            vm.ButtonYesText = yesButtonText;
            vm.ButtonNoText = noButtonText;
            vm.ButtonCancelText = cancelButtonText;

            vm.ComboBoxDisplayMemberPath = cbxDisplayMemberPath;
            vm.ComboBoxItemsSource = cbxItemsSource;
            vm.SelectedItem = cbxSelectedItem;

            vm.HeaderVisibility = headerVisibility;
            vm.TextVisibility = textVisibility;
            vm.ImageVisibility = imageVisibility;
            vm.InputTextVisibility = inputTextVisibility;
            vm.OkButtonVisibility = okButtonVisibility;
            vm.CancelButtonVisibility = cancelButtonVisibility;
            vm.YesButtonVisibility = yesButtonVisibility;
            vm.NoButtonVisibility = noButtonVisibility;
            vm.ComboBoxVisibility = cbxVisibility;

            bool? dialogResult = ShowDialog(owner, vm);

            inputText = vm.InputText;
            cbxSelectedItem = vm.SelectedItem;

            return dialogResult;
        }

        /// <summary>
        /// Shows dialog with properties based on <paramref name="viewModel"/>.
        /// </summary>
        /// <param name="owner">Dialog owner.</param>
        /// <param name="viewModel">Dialog properties.</param>
        /// <returns>Dialog result - true, false or null.</returns>
        public static bool? ShowDialog(Window owner, UniversalDialogViewModel viewModel)
        {
            UniversalDialog dlg = new UniversalDialog();
            dlg.Owner = owner;
            dlg.DataContext = viewModel;

            viewModel.InitializeCommands();

            bool? dialogResult = dlg.ShowDialog();

            return dialogResult;
        }
    }
}
