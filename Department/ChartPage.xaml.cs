using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using ScottPlot;

namespace DepartmentServer
{
    /// <summary>
    /// Represents the page for displaying a chart.
    /// </summary>
    public partial class ChartPage : Page
    {
        private readonly Chart chart;

        /// <summary>
        /// Constructs a new instance of the ChartPage class.
        /// </summary>
        public ChartPage()
        {
            InitializeComponent();
            chart = new Chart(wpfPlot1);
            chart.PlotScatter();
        }

        /// <summary>
        /// Event handler for the Save button click event.
        /// Saves the plot as a PNG image.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void SaveClickButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            chart.SavePlotAsPng();
        }
    }
}
