using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentClient
{
    /// <summary>
    /// Class responsible for client-side logic.
    /// </summary>
    public class ClientLogic
    {
        private readonly HttpRequestHandlerClient requestHandler;

        /// <summary>
        /// Initializes a new instance of the ClientLogic class.
        /// </summary>
        /// <param name="baseUrl">The base URL of the server.</param>
        public ClientLogic(string baseUrl)
        {
            requestHandler = new HttpRequestHandlerClient(baseUrl);
        }

        /// <summary>
        /// Connects to the server asynchronously and returns the response.
        /// </summary>
        /// <param name="loginUrl">The URL for login request.</param>
        /// <param name="requestBody">The request body to be sent.</param>
        /// <returns>The response from the server.</returns>
        public async Task<string> Connect(string loginUrl, string requestBody)
        {
            return await requestHandler.PostAsync(loginUrl, requestBody);
        }
    }
}
