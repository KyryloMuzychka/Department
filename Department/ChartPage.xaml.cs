using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using ScottPlot;

namespace DepartmentServer
{
    /// <summary>
    /// Interaction logic for ChartPage.xaml
    /// </summary>
    public partial class ChartPage : Page
    {
        private readonly GroupsViewModel _viewModel;
        private readonly StudentsViewModel _viewModelStudents;

        public ChartPage()
        {
            InitializeComponent();            
            _viewModel = new GroupsViewModel();
            _viewModelStudents = new StudentsViewModel();
            DataContext = _viewModel;
            _viewModel.LoadData();
            _viewModelStudents.LoadData();
            PlotScatter();
        }

        private void PlotScatter()
        {
            List<int> groupIds = new List<int>();
            List<int> studentCounts = new List<int>();

            foreach (var group in _viewModel.GroupData)
            {
                groupIds.Add(group.group_id);
                studentCounts.Add(_viewModelStudents.StudentData.Count(student => student.group_fk == group.group_id));                
            }
           
            wpfPlot1.Plot.Clear();
            wpfPlot1.Plot.Add.Scatter(groupIds, studentCounts);            
            wpfPlot1.Plot.Title("Number of Students in Each Group");
            wpfPlot1.Refresh();
        }

        private void SaveClickButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {            
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PNG Image|*.png";
            saveFileDialog.Title = "Save Plot As";
            
            if (saveFileDialog.ShowDialog() == true)
            {
                wpfPlot1.Plot.SavePng(saveFileDialog.FileName, 700, 410);
                MessageBox.Show($"Plot saved as {saveFileDialog.FileName}");
            }
        }
    }
}
