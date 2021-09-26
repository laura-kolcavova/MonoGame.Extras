namespace MonoGame.Extras.Ecs.Managers
{
    using MonoGame.Extras.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;

    public sealed class EntityGroupManager
    {
        private readonly EntityManager _entityManager;

        private readonly Dictionary<string, Bag<int>> _entitiesByGroup;
        private readonly Bag<string> _groupByEntity;
        private readonly Bag<int> _emptyBag;

        public EntityGroupManager(EntityManager entityManager)
        {
            _entityManager = entityManager;

            _entitiesByGroup = new Dictionary<string, Bag<int>>();
            _groupByEntity = new Bag<string>(_entityManager.Capacity);
            _emptyBag = new Bag<int>();

            _entityManager.EntityRemoved += OnEntityRemoved;
        }

        private void OnEntityRemoved(int entityId)
        {
            Remove(entityId);
        }

        public IEnumerable<int> GetEntities(string group)
        {
            Debug.Assert(!string.IsNullOrEmpty(group), "Group must not be null or empty.");

            if (!_entitiesByGroup.TryGetValue(group, out var entities))
            {
                entities = _emptyBag;
            }

            return entities;
        }

        public string GetGroupOf(int entityId)
        {
            if(entityId < _groupByEntity.Capacity)
            {
                return _groupByEntity[entityId];
            }

            return null;
        }

        public bool IsInGroup(int entityId)
        {
            return !string.IsNullOrEmpty(GetGroupOf(entityId));
        }

        public void Set(string group, int entityId)
        {
            Debug.Assert(!string.IsNullOrEmpty(group), "Group must not be null or empty.");

            Remove(entityId);

            if (!_entitiesByGroup.TryGetValue(group, out var entities))
            {
                entities = new Bag<int>();
                _entitiesByGroup.Add(group, entities);
            }

            entities.Add(entityId);
            _groupByEntity[entityId] = group;
        }

        public void Remove(int entityId)
        {
            string group = GetGroupOf(entityId);

            if (!string.IsNullOrEmpty(group))
            {
                _groupByEntity[entityId] = null;

                if (_entitiesByGroup.TryGetValue(group, out var entities))
                {
                    int index = entities.IndexOf(entityId);
                    entities.Remove(index);
                }
            }
        }
    }
}
