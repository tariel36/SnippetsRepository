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
using System.IO;
using System.Linq;

namespace NutaDev.CsLib.Internal.ConsoleTools.Files
{
    /// <summary>
    /// Crawls directories based on provided root and performs actions on file system entries found by provided pattern.
    /// </summary>
    public class DirectoryCrawler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DirectoryCrawler"/> class.
        /// </summary>
        /// <param name="root">Root of crawling.</param>
        /// <param name="callback">Action to perform on file systerm entries.</param>
        /// <param name="searchPattern">Search pattern.</param>
        public DirectoryCrawler(string root, Action<string> callback, string searchPattern = "*.*")
        {
            Root = root;
            Callback = callback;
            SearchPattern = searchPattern;
        }

        /// <summary>
        /// Gets search pattern.
        /// </summary>
        public string SearchPattern { get; }

        /// <summary>
        /// Gets crawling root path.
        /// </summary>
        public string Root { get; }

        /// <summary>
        /// Gets action to perform on found file systerm entries.
        /// </summary>
        public Action<string> Callback { get; }

        /// <summary>
        /// Starts the crawling.
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
        /// Crawling handler.
        /// </summary>
        /// <param name="directory">Current directory.</param>
        /// <param name="depth">Tree depth.</param>
        private void Crawl(string directory, int depth)
        {
            if (string.IsNullOrWhiteSpace(directory) || !System.IO.Directory.Exists(directory))
            {
                return;
            }

            List<string> entries = new List<string>();

            try
            {
                entries = Directory.EnumerateFileSystemEntries(directory, SearchPattern, SearchOption.AllDirectories).ToList();
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine(ex);
            }

            foreach (string path in entries)
            {
                if (string.IsNullOrWhiteSpace(path))
                {
                    continue;
                }

                FileInfo fileInfo = new FileInfo(path);

                bool isDirectory = fileInfo.Attributes.HasFlag(FileAttributes.Directory);

                if (isDirectory)
                {
                    Crawl(path, depth + 1);
                }
                else
                {
                    if (File.Exists(path))
                    {
                        Callback(path);
                    }
                }
            }
        }
    }
}
