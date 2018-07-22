using System.Collections.Generic;

namespace SearchEngine
{
    public interface ISearchManager
    {
        IList<ISearchResult> GetSearchResultFrom(string modelSearchText);
        IList<ISearchResult> ConvertHtmlDataToResultList(string resultString);
    }
}