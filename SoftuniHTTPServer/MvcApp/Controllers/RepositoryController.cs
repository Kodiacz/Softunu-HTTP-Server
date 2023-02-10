namespace MvcApp.Controllers
{
    using SoftuniHTTPServer.HTTP;
    using SoftuniHTTPServer.MvcFramework;

    public class RepositoryController : Controller
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
