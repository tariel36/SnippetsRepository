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

using System.Collections.Generic;

namespace NutaDev.CsLib.Gaming.Framework.TextureAtlases.Models.Specific
{
    /// <summary>
    /// Texture atlas.
    /// </summary>
    public class TextureAtlas
    {
        /// <summary>
        /// Backing field to <see cref="Textures"/>.
        /// </summary>
        private List<SubTexture> _textures;

        /// <summary>
        /// Backing filed for <see cref="Items"/>.
        /// </summary>
        private readonly Dictionary<string, SubTexture> _items;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextureAtlas"/> class.
        /// </summary>
        public TextureAtlas()
        {
            _items = new Dictionary<string, SubTexture>();
        }

        /// <summary>
        /// Gets or sets atlas name.
        /// </summary>
        public string AtlasKey { get; set; }

        /// <summary>
        /// Gets or sets name of texture.
        /// </summary>
        public string TextureKey { get; set; }

        /// <summary>
        /// Gets or sets path to texture.
        /// </summary>
        public string FilePath { get; set; }
        
        /// <summary>
        /// Gets or sets collection of subtextures.
        /// </summary>
        public List<SubTexture> Textures
        {
            get { return _textures; }
            set
            {
                _textures = value;

                PrepareItemsDictionary();
            }
        }

        /// <summary>
        /// Gets dictionary of subtextures.
        /// </summary>
        private Dictionary<string, SubTexture> Items
        {
            get
            {
                PrepareItemsDictionary();
                return _items;
            }
        }

        /// <summary>
        /// Returns subtexture or default value.
        /// </summary>
        /// <param name="key">Key of subtexture.</param>
        /// <returns>Subtexture or null.</returns>
        public SubTexture GetItem(string key)
        {
            if (Items.ContainsKey(key))
            {
                return Items[key];
            }

            return default(SubTexture);
        }

        /// <summary>
        /// Prepares the lookup dictionary.
        /// </summary>
        private void PrepareItemsDictionary()
        {
            _items.Clear();

            foreach (SubTexture texture in _textures)
            {
                _items[texture.Key] = texture;
            }
        }
    }
}
