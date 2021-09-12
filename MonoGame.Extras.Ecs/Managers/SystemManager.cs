namespace MonoGame.Extras.Ecs.Managers
{
    using DotNet.Extras.Collections;
    using Microsoft.Xna.Framework;
    using MonoGame.Extras.Ecs.Systems;
    using System;

    public sealed class SystemManager : IDisposable
    {
        private readonly Bag<ISystem> _systems;
        private readonly Bag<IUpdateSystem> _updateSystems;
        private readonly Bag<IDrawSystem> _drawSystems;

        public SystemManager()
        {
            _systems = new Bag<ISystem>();
            _updateSystems = new Bag<IUpdateSystem>();
            _drawSystems = new Bag<IDrawSystem>();
        }

        public void Dispose()
        {
            foreach(var system in _systems)
            {
                system.Dispose();
            }

            _systems.Clear();
            _updateSystems.Clear();
            _drawSystems.Clear();
        }

        public void Add(ISystem system)
        {
            _systems.Add(system);

            if(system is IUpdateSystem)
            {
                _updateSystems.Add(system as IUpdateSystem);
            }

            if(system is IDrawSystem)
            {
                _drawSystems.Add(system as IDrawSystem);
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (var updateSystem in _updateSystems)
                updateSystem.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            foreach (var drawSystem in _drawSystems)
                drawSystem.Draw(gameTime);
        }


    }
}
