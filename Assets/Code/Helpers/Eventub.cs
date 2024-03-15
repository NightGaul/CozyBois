using System;
using System.Collections.Generic;


public class EventHub
    {
        // Static
        public static Action AddEventListener(On eventName, Action callback)
        {
            Instance._eventMap[eventName].EventAction += callback;
            
            // RemoveEnventListener
            return () =>
            {
                Instance._eventMap[eventName].EventAction -= callback;
            };
        }
        
        public static void AddEventListenerOnce(On eventName, Action callback)
        {
            Instance._eventMap[eventName].EventAction += CallbackOnce;
            return;

            void CallbackOnce()
            {
                callback();
                Instance._eventMap[eventName].EventAction -= CallbackOnce;
            }
        }

        public static void InvokeEvent(On eventName)
        {
            Instance._eventMap[eventName].InvokeEvent();
        }

        private static EventHub _instance;
        private static EventHub Instance => _instance?? new EventHub();

        // Instance
        private readonly Dictionary<On, EventWrapper> _eventMap = new();
        private EventHub()
        {
            _instance = this;

            foreach (On eventValue in Enum.GetValues(typeof(On)))
            {
                _eventMap.Add(eventValue, new EventWrapper());
            }
        }
        
        // Wrapper
        private class EventWrapper
        {
            public event Action EventAction;
            public void InvokeEvent()
            {
                EventAction?.Invoke();
            }
        }
    }

    // Event Enum (Add needed Events here)
    public enum On
    {
        BLOWDANDELION
    }


