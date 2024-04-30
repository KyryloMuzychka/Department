using Microsoft.Win32;
using ScottPlot;
using ScottPlot.WPF;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DepartmentServer
{
    public class Chart
    {
        private readonly WpfPlot plot;
        private readonly GroupsViewModel _viewModelGroups;
        private readonly StudentsViewModel _viewModelStudents;

        public Chart(WpfPlot _plot) {
            plot = _plot;   
            _viewModelGroups = new GroupsViewModel();
            _viewModelStudents = new StudentsViewModel();
            _viewModelStudents.LoadData();
            _viewModelGroups.LoadData();            
        }

        public void PlotScatter()
        {
            List<int> groupIds = new List<int>();
            List<int> studentCounts = new List<int>();

            foreach (var group in _viewModelGroups.GroupData)
            {
                groupIds.Add(group.group_id);
                studentCounts.Add(_viewModelStudents.StudentData.Count(student => student.group_fk == group.group_id));
            }

            plot.Plot.Clear();
            plot.Plot.Add.Scatter(groupIds, studentCounts);
            plot.Plot.Title("Number of Students in Each Group");
            plot.Refresh();
        }

        public void SavePlotAsPng()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PNG Image|*.png";
            saveFileDialog.Title = "Save Plot As";

            if (saveFileDialog.ShowDialog() == true)
            {
                plot.Plot.SavePng(saveFileDialog.FileName, 700, 410);
                MessageBox.Show($"Plot saved as {saveFileDialog.FileName}");
            }
        }

    }
}
