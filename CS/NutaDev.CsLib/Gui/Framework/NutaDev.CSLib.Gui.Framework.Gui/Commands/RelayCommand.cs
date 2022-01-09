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

using System;
using System.Windows.Input;

namespace NutaDev.CSLib.Gui.Framework.Gui.Commands
{
    /// <summary>
    /// <para>The <see cref="RelayCommand"/> allows to bind command which action is provided by lambda expression or function delegate.</para>
    /// </summary>
    public class RelayCommand
        : ICommand
    {
        /// <summary>
        /// Action to perform on command execute.
        /// </summary>
        private Action<object> _execute;

        /// <summary>
        /// Checks if command can be executed.
        /// </summary>
        private Func<object, bool> _canExecute;

        /// <summary>
        /// Notifies listeners when <see cref="CanExecute"/> has changed.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Initializes new instance of the <see cref="RelayCommand"/> class.
        /// </summary>
        /// <param name="execute"></param>
        /// <param name="canExecute"></param>
        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = o => execute();

            if (canExecute != null)
            {
                _canExecute = o => canExecute();
            }
        }

        /// <summary>
        /// Initializes new instance of the <see cref="RelayCommand"/> class.
        /// </summary>
        /// <param name="execute"></param>
        /// <param name="canExecute"></param>
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Implicit cast from delegate.
        /// </summary>
        /// <param name="del">Delegate that will be used as action to execute.</param>
        public static implicit operator RelayCommand(Action<object> del)
        {
            return new RelayCommand(del);
        }

        /// <summary>
        /// Checks if command can be executed.
        /// </summary>
        /// <param name="parameter">Command parameter.</param>
        /// <returns>True if command can be executed or no check has been performed.</returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="parameter">Command parameter.</param>
        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
