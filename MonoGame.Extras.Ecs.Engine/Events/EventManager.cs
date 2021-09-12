namespace MonoGame.Extras.Ecs.Engine.Events
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DotNet.Extras.Collections;
    using MonoGame.Extras.Ecs.Systems;

    public interface IEventManager
    {
        void Publish(IEvent routedEvent);

        void Subscribe<TEvent>(ISystem system, Action<TEvent> listener) where TEvent : IEvent;

        void Unsubscribe<TEvent>(ISystem system, Action<TEvent> listener) where TEvent : IEvent;
    }

    public sealed class EventManager : IEventManager
    {
        private Bag<IEvent> _eventQueue;
        private Dictionary<Type, Bag<IEventListener>> _eventListeners;

        public EventManager()
        {
            _eventQueue = new Bag<IEvent>();
            _eventListeners = new Dictionary<Type, Bag<IEventListener>>();
        }

        private EventListener<TEvent> GetEventListener<TEvent>(ISystem system) where TEvent : IEvent
        {
            if(_eventListeners.TryGetValue(typeof(TEvent), out var listenerBag))
            {
                var eventListener = listenerBag.FirstOrDefault(listener => listener.Equals(system));
                return eventListener as EventListener<TEvent>;
            }

            return null;
        }

        public void Publish(IEvent routedEvent)
        {
            _eventQueue.Add(routedEvent);
        }

        public void Subscribe<TEvent>(ISystem system, Action<TEvent> listener) where TEvent : IEvent
        {
            var eventListener = GetEventListener<TEvent>(system);

            if (eventListener == null)
            {
                var eventType = typeof(TEvent);

                if (!_eventListeners.ContainsKey(eventType))
                {
                    _eventListeners.Add(eventType, new Bag<IEventListener>());
                }

                eventListener = new EventListener<TEvent>(system);
                _eventListeners[eventType].Add(eventListener);
            }

            eventListener.Event += listener;

        }

        public void Unsubscribe<TEvent>(ISystem system, Action<TEvent> listener) where TEvent : IEvent
        {
            var eventListener = GetEventListener<TEvent>(system);

            if (eventListener != null)
            {
                eventListener.Event -= listener;
            }
        }

        public void ProcessEvents()
        {
            if (_eventQueue.Any())
            {
                foreach (var routedEvent in _eventQueue)
                {
                    var type = routedEvent.GetType();

                    if (_eventListeners.Any())
                    {
                        foreach (var eventListener in _eventListeners[type])
                        {
                            eventListener.Invoke(routedEvent);
                        }
                    }
                }

                _eventQueue.Clear();
            }
        }
    }
}
