using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace class7
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<NamesWithPrice> names { get; set; } = new List<NamesWithPrice>();
        public NamesWithPrice selectedNameWithPrice { get; set; }
        public int sum { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            names.Add(new NamesWithPrice("Cucumer", 100));
            names.Add(new NamesWithPrice("Potato", 3300));
            names.Add(new NamesWithPrice("Tomato", 50));
            names.Add(new NamesWithPrice("Carrot", 200));
            names.Add(new NamesWithPrice("Gas", 100000));
            names.Add(new NamesWithPrice(" +5 Social credit", 1973567857));

        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (selectedNameWithPrice == null) return;
            if (!listik.Items.Contains(selectedNameWithPrice))
            {
                listik.Items.Add(selectedNameWithPrice);
                sum += selectedNameWithPrice.Price;
                lableSum.GetBindingExpression(Label.ContentProperty).UpdateTarget();
            }
            selectedNameWithPrice.Count++;
            (sender as ListBox).SelectedIndex = -1;
        }
    }
}
