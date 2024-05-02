using System.Windows;
using System.Windows.Controls;

namespace DepartmentServer
{
    /// <summary>
    /// Interaction logic for StudentsPage.xaml
    /// </summary>
    public partial class StudentsPage : Page
    {
        /// <summary>
        /// View model instance for managing student data.
        /// </summary>
        private readonly StudentsViewModel _viewModel;

        /// <summary>
        /// Constructor for StudentsPage class.
        /// Initializes a new instance of the StudentsPage class and sets up the view model.
        /// </summary>
        public StudentsPage()
        {
            InitializeComponent();
            _viewModel = new StudentsViewModel();
            DataContext = _viewModel;
            _viewModel.LoadData();
        }

        /// <summary>
        /// Event handler for the Add button click event.
        /// Adds a new student and reloads the data.
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.AddStudent();
            _viewModel.LoadData();
        }

        /// <summary>
        /// Event handler for the Update button click event.
        /// Updates a student's information and reloads the data.
        /// </summary>
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.UpdateStudent();
            _viewModel.LoadData();
        }

        /// <summary>
        /// Event handler for the Search button click event.
        /// Searches for a student.
        /// </summary>
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.SearchStudent();
        }

        /// <summary>
        /// Event handler for the Delete button click event.
        /// Deletes a student and reloads the data.
        /// </summary>
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.DeleteStudent();
            _viewModel.LoadData();
        }
    }
}
