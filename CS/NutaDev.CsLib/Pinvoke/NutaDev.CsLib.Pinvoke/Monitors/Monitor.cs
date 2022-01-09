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

using NutaDev.CsLib.Pinvoke.Handles;
using NutaDev.CsLib.Structures.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace NutaDev.CsLib.Pinvoke.Monitors
{
    /// <summary>
    /// Class that represents contains monitor info.
    /// </summary>
    public class Monitor
    {
        /// <summary>
        /// Id of primary monitor.
        /// </summary>
        public const int PrimaryMonitorId = 0x01;

        /// <summary>
        /// Gets monitor bounds.
        /// </summary>
        public Rectangle Bounds { get; }

        /// <summary>
        /// Gets monitor working area.
        /// </summary>
        public Rectangle WorkingArea { get; }

        /// <summary>
        /// Gets monitor name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Indicates whether monitor is considered primary monitor.
        /// </summary>
        public bool IsPrimary { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Monitor"/> class.
        /// </summary>
        /// <param name="monitorHandle">Handle to a monitor.</param>
        internal Monitor(IntPtr monitorHandle)
        {
            MonitorInfoEx info = new MonitorInfoEx();

            Externs.GetMonitorInfo(new HandleRef(null, monitorHandle), info);

            Bounds = new Rectangle(
                info.rcMonitor.left, info.rcMonitor.top,
                info.rcMonitor.right - info.rcMonitor.left,
                info.rcMonitor.bottom - info.rcMonitor.top);

            WorkingArea = new Rectangle(
                info.rcWork.left, info.rcWork.top,
                info.rcWork.right - info.rcWork.left,
                info.rcWork.bottom - info.rcWork.top);

            IsPrimary = (info.dwFlags & PrimaryMonitorId) != 0;

            Name = new string(info.szDevice).TrimEnd((char)0);
        }

        /// <summary>
        /// Returns collection of all monitors.
        /// </summary>
        /// <returns>Collection of all monitors.</returns>
        public static ICollection<Monitor> GetAllMonitors()
        {
            MonitorEnumCallback callback = new MonitorEnumCallback();

            Externs.EnumDisplayMonitors(HandleHelper.NullObject, IntPtr.Zero, callback.Callback, IntPtr.Zero);

            return callback.Monitors.ToList();
        }
    }
}
