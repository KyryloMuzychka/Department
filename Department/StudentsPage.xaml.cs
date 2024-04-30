using System.Windows;
using System.Windows.Controls;


namespace DepartmentServer
{
    /// <summary>
    /// Interaction logic for StudentsPage.xaml
    /// </summary>
    public partial class StudentsPage : Page
    {
        private readonly StudentsViewModel _viewModel;

        public StudentsPage()
        {
            InitializeComponent();
            _viewModel = new StudentsViewModel();
            DataContext = _viewModel;
            _viewModel.LoadData();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.AddStudent();
            _viewModel.LoadData();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.UpdateStudent();
            _viewModel.LoadData();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.SearchStudent();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.DeleteStudent();
            _viewModel.LoadData();
        }
    }
}