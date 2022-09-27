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
    /// Логика взаимодействия для SpecialityFrame.xaml
    /// </summary>
    public partial class SpecialityFrame : Page
    {
        public ObservableCollection<Speciality> Specialities { get; set; } = new ObservableCollection<Speciality>();
        public Speciality Specialitiy { get; set; } = new Speciality();
        public SpecialityFrame()
        {
            InitializeComponent();
            DataContext = this;
            LoadSpec();
        }

        private void AddSpec(object sender, RoutedEventArgs e)
        {
            try
            {
                int Id = Convert.ToInt32(tbSpecId.Text.Trim());
                string Name = tbSpecName.Text.Trim();
                if (Id == 0 && Name.Length == 0) return;

                NpgsqlCommand command = dbConnect.GetCommand("INSERT INTO \"Speciality\"(\"Id\",\"Name\") VALUES (@id,@name)");
                command.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, Id);
                command.Parameters.AddWithValue("@name", NpgsqlDbType.Varchar, Name);
                int result = command.ExecuteNonQuery();
            }
            catch (Exception er)
            {
                MessageBox.Show("already exist or ERROR " + er.Message);
            }
            tbSpecId.Clear();
            tbSpecName.Clear();
            LoadSpec();
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
        //ДОРАБОТАТЬ РЕДАКТИРОВАНИЕ
        private void SaveSpecEditions(object sender, RoutedEventArgs e)
        {
            spSpecEditor.IsEnabled = false;
            try
            {
                //int Id = Convert.ToInt32(tbSpecIdEdit.Text.Trim());
                string Name = tbSpecNameEdit.Text.Trim();

                if (tbSpecIdEdit == null &&
                    tbSpecNameEdit.Text.Length == 0) return;

                NpgsqlCommand command = dbConnect.GetCommand("UPDATE \"Speciality\" SET \"Name\" = @name WHERE \"Id\" = @id");
                command.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, (lbSpecialties.SelectedItem as Speciality).Id);
                command.Parameters.AddWithValue("@name", NpgsqlDbType.Varchar, Name);
                int result = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("somme errors here " + ex.Message);
            }
        }

        private void lbSpecs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            spSpecEditor.IsEnabled = true;
        }
    }
}
