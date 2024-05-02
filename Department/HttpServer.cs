using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DepartmentServer
{
    /// <summary>
    /// Represents an HTTP server for handling login and group creation requests.
    /// </summary>
    public class HttpServer
    {
        private readonly HttpListener listener;
        private readonly Thread serverThread;
        private readonly HttpRequestHandler requestHandler;

        /// <summary>
        /// Initializes a new instance of the HttpServer class with the specified URL prefix and data context.
        /// </summary>
        /// <param name="url">The URL prefix on which the server will listen for incoming HTTP requests.</param>
        /// <param name="dataContext">The data context for database access.</param>
        public HttpServer(string url, DataClasses1DataContext dataContext)
        {
            listener = new HttpListener();
            listener.Prefixes.Add(url);
            listener.Start();
            serverThread = new Thread(StartServer);
            requestHandler = new HttpRequestHandler(dataContext);
            serverThread.Start();
        }

        /// <summary>
        /// Starts the HTTP server and listens for incoming requests.
        /// </summary>
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
                // Handle HttpListenerException
            }
        }

        /// <summary>
        /// Stops the HTTP server.
        /// </summary>
        public void Stop()
        {
            listener?.Stop();
            serverThread?.Join();
        }
    }
}
