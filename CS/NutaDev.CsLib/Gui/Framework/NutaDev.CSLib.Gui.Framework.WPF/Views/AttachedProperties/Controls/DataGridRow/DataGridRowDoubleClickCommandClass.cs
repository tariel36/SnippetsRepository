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

using System.Windows;
using System.Windows.Input;

namespace NutaDev.CsLib.Gui.Framework.WPF.Views.AttachedProperties.Controls.DataGridRow
{
    /// <summary>
    /// This provides attached property that allows us to bind "double click on <see cref="DataGridRow"/>" command.
    /// </summary>
    public static class DataGridRowDoubleClickCommandClass
    {
        /// <summary>
        /// Dependency property name.
        /// </summary>
        private const string DataGridRowDoubleClickCommand = nameof(DataGridRowDoubleClickCommand);

        /// <summary>
        /// The dependency property for DataGridRow double click.
        /// </summary>
        public static readonly DependencyProperty DataGridRowDoubleClickProperty = DependencyProperty.RegisterAttached(DataGridRowDoubleClickCommand,
            typeof(ICommand),
            typeof(DataGridRowDoubleClickCommandClass),
            new PropertyMetadata(AttachOrRemoveDataGridRowDoubleClickEvent));

        /// <summary>
        /// Gets the command.
        /// </summary>
        /// <param name="obj">Source object.</param>
        /// <returns>Bound command.</returns>
        public static ICommand GetDataGridRowDoubleClickCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(DataGridRowDoubleClickProperty);
        }

        /// <summary>
        /// Sets the command.
        /// </summary>
        /// <param name="obj">Target object.</param>
        /// <param name="value">Command to set.</param>
        public static void SetDataGridRowDoubleClickCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(DataGridRowDoubleClickProperty, value);
        }

        /// <summary>
        /// Attaches or detaches command to row.
        /// </summary>
        /// <param name="obj">Source DataGrid.</param>
        /// <param name="args">Event arguments.</param>
        public static void AttachOrRemoveDataGridRowDoubleClickEvent(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            System.Windows.Controls.DataGrid dataGrid = obj as System.Windows.Controls.DataGrid;

            if (dataGrid != null)
            {
                ICommand cmd = (ICommand)args.NewValue;

                dataGrid.LoadingRow += (sender, rowArgs) =>
                {
                    rowArgs.Row.MouseDoubleClick += ExecuteDataGridRowDoubleClick;
                    SetDataGridRowDoubleClickCommand(rowArgs.Row, cmd);
                };

                dataGrid.UnloadingRow += (sender, rowArgs) =>
                {
                    rowArgs.Row.MouseDoubleClick -= ExecuteDataGridRowDoubleClick;
                    SetDataGridRowDoubleClickCommand(rowArgs.Row, null);
                };
            }
        }

        /// <summary>
        /// Executes attached command.
        /// </summary>
        /// <param name="sender">Event source.</param>
        /// <param name="args">Event arguments.</param>
        private static void ExecuteDataGridRowDoubleClick(object sender, MouseButtonEventArgs args)
        {
            DependencyObject obj = sender as DependencyObject;
            ICommand cmd = (ICommand)obj?.GetValue(DataGridRowDoubleClickProperty);

            if (cmd != null)
            {
                if (cmd.CanExecute(obj))
                {
                    cmd.Execute(obj);
                }
            }
        }
    }
}
