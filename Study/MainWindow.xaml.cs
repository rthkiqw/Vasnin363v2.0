using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace Study
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Group> Groups { get; set; } = new ObservableCollection<Group>();
        public ObservableCollection<Course> Courses { get; set; } = new ObservableCollection<Course>();
        public Speciality Specialitiy { get; set; } = new Speciality();
        public ObservableCollection<Employee> Employees { get; set; } = new ObservableCollection<Employee>();
        public MainWindow()
        {
            InitializeComponent();


            DataContext = this;

            dbConnect.Connect("10.14.206.27", "5432", "student", "1234", "study");

            LoadEmployees();
            if (Employees.Count == 0)
            {
                MainFrame.Navigate(PageControl.CreateAccPage);
            }
            else
            {
                MainFrame.Navigate(PageControl.Authorization);
            }
        }

        private void LoadEmployees()
        {
            Employees.Clear();
            NpgsqlCommand command = dbConnect.GetCommand("Select phone,name,surname,position,password FROM employees WHERE position = @position");
            command.Parameters.AddWithValue("@position", NpgsqlDbType.Varchar, "Admin");
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

        private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
                DragMove();

        }

        private void MinimizeWindow_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void StateWindow_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow.WindowState != WindowState.Maximized)
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
            else
                Application.Current.MainWindow.WindowState = WindowState.Normal;
        }

        private void ShutDownWindow_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
