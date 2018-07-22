using System.Collections;
using System.Collections.Generic;
using SearchEngine;

namespace AskDanny.Models
{
    public interface ISearchCriteria
    {
        string SearchText { get; set; }
        string SearchResultReminder { get; set; }
        IList<ISearchResult> SearchResults { get; set; }
    }
}