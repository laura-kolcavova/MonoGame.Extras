// -----------------------------------------------------------------------
// <copyright file="TextureAtlas.cs" company="Laura Kolcavova">
// Copyright (c) Laura Kolcavova. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace MonoGame.Extras.TextureAtlases
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Represents atlas of <see cref="TextureRegion2D"/> instances.
    /// </summary>
    public class TextureAtlas
    {
        private readonly Dictionary<string, TextureRegion2D> regionsByName;

        private readonly List<TextureRegion2D> regionsByIndex;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextureAtlas"/> class.
        /// </summary>
        /// <param name="name">A name of the atlas.</param>
        /// <param name="texture">A source <see cref="Texture2D"/> instance.</param>
        public TextureAtlas(string name, Texture2D texture)
        {
            this.Name = name;
            this.Texture = texture;

            this.regionsByName = new Dictionary<string, TextureRegion2D>();
            this.regionsByIndex = new List<TextureRegion2D>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextureAtlas"/> class.
        /// </summary>
        /// <param name="name">A name of the atlas.</param>
        /// <param name="texture">A source <see cref="Texture2D"/> instance.</param>
        /// <param name="regions">A collection of names as keys and bounding rectangles as values used to create <see cref="TextureRegion2D"/> instances.</param>
        public TextureAtlas(string name, Texture2D texture, Dictionary<string, Rectangle> regions)
           : this(name, texture)
        {
            foreach (var region in regions)
            {
                this.CreateRegion(region.Key, region.Value.X, region.Value.Y, region.Value.Width, region.Value.Height);
            }
        }

        /// <summary>
        /// Gets name of the atlas.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets source <see cref="Texture2D"/> instnace.
        /// </summary>
        public Texture2D Texture { get; }

        /// <summary>
        /// Gets <see cref="TextureRegion2D"/> instance from the atlas by specified name.
        /// </summary>
        /// <param name="name">The name of the texture region.</param>
        /// <returns>The <see cref="TextureRegion2D"/> instance.</returns>
        public TextureRegion2D this[string name] => this.GetRegion(name);

        /// <summary>
        /// Gets <see cref="TextureRegion2D"/> instance from the atlas by specified index.
        /// </summary>
        /// <param name="index">The index value.</param>
        /// <returns>The <see cref="TextureRegion2D"/> instance.</returns>
        public TextureRegion2D this[int index] => this.GetRegion(index);

        /// <summary>
        /// Creates new <see cref="TextureRegion2D"/> instance and adds this instance to the atlas.
        /// </summary>
        /// <param name="name">A name of the texture region.</param>
        /// <param name="x">A x position of the texture region.</param>
        /// <param name="y">An y position of the texture region.</param>
        /// <param name="width">A width of the texture region.</param>
        /// <param name="height">A height of the texture region.</param>
        /// <returns>New <see cref="TextureRegion2D"/> instance.</returns>
        public TextureRegion2D CreateRegion(string name, int x, int y, int width, int height)
        {
            if (this.regionsByName.ContainsKey(name))
            {
                throw new InvalidOperationException($"Region {name} already exists in the region sheet");
            }

            var region = new TextureRegion2D(name, this.Texture, x, y, width, height);
            this.AddRegion(region);
            return region;
        }

        /// <summary>
        /// Gets <see cref="TextureRegion2D"/> instance from the atlas by specified index.
        /// </summary>
        /// <param name="index">The index value.</param>
        /// <returns>The <see cref="TextureRegion2D"/> instance.</returns>
        public TextureRegion2D GetRegion(int index)
        {
            if (index < 0 || index > this.regionsByIndex.Count)
            {
                throw new IndexOutOfRangeException();
            }

            return this.regionsByIndex[index];
        }

        /// <summary>
        /// Gets <see cref="TextureRegion2D"/> instance from the atlas by specified name.
        /// </summary>
        /// <param name="name">The name of the texture region.</param>
        /// <returns>The <see cref="TextureRegion2D"/> instance.</returns>
        public TextureRegion2D GetRegion(string name)
        {
            if (this.regionsByName.TryGetValue(name, out TextureRegion2D region))
            {
                return region;
            }

            throw new KeyNotFoundException(name);
        }

        /// <summary>
        /// Adds given <see cref="TextureRegion2D"/> instance to the atlas.
        /// </summary>
        /// <param name="region">The <see cref="TextureRegion2D"/> instance.</param>
        private void AddRegion(TextureRegion2D region)
        {
            this.regionsByName.Add(region.Name, region);
            this.regionsByIndex.Add(region);
        }
    }
}
