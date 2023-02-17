namespace SoftuniHTTPServer.MvcFramework
{
    using SoftuniHTTPServer.HTTP;
    using System.Reflection;
    using HttpMethod = SoftuniHTTPServer.HTTP.HttpMethod;

    public static class Host 
    {
        public static async Task CreateHostAsync(IMvcApplication app, int port = 80)
        {
            // TODO: {controller}/{action}/{id}
            List<Route> routeTable = new List<Route>();
            IServiceCollection serviceCollection = new ServiceCollection();

            app.ConfigureServices(serviceCollection);
            app.Configure(routeTable);

            RegisterStaticFiles(routeTable);
            RegisterRoutes(routeTable, app, serviceCollection);

            IHttpServer server = new HttpServer(routeTable);

            await server.StartAsync(port);
        }

        private static void RegisterRoutes(List<Route> routeTable, IMvcApplication app, IServiceCollection serviceCollection)
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

                    routeTable.Add(new Route(
                        url, 
                        httpMethod, 
                        request => ExecuteAction(request, controller, method, serviceCollection)));

                }
            }
        }

        private static HttpResponse ExecuteAction(
            HttpRequest request, 
            Type controller, 
            MethodInfo action, 
            IServiceCollection serviceCollection)
        {
            var instance = serviceCollection.CreateInstance(controller) as Controller;
            instance.Request = request;
            var arguments = new List<object> { };
            var parametars = action.GetParameters();
            foreach (var param in parametars)
            {
                var httpParamValue = GetParametarFromRequest(request, param.Name);
                var paramValue = Convert.ChangeType(request, param.ParameterType);
                if (paramValue == null && param.ParameterType != typeof(string))
                {
                    paramValue = Activator.CreateInstance(param.ParameterType);
                    var properties = paramValue.GetType().GetRuntimeProperties();
                    foreach (var property in properties)
                    {
                        var propertyHttpParamValue = GetParametarFromRequest(request, property.Name);
                        var propertyParamValue = Convert.ChangeType(request, property.PropertyType);
                        property.SetValue(paramValue, propertyParamValue);
                    }
                }

                arguments.Add(paramValue);
            }
            var res = action.Invoke(instance, arguments.ToArray()) as HttpResponse;
            return res;
        }

        private static string GetParametarFromRequest(HttpRequest request, string parametarName)
        {
            parametarName = parametarName.ToLower();
            if (request.FormData.Any(x => x.Key.ToLower() == parametarName))
            {
                return request.FormData
                    .FirstOrDefault(x => x.Key.ToLower() == parametarName).Value;
            }
            
            if (request.QueryData.Any(x => x.Key.ToLower() == parametarName))
            {
                return request.QueryData
                    .FirstOrDefault(x => x.Key.ToLower() == parametarName).Value;
            }

            return null;
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
