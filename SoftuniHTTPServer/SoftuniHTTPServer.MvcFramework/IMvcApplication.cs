namespace SoftuniHTTPServer.MvcFramework
{
    using SoftuniHTTPServer.HTTP;

    public interface IMvcApplication
    {
        void ConfigureServices(IServiceCollection serviceCollection);

        void Configure(List<Route> routeTable);
    }
}
