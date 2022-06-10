using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.IO;
using System.Text.Json;

namespace _12
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        List<string> users = new List<string>() { "Consultant", "Manager" };
        public LoginPage()
        {
            InitializeComponent();
            cbox_login.ItemsSource = users;
        }

        private void but_login_Click(object sender, RoutedEventArgs e)
        {
            if ((string)cbox_login.SelectedItem == users[1])
            {
                Client.access = new Manager();
                Client.access.Login();
            }
            else
            {
                Client.access = new Consultant();
                Client.access.Login();
            }
            if ((bool)chckbox_generate.IsChecked)
            {
                Client.GenerateData(20);
            }
            Client.clients = JsonSerializer.Deserialize<List<Client>>(File.ReadAllText("clients.json"));
            NavigationService.Navigate(new MainPage());
        }
    }
}
