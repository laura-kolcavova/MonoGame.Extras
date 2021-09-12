namespace MonoGame.Extras.Scenes
{
    using Microsoft.Xna.Framework;
    using System;

    public interface IScene : IDisposable
    {
        void LoadContent();
        void UnloadContent();
        void Update(GameTime gameTime);
        void Draw(GameTime gameTime);
    }
}
