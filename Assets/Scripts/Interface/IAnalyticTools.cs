using System.Collections.Generic;

public interface IAnalyticTools
{
    void SendMessage(string alias);
    
    void SendMessage(string alias, (string, object) eventData);
}