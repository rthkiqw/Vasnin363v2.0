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
    /// Логика взаимодействия для CreateAccPage.xaml
    /// </summary>
    public partial class CreateAccPage : Page
    {
        public ObservableCollection<Employee> Roles { get; set; } = new ObservableCollection<Employee>();
        public Employee Employee { get; set; }
        public CreateAccPage()
        {
            InitializeComponent();

            DataContext = this;
        }

        private void CreateAcc_Button(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Validate() == true)
                {

                    string role = "Админ";
                    string name = tbCreateName.Text.Trim();
                    string surname = tbCreateSurname.Text.Trim();
                    string phone = tbCreateLogin.Text.Trim();
                    string pass1 = tbCreatePass.Text.Trim();
                    string pass2 = tbCreatePass2.Text.Trim();
                    string password = "";
                    if (pass1 == pass2)
                    {
                        password = pass1;
                    }
                    else
                    {
                        MessageBox.Show("Не совпадают пароли!");
                        return;
                    }
                    NpgsqlCommand command = dbConnect.GetCommand("INSERT INTO employees(phone,surname,name,password,position) VALUES (@phone,@surname,@name,@password,@position)");
                    command.Parameters.AddWithValue("@phone", NpgsqlDbType.Varchar, phone);
                    command.Parameters.AddWithValue("@name", NpgsqlDbType.Varchar, surname);
                    command.Parameters.AddWithValue("@surname", NpgsqlDbType.Varchar, name);
                    command.Parameters.AddWithValue("@password", NpgsqlDbType.Varchar, password);
                    command.Parameters.AddWithValue("@position", NpgsqlDbType.Varchar, role);
                    int result = command.ExecuteNonQuery();

                    NavigationService.Navigate(PageControl.Authorization);
                    PageControl.Authorization.RoleChecker(role);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("some errors here -> " + error.Message);
            }
        }
        private bool Validate()
        {
            if(tbCreateName.Text.Length < 2)
            {
                MainWindow.MessageShow("Поле \"Имя\" введено некорректно");
                return false;
            }
            foreach (char c in tbCreateName.Text)
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

            foreach (char c in tbCreateSurname.Text)
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

            if (tbCreateLogin.Text[0] == '8' || tbCreateLogin.Text[0] == '+' || tbCreateLogin.Text[0] != '9')
            {
                MainWindow.MessageShow("Поле \"Номер телефона\" введено некорректно");
                return false;
            }


            foreach(char c in tbCreatePass.Text)
            {
                if( c >= '!' && c <= '/' ||
                    c>= ':' && c<= '@' ||
                    c >= '[' && c <= 92 ||
                    c >= '^' && c <= '_' ||
                    c >= '{' && c <= '~' ) return false;
            }

            if(tbCreatePass.Text == tbCreatePass2.Text) return true;

            else
            {
                MainWindow.MessageShow("Пароли не совпадают");
                return false;
            }
        }
    }

}
