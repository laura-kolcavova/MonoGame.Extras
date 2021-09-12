namespace MonoGame.Extras
{
    using Microsoft.Xna.Framework;

    public static class GameTimeExtensions
    {
        public static float GetElapsedSeconds(this GameTime gameTime)
        {
            return (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public static float GetElapsedMiliseconds(this GameTime gameTime)
        {
            return (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }
    }
}
