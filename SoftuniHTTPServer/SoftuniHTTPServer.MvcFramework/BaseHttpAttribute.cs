namespace SoftuniHTTPServer.MvcFramework
{
    using SoftuniHTTPServer.HTTP;

    public abstract class BaseHttpAttribute : Attribute
    {
        public abstract string Url { get; set; }

        public abstract HttpMethod Method { get; }
    }
}
