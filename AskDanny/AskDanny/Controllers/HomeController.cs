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
        public ActionResult Index(SearchCriteria model)
        {
            if (model == null)
            {
                model = new SearchCriteria();
            }
            model.SearchResults = PreviousResults;
            return View(model);
        }

        [HttpPost]
        public ActionResult Search(SearchCriteria model)
        {
            if (model == null)
            {
                return RedirectToAction("Index");
            }

            if (model.SearchText != null)
            {
                model.SearchResultReminder = $"You searched for the keywords: {model.SearchText}";
                var search = new SearchManager();
                model.SearchResults = search.GetSearchResultFrom(model.SearchText);
                PreviousResults = model.SearchResults;
            }

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