using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication5.Data.Models;
using WebApplication5.Data.Interfaces;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserData data;
        private readonly IRoles roles;
        private static List<UserData> users;

        public AccountController(IUserData data,IRoles roles) {
            this.data = data;
            this.roles = roles;
            users = data.GetUsers();
        }
        private void AddUserToDB(UserData user) {
            data.addUser(user);
        }

        [HttpGet]
        public IActionResult Registration(string returnUrl) {
            ViewBag.ReturnUrl = returnUrl;
            return View(new AccountModel { isCorrect = true });
        }
        private bool isExist(string login)
        {
            foreach (var user in users)
            {
                if (user.Login.Equals(login))
                    return true;
            }
            return false;
        }
        [HttpPost]
        public async Task<IActionResult> Registration(string userName, string password, string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            if (!isExist(userName))
            {
                var claims = new List<Claim>
                {
                    new Claim("name", userName),
                    new Claim("role", "Member")
                };
                await HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims, "Cookies", "name", "role")));
                UserData userData = new UserData(password, userName,0);
                users.Add(userData);
                AddUserToDB(userData);
                return Redirect("~/Home/Index");
            }
            return View(new AccountModel { isCorrect = false });
        }




        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View(new AccountModel { isCorrect = true });
        }
        private int ValidateLogin(string userName, string password)
        {
            foreach (var user in users)
            {
                if(user.Login.Equals(userName) && user.Password.Equals(password))
                    return user.AccessLevel;
            }
            return -1;
        }
        [HttpPost]
        public async Task<IActionResult> Login(string userName, string password, string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            // Normally Identity handles sign in, but you can do it directly
            int access = ValidateLogin(userName, password);
            if (access != -1)
            {
                var claims = new List<Claim>
                {
                new Claim("name", userName),
                new Claim("role", roles.GetRoleName(access))
                };
                await HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims, "Cookies", "name", "role")));
                return Redirect("~/Home/Index");
            }
            return View(new AccountModel { isCorrect = false });
        }




        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}
