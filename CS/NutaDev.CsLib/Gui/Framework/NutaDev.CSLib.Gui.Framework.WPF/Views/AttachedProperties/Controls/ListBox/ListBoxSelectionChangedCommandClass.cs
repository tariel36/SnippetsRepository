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
using System.Windows.Controls;
using System.Windows.Input;

namespace NutaDev.CsLib.Gui.Framework.WPF.Views.AttachedProperties.Controls.ListBox
{
    /// <summary>
    /// Attached property class that adds the SelectionChanged command (<see cref="ICommand"/>) to <see cref="ListBox"/>.
    /// </summary>
    public static class ListBoxSelectionChangedCommandClass
    {
        /// <summary>
        /// Dependency property name.
        /// </summary>
        private const string ListBoxSelectionChangedCommand = nameof(ListBoxSelectionChangedCommand);

        /// <summary>
        /// The command's <see cref="DependencyProperty"/>.
        /// </summary>
        public static readonly DependencyProperty ListBoxSelectionChangedProperty = DependencyProperty.RegisterAttached(ListBoxSelectionChangedCommand,
            typeof(ICommand),
            typeof(ListBoxSelectionChangedCommandClass),
            new PropertyMetadata(AttachOrRemoveListBoxSelectionChangedEvent));

        /// <summary>
        /// Gets the associated command.
        /// </summary>
        /// <param name="obj">Source object.</param>
        /// <returns>Associated command or default value.</returns>
        public static ICommand GetListBoxSelectionChangedCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(ListBoxSelectionChangedProperty);
        }

        /// <summary>
        /// Sets the command to target object.
        /// </summary>
        /// <param name="obj">Target object.</param>
        /// <param name="value">Command to assign.</param>
        public static void SetListBoxSelectionChangedCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(ListBoxSelectionChangedProperty, value);
        }

        /// <summary>
        /// Attaches the SelectionChangedEvent to <see cref="ListBox"/>.
        /// </summary>
        /// <param name="obj">Target <see cref="ListBox"/>.</param>
        /// <param name="args">Event arguments.</param>
        public static void AttachOrRemoveListBoxSelectionChangedEvent(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            System.Windows.Controls.ListBox listbox = obj as System.Windows.Controls.ListBox;

            if (listbox != null)
            {
                ICommand cmd = (ICommand)args.NewValue;

                listbox.Loaded += (sender, eventArgs) =>
                {
                    if (sender is System.Windows.Controls.ListBox)
                    {
                        (sender as System.Windows.Controls.ListBox).SelectionChanged += ExecuteListBoxSelectionChanged;
                        SetListBoxSelectionChangedCommand(sender as System.Windows.Controls.ListBox, cmd);
                    }
                };

                listbox.Unloaded += (sender, eventArgs) =>
                {
                    if (sender is System.Windows.Controls.ListBox)
                    {
                        (sender as System.Windows.Controls.ListBox).SelectionChanged += ExecuteListBoxSelectionChanged;
                        SetListBoxSelectionChangedCommand(sender as System.Windows.Controls.ListBox, null);
                    }
                };
            }
        }

        /// <summary>
        /// Fires the command on SelectionChangedEvent.
        /// </summary>
        /// <param name="sender">Source <see cref="ListBox"/>.</param>
        /// <param name="args">Event arguments.</param>
        private static void ExecuteListBoxSelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            DependencyObject obj = sender as DependencyObject;
            ICommand cmd = (ICommand)obj?.GetValue(ListBoxSelectionChangedProperty);

            if (cmd != null)
            {
                object[] cmdArgs = new object[] { sender, args };
                if (cmd.CanExecute(cmdArgs))
                {
                    cmd.Execute(cmdArgs);
                }
            }
        }
    }
}
