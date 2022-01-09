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

using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;

namespace NutaDev.CsLib.Maintenance.Logging.TraceListeners
{
    /// <summary>
    /// Text writer implemnetation that skips Microsoft's log formatting.
    /// </summary>
    public class TextWriterTraceListener
        : System.Diagnostics.TextWriterTraceListener
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextWriterTraceListener"/> class.
        /// </summary>
        public TextWriterTraceListener()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextWriterTraceListener"/> class.
        /// </summary>
        /// <param name="writer">Writer object.</param>
        /// <param name="name">Listener name.</param>
        public TextWriterTraceListener(TextWriter writer, string name)
            : base(writer, name)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextWriterTraceListener"/> class.
        /// </summary>
        /// <param name="stream">Stream object.</param>
        /// <param name="name">Listener name.</param>
        public TextWriterTraceListener(Stream stream, string name)
            : base(stream, name)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextWriterTraceListener"/> class.
        /// </summary>
        /// <param name="fileName">Target file path.</param>
        /// <param name="name">Listener name.</param>
        public TextWriterTraceListener(string fileName, string name)
            : base(fileName, name)
        {

        }

        /// <summary>
        /// Traces data.
        /// </summary>
        /// <param name="eventCache">Event cache.</param>
        /// <param name="source">Data source.</param>
        /// <param name="eventType">Event type.</param>
        /// <param name="id">Event id.</param>
        /// <param name="data">Trace data.</param>
        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
        {
            if (Filter != null && !Filter.ShouldTrace(eventCache, source, eventType, id, null, null, data, null))
            {
                return;
            }

            string datastring = data.ToString();

            WriteLine(datastring);
        }

        /// <summary>
        /// Traces data.
        /// </summary>
        /// <param name="eventCache">Event cache.</param>
        /// <param name="source">Data source.</param>
        /// <param name="eventType">Event type.</param>
        /// <param name="id">Event id.</param>
        /// <param name="data">Trace data.</param>
        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, params object[] data)
        {
            if (Filter != null && !Filter.ShouldTrace(eventCache, source, eventType, id, null, null, null, data))
            {
                return;
            }

            StringBuilder sb = new StringBuilder();

            if (data != null)
            {
                for (int i = 0; i < data.Length; ++i)
                {
                    if (i != 0)
                    {
                        sb.Append(", ");
                    }

                    if (data[i] != null)
                    {
                        sb.Append(data[i]);
                    }
                }
            }

            WriteLine(sb.ToString());
        }

        /// <summary>
        /// Traces data.
        /// </summary>
        /// <param name="eventCache">Event cache.</param>
        /// <param name="source">Data source.</param>
        /// <param name="eventType">Event type.</param>
        /// <param name="id">Event id.</param>
        /// <param name="message">Message to write.</param>
        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
        {
            if (Filter != null && !Filter.ShouldTrace(eventCache, source, eventType, id, message, null, null, null))
            {
                return;
            }

            WriteLine(message);
        }

        /// <summary>
        /// Traces data.
        /// </summary>
        /// <param name="eventCache">Event cache.</param>
        /// <param name="source">Data source.</param>
        /// <param name="eventType">Event type.</param>
        /// <param name="id">Event id.</param>
        /// <param name="format">Message format.</param>
        /// <param name="args">Message arguments.</param>
        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string format, params object[] args)
        {
            if (Filter != null && !Filter.ShouldTrace(eventCache, source, eventType, id, format, args, null, null))
            {
                return;
            }

            WriteLine(string.Format(CultureInfo.InvariantCulture, format, args));
        }
    }
}
