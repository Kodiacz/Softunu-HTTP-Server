using SoftuniHTTPServer.HTTP;

IHttpServer server = new HttpServer();

server.AddRoute("/", HomePage);
server.AddRoute("/about", About);
server.AddRoute("/users/login", Login);

await server.StartAsync(80);

static HttpResponse HomePage(HttpRequest request)
{
    throw new NotImplementedException();
}

static HttpResponse About(HttpRequest request)
{
    throw new NotImplementedException();
}

static HttpResponse Login(HttpRequest request)
{
    throw new NotImplementedException();
}