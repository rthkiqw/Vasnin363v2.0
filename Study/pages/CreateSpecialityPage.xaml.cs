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
        public Speciality Specialitiy { get; set; } = new Speciality();
        public SpecialityFrame()
        {
            InitializeComponent();
            DataContext = this;
            DataLoader.LoadSpecialities();
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
            DataLoader.LoadSpecialities();
        }

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
