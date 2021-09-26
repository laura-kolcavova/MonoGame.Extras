// -----------------------------------------------------------------------
// <copyright file="MatrixExtensions.cs" company="Laura Kolcavova">
// Copyright (c) Laura Kolcavova. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace MonoGame.Extras
{
    using System;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// A set of extension methods for <see cref="Matrix"/> object.
    /// </summary>
    public static class MatrixExtensions
    {
        /// <summary>
        /// Decompose the current <see cref="Matrix"/> object.
        /// </summary>
        /// <param name="matrix">The <see cref="Matrix"/> object.</param>
        /// <param name="position">Decomposed matrix position.</param>
        /// <param name="rotation">Decomposed matrix rotation.</param>
        /// <param name="scale">Decomposed matrix scale.</param>
        public static void Decompose(this Matrix matrix, out Vector2 position, out float rotation, out Vector2 scale)
        {
            matrix.Decompose(out var scale3, out var rotationQ, out var position3);

            Vector2 direction = Vector2.Transform(Vector2.UnitX, rotationQ);
            rotation = (float)Math.Atan2(direction.Y, direction.X);
            position = new Vector2(position3.X, position3.Y);
            scale = new Vector2(scale3.X, scale3.Y);
        }
    }
}
