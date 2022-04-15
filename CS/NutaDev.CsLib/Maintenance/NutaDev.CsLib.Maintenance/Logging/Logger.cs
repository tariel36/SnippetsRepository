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
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace NutaDev.CsLib.Maintenance.Logging
{
    /// <summary>
    /// <para>This Log class uses the <see cref="TraceSource"/> and <see cref="TraceListener"/> classes to log provided messages.</para>
    /// <para>This class implements singleton pattern.</para>
    /// </summary>
    public class Logger
    {
        /// <summary>
        /// Null object of <see cref="Logger"/> class.
        /// </summary>
        public static readonly Logger NullObject = new Logger(false);

        /// <summary>
        /// Synchronization object for multithreaded applications.
        /// </summary>
        private static readonly object SyncRoot = new object();

        /// <summary>
        /// Several <see cref="TraceSource"/> are used. This is the name for default one.
        /// </summary>
        public const string DefaultTraceSourceName = "DefaultTraceSource";

        /// <summary>
        /// Default timestamp format for log messages.
        /// </summary>
        private const string DefaultTimestampFormat = "[yyyy-MM-dd HH:mm:ss]";

        /// <summary>
        /// Default TimeSpan format for log messages.
        /// </summary>
        private const string DefaultTimeSpanFormat = "\\[hh\\:mm\\:ss\\.fff\\]";

        /// <summary>
        /// Initializes new instance of the <see cref="Logger"/> class.
        /// </summary>
        public Logger()
            : this(true)
        {

        }

        /// <summary>
        /// Initializes new instance of the <see cref="Logger"/> class. This instance is not initialized.
        /// </summary>
        /// <param name="noInit">This argument indicates whether logger should be initialized</param>
        private Logger(bool init)
        {
            TraceListeners = new Dictionary<string, TraceListener>();
            TraceSources = new Dictionary<string, TraceSource>();

            if (init)
            {
                AddSource(DefaultTraceSourceName, SourceLevels.All);
                DefaultTraceSource = TraceSources[DefaultTraceSourceName];

                AutoFlush = true;
                AppendTimestamp = true;
                AppendHeader = true;

                TimestampFormat = DefaultTimestampFormat;
                TimeSpanFormat = DefaultTimeSpanFormat;

                EventTypeMaxLength = Enum.GetNames(typeof(TraceEventType)).Max(x => x.Length);
                EventIdLength = 5;
            }
        }

        /// <summary>
        /// Gets or sets value that indicates when current application has started.
        /// </summary>
        public DateTime ApplicationStartTime { get; set; }

        /// <summary>
        /// Gets or sets value that indicates whether application run time should be added to every message.
        /// </summary>
        public bool AppendRunTime { get; set; }

        /// <summary>
        /// Gets or sets value that indicates whether log header should be added to every message.
        /// </summary>
        public bool AppendHeader { get; set; }

        /// <summary>
        /// Gets or sets value that indicates whether <see cref="TraceSource"/>s should be flished on every message.
        /// </summary>
        public bool AutoFlush
        {
            get { return Trace.AutoFlush; }
            set { Trace.AutoFlush = value; }
        }

        /// <summary>
        /// Forces filestream to flush data onto disks. Might be expensive!
        /// </summary>
        public bool AutoFlushToDisk { get; set; }

        /// <summary>
        /// Gets or sets value that indicates whether timestamp should be added to every message.
        /// </summary>
        public bool AppendTimestamp { get; set; }

        /// <summary>
        /// Gets or sets timestamp format.
        /// </summary>
        public string TimestampFormat { get; set; }

        /// <summary>
        /// Gets or sets TimeSpan format.
        /// </summary>
        public string TimeSpanFormat { get; set; }

        /// <summary>
        /// Gets or sets maximum length of log `id`. Used for formatting.
        /// </summary>
        public int EventIdLength { get; set; }

        /// <summary>
        /// Gets the default <see cref="TraceSource"/>.
        /// </summary>
        private TraceSource DefaultTraceSource { get; }

        /// <summary>
        /// Dictionary of <see cref="TraceSource"/>. The key is <see cref="TraceSource"/>'s name.
        /// </summary>
        private Dictionary<string, TraceSource> TraceSources { get; }

        /// <summary>
        /// Dictionary of <see cref="TraceListener"/>. The key is <see cref="TraceListener"/>'s name.
        /// </summary>
        private Dictionary<string, TraceListener> TraceListeners { get; }

        /// <summary>
        /// Gets or sets maximum length of <see cref="TraceEventType"/> name. Used for formatting.
        /// </summary>
        private int EventTypeMaxLength { get; }

        /// <summary>
        /// Adds message to log.
        /// </summary>
        /// <param name="msg">The message.</param>
        /// <param name="type">The message log level.</param>
        /// <param name="id">The message id.</param>
        /// <param name="callerPath">Caller path.</param>
        /// <param name="callerMemberName">Caller member name.</param>
        /// <param name="line">Caller line number.</param>
        /// <seealso cref="TraceEventType"/>
        /// <seealso cref="CallerFilePathAttribute"/>
        /// <seealso cref="CallerMemberNameAttribute"/>
        /// <seealso cref="CallerLineNumberAttribute"/>
        public Logger Add(string msg, TraceEventType type = TraceEventType.Information, int id = 0, [CallerFilePath] string callerPath = null, [CallerMemberName] string callerMemberName = null, [CallerLineNumber] int line = 0)
        {
            return Message(type, id, Format(msg, null, null, type, id, callerPath, callerMemberName, line), null);
        }

        /// <summary>
        /// Adds message to log.
        /// </summary>
        /// <param name="msg">The message.</param>
        /// <param name="args">The arguments used by message.</param>
        /// <param name="type">The message log level.</param>
        /// <param name="id">The message id.</param>
        /// <param name="callerPath">Caller path.</param>
        /// <param name="callerMemberName">Caller member name.</param>
        /// <param name="line">Caller line number.</param>
        /// <seealso cref="TraceEventType"/>
        /// <seealso cref="CallerFilePathAttribute"/>
        /// <seealso cref="CallerMemberNameAttribute"/>
        /// <seealso cref="CallerLineNumberAttribute"/>
        public Logger Add(string msg, object[] args = null, TraceEventType type = TraceEventType.Information, int id = 0, [CallerFilePath] string callerPath = null, [CallerMemberName] string callerMemberName = null, [CallerLineNumber] int line = 0)
        {
            return Message(type, id, Format(msg, args, null, type, id, callerPath, callerMemberName, line), args);
        }

        /// <summary>
        /// Adds message to log.
        /// </summary>
        /// <param name="ex">The exception to log.</param>
        /// <param name="type">The message log level.</param>
        /// <param name="id">The message id.</param>
        /// <param name="callerPath">Caller path.</param>
        /// <param name="callerMemberName">Caller member name.</param>
        /// <param name="line">Caller line number.</param>
        /// <seealso cref="TraceEventType"/>
        /// <seealso cref="CallerFilePathAttribute"/>
        /// <seealso cref="CallerMemberNameAttribute"/>
        /// <seealso cref="CallerLineNumberAttribute"/>
        public Logger Add(Exception ex, TraceEventType type = TraceEventType.Error, int id = 0, [CallerFilePath] string callerPath = null, [CallerMemberName] string callerMemberName = null, [CallerLineNumber] int line = 0)
        {
            return Message(type, id, Format(null, null, ex, type, id, callerPath, callerMemberName, line), ex);
        }

        /// <summary>
        /// Adds message to log.
        /// </summary>
        /// <param name="msg">The message.</param>
        /// <param name="ex">The exception to log.</param>
        /// <param name="type">The message log level.</param>
        /// <param name="id">The message id.</param>
        /// <param name="callerPath">Caller path.</param>
        /// <param name="callerMemberName">Caller member name.</param>
        /// <param name="line">Caller line number.</param>
        /// <seealso cref="TraceEventType"/>
        /// <seealso cref="CallerFilePathAttribute"/>
        /// <seealso cref="CallerMemberNameAttribute"/>
        /// <seealso cref="CallerLineNumberAttribute"/>
        public Logger Add(string msg, Exception ex = null, TraceEventType type = TraceEventType.Information, int id = 0, [CallerFilePath] string callerPath = null, [CallerMemberName] string callerMemberName = null, [CallerLineNumber] int line = 0)
        {
            return Message(type, id, Format(msg, null, ex, type, id, callerPath, callerMemberName, line), ex);
        }

        public Logger Add(LogEntry entry, Exception ex = null, [CallerFilePath] string callerPath = null, [CallerMemberName] string callerMemberName = null, [CallerLineNumber] int line = 0)
        {
            return Message(entry.EventType, entry.Id, Format(entry.Message, null, ex, entry.EventType, entry.Id, callerPath, callerMemberName, line), ex);
        }

        public Logger Add(LogEntry entry, [CallerFilePath] string callerPath = null, [CallerMemberName] string callerMemberName = null, [CallerLineNumber] int line = 0, params object[] args)
        {
            return Message(entry.EventType, entry.Id, Format(entry.Message, args, null, entry.EventType, entry.Id, callerPath, callerMemberName, line), args);
        }

        /// <summary>
        /// Formats log message.
        /// </summary>
        /// <param name="msg">The message.</param>
        /// <param name="args">The arguments used by message.</param>
        /// <param name="ex">The exception to log.</param>
        /// <param name="type">The message log level.</param>
        /// <param name="id">The message id.</param>
        /// <param name="callerPath">Caller path.</param>
        /// <param name="callerMemberName">Caller member name.</param>
        /// <param name="line">Caller line number.</param>
        /// <seealso cref="TraceEventType"/>
        /// <seealso cref="CallerFilePathAttribute"/>
        /// <seealso cref="CallerMemberNameAttribute"/>
        /// <seealso cref="CallerLineNumberAttribute"/>
        private string Format(string msg,
            object[] args,
            Exception ex,
            TraceEventType type,
            int id,
            string callerPath,
            string callerMemberName,
            int line)
        {
            StringBuilder sb = new StringBuilder();
            string padding = string.Empty;

            if (AppendTimestamp)
            {
                string dtStr = DateTime.Now.ToString(TimestampFormat) + " ";
                sb.Append(dtStr);
                padding += new string(' ', dtStr.Length);
            }

            if (AppendRunTime)
            {
                string tsStr = (DateTime.Now - ApplicationStartTime).ToString(TimeSpanFormat) + " ";
                sb.Append(tsStr);
                padding += new string(' ', tsStr.Length);
            }

            if (AppendHeader)
            {
                string header = string.Format($"[{{0,{EventTypeMaxLength}}}] [{{1,{EventIdLength}}}] ", type, id);
                padding += new string(' ', header.Length);

                sb.Append($"{header}In {(string.IsNullOrWhiteSpace(callerMemberName) ? "<member> " : callerMemberName)} at {(line > 0 ? line.ToString() : "<line>")} of {(string.IsNullOrWhiteSpace(callerPath) ? "<path>" : callerPath)}");
            }

            bool messageAppended = false;

            if (msg != null)
            {
                if (args?.Length > 0)
                {
                    string oldMsg = msg;

                    try
                    {
                        msg = string.Format(msg, args);
                    }
                    catch
                    {
                        msg = oldMsg;
                    }
                }

                int messageLineNbr = 0;

                foreach (string msgLine in msg.Split(new[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (AppendHeader || messageLineNbr > 0)
                    {
                        sb.AppendLine().Append($"{padding}");
                    }

                    sb.Append(msgLine);

                    messageLineNbr++;
                }

                messageAppended = true;
            }

            if (ex != null)
            {
                int messageLineNbr = 0;

                foreach (string exLine in ex.ToString().Split(new[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (AppendHeader || messageLineNbr > 0 || messageAppended)
                    {
                        sb.AppendLine().Append($"{padding}");
                    }

                    sb.Append(exLine);

                    messageLineNbr++;
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Adds message to the log.
        /// </summary>
        /// <param name="type">The message log level.</param>
        /// <param name="id">The message id.</param>
        /// <param name="msg">The message.</param>
        /// <param name="args">The arguments used by message.</param>
        public Logger Message(TraceEventType type, int id, string msg, params object[] args)
        {
            foreach (KeyValuePair<string, TraceSource> source in TraceSources)
            {
                source.Value.TraceData(type, id, args == null ? new object[] { msg } : (new object[] { msg }.Concat(args).ToArray()));

                if (AutoFlush)
                {
                    source.Value.Flush();

                    foreach (TraceListener listener in source.Value.Listeners)
                    {
                        TryForceFlushListenerStream(listener);
                    }
                }
            }

            return this;
        }

        /// <summary>
        /// Adds <see cref="TraceListener"/> to the log.
        /// </summary>
        /// <param name="listener">Listener which will be added.</param>
        public void AddListener(TraceListener listener)
        {
            if (!string.IsNullOrWhiteSpace(listener.Name) && !TraceListeners.ContainsKey(listener.Name))
            {
                TraceListeners.Add(listener.Name, listener);
                foreach (KeyValuePair<string, TraceSource> kvNameSource in TraceSources)
                {
                    kvNameSource.Value.Listeners.Add(listener);
                }
            }
        }

        /// <summary>
        /// Returns <see cref="IReadOnlyCollection{T}"/> of registered <see cref="TraceListener"/>.
        /// </summary>
        /// <returns><see cref="IReadOnlyCollection{T}"/> of registered <see cref="TraceListener"/>.</returns>
        public IReadOnlyCollection<TraceListener> GetListeners()
        {
            return new ReadOnlyCollection<TraceListener>(TraceSources.SelectMany(x => x.Value.Listeners.OfType<TraceListener>()).Distinct().ToList());
        }

        /// <summary>
        /// Adds new <see cref="TraceSource"/>. Sets name to <paramref name="name"/> and level to <paramref name="level"/>.
        /// All registered (<see cref="AddListener"/>) <see cref="TraceListener"/> are added to the source.
        /// </summary>
        /// <param name="name">Source's name.</param>
        /// <param name="level">Source's level.</param>
        public void AddSource(string name, SourceLevels level)
        {
            if (!TraceSources.ContainsKey(name))
            {
                TraceSource source = new TraceSource(name, level);
                source.Listeners.AddRange(TraceListeners.Values.ToArray());
                TraceSources.Add(name, source);
            }
        }

        /// <summary>
        /// Sets <see cref="SourceLevels"/> to every registered <see cref="TraceSource"/> with <paramref name="sourceName"/>.
        /// </summary>
        /// <param name="sourceName">The name of <see cref="TraceSource"/>.</param>
        /// <param name="level"><see cref="SourceLevels"/> to set.</param>
        public void SetSourceLevel(string sourceName, SourceLevels level)
        {
            if (TraceSources.ContainsKey(sourceName))
            {
                TraceSources[sourceName].Switch.Level = level;
            }
        }

        /// <summary>
        /// Tries to force flush the content of the <paramref name="listener"/> to the file onto hard drive. This operation may be very expensive.
        /// The <see cref="AutoFlush"/> and <see cref="AutoFlushToDisk"/> flags must be set.
        /// </summary>
        /// <param name="listener">The listener which content shall be flushed.</param>
        /// <seealso cref="TraceListener"/>
        private void TryForceFlushListenerStream(TraceListener listener)
        {
            if (!AutoFlush) { return; }

            if (listener is TextWriterTraceListener)
            {
                if (((TextWriterTraceListener)listener).Writer is StreamWriter)
                {
                    if ((((TextWriterTraceListener)listener).Writer as StreamWriter)?.BaseStream is FileStream fstream)
                    {
                        if (AutoFlushToDisk)
                        {
                            fstream.Flush(AutoFlushToDisk);
                        }
                    }
                    else
                    {
                        ((((TextWriterTraceListener)listener).Writer as StreamWriter)?.BaseStream)?.Flush();
                    }
                }
                else
                {
                    ((TextWriterTraceListener)listener).Writer.Flush();
                }
            }
        }
    }
}