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

namespace NutaDev.CSLib.Gui.Framework.WPF.Views.AttachedProperties.Windows.Window
{
    /// <summary>
    /// Wrappers for window events in form of commands.
    /// </summary>
    public static class WindowLoadedCommand
    {
        /// <summary>
        /// Window loaded command for window loaded event.
        /// </summary>
        public static readonly DependencyProperty WindowLoadedProperty = DependencyProperty.RegisterAttached(nameof(WindowLoadedCommand),
                typeof(ICommand),
                typeof(WindowLoadedCommand),
                new PropertyMetadata(AttachOrRemoveWindowLoadedEvent));

        /// <summary>
        /// Gets the WindowLoaded command.
        /// </summary>
        /// <param name="obj">Source object.</param>
        /// <returns>Set command.</returns>
        public static ICommand GetWindowLoadedCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(WindowLoadedProperty);
        }

        /// <summary>
        /// Sets the window loaded command.
        /// </summary>
        /// <param name="obj">Target object.</param>
        /// <param name="value">Command to set.</param>
        public static void SetWindowLoadedCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(WindowLoadedProperty, value);
        }

        /// <summary>
        /// Attaches or detaches command.
        /// </summary>
        /// <param name="obj">Target object.</param>
        /// <param name="args">Event arguments.</param>
        public static void AttachOrRemoveWindowLoadedEvent(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            if (obj is System.Windows.Window wnd
                && args.NewValue is ICommand cmd)
            {
                wnd.Loaded += (sender, loadedArgs) =>
                {
                    ExecuteWindowLoaded(sender);
                    SetWindowLoadedCommand(sender as System.Windows.Window, cmd);
                };

                wnd.Unloaded += (sender, loadedArgs) =>
                {
                    SetWindowLoadedCommand(sender as System.Windows.Window, null);
                };
            }
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="sender">Event source.</param>
        private static void ExecuteWindowLoaded(object sender)
        {
            DependencyObject obj = sender as DependencyObject;
            ICommand cmd = (ICommand)obj?.GetValue(WindowLoadedProperty);
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
