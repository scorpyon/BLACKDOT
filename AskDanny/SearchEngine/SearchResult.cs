namespace SearchEngine
{
    public class SearchResult : ISearchResult
    {
        public string ResultUrl { get; set; }
        public string ResultTitle { get; set; }
        public string ResultSummary { get; set; }
    }
}