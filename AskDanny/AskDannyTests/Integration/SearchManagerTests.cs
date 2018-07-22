using System.Linq;
using NUnit.Framework;
using SearchEngine;

namespace AskDannyTests.Integration
{
    [TestFixture]
    public class SearchManagerTests
    {
        [Test]
        public void SearchManager_ScrapeDataFromBing_ShouldReturnTheCorrectResultData()
        {
            var manager = new SearchManager();
            const string testString = "Danny";

            var result = manager.ScrapeDataFromBing(testString);

            Assert.That(result.Contains(testString));
            Assert.That(result.Contains("Danny O'Donoghue"));
        }

        [Test]
        public void SearchManager_ConvertHtmlDataToResultList_ShouldConvertHtmlDataToUsableObjectList()
        {
            var manager = new SearchManager();
            const string testString = "Danny";

            var resultHtml = manager.ScrapeDataFromBing(testString);
            var result = manager.ConvertHtmlDataToResultList(resultHtml);

            Assert.That(result.Count,Is.GreaterThan(0));
            Assert.That(result.FirstOrDefault().ResultTitle.Contains(testString));
            Assert.That(result.FirstOrDefault().ResultUrl.Contains("http"));
            Assert.That(result.FirstOrDefault().ResultSummary.Contains(testString));
        }
    }
}
