namespace SoftuniHTTPServer.MvcFramework
{
    using SoftuniHTTPServer.HTTP;

    public class HttpGetAttribute : BaseHttpAttribute
    {
        public HttpGetAttribute()
        {
        }

        public HttpGetAttribute(string url)
        {
            this.Url = url;
        }

        public override HttpMethod Method => HttpMethod.Get;

        public override string Url { get; set; }
    }


}
