namespace SoftuniHTTPServer.HTTP
{
    public interface IHttpServer
    {
        Task StartAsync(int port);
    }
}
