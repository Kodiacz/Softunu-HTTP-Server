namespace MvcApp.Controllers
{
    using MvcApp.ViewModels;
    using SoftuniHTTPServer.HTTP;
    using SoftuniHTTPServer.MvcFramework;
    using System.Text;

    public class HomeController : Controller
    {
        [HttpGet("/")]
        public HttpResponse Index()
        {
            var viewModel = new HomeViewModel();
            viewModel.CurrentYear = DateTime.UtcNow.Year;
            viewModel.Message = "Welcome to Git Repositories";
            return View(viewModel);
        }

        

        [HttpGet]
        public HttpResponse About()
        {
            return this.View();
        }
    }
}
