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

namespace Study
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private void StudPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(PageControl.createStudent);
        }

        private void GrPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(PageControl.createGroup);
        }

        private void SpecPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(PageControl.createSpec);
        }
    }
}
