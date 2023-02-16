namespace SoftuniHTTPServer.HTTP
{
    using System.Net;
    using System.Text;
    using static SoftuniHTTPServer.HTTP.HttpConstants;

    public class HttpRequest
    {
        public HttpRequest(string requestString)
        {
            this.Headers = new List<Header>();
            this.Cookies = new List<Cookie>();
            this.FormData = new Dictionary<string, string>();

            var lines = requestString.Split(new string[] { NewLine }, StringSplitOptions.None);

            var HeaderLine = lines[0];
            var headerLineParsts = HeaderLine.Split(' ');
            //this.Method = (HttpMethod)Enum.Parse(typeof(HttpMethod), headerLineParsts[0], true);
            //this.Path = headerLineParsts[1];


            var successParse = Enum.TryParse(headerLineParsts[0], true, out HttpMethod method);
            if (successParse)
            {
                this.Method = method;
            }

            if (headerLineParsts.Length > 1)
            {
                this.Path = headerLineParsts[1];
            }


            int lineIndex = 1;
            bool isInHeaders = true;
            StringBuilder bodyBuilder = new StringBuilder();
            while (lineIndex < lines.Length)
            {
                var line = lines[lineIndex];
                lineIndex++;

                if (string.IsNullOrWhiteSpace(line))
                {
                    isInHeaders = false;
                    continue;
                }

                if (isInHeaders)
                {
                    // read header (separation of concerns)
                    this.Headers.Add(new Header(line));
                }
                else
                {
                    // read body
                    bodyBuilder.AppendLine(line);
                }

            }

            if (this.Headers.Any(x => x.Name == RequestCookieHeader))
            {
                var cookiesAsString = this.Headers.FirstOrDefault(x => x.Name == RequestCookieHeader)!.Value;
                var cookies = cookiesAsString.Split("; ", StringSplitOptions.RemoveEmptyEntries);
                foreach (var cookieAsString in cookies)
                {
                    this.Cookies.Add(new Cookie(cookieAsString));
                }
            }

            var sessionCookie = this.Cookies.FirstOrDefault(x => x.Name == SessionCookieName);
            if (sessionCookie == null)
            {
                var sessionId = Guid.NewGuid().ToString();
                this.Session = new Dictionary<string, string>();
                Sessions.Add(sessionId, this.Session);
                this.Cookies.Add(new Cookie(SessionCookieName, sessionId));
            }
            else if (!Sessions.ContainsKey(sessionCookie.Value))
            {
                this.Session = new Dictionary<string, string>();
                Sessions.Add(sessionCookie.Value, this.Session);
            }
            else
            {
                this.Session = Sessions[sessionCookie.Value];
            }

            this.Body = bodyBuilder.ToString().TrimEnd('\n', '\r');
            var parametars = this.Body.Split("&", StringSplitOptions.RemoveEmptyEntries);
            foreach (var parametar in parametars)
            {
                var parametarParts = parametar.Split("=", 2);
                var key = parametarParts[0];
                var value = WebUtility.UrlDecode(parametarParts[1]);
                if (!this.FormData.ContainsKey(key))
                {
                    this.FormData.Add(key, value);
                }
            }
        }

        public static IDictionary<string, Dictionary<string, string>>
            Sessions = new Dictionary<string, Dictionary<string, string>>();

        public string Path { get; set; }

        public HttpMethod Method { get; set; }

        public ICollection<Header> Headers { get; set; }

        public ICollection<Cookie> Cookies { get; set; }

        public IDictionary<string, string> FormData { get; set; }

        public Dictionary<string, string> Session { get; set; }

        public string Body { get; set; }
    }
}