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

using NutaDev.CsLib.Types.RegularExpressions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NutaDev.CsLib.Audio.Formats.Cue
{
    /// <summary>
    /// Represents a Cue file with metadata.
    /// </summary>
    public class CueFile
    {
        /// <summary>
        /// Keyword tag.
        /// </summary>
        private const string KeyKeyword = "KEYWORD";

        /// <summary>
        /// Value tag.
        /// </summary>
        private const string KeyValue = "VALUE";

        /// <summary>
        /// Header indent.
        /// </summary>
        private const string HeaderIndent = "";

        /// <summary>
        /// Start of file.
        /// </summary>
        private const string FileStart = "FILE";

        /// <summary>
        /// Start of track.
        /// </summary>
        private const string TrackStart = "  TRACK";

        /// <summary>
        /// Property indent.
        /// </summary>
        private const string TrackPropertyIndent = "    ";

        /// <summary>
        /// Regex for header.
        /// </summary>
        private static readonly Regex HeaderRegex = new Regex("^(?\'KEYWORD\'(?\'GENRE\'REM GENRE)|(?\'DISCID\'REM DISCID)|(?\'CATALOG\'CATALOG)|(?\'COMMENT\'REM COMMENT)|(?\'DATE\'REM DATE)|(?\'PERFORMER\'PERFORMER)|(?\'TITLE\'TITLE)|(?\'COMPOSER\'REM COMPOSER)) (?\'VALUE\'\"?.*\"?)", RegexOptions.Compiled);

        /// <summary>
        /// Regex for disc within cue file.
        /// </summary>
        private static readonly Regex FileRegex = new Regex("^(?\'KEYWORD\'FILE) (?\'PATH\'\\\".*\\\") (?\'TYPE\'.*)", RegexOptions.Compiled);

        /// <summary>
        /// Regex for track header.
        /// </summary>
        private static readonly Regex TrackHeaderRegex = new Regex("^\\s+(?\'KEYWORD\'TRACK) (?\'IDX\'[0-9]+) (?\'TYPE\'.*)", RegexOptions.Compiled);

        /// <summary>
        /// Regex for track properties.
        /// </summary>
        private static readonly Regex TrackPropertiesRegex = new Regex("^\\s+(?\'KEYWORD\'(TITLE|PERFORMER|REM COMPOSER|ISRC|INDEX)) ((?\'IDX\'[0-9]+) (?\'TIME\'[0-9]+:[0-9]+:[0-9]+)|(?\'VALUE\'.*))", RegexOptions.Compiled);

        /// <summary>
        /// Regex for time.
        /// </summary>
        private static readonly Regex TimeRegex = new Regex("(?\'MIN\'[0-9]+):(?\'SEC\'[0-9]+):(?\'MS\'[0-9]+)");

        /// <summary>
        /// Regex for key and value pair.
        /// </summary>
        private static readonly Regex KeyValueRegex = new Regex("^\\s*(?\'KEYWORD\'.*) (?\'VALUE\'.*)", RegexOptions.Compiled);

        /// <summary>
        /// Initializes a new instance of the <see cref="CueFile"/> class.
        /// </summary>
        public CueFile()
        {
            Discs = new List<CueDisc>();
            Properties = new List<CueCustomProperty>();
        }

        /// <summary>
        /// Gets or sets path to file.
        /// </summary>
        public string Path { get; private set; }

        /// <summary>
        /// Gets or sets genre of file.
        /// </summary>
        public string Genre { get; private set; }

        /// <summary>
        /// Gets date of file.
        /// </summary>
        public DateTime Date { get { return new DateTime(Year, Month, Day); } }

        /// <summary>
        /// Gets or sets year within file.
        /// </summary>
        public int Year { get; private set; }

        /// <summary>
        /// Gets or sets month withi file.
        /// </summary>
        public int Month { get; private set; }

        /// <summary>
        /// Gets or sets day within file.
        /// </summary>
        public int Day { get; private set; }

        /// <summary>
        /// Gets or sets id of disc.
        /// </summary>
        public string DiscId { get; private set; }

        /// <summary>
        /// Gets or sets comment within cue file.
        /// </summary>
        public string Comment { get; private set; }

        /// <summary>
        /// Gets or sets cue file catalog.
        /// </summary>
        public string Catalog { get; set; }

        /// <summary>
        /// Gets or sets the performer.
        /// </summary>
        public string Performer { get; set; }

        /// <summary>
        /// Gets or sets title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the composer.
        /// </summary>
        public string Composer { get; set; }

        /// <summary>
        /// Gets or sets the collection of discs within cue file.
        /// </summary>
        public IReadOnlyCollection<CueDisc> Discs { get; private set; }

        /// <summary>
        /// Gets or sets the properties of cue file.
        /// </summary>
        public IReadOnlyCollection<CueCustomProperty> Properties { get; private set; }

        /// <summary>
        /// Loads cue file.
        /// </summary>
        /// <param name="path"></param>
        public void Load(string path)
        {
            Path = path;

            Parse(System.IO.File.ReadAllLines(path));
        }

        /// <summary>
        /// Parses the file.
        /// </summary>
        /// <param name="lines">Lines of file.</param>
        private void Parse(string[] lines)
        {
            if (lines == null || lines.Length == 0) { return; }

            List<CueTrack> tracks = new List<CueTrack>();
            List<CueIndex> indexes = new List<CueIndex>();
            List<CueDisc> files = new List<CueDisc>();
            List<CueCustomProperty> fileCustomProperties = new List<CueCustomProperty>();
            List<CueCustomProperty> trackCustomProperties = new List<CueCustomProperty>();
            List<CueCustomProperty> customProperties = new List<CueCustomProperty>();

            Properties = customProperties;

            CueDisc currentFile = null;
            CueTrack currentTrack = null;

            for (int i = 0; i < lines.Length; ++i)
            {
                string line = lines[i];

                if (!string.IsNullOrWhiteSpace(line))
                {
                    bool isHeader = line.StartsWith(HeaderIndent);
                    bool isFile = line.StartsWith(FileStart);
                    bool isTrackHeader = line.StartsWith(TrackStart);
                    bool isTrackProperties = line.StartsWith(TrackPropertyIndent);

                    if (isHeader && (isFile || isTrackHeader || isTrackProperties))
                    {
                        isHeader = false;
                    }

                    if (isHeader)
                    {
                        Match match = HeaderRegex.Match(line);

                        if (match.Success)
                        {
                            switch (match.GetGroupValue(KeyKeyword))
                            {
                                case "REM GENRE":
                                {
                                    Genre = ClearString(match.GetGroupValue(KeyValue));
                                    break;
                                }
                                case "REM DATE":
                                {
                                    string date = ClearString(match.GetGroupValue(KeyValue));

                                    if (DateTime.TryParse(date, out DateTime dt))
                                    {
                                        Year = dt.Year;
                                        Month = dt.Month;
                                        Day = dt.Day;
                                    }
                                    else if (int.TryParse(date, out int iVal))
                                    {
                                        if (iVal > 31)
                                        {
                                            Year = iVal;
                                        }
                                        else if (iVal > 12)
                                        {
                                            Day = iVal;
                                        }
                                        else
                                        {
                                            Month = iVal;
                                        }
                                    }

                                    break;
                                }
                                case "REM DISCID":
                                {
                                    DiscId = ClearString(match.GetGroupValue(KeyValue));
                                    break;
                                }
                                case "REM COMMENT":
                                {
                                    Comment = ClearString(match.GetGroupValue(KeyValue));
                                    break;
                                }
                                case "CATALOG":
                                {
                                    Catalog = ClearString(match.GetGroupValue(KeyValue));
                                    break;
                                }
                                case "PERFORMER":
                                {
                                    Performer = ClearString(match.GetGroupValue(KeyValue));
                                    break;
                                }
                                case "TITLE":
                                {
                                    Title = ClearString(match.GetGroupValue(KeyValue));
                                    break;
                                }
                                case "REM COMPOSER":
                                {
                                    Composer = ClearString(match.GetGroupValue(KeyValue));
                                    break;
                                }
                            }
                        }
                        else
                        {
                            CreateCustomProperty(customProperties, line);
                        }
                    }
                    else if (isFile)
                    {
                        Match match = FileRegex.Match(line);

                        if (match.Success)
                        {
                            if (currentFile != null)
                            {
                                files.Add(currentFile);
                            }

                            tracks = new List<CueTrack>();
                            fileCustomProperties = new List<CueCustomProperty>();

                            currentFile = new CueDisc();
                            currentFile.Tracks = tracks;
                            currentFile.Properties = fileCustomProperties;

                            currentFile.DiscName = match.GetGroupValue("PATH");
                            currentFile.DiscType = match.GetGroupValue("TYPE");
                        }
                    }
                    else if (isTrackHeader)
                    {
                        Match match = TrackHeaderRegex.Match(line);

                        if (match.Success)
                        {
                            if (currentTrack != null)
                            {
                                tracks.Add(currentTrack);
                            }

                            indexes = new List<CueIndex>();
                            trackCustomProperties = new List<CueCustomProperty>();

                            currentTrack = new CueTrack();
                            currentTrack.Indexes = indexes;
                            currentTrack.Properties = trackCustomProperties;

                            currentTrack.Index = int.Parse(match.GetGroupValue("IDX"));
                            currentTrack.Type = match.GetGroupValue("TYPE");
                        }
                    }
                    else if (isTrackProperties)
                    {
                        Match match = TrackPropertiesRegex.Match(line);

                        if (match.Success && currentTrack != null)
                        {
                            switch (match.GetGroupValue(KeyKeyword))
                            {
                                case "TITLE":
                                {
                                    currentTrack.Title = ClearString(match.GetGroupValue(KeyValue));
                                    break;
                                }
                                case "PERFORMER":
                                {
                                    currentTrack.Performer = ClearString(match.GetGroupValue(KeyValue));
                                    break;
                                }
                                case "REM COMPOSER":
                                {
                                    currentTrack.Composer = ClearString(match.GetGroupValue(KeyValue));
                                    break;
                                }
                                case "ISRC":
                                {
                                    currentTrack.ISRC = ClearString(match.GetGroupValue(KeyValue));
                                    break;
                                }
                                case "INDEX":
                                {
                                    string timeStr = ClearString(match.GetGroupValue("TIME"));
                                    Match timeMatch = TimeRegex.Match(timeStr);

                                    indexes.Add(new CueIndex()
                                    {
                                        Index = int.Parse(ClearString(match.GetGroupValue("IDX"))),
                                        Time = new TimeSpan(0, 0, int.Parse(timeMatch.GetGroupValue("MIN")), int.Parse(timeMatch.GetGroupValue("SEC")), int.Parse(timeMatch.GetGroupValue("MS").PadRight(3, '0')))
                                    });
                                    break;
                                }
                            }
                        }
                        else if (currentTrack != null)
                        {
                            CreateCustomProperty(trackCustomProperties, line);
                        }
                    }
                }
            }

            if (currentFile != null)
            {
                files.Add(currentFile);

                if (currentTrack != null)
                {
                    if (!currentFile.Tracks.Contains(currentTrack))
                    {
                        tracks.Add(currentTrack);
                    }
                }
            }

            Discs = files;
        }

        /// <summary>
        /// Clears string form useless characters.
        /// </summary>
        /// <param name="str">String to clear.</param>
        /// <returns>Clear string.</returns>
        private string ClearString(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return str;
            }

            if (str.StartsWith("\"") && str.EndsWith("\""))
            {
                return str.Substring(1, str.Length - 2);
            }

            return str;
        }

        /// <summary>
        /// Creates custom property and adds it to list.
        /// </summary>
        /// <param name="list">List with properties.</param>
        /// <param name="line">Parses the line.</param>
        private void CreateCustomProperty(List<CueCustomProperty> list, string line)
        {
            Match match = KeyValueRegex.Match(line);

            if (match.Success)
            {
                list.Add(new CueCustomProperty()
                {
                    Key = match.GetGroupValue(KeyKeyword),
                    Value = match.GetGroupValue(KeyValue)
                });
            }
        }
    }
}
