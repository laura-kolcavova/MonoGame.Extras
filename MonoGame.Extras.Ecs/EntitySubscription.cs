namespace MonoGame.Extras.Ecs
{
    using MonoGame.Extras.Collections;
    using MonoGame.Extras.Ecs.Managers;
    using System;
    using System.Collections.Generic;

    internal class EntitySubscription : IDisposable
    {
        private readonly EntityManager _entityManager;
        private readonly Aspect _aspect;
        private readonly Bag<int> _activeEntities;

        public EntitySubscription(EntityManager entityManager, Aspect aspect)
        {
            _entityManager = entityManager;
            _aspect = aspect;

            _activeEntities = new Bag<int>(_entityManager.Capacity);

            _entityManager.EntityChanged += OnEntityChanged;
            _entityManager.EntityRemoved += OnEntityRemoved;
        }

        public IEnumerable<int> ActiveEntities => _activeEntities;

        public event Action<int> EntitySubscribed;
        public event Action<int> EntityUnsubscribed;

        private bool IsInterested(int entityId)
        {
            return _aspect.IsInterested(_entityManager.GetComponentBits(entityId));
        }

        private void SubscribeEntity(int entityId)
        {
            _activeEntities.Add(entityId);
            EntitySubscribed?.Invoke(entityId);
        }

        private void UnsubscribeEntity(int index, int entityId)
        {
            _activeEntities.Remove(index);
            EntityUnsubscribed?.Invoke(entityId);
        }

        private void OnEntityChanged(int entityId)
        {
            int index = _activeEntities.IndexOf(entityId);

            if(index != -1)
            {
                if (!IsInterested(entityId))
                    UnsubscribeEntity(index, entityId);
            }
            else
            {
                if (IsInterested(entityId))
                    SubscribeEntity(entityId);
            }
        }

        private void OnEntityRemoved(int entityId)
        {
            int index = _activeEntities.IndexOf(entityId);

            if(index != -1)
            {
                UnsubscribeEntity(index, entityId);
            }
        }

        public void Dispose()
        {
            _entityManager.EntityChanged -= OnEntityChanged;
            _entityManager.EntityRemoved -= OnEntityRemoved;
        }
    }
}
