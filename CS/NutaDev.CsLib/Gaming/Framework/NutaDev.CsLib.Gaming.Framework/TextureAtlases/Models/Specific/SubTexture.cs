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
    /// <summary>
    /// The part of <see cref="TextureAtlas"/>.
    /// </summary>
    public class SubTexture
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubTexture"/> class.
        /// </summary>
        public SubTexture()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SubTexture"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="x">The x position.</param>
        /// <param name="y">The y position.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public SubTexture(string key, int x, int y, int width, int height)
        {
            Key = key;
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Gets or sets subtexture key.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets x position.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Gets or sets y position.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Gets or sets width.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets height.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Gets or sets rectangle properties.
        /// </summary>
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle(X, Y, Width, Height);
            }
            set
            {
                X = value.X;
                Y = value.Y;
                Width = value.Width;
                Height = value.Height;
            }
        }
    }
}
