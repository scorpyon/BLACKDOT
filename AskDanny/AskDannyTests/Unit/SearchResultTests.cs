using System.Collections.Generic;
using AskDanny.Models;
using NUnit.Framework;
using SearchEngine;

namespace AskDannyTests.Unit
{
    [TestFixture]
    public class SearchResultTests
    {
        [Test]
        public void SearchResultTests_ShouldContainTheCorrectInformation()
        {
            const string title = "Title";
            const string summary = "Summary";
            const string url = "MyUrl";

            var search = new SearchResult()
            {
                ResultTitle = title,
                ResultSummary = summary,
                ResultUrl = url
            };
            
            Assert.That(search.ResultTitle, Is.EqualTo(title));
            Assert.That(search.ResultSummary, Is.EqualTo(summary));
            Assert.That(search.ResultUrl, Is.EqualTo(url));
        }
    }
}