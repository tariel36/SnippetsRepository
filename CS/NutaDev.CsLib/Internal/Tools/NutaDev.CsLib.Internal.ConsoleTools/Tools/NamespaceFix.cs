using NutaDev.CsLib.Internal.ConsoleTools.Files;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace NutaDev.CsLib.Internal.ConsoleTools.Tools
{
    /// <summary>
    /// Fixes namespaces within files.
    /// </summary>
    public class NamespaceFix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NamespaceFix"/> class.
        /// </summary>
        /// <param name="rootPath">Root path of solution.</param>
        public NamespaceFix(string rootPath)
        {
            RootPath = rootPath;

            WhitespacePrefixRegex = new Regex("^\\s+", RegexOptions.Compiled | RegexOptions.Multiline);
            NamespaceRegex = new Regex("(^\\s+)?namespace", RegexOptions.Compiled | RegexOptions.Multiline);

            LicenseProvider = new LicenseProvider();
        }

        /// <summary>
        /// Gets root path of the project.
        /// </summary>
        public string RootPath { get; }

        /// <summary>
        /// Gets whitespace prefix regex.
        /// </summary>
        private Regex WhitespacePrefixRegex { get; }

        /// <summary>
        /// Gets namespace regex.
        /// </summary>
        private Regex NamespaceRegex { get; }

        /// <summary>
        /// Gets license provider.
        /// </summary>
        private LicenseProvider LicenseProvider { get; }

        /// <summary>
        /// Executes the script.
        /// </summary>
        public void Execute()
        {
            string[] extensions = new[]
            {
                "cpp", "hpp"
            };

            foreach (string ext in extensions)
            {
                DirectoryCrawler crawler = new DirectoryCrawler(RootPath, OnFileAction, $"*.{ext}");
                crawler.Start();
            }
        }

        /// Handles actions when certain file is found.
        /// </summary>
        /// <param name="filePath">Absolute path to file.</param>
        private void OnFileAction(string filePath)
        {
            string fileText = File.ReadAllText(filePath);
            List<string> lines = fileText.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None).ToList();

            bool foundFirstNamespace = false;

            List<string> namespaces = new List<string>();

            string firstCodeLine = string.Empty;

            for (int i = 0; i < lines.Count; ++i)
            {
                string line = lines[i];

                if (NamespaceRegex.IsMatch(line))
                {
                    namespaces.Add(line.Replace("namespace ", string.Empty).Trim());

                    foundFirstNamespace = true;

                    ++i;
                }
                else if (foundFirstNamespace)
                {
                    firstCodeLine = line;
                    break;
                }
            }

            string directory = filePath;

            do
            {
                directory = Path.GetDirectoryName(directory);
            }
            while (!Directory.EnumerateFiles(directory, "*.vcxproj").Any());

            string[] projectNamespaces = Path.GetFileNameWithoutExtension(Directory.EnumerateFiles(directory, "*.vcxproj").First()).Split(".");
            string[] fileNamespaces = Path.GetDirectoryName(filePath.Replace(directory, string.Empty)).Split(new[] { "/", "\\" }, StringSplitOptions.RemoveEmptyEntries);
            string[] namespaceParts = projectNamespaces.Concat(fileNamespaces).ToArray();

            string[] namespacesPrefix = namespaceParts.Select((x, i) =>
            {
                string padding = new string(' ', i * 4);
                return $"{padding}namespace {x}\r\n{padding}{{";
            })
            .ToArray();

            string[] namespacesSuffix = namespaceParts.Select((x, i) =>
            {
                string padding = new string(' ', i * 4);
                return $"{padding}}}";
            }).Reverse()
            .ToArray();

            if (namespaces.Count == 0)
            {
                Console.WriteLine($"File `{filePath}` has no namespaces at all!");
                Console.WriteLine("Use following code:");
                Console.WriteLine(string.Join("\r\n", namespacesPrefix));
                Console.WriteLine(string.Join("\r\n", namespacesSuffix));
                Console.ReadKey();
                return;
            }

            string[] licenseLines = lines.Take(lines.IndexOf($"namespace {namespaces.First()}")).ToArray();
            string[] fileSuffixLines = new string[0];

            lines = lines.Skip(lines.IndexOf(firstCodeLine)).ToList();

            for (int i = lines.Count - 1; i >= 0; --i)
            {
                if (lines[i] == "}")
                {
                    fileSuffixLines = lines.Skip(i + 1).ToArray();
                    lines = lines.Take(i - namespaces.Count + 1).ToList();
                    break;
                }
            }

            string firstPadding = WhitespacePrefixRegex.Match(firstCodeLine).Value;
            string newPadding = new string(' ', namespaceParts.Length * 4);

            Regex leftPaddingReplaceRegex = new Regex(Regex.Escape(firstPadding));

            for (int i = 0; i < lines.Count; ++i)
            {
                if (string.IsNullOrWhiteSpace(lines[i]))
                {
                    lines[i] = string.Empty;
                }
                else
                {
                    lines[i] = leftPaddingReplaceRegex.Replace(lines[i], newPadding, 1);
                }
            }

            lines = licenseLines.Concat(namespacesPrefix).Concat(lines).Concat(namespacesSuffix).Concat(fileSuffixLines).ToList();

            bool guardsModified = false;

            if (Path.GetExtension(filePath).Equals(".hpp"))
            {
                string guard = string.Join("_", namespaces.Concat(new[] { Path.GetFileNameWithoutExtension(filePath), "hpp" })).ToUpper();
                string oldGuard = string.Empty;

                int oldGuardIdx = -1;
                for (int i = 0; i < lines.Count; ++i)
                {
                    if (lines[i].StartsWith("#ifndef", StringComparison.InvariantCultureIgnoreCase))
                    {
                        oldGuard = lines[i].Split(' ').Last();
                        oldGuardIdx = i;
                        break;
                    }
                }
                
                if (!string.Equals(guard, oldGuard))
                {
                    if (oldGuardIdx < 0)
                    {
                        string hppLicense = LicenseProvider.GetLicenseText(string.Empty, "hpp").Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries).Last();
                        int lastLicenseLine = lines.IndexOf(hppLicense) + 2;

                        lines.Insert(lastLicenseLine, $"#ifndef {guard}");
                        lines.Insert(lastLicenseLine + 1, $"#define {guard}");
                        lines.Insert(lastLicenseLine + 2, string.Empty);

                        lines = lines.Concat(new[] { "", "#endif" }).ToList();
                    }
                    else
                    {
                        lines[oldGuardIdx] = $"#ifndef {guard}";
                        lines[oldGuardIdx + 1] = $"#define {guard}";
                    }

                    guardsModified = true;
                }
            }

            if (string.Equals(string.Join(".", namespaces), string.Join(".", namespaceParts)) && !guardsModified)
            {
                return;
            }

            fileText = string.Join("\r\n", lines);

            File.WriteAllText(filePath, fileText);
        }
    }
}
