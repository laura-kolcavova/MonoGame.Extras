// -----------------------------------------------------------------------
// <copyright file="GameTimeExtensions.cs" company="Laura Kolcavova">
// Copyright (c) Laura Kolcavova. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace MonoGame.Extras
{
    using Microsoft.Xna.Framework;

    /// <summary>
    /// A set of extensions methods for <see cref="GameTime"/> object..
    /// </summary>
    public static class GameTimeExtensions
    {
        /// <summary>
        /// Gets total elapsed seconds from <see cref="GameTime"/> instnace as float.
        /// </summary>
        /// <param name="gameTime">The GameTime instance.</param>
        /// <returns>Total elapsed seconds as float.</returns>
        public static float GetElapsedSeconds(this GameTime gameTime)
        {
            return (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        /// <summary>
        /// Gets total elapsed miliseconds from <see cref="GameTime"/> instnace as float.
        /// </summary>
        /// <param name="gameTime">The GameTime instance.</param>
        /// <returns>Total elapsed miliseconds as float.</returns>
        public static float GetElapsedMiliseconds(this GameTime gameTime)
        {
            return (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }
    }
}
