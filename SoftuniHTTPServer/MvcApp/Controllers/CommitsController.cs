namespace MvcApp.Controllers
{
    using SoftuniHTTPServer.HTTP;
    using SoftuniHTTPServer.MvcFramework;
    using System;

    public class CommitsController : Controller
    {
        public HttpResponse All(HttpRequest request)
        {
            return View();
        }
        
        public HttpResponse Create(HttpRequest request)
        {
            return View();
        }
    }
}
