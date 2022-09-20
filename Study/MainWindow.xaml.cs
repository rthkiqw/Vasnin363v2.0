using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Study
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Speciality Specialitiy { get; set; } = new Speciality();
        public MainWindow()
        {
            InitializeComponent();


            DataContext = this;
            /*Binding binding = new Binding();
            binding.Source = Groups;
            cmbStGroup.SetBinding(ComboBox.ItemsSourceProperty, binding);
            Binding binding1 = new Binding();
            binding1.Source = Specialities;
            cmbGSpecId.SetBinding(ComboBox.ItemsSourceProperty, binding1);
            Binding binding2 = new Binding();
            binding2.Source = Courses;
            cmbGCourseId.SetBinding(ComboBox.ItemsSourceProperty, binding2);
            */

            dbConnect.Connect("10.14.206.27", "5432", "student", "1234", "study");

            MainFrame.Navigate(PageControl.main_page);
        }
    }
}
