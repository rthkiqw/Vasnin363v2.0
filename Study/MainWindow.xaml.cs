using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Threading;

namespace Study
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static DispatcherTimer timer = new DispatcherTimer();
        public static Border border;
        public static TextBlock textBlock;

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

            timer.Interval = new TimeSpan(0, 0, 0, 3);
            timer.Tick += Timer_Tick;

            border = notify;
            textBlock = notifyText;

            AppFrame = MainFrame;
            DataContext = this;

            dbConnect.Connect("10.14.206.27", "5432", "student", "1234", "study");

            toStudPage = GoToStudPage_Button;
            toGrPage = GoToGroupPage_Button;
            toSpecPage = GoToSpecialityPage_Button;
            toEmpPage = GoToEmployeePage_Button;

            GoToStudPage_Button.Visibility = Visibility.Collapsed;
            GoToGroupPage_Button.Visibility = Visibility.Collapsed;
            GoToSpecialityPage_Button.Visibility = Visibility.Collapsed;
            GoToEmployeePage_Button.Visibility = Visibility.Collapsed;

            DataLoader.LoadEmployees();
            if (DataLoader.Employees.Count == 0)
            {
                MainFrame.Navigate(PageControl.CreateAccPage);
            }
            else
            {
                MainFrame.Navigate(PageControl.Authorization);
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            notifyText.Text = "";
            notify.Visibility = Visibility.Hidden;
            timer.Stop();
        }

        public static void MessageShow(string notify)
        {
            textBlock.Text = notify;
            border.Visibility = Visibility.Visible;
            timer.Start();
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
