// -----------------------------------------------------------------------
// <copyright file="SceneManager.cs" company="Laura Kolcavova">
// Copyright (c) Laura Kolcavova. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace MonoGame.Extras.Scenes
{
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Represents a manager for <see cref="IScene"/> instances.
    /// </summary>
    public class SceneManager : DrawableGameComponent, ISceneManager
    {
        private IScene activeScene;
        private IScene prevScene;

        /// <summary>
        /// Initializes a new instance of the <see cref="SceneManager"/> class.
        /// </summary>
        /// <param name="game">A <see cref="Game"/> instance.</param>
        public SceneManager(Game game)
            : base(game)
        {
            activeScene = null;
            prevScene = null;
        }

        /// <inheritdoc/>
        public IScene ActiveScene => activeScene;

        /// <inheritdoc/>
        public void LoadScene(IScene scene)
        {
            if (activeScene != null)
            {
                prevScene = activeScene;
                prevScene.Dispose();
                prevScene.UnloadContent();
            }

            activeScene = scene;

            activeScene.LoadContent();
        }

        /// <inheritdoc/>
        public override void Update(GameTime gameTime)
        {
            activeScene.Update(gameTime);
            base.Update(gameTime);
        }

        /// <inheritdoc/>
        public override void Draw(GameTime gameTime)
        {
            activeScene.Draw(gameTime);
            base.Draw(gameTime);
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            activeScene.Dispose();
            activeScene.UnloadContent();
        }
    }
}
