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
            //try
            //{
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

                var request = new HttpRequest(requestAsString);
                Console.WriteLine($"{request.Method} {request.Path} => {request.Headers.Count} headers");

                HttpResponse response;
                if (this.routeTable.ContainsKey(request.Path))
                {
                    var action = this.routeTable[request.Path];
                    response = action(request);
                }
                else
                {
                    // 404
                    response = new HttpResponse("text/html", new byte[0], HttpStatusCode.NotFound);
                }
                response.Headers.Add(new Header("Server", "SoftuniHttpServer 1.0"));
                response.Cookies.Add(new ResponseCookie("sid", Guid.NewGuid().ToString()) { HttpOnly = true, MaxAge = 60 * 24 * 60 * 60 });

                var responseHeaderBytes = Encoding.UTF8.GetBytes(response.ToString());

                await stream.WriteAsync(responseHeaderBytes);
                await stream.WriteAsync(response.Body);
            }

            tcpClient.Close();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex);
            //}
        }
    }
}
