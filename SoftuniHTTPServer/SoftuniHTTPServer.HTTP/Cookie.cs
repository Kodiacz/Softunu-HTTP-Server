namespace SoftuniHTTPServer.HTTP
{
    public class Cookie
    {
        public Cookie(string cookieAsString)
        {
            var cookieParts = cookieAsString.Split('=', 2);
            this.Name = cookieParts[0];
            this.Value = cookieParts[1];
        }

        public Cookie(string name, string value) 
        {
            this.Value = value;
            this.Name = name;
        }

        public string Name { get; set; }

        public string Value { get; set; }

        public override string ToString()
        {
            return $"{this.Name}={this.Value}";
        }
    }
}