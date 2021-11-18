using System.Collections.Generic;
using UnityEngine.Analytics;


public sealed class UnityAnalyticTools : IAnalyticTools
{
    public void SendMessage(string alias)
    {
        Analytics.CustomEvent(alias);
    }

    public void SendMessage(string alias, (string, object) eventData)
    {
            var res = new Dictionary<string, object> {[eventData.Item1] = eventData.Item2};
        Analytics.CustomEvent(alias, res);
    }
} 