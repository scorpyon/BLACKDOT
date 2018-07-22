using System.Collections.Generic;
using AskDanny.Models;
using NUnit.Framework;
using SearchEngine;

namespace AskDannyTests.Unit
{
    [TestFixture]
    public class SearchCriteriaTests
    {
        [Test]
        public void SearchCriteria_ShouldContainTheCorrectInformation()
        {
            const string searchText = "My Search";
            const string searchReminder = "My reminder";

            var search = new SearchCriteria()
            {
                SearchText = searchText,
                SearchResultReminder = searchReminder,
                SearchResults = new List<ISearchResult>()
            };
            
            Assert.That(search.SearchResults, Is.Not.Null);
            Assert.That(search.SearchText, Is.EqualTo(searchText));
            Assert.That(search.SearchResultReminder, Is.EqualTo(searchReminder));
        }
    }
}