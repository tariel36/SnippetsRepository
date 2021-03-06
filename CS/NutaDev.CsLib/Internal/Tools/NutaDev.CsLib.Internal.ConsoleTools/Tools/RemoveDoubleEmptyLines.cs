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
using System.IO;
using System.Text.RegularExpressions;

namespace NutaDev.CsLib.Internal.ConsoleTools.Tools
{
    /// <summary>
    /// Removes double empty lines.
    /// </summary>
    public class RemoveDoubleEmptyLines
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveDoubleEmptyLines"/> class.
        /// </summary>
        /// <param name="rootPath">Root path of solution.</param>
        public RemoveDoubleEmptyLines(string rootPath)
        {
            RootPath = rootPath;
            DoubleLineRegex = new Regex("^\r?\n\r?\n", RegexOptions.Compiled | RegexOptions.Multiline);
        }

        /// <summary>
        /// Gets root path of the project.
        /// </summary>
        public string RootPath { get; }

        /// <summary>
        /// Gets double line regex.
        /// </summary>
        private Regex DoubleLineRegex { get; }

        /// <summary>
        /// Executes the script.
        /// </summary>
        public void Execute()
        {
            DirectoryCrawler crawler = new DirectoryCrawler(RootPath, OnFileAction, "*.cs");
            crawler.Start();
        }

        /// <summary>
        /// Handles actions when certain file is found.
        /// </summary>
        /// <param name="fullFilePath">Absolute path to file.</param>
        private void OnFileAction(string fullFilePath)
        {
            string text = File.ReadAllText(fullFilePath);

            if (!DoubleLineRegex.IsMatch(text))
            {
                return;
            }

            text = DoubleLineRegex.Replace(text, "\r\n");

            File.WriteAllText(fullFilePath, text);
        }
    }
}
