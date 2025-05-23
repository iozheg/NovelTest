using System.Collections.Generic;

using Cysharp.Threading.Tasks;

using GameConfigs;

namespace Common.Analytics
{
    public interface IAnalyticsProvider
    {
        UniTask Init();
        void LogEvent(AnalyticsEvent eventName, Dictionary<string, object> parameters);
        void LogEvent(string eventName, Dictionary<string, object> parameters);
    }
}
