using NUnit.Framework;
using DepartmentClient;
using DepartmentServer;
using System.Threading.Tasks;
using System.Windows;
using NUnit.Framework.Legacy;

namespace TestProject
{
    public class SuccessfulLogin
    {                
        private HttpRequestHandlerClient httpRequestHandlerClient;
        private DatabaseManager databaseManager;
        private HttpServer server;
        private ClientLogic clientLogic;

        private string url = "http://localhost:8080/";
        private string validName = "mrs_z";
        private string validPassword = "password123";
        
        [SetUp]
        public void Setup()
        {                      
            databaseManager = new DatabaseManager();
            server = new HttpServer(url, databaseManager.GetDatabase());
            httpRequestHandlerClient = new HttpRequestHandlerClient(url);
            clientLogic = new ClientLogic(url);
        }

        [Test]
        public async Task Success()
        {
            string loginUrl = url + "login";
            string requestBody = $"username={validName}&password={validPassword}";
            string responseString = await clientLogic.Connect(loginUrl, requestBody);            
            ClassicAssert.AreEqual("Login successful", responseString);
        }      
    }
}