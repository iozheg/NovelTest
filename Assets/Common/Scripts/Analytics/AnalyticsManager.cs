using System.Collections.Generic;

using GameConfigs;

using UnityEngine;

namespace Common.Analytics
{
    public class AnalyticsManager
    {
        public static AnalyticsManager Instance { get; private set; }
        public string PlatformIdentifier { get; set; }

        private readonly List<IAnalyticsProvider> _analyticsProviders = new();
        
        public AnalyticsManager()
        {
            if (Instance != null)
            {
                return;
            }
            Instance = this;
        }

        public void RegisterProvider(IAnalyticsProvider provider)
        {
            _analyticsProviders.Add(provider);
        }

        public void LogEvent(string eventName)
        {
            LogEvent(eventName, new Dictionary<string, object>());
        }
    
        public void LogEvent(AnalyticsEvent eventName)
        {
            LogEvent(eventName, new Dictionary<string, object>());
        }

        public void LogEvent(AnalyticsEvent eventName, string key, string data)
        {
            LogEvent(eventName.ToString(), new Dictionary<string, object> { { key, data } });
        }

        public void LogEvent(string eventName, string key, string data)
        {
            LogEvent(eventName, new Dictionary<string, object> { { key, data } });
        }

        public void LogEvent(AnalyticsEvent eventName, Dictionary<string, object> parameters = null)
        {
            LogEvent(eventName.ToString(), parameters);
        }

        public void LogEvent(string eventName, Dictionary<string, object> parameters = null)
        {
            var eventData = parameters ?? new Dictionary<string, object>();
            eventData["platform"] = PlatformIdentifier;

            if (_analyticsProviders.Count == 0)
            {
                Debug.Log("LogEvent: " + eventName);
                foreach (var param in eventData)
                {
                    Debug.Log("Param: " + param.Key + " = " + param.Value);
                }
            }
            else
            {
                foreach (var provider in _analyticsProviders)
                {
                    provider.LogEvent(eventName, eventData);
                }
            }
        }
    }
}
