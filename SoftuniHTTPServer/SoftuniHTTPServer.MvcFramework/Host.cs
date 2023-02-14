namespace SoftuniHTTPServer.MvcFramework
{
    using SoftuniHTTPServer.HTTP;
    using HttpMethod = SoftuniHTTPServer.HTTP.HttpMethod;

    public static class Host
    {
        public static async Task CreateHostAsync(IMvcApplication app, int port = 80)
        {
            // TODO: {controller}/{action}/{id}
            List<Route> routeTable = new List<Route>();
            RegisterStaticFiles(routeTable);
            RegisterRoutes(routeTable, app);
            app.ConfigureServices();
            app.Configure(routeTable);
            IHttpServer server = new HttpServer(routeTable);

            await server.StartAsync(port);
        }

        private static void RegisterRoutes(List<Route> routeTable, IMvcApplication app)
        {
            // give me all types that are class, that are not abstract and those that
            // inherit from Controller class (Reflection)
            var controllerTypes = app.GetType().Assembly.GetTypes()
                .Where(x => x.IsClass && !x.IsAbstract && x.IsSubclassOf(typeof(Controller)));

            foreach (var controller in controllerTypes)
            {
                var methods = controller.GetMethods()
                    .Where(x => x.IsPublic &&
                                !x.IsStatic &&
                                x.DeclaringType == controller &&
                                !x.IsAbstract &&
                                !x.IsConstructor &&
                                !x.IsSpecialName);

                foreach (var method in methods)
                {
                    var url = "/" + controller.Name.Replace("Controller", string.Empty)
                        + "/" + method.Name;

                    var attribute = method.GetCustomAttributes(false)
                        .Where(x => x.GetType().IsSubclassOf(typeof(BaseHttpAttribute)))
                        .FirstOrDefault() as BaseHttpAttribute;

                    var httpMethod = HttpMethod.Get;

                    if (attribute != null)
                    {
                        httpMethod = attribute.Method;
                    }

                    if (!string.IsNullOrEmpty(attribute?.Url))
                    {
                        url = attribute.Url;
                    }

                    routeTable.Add(new Route(url, httpMethod, (request) =>
                    {
                        var instance = Activator.CreateInstance(controller) as Controller;
                        instance.Request = request;
                        var res = method.Invoke(instance, new object[] { }) as HttpResponse;
                        return res;
                    }));

                }
            }
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
