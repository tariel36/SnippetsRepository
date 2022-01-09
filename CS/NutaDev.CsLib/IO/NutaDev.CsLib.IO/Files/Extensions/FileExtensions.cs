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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace NutaDev.CsLib.IO.Files.Extensions
{
    /// <summary>
    /// Utility class to work with file extensions.
    /// </summary>
    public static class FileExtensions
    {
        /// <summary>
        /// All extensions.
        /// </summary>
        public static readonly IReadOnlyCollection<Names> All = new ReadOnlyCollection<Names>(Enum.GetValues(typeof(Names)).Cast<Names>().Where(x => x != Names.Unknown && x != Names.MaxValue).ToList());

        /// <summary>
        /// Extensions qualified as image extensions.
        /// </summary>
        public static readonly IReadOnlyCollection<Names> Images = new ReadOnlyCollection<Names>(new List<Names>()
        {
            Names.Jpg,
            Names.Jpeg,
            Names.Png,
            Names.Bmp,
            Names.Tif,
            Names.Webp,
        });

        /// <summary>
        /// Extensions qualified as animated extensions.
        /// </summary>
        public static readonly IReadOnlyCollection<Names> Animated = new ReadOnlyCollection<Names>(new List<Names>()
        {
            Names.Gif,
        });

        /// <summary>
        /// Extensions qualified as music extensions.
        /// </summary>
        public static readonly IReadOnlyCollection<Names> Music = new ReadOnlyCollection<Names>(new List<Names>()
        {
            Names.Mp3,
            Names.Flac,
            Names.M4A,
            Names.Mdx,
            Names.Tta,
            Names.Wav,
            Names.Ape,
            Names.Wma,
        });

        /// <summary>
        /// Extensions qualified as playlist extensions.
        /// </summary>
        public static readonly IReadOnlyCollection<Names> Playlist = new ReadOnlyCollection<Names>(new List<Names>()
        {
            Names.Cue
        });

        /// <summary>
        /// Extensions qualified as dvd extensions.
        /// </summary>
        public static readonly IReadOnlyCollection<Names> Dvd = new ReadOnlyCollection<Names>(new List<Names>()
        {
            Names.Bup,
            Names.Ifo,
            Names.Vob,
        });

        /// <summary>
        /// Extensions qualified as video extensions.
        /// </summary>
        public static readonly IReadOnlyCollection<Names> Video = new ReadOnlyCollection<Names>(new List<Names>()
        {
            Names.Avi,
            Names.Mp4,
            Names.Flv,
            Names.Wmv,
            Names.Mkv,
            Names.Mpg,
            Names.Webm,
            Names.Mov,
            Names.M4v,
        });

        /// <summary>
        /// Checks if provided <paramref name="filePath"/> ends with one of the <paramref name="extensions"/>.
        /// </summary>
        /// <param name="filePath">File path to check.</param>
        /// <param name="extensions">Extensions to check.</param>
        /// <returns>True if <paramref name="filePath"/> ends with one of the <paramref name="extensions"/>, otherwise false.</returns>
        public static bool HasExtension(string filePath, params Names[] extensions)
        {
            if (string.IsNullOrWhiteSpace(filePath) || extensions == null || extensions.Length == 0)
            {
                return false;
            }

            string extension = Path.GetExtension(filePath);

            return extensions.Any(x => string.Equals(extension, $".{x}", StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
        /// Gets all extensions.
        /// </summary>
        /// <returns>Collection of all extensions.</returns>
        public static Names[] GetAllExtensions()
        {
            return All.ToArray();
        }

        /// <summary>
        /// Gets extensions qualified as image extensions.
        /// </summary>
        /// <returns>Extensions qualified as image extensions.</returns>
        public static Names[] GetImageExtensions()
        {
            return Images.ToArray();
        }

        /// <summary>
        /// Gets extensions qualified as animated extensions.
        /// </summary>
        /// <returns>Extensions qualified as animated extensions.</returns>
        public static Names[] GetAnimatedExtensions()
        {
            return Animated.ToArray();
        }

        /// <summary>
        /// Gets extensions qualified as music extensions.
        /// </summary>
        /// <returns>Extensions qualified as music extensions.</returns>
        public static Names[] GetMusicExtensions()
        {
            return Music.ToArray();
        }

        /// <summary>
        /// Gets extensions qualified as video extensions.
        /// </summary>
        /// <returns>Extensions qualified as video extensions.</returns>
        public static Names[] GetVideoExtensions()
        {
            return Video.ToArray();
        }

        /// <summary>
        /// Names of supported extensions.
        /// </summary>
        public enum Names
        {
            Unknown = 0,

            Jpg,
            Jpeg,
            Png,
            Gif,
            Mp3,
            Flac,
            M4A,
            Mdx,
            Tta,
            Wav,
            Ape,
            Cue,
            Bmp,
            Bup,
            Ifo,
            Vob,
            Avi,
            Mp4,
            Flv,
            Wmv,
            Wma,
            Mkv,
            Mpg,
            Tif,
            Webm,
            Mov,
            M4v,
            Webp,
            Torrent,

            MaxValue,
        }
    }
}
