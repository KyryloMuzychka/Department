using System.Windows;
using System.Windows.Controls;

namespace DepartmentServer
{      
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly HttpServer httpServer;
        private readonly DatabaseManager databaseManager;

        private TeachersPage teachersPage;
        private StudentsPage studentsPage;
        private GroupsPage groupsPage;
        private ChartPage chartPage;
        
        public MainWindow()
        {
            InitializeComponent();
            databaseManager = new DatabaseManager();
            httpServer = new HttpServer("http://localhost:8080/", databaseManager.GetDatabase());
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {            
            httpServer.Stop();
        }

        private void Teachers_Click(object sender, RoutedEventArgs e)
        {
            if (teachersPage == null) {
                teachersPage = new TeachersPage();
            }
            else {
                teachersPage = new TeachersPage();
            }
            Frame.Content = teachersPage;
        }

        private void Students_Click(object sender, RoutedEventArgs e)
        {
            if (studentsPage == null) {
                studentsPage = new StudentsPage();
            }
            else {
                studentsPage = new StudentsPage();
            }
            Frame.Content = studentsPage;
        }

        private void Groups_Click(object sender, RoutedEventArgs e)
        {
            if (groupsPage == null) {
                groupsPage = new GroupsPage();
            }
            else {
                groupsPage = new GroupsPage();
            }
            Frame.Content = groupsPage;
        }       

        private void ChartButton_Click(object sender, RoutedEventArgs e)
        {
            if (chartPage == null) {
                chartPage = new ChartPage();
            }
            else {
                chartPage = new ChartPage();
            }
            Frame.Content = chartPage;
        }
    }
}
