namespace MonoGame.Extras.Ecs.Systems
{
    using Microsoft.Xna.Framework;

    public interface IDrawSystem : ISystem
    {
        void Draw(GameTime gameTime);
    }
}
