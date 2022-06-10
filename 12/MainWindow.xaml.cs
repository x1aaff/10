using System.Windows;
using System.IO;
using System.Text.Json;

namespace _12
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Content = new LoginPage();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Client.access.Logout();
            string jsonString = JsonSerializer.Serialize(Client.clients);
            File.WriteAllText("clients.json", jsonString);
        }
    }
}
