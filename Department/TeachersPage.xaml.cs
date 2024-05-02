using System.Windows;
using System.Windows.Controls;

namespace DepartmentServer
{
    /// <summary>
    /// Interaction logic for TeachersPage.xaml
    /// </summary>
    public partial class TeachersPage : Page
    {
        private readonly TeachersViewModel _viewModel;

        /// <summary>
        /// Initializes a new instance of the TeachersPage class.
        /// </summary>
        public TeachersPage()
        {
            InitializeComponent();
            _viewModel = new TeachersViewModel();
            DataContext = _viewModel;
            _viewModel.LoadData();
        }

        /// <summary>
        /// Handles the Click event of the Add button.
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.AddTeacher();
            _viewModel.LoadData();
        }

        /// <summary>
        /// Handles the Click event of the Update button.
        /// </summary>
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.UpdateTeacher();
            _viewModel.LoadData();
        }

        /// <summary>
        /// Handles the Click event of the Search button.
        /// </summary>
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.SearchTeacher();
        }

        /// <summary>
        /// Handles the Click event of the Delete button.
        /// </summary>
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.DeleteTeacher();
            _viewModel.LoadData();
        }
    }
}
