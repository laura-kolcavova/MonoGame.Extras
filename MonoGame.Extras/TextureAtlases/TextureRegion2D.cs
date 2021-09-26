// -----------------------------------------------------------------------
// <copyright file="TextureRegion2D.cs" company="Laura Kolcavova">
// Copyright (c) Laura Kolcavova. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace MonoGame.Extras.TextureAtlases
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Represents 2d texture region.
    /// </summary>
    public class TextureRegion2D
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextureRegion2D"/> class.
        /// </summary>
        /// <param name="name">A name of texture.</param>
        /// <param name="texture">A <see cref="Texture2D"/> instance.</param>
        /// <param name="x">A x position of the region.</param>
        /// <param name="y">An y position of the region.</param>
        /// <param name="width">A width of the region.</param>
        /// <param name="height">A height of the region.</param>
        public TextureRegion2D(string name, Texture2D texture, int x, int y, int width, int height)
        {
            this.Name = name;
            this.Texture = texture;
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextureRegion2D"/> class.
        /// </summary>
        /// <param name="name">A name of texture.</param>
        /// <param name="texture">A <see cref="Texture2D"/> instance.</param>
        /// <param name="bounds">A bounding rectangle of the region.</param>
        public TextureRegion2D(string name, Texture2D texture, Rectangle bounds)
            : this(name, texture, bounds.X, bounds.Y, bounds.Width, bounds.Height)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextureRegion2D"/> class.
        /// </summary>
        /// <param name="texture">A <see cref="Texture2D"/> instance.</param>
        public TextureRegion2D(Texture2D texture)
            : this(texture.Name, texture, texture.Bounds)
        {
        }

        /// <summary>
        /// Gets name of the region.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets <see cref="Texture2D"/> instance.
        /// </summary>
        public Texture2D Texture { get; }

        /// <summary>
        /// Gets x position of the region.
        /// </summary>
        public int X { get; }

        /// <summary>
        /// Gets y position of the region.
        /// </summary>
        public int Y { get; }

        /// <summary>
        /// Gets width of the region.
        /// </summary>
        public int Width { get; }

        /// <summary>
        /// Gets height of the region.
        /// </summary>
        public int Height { get; }

        /// <summary>
        /// Gets new created <see cref="Rectangle"/> object which represents bounding rectangle of the region.
        /// </summary>
        public Rectangle Bounds => new Rectangle(this.X, this.Y, this.Width, this.Height);

        /// <summary>
        /// Gets new created <see cref="Point"/> object which represents size of the region.
        /// </summary>
        public Point Size => new Point(this.Width, this.Height);
    }
}
