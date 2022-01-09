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

using NutaDev.CsLib.Pinvoke.Structures;
using System.Runtime.InteropServices;

namespace NutaDev.CsLib.Pinvoke.Monitors
{
    /// <summary>
    /// The MONITORINFOEX structure contains information about a display monitor.
    /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-monitorinfo
    /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-tagmonitorinfoexa
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 4)]
    public class MonitorInfoEx
    {
        /// <summary>
        /// The size of the structure, in bytes.
        /// </summary>
        public int cbSize;

        /// <summary>
        /// A <see cref="Rect"/> structure that specifies the display monitor rectangle, expressed in virtual-screen coordinates.
        /// </summary>
        public Rect rcMonitor;

        /// <summary>
        /// A <see cref="Rect"/> structure that specifies the work area rectangle of the display monitor, expressed in virtual-screen coordinates. 
        /// </summary>
        public Rect rcWork;

        /// <summary>
        /// A set of flags that represent attributes of the display monitor.
        /// </summary>
        public int dwFlags;

        /// <summary>
        /// Device name.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public char[] szDevice;

        /// <summary>
        /// Initializes a new instance of the <see cref="MonitorInfoEx"/> class.
        /// </summary>
        public MonitorInfoEx()
        {
            cbSize = Marshal.SizeOf(typeof(MonitorInfoEx));
            rcMonitor = new Rect();
            rcWork = new Rect();
            dwFlags = 0;
            szDevice = new char[32];
        }
    }
}
