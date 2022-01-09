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

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Resources;

namespace NutaDev.CsLib.Reflection.Helpers
{
    /// <summary>
    /// Class with utility methods to work with <see cref="System.Reflection.Assembly"/> resources.
    /// </summary>
    public static class ResourcesHelper
    {
        /// <summary>
        /// Default resource suffix.
        /// </summary>
        public const string ResourceSuffix = ".g.resources";

        /// <summary>
        /// Returns array of file paths to <see cref="System.Reflection.Assembly"/>'s resources.
        /// </summary>
        /// <param name="assembly">Assembly to check.</param>
        /// <param name="directoryPath">Path to resource's directory.</param>
        /// <param name="resourcesSuffix">Source item suffix.</param>
        /// <returns>Array of file paths or null</returns>
        public static string[] GetFileList(Assembly assembly, string directoryPath, string resourcesSuffix = ResourceSuffix)
        {
            try
            {
                List<string> paths = new List<string>();

                Stream stream = assembly.GetManifestResourceStream(assembly.GetName().Name + resourcesSuffix);

                if (stream != null)
                {
                    using (ResourceReader reader = new ResourceReader(stream))
                    {
                        string path = directoryPath.ToUpper();

                        foreach (DictionaryEntry entry in reader)
                        {
                            string keyPath = entry.Key.ToString();

                            if (keyPath.ToUpper().StartsWith(path))
                            {
                                paths.Add(keyPath);
                            }
                        }
                    }
                }

                return paths.ToArray();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Returns resource's bytes from specific assembly at specified path.
        /// </summary>
        /// <param name="assembly">Source assembly.</param>
        /// <param name="pathToFile">Path to the specific file.</param>
        /// <param name="resourcesSuffix">Resource's file suffix.</param>
        /// <returns>Resource's bytes or null.</returns>
        public static byte[] GetResourceBytes(Assembly assembly, string pathToFile, string resourcesSuffix = ResourceSuffix)
        {
            try
            {
                Stream stream = assembly.GetManifestResourceStream(assembly.GetName().Name + resourcesSuffix);

                if (stream != null)
                {
                    using (ResourceReader reader = new ResourceReader(stream))
                    {
                        string path = pathToFile.ToUpper();

                        foreach (DictionaryEntry entry in reader)
                        {
                            if (entry.Key.ToString().ToUpper().Equals(path))
                            {
                                using (MemoryStream ms = new MemoryStream())
                                {
                                    if (entry.Value is Stream)
                                    {
                                        ((Stream)entry.Value).CopyTo(ms);
                                        return ms.ToArray();
                                    }
                                }
                            }
                        }
                    }
                }

                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}
