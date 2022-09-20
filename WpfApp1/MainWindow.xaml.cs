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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox == null) return;
            ComboBoxItem item = comboBox.SelectedItem as ComboBoxItem;
            if (item == null) return;
            var a = (string)item.Content;
            txtblock.FontFamily = new FontFamily(a);
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider slider = sender as Slider;
            if (slider == null) return;
            txtblock.FontSize = slider.Value;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton == null) return;
            switch (radioButton.Content)
            {
                case "Normal":
                    txtblock.FontStyle = FontStyles.Normal;
                    break;
                case "Cursive":
                    txtblock.FontStyle = FontStyles.Italic;
                    break;
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            switch (check.IsChecked.Value)
            {
                case true:
                    check.Content = "Bold";
                    txtblock.FontWeight = FontWeights.Bold;
                    break;
                case false:
                    check.Content = "Normal";
                    txtblock.FontWeight = FontWeights.Normal;
                    break;
            }

            
        }
        private void ComboBox_SelectionChanged_color(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox == null) return;
            ComboBoxItem item = comboBox.SelectedItem as ComboBoxItem;
            if (item == null) return;

            switch((string)item.Content)
            {
                case "Black":
                    txtblock.Foreground = Brushes.Black;
                    break;
                case "Pink":
                    txtblock.Foreground = Brushes.Pink;
                    break;
                case "Purple":
                    txtblock.Foreground = Brushes.Purple;
                    break;
            }
        }

        private void ComboBox_SelectionChanged_align(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if(comboBox == null) return;
            ComboBoxItem item = comboBox.SelectedItem as ComboBoxItem;
            if(item == null) return;

            switch((string)item.Content)
            {
                case "Left":
                    txtblock.TextAlignment = TextAlignment.Left;
                    break;
                case "Center":
                    txtblock.TextAlignment = TextAlignment.Center;
                    break;
                case "Right":
                    txtblock.TextAlignment = TextAlignment.Right;
                    break;
                case "Justify":
                    txtblock.TextAlignment = TextAlignment.Justify;
                    break;
            }
        }
    }
}
