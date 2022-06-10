using System.Windows;
using System.Windows.Controls;

namespace _12
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            lvClients.ItemsSource = Client.clients;
        }

        private void btnSort_Click(object sender, RoutedEventArgs e)
        {
            Client.clients.Sort((x, y) => string.Compare(x.LastName, y.LastName));
            lvClients.Items.Refresh();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (Client.access is Manager)
            {
                Manager.AddClient(tbLname_create.Text, tbFname_create.Text, tbPname_create.Text,
                tbPnumber_create.Text, tbInumber_create.Text, out bool error);
                if (error)
                {
                    MessageBox.Show("Все поля должны быть заполнены!");
                }
                lvClients.Items.Refresh();
            }
            else
            {
                MessageBox.Show("нет прав");
            }
        }
    }
}
