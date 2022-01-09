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
using NutaDev.CsLib.Maintenance.Exceptions.Factories;
using NutaDev.CsLib.Resources.Text.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace NutaDev.CsLib.Internal.ConsoleTools.Tools
{
    /// <summary>
    /// Class that parses csproj files and appends various information about assemblies, adds links to shared assemblies and similar for .netframework projects.
    /// </summary>
    public class AssemblyInfoLinkerFramework
    {
        /// <summary>
        /// Contains header of .netframework project.
        /// </summary>
        private const string FrameworkProjectHeader = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";

        /// <summary>
        /// Assembly info full link.
        /// </summary>
        private const string AssemblyInfoLink = "\r\n\r\n  <ItemGroup>\r\n    <Compile Include=\"Properties\\AssemblyInfo.cs\" />\r\n  </ItemGroup>\r\n\r\n";

        /// <summary>
        /// Shared assembly info full link.
        /// </summary>
        private const string SharedAssemblyInfoLink = "\r\n\r\n  <ItemGroup>\r\n    <Compile Include=\"{0}\\Internal\\NutaDev.CsLib.Internal.Shared\\App\\SharedAssemblyInfo.cs\">\r\n      <Link>Properties\\SharedAssemblyInfo.cs</Link>\r\n    </Compile>\r\n  </ItemGroup>\r\n\r\n";

        /// <summary>
        /// Initializes a new instance of the <see cref="AssemblyInfoLinkerFramework"/> class.
        /// </summary>
        /// <param name="rootPath">Root path of solution.</param>
        /// <param name="author">Author of the project.</param>
        public AssemblyInfoLinkerFramework(string rootPath, string author)
        {
            RootPath = rootPath;
            Author = author;
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
        /// Gets or sets assembly info text.
        /// </summary>
        private string AssemblyInfoText { get; set; }

        /// <summary>
        /// Executes the script.
        /// </summary>
        public void Execute()
        {
            AssemblyInfoText = Resources.Resources.FrameworkAssemblyInfo;

            DirectoryCrawler crawler = new DirectoryCrawler(RootPath, OnFileAction, "*.csproj");
            crawler.Start();
        }

        /// <summary>
        /// Handles actions when certain file is found.
        /// </summary>
        /// <param name="fullFilePath">Absolute path to file.</param>
        private void OnFileAction(string fullFilePath)
        {
            string fileText = File.ReadAllText(fullFilePath);

            if (!fileText.StartsWith(FrameworkProjectHeader))
            {
                return;
            }

            string fileName = Path.GetFileNameWithoutExtension(fullFilePath);
            string fileDirectory = Path.GetDirectoryName(fullFilePath);
            string propertiesDirectory = Path.Combine(fileDirectory, "Properties");
            string assemblyInfoFilePath = Path.Combine(propertiesDirectory, "AssemblyInfo.cs");

            if (!Directory.Exists(propertiesDirectory))
            {
                Directory.CreateDirectory(propertiesDirectory);
            }

            bool shouldLinkAssemblyInfo = !File.Exists(assemblyInfoFilePath);

            if (!string.Equals(File.ReadAllText(assemblyInfoFilePath), string.Format(AssemblyInfoText, fileName)))
            {
                File.WriteAllText(assemblyInfoFilePath, string.Format(AssemblyInfoText, fileName));
            }

            if (shouldLinkAssemblyInfo || !fileText.Contains("Include=\"Properties\\AssemblyInfo.cs\""))
            {
                int idx = fileText.LastIndexOf("</PropertyGroup>");

                if (idx < 0)
                {
                    throw ExceptionFactory.InvalidOperationException(Text.NoPropertyGroupFoundInFile_0_, fullFilePath);
                }

                idx += "</PropertyGroup>".Length;

                fileText = fileText.Insert(idx, AssemblyInfoLink);

                File.WriteAllText(fullFilePath, fileText);
            }

            if (!fileText.Contains("SharedAssemblyInfo.cs"))
            {
                int depth = 0;

                while (!string.Equals(RootPath, fileDirectory))
                {
                    depth++;
                    fileDirectory = Path.GetDirectoryName(fileDirectory);
                }

                string prefix = string.Join("\\", Enumerable.Range(0, depth).Select(x => ".."));
                int idx = fileText.LastIndexOf("</PropertyGroup>");

                if (idx < 0)
                {
                    throw ExceptionFactory.InvalidOperationException(Text.NoPropertyGroupFoundInFile_0_, fullFilePath);
                }

                idx += "</PropertyGroup>".Length;

                fileText = fileText.Insert(idx, string.Format(SharedAssemblyInfoLink, prefix));

                File.WriteAllText(fullFilePath, fileText);
            }

            if (fileText.Contains("<Deterministic>true</Deterministic>"))
            {
                fileText = fileText.Replace("<Deterministic>true</Deterministic>", "<Deterministic>false</Deterministic>");
                File.WriteAllText(fullFilePath, fileText);
            }
            else if (!fileText.Contains("<Deterministic>false</Deterministic>"))
            {
                int idx = fileText.IndexOf("</PropertyGroup>");

                if (idx < 0)
                {
                    throw ExceptionFactory.InvalidOperationException(Text.NoPropertyGroupFoundInFile_0_, fullFilePath);
                }

                fileText = fileText.Insert(idx, "  <Deterministic>false</Deterministic>\r\n  ");
                File.WriteAllText(fullFilePath, fileText);
            }
        }
    }
}
