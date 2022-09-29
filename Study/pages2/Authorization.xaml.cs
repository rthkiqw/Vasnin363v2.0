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
            if (Validate() == true)
            {
                var Login = tbAuthLogin.Text.Trim();
                var Pass = tbAuthPass.Password.Trim();

                NpgsqlCommand command = dbConnect.GetCommand("Select surname,name,position FROM employees WHERE phone=@phone AND password=@pass");
                command.Parameters.AddWithValue("@phone", NpgsqlDbType.Varchar, Login);
                command.Parameters.AddWithValue("@pass", NpgsqlDbType.Varchar, Pass);
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
                        MainWindow.MessageShow("Убедитесь, что заполнили ВСЕ поля");
                    else
                    {
                        MainWindow.MessageShow("Неверный логин\nили пароль");
                    }
                }
                result.Close();
            }
        }

        public void RoleChecker(string role)
        {
            if (role == "Админ")
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
            if (role == "Приемная комиссия")
            {
                MainWindow.toStudPage.Visibility = Visibility.Visible;

                MainWindow.AppFrame.Navigate(PageControl.main_page);
            }
        }

        private bool Validate()
        {

            foreach (char c in tbAuthLogin.Text)
            {
                if (c >= 'A' && c <= 'Z' ||
                    c >= 'a' && c <= 'z' ||
                    c >= 'А' && c <= 'Я' ||
                    c >= 'а' && c <= 'я' ||
                    c >= '!' && c <= '/' ||
                    c >= ':' && c <= '@' ||
                    c >= '[' && c <= 92 ||
                    c >= '^' && c <= '_' ||
                    c >= '{' && c <= '~')
                {
                    MainWindow.MessageShow("Поле \"Номер телефона\" введено некорректно");
                    return false;
                }
            }

            if (tbAuthLogin.Text.Length != 10)
            {
                MainWindow.MessageShow("Поле \"Номер телефона\" введено некорректно");
                return false;
            }
            else return true;
        }

    }
}
