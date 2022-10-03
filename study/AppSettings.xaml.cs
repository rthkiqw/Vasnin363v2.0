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
using System.Windows.Shapes;

namespace Study
{
    /// <summary>
    /// Логика взаимодействия для AppSettings.xaml
    /// </summary>
    public partial class AppSettings : Window
    {
        public AppSettings()
        {
            InitializeComponent();
        }

        private void SwitchDarkTheme_Button_Click(object sender, RoutedEventArgs e)
        {
            var uri = new Uri(@"DarkTheme.xaml", UriKind.Relative);
            ResourceDictionary resourceDictionary = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
        }

        private void SwitchLightTheme_Button_Click(object sender, RoutedEventArgs e)
        {
            var uri = new Uri(@"LightTheme.xaml", UriKind.Relative);
            ResourceDictionary resourceDictionary = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
        }

    }
}
