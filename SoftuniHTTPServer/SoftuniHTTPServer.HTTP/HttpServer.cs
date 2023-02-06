namespace SoftuniHTTPServer.HTTP
{
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using static SoftuniHTTPServer.HTTP.HttpConstants;

    public class HttpServer : IHttpServer
    {
        IDictionary<string, Func<HttpRequest, HttpResponse>>
            routeTable = new Dictionary<string, Func<HttpRequest, HttpResponse>>();

        public void AddRoute(string path, Func<HttpRequest, HttpResponse> action)
        {
            if (routeTable.ContainsKey(path))
            {
                routeTable[path] = action;
            }
            else
            {
                routeTable.Add(path, action);
            }


        }

        public async Task StartAsync(int port)
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Loopback, port);
            tcpListener.Start();

            while (true)
            {
                TcpClient tcpClient = await tcpListener.AcceptTcpClientAsync();
                ProcessClient(tcpClient);
            }
        }

        private async Task ProcessClient(TcpClient tcpClient)
        {
            using (NetworkStream stream = tcpClient.GetStream())
            {
                // TODO: research if there is faster data structure for array of bytes
                List<byte> data = new List<byte>();
                int position = 0;
                byte[] buffer = new byte[BufferSize];
                while (true)
                {
                    int count = await stream.ReadAsync(buffer, position, buffer.Length); ;

                    position += count;

                    // because ReadAsync method will read 4092 bytes of information
                    // but the last part may be less then 4092 and the rest bytes will
                    // be useless. So we need a way to get rid of does useless bytes
                    if (count < buffer.Length)
                    {
                        var bufferWithData = new byte[count];
                        Array.Copy(buffer, bufferWithData, count);
                        data.AddRange(bufferWithData);
                        break;
                    }
                    else
                    {
                        data.AddRange(buffer);
                    }
                }

                // byte[] => string (text) => this is called Encoding 
                // (ASCII is one of the ways for encoding, Unicode is another, UTF8)

                var requestAsString = Encoding.UTF8.GetString(data.ToArray());

                Console.WriteLine(requestAsString);

                // TODO: extract information from infoRequestAsString

                var responseHtml = "<h1>Welcome</h1>";

                var responseBodyBytes = Encoding.UTF8.GetBytes(responseHtml);

                var responseHttp = "HTTP/1:1" + NewLine +
                    "Server: SoftuniHttpServer 1.0" + NewLine +
                    "Content-Type: text-html" + NewLine +
                    "Content-Length: " + responseBodyBytes.Length + NewLine +
                    NewLine;

                var responseHeaderBytes = Encoding.UTF8.GetBytes(responseHttp);

                await stream.WriteAsync(responseHeaderBytes);
                await stream.WriteAsync(responseBodyBytes);
            }

            tcpClient.Close();
        }
    }
}
