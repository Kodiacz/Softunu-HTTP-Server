namespace MvcApp
{
    using MvcApp.Controllers;
    using SoftuniHTTPServer.HTTP;
    using SoftuniHTTPServer.MvcFramework;
    using System.Collections.Generic;

    public class Startup : IMvcApplication
    {
        public void Configure(List<Route> routeTable)
        {
            routeTable.Add(new Route("/", HttpMethod.Get, new HomeController().Index));
            routeTable.Add(new Route("/about", HttpMethod.Get, new HomeController().About));
            routeTable.Add(new Route("/Users/Login", HttpMethod.Get, new UsersController().Login));
            routeTable.Add(new Route("/Users/Login", HttpMethod.Post, new UsersController().DoLogin));
            routeTable.Add(new Route("/Users/Register", HttpMethod.Get, new UsersController().Register));
            routeTable.Add(new Route("/Repositories/All", HttpMethod.Get, new RepositoryController().All));
            routeTable.Add(new Route("/Repositories/Create", HttpMethod.Get, new RepositoryController().Create));
            routeTable.Add(new Route("/Commits/Create", HttpMethod.Get, new CommitsController().Create));
            routeTable.Add(new Route("/Commits/All", HttpMethod.Get, new CommitsController().All));
            routeTable.Add(new Route("/favicon.ico", HttpMethod.Get, new StaticFilesController().Favicon));
            routeTable.Add(new Route("/css/bootstrap.min.css", HttpMethod.Get, new StaticFilesController().CssBootStrapCss));
            routeTable.Add(new Route("/css/bootstrap.min.js", HttpMethod.Get, new StaticFilesController().CssBootStrapJs));
            routeTable.Add(new Route("/css/lazy.css", HttpMethod.Get, new StaticFilesController().LazyCss));
            routeTable.Add(new Route("/css/demo.css", HttpMethod.Get, new StaticFilesController().DemoCss));
            routeTable.Add(new Route("/css/rese-css.css", HttpMethod.Get, new StaticFilesController().ResetCss));
            routeTable.Add(new Route("/css/site.css", HttpMethod.Get, new StaticFilesController().Site));
            routeTable.Add(new Route("/css/text.css", HttpMethod.Get, new StaticFilesController().Text));
            routeTable.Add(new Route("/js/lazy.js", HttpMethod.Get, new StaticFilesController().LazyJs));
            routeTable.Add(new Route("/vendor/bootstrap/bootstrap.min.css", HttpMethod.Get, new StaticFilesController().VendorBootStrapCss));
            routeTable.Add(new Route("/vendor/bootstrap/bootstrap.min.js", HttpMethod.Get, new StaticFilesController().VendorBootStrapJs));
            routeTable.Add(new Route("/vendor/jquery/jquery.min.js", HttpMethod.Get, new StaticFilesController().Jquery));
            routeTable.Add(new Route("/vendor/popper/popper.min.js", HttpMethod.Get, new StaticFilesController().Popper));
            routeTable.Add(new Route("/vendor/nouislider/js/nouislider.min.js", HttpMethod.Get, new StaticFilesController().Noislider));
        }

        public void ConfigureServices()
        {

        }
    }
}
