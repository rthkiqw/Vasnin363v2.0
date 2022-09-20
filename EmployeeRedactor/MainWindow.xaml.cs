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
using Npgsql;
using NpgsqlTypes;

namespace EmployeeRedactor
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private NpgsqlConnection connection;
        public ObservableCollection<Employee> Employees { get; set; } = new ObservableCollection<Employee>();
        public Employee SelectedItem { get; set; }
        public ObservableCollection<string> Positions { get; set; } = new ObservableCollection<string>();
        public MainWindow()
        {
            InitializeComponent();


            DataContext = this;

            Connect("10.14.206.27", "5432", "student", "1234", "rthkiqw363");

            Employees = new ObservableCollection<Employee>();
            LoadPositions();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            /*if (tbName.Text.Length != 0)
            {
                if (tbSurname.Text.Length != 0)
                {
                    if (tbMiddleName.Text.Length != 0)
                    {
                        if (tbPosition.Text.Length != 0)
                        {
                            if (tbPosition.Text.Length != 0)
                            {
                                Employees.Add(new Employee(tbName.Text, tbSurname.Text, tbMiddleName.Text, tbPosition.Text));
                                empList.ItemsSource = Employees;
                            }
                            else
                            {
                                MessageBox.Show("Here some empty strings,check it!");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Here some empty strings,check it!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Here some empty strings,check it!");
                    }
                }
                else
                {
                    MessageBox.Show("Here some empty strings,check it!");
                }
            }
            else
            {
                MessageBox.Show("Here some empty strings,check it!");
            }
            */
            CreateEmployee();
        }

        private void empList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            editor.IsEnabled = true;
        }

        private void Connect(string host, string port, string user, string pass, string dbname)
        {
            string cs = string.Format("Server={0};Port={1};User ID={2};Password={3};DataBase={4}", host, port, user, pass, dbname);

            connection = new NpgsqlConnection(cs);
            connection.Open();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                string positionName = CreateNewPos.Text.Trim();
                if (positionName.Length == 0) return;

                NpgsqlCommand command = new NpgsqlCommand();
                command.Connection = connection;
                command.CommandText = "INSERT INTO position(name) VALUES (@name)";
                command.Parameters.AddWithValue("@name", NpgsqlDbType.Varchar, positionName);
                int result = command.ExecuteNonQuery();
                if (result == 1)
                {
                    MessageBox.Show("success");
                    LoadPositions();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("already exist");
            }
        }

        private void LoadPositions()
        {
                Positions.Clear();
                NpgsqlCommand command = new NpgsqlCommand();
                command.Connection = connection;
                command.CommandText = "Select name FROM position ORDER BY name";
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

        private void CreateEmployee()
        {
            try
            {
                string Name = tbName.Text.Trim();
                string Surname = tbSurname.Text.Trim();
                string MiddleName = tbMiddleName.Text.Trim();
                string Position = tbPosition.Text.Trim();
                if (tbName.Text.Length == 0 &&
                    tbSurname.Text.Length == 0 &&
                    tbMiddleName.Text.Length == 0 &&
                    tbPosition.Text.Length == 0) return;

                Employees.Add(new Employee(tbName.Text, tbSurname.Text, tbMiddleName.Text, tbPosition.Text));
                empList.ItemsSource = Employees;

                NpgsqlCommand command = new NpgsqlCommand();
                command.Connection = connection;
                command.CommandText = "INSERT INTO employee(name,surname,patrnymic,position) VALUES (@name,@surname,@middlename,@position)";
                command.Parameters.AddWithValue("@name", NpgsqlDbType.Varchar, Name);
                command.Parameters.AddWithValue("@surname", NpgsqlDbType.Varchar, Surname);
                command.Parameters.AddWithValue("@middlename", NpgsqlDbType.Varchar, MiddleName);
                command.Parameters.AddWithValue("@position", NpgsqlDbType.Varchar, Position);
                int result = command.ExecuteNonQuery();
                if (result == 1)
                {
                    MessageBox.Show("success");
                    LoadPositions();
                }
            }
            catch (Exception e)
            {
                
                MessageBox.Show("already exist" + e.Message);
            }
        }
    }
}
