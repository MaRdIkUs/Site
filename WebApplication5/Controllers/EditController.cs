using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using WebApplication5.Data.Interfaces;
using WebApplication5.Data.Models;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class EditController : Controller
    {
        private readonly ITitle title;
        private readonly IPage page;
        private readonly IRoles roles;
        private readonly IUserData userData;
        public EditController(ITitle title,IPage page,IRoles roles, IUserData userData) { 
            this.title = title;
            this.page = page;
            this.roles = roles;
            this.userData = userData;
        }

        [Authorize]
        public IActionResult EditTitlePage(int editPage)
        {
            DefaultTitlePageModel model = new DefaultTitlePageModel();
            model.access = roles.IsUserHaveAccess(1,User);
            if (!model.access)
                return Redirect("~/Home/AccessDenied");
            model.Title = title.GetTitle(editPage);
            model.Id = editPage;
            model.Pages = page.GetPages(editPage);
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public IActionResult EditTitlePage(int pageId, String command)
        {
            if (!roles.IsUserHaveAccess(1, User))
                return Redirect("~/Home/AccessDenied");
            Page mypage = page.GetPage(pageId);
            switch (command)
            {
                case "delete":
                    page.removePage(mypage);
                    break;
            }
            return Redirect("~/Edit/EditTitlePage?editPage=" + mypage.PageType);
        }

        [HttpGet]
        [Authorize]
        public IActionResult EditDescPage(int editPage)
        {
            if (!roles.IsUserHaveAccess(1, User))
                return Redirect("~/Home/AccessDennied");
            Page model = new Page();
            if (editPage >= 0)
                model = page.GetPage(editPage);
            else
            {
                model.PageType = Math.Abs(editPage + 1);
                model.shortDescrition = string.Empty;
                model.Content = string.Empty;
                model.Title = string.Empty;
                model.Id = -1;
            }
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public IActionResult EditDescPage(int pageType, string content, string title, int id)
        {
            if (!roles.IsUserHaveAccess(1, User))
                return Redirect("~/Home/AccessDennied");
            if (id == -1)
            {
                if (page.GetAllPages().Count != 0)
                    id = page.GetMaxId() + 1;
                else
                    id = 0;
                Page model = new Page(id, title, content, pageType);
                page.addPage(model);
            }
            else {
                Page model = new Page(id, title, content, pageType);
                page.editPage(page.GetPage(id),model);
            }
            return Redirect("~/Edit/EditTitlePage?editPage=" + pageType);
        }

        [Authorize]
        public IActionResult UsersList() {
            if (!roles.IsUserHaveAccess(2, User))
                return Redirect("~/Home/AccessDennied");
            return View(new UsersListModel { UsersData = userData.GetUsers(),Roles = roles.getRoles() });
        }

        [Authorize]
        [HttpGet]
        public IActionResult EditUser(string userLg) {
            if (!roles.IsUserHaveAccess(2, User))
                return Redirect("~/Home/AccessDennied");
            EditUserModel model = new EditUserModel { UserData = userData.GetUser(userLg), Roles = roles.getRoles() };
            model.Role = roles.GetRoleName(model.UserData.AccessLevel);
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult EditUser(string login, string password, string role,int command)
        {
            if (!roles.IsUserHaveAccess(2, User))
                return Redirect("~/Home/AccessDennied");
            switch (command) {
                case 0:
                    updateUser(login,password,role);
                    break;
                case 1:
                    DeleteUser(login);
                    break;
            }
            
            return Redirect("~/Edit/UsersList");
        }

        private void updateUser(string login, string password, string role) {
            int accessLevel = -1;
            for (int i = 0; i < roles.getRoles().Length; i++)
                if (role.Equals(roles.GetRoleName(i)))
                    accessLevel = i;
            if (!userData.GetUser(login).Equals(new UserData(password, login, accessLevel)))
                userData.updateUser(login, new UserData(password, login, accessLevel));
        }

        public void DeleteUser(string login)
        {
            userData.removeUser(userData.GetUser(login));
        }

    }
}
