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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace NutaDev.CsLib.Internal.ConsoleTools.Tools
{
    /// <summary>
    /// Performs various actions related to license texts within files.
    /// </summary>
    public class Licensor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Licensor"/> class.
        /// </summary>
        /// <param name="rootPath">Root path of solution.</param>
        /// <param name="author">Author of the project.</param>
        public Licensor(string rootPath, string author)
        {
            RootPath = rootPath;
            Author = author;

            CopyrightRegex = new Regex("\\/\\/ Copyright (c) [0-9]{4} .*\r?\n");

            LicenseTextByExtension = new Dictionary<string, LicenseInfo>(StringComparer.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Gets root path of the project.
        /// </summary>
        public string RootPath { get; }

        /// <summary>
        /// Gets author of the project.
        /// </summary>
        public string Author { get; }

        /// <summary>
        /// Gets copyright regex instance.
        /// </summary>
        private Regex CopyrightRegex { get; }

        /// <summary>
        /// Gets collection of license texts.
        /// </summary>
        private Dictionary<string, LicenseInfo> LicenseTextByExtension { get; }

        /// <summary>
        /// Executes the script.
        /// </summary>
        public void Execute()
        {
            LicenseProvider provider = new LicenseProvider();

            CreateInfo(provider, OnXamlFileAction, "xaml");
            CreateInfo(provider, OnCsFileAction, "cs");

            foreach (LicenseInfo info in LicenseTextByExtension.Values)
            {
                DirectoryCrawler crawler = new DirectoryCrawler(RootPath, info.FileAction, info.Filter);
                crawler.Start();
            }
        }

        /// <summary>
        /// Creates <see cref="LicenseInfo"/> structure.
        /// </summary>
        /// <param name="provider">License provider.</param>
        /// <param name="action">Action to perform.</param>
        /// <param name="extension">File extension.</param>
        private void CreateInfo(LicenseProvider provider, Action<string> action, string extension)
        {
            LicenseTextByExtension[extension] = new LicenseInfo()
            {
                Text = provider.GetLicenseText(Author, extension),
                CopyrightText = provider.GetCopyright(Author, extension),
                FirstLine = provider.GetLicenseText(0, Author, extension),
                FileAction = action,
                Filter = $"*.{extension}"
            };
        }

        /// <summary>
        /// Handles actions when certain XAML file is found.
        /// </summary>
        /// <param name="filePath">Absolute path to file.</param>
        private void OnXamlFileAction(string filePath)
        {
            OnFileAction(LicenseTextByExtension["xaml"], filePath);
        }

        /// <summary>
        /// Handles actions when certain CS file is found.
        /// </summary>
        /// <param name="filePath">Absolute path to file.</param>
        private void OnCsFileAction(string filePath)
        {
            if (filePath.EndsWith(".Designer.cs") || filePath.Contains("TemporaryGeneratedFile") || filePath.Contains("obj\\Debug"))
            {
                return;
            }

            OnFileAction(LicenseTextByExtension["cs"], filePath);
        }

        /// <summary>
        /// Handles actions when certain file is found.
        /// </summary>
        /// <param name="info">License info structure.</param>
        /// <param name="filePath">Absolute path to file.</param>
        private void OnFileAction(LicenseInfo info, string filePath)
        {
            string fileText = File.ReadAllText(filePath);
            string firstLine = fileText.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries).First();

            if (string.Equals(firstLine, info.FirstLine))
            {
                if (!fileText.Contains(info.CopyrightText))
                {
                    fileText = CopyrightRegex.Replace(fileText, info.CopyrightText);

                    File.WriteAllText(filePath, fileText);
                }
            }
            else if (firstLine.StartsWith("//"))
            {
                Console.WriteLine($"File `{filePath}` has different license.");
            }
            else
            {
                fileText = info.Text + fileText;
                File.WriteAllText(filePath, fileText);
            }
        }

        /// <summary>
        /// Structure that holds license information.
        /// </summary>
        private class LicenseInfo
        {
            /// <summary>
            /// License text.
            /// </summary>
            public string Text;

            /// <summary>
            /// First line of license text.
            /// </summary>
            public string FirstLine;

            /// <summary>
            /// Line with copyright info.
            /// </summary>
            public string CopyrightText;

            /// <summary>
            /// File extension filter.
            /// </summary>
            public string Filter;

            /// <summary>
            /// File action.
            /// </summary>
            public Action<string> FileAction;
        }
    }
}
