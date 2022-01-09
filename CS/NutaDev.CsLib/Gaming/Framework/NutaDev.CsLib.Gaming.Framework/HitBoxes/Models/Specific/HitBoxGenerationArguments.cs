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

using NutaDev.CsLib.Gaming.Framework.HitBoxes.Services;
using System.Drawing;

namespace NutaDev.CsLib.Gaming.Framework.HitBoxes.Models.Specific
{
    /// <summary>
    /// HitBox generation arguments for <see cref="HitBoxService"/>.
    /// </summary>
    public class HitBoxGenerationArguments
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HitBoxGenerationArguments"/> class.
        /// </summary>
        /// <param name="generationMode">Generation mode.</param>
        /// <param name="cropRect">Cropping rectangle.</param>
        /// <param name="textureFilePath">Path to image.</param>
        /// <param name="rectWidth">Rectangle width.</param>
        /// <param name="heightMargin">Rectangle height.</param>
        /// <param name="step">The step.</param>
        /// <param name="tolerance">The tolerance.</param>
        public HitBoxGenerationArguments(HitBoxGenerationMode generationMode, Rectangle cropRect, string textureFilePath, int rectWidth = 100, int heightMargin = 0, int step = 1, double tolerance = 1.0)
        {
            GenerationMode = generationMode;
            CropRect = cropRect;
            TextureFilePath = textureFilePath;
            RectWidth = rectWidth;
            HeightMargin = heightMargin;
            Step = step;
            Tolerance = tolerance;
        }

        /// <summary>
        /// Gets or sets generation mode.
        /// </summary>
        public HitBoxGenerationMode GenerationMode { get; set; }

        /// <summary>
        /// Gets or sets cropping rectangle.
        /// </summary>
        public Rectangle CropRect { get; set; }

        /// <summary>
        /// Gets or sets path to file.
        /// </summary>
        public string TextureFilePath { get; set; }

        /// <summary>
        /// Gets or sets rect width.
        /// </summary>
        public int RectWidth { get; set; }

        /// <summary>
        /// Gets or sets height margin.
        /// </summary>
        public int HeightMargin { get; set; }

        /// <summary>
        /// Gets or sets step.
        /// </summary>
        public int Step { get; set; }

        /// <summary>
        /// Gets or sets tolerance.
        /// </summary>
        public double Tolerance { get; set; }
    }
}
