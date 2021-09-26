// -----------------------------------------------------------------------
// <copyright file="IScene.cs" company="Laura Kolcavova">
// Copyright (c) Laura Kolcavova. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace MonoGame.Extras.Scenes
{
    using System;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// An interface for game scene used by <see cref="SceneManager"/> instance.
    /// </summary>
    public interface IScene : IDisposable
    {
        /// <summary>
        /// Load content resources used by this scene.
        /// </summary>
        void LoadContent();

        /// <summary>
        /// Unload content resources used by this scene.
        /// </summary>
        void UnloadContent();

        /// <summary>
        /// Updates scene.
        /// </summary>
        /// <param name="gameTime">A game time state.</param>
        void Update(GameTime gameTime);

        /// <summary>
        /// Draws scene.
        /// </summary>
        /// <param name="gameTime">A game time state.</param>
        void Draw(GameTime gameTime);
    }
}
