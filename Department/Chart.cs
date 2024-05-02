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
    /// <summary>
    /// Represents a chart for displaying data related to groups and students.
    /// </summary>
    public class Chart
    {
        private readonly WpfPlot plot;
        private readonly GroupsViewModel _viewModelGroups;
        private readonly StudentsViewModel _viewModelStudents;

        /// <summary>
        /// Constructs a new instance of the Chart class.
        /// </summary>
        /// <param name="_plot">The WpfPlot object to be used for plotting.</param>
        public Chart(WpfPlot _plot)
        {
            plot = _plot;
            _viewModelGroups = new GroupsViewModel();
            _viewModelStudents = new StudentsViewModel();
            _viewModelStudents.LoadData();
            _viewModelGroups.LoadData();
        }

        /// <summary>
        /// Plots a scatter plot representing the number of students in each group.
        /// </summary>
        public void PlotScatter()
        {
            List<int> groupIds = new List<int>();
            List<int> studentCounts = new List<int>();

            // Calculate the number of students in each group
            foreach (var group in _viewModelGroups.GroupData)
            {
                groupIds.Add(group.group_id);
                studentCounts.Add(_viewModelStudents.StudentData.Count(student => student.group_fk == group.group_id));
            }

            // Clear the plot and add the scatter plot
            plot.Plot.Clear();
            plot.Plot.Add.Scatter(groupIds, studentCounts);
            plot.Plot.Title("Number of Students in Each Group");
            plot.Refresh();
        }

        /// <summary>
        /// Saves the plot as a PNG image file.
        /// </summary>
        public void SavePlotAsPng()
        {
            // Open a save file dialog for selecting the file path
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PNG Image|*.png";
            saveFileDialog.Title = "Save Plot As";

            // If the user selects a file path and confirms, save the plot as a PNG image
            if (saveFileDialog.ShowDialog() == true)
            {
                plot.Plot.SavePng(saveFileDialog.FileName, 700, 410);
                MessageBox.Show($"Plot saved as {saveFileDialog.FileName}");
            }
        }
    }
}
