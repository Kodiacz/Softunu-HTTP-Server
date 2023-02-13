namespace MvcApp.Controllers
{
    using MvcApp.ViewModels;
    using SoftuniHTTPServer.HTTP;
    using SoftuniHTTPServer.MvcFramework;
    using System.Text;

    public class HomeController : Controller
    {
        public HttpResponse Index(HttpRequest request)
        {
            var viewModel = new HomeViewModel();
            viewModel.CurrentYear = DateTime.UtcNow.Year;
            viewModel.Message = "Welcome to Git Repositories";
            return View(viewModel);
        }

        [HttpGet]
        public HttpResponse About(HttpRequest request)
        {
            return this.View();
        }
    }
}
