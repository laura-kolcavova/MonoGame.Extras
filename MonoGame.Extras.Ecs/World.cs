namespace MonoGame.Extras.Ecs
{
    using Microsoft.Xna.Framework;
    using MonoGame.Extras.Ecs.Managers;
    using MonoGame.Extras.Ecs.Systems;
    using System;

    public sealed class World : IDisposable
    {
        private readonly SystemManager _systemManager;

        internal World()
        {
            EntityManager = new EntityManager(this);
            GroupManager = new EntityGroupManager(EntityManager);
            _systemManager = new SystemManager();
        }

        internal EntityManager EntityManager { get; }

        internal ComponentManager ComponentManager => EntityManager.ComponentManager;

        public IComponentMapperService ComponentMapperService => EntityManager.ComponentManager;

        public EntityGroupManager GroupManager { get; }

        internal void RegisterSystem(ISystem system)
        {
            _systemManager.Add(system);

            system.Initialize(this);
        }

        public Entity GetEntity(int entityId)
        {
            return EntityManager.GetEntity(entityId);
        }

        public Entity CreateEntity()
        {
            return EntityManager.Create();
        }

        public void DestroyEntity(int entityId)
        {
            EntityManager.Destroy(entityId);
        }

        public void Dispose()
        {
            _systemManager.Dispose();
        }

        public void Update(GameTime gameTime)
        {
            EntityManager.Update(gameTime);
            _systemManager.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            _systemManager.Draw(gameTime);
        }
    }
}
