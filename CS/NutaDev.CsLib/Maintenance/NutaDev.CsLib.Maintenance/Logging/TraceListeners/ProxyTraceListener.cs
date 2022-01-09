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

using NutaDev.CsLib.Maintenance.Logging.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace NutaDev.CsLib.Maintenance.Logging.TraceListeners
{
    /// <summary>
    /// Class that acts as a proxy between logger that utilizes <see cref="TraceListener"/> descendants.
    /// </summary>
    public class ProxyTraceListener
        : TraceListener
    {
        /// <summary>
        /// Default event id.
        /// </summary>
        private const int DefaultId = 0;

        /// <summary>
        /// Default event type.
        /// </summary>
        private const TraceEventType DefaultEventType = TraceEventType.Verbose;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProxyTraceListener"/> class.
        /// </summary>
        public ProxyTraceListener()
        {
            Logs = new Queue<LogEntry>();
            IsQueueEnabled = false;
        }

        /// <summary>
        /// Indicates whether queue is enabled.
        /// </summary>
        public bool IsQueueEnabled { get; set; }

        /// <summary>
        /// Gets or sets action that is executed when something writes to listener.
        /// </summary>
        public Action<LogEntry> WriteTarget { get; set; }

        /// <summary>
        /// Gets queue of log entries written to instance of <see cref="ProxyTraceListener"/>.
        /// </summary>
        private Queue<LogEntry> Logs { get; }

        /// <summary>
        /// Gets or sets name of this trace listener.
        /// </summary>
        public override string Name { get; set; }

        /// <summary>
        /// Indicates whether this listener is thread safe.
        /// </summary>
        public override bool IsThreadSafe { get { return false; } }

        /// <summary>
        /// Writes to listener.
        /// </summary>
        /// <param name="message">Message to be written.</param>
        public override void Write(string message)
        {
            if (ShouldIgnore(null, string.Empty, DefaultEventType, DefaultId, message, null, null, null))
            {
                return;
            }

            TraceData(DefaultId, DefaultEventType, message);
        }

        /// <summary>
        /// Writes to listener.
        /// </summary>
        /// <param name="message">Message to be written.</param>
        public override void WriteLine(string message)
        {
            if (ShouldIgnore(null, string.Empty, DefaultEventType, DefaultId, message, null, null, null))
            {
                return;
            }

            TraceData(DefaultId, DefaultEventType, $"{message}{Environment.NewLine}");
        }

        /// <summary>
        /// Writes data to listener.
        /// </summary>
        /// <param name="eventCache">Events cache.</param>
        /// <param name="source">Source of event.</param>
        /// <param name="eventType">Event type.</param>
        /// <param name="id">Event id.</param>
        /// <param name="data">Event data to write.</param>
        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
        {
            if (ShouldIgnore(null, string.Empty, eventType, id, null, null, null, null))
            {
                return;
            }

            TraceData(eventCache, source, eventType, id, new[] { data });
        }

        /// <summary>
        /// Writes data to listener.
        /// </summary>
        /// <param name="eventCache">Events cache.</param>
        /// <param name="source">Source of event.</param>
        /// <param name="eventType">Event type.</param>
        /// <param name="id">Event id.</param>
        /// <param name="data">An array of data to write.</param>
        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, params object[] data)
        {
            if (ShouldIgnore(eventCache, source, eventType, id, null, null, null, null))
            {
                return;
            }

            TraceData(id, eventType, CreateMessage(null, data));
        }

        /// <summary>
        /// Writes fail message.
        /// </summary>
        /// <param name="message">Message to write.</param>
        public override void Fail(string message)
        {
            Fail(message, string.Empty);
        }

        /// <summary>
        /// Writes fail message.
        /// </summary>
        /// <param name="message">Message to write.</param>
        /// <param name="detailMessage">Details of fail.</param>
        public override void Fail(string message, string detailMessage)
        {
            StringBuilder failMessage = new StringBuilder();

            failMessage.Append($"TraceListenerFail {message}");

            if (!string.IsNullOrWhiteSpace(detailMessage))
            {
                failMessage.Append(" ");
                failMessage.Append(detailMessage);
            }

            TraceData(DefaultId, TraceEventType.Critical, failMessage.ToString());
        }

        /// <summary>
        /// Writes object to listener.
        /// </summary>
        /// <param name="obj">Object to write.</param>
        public override void Write(object obj)
        {
            if (ShouldIgnore(null, string.Empty, DefaultEventType, DefaultId, null, null, null, null))
            {
                return;
            }

            Write(obj.ToString());
        }

        /// <summary>
        /// Writes message of given category.
        /// </summary>
        /// <param name="message">Message to write.</param>
        /// <param name="category"><paramref name="message"/>'s category.</param>
        public override void Write(string message, string category)
        {
            if (ShouldIgnore(null, string.Empty, DefaultEventType, DefaultId, message, null, null, null))
            {
                return;
            }

            if (category == null)
            {
                Write(message);
            }
            else
            {
                Write(category + ": " + (message ?? string.Empty));
            }
        }

        /// <summary>
        /// Writes object of given category.
        /// </summary>
        /// <param name="obj">Object to write.</param>
        /// <param name="category"><paramref name="obj"/>'s category.</param>
        public override void Write(object obj, string category)
        {
            Write(obj?.ToString(), category);
        }

        /// <summary>
        /// Supposed to add indent. Not implemented.
        /// </summary>
        protected override void WriteIndent()
        {

        }

        /// <summary>
        /// Writes object to listener.
        /// </summary>
        /// <param name="obj">Object to write.</param>
        public override void WriteLine(object obj)
        {
            WriteLine(obj.ToString());
        }

        /// <summary>
        /// <summary>
        /// Writes message of given category.
        /// </summary>
        /// <param name="message">Message to write.</param>
        /// <param name="category"><paramref name="message"/>'s category.</param>
        public override void WriteLine(string message, string category)
        {
            if (ShouldIgnore(null, string.Empty, DefaultEventType, DefaultId, message, null, null, null))
            {
                return;
            }

            if (category == null)
            {
                WriteLine(message);
            }
            else
            {
                WriteLine(category + ": " + (message ?? string.Empty));
            }
        }

        /// <summary>
        /// Writes object of given category.
        /// </summary>
        /// <param name="obj">Object to write.</param>
        /// <param name="category"><paramref name="obj"/>'s category.</param>
        public override void WriteLine(object o, string category)
        {
            WriteLine(o?.ToString() ?? string.Empty, category);
        }

        /// <summary>
        /// Traces event.
        /// </summary>
        /// <param name="eventCache">Event cache.</param>
        /// <param name="source">Source of event.</param>
        /// <param name="eventType">Event type.</param>
        /// <param name="id">Event id.</param>
        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id)
        {
            if (ShouldIgnore(eventCache, source, eventType, id, null, null, null, null))
            {
                return;
            }

            TraceData(id, eventType, string.Empty);
        }

        /// <summary>
        /// Traces event.
        /// </summary>
        /// <param name="eventCache">Event cache.</param>
        /// <param name="source">Source of event.</param>
        /// <param name="eventType">Event type.</param>
        /// <param name="id">Event id.</param>
        /// <param name="message">Message to write.</param>
        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
        {
            if (ShouldIgnore(eventCache, source, eventType, id, message, null, null, null))
            {
                return;
            }

            TraceData(id, eventType, CreateMessage(message));
        }

        /// <summary>
        /// Traces event.
        /// </summary>
        /// <param name="eventCache">Event cache.</param>
        /// <param name="source">Source of event.</param>
        /// <param name="eventType">Event type.</param>
        /// <param name="id">Event id.</param>
        /// <param name="format">Message format.</param>
        /// <param name="args">Format arguments.</param>
        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string format, params object[] args)
        {
            if (ShouldIgnore(eventCache, source, eventType, id, format, args, null, null))
            {
                return;
            }

            TraceData(id, eventType, CreateMessage(format, args));
        }

        /// <summary>
        /// Traces transfer.
        /// </summary>
        /// <param name="eventCache">Event cache.</param>
        /// <param name="source">Source of event.</param>
        /// <param name="id">Event id.</param>
        /// <param name="message">Message to write.</param>
        /// <param name="relatedActivityId">Related activity id.</param>
        public override void TraceTransfer(TraceEventCache eventCache, string source, int id, string message, Guid relatedActivityId)
        {
            TraceEvent(eventCache, source, TraceEventType.Transfer, id, message + ", relatedActivityId=" + relatedActivityId.ToString());
        }

        /// <summary>
        /// Determines whether event should be ignored.
        /// </summary>
        /// <param name="cache">Event cache.</param>
        /// <param name="source">Event source.</param>
        /// <param name="eventType">Event type.</param>
        /// <param name="id">Event id.</param>
        /// <param name="formatOrMessage">Format or message.</param>
        /// <param name="args">Format arguments.</param>
        /// <param name="data1">Additional data.</param>
        /// <param name="data">Additional data.</param>
        /// <returns>True if event should be ignored, false otherwise.</returns>
        private bool ShouldIgnore(TraceEventCache cache, string source, TraceEventType eventType, int id, string formatOrMessage, object[] args, object data1, object[] data)
        {
            return Filter != null && !Filter.ShouldTrace(cache, source, eventType, id, formatOrMessage, args, data1, data);
        }

        /// <summary>
        /// Writes trace message to listener.
        /// </summary>
        /// <param name="id">Event id.</param>
        /// <param name="eventType">Event type.</param>
        /// <param name="message">Message to write.</param>
        private void TraceData(int id, TraceEventType eventType, string message)
        {
            LogEntry entry = new LogEntry(id, eventType, message);

            if (WriteTarget == null)
            {
                if (IsQueueEnabled)
                {
                    Logs.Enqueue(entry);
                }
            }
            else
            {
                if (IsQueueEnabled)
                {
                    while (Logs.Count > 0)
                    {
                        WriteTarget(Logs.Dequeue());
                    }
                }

                Logs.Enqueue(entry);
            }
        }

        /// <summary>
        /// Creates message of given format and with provided data.
        /// </summary>
        /// <param name="format">Message format.</param>
        /// <param name="data">Additional data.</param>
        /// <returns>Formatted message.</returns>
        private string CreateMessage(string format = null, object[] data = null)
        {
            StringBuilder sbMessage = new StringBuilder();

            string message;

            if (format != null)
            {
                message = format;

                if (!string.IsNullOrWhiteSpace(message) && data?.Length > 0)
                {
                    sbMessage.AppendFormat(message, data);
                }
                else
                {
                    sbMessage.Append(message);
                }
            }
            else if (data?.Length > 0)
            {
                message = data[0].ToString();

                if (data.Length > 1 && data[0] is string)
                {
                    sbMessage.AppendFormat(message, data.Skip(1).ToArray());
                }
                else
                {
                    sbMessage.Append(message);
                }
            }

            return sbMessage.ToString();
        }
    }
}
