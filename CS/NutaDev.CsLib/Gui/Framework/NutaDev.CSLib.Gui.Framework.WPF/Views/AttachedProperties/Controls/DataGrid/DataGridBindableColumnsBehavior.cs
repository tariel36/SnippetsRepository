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

using NutaDev.CsLib.Maintenance.Exceptions.Factories;
using NutaDev.CsLib.Resources.Text.Exceptions;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;

namespace NutaDev.CSLib.Gui.Framework.WPF.Views.AttachedProperties.Controls.DataGrid
{
    /// <summary>
    /// <para>Provides behaviour for bindable columns.</para>
    /// </summary>
    public class DataGridBindableColumnsBehavior
    {
        /// <summary>
        /// Dependency property name.
        /// </summary>
        private const string BindableColumns = nameof(BindableColumns);

        /// <summary>
        /// Dependency property for bindable columns.
        /// </summary>
        public static readonly DependencyProperty BindableColumnsProperty = DependencyProperty.RegisterAttached(BindableColumns,
            typeof(ObservableCollection<DataGridColumn>),
            typeof(DataGridBindableColumnsBehavior),
            new UIPropertyMetadata(null, BindableColumnsPropertyChanged));

        /// <summary>
        /// Property changed for bindable columns.
        /// </summary>
        /// <param name="source">Event source.</param>
        /// <param name="e">Event arguments.</param>
        private static void BindableColumnsPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            System.Windows.Controls.DataGrid dataGrid = source as System.Windows.Controls.DataGrid;
            ObservableCollection<DataGridColumn> newColumns = e.NewValue as ObservableCollection<DataGridColumn>;

            if (dataGrid == null) { throw ExceptionFactory.Create<InvalidOperationException>(Text.EventSourceIsNot_0_, nameof(DataGrid)); }

            dataGrid.Columns.Clear();

            if (newColumns != null)
            {
                foreach (DataGridColumn column in newColumns)
                {
                    dataGrid.Columns.Add(column);
                }

                newColumns.CollectionChanged += (sender, args) =>
                {
                    if (sender == null) { return; }
                    if (args == null) { return; }

                    switch (args.Action)
                    {
                        case NotifyCollectionChangedAction.Add:
                        {
                            if (args.NewItems?.Count > 0)
                            {
                                foreach (DataGridColumn column in args.NewItems)
                                {
                                    dataGrid.Columns.Add(column);
                                }
                            }

                            break;
                        }
                        case NotifyCollectionChangedAction.Remove:
                        {
                            if (args.OldItems?.Count > 0)
                            {
                                foreach (DataGridColumn column in args.OldItems)
                                {
                                    dataGrid.Columns.Remove(column);
                                }
                            }

                            break;
                        }
                        case NotifyCollectionChangedAction.Replace:
                        {
                            if (args.NewStartingIndex < dataGrid.Columns.Count
                                && args.NewItems.Count > 0
                                && args.NewItems[0] is DataGridColumn column)
                            {
                                dataGrid.Columns[args.NewStartingIndex] = column;
                            }

                            break;
                        }
                        case NotifyCollectionChangedAction.Move:
                        {
                            dataGrid.Columns.Move(args.OldStartingIndex, args.NewStartingIndex);
                            break;
                        }
                        case NotifyCollectionChangedAction.Reset:
                        {
                            dataGrid.Columns.Clear();

                            if (args.NewItems?.Count > 0)
                            {
                                foreach (DataGridColumn column in args.NewItems)
                                {
                                    dataGrid.Columns.Add(column);
                                }
                            }

                            break;
                        }
                    }
                };
            }
        }

        /// <summary>
        /// Sets the bindable columns.
        /// </summary>
        /// <param name="element">Target to set.</param>
        /// <param name="value">Columns to set.</param>
        public static void SetBindableColumns(DependencyObject element, ObservableCollection<DataGridColumn> value)
        {
            element.SetValue(BindableColumnsProperty, value);
        }

        /// <summary>
        /// Gets the bindable columns.
        /// </summary>
        /// <param name="element">Columns source.</param>
        /// <returns>Collection of bindable columns.</returns>
        public static ObservableCollection<DataGridColumn> GetBindableColumns(DependencyObject element)
        {
            return (ObservableCollection<DataGridColumn>)element.GetValue(BindableColumnsProperty);
        }
    }
}
