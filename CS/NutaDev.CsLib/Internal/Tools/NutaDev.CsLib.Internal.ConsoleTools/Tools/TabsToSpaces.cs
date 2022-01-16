using NutaDev.CsLib.Internal.ConsoleTools.Files;
using System.IO;
using System.Text.RegularExpressions;

namespace NutaDev.CsLib.Internal.ConsoleTools.Tools
{
    /// <summary>
    /// Performs replacement of tabs into spaces.
    /// </summary>
    public class TabsToSpaces
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TabsToSpaces"/> class.
        /// </summary>
        /// <param name="rootPath">Root path of solution.</param>
        public TabsToSpaces(string rootPath)
        {
            RootPath = rootPath;

            InnerWhitespacesRegex = new Regex("(?<=[a-zA-Z0-9><:])[\\t ][\\t ]+", RegexOptions.Compiled | RegexOptions.Multiline);
        }

        /// <summary>
        /// Gets root path of the project.
        /// </summary>
        public string RootPath { get; }

        /// <summary>
        /// Gets inner whitespace replace regex.
        /// </summary>
        private Regex InnerWhitespacesRegex { get; }

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

        /// <summary>
        /// Handles actions when certain file is found.
        /// </summary>
        /// <param name="filePath">Absolute path to file.</param>
        private void OnFileAction(string filePath)
        {
            string fileText = File.ReadAllText(filePath);

            if (fileText.Contains("\t"))
            {
                fileText = fileText.Replace("\t", "    ");
                fileText = InnerWhitespacesRegex.Replace(fileText, " ");

                File.WriteAllText(filePath, fileText);
            }
        }
    }
}
