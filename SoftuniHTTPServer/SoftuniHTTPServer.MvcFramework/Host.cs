using SoftuniHTTPServer.HTTP;

namespace SoftuniHTTPServer.MvcFramework
{
    public static class Host
    {
        public static async Task CreateHostAsync(IMvcApplication app, int port = 80)
        {
            // TODO: {controller}/{action}/{id}
            List<Route> routeTable = new List<Route>();
            app.ConfigureServices();
            app.Configure(routeTable);
            IHttpServer server = new HttpServer(routeTable);

            await server.StartAsync(port);
        }
    }
}
