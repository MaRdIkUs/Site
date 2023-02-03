using Microsoft.AspNetCore.Mvc;
using WebApplication5.Data.Interfaces;
using WebApplication5.Data.Models;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class SearchController : Controller
    {
        IPage pages;

        public SearchController(IPage ipages) {
            pages = ipages;
        }

        [HttpPost]
        public ActionResult Search(string textToSearch)
        {
            if (textToSearch == null || textToSearch.Trim() == "") { 
                return View(new SearchResultModel { searchText = "Поиск..." });
            }
            SearchResultModel model = new SearchResultModel { searchText= textToSearch };
            if (textToSearch != null)
            {
                foreach (Page i in pages.GetAllPages())
                {
                    if (i.Content.Contains(textToSearch) || i.Title.Contains(textToSearch))
                        model.pages.Add(i);
                }
            }
            return View(model);
        }
    }
}
