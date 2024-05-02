using System.Windows;
using System.Windows.Controls;

namespace DepartmentServer
{
    /// <summary>
    /// Represents the page for managing groups.
    /// </summary>
    public partial class GroupsPage : Page
    {
        private readonly GroupsViewModel _viewModel;

        /// <summary>
        /// Constructs a new instance of the GroupsPage class.
        /// </summary>
        public GroupsPage()
        {
            InitializeComponent();
            _viewModel = new GroupsViewModel();
            DataContext = _viewModel;
            _viewModel.LoadData();
        }

        /// <summary>
        /// Event handler for the Search button click event.
        /// Searches for a group.
        /// </summary>
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.SearchGroup();
        }

        /// <summary>
        /// Event handler for the Delete button click event.
        /// Deletes a group.
        /// </summary>
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.DeleteGroup();
            _viewModel.LoadData();
        }

        /// <summary>
        /// Event handler for the Add button click event.
        /// Adds a new group.
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.AddGroup();
            _viewModel.LoadData();
        }

        /// <summary>
        /// Event handler for the Update button click event.
        /// Updates an existing group.
        /// </summary>
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.UpdateGroup();
            _viewModel.LoadData();
        }
    }
}
