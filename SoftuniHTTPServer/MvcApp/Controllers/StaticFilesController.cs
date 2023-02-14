namespace MvcApp.Controllers
{
    using SoftuniHTTPServer.HTTP;
    using SoftuniHTTPServer.MvcFramework;
    using System;

    public class StaticFilesController : Controller
    {
        public HttpResponse Favicon()
        {
            return File("wwwroot/favicon.ico", "image/vnd.microsoft.icon");
        }

        public HttpResponse VendorBootStrapCss()
        {
            return File("wwwroot/vendor/bootstrap/bootstrap.min.css", "text/css");
        }

        public HttpResponse LazyCss()
        {
            return File("wwwroot/css/lazy.css", "text/css");
        }

        public HttpResponse LazyJs()
        {
            return File("wwwroot/js/lazy.js", "text/javascript");
        }

        public HttpResponse DemoCss()
        {
            return File("wwwroot/css/demo.css", "text/css");
        }

        public HttpResponse CssBootStrapCss()
        {
            return File("wwwroot/css/bootstrap.min.css", "text/css");
        }

        public HttpResponse CssBootStrapJs()
        {
            return File("wwwroot/css/bootstrap.min.js", "text/javascript");
        }

        public HttpResponse Jquery()
        {
            return File("wwwroot/vendor/jquery/jquery.min.js", "text/javascript");
        }

        public HttpResponse Popper()
        {
            return File("wwwroot/vendor/popper/popper.min.js", "text/javascript");
        }

        public HttpResponse Site()
        {
            return File("wwwroot/css/site.css", "text/css");
        }

        public HttpResponse VendorBootStrapJs()
        {
            return File("wwwroot/vendor/bootstrap/bootstrap.min.js", "text/javascript");
        }

        public HttpResponse Text()
        {
            return File("wwwroot/css/text.css", "text/css");
        }

        public HttpResponse ResetCss()
        {
            return File("wwwroot/css/reset-css.css", "text/css");
        }

        public HttpResponse Noislider()
        {
            return File("wwwroot/vendor/nouislider/js/nouislider.min.js", "text/javascript");
        }
    }
}
