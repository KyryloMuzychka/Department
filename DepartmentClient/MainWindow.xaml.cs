//using DepartmentServer;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Windows;

namespace DepartmentClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

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

                using (HttpClient client = new HttpClient())
                {
                    string url = ServerIPTextBox.Text;
                    string loginUrl = url + "login";

                    string requestBody = $"username={LoginTextBox.Text}&password={PasswordTextBox.Text}";
                    StringContent content = new StringContent(requestBody, Encoding.UTF8, "application/x-www-form-urlencoded");

                    HttpResponseMessage response = await client.PostAsync(loginUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = await response.Content.ReadAsStringAsync();

                        if (responseString == "Login successful")
                        {
                            ClientForm clientForm = new ClientForm(url);
                            clientForm.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Invalid response from server.");
                        }
                    }
                    else
                    {
                        if (response.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            MessageBox.Show("Invalid username or password. Please try again.");
                        }
                        else
                        {
                            MessageBox.Show($"Error: {response.StatusCode}");
                        }
                    }
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

