﻿namespace SoftuniHTTPServer.MvcFramework
{
    using SoftuniHTTPServer.HTTP;
    using SoftuniHTTPServer.MvcFramework.ViewEngine;
    using System.Runtime.CompilerServices;
    using System.Text;

    public abstract class Controller
    {
        private readonly ShsViewEngine viewEngine;

        public Controller()
        {
            this.viewEngine = new ShsViewEngine();
        }

        public HttpRequest Request { get; set; }   

        // CallerMemberName is an attribute that takes the name of the method
        // that is calling View method and it will asign its name to viewPath variable
        public HttpResponse View(
            object viewModel = null,
            [CallerMemberName]string viewPath = null)
        {
            var layout = System.IO.File.ReadAllText("Views/Shared/_Layout.cshtml");
            layout = layout.Replace("@RenderBody()", "___VIEW_GOES_HERE___");
            layout = this.viewEngine.GetHtml(layout, viewModel);

            // by reflection and polimorphysm this.GetType() will refer to
            // the class that inherits Controller class and calls this View
            // method
            var controllerName = this.GetType().Name.Replace("Controller", string.Empty) + "/";

            var viewContent = System.IO.File.ReadAllText("Views/" + controllerName + viewPath + ".cshtml");
            viewContent = this.viewEngine.GetHtml(viewContent, viewModel);

            var responseHtml = layout.Replace("___VIEW_GOES_HERE___", viewContent);

            var responseBodyBytes = Encoding.UTF8.GetBytes(responseHtml);

            var response = new HttpResponse("text/html", responseBodyBytes);

            return response;
        }

        public HttpResponse File(string filePath, string contentType) 
        {
            try
            {
                var fileBytes = System.IO.File.ReadAllBytes(filePath);
                var response = new HttpResponse(contentType, fileBytes);
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        } 

        public HttpResponse Redirect(string url)
        {
            var response = new HttpResponse(HttpStatusCode.Found);
            response.Headers.Add(new Header("Location", url));
            return response;
        }
    }
}
