using System.Collections.Generic;
using System.Web.Mvc;
using AskDanny.Models;
using FileManagement;
using JsonManagement;
using SearchEngine;

namespace AskDanny.Controllers
{
    public class HomeController : Controller
    {
        public static IList<ISearchResult> PreviousResults = new List<ISearchResult>();

        [HttpGet]
        public ActionResult Index(SearchCriteria model)
        {
            if (model == null)
            {
                model = new SearchCriteria();
            }
            model.SearchResults = PreviousResults;
            return View(model);
        }

        public ActionResult Search(string searchText, string site)
        {
            var model = new SearchCriteria();
            if (searchText == null)
            {
                return RedirectToAction("Index", model);
            }

            model.SearchText = searchText;
            model.SearchResultReminder = $"You searched for the keywords: {searchText}";
            var search = new SearchManager();
            var website = search.GetSiteFromNameString(site);
            model.SearchResults = search.GetSearchResultFrom(model.SearchText, website);
            PreviousResults = model.SearchResults;

            return RedirectToAction("Index", model);
        }

        public ActionResult ConvertToJson(string searchText)
        {
            if(PreviousResults.Count < 1) { return RedirectToAction("Index"); }
            var jsonManager = new JsonManager();
            var json = jsonManager.SerializeToJson(PreviousResults);
                //new JavaScriptSerializer().Serialize(PreviousResults);
            var fileManager = new FileManager();
            fileManager.SaveToFile(json);
            return RedirectToAction("Index");
        }

        public ActionResult ImportFromJson()
        {
            var fileManager = new FileManager();
            var jsonString = fileManager.LoadFile();
            var jsonManager = new JsonManager();
            var jsonResult = jsonManager.DeserializeJson(jsonString);
            var model = new SearchCriteria()
            {
                SearchResults = jsonResult
            };
            PreviousResults = jsonResult;
            return RedirectToAction("Index", model);
        }
    }
}