using SoftuniHTTPServer.HTTP;
using System.Text;

IHttpServer server = new HttpServer();

server.AddRoute("/", HomePage);
server.AddRoute("/about", About);
server.AddRoute("/favicon.ico", Favicon);
server.AddRoute("/users/login", Login);

await server.StartAsync(80);

static HttpResponse HomePage(HttpRequest request)
{
    var responseHtml = "<h1>Welcome</h1>" +
                    request.Headers.FirstOrDefault(x => x.Name == "User-Agent")?.Value;
    var responseBodyBytes = Encoding.UTF8.GetBytes(responseHtml);
    var response = new HttpResponse("text/html", responseBodyBytes);
    return response;
}

static HttpResponse About(HttpRequest request)
{
    var responseHtml = "<h1>About...</h1>";
    var responseBodyBytes = Encoding.UTF8.GetBytes(responseHtml);
    var response = new HttpResponse("text/html", responseBodyBytes);
    return response;
}

static HttpResponse Login(HttpRequest request)
{
    throw new NotImplementedException();
}

static HttpResponse Favicon(HttpRequest request)
{
    var fileBytes = File.ReadAllBytes("wwwroot/favicon.ico");
    var response = new HttpResponse("image.vnd.microsoft.icon", fileBytes);
    return response;
}