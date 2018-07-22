using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using HtmlAgilityPack;

namespace SearchEngine
{
    public class SearchManager : ISearchManager
    {
        public enum Website
        {
            Bing,
            Google
        }

        public IList<ISearchResult> GetSearchResultFrom(string modelSearchText, Website siteName)
        {
            var resultString = ScrapeDataFrom(modelSearchText, siteName);
            return string.IsNullOrEmpty(resultString) 
                ? null 
                : ConvertHtmlDataToResultList(resultString, siteName);
        }

        public IList<ISearchResult> ConvertHtmlDataToResultList(string resultString, Website siteName)
        {
            //Parse the HTML string as an HTMLDoc using HTMLAgility
            var doc = new HtmlDocument();
            doc.LoadHtml(resultString);

            var outputList = new List<ISearchResult>();

            //Cycle throught the results nodes and create the model objects for each result
            if (siteName == Website.Bing) { outputList = GetResultsFromBing(doc); }
            else if(siteName == Website.Google) { outputList = GetResultsFromGoogle(doc); }

            //Return our result
            return outputList;
        }

        private List<ISearchResult> GetResultsFromGoogle(HtmlDocument nodes)
        {
            var searchNode = nodes.DocumentNode
                .SelectNodes("//div")
                .Where(x => x.Id == "search");

            var resultNodes = searchNode.FirstOrDefault()
                .ChildNodes
                .FirstOrDefault()
                .FirstChild
                .ChildNodes;



            var result = new List<ISearchResult>();
            foreach (var resultNode in resultNodes)
            {
                var titleNode = resultNode.ChildNodes.FindFirst("h3");
                var linkNode = resultNode.ChildNodes.FindFirst("cite");
                var descNode = resultNode.ChildNodes.FindFirst("div");

                if(titleNode == null || linkNode == null || descNode == null)
                {
                    continue;
                }

                var title = titleNode.InnerText;
                var link = linkNode.InnerText;
                var desc = descNode.InnerText;

                var modifiedDesc = desc.Replace(link, "");
                modifiedDesc = modifiedDesc.Replace("CachedSimilar", "");
                modifiedDesc = modifiedDesc.Replace("Cached", "");

                //Define the result object and add it to our output list
                var searchResult = new SearchResult()
                {
                    ResultTitle = title,
                    ResultUrl = link,
                    ResultSummary = modifiedDesc
                };
                result.Add(searchResult);
            }
            return result;
        }

        private List<ISearchResult> GetResultsFromBing(HtmlDocument nodes)
        {
            const string nodeParent = "//ol/li";
            const string resultNodeParent = "b_algo";
            var resultNodes = nodes.DocumentNode
                .SelectNodes(nodeParent)
                .Where(x => x.Attributes["class"].Value == resultNodeParent);

            var result = new List<ISearchResult>();
            foreach (var resultNode in resultNodes)
            {
                var titleNode = resultNode.ChildNodes.FindFirst("h2");
                var linkNode = resultNode.ChildNodes.FindFirst("cite");
                var descNode = resultNode.ChildNodes.FindFirst("div");

                if (titleNode == null || linkNode == null || descNode == null)
                {
                    continue;
                }

                var title = titleNode.InnerText;
                var link = linkNode.InnerText;
                var desc = descNode.InnerText;


                var modifiedDesc = desc.Replace(link, "");

                //Define the result object and add it to our output list
                var searchResult = new SearchResult()
                {
                    ResultTitle = title,
                    ResultUrl = link,
                    ResultSummary = modifiedDesc
                };
                result.Add(searchResult);
            }
            return result;
        }

        public string ScrapeDataFrom(string modelSearchText, Website siteName)
        {
            var site = GetSiteName(siteName);

            if(string.IsNullOrEmpty(modelSearchText)) {  return String.Empty; }
            // Remove spaces
            var modifiedSearchText = modelSearchText.Replace(" ", "+");

            // Set the url to scrape from - normally this would not be hardcoded in this way and I would even prefer a selectable config of some sort!
            // Also, I would prefer to use a string.format here or even better, the string interpolation from C#6, but it seems that the 
            //Character coding causes issues with the format so I used this style for now (though it is slightly inefficient)
            var urlAddress = @"https://www." + site + @".com/search?q=" + modifiedSearchText;

            var data = string.Empty;
            var request = (HttpWebRequest)WebRequest.Create(urlAddress);
            var response = (HttpWebResponse)request.GetResponse();

            // Make sure the response came back from the Bing site correctly
            if (response.StatusCode != HttpStatusCode.OK) { return data; }

            // Retrieve the html data as a string
            var receiveStream = response.GetResponseStream();

            //Make sure the retrieved data is not null
            if (receiveStream == null) { return data; }
            var readStream = response.CharacterSet == null 
                ? new StreamReader(receiveStream) 
                : new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));

            // Read the data into our variable
            data = readStream.ReadToEnd();

            // Clean up after ourselves
            response.Close();
            readStream.Close();

            //Return the output result
            return data;
        }

        public string GetSiteName(Website siteName)
        {
            // This could be expanded to include future search engines
            return siteName == Website.Google 
                ? Website.Google.ToString() 
                : Website.Bing.ToString();
        }

        public Website GetSiteFromNameString(string site)
        {
            switch (site)
            {
                case "bing":
                    return Website.Bing;
                case "google":
                    return Website.Google;
            }
            return Website.Bing;
        }
    }
}
