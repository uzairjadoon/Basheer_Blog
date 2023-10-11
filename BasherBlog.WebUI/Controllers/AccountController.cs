using Azure.Core;
using BasherBlog.Models;
using BasherBlog.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BasherBlog.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserAccount _account;

        public AccountController(IUserAccount account)
        {
            _account = account;
        }

        [HttpGet]


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(IFormCollection data)
        {
            string email = data["EmailAddress"];
            string password = data["Password"];
            var dbuser= _account.GetUserForLogin(email, password);
            if (dbuser != null)
            {
                CookieOptions options = new CookieOptions();
                options.Expires = DateTime.UtcNow.AddDays(30);
                Response.Cookies.Append("user-access-token", dbuser.AccessToken, options);
                return Redirect("/Home/Index");

            }

            ViewBag.Error = "Sae Password Enter Karo";
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();

        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            string confirmationToken= _account.Register(user);

            if (string.IsNullOrEmpty(confirmationToken))
            {
                ViewBag.error = "Plaease Enter Correct Information";
                return View();
            }
            else
            {
                CookieOptions options=new CookieOptions();
                options.Expires= DateTime.UtcNow.AddDays(30);
                Response.Cookies.Append("user-access-token",user.AccessToken,options);
                return Redirect("/Home/Index");
            }
           
        }
        [HttpGet]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("user-access-token");
            return Redirect("/Home/Index");
        }


    }

    }

