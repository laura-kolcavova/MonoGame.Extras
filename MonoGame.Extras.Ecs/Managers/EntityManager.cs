namespace MonoGame.Extras.Ecs.Managers
{
    using Microsoft.Xna.Framework;
    using MonoGame.Extras.Collections;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public sealed class EntityManager
    {
        private const int _defaultBagSize = 128;

        private readonly World _world;

        private readonly Bag<Entity> _entities;
        private readonly Bag<int> _addedEntities;
        private readonly Bag<int> _changedEntities;
        private readonly Bag<int> _removedEntities;
        private readonly Bag<BitArray64> _entityToComponentBits;

        private int _nextId;

        public EntityManager(World world)
        {
            _world = world;
            ComponentManager = new ComponentManager();

            _entities = new Bag<Entity>(_defaultBagSize);
            _addedEntities = new Bag<int>(_defaultBagSize);
            _changedEntities = new Bag<int>(_defaultBagSize);
            _removedEntities = new Bag<int>(_defaultBagSize);
            _entityToComponentBits = new Bag<BitArray64>(_defaultBagSize);

            ComponentManager.ComponentsChanged += OnComponentsChanged;

            _nextId = 0;
        }

        internal ComponentManager ComponentManager { get; }

        public event Action<int> EntityAdded;
        public event Action<int> EntityChanged;
        public event Action<int> EntityRemoved;

        public int Capacity => _entities.Capacity;

        public IEnumerable<int> Entities => _entities.Where(e => e != null).Select(e => e.Id);

        private void OnComponentsChanged(int entityId)
        {
            if (!_changedEntities.Contains(entityId))
                _changedEntities.Add(entityId);
        }

        public Entity GetEntity(int entityId)
        {
            return _entities[entityId];
        }

        public Entity Create()
        {
            int id = _nextId;
            _nextId++;

            var entity = new Entity(id, _world);

            _entities[id] = entity;

            _addedEntities.Add(id);

            return entity;
        }

        public void Destroy(int entityId)
        {
            if (!_removedEntities.Contains(entityId))
                _removedEntities.Add(entityId);
        }

        public BitArray64 GetComponentBits(int entityId)
        {
            return _entityToComponentBits[entityId];
        }

        public void Update(GameTime gameTime)
        {
            if (gameTime == null)
                throw new ArgumentNullException(nameof(gameTime));

            foreach (int entityId in _addedEntities)
            {
                EntityAdded?.Invoke(entityId);
            }

            foreach (int entityId in _changedEntities)
            {
                _entityToComponentBits[entityId] = ComponentManager.CreateComponentBits(entityId);
                EntityChanged?.Invoke(entityId);
            }

            foreach (int entityId in _removedEntities)
            {
                _entities[entityId] = null;
                _entityToComponentBits[entityId] = default;
                ComponentManager.DestroyComponents(entityId);

                EntityRemoved?.Invoke(entityId);
            }

            _addedEntities.Clear();
            _changedEntities.Clear();
            _removedEntities.Clear();
        }
    }
}
