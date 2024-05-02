using DepartmentServer;
using System.Threading.Tasks;
using System.Windows;
using NUnit.Framework.Legacy;
using DepartmentClient;
using NUnit.Framework;

namespace TestProject
{
    public class ErrorLogin
    {
        private HttpRequestHandlerClient httpRequestHandlerClient;
        private DatabaseManager databaseManager;
        private HttpServer server;
        private ClientLogic clientLogic;

        private string url = "http://localhost:8081/";
        private string invalidName = "just_name";
        private string invalidPassword = "just_password";

        [SetUp]
        public void Setup()
        {
            databaseManager = new DatabaseManager();
            server = new HttpServer(url, databaseManager.GetDatabase());
            httpRequestHandlerClient = new HttpRequestHandlerClient(url);
            clientLogic = new ClientLogic(url);
        }

        [Test]
        public async Task Error()
        {
            string loginUrl = url + "login";
            string requestBody = $"username={invalidName}&password={invalidPassword}";
            string responseString = await clientLogic.Connect(loginUrl, requestBody);            
            ClassicAssert.AreEqual("Unauthorized", responseString);
        }
    }
}
