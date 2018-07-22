namespace SearchEngine
{
    public interface ISearchResult
    {
        string ResultUrl { get; set; }
        string ResultTitle { get; set; }
        string ResultSummary { get; set; }

    }
}