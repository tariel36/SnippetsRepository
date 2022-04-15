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

using NutaDev.CsLib.Internal.ConsoleTools.Files;
using System;
using System.IO;
using System.Linq;

namespace NutaDev.CsLib.Internal.ConsoleTools.Tools
{
    /// <summary>
    /// Provides functionality to find meaningful comments within code.
    /// </summary>
    public class CommentsFinder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommentsFinder"/> class.
        /// </summary>
        /// <param name="rootPath">Root path of solution.</param>
        public CommentsFinder(string rootPath)
        {
            RootPath = rootPath;

            IgnoredExtensions = new[]
            {
                ".Designer.cs",
                ".g.cs",
                ".g.i.cs"
            };

            IgnoredPaths = new[]
            {
                @"\obj\Debug\",
                @"\obj\Release\",
                @"\bin\Debug\",
                @"\bin\Release\",
                @"NutaDev.CsLib.Internal.Tests"
            };
        }

        /// <summary>
        /// Gets root path of the project.
        /// </summary>
        public string RootPath { get; }

        /// <summary>
        /// Gets ignored extensions.
        /// </summary>
        private string[] IgnoredExtensions { get; }

        /// <summary>
        /// Gets ignored paths.
        /// </summary>
        private string[] IgnoredPaths { get; }

        /// <summary>
        /// Executes the script.
        /// </summary>
        public void Execute()
        {
            FileTypesToCheck[] toCheck = new[]
            {
                new FileTypesToCheck() { Filter = "*.cs", OnFileAction = OnCsFileAction },
                new FileTypesToCheck() { Filter = "*.xaml", OnFileAction = OnXamlFileAction },
                new FileTypesToCheck() { Filter = "*.cpp", OnFileAction = OnCppFileAction },
                new FileTypesToCheck() { Filter = "*.hpp", OnFileAction = OnHppFileAction },
            };

            foreach (FileTypesToCheck fileType in toCheck)
            {
                DirectoryCrawler crawler = new DirectoryCrawler(RootPath, fileType.OnFileAction, fileType.Filter);
                crawler.Start();
            }

            Console.ReadKey();
        }

        /// <summary>
        /// Find comments in CS files.
        /// </summary>
        /// <param name="filePath">Absolute path.</param>
        private void OnCppFileAction(string filePath)
        {
            OnCLikeFileAction(filePath);
        }

        /// <summary>
        /// Find comments in CS files.
        /// </summary>
        /// <param name="filePath">Absolute path.</param>
        private void OnHppFileAction(string filePath)
        {
            OnCLikeFileAction(filePath);
        }

        /// <summary>
        /// Find comments in CS files.
        /// </summary>
        /// <param name="filePath">Absolute path.</param>
        private void OnCsFileAction(string filePath)
        {
            OnCLikeFileAction(filePath);
        }

        /// <summary>
        /// Find comments in CS files.
        /// </summary>
        /// <param name="filePath">Absolute path.</param>
        private void OnCLikeFileAction(string filePath)
        {
            if (IgnoredExtensions.Any(x => filePath.EndsWith(x)))
            {
                return;
            }

            if (IgnoredPaths.Any(x => filePath.Contains(x)))
            {
                return;
            }

            LicenseProvider provider = new LicenseProvider();

            string[] comments = new[] { "//", "/*" };

            string[] fileLines = File.ReadAllLines(filePath);

            for (int idx = 0; idx < fileLines.Length; ++idx)
            {
                string line = fileLines[idx];

                if (line.Trim().StartsWith("/// <"))
                {
                    if (line.Trim().Contains("</"))
                    {
                        continue;
                    }
                    else
                    {
                        while (idx < fileLines.Length && (!(line = fileLines[idx]).Trim().StartsWith("/// </")))
                        {
                            ++idx;
                        }

                        continue;
                    }
                }

                string licenseFirst = provider.GetLicenseTextLine(0);
                string licenseLast = provider.GetLicenseTextLine(provider.GetLicenseLineCount() - 1);

                if (line.Contains(licenseFirst))
                {
                    while (idx < fileLines.Length && (!(line = fileLines[idx]).Contains(licenseLast)))
                    {
                        ++idx;
                    }

                    continue;
                }

                if (comments.Any(x => fileLines[idx].Contains(x)))
                {
                    Console.WriteLine(string.Format($"Line {idx,4} of file `{filePath}`"));
                }
            }
        }

        /// <summary>
        /// Find comments in XAML files.
        /// </summary>
        /// <param name="filePath">Absolute path.</param>
        private void OnXamlFileAction(string filePath)
        {
            string[] fileLines = File.ReadAllLines(filePath);

            int idx = 0;

            while (fileLines[idx].StartsWith("<!--"))
            {
                idx++;
            }

            for (; idx < fileLines.Length; ++idx)
            {
                if (fileLines[idx].Contains("<!--"))
                {
                    Console.WriteLine(string.Format($"Line {idx,4} of file `{filePath}`"));
                }
            }
        }

        /// <summary>
        /// Contains data about file type to process.
        /// </summary>
        private class FileTypesToCheck
        {
            /// <summary>
            /// Action to perform on file.
            /// </summary>
            public Action<string> OnFileAction;

            /// <summary>
            /// File type filter.
            /// </summary>
            public string Filter;
        }
    }
}
