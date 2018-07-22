using NUnit.Framework;
using NUnit.Framework.Internal;
using SearchEngine;

namespace AskDannyTests.Unit
{
    [TestFixture]
    public class SearchManagerTests
    {
        [Test]
        public void SearchManager_ShouldReturnCorrectStringForWebsite()
        {
            var search = new SearchManager();

            var result = search.GetSiteName(SearchManager.Website.Bing);
            Assert.That(result, Is.EqualTo(SearchManager.Website.Bing.ToString()));

            result = search.GetSiteName(SearchManager.Website.Google);
            Assert.That(result, Is.EqualTo(SearchManager.Website.Google.ToString()));
        }

        [Test]
        public void SearchManager_ShouldReturnCorrectEnumForWebsiteName()
        {
            var search = new SearchManager();

            var result = search.GetSiteFromNameString("bing");
            Assert.That(result, Is.EqualTo(SearchManager.Website.Bing));

            result = search.GetSiteFromNameString("google");
            Assert.That(result, Is.EqualTo(SearchManager.Website.Google));

            result = search.GetSiteFromNameString("DASDAS");
            Assert.That(result, Is.EqualTo(SearchManager.Website.Bing));
        }
    }
}