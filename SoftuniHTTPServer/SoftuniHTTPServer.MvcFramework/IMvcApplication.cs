namespace SoftuniHTTPServer.MvcFramework
{
    using SoftuniHTTPServer.HTTP;

    public interface IMvcApplication
    {
        void ConfigureServices();

        void Configure(List<Route> routeTable);
    }
}
