namespace MonoGame.Extras.Ecs
{
    using Microsoft.Xna.Framework;
    using MonoGame.Extras.Ecs.Managers;
    using MonoGame.Extras.Ecs.Systems;
    using System;

    public sealed class World : IDisposable
    {
        private readonly EntityManager _entityManager;
        private readonly EntityGroupManager _groupManager;
        private readonly SystemManager _systemManager;

        internal World()
        {
            _entityManager = new EntityManager(this);
            _groupManager = new EntityGroupManager(_entityManager);
            _systemManager = new SystemManager();
        }

        internal EntityManager EntityManager => _entityManager;

        internal ComponentManager ComponentManager => _entityManager.ComponentManager;

        public IComponentMapperService ComponentMapperService => _entityManager.ComponentManager;

        public EntityGroupManager GroupManager => _groupManager;

        internal void RegisterSystem(ISystem system)
        {
            _systemManager.Add(system);

            system.Initialize(this);
        }

        public Entity GetEntity(int entityId)
        {
            return _entityManager.GetEntity(entityId);
        }

        public Entity CreateEntity()
        {
            return _entityManager.Create();
        }

        public void DestroyEntity(int entityId)
        {
            _entityManager.Destroy(entityId);
        }

        public void Dispose()
        {
            _systemManager.Dispose();
        }

        public void Update(GameTime gameTime)
        {
            _entityManager.Update(gameTime);
            _systemManager.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            _systemManager.Draw(gameTime);
        }
    }
}
