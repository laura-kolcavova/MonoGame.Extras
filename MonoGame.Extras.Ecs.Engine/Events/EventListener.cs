namespace MonoGame.Extras.Ecs.Engine.Events
{
    using MonoGame.Extras.Ecs.Systems;
    using System;

    public interface IEventListener
    {
        ISystem System { get; }
        Type EventType { get; }
        void Invoke(IEvent routedEvent);
    }

    public class EventListener<TEvent> : IEventListener where TEvent : IEvent
    {
        public ISystem System { get; private set; }

        public Type EventType { get; private set; }

        public event Action<TEvent> Event;

        internal EventListener(ISystem system)
        {
            System = system;
            EventType = typeof(TEvent);

        }
        public void Invoke(IEvent routedEvent)
        {
            Event?.Invoke((TEvent)routedEvent);
        }
    }
}
