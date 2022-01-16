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

using System;
using System.IO;
using System.Text;

namespace NutaDev.CsLib.Internal.ConsoleTools.Files
{
    /// <summary>
    /// Provides license in various forms.
    /// </summary>
    public class LicenseProvider
    {
        /// <summary>
        /// Gets or sets cached license text.
        /// </summary>
        private string LicenseText { get; set; }

        /// <summary>
        /// Returns untouched license text.
        /// </summary>
        /// <returns></returns>
        public string GetLicenseText()
        {
            if (string.IsNullOrEmpty(LicenseText))
            {
                LicenseText = Resources.Resources.LicenseText;
            }

            return LicenseText;
        }

        /// <summary>
        /// Returns license text after formatting.
        /// </summary>
        /// <param name="author">Author to set.</param>
        /// <returns>Formatted license text.</returns>
        public string GetLicenseTextFormatted(string author)
        {
            string licenseText = GetLicenseText();
            return string.Format(licenseText, DateTime.Now.Year, author);
        }

        /// <summary>
        /// Gets specific license text line, 0-base index.
        /// </summary>
        /// <param name="idx">Line number, 0-base index.</param>
        /// <returns>Speicifc line or null.</returns>
        public string GetLicenseTextLine(int idx)
        {
            return GetLine(idx, GetLicenseText());
        }

        /// <summary>
        /// Gets specific license text line, 0-base index and performs formatting for specific file type.
        /// </summary>
        /// <param name="idx">Line number.</param>
        /// <param name="extension">File extension.</param>
        /// <returns>License line or null.</returns>
        public string GetLicenseText(int idx, string author, string extension)
        {
            return GetLine(idx, GetLicenseText(author, extension));
        }

        /// <summary>
        /// Gets license text prepared for insertion into specific file type.
        /// </summary>
        /// <param name="extension">File extension.</param>
        /// <returns>Prepared license text.</returns>
        public string GetLicenseText(string author, string extension)
        {
            string licenseText = GetLicenseTextFormatted(author);

            switch (extension)
            {
                case "hpp":
                case "cpp":
                case "cs":
                {
                    return $"// {licenseText.Replace("\r\n", "\n").Replace("\n", "\r\n// ")}\r\n\r\n";
                }
                case "xaml":
                {
                    return $"<!--\r\n{licenseText}\r\n-->\r\n\r\n";
                }
            }

            return null;
        }

        /// <summary>
        /// Gets copyright line.
        /// </summary>
        /// <param name="author">Author to set.</param>
        /// <param name="extension">File extension. Null is allowed.</param>
        /// <returns>Copyright line.</returns>
        public string GetCopyright(string author, string extension = null)
        {
            if (string.IsNullOrWhiteSpace(null))
            {
                return string.Format("Copyright (c) {0} {1}", DateTime.Now.Year, author);
            }

            switch (extension)
            {
                case "hpp":
                case "cpp":
                case "cs":
                {
                    return $"// {GetCopyright(author)}";
                }
                case "xaml":
                {
                    return GetCopyright(author);
                }
            }

            return null;
        }

        /// <summary>
        /// Gets number of lines in license.
        /// </summary>
        /// <returns>Number of lines.</returns>
        public int GetLicenseLineCount()
        {
            return GetLicenseText().Split(new[] { "\r\n", "\n" }, StringSplitOptions.None).Length;
        }

        /// <summary>
        /// Gets specific line from next, 0-base index.
        /// </summary>
        /// <param name="idx">Line number, 0-bae index.</param>
        /// <param name="text">Text to parse.</param>
        /// <returns>Line text or null.</returns>
        private string GetLine(int idx, string text)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    using (StreamWriter sw = new StreamWriter(ms, Encoding.Default, 128, true))
                    {
                        sw.Write(text);
                    }

                    ms.Position = 0;

                    using (StreamReader sr = new StreamReader(ms))
                    {
                        for (int i = 0; i < idx; ++i)
                        {
                            string tmp1 = sr.ReadLine();
                        }

                        string tmp2 = sr.ReadLine();
                        return tmp2;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.ToString());
            }

            return null;
        }
    }
}
