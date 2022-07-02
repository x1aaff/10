using System.Windows;
using System.Data;

namespace _17
{
    /// <summary>
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        private Window2()
        {
            InitializeComponent();
        }

        public Window2(DataRow row) : this()
        {
            disBtn.Click += delegate
            {
                this.DialogResult = false;
            };
            addBtn.Click += delegate
            {
                row["Email"] = tbPEmail.Text;
                row["ProductCode"] = tbPCode.Text;
                row["ProductName"] = tbPName.Text;

                this.DialogResult = true;
            };
        }
    }
}
