using System;

namespace EventSystems
{
    public interface IBaseEvent
    { 
    }

    public class GameEvent<T> : IBaseEvent
    {
        private event Action<T> Listeners;

        public void Add(Action<T> listener) => Listeners += listener;

        public void Remove(Action<T> listener) => Listeners -= listener;

        public void Invoke(T eventData) => Listeners?.Invoke(eventData);
    }
}