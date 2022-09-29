using Npgsql;
using NpgsqlTypes;
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
        public ObservableCollection<Employee> Employees { get; set; }
        public ObservableCollection<string> Positions { get; set; } = new ObservableCollection<string>();
        public Employee Employee { get; set; } = new Employee();
        public AddEmployeePage()
        {
            InitializeComponent();
            DataContext = this;

            DataLoader.LoadEmployees();
            DataLoader.LoadPositions();

            lbEmployees.SetBinding(ListBox.ItemsSourceProperty, new Binding()
            {
                Source = DataLoader.Employees
            });

            Binding binding2 = new Binding();
            binding2.Source = DataLoader.Positions;
            cmbEmpPos.SetBinding(ComboBox.ItemsSourceProperty, binding2);

            Binding binding = new Binding();
            binding.Source = DataLoader.Positions;
            cmbEmpPosEdit.SetBinding(ComboBox.ItemsSourceProperty, binding);
        }

        private void CurrentEmployeeEdit(object sender, SelectionChangedEventArgs e)
        {
            spEmployeesEditor.IsEnabled = true;
            remove_Button.IsEnabled = true;
        }

        private void SaveEditionsButton(object sender, RoutedEventArgs e)
        {
            spEmployeesEditor.IsEnabled = false;
            try
            {
                if (EditEmployeeValidate() == true)
                {
                    string Login = tbEmpPhoneEdit.Text.Trim();
                    string Name = tbEmpNameEdit.Text.Trim();
                    string Surname = tbEmpSurnameEdit.Text.Trim();
                    if (cmbEmpPosEdit.SelectedItem == null) return;
                    string Position = (string)cmbEmpPosEdit.SelectedItem;
                    string Password = tbEmpPassEdit.Text.Trim();

                    NpgsqlCommand command = dbConnect.GetCommand("UPDATE employees SET name = @name,surname = @surname,position = @position,password = @password WHERE phone = @phone");
                    command.Parameters.AddWithValue("@phone", NpgsqlDbType.Varchar, Login);
                    command.Parameters.AddWithValue("@name", NpgsqlDbType.Varchar, Name);
                    command.Parameters.AddWithValue("@surname", NpgsqlDbType.Varchar, Surname);
                    command.Parameters.AddWithValue("@position", NpgsqlDbType.Varchar, Position);
                    command.Parameters.AddWithValue("@password", NpgsqlDbType.Varchar, Password);
                    int result = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("somme errors here " + ex.Message);
            }
            DataLoader.LoadEmployees();
        }

        private void AddEmployee(object sender, RoutedEventArgs e)
        {
            try
            {
                if (AddEmployeeValidate() == true)
                {
                    if (cmbEmpPos.SelectedIndex == -1) return;

                    string EmpName = tbEmpName.Text.Trim();
                    string EmpSurname = tbEmpSurname.Text.Trim();
                    string EmpPhone = tbEmpLogin.Text.Trim();
                    string EmpPosition = (string)cmbEmpPos.SelectedItem;
                    string EmpPassword = tbEmpPassword.Text.Trim();

                    NpgsqlCommand command = dbConnect.GetCommand("INSERT INTO employees (phone,surname,name,position,password) VALUES(@phone,@name,@surname,@position,@password)");
                    command.Parameters.AddWithValue("@phone", NpgsqlDbType.Varchar, EmpPhone);
                    command.Parameters.AddWithValue("@name", NpgsqlDbType.Varchar, EmpName);
                    command.Parameters.AddWithValue("@surname", NpgsqlDbType.Varchar, EmpSurname);
                    command.Parameters.AddWithValue("@position", NpgsqlDbType.Varchar, EmpPosition);
                    command.Parameters.AddWithValue("@password", NpgsqlDbType.Varchar, EmpPassword);
                    int result = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            tbEmpName.Clear();
            tbEmpSurname.Clear();
            tbEmpLogin.Clear();
            cmbEmpPos.SelectedItem = null;
            tbEmpPassword.Clear();
            DataLoader.LoadEmployees();
        }
        //Loaders has been removed

        private void EmployeeRemove(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lbEmployees.SelectedItem == null) return;

                lbEmployees.Items.Remove(lbEmployees.SelectedItem);

                string Phone = (lbEmployees.SelectedItem as Employee).Login;
                NpgsqlCommand command = dbConnect.GetCommand("DELETE FROM employees WHERE phone = @phone");
                command.Parameters.AddWithValue("@phone", NpgsqlDbType.Varchar, Phone);
                int result = command.ExecuteNonQuery();

                DataLoader.LoadEmployees();

                remove_Button.Background = Brushes.Red;
            }
            catch (Exception ex)
            {
                remove_Button.Background = Brushes.Red;
                MessageBox.Show(ex.Message);
            }
        }

        private bool AddEmployeeValidate()
        {
            if (tbEmpName.Text.Length < 2)
            {
                MainWindow.MessageShow("Поле \"Имя\" введено некорректно");
                return false;
            }
            foreach (char c in tbEmpName.Text)
            {
                if (c >= '0' && c <= '9' ||
                    c >= '!' && c <= '/' ||
                    c >= ':' && c <= '@' ||
                    c >= '[' && c <= 92 ||
                    c >= '^' && c <= '_' ||
                    c >= '{' && c <= '~')
                {
                    MainWindow.MessageShow("Поле \"Имя\" введено некорректно");
                    return false;
                }
            }

            foreach (char c in tbEmpSurname.Text)
            {
                if (c >= '0' && c <= '9' ||
                    c >= '!' && c <= '/' ||
                    c >= ':' && c <= '@' ||
                    c >= '[' && c <= 92 ||
                    c >= '^' && c <= '_' ||
                    c >= '{' && c <= '~')
                {
                    MainWindow.MessageShow("Поле \"Фамилия\" введено некорректно");
                    return false;
                }
            }

            if (tbEmpLogin.Text[0] == '8' || tbEmpLogin.Text[0] == '+' || tbEmpLogin.Text[0] != '9')
            {
                MainWindow.MessageShow("Поле \"Номер телефона\" введено некорректно");
                return false;
            }

            foreach (char c in tbEmpPassword.Text)
            {
                if (c >= '!' && c <= '/' ||
                    c >= ':' && c <= '@' ||
                    c >= '[' && c <= 92 ||
                    c >= '^' && c <= '_' ||
                    c >= '{' && c <= '~') return false;
            }

            if (cmbEmpPos.SelectedItem != null) return true;
            else
            {
                MainWindow.MessageShow("Выберите должность!");
                return false;
            }
        }

        private bool EditEmployeeValidate()
        {
            if (tbEmpNameEdit.Text.Length < 2)
            {
                MainWindow.MessageShow("Поле \"Имя\" введено некорректно");
                return false;
            }
            foreach (char c in tbEmpNameEdit.Text)
            {
                if (c >= '0' && c <= '9' ||
                    c >= '!' && c <= '/' ||
                    c >= ':' && c <= '@' ||
                    c >= '[' && c <= 92 ||
                    c >= '^' && c <= '_' ||
                    c >= '{' && c <= '~')
                {
                    MainWindow.MessageShow("Поле \"Имя\" введено некорректно");
                    return false;
                }
            }

            foreach (char c in tbEmpSurnameEdit.Text)
            {
                if (c >= '0' && c <= '9' ||
                    c >= '!' && c <= '/' ||
                    c >= ':' && c <= '@' ||
                    c >= '[' && c <= 92 ||
                    c >= '^' && c <= '_' ||
                    c >= '{' && c <= '~')
                {
                    MainWindow.MessageShow("Поле \"Фамилия\" введено некорректно");
                    return false;
                }
            }

            if (tbEmpPhoneEdit.Text[0] == '8' || tbEmpPhoneEdit.Text[0] == '+' || tbEmpPhoneEdit.Text[0] != '9')
            {
                MainWindow.MessageShow("Поле \"Номер телефона\" введено некорректно");
                return false;
            }

            foreach (char c in tbEmpPassEdit.Text)
            {
                if (c >= '!' && c <= '/' ||
                    c >= ':' && c <= '@' ||
                    c >= '[' && c <= 92 ||
                    c >= '^' && c <= '_' ||
                    c >= '{' && c <= '~') return false;
            }

            if (cmbEmpPosEdit.SelectedItem != null) return true;
            else
            {
                MainWindow.MessageShow("Выберите должность!");
                return false;
            }
        }
    }
}
