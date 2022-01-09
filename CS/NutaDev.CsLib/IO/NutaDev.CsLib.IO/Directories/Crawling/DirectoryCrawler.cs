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

using NutaDev.CsLib.Maintenance.Exceptions.Delegates;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace NutaDev.CsLib.IO.Directories.Crawling
{
    /// <summary>
    /// Directory crawler that searches through directories.
    /// </summary>
    public class DirectoryCrawler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DirectoryCrawler"/> class.
        /// </summary>
        /// <param name="root">Root directory.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <param name="exceptionHandler">Exception handler.</param>
        public DirectoryCrawler(string root, CancellationToken cancellationToken, ExceptionHandlerDelegate exceptionHandler)
        {
            Callbacks = new HashSet<DirectoryCrawlerCallback>();

            Root = root;
            CancellationToken = cancellationToken;
            ExceptionHandler = exceptionHandler;
        }

        /// <summary>
        /// Gets root directory path.
        /// </summary>
        public string Root { get; }

        /// <summary>
        /// Gets cancellation token.
        /// </summary>
        private CancellationToken CancellationToken { get; }

        /// <summary>
        /// Collection of invoked callbacks.
        /// </summary>
        private HashSet<DirectoryCrawlerCallback> Callbacks { get; }

        /// <summary>
        /// Exception handler.
        /// </summary>
        private ExceptionHandlerDelegate ExceptionHandler { get; }

        /// <summary>
        /// Adds callback.
        /// </summary>
        /// <param name="callback">Callback to be added.</param>
        public void AddCallback(DirectoryCrawlerCallback callback)
        {
            Callbacks.Add(callback);
        }

        /// <summary>
        /// Starts crawling procedure.
        /// </summary>
        public void Start()
        {
            if (string.IsNullOrWhiteSpace(Root))
            {
                return;
            }

            Crawl(Root, 0);
        }

        /// <summary>
        /// Crawls through directory.
        /// </summary>
        /// <param name="directory">Directory path.</param>
        /// <param name="depth">Current depth.</param>
        private void Crawl(string directory, int depth)
        {
            if (CancellationToken.IsCancellationRequested || string.IsNullOrWhiteSpace(directory) || !System.IO.Directory.Exists(directory))
            {
                return;
            }

            List<string> entries = new List<string>();

            try
            {
                entries = System.IO.Directory.EnumerateFileSystemEntries(directory).ToList();
            }
            catch (UnauthorizedAccessException ex)
            {
                ExceptionHandler?.Invoke(ex);
            }

            foreach (string path in entries)
            {
                if (CancellationToken.IsCancellationRequested)
                {
                    return;
                }

                if (string.IsNullOrWhiteSpace(path))
                {
                    continue;
                }

                FileInfo fileInfo = new FileInfo(path);

                bool isDirectory = fileInfo.Attributes.HasFlag(FileAttributes.Directory);

                FileDetails fsInfo = new FileDetails(fileInfo, isDirectory ? FileTypes.Directory : FileTypes.File, depth);

                if (isDirectory)
                {
                    RunCallbacks(fsInfo, FileTypes.Directory, CallbackCallTypes.Before);

                    Crawl(path, depth + 1);

                    RunCallbacks(fsInfo, FileTypes.Directory, CallbackCallTypes.After);
                }
                else
                {
                    if (File.Exists(path))
                    {
                        RunCallbacks(fsInfo, FileTypes.File, CallbackCallTypes.Parallel);
                    }
                }
            }
        }

        /// <summary>
        /// Runs callbacks.
        /// </summary>
        /// <param name="fsInfo">File details.</param>
        /// <param name="entryType">Entry type.</param>
        /// <param name="callType">Callback type.</param>
        private void RunCallbacks(FileDetails fsInfo, FileTypes entryType, CallbackCallTypes callType)
        {
            foreach (DirectoryCrawlerCallback callback in Callbacks.Where(x => (x.FileType == entryType || x.FileType == FileTypes.All) && x.CallType == callType))
            {
                if (entryType == FileTypes.Directory && !System.IO.Directory.Exists(fsInfo.FileInfo.FullName))
                {
                    break;
                }

                callback.Callback(fsInfo);
            }
        }
    }
}
