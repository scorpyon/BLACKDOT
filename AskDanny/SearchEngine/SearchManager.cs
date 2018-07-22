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
        public IList<ISearchResult> GetSearchResultFrom(string modelSearchText)
        {
            var resultString = ScrapeDataFromBing(modelSearchText);
            return string.IsNullOrEmpty(resultString) 
                ? null 
                : ConvertHtmlDataToResultList(resultString);
        }

        public IList<ISearchResult> ConvertHtmlDataToResultList(string resultString)
        {
            //Parse the HTML string as an HTMLDoc using HTMLAgility
            var doc = new HtmlDocument();
            doc.LoadHtml(resultString);

            //Locate the results nodes within the parsed document
            var resultNodes = doc.DocumentNode
                .SelectNodes("//ol/li")
                .Where(x => x.Attributes["class"].Value == "b_algo");

            var outputList = new List<ISearchResult>();

            //Cycle throught the results nodes and create the model objects for each result
            foreach (var resultNode in resultNodes)
            {
                var title = resultNode.ChildNodes.FindFirst("h2").InnerText;
                var link = resultNode.ChildNodes.FindFirst("cite").InnerText;
                var desc = resultNode.ChildNodes.FindFirst("div").InnerText;
                var modifiedDesc = desc.Replace(link, "");

                //Define the result object and add it to our output list
                var searchResult = new SearchResult()
                {
                    ResultTitle = title,
                    ResultUrl = link,
                    ResultSummary = modifiedDesc
                };
                outputList.Add(searchResult);
            }

            //Return our result
            return outputList;
        }

        public string ScrapeDataFromBing(string modelSearchText)
        {
            if(string.IsNullOrEmpty(modelSearchText)) {  return String.Empty; }
            // Remove spaces
            var modifiedSearchText = modelSearchText.Replace(" ", "+");

            // Set the url to scrape from
            var urlAddress = @"https://www.bing.com/search?q=" + modifiedSearchText;

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
    }
}
