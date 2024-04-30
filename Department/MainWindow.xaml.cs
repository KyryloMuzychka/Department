using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DepartmentServer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static HttpListener listener;
        private static Thread serverThread;

        private TeachersPage teachersPage;
        private StudentsPage studentsPage;
        private GroupsPage groupsPage;
        private ChartPage chartPage;

        private readonly DataClasses1DataContext db = new DataClasses1DataContext();

        public MainWindow()
        {
            InitializeComponent();

            string url = "http://localhost:8080/";

            listener = new HttpListener();
            listener.Prefixes.Add(url);
            listener.Start();
            serverThread = new Thread(StartServer);
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
                        HandleLoginRequest(request, response);
                    }
                    else if (request.HttpMethod == "POST" && request.Url.AbsolutePath == "/create_group")
                    {
                        HandleCreateGroup(request, response);
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

        private void HandleLoginRequest(HttpListenerRequest request, HttpListenerResponse response)
        {
            try
            {
                string username = null;
                string password = null;
                using (var reader = new System.IO.StreamReader(request.InputStream, request.ContentEncoding))
                {
                    string requestBody = reader.ReadToEnd();
                    var parts = requestBody.Split('&');
                    foreach (var part in parts)
                    {
                        var keyValue = part.Split('=');
                        if (keyValue.Length == 2)
                        {
                            var key = keyValue[0];
                            var value = keyValue[1];
                            if (key == "username")
                            {
                                username = WebUtility.UrlDecode(value);
                            }
                            else if (key == "password")
                            {
                                password = WebUtility.UrlDecode(value);
                            }
                        }
                    }
                }
                if (IsValidLogin(username, password))
                {
                    string responseString = "Login successful";
                    byte[] buffer = Encoding.UTF8.GetBytes(responseString);
                    response.StatusCode = 200;
                    response.ContentType = "text/plain";
                    response.ContentLength64 = buffer.Length;
                    response.OutputStream.Write(buffer, 0, buffer.Length);
                }
                else
                {
                    string responseString = "Invalid credentials";
                    byte[] buffer = Encoding.UTF8.GetBytes(responseString);
                    response.StatusCode = 401; // Unauthorized
                    response.ContentType = "text/plain";
                    response.ContentLength64 = buffer.Length;
                    response.OutputStream.Write(buffer, 0, buffer.Length);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while processing login request: " + ex.Message);
                string responseString = "An error occurred while processing the request";
                byte[] buffer = Encoding.UTF8.GetBytes(responseString);
                response.StatusCode = 500;
                response.ContentType = "text/plain";
                response.ContentLength64 = buffer.Length;
                response.OutputStream.Write(buffer, 0, buffer.Length);
            }
            finally
            {
                response.Close();
            }
        }


        private bool IsValidLogin(string username, string password)
        {
            var user = AuthenticateUserAsync(username, password).Result;

            if (user != null)
            {
                return true;
            }

            return false;
        }


        private async Task<Teacher> AuthenticateUserAsync(string login, string password)
        {
            var user = await Task.Run(() =>
            {
                return (from s in db.Teachers where s.teacher_login == login select s).FirstOrDefault();
            });

            if (user != null && user.teacher_password == password)
            {
                return user;
            }
            else
            {
                return null;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            listener?.Stop();
            serverThread?.Join();
        }

        private void Teachers_Click(object sender, RoutedEventArgs e)
        {
            if (teachersPage == null)
            {
                teachersPage = new TeachersPage();
            }
            else
            {
                teachersPage = new TeachersPage();
            }

            Frame.Content = teachersPage;
        }

        private void Students_Click(object sender, RoutedEventArgs e)
        {
            if (studentsPage == null)
            {
                studentsPage = new StudentsPage();
            }
            else
            {
                studentsPage = new StudentsPage();
            }

            Frame.Content = studentsPage;
        }

        private void Groups_Click(object sender, RoutedEventArgs e)
        {
            if (groupsPage == null)
            {
                groupsPage = new GroupsPage();
            }
            else
            {
                groupsPage = new GroupsPage();
            }

            Frame.Content = groupsPage;
        }

        public void HandleCreateGroup(HttpListenerRequest request, HttpListenerResponse response)
        {
            try
            {
                string groupName = null;
                using (var reader = new StreamReader(request.InputStream, request.ContentEncoding))
                {
                    string requestBody = reader.ReadToEnd();
                    var parts = requestBody.Split('&');
                    foreach (var part in parts)
                    {
                        var keyValue = part.Split('=');
                        if (keyValue.Length == 2 && keyValue[0] == "group_name")
                        {
                            groupName = WebUtility.UrlDecode(keyValue[1]);
                            break;
                        }
                    }
                }

                if (string.IsNullOrWhiteSpace(groupName))
                {
                    response.StatusCode = 400;
                    response.Close();
                    return;
                }

                StudentsGroup newGroup = new StudentsGroup { group_name = groupName };
                db.StudentsGroups.InsertOnSubmit(newGroup);
                db.SubmitChanges();

                string responseString = "Group created successfully";
                byte[] buffer = Encoding.UTF8.GetBytes(responseString);
                response.StatusCode = 200;
                response.ContentType = "text/plain";
                response.ContentLength64 = buffer.Length;
                response.OutputStream.Write(buffer, 0, buffer.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while processing create group request: " + ex.Message);
                string responseString = "An error occurred while processing the request";
                byte[] buffer = Encoding.UTF8.GetBytes(responseString);
                response.StatusCode = 500;
                response.ContentType = "text/plain";
                response.ContentLength64 = buffer.Length;
                response.OutputStream.Write(buffer, 0, buffer.Length);
            }
            finally
            {
                response.Close();
            }
        }

        private void ChartButton_Click(object sender, RoutedEventArgs e)
        {
            if (chartPage == null)
            {
                chartPage = new ChartPage();
            }
            else
            {
                chartPage = new ChartPage();
            }

            Frame.Content = chartPage;
        }
    }
}
