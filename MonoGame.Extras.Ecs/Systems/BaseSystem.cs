namespace MonoGame.Extras.Ecs.Systems
{
    using MonoGame.Extras.Ecs.Managers;
    using System.Collections.Generic;

    public abstract class BaseSystem : ISystem
    {
        protected BaseSystem()
        {
        }

        private World _world;

        protected IEnumerable<int> Entities => _world.EntityManager.Entities;

        protected EntityGroupManager GroupManager => _world.GroupManager;

        protected virtual void OnEntityChanged(int entityId) { }
        protected virtual void OnEntityAdded(int entityId) { }
        protected virtual void OnEntityRemoved(int entityId) { }

        protected Entity GetEntity(int entityId)
            => _world.GetEntity(entityId);

        protected Entity CreateEntity()
            => _world.CreateEntity();

        protected void DestroyEntity(int entityId)
            => _world.DestroyEntity(entityId);

        public virtual void Dispose()
        {
            _world.EntityManager.EntityAdded -= OnEntityAdded;
            _world.EntityManager.EntityChanged -= OnEntityChanged;
            _world.EntityManager.EntityRemoved -= OnEntityRemoved;
        }

        public virtual void Initialize(World world)
        {
            _world = world;

            _world.EntityManager.EntityAdded += OnEntityAdded;
            _world.EntityManager.EntityChanged += OnEntityChanged;
            _world.EntityManager.EntityRemoved += OnEntityRemoved;

            Initialize(_world.ComponentMapperService);
        }

        public abstract void Initialize(IComponentMapperService componentService);
    }
}
