using System.Collections.Generic;
using SearchEngine;

namespace AskDanny.Models
{
    public class SearchCriteria : ISearchCriteria
    {
        public string SearchText { get; set; }
        public string SearchResultReminder { get; set; }
        public IList<ISearchResult> SearchResults { get; set; }
    }
}