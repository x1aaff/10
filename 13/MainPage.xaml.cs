using System.Windows;
using System.Windows.Controls;
using _15lib;

namespace _13
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            lvClients.ItemsSource = Manager.clientsList;

            popup1.PopupAnimation = System.Windows.Controls.Primitives.PopupAnimation.Slide;

            Client.Notify += Manager.LogAction;
            Client.Notify += ShowNotification;
            Account.AccountNotify += Manager.LogAction;
            Account.AccountNotify += ShowNotification;

            logLb.ItemsSource = Manager.logs;
        }
        private void ShowNotification(string param)
        {
            popup1Text.Text = param;
            logLb.Items.Refresh();
            popup1.IsOpen = true;
        }

        private void lvClients_selchng(object sender, SelectionChangedEventArgs e)
        {
            lbAccounts.ItemsSource = (lvClients.SelectedItem as Client).ClientAccounts;
        }
        private void CloseAccountBtn_Click(object sender, RoutedEventArgs e)
        {
            (lvClients.SelectedItem as Client).Close(lbAccounts.SelectedItem as Account);
            lbAccounts.Items.Refresh();
        }

        private void lbAccounts_selchng(object sender, SelectionChangedEventArgs e)
        {
            cbClientalTransfer.ItemsSource = Manager.clientsList;
            cbClientalTransfer.Items.Refresh();
            cbClientalTransfer.SelectedItem = lvClients.SelectedItem as Client;

            var possibleTransfers = (cbClientalTransfer.SelectedItem as Client).GetPossibleTransfers(
                (lbAccounts.SelectedItem as Account));
            cbAccounts2.ItemsSource = possibleTransfers;
            cbAccounts2.Items.Refresh();
        }

        private void TransferBtn_Click(object sender, RoutedEventArgs e)
        {
            if ((lbAccounts.SelectedItem as Account).
                SendTransfer(cbClientalTransfer.SelectedItem as Client,
                cbAccounts2.SelectedItem as Account,
                double.Parse(TransferSumBox.Text))
                )
            {
                lbAccounts.Items.Refresh();
            }
            else
            {
                MessageBox.Show("недостаточно средств");
                lbAccounts.Items.Refresh();
            }
        }

        private void OpenDepAccountBtn_Click(object sender, RoutedEventArgs e)
        {
            (lvClients.SelectedItem as Client).Open<DepositAccount>();
            lbAccounts.Items.Refresh();
        }

        private void OpenAnoAccountBtn_Click(object sender, RoutedEventArgs e)
        {
            (lvClients.SelectedItem as Client).Open<AnotherAccount>();
            lbAccounts.Items.Refresh();
        }

        private void DepositBtn_Click(object sender, RoutedEventArgs e)
        {
            if (lbAccounts.SelectedItem is DepositAccount)
            {
                Account.Deposit<DepositAccount>(double.Parse(DepositSumBox.Text),
                    lbAccounts.SelectedItem as DepositAccount);
            }
            else
            {
                Account.Deposit<AnotherAccount>(double.Parse(DepositSumBox.Text),
                    lbAccounts.SelectedItem as AnotherAccount);
            }
            lbAccounts.Items.Refresh();
        }

        private void cbClientalTransfer_selchng(object sender, SelectionChangedEventArgs e)
        {
            var possibleTransfers = (cbClientalTransfer.SelectedItem as Client).GetPossibleTransfers(
                (lbAccounts.SelectedItem as Account));
            cbAccounts2.ItemsSource = possibleTransfers;
            cbAccounts2.Items.Refresh();
        }
    }
}
