using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DepartmentServer
{
    public class HttpServer
    {
        private readonly HttpListener listener;
        private readonly Thread serverThread;
        private readonly HttpRequestHandler requestHandler;

        public HttpServer(string url, DataClasses1DataContext dataContext)
        {
            listener = new HttpListener();
            listener.Prefixes.Add(url);
            listener.Start();
            serverThread = new Thread(StartServer);
            requestHandler = new HttpRequestHandler(dataContext);
            serverThread.Start();
        }

        private void StartServer()
        {
            try
            {
                while (listener.IsListening)
                {
                    HttpListenerContext context = listener.GetContext();
                    HttpListenerRequest request = context.Request;
                    HttpListenerResponse response = context.Response;

                    if (request.HttpMethod == "POST" && request.Url.AbsolutePath == "/login")
                    {
                        requestHandler.HandleLoginRequest(request, response);
                    }
                    else if (request.HttpMethod == "POST" && request.Url.AbsolutePath == "/create_group")
                    {
                        requestHandler.HandleCreateGroup(request, response);
                    }
                    else
                    {
                        response.StatusCode = 404;
                        response.Close();
                    }
                }
            }
            catch (HttpListenerException ex)
            {
            }
        }

        public void Stop()
        {
            listener?.Stop();
            serverThread?.Join();
        }
    }
}
