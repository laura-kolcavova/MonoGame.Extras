namespace MonoGame.Extras.Ecs.Systems
{
    using System;

    public interface ISystem : IDisposable
    {
        void Initialize(World world);
    }
}
