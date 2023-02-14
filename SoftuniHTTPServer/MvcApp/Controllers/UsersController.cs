namespace MvcApp.Controllers
{
    using SoftuniHTTPServer.HTTP;
    using SoftuniHTTPServer.MvcFramework;
    using System;
    using System.Text;

    public class UsersController : Controller
    {
        [HttpGet]
        public HttpResponse Login()
        {
            return View();
        }

        [HttpPost("/Users/Login")]
        public HttpResponse DoLogin()
        {
            var test = this.Request;
            // TODO: READ DATA
            // TODO: CHECK DATA
            // TODO: LOG USER
            // TODO: HOME PAGE
            return this.Redirect("/");
        }

        [HttpGet]
        public HttpResponse Register()
        {
            return View();
        }

    }
}
