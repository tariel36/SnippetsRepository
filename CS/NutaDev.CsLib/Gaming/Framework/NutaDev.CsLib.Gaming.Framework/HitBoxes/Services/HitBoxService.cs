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

using NutaDev.CsLib.Collections.Extensions;
using NutaDev.CsLib.Gaming.Framework.HitBoxes.Models.Specific;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace NutaDev.CsLib.Gaming.Framework.HitBoxes.Services
{
    /// <summary>
    /// Provides utility methods to generate hitboxes.
    /// </summary>
    public class HitBoxService
    {
        #region Members

        /// <summary>
        /// The padding.
        /// </summary>
        private const int Padding = 10;

        /// <summary>
        /// The <see cref="Padding"/> twice.
        /// </summary>
        private const int PaddingTwice = Padding * 2;

        /// <summary>
        /// The grayscale color matrix based on https://stackoverflow.com/questions/2265910/convert-an-image-to-grayscale article.
        /// </summary>
        private static readonly ColorMatrix GreyScaleColorMatrix = new ColorMatrix(new[]
        {
            new [] { 0.3f, 0.3f, 0.3f, 0.0f, 0.0f },
            new [] { 0.59f, 0.59f, 0.59f, 0.0f, 0.0f },
            new [] { 0.11f, 0.11f, 0.11f, 0.0f, 0.0f },
            new [] { 0.0f, 0.0f, 0.0f, 1.0f, 0.0f },
            new [] { 0.0f, 0.0f, 0.0f, 0.0f, 1.0f }
        });

        #endregion

        /// <summary>
        /// Get the hitboxes.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static Rectangle[] GetHitboxRectangles(HitBoxGenerationArguments args)
        {
            switch (args.GenerationMode)
            {
                case HitBoxGenerationMode.PixelPerfect:
                case HitBoxGenerationMode.LowRectangleCount:
                {
                    return GenerateMultipleRectangleHitBox(args.GenerationMode, args.CropRect, args.TextureFilePath, args.RectWidth, args.HeightMargin);
                }
                case HitBoxGenerationMode.SourceRectangle:
                {
                    return GenerateSourceRectangleHitBox(args.CropRect);
                }
            }

            return new Rectangle[0];
        }

        #region Rectangle based

        /// <summary>
        /// Generates hitboxes based on multiple rectangles.
        /// </summary>
        /// <param name="generationMode">Generation mode.</param>
        /// <param name="cropRect">The cropping rectangle.</param>
        /// <param name="textureFilePath">File path to bitmap.</param>
        /// <param name="rectWidth">Rectangle width.</param>
        /// <param name="heightMargin">Rectangle height.</param>
        /// <returns></returns>
        private static Rectangle[] GenerateMultipleRectangleHitBox(HitBoxGenerationMode generationMode, Rectangle cropRect, string textureFilePath, int rectWidth, int heightMargin)
        {
            using (Bitmap sourceBmp = new Bitmap(textureFilePath))
            {
                using (Bitmap crop = Crop(sourceBmp, cropRect))
                {
                    using (Bitmap paddedBitmap = ApplyPadding(crop))
                    {
                        using (Bitmap gsBitmap = MakeGrayscale3(paddedBitmap))
                        {
                            List<Point> edges = FindEdgesByFloodFill(gsBitmap);
                            edges = RemovePaddingFromPoints(edges);

                            Rectangle[] rectangles = GetHitboxRectangles(edges, rectWidth).ToArray();

                            if (generationMode == HitBoxGenerationMode.LowRectangleCount)
                            {
                                rectangles = SimplifyRectangles(rectangles, heightMargin);
                            }

                            return rectangles;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Creates hitbox based on source rectangle.
        /// </summary>
        /// <param name="source">Source rectangle.</param>
        /// <returns>The hitbox.</returns>
        private static Rectangle[] GenerateSourceRectangleHitBox(Rectangle source)
        {
            return new[] { new Rectangle(0, 0, source.Width, source.Height) };
        }

        /// <summary>
        /// Tries to remove redundant rectangles.
        /// </summary>
        /// <param name="rectangles">Source rectangles.</param>
        /// <param name="heightMargin">The margin.</param>
        /// <returns>Collection of simplified rectangles.</returns>
        private static Rectangle[] SimplifyRectangles(Rectangle[] rectangles, int heightMargin)
        {
            List<Rectangle> result = new List<Rectangle>();

            Rectangle currentRectangle = rectangles[0];

            List<int> heights = new List<int>();
            List<int> ys = new List<int>();

            for (int i = 1; i < rectangles.Length; ++i)
            {
                Rectangle rect = rectangles[i];

                heights.Add(rect.Height);
                ys.Add(rect.Y);

                if (Math.Abs(rect.Y - currentRectangle.Y) > heightMargin || Math.Abs(rect.Bottom - currentRectangle.Bottom) > heightMargin)
                {
                    result.Add(new Rectangle(currentRectangle.X, (int)ys.Average(), rectangles[i].X - currentRectangle.X, (int)heights.Average()));

                    ++i;

                    if (rectangles.IndexWithinBounbds(i))
                    {
                        currentRectangle = rectangles[i];
                    }
                }
            }

            return result.ToArray();
        }

        /// <summary>
        /// Finds edges using flood fill algorithm.
        /// </summary>
        /// <param name="bmp">Source bitmap.</param>
        /// <returns>Collection of edges.</returns>
        private static List<Point> FindEdgesByFloodFill(Bitmap bmp)
        {
            List<Point> edges = new List<Point>();

            Color targetColor = bmp.GetPixel(0, 0);
            Color replacementColor = Color.FromArgb(255, 1, 0, 0);

            Stack<Point> pixels = new Stack<Point>();
            pixels.Push(new Point(0, 0));

            while (pixels.Count > 0)
            {
                Point point = pixels.Pop();

                if (point.X < bmp.Width && point.X >= 0 && point.Y < bmp.Height && point.Y >= 0)
                {
                    Color pixel = bmp.GetPixel(point.X, point.Y);

                    if (pixel == targetColor)
                    {
                        bmp.SetPixel(point.X, point.Y, replacementColor);
                        pixels.Push(new Point(point.X - 1, point.Y));
                        pixels.Push(new Point(point.X + 1, point.Y));
                        pixels.Push(new Point(point.X, point.Y - 1));
                        pixels.Push(new Point(point.X, point.Y + 1));
                    }
                    else if (!ColorEquals(pixel, targetColor) && !ColorEquals(pixel, replacementColor))
                    {
                        edges.Add(point);
                    }
                }
            }

            return edges;
        }

        /// <summary>
        /// Gets rectangles that indicates hitbox.
        /// </summary>
        /// <param name="edges">Existing edges.</param>
        /// <param name="lineWidth">The hitbox line width.</param>
        /// <returns>Collection of hitboxes.</returns>
        private static List<Rectangle> GetHitboxRectangles(List<Point> edges, int lineWidth)
        {
            Dictionary<int, Tuple<Point, Point>> edgePairsByX = edges.GroupBy(x => x.X).ToDictionary(k => k.Key, v => Tuple.Create(v.OrderBy(y => y.Y).First(), v.OrderBy(y => y.Y).Last()));

            List<List<KeyValuePair<int, Tuple<Point, Point>>>> groupedByEqualY = new List<List<KeyValuePair<int, Tuple<Point, Point>>>>();

            List<KeyValuePair<int, Tuple<Point, Point>>> group = new List<KeyValuePair<int, Tuple<Point, Point>>>();
            foreach (KeyValuePair<int, Tuple<Point, Point>> kvYEdges in edgePairsByX)
            {
                if (group.Count == 0)
                {
                    group.Add(kvYEdges);
                }
                else
                {
                    KeyValuePair<int, Tuple<Point, Point>> prev = group.Last();
                    KeyValuePair<int, Tuple<Point, Point>> curr = kvYEdges;

                    if (prev.Value.Item1.Y == curr.Value.Item1.Y
                        && prev.Value.Item2.Y == curr.Value.Item2.Y
                        && group.Count < lineWidth)
                    {
                        group.Add(curr);
                    }
                    else
                    {
                        groupedByEqualY.Add(group);
                        group = new List<KeyValuePair<int, Tuple<Point, Point>>>();
                        group.Add(curr);
                    }
                }
            }

            List<Rectangle> rects = new List<Rectangle>();

            for (int i = 0; i < groupedByEqualY.Count; ++i)
            {
                Tuple<Point, Point> left = groupedByEqualY[i].OrderBy(x => x.Value.Item1.X).First().Value;
                Tuple<Point, Point> right = groupedByEqualY[i].OrderBy(x => x.Value.Item1.X).Last().Value;

                int width = right.Item2.X - left.Item1.X + 1;

                rects.Add(new Rectangle(left.Item1.X, left.Item1.Y, width, right.Item2.Y - left.Item1.Y));
            }

            return rects;
        }

        #endregion
        
        #region Utility

        /// <summary>
        /// Calculates points without padding.
        /// </summary>
        /// <param name="points">Points to calculate.</param>
        /// <returns>Points without padding.</returns>
        private static List<Point> RemovePaddingFromPoints(List<Point> points)
        {
            return points.Select(x => new Point(x.X - Padding, x.Y - Padding)).ToList();
        }

        /// <summary>
        /// Crops the bitmap.
        /// </summary>
        /// <param name="original">Source bitmap.</param>
        /// <param name="cropRect">Cropping rectangle.</param>
        /// <returns>Cropped bitmap.</returns>
        private static Bitmap Crop(Bitmap original, Rectangle cropRect)
        {
            Bitmap newBitmap = new Bitmap(cropRect.Width, cropRect.Height);

            using (Graphics g = Graphics.FromImage(newBitmap))
            {
                ImageAttributes attributes = new ImageAttributes();
                g.DrawImage(original, new Rectangle(0, 0, cropRect.Width, cropRect.Height), cropRect.X, cropRect.Y, cropRect.Width, cropRect.Height, GraphicsUnit.Pixel, attributes);
            }

            return newBitmap;
        }

        /// <summary>
        /// Applies padding to bitmap.
        /// </summary>
        /// <param name="original">Source bitmap.</param>
        /// <returns>Padded bitmap.</returns>
        private static Bitmap ApplyPadding(Bitmap original)
        {
            Bitmap newBitmap = new Bitmap(original.Width + PaddingTwice, original.Height + PaddingTwice);

            using (Graphics g = Graphics.FromImage(newBitmap))
            {
                ImageAttributes attributes = new ImageAttributes();
                g.DrawImage(original, new Rectangle(Padding, Padding, original.Width, original.Height), 0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);
            }

            return newBitmap;
        }

        /// <summary>
        /// Conver bitmap to greyscale.
        /// </summary>
        /// <param name="source">Source image.</param>
        /// <returns>Greyscale image.</returns>
        private static Bitmap MakeGrayscale3(Bitmap source)
        {
            Bitmap newBitmap = new Bitmap(source.Width, source.Height);

            using (Graphics graphics = Graphics.FromImage(newBitmap))
            {
                ImageAttributes attributes = new ImageAttributes();

                attributes.SetColorMatrix(GreyScaleColorMatrix);

                graphics.DrawImage(source, new Rectangle(0, 0, source.Width, source.Height), 0, 0, source.Width, source.Height, GraphicsUnit.Pixel, attributes);
            }

            return newBitmap;
        }

        /// <summary>
        /// Check whether colors are equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        private static bool ColorEquals(Color left, Color right)
        {
            return left.A == right.A
                   && left.R == right.R
                   && left.G == right.G
                   && left.B == right.B
                ;
        }

        #endregion
    }
}
