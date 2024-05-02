using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DepartmentServer
{
    /// <summary>
    /// Handles HTTP requests for login and group creation.
    /// </summary>
    public class HttpRequestHandler
    {
        private readonly DataClasses1DataContext db;

        /// <summary>
        /// Initializes a new instance of the HttpRequestHandler class with the specified data context.
        /// </summary>
        /// <param name="dataContext">The data context for database access.</param>
        public HttpRequestHandler(DataClasses1DataContext dataContext)
        {
            db = dataContext;
        }

        /// <summary>
        /// Handles a login request.
        /// </summary>
        /// <param name="request">The HTTP request.</param>
        /// <param name="response">The HTTP response.</param>
        public void HandleLoginRequest(HttpListenerRequest request, HttpListenerResponse response)
        {
            try
            {
                string username = null;
                string password = null;
                using (var reader = new StreamReader(request.InputStream, request.ContentEncoding))
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

        /// <summary>
        /// Handles a request to create a new group.
        /// </summary>
        /// <param name="request">The HTTP request.</param>
        /// <param name="response">The HTTP response.</param>
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

        /// <summary>
        /// Asynchronously authenticates a user based on the provided login and password.
        /// </summary>
        /// <param name="login">The login username of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>A Task representing the asynchronous operation, containing the authenticated user if successful; otherwise, null.</returns>
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

        /// <summary>
        /// Synchronously checks if the login credentials (username and password) are valid.
        /// </summary>
        /// <param name="username">The login username.</param>
        /// <param name="password">The login password.</param>
        /// <returns>True if the login credentials are valid; otherwise, false.</returns>
        private bool IsValidLogin(string username, string password)
        {
            var user = AuthenticateUserAsync(username, password).Result;

            if (user != null)
            {
                return true;
            }

            return false;
        }
    }
}
