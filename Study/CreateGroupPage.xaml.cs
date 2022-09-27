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
    /// Логика взаимодействия для CreateGroupPage.xaml
    /// </summary>
    public partial class CreateGroupPage : Page
    {
        public ObservableCollection<Group> Groups { get; set; } = new ObservableCollection<Group>();
        public ObservableCollection<Course> Courses { get; set; } = new ObservableCollection<Course>();
        public ObservableCollection<Speciality> Specialities { get; set; } = new ObservableCollection<Speciality>();
        public Group Group { get; set; } = new Group();
        public CreateGroupPage()
        {
            InitializeComponent();
            DataContext = this;

            LoadCourses();
            LoadSpec();
            LoadGroups();

            Binding binding = new Binding();
            binding.Source = Courses;
            cmbGCourseId.SetBinding(ComboBox.ItemsSourceProperty, binding);

            Binding binding1 = new Binding();
            binding1.Source = Specialities;
            cmbGSpecId.SetBinding(ComboBox.ItemsSourceProperty, binding1);

            Binding binding3 = new Binding();
            binding3.Source = Specialities;
            cmbGrSpecEdit.SetBinding(ComboBox.ItemsSourceProperty, binding3);

        }
        private void AddGroup(object sender, RoutedEventArgs e)
        {
            try
            {
                int Id = Convert.ToInt32(tbGropId.Text.Trim());
                if (cmbGSpecId.SelectedItem == null) return;
                if (cmbGCourseId.SelectedItem == null) return;
                int Spec = (cmbGSpecId.SelectedItem as Speciality).Id;
                int Course = (cmbGCourseId.SelectedItem as Course).Id;
                if (Id == 0) return;

                NpgsqlCommand command = dbConnect.GetCommand("INSERT INTO \"Group\"(\"Id\",\"Speciality\",\"Course\") VALUES (@id,@spec,@course)");
                command.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, Id);
                command.Parameters.AddWithValue("@spec", NpgsqlDbType.Integer, Spec);
                command.Parameters.AddWithValue("@course", NpgsqlDbType.Integer, Course);
                int result = command.ExecuteNonQuery();
            }
            catch (Exception er)
            {
                MessageBox.Show("already exist or ERROR " + er.Message);
            }
            tbGropId.Clear();
            cmbGCourseId.SelectedItem = null;
            cmbGSpecId.SelectedItem = null;
            LoadGroups();
        }

        private void FirstPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(PageControl.main_page);
        }
        private void StudPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(PageControl.createStudent);
        }

        private void GrPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(PageControl.createGroup);
        }

        private void SpecPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(PageControl.createSpec);
        }

        private void GoToAddEmployeePage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(PageControl.AddEmployeePage);
        }

        #region Loaders

        private void LoadCourses()
        {
            Courses.Clear();
            NpgsqlCommand command = dbConnect.GetCommand("Select \"Id\" FROM \"Course\" ORDER by \"Id\"");
            NpgsqlDataReader result = command.ExecuteReader();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Courses.Add(new Course(result.GetInt32(0)));
                }
            }
            result.Close();

        }
        private void LoadSpec()
        {
            Specialities.Clear();
            NpgsqlCommand command = dbConnect.GetCommand("Select \"Id\",\"Name\" FROM \"Speciality\" ORDER by \"Id\"");
            NpgsqlDataReader result = command.ExecuteReader();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Specialities.Add(new Speciality(result.GetInt32(0), result.GetString(1)));
                }
            }
            result.Close();

        }
        private void LoadGroups()
        {
            Groups.Clear();
            NpgsqlCommand command = dbConnect.GetCommand("Select \"Id\",\"Speciality\",\"Course\" FROM \"Group\" ORDER by \"Id\"");
            NpgsqlDataReader result = command.ExecuteReader();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    int specId = result.GetInt32(1);
                    int courseId = result.GetInt32(2);
                    var scpecialty = Specialities.Where(x => x.Id == specId).FirstOrDefault();
                    var course = Courses.Where(x => x.Id == courseId).FirstOrDefault();

                    Groups.Add(new Group(result.GetInt32(0), scpecialty, course));
                }
            }
            result.Close();

        }
        #endregion

        private void SaveGroupEditions(object sender, RoutedEventArgs e)
        {
            spGroupEditor.IsEnabled = false;

            try
            {
                int SpecId = (cmbGrSpecEdit.SelectedItem as Speciality).Id;
                int GroupId = (lbGroups.SelectedItem as Group).Id;
                if (cmbGrSpecEdit.SelectedItem == null) return;

                NpgsqlCommand command = dbConnect.GetCommand("UPDATE \"Group\" SET \"Speciality\" = @speciality WHERE \"Id\" = @id");
                command.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, GroupId);
                command.Parameters.AddWithValue("@speciality", NpgsqlDbType.Integer, SpecId);
                int result = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("somme errors here " + ex.Message);
            }
        }

        private void lbGroups_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            spGroupEditor.IsEnabled = true;
        }
    }
}
