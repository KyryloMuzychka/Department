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
using System.Windows.Shapes;

namespace DepartmentClient
{
    /// <summary>
    /// Interaction logic for ClientForm.xaml
    /// </summary>   
    public partial class ClientForm : Window
    {
        public ClientForm()
        {
            InitializeComponent();
            Closing += ClientForm_Closing;
        }

        private void ClientForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {            
            Application.Current.Shutdown();
        }
    }
}
