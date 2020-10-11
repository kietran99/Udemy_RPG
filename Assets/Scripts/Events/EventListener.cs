using System;
using UnityEngine;

namespace EventSystems
{
    /// <summary>
    /// Invoke <c>StartListening</c> to start listening to an event
    /// </summary>
    public class EventListener<T> : MonoBehaviour where T : IEventData
    {
        private Action<T> listener;
        
        protected virtual void OnDestroy()
        {
            if (listener == null) return;
            EventManager.Instance.StopListening<T>(listener);
        }

        protected void StartListening(Action<T> listener)
        {
            this.listener = listener;
            EventManager.Instance.StartListening<T>(listener);
        }
    }
}