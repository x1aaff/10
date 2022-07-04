using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Data.Entity;
using System.Data;

namespace _18
{
    /// <summary>
    /// Логика взаимодействия для DataPage.xaml
    /// </summary>
    public partial class DataPage : Page
    {
        public static base1Entities1 context;

        public DataPage()
        {
            InitializeComponent();

            context = new base1Entities1();
        }

        private void LoadBtn_Click(object sender, RoutedEventArgs e)
        {
            context.Clients.Load();
            DGClients.ItemsSource = context.Clients.Local.ToBindingList<Clients>();

            context.Products.Load();
            DGProducts.ItemsSource = context.Products.Local.ToBindingList<Products>();
        }

        private void DGClients_CurrentCellChanged(object sender, EventArgs e)
        {
            context.SaveChanges();
        }

        private void DGProducts_CurrentCellChanged(object sender, EventArgs e)
        {
            context.SaveChanges();
        }

        private void MenuItemAdd_Click(object sender, RoutedEventArgs e)//клиенты
        {
            Window1 add = new Window1();
            add.ShowDialog();

            if (add.DialogResult.Value) context.SaveChanges();
        }

        private void MenuItemDelete_Click(object sender, RoutedEventArgs e)//клиенты
        {
            Clients client = DGClients.SelectedItem as Clients;
            context.Entry(client).State = EntityState.Deleted;
            context.SaveChanges();
        }

        private void MenuPAdd_Click(object sender, RoutedEventArgs e)//товары
        {
            Window2 add = new Window2();
            add.ShowDialog();

            if (add.DialogResult.Value) context.SaveChanges();
        }

        private void MenuPDel_Click(object sender, RoutedEventArgs e)//товары
        {
            context.Products.Remove(DGProducts.SelectedItem as Products);
            context.SaveChanges();
        }

        private void DGClients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DGClients.SelectedItem as Clients == null) return;

            string email = (DGClients.SelectedItem as Clients).Email;
            var res = context.Products.Where(p => p.Email == email);
            var purchases = new List<Products>();
            purchases.AddRange(res);
            DGPurchases.ItemsSource = purchases;
        }
    }
}
