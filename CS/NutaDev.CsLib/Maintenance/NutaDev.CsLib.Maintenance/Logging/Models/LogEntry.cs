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
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace NutaDev.CsLib.Maintenance.Logging.Models
{
    /// <summary>
    /// Log entry class.
    /// </summary>
    public class LogEntry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogEntry"/> class.
        /// </summary>
        /// <param name="id">Log id.</param>
        /// <param name="eventType">Event type.</param>
        /// <param name="msg">Log message.</param>
        public LogEntry(int id, TraceEventType eventType, string msg)
        {
            Id = id;
            EventType = eventType;
            Message = msg;

            CreationDate = DateTime.Now;
            CreationDateUtc = CreationDate.ToUniversalTime();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogEntry"/> class.
        /// </summary>
        /// <param name="template">Entry template.</param>
        /// <param name="args">Entry message arguments.</param>
        /// <param name="ex">An exception object.</param>
        /// <param name="callerPath">Caller file path.</param>
        /// <param name="callerMemberName">Caller member name.</param>
        /// <param name="line">Caller line.</param>
        internal LogEntry(LogEntry template,
            object[] args,
            Exception ex,
            [CallerFilePath] string callerPath = null,
            [CallerMemberName] string callerMemberName = null,
            [CallerLineNumber] int line = 0)
        {
            Id = template.Id;
            EventType = template.EventType;
            Message = template.Message;
            Arguments = args?.ToArray();
            Exception = ex;
            CallerPath = callerPath;
            CallerMemberName = callerMemberName;
            CallerLineNumber = line;

            CreationDate = DateTime.Now;
            CreationDateUtc = CreationDate.ToUniversalTime();
        }

        /// <summary>
        /// Gets log id.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Gets event type.
        /// </summary>
        public TraceEventType EventType { get; }

        /// <summary>
        /// Gets log message.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Gets log arguments.
        /// </summary>
        public object[] Arguments { get; }

        /// <summary>
        /// Gets exception object.
        /// </summary>
        public Exception Exception { get; }

        /// <summary>
        /// Gets caller file path.
        /// </summary>
        public string CallerPath { get; }

        /// <summary>
        /// Gets caller member name.
        /// </summary>
        public string CallerMemberName { get; }

        /// <summary>
        /// Gets caller line number.
        /// </summary>
        public int CallerLineNumber { get; }

        /// <summary>
        /// Gets object creation time.
        /// </summary>
        public DateTime CreationDate { get; }

        /// <summary>
        /// Gets object creation time in UTC.
        /// </summary>
        public DateTime CreationDateUtc { get; }

        /// <summary>
        /// Creates new instance of the <see cref="LogEntry"/> class with provided message <paramref name="args"/>.
        /// </summary>
        /// <param name="args">Message arguments.</param>
        /// <returns>New instnace of the <see cref="LogEntry"/>.</returns>
        public LogEntry SetArguments(params object[] args)
        {
            return new LogEntry(this, args, null);
        }
    }
}
