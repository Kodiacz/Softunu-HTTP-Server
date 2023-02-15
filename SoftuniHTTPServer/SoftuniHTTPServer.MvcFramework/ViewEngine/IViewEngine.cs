namespace SoftuniHTTPServer.MvcFramework.ViewEngine
{
    public interface IViewEngine
    {
        string GetHtml(string templateCode, object viewModel, object user);
    }
}
