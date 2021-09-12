namespace MonoGame.Extras.Ecs.Systems
{
    using MonoGame.Extras.Ecs.Managers;
    using System.Collections.Generic;

    public abstract class EntitySystem : ISystem
    {
        private readonly AspectBuilder _aspectBuilder;

        protected EntitySystem(AspectBuilder aspectBuilder)
        {
            _aspectBuilder = aspectBuilder;
        }

        private World _world;
        private EntitySubscription _entitySubscription;

        protected IEnumerable<int> ActiveEntities => _entitySubscription.ActiveEntities;

        protected IEnumerable<int> Entities => _world.EntityManager.Entities;

        protected EntityGroupManager GroupManager => _world.GroupManager;

        protected virtual void OnEntityChanged(int entityId) { }
        protected virtual void OnEntityAdded(int entityId) { }
        protected virtual void OnEntityRemoved(int entityId) { }
        protected virtual void OnEntitySubscribed(int entityId) { }
        protected virtual void OnEntityUnsubsribed(int entityId) { }

        protected Entity GetEntity(int entityId)
            => _world.GetEntity(entityId);

        protected Entity CreateEntity()
            => _world.CreateEntity();

        protected void DestroyEntity(int entityId)
            => _world.DestroyEntity(entityId);

        public virtual void Dispose()
        {
            _entitySubscription.Dispose();

            _world.EntityManager.EntityAdded -= OnEntityAdded;
            _world.EntityManager.EntityChanged -= OnEntityChanged;
            _world.EntityManager.EntityRemoved -= OnEntityRemoved;
            _entitySubscription.EntitySubscribed -= OnEntitySubscribed;
            _entitySubscription.EntityUnsubscribed -= OnEntityUnsubsribed;
        }

        public virtual void Initialize(World world)
        {
            _world = world;

            var aspect = _aspectBuilder.Build(_world.ComponentManager);
            _entitySubscription = new EntitySubscription(_world.EntityManager, aspect);

            _world.EntityManager.EntityAdded += OnEntityAdded;
            _world.EntityManager.EntityChanged += OnEntityChanged;
            _world.EntityManager.EntityRemoved += OnEntityRemoved;
            _entitySubscription.EntitySubscribed += OnEntitySubscribed;
            _entitySubscription.EntityUnsubscribed += OnEntityUnsubsribed;

            Initialize(_world.ComponentMapperService);
        }

        public abstract void Initialize(IComponentMapperService componentService);
    }
}
