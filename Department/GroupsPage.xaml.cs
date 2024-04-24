using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for GroupsPage.xaml
    /// </summary>
    public partial class GroupsPage : Page
    {
        private readonly GroupsViewModel _viewModel;

        public GroupsPage()
        {
            InitializeComponent();
            _viewModel = new GroupsViewModel();
            DataContext = _viewModel;
            _viewModel.LoadData();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.SearchGroup();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.DeleteGroup();
            _viewModel.LoadData();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.AddGroup();
            _viewModel.LoadData();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.UpdateGroup();
            _viewModel.LoadData();
        }
    }
}
