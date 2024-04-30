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

        public TeachersPage()
        {
            InitializeComponent();
            _viewModel = new TeachersViewModel();
            DataContext = _viewModel;
            _viewModel.LoadData();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.AddTeacher();
            _viewModel.LoadData();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.UpdateTeacher();
            _viewModel.LoadData();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.SearchTeacher();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.DeleteTeacher();
            _viewModel.LoadData();
        }
    }
}