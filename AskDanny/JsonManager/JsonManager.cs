using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using SearchEngine;

namespace JsonManagement
{
    public class JsonManager : IJsonManager
    {
        public List<ISearchResult> DeserializeJson(string jsonString)
        {
            try
            {
                var list = new JavaScriptSerializer().Deserialize<List<SearchResult>>(jsonString);
                return list.Cast<ISearchResult>().ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }

        public string SerializeToJson(IList<ISearchResult> previousResults)
        {
            try
            {
                return new JavaScriptSerializer().Serialize(previousResults);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return string.Empty;
        }
    }
}
