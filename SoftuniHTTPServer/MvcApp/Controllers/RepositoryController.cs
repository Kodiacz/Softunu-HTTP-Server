namespace MvcApp.Controllers
{
    using MvcApp.ViewModels;
    using SoftuniHTTPServer.HTTP;
    using SoftuniHTTPServer.MvcFramework;

    public class RepositoryController : Controller
    {
        [HttpGet("/repositories/all")]
        public HttpResponse All()
        {
            return View();
        }

        [HttpGet("/Repositories/Create")]
        public HttpResponse Create()
        {
            return View();
        }

        [HttpPost("/Repositories/Create")]
        public HttpResponse PostCreate()
        {
            var test = this.Request;
            //var viewModel = new CreateRepositoryViewModel()
            //{
            //    Name = this.Request.FormData["name"],
            //    Type = this.Request.FormData["type"],
            //};

            return View();
        }
    }
}
