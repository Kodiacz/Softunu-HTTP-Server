namespace MvcApp.Controllers
{
    using MvcApp.Data;
    using MvcApp.Services;
    using SoftuniHTTPServer.HTTP;
    using SoftuniHTTPServer.MvcFramework;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using System.Text.RegularExpressions;

    public class UsersController : Controller
    {
        private readonly IUserService userServices;

        public UsersController(IUserService userServices)
        {
            this.userServices = userServices;
        }

        [HttpGet]
        public HttpResponse Login()
        {
            return View();
        }

        [HttpPost("/Users/Login")]
        public HttpResponse DoLogin(string username, string password)
        {
            //var username = this.Request.FormData["username"];
            //var password = this.Request.FormData["password"];
            var userId = this.userServices.GetUserId(username, password);

            if (userId == null)
            {
                // return error
                return null;
            }

            this.SignIn((string)userId);

            return this.Redirect("/");
        }

        [HttpGet]
        public HttpResponse Register()
        {
            return View();
        }

        [HttpPost("/Users/Register")]
        public HttpResponse DoRegister(string username, string email, string password, string confirmPassword)
        {
            //var username = this.Request.FormData["username"];
            //var email = this.Request.FormData["email"];
            //var password = this.Request.FormData["password"];
            //var confirmPassword = this.Request.FormData["confirmPassword"];

            //if (username == null || username.Length < 5 || username.Length > 20)
            //{
            //    // return error
            //    return null;
            //}

            //if (!Regex.IsMatch(username, @"[a-zA-Z0-9\.]+"))
            //{
            //    // return error
            //    return null;
            //}

            //if (string.IsNullOrEmpty(email) || !new EmailAddressAttribute().IsValid(email))
            //{
            //    // return error
            //    return null;
            //}

            //if (password != confirmPassword)
            //{
            //    // return error
            //    return null;
            //}

            //if (!this.userServices.IsUsernameAvailable(username))
            //{
            //    // return error
            //    return null;
            //}

            //if (!this.userServices.IsEmailAvailable(email))
            //{
            //    // return error
            //    return null;
            //}

            this.userServices.CreateUser(username, email, password);

            return View(null, "Login");
        }

        [HttpGet]
        public HttpResponse Logout()
        {
            this.SignOut();
            return this.Redirect("/");
        }
    }
}
