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

using NutaDev.CsLib.Audio.Formats.Cue;
using NutaDev.CsLib.External.Audio.TagLib.Models;
using NutaDev.CsLib.IO.Files.Extensions;
using NutaDev.CsLib.Maintenance.Exceptions.Factories;
using NutaDev.CsLib.Resources.Text.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TagLib;

namespace NutaDev.CsLib.External.Audio.TagLib.Files
{
    /// <summary>
    /// Applies metadata to file based on CUE file. The CUE file must be in the same directory that modified file.
    /// </summary>
    public class ApplyCueToFileMetadata
    {
        /// <summary>
        /// Executes the modification.
        /// </summary>
        /// <param name="absoluteFilePath">Absolute path to file that will be modified.</param>
        public void Execute(string absoluteFilePath)
        {
            if (string.IsNullOrWhiteSpace(absoluteFilePath))
            {
                throw ExceptionFactory.Create<FileNotFoundException>(Text.File_0_NotFound, absoluteFilePath);
            }

            string directory = Path.GetDirectoryName(absoluteFilePath);

            if (string.IsNullOrWhiteSpace(directory))
            {
                throw ExceptionFactory.Create<DirectoryNotFoundException>(Text.Directory_0_NotFound, directory);
            }

            List<string> cueFilePaths = Directory.GetFiles(directory).Where(x => FileExtensions.HasExtension(x, FileExtensions.Names.Cue)).ToList();

            if (cueFilePaths.Count > 1)
            {
                throw ExceptionFactory.Create<InvalidOperationException>(Text.MoreThanOneCueFileInDirectory_0_, directory);
            }
            else if (cueFilePaths.Count == 0)
            {
                throw ExceptionFactory.Create<InvalidOperationException>(Text.NoCueFilesInDirectory_0_, directory);
            }

            CueFile cue = new CueFile();
            cue.Load(cueFilePaths[0]);

            if (cue.Discs.Count == 0)
            {
                throw ExceptionFactory.Create<InvalidOperationException>(Text.NoDiscsInCue_0_, cue.Path);
            }

            foreach (CueDisc file in cue.Discs)
            {
                foreach (CueTrack track in file.Tracks)
                {
                    if (absoluteFilePath.ToUpperInvariant().Contains(track.Title.ToUpperInvariant()))
                    {
                        using (global::TagLib.File tagsfile = global::TagLib.File.Create(absoluteFilePath, ReadStyle.None))
                        {
                            tagsfile.Tag.AlbumArtists = string.IsNullOrWhiteSpace(cue.Performer)
                                ? null
                                : new[] { cue.Performer };
                            tagsfile.Save();

                            List<string> composers = new List<string>();
                            if (!string.IsNullOrWhiteSpace(cue.Composer)) { composers.Add(cue.Composer); }
                            if (!string.IsNullOrWhiteSpace(track.Composer)) { composers.Add(track.Composer); }

                            tagsfile.Tag.Composers = composers.Count > 0
                                ? composers.ToArray()
                                : null;
                            tagsfile.Save();

                            tagsfile.Tag.Title = track.Title;
                            tagsfile.Save();

                            tagsfile.Tag.AlbumArtists = string.IsNullOrWhiteSpace(track.Performer)
                                ? null
                                : new[] { track.Performer };
                            tagsfile.Save();

                            tagsfile.Tag.Year = (uint)cue.Year;
                            tagsfile.Save();

                            tagsfile.Tag.Genres = new[] { cue.Genre };
                            tagsfile.Save();

                            tagsfile.Tag.Album = cue.Title;
                            tagsfile.Save();

                            tagsfile.Tag.Track = (uint)track.Index;
                            tagsfile.Save();

                            global::TagLib.Ogg.XiphComment xiphComment = tagsfile.GetTag(TagTypes.Xiph, true) as global::TagLib.Ogg.XiphComment;

                            if (xiphComment != null)
                            {
                                XiphCommentDescription myXiphComment = new XiphCommentDescription(xiphComment);

                                myXiphComment.Comment = null;
                                tagsfile.Save();

                                myXiphComment.Description = null;
                                tagsfile.Save();
                            }
                            else
                            {
                                string oldComment = tagsfile.Tag.Comment ?? string.Empty;
                                tagsfile.Tag.Comment = null;
                            }

                            tagsfile.Save();
                        }
                    }
                }
            }
        }
    }
}
