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
       
        public ObservableCollection<Group> Groups { get; set; } = new ObservableCollection<Group>();
        public Group Group { get; set; } = new Group();
        public Student Student { get; set; } = new Student();
        public CreateStudentPage()
        {
            InitializeComponent();
            DataContext = this;
            DataLoader.LoadGroups();
            DataLoader.LoadStudents();
            Binding binding = new Binding();
            binding.Source = Groups;
            cmbStGroup.ItemsSource = Groups;
            cmbStGroupEdit.ItemsSource = Groups;

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
            catch (Exception error)
            {
                MessageBox.Show("already exist" + error.Message);
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
            catch (Exception ex)
            {
                MessageBox.Show("somme errors here " + ex.Message);
            }
    }

        private void StudentEditor(object sender, SelectionChangedEventArgs e)
        {

            spStudentEditor.IsEnabled = true;

        }
    }
}
