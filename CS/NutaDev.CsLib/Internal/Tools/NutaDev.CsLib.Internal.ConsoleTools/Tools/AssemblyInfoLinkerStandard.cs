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
using System.IO;
using System.Linq;

namespace NutaDev.CsLib.Internal.ConsoleTools.Tools
{
    /// <summary>
    /// Class that parses csproj files and appends various information about assemblies, adds links to shared assemblies and similar for .netstandard projects.
    /// </summary>
    public class AssemblyInfoLinkerStandard
    {
        /// <summary>
        /// Contains string that provides target framework of standard 2.0.
        /// </summary>
        private const string TargetFramework20 = "<TargetFramework>netstandard2.0</TargetFramework>";

        /// <summary>
        /// Contains string that provides target framework of core 2.1.
        /// </summary>
        private const string TargetCore21 = "<TargetFramework>netcoreapp2.1</TargetFramework>";

        /// <summary>
        /// Contains string that provides generate assembly info flag.
        /// </summary>
        private const string GenerateAssembly = "<GenerateAssemblyInfo>false</GenerateAssemblyInfo>";

        /// <summary>
        /// Contains string that provides deterministic flag.
        /// </summary>
        private const string Deterministic = "<Deterministic>false</Deterministic>";

        /// <summary>
        /// Contains string that provides SharedAssemblyInfo link text.
        /// </summary>
        private const string SharedAssemblyInfo = "Link=\"App\\SharedAssemblyInfo.cs\"";

        /// <summary>
        /// Contains string that provides PropertyGroup closing tag.
        /// </summary>
        private const string ClosedPropertyGroup = "</PropertyGroup>";

        /// <summary>
        /// Contains string that provides SharedAssemblyInfo linking tags.
        /// </summary>
        private const string SharedAssemblyInfoLink = "  <ItemGroup>\r\n    <Compile Include=\"{0}\\Internal\\NutaDev.CsLib.Internal.Shared\\App\\SharedAssemblyInfo.cs\" Link=\"App\\SharedAssemblyInfo.cs\" />\r\n  </ItemGroup>";

        /// <summary>
        /// Contains string that provides folder inclusing for App directory.
        /// </summary>
        private const string AppDirectoryInfo = "<Folder Include=\"App\\\" />";

        /// <summary>
        /// Contains string that provides folder inclusion for App directory tags.
        /// </summary>
        private const string AppDirectoryLink = "\r\n  <ItemGroup>\r\n    <Folder Include=\"App\\\" />\r\n  </ItemGroup>";

        /// <summary>
        /// Contains string that provides ItemGroup closing tag.
        /// </summary>
        private const string ItemGroupInfo = "</ItemGroup>";

        /// <summary>
        /// Contains string that provides Authors opening tag.
        /// </summary>
        private const string AuthorsInfo = "<Authors>";

        /// <summary>
        /// Contains string that provides Authors tags template.
        /// </summary>
        private const string Authors = "    <Authors>{0}</Authors>";

        /// <summary>
        /// Contains string that provides AssemblyTitle for AssemblyInfo.
        /// </summary>
        private const string AssemblyInfoContent = "using System.Reflection;\r\n\r\n\r\n[assembly: AssemblyTitle(\"{0}\")]";

        /// <summary>
        /// Contains header of .netstandard project.
        /// </summary>
        private const string StandardProjectHeader = "<Project Sdk=\"Microsoft.NET.Sdk\">";

        /// <summary>
        /// Initializes a new instance of the <see cref="AssemblyInfoLinkerStandard"/> class.
        /// </summary>
        /// <param name="rootPath">Root path of solution.</param>
        /// <param name="author">Author of the project.</param>
        public AssemblyInfoLinkerStandard(string rootPath, string author)
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
        /// Executes the script.
        /// </summary>
        public void Execute()
        {
            DirectoryCrawler crawler = new DirectoryCrawler(RootPath, OnFileAction, "*.csproj");
            crawler.Start();
        }

        /// <summary>
        /// Handles actions when certain file is found.
        /// </summary>
        /// <param name="fullFilePath">Absolute path to file.</param>
        private void OnFileAction(string fullFilePath)
        {
            string fileText = System.IO.File.ReadAllText(fullFilePath);

            if (!fileText.StartsWith(StandardProjectHeader))
            {
                return;
            }

            int idx = fileText.IndexOf(TargetFramework20);

            if (idx < 0)
            {
                idx = fileText.IndexOf(TargetCore21);

                if (idx < 0)
                {
                    throw ExceptionFactory.InvalidOperationException(Text.NoTargetFrameworkForFile, fullFilePath);
                }

                idx += TargetCore21.Length;
            }
            else
            {
                idx += TargetFramework20.Length;
            }

            if (!fileText.Contains(GenerateAssembly))
            {
                fileText = fileText.Insert(idx, $"\r\n    {GenerateAssembly}");
            }

            idx = fileText.IndexOf(GenerateAssembly) + GenerateAssembly.Length;

            if (!fileText.Contains(Deterministic))
            {
                fileText = fileText.Insert(idx, $"\r\n    {Deterministic}");
            }

            if (!fileText.Contains(SharedAssemblyInfo))
            {
                idx = fileText.IndexOf(ClosedPropertyGroup) + ClosedPropertyGroup.Length;

                int depth = 0;

                string fileDirectory = System.IO.Path.GetDirectoryName(fullFilePath);

                while (!string.Equals(RootPath, fileDirectory))
                {
                    depth++;
                    fileDirectory = System.IO.Path.GetDirectoryName(fileDirectory);
                }

                string prefix = string.Join("\\", Enumerable.Range(0, depth).Select(x => ".."));

                fileText = fileText.Insert(idx, $"\r\n\r\n{string.Format(SharedAssemblyInfoLink, prefix)}");
            }

            if (!fileText.Contains(AppDirectoryInfo))
            {
                idx = fileText.IndexOf(ItemGroupInfo) + ItemGroupInfo.Length;
                fileText = fileText.Insert(idx, $"\r\n{AppDirectoryLink}");

                string fileDirectory = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(fullFilePath), "App");

                if (!System.IO.Directory.Exists(fileDirectory))
                {
                    System.IO.Directory.CreateDirectory(fileDirectory);
                }
            }

            if (!fileText.Contains(AuthorsInfo))
            {
                idx = fileText.IndexOf(Deterministic) + Deterministic.Length;
                fileText = fileText.Insert(idx, $"\r\n{string.Format(Authors, Author)}");
            }

            string assemblyInfoPath = Path.Combine(Path.GetDirectoryName(fullFilePath), "App", "AssemblyInfo.cs");

            if (!File.Exists(assemblyInfoPath))
            {
                string fileName = Path.GetFileNameWithoutExtension(fullFilePath);
                System.IO.File.WriteAllText(assemblyInfoPath, string.Format(AssemblyInfoContent, fileName));
            }

            System.IO.File.WriteAllText(fullFilePath, fileText);
        }
    }
}
