namespace MonoGame.Extras.Ecs
{
    using MonoGame.Extras.Collections;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public interface IComponentMapper
    {
        int Id { get; }
        Type ComponentType { get; }

        bool Has(int entityId);
        void Remove(int entityId);
    }

    public sealed class ComponentMapper<TComponent> : IComponentMapper
        where TComponent : IEntityComponent
    {
        private readonly Action<int> _onComponentsChanged;

        private readonly Bag<IEntityComponent> _components;

        public ComponentMapper(int id, Action<int> onComponentsChanged)
        {
            Id = id;
            ComponentType = typeof(TComponent);

            _onComponentsChanged = onComponentsChanged;
            _components = new Bag<IEntityComponent>();
        }

        public int Id { get; }

        public Type ComponentType { get; }

        public IEnumerable<TComponent> Components => _components.Where(c => c != null).Cast<TComponent>();

        public void Put(int entityId, TComponent component)
        {
            _components[entityId] = component;
            _onComponentsChanged.Invoke(entityId);
        }

        public void Remove(int entityId)
        {
            _components[entityId] = null;
            _onComponentsChanged?.Invoke(entityId);
        }

        public TComponent Get (Entity entity)
        {
            return Get(entity.Id);
        }

        public TComponent Get(int entityId)
        {
            return (TComponent) _components[entityId];
        }

        public bool Has(int entityId)
        {
            if (entityId >= _components.Count)
                return false;

            return _components[entityId] != null;
        }
    }
}
