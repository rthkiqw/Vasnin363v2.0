using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для AddEmployeePage.xaml
    /// </summary>
    public partial class AddEmployeePage : Page
    {
        public ObservableCollection<Employee> Employees = new ObservableCollection<Employee>();
        public AddEmployeePage()
        {
            InitializeComponent();
            DataContext = this;

            Binding binding = new Binding();
            binding.Source = Employees;
            cmbEmpPosEdit.ItemsSource = Employees;

            Binding binding2 = new Binding();
            binding2.Source = Employees;
            cmbEmpPos.ItemsSource = Employees;
        }

        private void CurrentEmployeeEdit(object sender, SelectionChangedEventArgs e)
        {

        }

        private void GoToAddEmployeePage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(PageControl.AddEmployeePage);
        }
    }
}
