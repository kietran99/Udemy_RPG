using System;

namespace EventSystems
{
    public class GameEvent<T> : IBaseEvent where T : IEventData
    {
        private event Action<T> Listeners;

        public void Add(Action<T> listener) => Listeners += listener;

        public void Remove(Action<T> listener) => Listeners -= listener;

        public void Invoke(T eventData) => Listeners?.Invoke(eventData);
    }
}