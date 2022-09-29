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

namespace Study
{
    /// <summary>
    /// Логика взаимодействия для CreateStudentPage.xaml
    /// </summary>
    public partial class CreateStudentPage : Page
    {

        public Group Group { get; set; } = new Group();
        public Student Student { get; set; } = new Student();
        public CreateStudentPage()
        {
            InitializeComponent();

            DataContext = this;

            DataLoader.LoadGroups();
            DataLoader.LoadStudents();

            lbStudents.SetBinding(ListBox.ItemsSourceProperty, new Binding()
            {
                Source = DataLoader.Students
            });

            Binding binding = new Binding();
            binding.Source = DataLoader.Groups;
            cmbStGroup.ItemsSource = DataLoader.Groups;
            cmbStGroupEdit.ItemsSource = DataLoader.Groups;

            //cmbStGroup.SetBinding(ComboBox.ItemsSourceProperty, binding);
            //Binding binding1 = new Binding();
            //binding1.Source = Specialities;
            //cmbGSpecId.SetBinding(ComboBox.ItemsSourceProperty, binding1);
            //Binding binding2 = new Binding();
            //binding2.Source = Courses;
            //cmbGCourseId.SetBinding(ComboBox.ItemsSourceProperty, binding2);
        }
        private void AddStudent(object sender, RoutedEventArgs e)
        {
            try
            {
                if (AddStudentValidate() == true)
                {
                    int Id = Convert.ToInt32(tbStudentId.Text.Trim());
                    string Name = tbStundetName.Text.Trim();
                    string Surname = tbStudentSurname.Text.Trim();
                    string Patronymic = tbStudentPatronymic.Text.Trim();
                    if (cmbStGroup.SelectedItem == null) return;
                    int group = (cmbStGroup.SelectedItem as Group).Id;
                    if (Id == 0 &&
                       Name.Length == 0 &&
                       Surname.Length == 0 &&
                       Patronymic.Length == 0) return;

                    NpgsqlCommand command = dbConnect.GetCommand("INSERT INTO \"Student\"(\"Id\",\"Name\",\"Surname\",\"Patronymic\",\"Group\") VALUES (@id,@name,@surname,@patronymic,@group)");
                    command.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, Id);
                    command.Parameters.AddWithValue("@name", NpgsqlDbType.Varchar, Name);
                    command.Parameters.AddWithValue("@surname", NpgsqlDbType.Varchar, Surname);
                    command.Parameters.AddWithValue("@patronymic", NpgsqlDbType.Varchar, Patronymic);
                    command.Parameters.AddWithValue("@group", NpgsqlDbType.Integer, group);
                    int result = command.ExecuteNonQuery();
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("some errors here -> " + error.Message);
            }
            tbStudentId.Clear();
            tbStundetName.Clear();
            tbStudentSurname.Clear();
            tbStudentPatronymic.Clear();
            cmbStGroup.SelectedItem = null;
            DataLoader.LoadStudents();
        }

        private void SaveEditionsButton(object sender, RoutedEventArgs e)
        {
            spStudentEditor.IsEnabled = false;
            try
            {
                if (EditStudentValidate() == true)
                {
                    int Id = (lbStudents.SelectedItem as Student).Id;
                    string Name = tbStNameEdit.Text.Trim();
                    string Surname = tbStSurnameEdit.Text.Trim();
                    string Patronymic = tbStPatronymicEdit.Text.Trim();
                    if (cmbStGroupEdit.SelectedItem == null) return;
                    int group = (cmbStGroupEdit.SelectedItem as Group).Id;
                    if (Name.Length == 0 &&
                        Surname.Length == 0 &&
                        Patronymic.Length == 0) return;

                    NpgsqlCommand command = dbConnect.GetCommand("UPDATE \"Student\" SET \"Id\" = @id,\"Name\" = @name,\"Surname\" = @surname,\"Patronymic\" = @patronymic,\"Group\" = @group WHERE \"Id\" = @id");
                    command.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, Id);
                    command.Parameters.AddWithValue("@name", NpgsqlDbType.Varchar, Name);
                    command.Parameters.AddWithValue("@surname", NpgsqlDbType.Varchar, Surname);
                    command.Parameters.AddWithValue("@patronymic", NpgsqlDbType.Varchar, Patronymic);
                    command.Parameters.AddWithValue("@group", NpgsqlDbType.Integer, group);
                    int result = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("somme errors here " + ex.Message);
            }
        }

        private void StudentEditor(object sender, SelectionChangedEventArgs e)
        {

            spStudentEditor.IsEnabled = true;

        }

        private bool AddStudentValidate()
        {
            if (tbStudentId.Text.Length != 5)
            {
                MainWindow.MessageShow("Поле \"Номер студ. билета\" введено некорректно");
                return false;
            }

            foreach (char c in tbStudentId.Text)
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
                    MainWindow.MessageShow("Поле \"Номер студ. билета\" введено некорректно");
                    return false;
                }
            }

            if (tbStundetName.Text.Length < 2)
            {
                MainWindow.MessageShow("Поле \"Имя\" введено некорректно");
                return false;
            }
            foreach (char c in tbStundetName.Text)
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

            if (tbStudentSurname.Text.Length < 2)
            {
                MainWindow.MessageShow("Поле \"Фамилия\" введено некорректно");
                return false;
            }

            foreach (char c in tbStudentSurname.Text)
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

            if (tbStudentPatronymic.Text.Length < 2)
            {
                MainWindow.MessageShow("Поле \"Отчество\" введено некорректно");
                return false;
            }
            foreach (char c in tbStudentPatronymic.Text)
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

            if (cmbStGroup.SelectedItem != null) return true;
            else
            {
                MainWindow.MessageShow("Выберите группу!");
                return false;
            }
        }

        private bool EditStudentValidate()
        {

            if (tbStNameEdit.Text.Length < 2)
            {
                MainWindow.MessageShow("Поле \"Имя\" введено некорректно");
                return false;
            }
            foreach (char c in tbStNameEdit.Text)
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

            if (tbStSurnameEdit.Text.Length < 2)
            {
                MainWindow.MessageShow("Поле \"Фамилия\" введено некорректно");
                return false;
            }

            foreach (char c in tbStSurnameEdit.Text)
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

            if (tbStPatronymicEdit.Text.Length < 2)
            {
                MainWindow.MessageShow("Поле \"Отчество\" введено некорректно");
                return false;
            }
            foreach (char c in tbStPatronymicEdit.Text)
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

            if (cmbStGroupEdit.SelectedItem != null) return true;
            else
            {
                MainWindow.MessageShow("Выберите группу!");
                return false;
            }
        }
    }
}
