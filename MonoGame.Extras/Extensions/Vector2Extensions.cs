// -----------------------------------------------------------------------
// <copyright file="Vector2Extensions.cs" company="Laura Kolcavova">
// Copyright (c) Laura Kolcavova. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace MonoGame.Extras
{
    using Microsoft.Xna.Framework;

    /// <summary>
    /// A set of extension methods for <see cref="Vector2"/> object.
    /// </summary>
    public static class Vector2Extensions
    {
        /// <summary>
        /// Creates new <see cref="Vector2"/> object which is moved  from the current <see cref="Vector2"/> object towards given <see cref="Vector2"/> target object by specified maximal distance delta.
        /// </summary>
        /// <param name="current">The current <see cref="Vector2"/> object.</param>
        /// <param name="target">The target <see cref="Vector2"/> object.</param>
        /// <param name="maxDistanceDelta">The maximal distance delta as float.</param>
        /// <returns>New <see cref="Vector2"/> object.</returns>
        public static Vector2 MoveTowards(this Vector2 current, Vector2 target, float maxDistanceDelta)
        {
            Vector2 a = target - current;
            float magnitude = a.Length();

            if (magnitude <= maxDistanceDelta || magnitude == 0f)
            {
                return target;
            }

            return current + (a / magnitude * maxDistanceDelta);
        }
    }
}
