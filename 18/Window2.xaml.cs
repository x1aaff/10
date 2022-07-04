using System.Windows;

namespace _18
{
    /// <summary>
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();

            disBtn.Click += delegate
            {
                this.DialogResult = false;
            };
            addBtn.Click += delegate
            {
                DataPage.context.Products.Add(new Products()
                {
                    Email = tbPEmail.Text,
                    ProductCode = int.Parse(tbPCode.Text),
                    ProductName = tbPName.Text,
                });

                this.DialogResult = true;
            };
        }
    }
}
