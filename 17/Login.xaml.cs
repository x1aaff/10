using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace _17
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }

        private void BtnAuthorize_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DataPage());
        }
    }
}
