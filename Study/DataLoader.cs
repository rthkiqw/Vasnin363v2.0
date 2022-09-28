using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Study
{
    public class DataLoader
    {
        private static ObservableCollection<Course> Courses { get; set; } = new ObservableCollection<Course>();
        private static ObservableCollection<Student> Students { get; set; } = new ObservableCollection<Student>();
        private static ObservableCollection<Speciality> Specialities { get; set; } = new ObservableCollection<Speciality>();
        private static ObservableCollection<Group> Groups { get; set; } = new ObservableCollection<Group>();
        private static ObservableCollection<Employee> Employees { get; set; } = new ObservableCollection<Employee>();
        private static ObservableCollection<string> Positions { get; set; } = new ObservableCollection<string>();

        public static void LoadPositions()
        {
            Positions.Clear();
            NpgsqlCommand command = dbConnect.GetCommand("Select Distinct position FROM employees GROUP BY position ORDER by position");
            NpgsqlDataReader result = command.ExecuteReader();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Positions.Add(result.GetString(0));
                }
            }
            result.Close();

        }

        public static void LoadEmployees()
        {
            Employees.Clear();
            NpgsqlCommand command = dbConnect.GetCommand("Select phone,name,surname,position,password FROM employees ORDER by surname");
            NpgsqlDataReader result = command.ExecuteReader();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Employees.Add(new Employee(result.GetString(0), result.GetString(1), result.GetString(2), result.GetString(3), result.GetString(4)));
                }
            }
            result.Close();

        }

        public static void LoadGroups()
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

        public static void LoadStudents()
        {
            Students.Clear();
            NpgsqlCommand command = dbConnect.GetCommand("Select \"Id\",\"Surname\",\"Name\",\"Patronymic\", \"Group\" FROM \"Student\" ORDER by \"Id\"");
            NpgsqlDataReader result = command.ExecuteReader();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Students.Add(new Student(result.GetInt32(0), result.GetString(1), result.GetString(2), result.GetString(3), result.GetInt32(4)));
                }
            }
            result.Close();
        }

        public static void LoadSpecialities()
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

        public static void LoadCourses()
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
    }
}
