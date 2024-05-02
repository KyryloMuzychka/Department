using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentClient
{
    /// <summary>
    /// Class responsible for handling HTTP requests to the server.
    /// </summary>
    public class HttpRequestHandlerClient
    {
        private readonly HttpClient client;

        /// <summary>
        /// Initializes a new instance of the HttpRequestHandlerClient class.
        /// </summary>
        /// <param name="baseUrl">The base URL of the server.</param>
        public HttpRequestHandlerClient(string baseUrl)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
        }

        /// <summary>
        /// Sends a POST request to the specified endpoint with the provided request body asynchronously.
        /// </summary>
        /// <param name="endpoint">The endpoint to send the request to.</param>
        /// <param name="requestBody">The request body to send.</param>
        /// <returns>The response from the server as a string.</returns>
        public async Task<string> PostAsync(string endpoint, string requestBody)
        {
            try
            {
                StringContent content = new StringContent(requestBody, Encoding.UTF8, "application/x-www-form-urlencoded");
                HttpResponseMessage response = await client.PostAsync(endpoint, content);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    return response.StatusCode.ToString();
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
