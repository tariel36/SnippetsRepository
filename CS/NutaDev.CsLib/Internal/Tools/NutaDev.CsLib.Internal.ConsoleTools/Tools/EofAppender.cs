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

namespace NutaDev.CsLib.Internal.ConsoleTools.Tools
{
    /// <summary>
    /// Provides means to append new line at the end of cs file.
    /// </summary>
    public class EofAppender
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EofAppender"/> class.
        /// </summary>
        /// <param name="rootPath">Root path of solution.</param>
        public EofAppender(string rootPath)
        {
            RootPath = rootPath;
        }

        /// <summary>
        /// Gets root path of the project.
        /// </summary>
        public string RootPath { get; }

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
        private void OnFileAction(string filePath)
        {
            string fileText = System.IO.File.ReadAllText(filePath).Trim();

            fileText += "\r\n";

            System.IO.File.WriteAllText(filePath, fileText);
        }
    }
}
