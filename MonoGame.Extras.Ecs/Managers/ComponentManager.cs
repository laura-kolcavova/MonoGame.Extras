namespace MonoGame.Extras.Ecs.Managers
{
    using MonoGame.Extras.Collections;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public sealed class ComponentManager : IComponentMapperService
    {
        private readonly Bag<IComponentMapper> _componentMappers;
        private readonly Dictionary<Type, int> _componentTypes;

        private int _nextId;

        public ComponentManager()
        {
            _componentMappers = new Bag<IComponentMapper>();
            _componentTypes = new Dictionary<Type, int>();

            _nextId = 0;
        }

        public event Action<int> ComponentsChanged;

        private ComponentMapper<TComponent> CreateMapperForType<TComponent>(int componentTypeId)
            where TComponent : IEntityComponent
        {
            // We can use BitArray class with arbitary length, but it costs performance benefits
            // BitArray is stil faster than bool array and we can use bitwise operations
            if (componentTypeId >= 64)
                throw new InvalidOperationException("Only 64 component types are allowed.");

            var mapper = new ComponentMapper<TComponent>(componentTypeId, ComponentsChanged);

            _componentMappers[componentTypeId] = mapper;

            return mapper;
        }

        public int GetComponentTypeId(Type type)
        {
            if (!_componentTypes.TryGetValue(type, out int id))
            {
                id = _nextId;
                _nextId++;

                _componentTypes.Add(type, id);
            }

            return id;
        }

        public ComponentMapper<T> GetMapper<T>() where T : IEntityComponent
        {
            int componentTypeId = GetComponentTypeId(typeof(T));

            var mapper = _componentMappers[componentTypeId];

            if(mapper != null)
            {
                return mapper as ComponentMapper<T>;
            }

            return CreateMapperForType<T>(componentTypeId);
        }

        public void DestroyComponents(int entityId)
        {
            foreach(var mapper in _componentMappers)
            {
                mapper.Remove(entityId);
            }
        }

        public BitArray64 CreateComponentBits(int entityId)
        {
            var bits = new BitArray64();

            for(int componentId = 0; componentId < _componentMappers.Count(); componentId++)
            {
                var mapper = _componentMappers[componentId];

                bits[componentId] = mapper?.Has(entityId) ?? false;
            }

            return bits;
        }
    }
}
