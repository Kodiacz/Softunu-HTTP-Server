namespace MvcApp.Controllers
{
    using SoftuniHTTPServer.HTTP;
    using SoftuniHTTPServer.MvcFramework;
    using System;

    public class StaticFilesController : Controller
    {
        public HttpResponse Favicon(HttpRequest request)
        {
            return File("wwwroot/favicon.ico", "image/vnd.microsoft.icon");
        }

        public HttpResponse VendorBootStrapCss(HttpRequest arg)
        {
            return File("wwwroot/vendor/bootstrap/bootstrap.min.css", "text/css");
        }

        public HttpResponse LazyCss(HttpRequest arg)
        {
            return File("wwwroot/css/lazy.css", "text/css");
        }

        public HttpResponse LazyJs(HttpRequest arg)
        {
            return File("wwwroot/js/lazy.js", "text/javascript");
        }

        public HttpResponse DemoCss(HttpRequest arg)
        {
            return File("wwwroot/css/demo.css", "text/css");
        }

        public HttpResponse CssBootStrapCss(HttpRequest arg)
        {
            return File("wwwroot/css/bootstrap.min.css", "text/css");
        }

        public HttpResponse CssBootStrapJs(HttpRequest arg)
        {
            return File("wwwroot/css/bootstrap.min.js", "text/javascript");
        }

        public HttpResponse Jquery(HttpRequest arg)
        {
            return File("wwwroot/vendor/jquery/jquery.min.js", "text/javascript");
        }

        public HttpResponse Popper(HttpRequest arg)
        {
            return File("wwwroot/vendor/popper/popper.min.js", "text/javascript");
        }

        public HttpResponse Site(HttpRequest arg)
        {
            return File("wwwroot/css/site.css", "text/css");
        }

        public HttpResponse VendorBootStrapJs(HttpRequest arg)
        {
            return File("wwwroot/vendor/bootstrap/bootstrap.min.js", "text/javascript");
        }

        public HttpResponse Text(HttpRequest arg)
        {
            return File("wwwroot/css/text.css", "text/css");
        }

        public HttpResponse ResetCss(HttpRequest arg)
        {
            return File("wwwroot/css/reset-css.css", "text/css");
        }

        public HttpResponse Noislider(HttpRequest arg)
        {
            return File("wwwroot/vendor/nouislider/js/nouislider.min.js", "text/javascript");
        }
    }
}
