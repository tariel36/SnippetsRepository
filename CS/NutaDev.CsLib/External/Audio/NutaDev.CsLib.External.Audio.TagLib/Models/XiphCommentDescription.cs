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

namespace NutaDev.CsLib.External.Audio.TagLib.Models
{
    /// <summary>
    /// This is wrapper class that fixes retarded assumption that COMMENT and DESCRIPTION can't exists within single file.
    /// </summary>
    internal class XiphCommentDescription
        : global::TagLib.Ogg.XiphComment
    {
        /// <summary>
        /// Comment filed name.
        /// </summary>
        private const string CommentField = "COMMENT";

        /// <summary>
        /// Description field name.
        /// </summary>
        private const string DescriptionField = "DESCRIPTION";

        /// <summary>
        /// Initializes a new instance of the <see cref="XiphCommentDescription"/> class.
        /// </summary>
        /// <param name="inner">XiphComment object.</param>
        public XiphCommentDescription(global::TagLib.Ogg.XiphComment inner)
        {
            Inner = inner;
        }

        /// <summary>
        /// Gets or sets internal, wrapped instance.
        /// </summary>
        public global::TagLib.Ogg.XiphComment Inner { get; set; }

        /// <summary>
        /// Gets ors sets comment field.
        /// </summary>
        public override string Comment
        {
            get { return Inner.GetFirstField(CommentField); }
            set { Inner.SetField(CommentField, value); }
        }

        /// <summary>
        /// Gets ors sets description field.
        /// </summary>
        public string Description
        {
            get { return Inner.GetFirstField(DescriptionField); }
            set { Inner.SetField(DescriptionField, value); }
        }

        #region Properties Inner.*
        /// <summary>
        /// Gets TagTypes.
        /// </summary>
        public override global::TagLib.TagTypes TagTypes
        {
            get { return Inner.TagTypes; }
        }

        /// <summary>
        /// Gets or sets title.
        /// </summary>
        public override string Title
        {
            get { return Inner.Title; }
            set { Inner.Title = value; }
        }

        /// <summary>
        /// Gets or sets title sort.
        /// </summary>
        public override string TitleSort
        {
            get { return Inner.TitleSort; }
            set { Inner.TitleSort = value; }
        }

        /// <summary>
        /// Gets or sets performers.
        /// </summary>
        public override string[] Performers
        {
            get { return Inner.Performers; }
            set { Inner.Performers = value; }
        }

        /// <summary>
        /// Gets or sets performers sort.
        /// </summary>
        public override string[] PerformersSort
        {
            get { return Inner.PerformersSort; }
            set { Inner.PerformersSort = value; }
        }

        /// <summary>
        /// Gets or sets album artists.
        /// </summary>
        public override string[] AlbumArtists
        {
            get { return Inner.AlbumArtists; }
            set { Inner.AlbumArtists = value; }
        }

        /// <summary>
        /// Gets or sets album artists sort.
        /// </summary>
        public override string[] AlbumArtistsSort
        {
            get { return Inner.AlbumArtistsSort; }
            set { Inner.AlbumArtistsSort = value; }
        }

        /// <summary>
        /// Gets or sets composers.
        /// </summary>
        public override string[] Composers
        {
            get { return Inner.Composers; }
            set { Inner.Composers = value; }
        }

        /// <summary>
        /// Gets or sets composers sort.
        /// </summary>
        public override string[] ComposersSort
        {
            get { return Inner.ComposersSort; }
            set { Inner.ComposersSort = value; }
        }

        /// <summary>
        /// Gets or sets album.
        /// </summary>
        public override string Album
        {
            get { return Inner.Album; }
            set { Inner.Album = value; }
        }

        /// <summary>
        /// Gets or sets album sort.
        /// </summary>
        public override string AlbumSort
        {
            get { return Inner.AlbumSort; }
            set { Inner.AlbumSort = value; }
        }

        /// <summary>
        /// Gets or sets genres.
        /// </summary>
        public override string[] Genres
        {
            get { return Inner.Genres; }
            set { Inner.Genres = value; }
        }

        /// <summary>
        /// Gets or sets year.
        /// </summary>
        public override uint Year
        {
            get { return Inner.Year; }
            set { Inner.Year = value; }
        }

        /// <summary>
        /// Gets or sets track.
        /// </summary>
        public override uint Track
        {
            get { return Inner.Track; }
            set { Inner.Track = value; }
        }

        /// <summary>
        /// Gets or sets track count.
        /// </summary>
        public override uint TrackCount
        {
            get { return Inner.TrackCount; }
            set { Inner.TrackCount = value; }
        }

        /// <summary>
        /// Gets or sets disc.
        /// </summary>
        public override uint Disc
        {
            get { return Inner.Disc; }
            set { Inner.Disc = value; }
        }

        /// <summary>
        /// Gets or sets disc count.
        /// </summary>
        public override uint DiscCount
        {
            get { return Inner.DiscCount; }
            set { Inner.DiscCount = value; }
        }

        /// <summary>
        /// Gets or sets lyrics.
        /// </summary>
        public override string Lyrics
        {
            get { return Inner.Lyrics; }
            set { Inner.Lyrics = value; }
        }

        /// <summary>
        /// Gets or sets grouping.
        /// </summary>
        public override string Grouping
        {
            get { return Inner.Grouping; }
            set { Inner.Grouping = value; }
        }

        /// <summary>
        /// Gets or sets .
        /// </summary>
        public override uint BeatsPerMinute
        {
            get { return Inner.BeatsPerMinute; }
            set { Inner.BeatsPerMinute = value; }
        }

        /// <summary>
        /// Gets or sets conductor.
        /// </summary>
        public override string Conductor
        {
            get { return Inner.Conductor; }
            set { Inner.Conductor = value; }
        }

        /// <summary>
        /// Gets or sets copyright.
        /// </summary>
        public override string Copyright
        {
            get { return Inner.Copyright; }
            set { Inner.Copyright = value; }
        }

        /// <summary>
        /// Gets or sets music brainz artist id.
        /// </summary>
        public override string MusicBrainzArtistId
        {
            get { return Inner.MusicBrainzArtistId; }
            set { Inner.MusicBrainzArtistId = value; }
        }

        /// <summary>
        /// Gets or sets music brainz release id.
        /// </summary>
        public override string MusicBrainzReleaseId
        {
            get { return Inner.MusicBrainzReleaseId; }
            set { Inner.MusicBrainzReleaseId = value; }
        }

        /// <summary>
        /// Gets or sets music brainz release artist id.
        /// </summary>
        public override string MusicBrainzReleaseArtistId
        {
            get { return Inner.MusicBrainzReleaseArtistId; }
            set { Inner.MusicBrainzReleaseArtistId = value; }
        }

        /// <summary>
        /// Gets or sets music brainz track id.
        /// </summary>
        public override string MusicBrainzTrackId
        {
            get { return Inner.MusicBrainzTrackId; }
            set { Inner.MusicBrainzTrackId = value; }
        }

        /// <summary>
        /// Gets or sets music brainz disc id.
        /// </summary>
        public override string MusicBrainzDiscId
        {
            get { return Inner.MusicBrainzDiscId; }
            set { Inner.MusicBrainzDiscId = value; }
        }

        /// <summary>
        /// Gets or sets music ip id.
        /// </summary>
        public override string MusicIpId
        {
            get { return Inner.MusicIpId; }
            set { Inner.MusicIpId = value; }
        }

        /// <summary>
        /// Gets or sets amazon id.
        /// </summary>
        public override string AmazonId
        {
            get { return Inner.AmazonId; }
            set { Inner.AmazonId = value; }
        }

        /// <summary>
        /// Gets or sets music brainz release status.
        /// </summary>
        public override string MusicBrainzReleaseStatus
        {
            get { return Inner.MusicBrainzReleaseStatus; }
            set { Inner.MusicBrainzReleaseStatus = value; }
        }

        /// <summary>
        /// Gets or sets music brainz release type.
        /// </summary>
        public override string MusicBrainzReleaseType
        {
            get { return Inner.MusicBrainzReleaseType; }
            set { Inner.MusicBrainzReleaseType = value; }
        }

        /// <summary>
        /// Gets or sets music brainz release country.
        /// </summary>
        public override string MusicBrainzReleaseCountry
        {
            get { return Inner.MusicBrainzReleaseCountry; }
            set { Inner.MusicBrainzReleaseCountry = value; }
        }

        /// <summary>
        /// Gets or sets pictures.
        /// </summary>
        public override global::TagLib.IPicture[] Pictures
        {
            get { return Inner.Pictures; }
            set { Inner.Pictures = value; }
        }

        /// <summary>
        /// Indicates whether is empty.
        /// </summary>
        public override bool IsEmpty
        {
            get { return Inner.IsEmpty; }
        }

        /// <summary>
        /// Gets or sets artists.
        /// </summary>
        [Obsolete("For album artists use AlbumArtists. For track artists, use Performers")]
        public override string[] Artists
        {
            get { return Inner.Artists; }
            set { Inner.Artists = value; }
        }
        #endregion

        #region Methods Inner.*
        /// <summary>
        /// Wrapper for <see cref="CopyTo(TagLib.Tag, bool)"/>.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="overwrite">Whether should overwrite.</param>
        public override void CopyTo(global::TagLib.Tag target, bool overwrite)
        {
            Inner.CopyTo(target, overwrite);
        }

        /// <summary>
        /// Wrapper for <see cref="Clear"/>.
        /// </summary>
        public override void Clear()
        {
            Inner.Clear();
        }

        /// <summary>
        /// Wrapper for <see cref="ToString"/>.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Inner.ToString();
        }

        /// <summary>
        /// Wrapper for <see cref="Equals(object)"/>.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return Inner.Equals(obj);
        }

        /// <summary>
        /// Wrapper for <see cref="GetHashCode"/>.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Inner.GetHashCode();
        }
        #endregion
    }
}
