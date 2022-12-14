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
        public ObservableCollection<Course> Courses { get; set; } = new ObservableCollection<Course>();
        public ObservableCollection<Speciality> Specialities { get; set; } = new ObservableCollection<Speciality>();
        public Group Group { get; set; } = new Group();
        public CreateGroupPage()
        {
            InitializeComponent();
            DataContext = this;

            DataLoader.LoadCourses();
            DataLoader.LoadSpecialities();
            DataLoader.LoadGroups();

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
            DataLoader.LoadGroups();
        }

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
