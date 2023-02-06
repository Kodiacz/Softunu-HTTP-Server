namespace SoftuniHTTPServer.HTTP
{
    using static SoftuniHTTPServer.HTTP.HttpConstants;

    public class HttpRequest
    {
        public HttpRequest(string requestString)
        {
            var lines = requestString.Split(new string[] { NewLine }, StringSplitOptions.None);
            var HeaderLine = lines[0];
            var headerLineParsts = HeaderLine.Split(' ');
            this.Method = headerLineParsts[0];
            this.Path = headerLineParsts[1];

            int lineIndex = 1;
            bool isInHeaders = true;
            while (lineIndex < lines.Length)
            {
                var line = lines[lineIndex];
                lineIndex++;

                if (isInHeaders)
                {
                    // read header
                }
                else
                {
                    // read body
                }

                if (string.IsNullOrWhiteSpace(line))
                {
                    isInHeaders = false;
                    break;
                }

            }
        }

        public string Path { get; set; }

        public string Method { get; set; }

        public List<Header> Headers { get; set; }

        public List<Cookie> Cookies { get; set; }

        public string Body { get; set; }
    }
}