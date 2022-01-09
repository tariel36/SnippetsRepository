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
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NutaDev.CsLib.Pinvoke.Monitors
{
    /// <summary>
    /// Class that contains callback data for <see cref="Externs.EnumDisplayMonitors"/>.
    /// </summary>
    internal class MonitorEnumCallback
    {
        /// <summary>
        /// Contains collection of enumerated <see cref="Monitor"/>.
        /// </summary>
        public IReadOnlyCollection<Monitor> Monitors { get; }

        /// <summary>
        /// Inner collection of <see cref="Monitor"/>.
        /// </summary>
        private List<Monitor> InnerMonitors { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MonitorEnumCallback"/> class.
        /// </summary>
        public MonitorEnumCallback()
        {
            InnerMonitors = new List<Monitor>();
            Monitors = new ReadOnlyCollection<Monitor>(InnerMonitors);
        }

        /// <summary>
        /// Represents MONITORENUMPROC callback function header.
        /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nc-winuser-monitorenumproc
        /// </summary>
        /// <param name="hMonitor">Handle to screen.</param>
        /// <param name="hdcMonitor">Handle to device context.</param>
        /// <param name="lprcMonitor">RECT pointer.</param>
        /// <param name="dwData">User data.</param>
        /// <returns>True to continue enumeration, false to stop.</returns>
        public bool Callback(IntPtr hMonitor, IntPtr hdcMonitor, IntPtr lprcMonitor, IntPtr dwData)
        {
            InnerMonitors.Add(new Monitor(hMonitor));

            return true;
        }
    }
}
