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
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Forms;

namespace NutaDev.CSLib.Gui.Framework.WFWPFIntegration.View.Controls
{
    /// <summary>
    /// Interaction logic for WrappedPropertyGrid.xaml
    /// </summary>
    public partial class WrappedPropertyGrid
        : System.Windows.Controls.UserControl
    {
        /// <summary>
        /// <see cref="DependencyProperty"/> fro <see cref="SelectedObject"/>.
        /// </summary>
        public static readonly DependencyProperty SelectedObjectProperty = DependencyProperty.Register(nameof(SelectedObject),
            typeof(object),
            typeof(WrappedPropertyGrid),
            new PropertyMetadata(default(object), PropertyChangedCallback));

        /// <summary>
        /// <see cref="DependencyProperty"/> fro <see cref="PropertyChangedCommand"/>.
        /// </summary>
        public static readonly DependencyProperty PropertyChangedCommandProperty = DependencyProperty.Register(nameof(PropertyChangedCommand),
            typeof(RelayCommand),
            typeof(WrappedPropertyGrid),
            new PropertyMetadata(default(RelayCommand), PropertyChangedCallback));

        /// <summary>
        /// Initializes a new instance of the <see cref="WrappedPropertyGrid"/> class.
        /// </summary>
        public WrappedPropertyGrid()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Property value changed event.
        /// </summary>
        public event EventHandler<PropertyValueChangedEventArgs> PropertyValueChangd;

        /// <summary>
        /// Gets or sets selected object.
        /// </summary>
        public object SelectedObject
        {
            get { return GetValue(SelectedObjectProperty); }
            set { SetValue(SelectedObjectProperty, value); }
        }

        /// <summary>
        /// Gets or sets property changed command.
        /// </summary>
        public RelayCommand PropertyChangedCommand
        {
            get { return (RelayCommand)GetValue(PropertyChangedCommandProperty); }
            set { SetValue(PropertyChangedCommandProperty, value); }
        }

        /// <summary>
        /// Property changed callback that setups inner controls properties.
        /// </summary>
        /// <param name="dependencyObject">Dependency object.</param>
        /// <param name="args">Event arguments.</param>
        private static void PropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            if (!string.IsNullOrWhiteSpace(args.Property?.Name)
                && dependencyObject is WrappedPropertyGrid wpg)
            {
                switch (args.Property.Name)
                {
                    case nameof(SelectedObject):
                    {
                        if (args.OldValue is INotifyPropertyChanged oldObj)
                        {
                            oldObj.PropertyChanged -= wpg.SelectedObjectOnPropertyChanged;
                        }

                        if (args.NewValue is INotifyPropertyChanged newObj)
                        {
                            newObj.PropertyChanged += wpg.SelectedObjectOnPropertyChanged;
                        }

                        wpg.PropertyGridControl.SelectedObject = args.NewValue;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Refreshes property grid when selected object changes.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="args">Event arguments.</param>
        private void SelectedObjectOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            PropertyGridControl.Refresh();
        }

        /// <summary>
        /// Executes <see cref="PropertyChangedCommand"/> when property grid value changes.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="args">Event arguments.</param>
        private void PropertyGridControl_OnPropertyValueChanged(object sender, PropertyValueChangedEventArgs args)
        {
            OnPropertyValueChangd(args);

            if (PropertyChangedCommand != null && PropertyChangedCommand.CanExecute(args))
            {
                PropertyChangedCommand.Execute(args);
            }
        }

        /// <summary>
        /// Invokes <see cref="PropertyValueChangd"/> event.
        /// </summary>
        /// <param name="args">Event arguments.</param>
        private void OnPropertyValueChangd(PropertyValueChangedEventArgs args)
        {
            PropertyValueChangd?.Invoke(this, args);
        }
    }
}
