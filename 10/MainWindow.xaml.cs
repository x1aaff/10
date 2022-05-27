using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _10
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TelegramMsgClient client;

        public MainWindow()
        {
            InitializeComponent();
            client = new TelegramMsgClient(this);
            logListBox.ItemsSource = TelegramMsgClient.BotMessageLog;
        }

        private void refrshClck(object sender, RoutedEventArgs e)
        {
            logListBox.Items.Refresh();
        }

        private void sndBtnClck(object sender, RoutedEventArgs e)
        {
            client.SendMessage(txtMsgSend.Text, txtTarget.Text);
        }

        private void exitBtnClck(object sender, RoutedEventArgs e)
        {
            App.Current.MainWindow.Close();
        }
        private void exportSmsBtnClck(object sender, RoutedEventArgs e)
        {
            MessageLog.SerializeAllLogs(TelegramMsgClient.BotMessageLog);
            logListBox.Items.Refresh();
            MessageBox.Show("Сохранено", "Message Logging");
        }
    }
}
