using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Study
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Frame AppFrame;
        public static Button toStudPage;
        public static Button toGrPage;
        public static Button toSpecPage;
        public static Button toEmpPage;
        public ObservableCollection<Group> Groups { get; set; } = new ObservableCollection<Group>();
        public ObservableCollection<Course> Courses { get; set; } = new ObservableCollection<Course>();
        public Speciality Specialitiy { get; set; } = new Speciality();
        public ObservableCollection<Employee> Employees { get; set; } = new ObservableCollection<Employee>();
        public MainWindow()
        {
            InitializeComponent();

            AppFrame = MainFrame;
            DataContext = this;

            //dbConnect.Connect("10.14.206.27", "5432", "student", "1234", "study");

            toStudPage = GoToStudPage_Button;
            toGrPage = GoToGroupPage_Button;
            toSpecPage = GoToSpecialityPage_Button;
            toEmpPage = GoToEmployeePage_Button;

            GoToStudPage_Button.Visibility = Visibility.Collapsed;
            GoToGroupPage_Button.Visibility = Visibility.Collapsed;
            GoToSpecialityPage_Button.Visibility = Visibility.Collapsed;
            GoToEmployeePage_Button.Visibility = Visibility.Collapsed;

            //LoadEmployees();
            if (Employees.Count == 0)
            {
                MainFrame.Navigate(PageControl.CreateAccPage);
            }
            else
            {
                MainFrame.Navigate(PageControl.Authorization);
            }
        }
        #region Navigation
        private void FirstPage(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(PageControl.main_page);
        }
        private void StudPage(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(PageControl.createStudent);
        }

        private void GrPage(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(PageControl.createGroup);
        }

        private void SpecPage(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(PageControl.createSpec);
        }
        private void GoToAddEmployeePage(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(PageControl.AddEmployeePage);
        }
        #endregion

        
        private void LoadEmployees()
        {
            Employees.Clear();
            NpgsqlCommand command = dbConnect.GetCommand("Select phone,name,surname,position,password FROM employees WHERE position = @position");
            command.Parameters.AddWithValue("@position", NpgsqlDbType.Varchar, "Админ");
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

        #region Window_ControlPanel
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
        #endregion

    }
}
