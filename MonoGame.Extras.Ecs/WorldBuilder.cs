namespace MonoGame.Extras.Ecs
{
    using MonoGame.Extras.Ecs.Systems;
    using System.Collections.Generic;

    public sealed class WorldBuilder
    {
        private readonly IList<ISystem> _systems;

        public WorldBuilder()
        {
            _systems = new List<ISystem>();
        }

        public WorldBuilder AddSystem(ISystem system)
        {
            _systems.Add(system);
            return this;
        }

        public World Build()
        {
            var world = new World();

            foreach (var system in _systems)
            {
                world.RegisterSystem(system);
            }

            return world;
        }
    }
}
