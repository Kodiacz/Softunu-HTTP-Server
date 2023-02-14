namespace MvcApp.Controllers
{
    using SoftuniHTTPServer.HTTP;
    using SoftuniHTTPServer.MvcFramework;
    using System;

    public class CommitsController : Controller
    {
        public HttpResponse All()
        {
            return View();
        }

        [HttpGet("/Commits/Create")]
        public HttpResponse Create()
        {
            return View();
        }
    }
}
