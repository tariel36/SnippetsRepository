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

using System.IO;

namespace NutaDev.CsLib.IO.Directories
{
    /// <summary>
    /// Utility methods for directory operations.
    /// </summary>
    public static class DirectoryHelper
    {
        /// <summary>
        /// Checks if the <paramref name="dirPath"/> exists. Creates the directory if not.
        /// </summary>
        /// <param name="dirPath">Directory to check.</param>
        public static void EnsureDirectoryExists(string dirPath)
        {
            if (!string.IsNullOrWhiteSpace(dirPath) && !Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
        }

        /// <summary>
        /// Copies files and directories from <paramref name="sourceDir"/> to <paramref name="targetDir"/>.
        /// </summary>
        /// <param name="sourceDir">Source directory.</param>
        /// <param name="targetDir">Target directory.</param>
        /// <param name="copyDirectories">If set to true, nested directories will be copied too.</param>
        /// <param name="enusureDirectoryExists">Checks if target directory exists and creates it if not.</param>
        /// <param name="throwOnException">Rethrows first exception.</param>
        public static void CopyFiles(string sourceDir, string targetDir, bool copyDirectories = true, bool enusureDirectoryExists = true, bool throwOnException = false)
        {
            if (!Directory.Exists(sourceDir))
            {
                return;
            }

            if (enusureDirectoryExists)
            {
                EnsureDirectoryExists(targetDir);
            }

            foreach (string path in Directory.EnumerateFileSystemEntries(sourceDir))
            {
                try
                {
                    if (!string.IsNullOrWhiteSpace(path))
                    {
                        FileAttributes attr = File.GetAttributes(path);
                        if (attr.HasFlag(FileAttributes.Directory) && copyDirectories)
                        {
                            string dirName = Path.GetDirectoryName(path);
                            if (!string.IsNullOrWhiteSpace(dirName))
                            {
                                string newPath = Path.Combine(targetDir, dirName);
                                EnsureDirectoryExists(newPath);
                                CopyFiles(path, newPath, true, enusureDirectoryExists, throwOnException);
                            }
                        }
                        else
                        {
                            File.Copy(path, Path.Combine(targetDir, Path.GetFileName(path)));
                        }
                    }
                }
                catch
                {
                    if (throwOnException)
                    {
                        throw;
                    }
                }
            }
        }
    }
}
