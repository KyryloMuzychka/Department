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
        private readonly Chart chart;

        public ChartPage()
        {
            InitializeComponent();
            chart = new Chart(wpfPlot1);
            chart.PlotScatter();           
        }        

        private void SaveClickButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            chart.SavePlotAsPng();
        }
    }
}
