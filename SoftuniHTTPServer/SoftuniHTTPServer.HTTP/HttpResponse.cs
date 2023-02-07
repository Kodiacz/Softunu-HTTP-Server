﻿namespace SoftuniHTTPServer.HTTP
{
    using System.Text;
    using static SoftuniHTTPServer.HTTP.HttpConstants;

    public class HttpResponse
    {
        public HttpResponse(string contentType, byte[] body, HttpStatusCode statusCode = HttpStatusCode.Ok)
        {
            if (body == null)
            {
                throw new ArgumentNullException(nameof(body));
            }

            StatusCode = statusCode;
            Body = body;
            this.Headers = new List<Header>()
            {
                {new Header("Content-Type", contentType) },
                {new Header("Content-Length", body.Length.ToString())},
            };
            this.Cookies = new List<Cookie>();
        }

        public HttpStatusCode StatusCode { get; set; }

        public ICollection<Header> Headers { get; set; }

        public ICollection<Cookie> Cookies { get; set; }

        public byte[] Body { get; set; }

        public override string ToString()
        {
            StringBuilder responseBuilder = new StringBuilder();
            responseBuilder.Append($"HTTP/1.1 {(int)this.StatusCode} {this.StatusCode}"+NewLine);
            foreach (var header in this.Headers)
            {
                responseBuilder.Append(header.ToString() + NewLine);
            }

            foreach (var cookie in this.Cookies)
            {
                responseBuilder.Append("Set-Cookie: " + cookie.ToString() + NewLine);
            }

            responseBuilder.Append(NewLine);

            return responseBuilder.ToString();
        }
    }
}