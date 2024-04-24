using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;

                byte[] buffer = Encoding.UTF8.GetBytes("Hello, client!");

                response.ContentType = "text/plain";
                response.ContentLength64 = buffer.Length;
                response.OutputStream.Write(buffer, 0, buffer.Length);
                response.Close();
            }
            catch (HttpListenerException ex)
            {                
                Console.WriteLine("HttpListenerException: " + ex.Message);
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
            Frame.Content = teachersPage;
        }

        private void Students_Click(object sender, RoutedEventArgs e)
        {            
            if (studentsPage == null)
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
            Frame.Content = groupsPage;
        }
    }
}
