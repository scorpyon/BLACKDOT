using System.Collections.Generic;
using System.IO;
using System.Linq;
using FileManagement;
using JsonManagement;
using NUnit.Framework;
using SearchEngine;

namespace AskDannyTests.Integration
{
    [TestFixture]
    public class JsonManagerTests
    {
        [Test]
        public void JsonManager_ShouldSerializeObject()
        {
            const string testUrl = "TestUrl";
            const string testTitle = "TestTitle";
            const string testSummary = "TestSummary";

            var testSearchCriteria = new SearchResult()
            {
                ResultSummary = testSummary,
                ResultTitle = testTitle,
                ResultUrl = testUrl
            };
            var testSearchResults = new List<ISearchResult>() {testSearchCriteria};
            var jsonManager = new JsonManager();

            var result = jsonManager.SerializeToJson(testSearchResults);

            Assert.IsInstanceOf<string>(result);
            Assert.That(result.Contains(testUrl));
            Assert.That(result.Contains(testTitle));
            Assert.That(result.Contains(testSummary));
        }

        [Test]
        public void JsonManager_ShouldDeserializeJsonToObject()
        {
            const string testUrl = "TestUrl";
            const string testTitle = "TestTitle";
            const string testSummary = "TestSummary";

            var testSearchCriteria = new SearchResult()
            {
                ResultSummary = testSummary,
                ResultTitle = testTitle,
                ResultUrl = testUrl
            };
            var testSearchResults = new List<ISearchResult>() { testSearchCriteria };
            var jsonManager = new JsonManager();

            var testSerializedString = jsonManager.SerializeToJson(testSearchResults);

            var result = jsonManager.DeserializeJson(testSerializedString);

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.FirstOrDefault().ResultUrl, Is.EqualTo(testUrl));
            Assert.That(result.FirstOrDefault().ResultSummary, Is.EqualTo(testSummary));
            Assert.That(result.FirstOrDefault().ResultTitle, Is.EqualTo(testTitle));
        }
    }
}