using System;
using System.Windows;
using System.Data;

namespace _17
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private Window1()
        {
            InitializeComponent();
        }

        public Window1(DataRow row) : this()
        {
            disBtn.Click += delegate
            {
                this.DialogResult = false;
            };
            addBtn.Click += delegate
            {
                row["LastName"] = tbLast.Text;
                row["FirstName"] = tbFirst.Text;
                row["PatronymicName"] = tbPatronymic.Text;
                row["PhoneNumber"] = string.IsNullOrEmpty(tbPhone.Text) ? DBNull.Value : tbPhone.Text;
                row["Email"] = tbEmail.Text;

                this.DialogResult = true;
            };
        }
    }
}
