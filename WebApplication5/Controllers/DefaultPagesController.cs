using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication5.Data.Interfaces;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class DefaultPagesController : Controller
    {
        private readonly IPage pages;
        private readonly ITitle title;
        private readonly IRoles roles;

        public DefaultPagesController(IPage page, ITitle title, IRoles roles) {
            this.pages = page;
            this.title = title;
            this.roles = roles;
        }
        [HttpGet]
        public IActionResult DefaultTitlePage(int pageId) {
            if (pageId == 0) {
                DefaultTitlePageModel model = new DefaultTitlePageModel();
                model.Pages = pages.GetAllPages();
            }
            DefaultTitlePageModel model = new DefaultTitlePageModel();
            model.Pages = pages.GetPages(pageId);
            model.Title = title.GetTitle(pageId);
            model.Id = pageId;
            model.access = roles.IsUserHaveAccess(1,User);
            return View(model);
        }

        [Authorize]
        public IActionResult DefaultDescPage(int pageId) {
            return View(new DefaultDescPageModel { page = pages.GetPage(pageId),access = roles.IsUserHaveAccess(1, User) });
        }
    }
}
