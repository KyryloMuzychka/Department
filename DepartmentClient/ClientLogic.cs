using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentClient
{
    public class ClientLogic
    {
        private readonly HttpRequestHandlerClient requestHandler;

        public ClientLogic(string baseUrl)
        {
            requestHandler = new HttpRequestHandlerClient(baseUrl);
        }

        public async Task<string> Connect(string loginUrl, string requestBody)
        {
            return await requestHandler.PostAsync(loginUrl, requestBody);
        }
    }
}