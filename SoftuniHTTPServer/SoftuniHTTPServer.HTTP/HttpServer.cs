﻿namespace SoftuniHTTPServer.HTTP
{
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using static SoftuniHTTPServer.HTTP.HttpConstants;

    public class HttpServer : IHttpServer
    {
        List<Route> routeTable;

        public HttpServer(List<Route> routeTable)
        {
            this.routeTable = routeTable;
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
                    int count = await stream.ReadAsync(buffer, position, buffer.Length);

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
                var route = this.routeTable.FirstOrDefault(
                    r => string.Compare(r.Path, request.Path, true) == 0 &&
                    r.Method == request.Method);

                if (route != null)
                {
                    response = route.Action(request);
                }
                else
                {
                    // 404
                    response = new HttpResponse("text/html", new byte[0], HttpStatusCode.NotFound);
                }
                response.Headers.Add(new Header("Server", "SoftuniHttpServer 1.0"));
                var sessionCookie = request.Cookies.FirstOrDefault(x => x.Name == SessionCookieName);
                if (sessionCookie != null)
                {
                    var responseSessionCookie = new ResponseCookie(SessionCookieName, sessionCookie.Value);
                    responseSessionCookie.Path = "/";
                    response.Cookies.Add(responseSessionCookie);
                }

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

