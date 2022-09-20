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

namespace class6
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
        string mess;

        private void Add()
        {
            mess = textBox.Text.Trim();
            if (cmbx.Items.Contains(mess))
            {
                MessageBox.Show("already exist");
                return;
            }

            if (mess.Length != 0)
                cmbx.Items.Add(mess);

            textBox.Text = "";
            textBox.Focus();
        }

        private void Button_Click_Remove(object sender, RoutedEventArgs e)
        {
            mess = textBox.Text.Trim();
            if (mess.Length != 0)
            {
                cmbx.Items.Remove(mess);
            }
            else
            {
                cmbx.Items.Remove(cmbx.SelectedItem);
                textBox.Text = "";
            }
        }

        private void Button_Click_Clear(object sender, RoutedEventArgs e)
        {
            cmbx.Items.Clear();
        }
        private void ButtonListBoxCopy(object sender, RoutedEventArgs e)
        {
            if (cmbx.SelectedItem != null)
            {
                if (listBox.Items.Contains(cmbx.SelectedItem.ToString()) == false)
                    listBox.Items.Add(cmbx.SelectedItem.ToString());
                else
                {
                    MessageBox.Show("already exist");
                }
            }
        }
        private void RemoveListBoxButton(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedIndex != -1)
            {
                listBox.Items.Remove(listBox.SelectedItem.ToString());
            }
            else
            {
                MessageBox.Show("ERROR\nSELECT LISTBOX ITEM");
            }
        }
        private void ClearListBoxButton(object sender, RoutedEventArgs e)
        {
            listBox.Items.Clear();
        }
        private void ButtonListBoxMove(object sender, RoutedEventArgs e)
        {
            if (cmbx.SelectedItem != null)
            {
                if (listBox.Items.Contains(cmbx.SelectedItem.ToString()) == false)
                {
                    listBox.Items.Add(cmbx.SelectedItem.ToString());
                    cmbx.Items.Remove(cmbx.SelectedItem);
                }
                else
                {
                    MessageBox.Show("already exist");
                }
            }
            else
            {
                MessageBox.Show("ERROR\nSELECT COMBOBOX ITEM");
            }
        }
        private void ButtonListBoxGetBack(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedItem != null)
            {
                if (cmbx.Items.Contains(listBox.SelectedItem) == false)
                {
                    cmbx.Items.Add(listBox.SelectedItem.ToString());
                    listBox.Items.Remove(listBox.SelectedItem.ToString());
                }
                else
                {
                    listBox.Items.Remove(listBox.SelectedItem);
                }
            }
            else
            {
                MessageBox.Show("ERROR\nSELECT LISTBOX ITEM");
            }
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            Add();
        }
    }
}
