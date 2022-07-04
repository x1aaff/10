using System;
using System.Windows;

namespace _18
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();

            disBtn.Click += delegate
            {
                this.DialogResult = false;
            };
            addBtn.Click += delegate
            {
                DataPage.context.Clients.Add(new Clients()
                {
                    LastName = tbLast.Text,
                    FirstName = tbFirst.Text,
                    PatronymicName = tbPatronymic.Text,
                    Email = tbEmail.Text,
                    PhoneNumber = int.TryParse(tbPhone.Text, out int phone) ? phone : default(Nullable<int>),
                });
                
                this.DialogResult = true;
            };
        }
    }
}
