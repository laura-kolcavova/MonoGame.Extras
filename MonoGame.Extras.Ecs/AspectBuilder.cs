namespace MonoGame.Extras.Ecs
{
    using MonoGame.Extras.Collections;
    using MonoGame.Extras.Ecs.Managers;
    using System;
    using System.Collections.Generic;

    public class AspectBuilder
    {
        private readonly Bag<Type> _allTypes;
        private readonly Bag<Type> _oneTypes;
        private readonly Bag<Type> _notTypes;

        internal AspectBuilder()
        {
            _allTypes = new Bag<Type>();
            _notTypes = new Bag<Type>();
            _oneTypes = new Bag<Type>();
        }

        public AspectBuilder All(params Type[] componentTypes)
        {
            foreach (var type in componentTypes)
                _allTypes.Add(type);

            return this;
        }

        public AspectBuilder One(params Type[] componentTypes)
        {
            foreach (var type in componentTypes)
                _oneTypes.Add(type);

            return this;
        }

        public AspectBuilder Not(params Type[] componentTypes)
        {
            foreach (var type in componentTypes)
                _notTypes.Add(type);

            return this;
        }

        public Aspect Build(ComponentManager componentManager)
        {
            var aspect = new Aspect();

            AssociateBits(componentManager, _allTypes, ref aspect.AllSet);
            AssociateBits(componentManager, _notTypes, ref aspect.NotSet);
            AssociateBits(componentManager, _oneTypes, ref aspect.OneSet);

            return aspect;
        }

        private void AssociateBits(ComponentManager componentManager, IEnumerable<Type> componentTypes, ref BitArray64 bits)
        {
            foreach (var type in componentTypes)
            {
                int id = componentManager.GetComponentTypeId(type);
                bits[id] = true;
            }
        }
    }
}
