// -----------------------------------------------------------------------
// <copyright file="RectangleExtensions.cs" company="Laura Kolcavova">
// Copyright (c) Laura Kolcavova. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace MonoGame.Extras
{
    using Microsoft.Xna.Framework;

    /// <summary>
    /// A set of extension methods for <see cref="Rectangle"/> object.
    /// </summary>
    public static class RectangleExtensions
    {
        /// <summary>
        /// Checks if <see cref="Rectangle"/> object intersects given <see cref="Point"/> object.
        /// </summary>
        /// <param name="bounds">The <see cref="Rectangle"/> object.</param>
        /// <param name="point">The <see cref="Point"/> object.</param>
        /// <returns>True if the <see cref="Rectangle"/> objects intersects the <see cref="Point"/> object, otherwise False.</returns>
        public static bool Intersects(this Rectangle bounds, Point point)
        {
            return
                point.X <= bounds.Right &&
                point.Y <= bounds.Top &&
                bounds.Left <= point.X &&
                bounds.Bottom <= point.Y;
        }

        /// <summary>
        /// Creates a new <see cref="Rectangle"/> object by transforming the current <see cref="Rectangle"/> object by given <see cref="Matrix"/> object.
        /// </summary>
        /// <param name="rectangle">The <see cref="Rectangle"/> object.</param>
        /// <param name="matrix">The <see cref="Matrix"/> object.</param>
        /// <returns>New <see cref="Rectangle"/> objects which is transformation of the current <see cref="Rectangle"/> object by the <see cref="Matrix"/> object.</returns>
        public static Rectangle Transform(this Rectangle rectangle, Matrix matrix)
        {
            // Get all four corners in local space
            Vector2 leftTop = new Vector2(rectangle.Left, rectangle.Top);
            Vector2 rightTop = new Vector2(rectangle.Right, rectangle.Top);
            Vector2 leftBottom = new Vector2(rectangle.Left, rectangle.Bottom);
            Vector2 rightBottom = new Vector2(rectangle.Right, rectangle.Bottom);

            // Transform all four corners into work space
            Vector2.Transform(ref leftTop, ref matrix, out leftTop);
            Vector2.Transform(ref rightTop, ref matrix, out rightTop);
            Vector2.Transform(ref leftBottom, ref matrix, out leftBottom);
            Vector2.Transform(ref rightBottom, ref matrix, out rightBottom);

            // Find the minimum and maximum extents of the rectangle in world space
            var min = Vector2
                 .Min(Vector2.Min(leftTop, rightTop), Vector2.Min(leftBottom, rightBottom))
                 .ToPoint();

            var max = Vector2
                 .Max(Vector2.Max(leftTop, rightTop), Vector2.Max(leftBottom, rightBottom))
                 .ToPoint();

            // Return that as a rectangle
            return new Rectangle(min.X, min.Y, max.X - min.X, max.Y - min.Y);
        }
    }
}
