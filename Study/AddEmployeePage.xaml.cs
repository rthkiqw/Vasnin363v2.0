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
        public ObservableCollection<Employee> Employees { get; set; } = new ObservableCollection<Employee>();
        public ObservableCollection<string> Positions { get; set; } = new ObservableCollection<string>();
        public Employee Employee { get; set; } = new Employee();
        public AddEmployeePage()
        {
            InitializeComponent();
            DataContext = this;

            LoadEmployees();
            LoadPositions();


            Binding binding2 = new Binding();
            binding2.Source = Positions;
            cmbEmpPos.SetBinding(ComboBox.ItemsSourceProperty, binding2);

            Binding binding = new Binding();
            binding.Source = Positions;
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
            catch (Exception ex)
            {
                MessageBox.Show("somme errors here " + ex.Message);
            }
            LoadEmployees();
        }

        private void AddEmployee(object sender, RoutedEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            tbEmpName.Clear();
            tbEmpSurname.Clear();
            tbEmpLogin.Clear();
            cmbEmpPos.SelectedItem = null;
            tbEmpPassword.Clear();
            LoadEmployees();
        }
        private void LoadPositions()
        {
            Positions.Clear();
            NpgsqlCommand command = dbConnect.GetCommand("Select Distinct position FROM employees GROUP BY position ORDER by position");
            NpgsqlDataReader result = command.ExecuteReader();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Positions.Add(result.GetString(0));
                }
            }
            result.Close();

        }
        private void LoadEmployees()
        {
            Employees.Clear();
            NpgsqlCommand command = dbConnect.GetCommand("Select phone,name,surname,position,password FROM employees ORDER by surname");
            NpgsqlDataReader result = command.ExecuteReader();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Employees.Add(new Employee(result.GetString(0), result.GetString(1), result.GetString(2), result.GetString(3), result.GetString(4)));
                }
            }
            result.Close();

        }

        private void EmployeeRemove(object sender, RoutedEventArgs e)
        {
            try
            {
                lbEmployees.Items.Remove(lbEmployees.SelectedItem);

                if (lbEmployees.SelectedItem == null) return;

                string Phone = (lbEmployees.SelectedItem as Employee).Login;
                NpgsqlCommand command = dbConnect.GetCommand("DELETE FROM employees WHERE phone = @phone");
                command.Parameters.AddWithValue("@phone", NpgsqlDbType.Varchar, Phone);
                int result = command.ExecuteNonQuery();

                LoadEmployees();

                remove_Button.Background = Brushes.Red;
            }
            catch (Exception ex)
            {
                remove_Button.Background = Brushes.Red;
                MessageBox.Show(ex.Message);
            }
        }
    }
}
