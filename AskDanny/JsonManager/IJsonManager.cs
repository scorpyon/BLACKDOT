using System.Collections.Generic;
using SearchEngine;

namespace JsonManagement
{
    public interface IJsonManager
    {
        List<ISearchResult> DeserializeJson(string jsonString);
        string SerializeToJson(IList<ISearchResult> previousResults);
    }
}