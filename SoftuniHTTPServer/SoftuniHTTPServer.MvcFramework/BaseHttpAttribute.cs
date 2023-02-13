namespace SoftuniHTTPServer.MvcFramework
{
    using SoftuniHTTPServer.HTTP;

    public abstract class BaseHttpAttribute : Attribute
    {
        public string Url { get; set; } = null!;

        public abstract HttpMethod Method { get; }
    }

    public class HttpPostAttribute : BaseHttpAttribute
    {
        public HttpPostAttribute()
        {
        }

        public HttpPostAttribute(string url)
        {
            this.Url = url;
        }
        
        public override HttpMethod Method => HttpMethod.Post;

        public string Url { get; }
    }
    
    public class HttpGetAttribute : BaseHttpAttribute
    {
        public HttpGetAttribute()
        {
        }

        public HttpGetAttribute(string url)
        {
            Url = url;
        }
        public override HttpMethod Method => HttpMethod.Get;

        public string Url { get; }
    }


}
