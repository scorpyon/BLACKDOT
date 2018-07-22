using System.Collections.Generic;

namespace SearchEngine
{
    public interface ISearchManager
    {
        IList<ISearchResult> GetSearchResultFrom(string modelSearchText, SearchManager.Website siteName);
        IList<ISearchResult> ConvertHtmlDataToResultList(string resultString, SearchManager.Website siteName);
        string ScrapeDataFrom(string modelSearchText, SearchManager.Website siteName);
        string GetSiteName(SearchManager.Website siteName);
        SearchManager.Website GetSiteFromNameString(string site);
    }
}