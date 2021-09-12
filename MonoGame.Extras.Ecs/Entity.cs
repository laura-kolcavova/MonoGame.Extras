namespace MonoGame.Extras.Ecs
{
    using DotNet.Extras.Collections;
    using MonoGame.Extras.Ecs.Managers;

    public sealed class Entity
    {
        private readonly EntityManager _entityManager;
        private readonly ComponentManager _componentManager;
        private readonly EntityGroupManager _groupManager;

        public Entity(int id, World world)
        {
            Id = id;
            _entityManager = world.EntityManager;
            _componentManager = world.ComponentManager;
            _groupManager = world.GroupManager;
        }

        public readonly int Id;

        public BitArray64 ComponentBits => _entityManager.GetComponentBits(Id);

        public string Group
        {
            get => _groupManager.GetGroupOf(Id);
            set => _groupManager.Set(value, Id);
        }

        public T AttachComponent<T>(T component) where T : IEntityComponent
        {
            _componentManager.GetMapper<T>().Put(Id, component);
            return component;
        }

        public void DettachComponent<T>() where T : IEntityComponent
        {
            _componentManager.GetMapper<T>().Remove(Id);
        }

        public T GetComponent<T>() where T : IEntityComponent
        {
            var component = _componentManager.GetMapper<T>().Get(Id);
            return component;
        }

        public bool HasComponent<T>() where T : IEntityComponent
        {
            return _componentManager.GetMapper<T>().Has(Id);
        }

        public void Destroy()
        {
            _entityManager.Destroy(Id);
        }
    }
}
