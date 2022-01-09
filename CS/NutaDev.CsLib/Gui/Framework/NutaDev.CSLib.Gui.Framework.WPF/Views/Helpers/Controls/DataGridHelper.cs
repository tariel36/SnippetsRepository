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

using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace NutaDev.CSLib.Gui.Framework.WPF.Views.Helpers.Controls
{
    /// <summary>
    /// This class provides helper methods for <see cref="DataGrid"/> type.
    /// </summary>
    public static class DataGridHelper
    {
        /// <summary>
        /// <para>Returns cell on intersection of row and column indexes from provided grid.</para>
        /// </summary>
        /// <param name="dg">Provided DataGrid.</param>
        /// <param name="row">Index of row.</param>
        /// <param name="column">Index of column.</param>
        /// <returns>Selected DataGrid.</returns>
        public static DataGridCell GetCell(DataGrid dg, int row, int column)
        {
            DataGridRow rowContainer = GetRow(dg, row);

            if (rowContainer != null)
            {
                DataGridCellsPresenter presenter = WPFVisualTreeHelper.GetVisualChild<DataGridCellsPresenter>(rowContainer);
                DataGridCell cell = presenter.ItemContainerGenerator.ContainerFromIndex(column) as DataGridCell;

                if (cell == null)
                {
                    dg.ScrollIntoView(rowContainer, dg.Columns[column]);

                    cell = presenter.ItemContainerGenerator.ContainerFromIndex(column) as DataGridCell;
                }

                return cell;
            }

            return null;
        }

        /// <summary>
        /// <para>Gets the row at selected index.</para>
        /// </summary>
        /// <param name="dg">The DataGrid with row.</param>
        /// <param name="index">Index of row.</param>
        /// <returns>Selected row.</returns>
        public static DataGridRow GetRow(DataGrid dg, int index)
        {
            DataGridRow row = dg.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;

            if (row == null && index >= 0 && index < dg.Items.Count)
            {
                dg.ScrollIntoView(dg.Items[index]);

                row = dg.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
            }

            return row;
        }
    }
}
