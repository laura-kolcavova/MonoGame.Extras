namespace MonoGame.Extras.Graphics.TextureAtlases
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;
    using System.Collections.Generic;

    public class TextureAtlas
    {
        #region Constructors

        public TextureAtlas(string name, Texture2D texture)
        {
            Name = name;
            Texture = texture;

            _regionsByName = new Dictionary<string, TextureRegion2D>();
            _regionsByIndex = new List<TextureRegion2D>();
        }

        public TextureAtlas(string name, Texture2D texture, Dictionary<string, Rectangle> regions)
           : this(name, texture)
        {
            foreach (var region in regions)
            {
                CreateRegion(region.Key, region.Value.X, region.Value.Y, region.Value.Width, region.Value.Height);
            }
        }

        #endregion

        #region Fields

        private readonly Dictionary<string, TextureRegion2D> _regionsByName;

        private readonly List<TextureRegion2D> _regionsByIndex;

        #endregion

        #region Properties

        public string Name { get; }

        public Texture2D Texture { get; }

        #endregion

        #region Methods

        public TextureRegion2D CreateRegion(string name, int x, int y, int width, int height)
        {
            if (_regionsByName.ContainsKey(name))
                throw new InvalidOperationException($"Region {name} already exists in the region sheet");

            var region = new TextureRegion2D(name, Texture, x, y, width, height);
            AddRegion(region);
            return region;
        }

        private void AddRegion(TextureRegion2D region)
        {
            _regionsByName.Add(region.Name, region);
            _regionsByIndex.Add(region);
        }

        public TextureRegion2D GetRegion(int index)
        {
            if (index < 0 || index > _regionsByIndex.Count)
                throw new IndexOutOfRangeException();

            return _regionsByIndex[index];
        }

        public TextureRegion2D GetRegion(string name)
        {
            if (_regionsByName.TryGetValue(name, out TextureRegion2D region))
                return region;

            throw new KeyNotFoundException(name);
        }

        #endregion

        #region Accessors

        public TextureRegion2D this[string name] => GetRegion(name);

        public TextureRegion2D this[int index] => GetRegion(index);

        #endregion
    }
}
