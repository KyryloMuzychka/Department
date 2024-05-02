using System;
using System.Windows;

namespace DepartmentClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ClientLogic clientLogic;

        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            clientLogic = new ClientLogic(ServerIPTextBox.Text);
        }

        /// <summary>
        /// Handles the Click event of the Connect button.
        /// </summary>
        private async void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ConnectButton.IsEnabled = false;

                if (string.IsNullOrWhiteSpace(LoginTextBox.Text) || string.IsNullOrWhiteSpace(PasswordTextBox.Text))
                {
                    MessageBox.Show("Please enter username and password.");
                    return;
                }

                string loginUrl = ServerIPTextBox.Text + "login";
                string requestBody = $"username={LoginTextBox.Text}&password={PasswordTextBox.Text}";

                string responseString = await clientLogic.Connect(loginUrl, requestBody);

                if (responseString == "Login successful")
                {
                    ClientForm clientForm = new ClientForm(ServerIPTextBox.Text);
                    clientForm.Show();
                    this.Close();
                }
                else if (responseString == "Unauthorized")
                {
                    MessageBox.Show("Invalid username or password. Please try again.");
                }
                else
                {
                    MessageBox.Show($"Error: {responseString}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                ConnectButton.IsEnabled = true;
            }
        }
    }
}
