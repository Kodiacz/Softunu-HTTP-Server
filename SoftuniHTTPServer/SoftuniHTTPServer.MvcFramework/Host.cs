using SoftuniHTTPServer.HTTP;
using HttpMethod = SoftuniHTTPServer.HTTP.HttpMethod;

namespace SoftuniHTTPServer.MvcFramework
{
    public static class Host
    {
        public static async Task CreateHostAsync(IMvcApplication app, int port = 80)
        {
            // TODO: {controller}/{action}/{id}
            List<Route> routeTable = new List<Route>();
            RegisterStaticFiles(routeTable);
            app.ConfigureServices();
            app.Configure(routeTable);
            IHttpServer server = new HttpServer(routeTable);

            await server.StartAsync(port);
        }

        private static void RegisterStaticFiles(List<Route> routeTable)
        {
            var staticFiles = Directory.GetFiles("wwwroot", "*", SearchOption.AllDirectories);
            foreach (var file in staticFiles)
            {
                var url = file
                    .Replace("wwwroot", string.Empty)
                    .Replace("\\", "/");

                routeTable.Add(new Route(url, HttpMethod.Get, (request) =>
                {
                    var fileContent = File.ReadAllBytes(file);
                    var fileExt = new FileInfo(file).Extension;
                    var contnentType = fileExt switch
                    {
                        ".txt" => "text/plain",
                        ".js" => "text/javascript",
                        ".css" => "text/css",
                        ".jpg" => "image/jpg",
                        ".jpeg" => "image/jpeg",
                        ".png" => "image/png",
                        ".gif" => "image/gif",
                        ".ico" => "image/vnd.microsoft.icon",
                        ".html" => "text/hml",
                        _ => "text/plain",
                    };
                    return new HttpResponse(contnentType, fileContent, HttpStatusCode.Ok);
                }));
            }
        }
    }
}
