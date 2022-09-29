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
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Page
    {
        public Authorization()
        {
            InitializeComponent();
            DataContext = this;

        }

        private void LogIn_Button(object sender, RoutedEventArgs e)
        {
            NpgsqlCommand command = dbConnect.GetCommand("Select surname,name,position FROM employees WHERE phone=@phone AND password=@pass");
            command.Parameters.AddWithValue("@phone",NpgsqlDbType.Varchar, tbAuthLogin.Text.Trim());
            command.Parameters.AddWithValue("@pass", NpgsqlDbType.Varchar, tbAuthPass.Password.Trim());
            NpgsqlDataReader result = command.ExecuteReader();
            if (result.HasRows)
            {
                string role = "";
                while (result.Read())
                {
                    role = result.GetString(2);
                }

                RoleChecker(role);
            }
            else
            {
                if (tbAuthLogin.Text.Length == 0 || tbAuthPass.Password.Length == 0)
                    MessageBox.Show("Убедитесь что вы заполнили все поля");
                else
                {
                    MessageBox.Show("Неверный логин или пароль");
                }
            }
            result.Close();
        }

        public void RoleChecker(string role)
        {
            if(role == "Админ")
            {
                MainWindow.toStudPage.Visibility = Visibility.Visible;
                MainWindow.toGrPage.Visibility = Visibility.Visible;
                MainWindow.toSpecPage.Visibility = Visibility.Visible;
                MainWindow.toEmpPage.Visibility = Visibility.Visible;

                MainWindow.AppFrame.Navigate(PageControl.main_page);
            }
            if (role == "Учебная часть")
            {
                MainWindow.toGrPage.Visibility = Visibility.Visible;
                MainWindow.toSpecPage.Visibility = Visibility.Visible;

                MainWindow.AppFrame.Navigate(PageControl.main_page);
            }
            if(role == "Приемная комиссия")
            {
                MainWindow.toStudPage.Visibility = Visibility.Visible;

                MainWindow.AppFrame.Navigate(PageControl.main_page);
            }
        }

    }
}
