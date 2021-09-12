namespace MonoGame.Extras.Ecs.Systems
{
    using Microsoft.Xna.Framework;

    public interface IUpdateSystem : ISystem
    {
        void Update(GameTime gameTime);
    }
}
