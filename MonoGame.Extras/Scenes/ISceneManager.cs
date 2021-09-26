// -----------------------------------------------------------------------
// <copyright file="ISceneManager.cs" company="Laura Kolcavova">
// Copyright (c) Laura Kolcavova. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace MonoGame.Extras.Scenes
{
    /// <summary>
    /// An interface for <see cref="SceneManager"/> instance.
    /// </summary>
    public interface ISceneManager
    {
        /// <summary>
        /// Gets active scene.
        /// </summary>
        IScene ActiveScene { get; }

        /// <summary>
        /// Initilize and loads given scene and sets the scene as active scene.
        /// </summary>
        /// <param name="scene">The scene instance.</param>
        void LoadScene(IScene scene);
    }
}
