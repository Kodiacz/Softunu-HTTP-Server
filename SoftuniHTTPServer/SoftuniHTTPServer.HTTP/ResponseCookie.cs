using System.Text;

namespace SoftuniHTTPServer.HTTP
{
    public class ResponseCookie : Cookie
    {
        public ResponseCookie(string name, string value)
            : base(name, value)
        {
            this.Path = "/";
        }

        public int MaxAge { get; set; }

        public bool HttpOnly { get; set; }

        public string Path { get; set; }

        public override string ToString()
        {
            StringBuilder cookieBuilder = new StringBuilder();
            cookieBuilder.Append($"{this.Name}={this.Value}; Path={this.Path};");

            // If MaxAge is not set then the default value will be 0
            if (MaxAge != 0)
            {
                cookieBuilder.Append($" Max-Age={this.MaxAge};");
            }

            if (HttpOnly)
            {
                cookieBuilder.Append($" Http-Only;");
            }

            return cookieBuilder.ToString();
        }
    }
}
