namespace MvcApp.Controllers
{
    using SoftuniHTTPServer.HTTP;
    using SoftuniHTTPServer.MvcFramework;
    using System.Text;

    public class HomeController : Controller
    {
        public HttpResponse Index(HttpRequest request)
        {
            return View();
        }

        public HttpResponse About(HttpRequest request)
        {
            var responseHtml = "<h1>About...</h1>";
            var responseBodyBytes = Encoding.UTF8.GetBytes(responseHtml);
            var response = new HttpResponse("text/html", responseBodyBytes);
            return response;
        }
    }
}
