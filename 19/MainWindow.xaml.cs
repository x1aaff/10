using System.Collections.Generic;
using System.Linq;
using System.Windows;
using _19lib;

namespace _19
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IView
    {
        Presenter presenter;

        public List<IAnimal> Animals { get => this.lvAnimals.Items.Cast<IAnimal>().ToList(); }
        public IAnimal CurrentAnimal { get => this.lvAnimals.SelectedItem as IAnimal; }
        public List<string> Strings { get => new List<string>() { tbAnimalType.Text, tbSpecies.Text, tbName.Text, tbDesc.Text }; }
        public IModel SelectedModel { get => cbModelChanger.SelectedItem as IModel; }
        public List<IAnimal> Result { set => this.lvAnimals.ItemsSource = value; }

        public MainWindow()
        {
            InitializeComponent();
            presenter = new Presenter(this);
            cbModelChanger.ItemsSource = Presenter.models;
            lvAnimals.ItemsSource = Animals;
        }

        private void btnRunModel_Click(object sender, RoutedEventArgs e)
        {
            presenter.Result();
        }
    }
}
