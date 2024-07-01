using System.Collections.Generic;
using System;

namespace CockroachRunner
{
    public class EventsManager
    {
        public delegate void OnEvent(params object[] args);
        
        private Dictionary<string, List<OnEvent>> listeners;
        
        public int ListenersCount { get { return listeners.Count; } }
        
        public EventsManager()
        {
            listeners = new Dictionary<string, List<OnEvent>>();
        }

        public void AddListener(Enum eventItem, OnEvent listener)
        {
            string eventName = GenerateEventKey(eventItem);
            List<OnEvent> listenList = null;

            if (listeners.TryGetValue(eventName, out listenList))
            {
                if (!listenList.Contains(listener))
                    listenList.Add(listener);
            }
            else
            {
                listenList = new List<OnEvent> { listener };
                listeners.Add(eventName, listenList);
            }
        }

        public void RemoveListener(Enum eventItem, OnEvent listener)
        {
            string eventName = GenerateEventKey(eventItem);
            List<OnEvent> listenList = null;

            if (listeners.TryGetValue(eventName, out listenList))
            {
                listenList.Remove(listener);
            }
        }
        
        public void InvokeEvent(Enum eventItem, params object[] args)
        {
            string eventName = GenerateEventKey(eventItem);
            List<OnEvent> listenList = null;

            if (listeners.TryGetValue(eventName, out listenList))
            {
                List<OnEvent> targetList = new List<OnEvent>(listenList);

                for (int i = 0; i < targetList.Count; i++)
                {
                    if (!targetList[i].Equals(null))
                    {
                        targetList[i](args);
                    }
                }
            }
        }
        
        public void Clear()
        {
            listeners.Clear();
        }

        public void RemoveEvent(Enum eventItem)
        {
            listeners.Remove(GenerateEventKey(eventItem));
        }

        public void RemoveNullTargets()
        {
            var tempListeners = new Dictionary<string, List<OnEvent>>();

            foreach (KeyValuePair<string, List<OnEvent>> item in listeners)
            {
                for (int i = item.Value.Count - 1; i >= 0; --i)
                {
                    if (item.Value[i].Target.Equals(null))
                    {
                        item.Value.RemoveAt(i);
                    }
                }

                if (item.Value.Count > 0)
                {
                    tempListeners.Add(item.Key, item.Value);
                }
            }

            listeners = tempListeners;
        }
        
        private string GenerateEventKey(Enum eventItem)
        {
            return string.Format("{0}:{1}", eventItem.GetType().ToString(), eventItem.ToString());
        }
    }
}