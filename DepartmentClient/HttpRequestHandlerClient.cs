using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentClient
{
    public class HttpRequestHandlerClient
    {
        private readonly HttpClient client;

        public HttpRequestHandlerClient(string baseUrl)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
        }

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