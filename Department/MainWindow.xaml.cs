using System.Windows;
using System.Windows.Controls;

namespace DepartmentServer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// HTTP server instance for handling requests.
        /// </summary>
        private readonly HttpServer httpServer;

        /// <summary>
        /// Database manager instance for managing database operations.
        /// </summary>
        private readonly DatabaseManager databaseManager;

        /// <summary>
        /// Page instance for displaying teachers' information.
        /// </summary>
        private TeachersPage teachersPage;

        /// <summary>
        /// Page instance for displaying students' information.
        /// </summary>
        private StudentsPage studentsPage;

        /// <summary>
        /// Page instance for displaying groups' information.
        /// </summary>
        private GroupsPage groupsPage;

        /// <summary>
        /// Page instance for displaying charts.
        /// </summary>
        private ChartPage chartPage;

        /// <summary>
        /// Constructor for MainWindow class.
        /// Initializes a new instance of the MainWindow class, including HTTP server and database manager.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            databaseManager = new DatabaseManager();
            httpServer = new HttpServer("http://localhost:8080/", databaseManager.GetDatabase());
        }

        /// <summary>
        /// Event handler for the window closing event.
        /// Stops the HTTP server when the window is closing.
        /// </summary>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            httpServer.Stop();
        }

        /// <summary>
        /// Event handler for the Teachers button click event.
        /// Displays the TeachersPage in the frame.
        /// </summary>
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

        /// <summary>
        /// Event handler for the Students button click event.
        /// Displays the StudentsPage in the frame.
        /// </summary>
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

        /// <summary>
        /// Event handler for the Groups button click event.
        /// Displays the GroupsPage in the frame.
        /// </summary>
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

        /// <summary>
        /// Event handler for the Chart button click event.
        /// Displays the ChartPage in the frame.
        /// </summary>
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
