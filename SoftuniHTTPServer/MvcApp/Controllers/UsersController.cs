namespace MvcApp.Controllers
{
    using SoftuniHTTPServer.HTTP;
    using SoftuniHTTPServer.MvcFramework;
    using System;
    using System.Text;

    public class UsersController : Controller
    {
        public HttpResponse Login(HttpRequest request)
        {
            return View();
        }
        
        public HttpResponse Register(HttpRequest request)
        {
            return View();
        }

        public HttpResponse DoLogin(HttpRequest arg)
        {
            // TODO: READ DATA
            // TODO: CHECK DATA
            // TODO: LOG USER
            // TODO: HOME PAGE
            return this.Redirect("/");
        }
    }
}
