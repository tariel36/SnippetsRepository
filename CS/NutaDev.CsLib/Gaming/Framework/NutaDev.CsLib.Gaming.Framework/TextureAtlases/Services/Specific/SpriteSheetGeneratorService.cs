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

using NutaDev.CsLib.Gaming.Framework.TextureAtlases.Models.Specific;
using NutaDev.CsLib.Gaming.Resources;
using NutaDev.CsLib.Maintenance.Exceptions.Factories;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace NutaDev.CsLib.Gaming.Framework.TextureAtlases.Services.Specific
{
    public class SpriteSheetGeneratorService
    {
        /// <summary>
        /// Generates <see cref="TextureAtlas"/> content.
        /// </summary>
        /// <param name="atlas">Atlas to fill.</param>
        /// <param name="files">Files to use.</param>
        /// <param name="margin">Margin of each image.</param>
        /// <param name="outBitmapPath">Output canvas path.</param>
        /// <returns>Generated texture atlas.</returns>
        public TextureAtlas Generate(TextureAtlas atlas, List<string> files, Rectangle margin, string outBitmapPath)
        {
            List<ImageInfo> images = GetImageInfo(files, margin).OrderByDescending(x => x.Image.Width * x.Image.Height).ToList();

            Bitmap bitmap = Draw(images);

            bitmap.Save(outBitmapPath, ImageFormat.Png);

            atlas.Textures.AddRange(images.Select(x => new SubTexture(Path.GetFileNameWithoutExtension(x.Path), x.Position.X, x.Position.Y, x.Image.Width, x.Image.Height)));

            Cleanup(images);
            bitmap.Dispose();

            return atlas;
        }

        /// <summary>
        /// Creates <see cref="ImageInfo"/> based on <see cref="files"/>.
        /// </summary>
        /// <param name="files">Files to use.</param>
        /// <param name="margin">Margin of image.</param>
        /// <returns>Collection of prepared images.</returns>
        private List<ImageInfo> GetImageInfo(List<string> files, Rectangle margin)
        {
            List<ImageInfo> images = new List<ImageInfo>();

            foreach (string fPath in files)
            {
                images.Add(new ImageInfo(fPath, Image.FromFile(fPath), margin));
            }

            return images;
        }

        /// <summary>
        /// Draws images on canvas.
        /// </summary>
        /// <param name="images">Images to draw.</param>
        /// <returns>Full size canvas.</returns>
        private Bitmap Draw(List<ImageInfo> images)
        {
            int maxWidth;
            int maxHeight;

            if (images.Count > 1)
            {
                maxWidth = images.Take(images.Count / 2).Sum(x => x.Width);
                maxHeight = images.Take(images.Count / 2).Sum(x => x.Height);
            }
            else
            {
                maxWidth = images[0].Width;
                maxHeight = images[0].Height;
            }

            List<Point> positions = new List<Point>();

            Size previousSize = Size.Empty;
            bool sizeFound = false;
            int side = Math.Max(maxWidth, maxHeight);
            Size bmpSize = new Size(side, side);

            while (true)
            {
                try
                {
                    if (sizeFound)
                    {
                        break;
                    }

                    List<Rectangle> usedRectangles = new List<Rectangle>();
                    foreach (ImageInfo info in images)
                    {
                        info.Position = Point.Empty;
                    }

                    foreach (ImageInfo info in images)
                    {
                        info.Position = FindNextPosition(info, bmpSize, usedRectangles);
                        usedRectangles.Add(new Rectangle(info.Position, new Size(info.Width, info.Height)));
                    }

                    positions.Clear();

                    foreach (ImageInfo info in images)
                    {
                        positions.Add(info.Position);
                    }

                    previousSize = bmpSize;

                    maxWidth /= 2;
                    maxHeight /= 2;

                    if (maxWidth == 0 || maxHeight == 0)
                    {
                        bmpSize = previousSize;
                        break;
                    }

                    side = Math.Max(maxWidth, maxHeight);
                    bmpSize = new Size(side, side);
                }
                catch
                {
                    bmpSize = previousSize;
                    sizeFound = true;
                }
            }

            Bitmap targetBitmap = new Bitmap(bmpSize.Width, bmpSize.Height);
            using (Graphics graphics = Graphics.FromImage(targetBitmap))
            {
                for (int i = 0; i < images.Count; ++i)
                {
                    ImageInfo info = images[i];
                    info.Position = positions[i];
                    graphics.DrawImage(info.Image, info.Position);
                }
            }

            return targetBitmap;
        }

        /// <summary>
        /// Finds the next viable postion.
        /// </summary>
        /// <param name="info">Image info.</param>
        /// <param name="bmpSize">Bitmap size.</param>
        /// <param name="usedRectangles">Already used rectangles.</param>
        /// <returns>The next viable position.</returns>
        private Point FindNextPosition(ImageInfo info, Size bmpSize, List<Rectangle> usedRectangles)
        {
            Rectangle bmpRectangle = new Rectangle(0, 0, bmpSize.Width, bmpSize.Height);

            if (usedRectangles.Count == 0)
            {
                Rectangle imageRectangle = new Rectangle(Point.Empty, info.Size);

                if (!bmpRectangle.Contains(imageRectangle))
                {
                    throw ExceptionFactory.InvalidOperationException(Text.CanvasIsTooSmall);
                }

                return Point.Empty;
            }

            Point currentPos = Point.Empty;
            int prevY = 0;

            while (true)
            {
                Rectangle imageRectangle = new Rectangle(currentPos, info.Size);

                foreach (Rectangle rect in usedRectangles)
                {
                    if (usedRectangles.All(x => !x.IntersectsWith(imageRectangle)) && bmpRectangle.Contains(imageRectangle))
                    {
                        return imageRectangle.Location;
                    }

                    currentPos = new Point(rect.X + rect.Width, currentPos.Y);
                    imageRectangle = new Rectangle(currentPos, info.Size);
                }

                int y = usedRectangles.Select(x => x.Y + x.Height).Where(x => x > prevY).Min();
                prevY = y;

                currentPos = new Point(0, y);
            }
        }

        /// <summary>
        /// Cleanups the resources.
        /// </summary>
        /// <param name="images">Images to clean.</param>
        private void Cleanup(List<ImageInfo> images)
        {
            foreach (ImageInfo info in images)
            {
                info.Image.Dispose();
                info.Image = null;
            }

            images.Clear();
        }
    }
}
