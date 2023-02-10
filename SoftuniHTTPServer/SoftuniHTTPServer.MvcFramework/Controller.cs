namespace SoftuniHTTPServer.MvcFramework
{
    using SoftuniHTTPServer.HTTP;
    using System.Runtime.CompilerServices;
    using System.Text;

    public abstract class Controller
    //C:\Simeon\Programming\SoftUni\C#-Web-Basics-септември-2020-Nikolay-Kostov\Softunu-HTTP-Server\SoftuniHTTPServer\MvcApp\bin\Debug\net6.0\wwwroot
    {
        // CallerMemberName is an attribute that takes the name of the method
        // that is calling View method and it will asign its name to viewPath variable
        public HttpResponse View([CallerMemberName]string viewPath = null)
        {
            var layout = System.IO.File.ReadAllText("Views/Shared/_Layout.cshtml");

            // by reflection and polimorphysm this.GetType() will refer to
            // the class that inherits Controller class and calls this View
            // method
            var controllerName = this.GetType().Name.Replace("Controller", string.Empty) + "/";
            var viewContent = System.IO.File.ReadAllText("Views/" + controllerName + viewPath + ".cshtml");
            var responseHtml = layout.Replace("@RenderBody()", viewContent);
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
