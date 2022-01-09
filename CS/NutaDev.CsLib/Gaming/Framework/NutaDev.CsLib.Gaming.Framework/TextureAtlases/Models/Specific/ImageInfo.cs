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

using System.Drawing;

namespace NutaDev.CsLib.Gaming.Framework.TextureAtlases.Models.Specific
{
    public class ImageInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageInfo"/> class.
        /// </summary>
        public ImageInfo()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageInfo"/> class.
        /// </summary>
        /// <param name="path">Path to image.</param>
        /// <param name="image">Image to wrap.</param>
        /// <param name="margin">The margin.</param>
        public ImageInfo(string path, Image image, Rectangle margin)
        {
            Path = path;
            Image = image;
            Margin = margin;
        }

        /// <summary>
        /// Gets or sets image path.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets image.
        /// </summary>
        public Image Image { get; set; }

        /// <summary>
        /// Gets or sets margin.
        /// </summary>
        public Rectangle Margin { get; set; }

        /// <summary>
        /// Gets or sets position.
        /// </summary>
        public Point Position { get; set; }

        /// <summary>
        /// Gets image width including margin.
        /// </summary>
        public int Width { get { return Image.Width + Margin.Width; } }

        /// <summary>
        /// Gets image height including margin.
        /// </summary>
        public int Height { get { return Image.Height + Margin.Height; } }

        /// <summary>
        /// Gets image size.
        /// </summary>
        public Size Size { get { return new Size(Width, Height); } }
    }
}
