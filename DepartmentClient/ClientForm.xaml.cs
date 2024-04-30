using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DepartmentClient
{
    /// <summary>
    /// Interaction logic for ClientForm.xaml
    /// </summary>   
    public partial class ClientForm : Window
    {
        private string serverAddress;

        public ClientForm(string serverAddress)
        {
            InitializeComponent();
            Closing += ClientForm_Closing;
            this.serverAddress = serverAddress;
        }

        public string ServerAddress
        {
            get { return serverAddress; }
        }

        private void ClientForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private async Task CreateGroupAsync()
        {
            try
            {
                // Проверяем, что имя группы не пустое
                if (string.IsNullOrWhiteSpace(NameTextBox.Text))
                {
                    MessageBox.Show("Please enter a group name.");
                    return;
                }

                // Создаем HttpClient для отправки запроса на сервер
                using (HttpClient client = new HttpClient())
                {
                    string createGroupUrl = serverAddress + "create_group";
                    // Формируем данные запроса
                    string groupName = NameTextBox.Text;
                    string requestBody = $"group_name={groupName}";
                    StringContent content = new StringContent(requestBody, Encoding.UTF8, "application/x-www-form-urlencoded");

                    // Отправляем POST-запрос на сервер для создания новой группы
                    HttpResponseMessage response = await client.PostAsync(createGroupUrl, content);

                    // Обрабатываем ответ от сервера
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Group created successfully.");
                    }
                    else
                    {
                        MessageBox.Show($"Error: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void CreateGroupButton_Click(object sender, RoutedEventArgs e)
        {
            _ = CreateGroupAsync();
        }
    }
}
