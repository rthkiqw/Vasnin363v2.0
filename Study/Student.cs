using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Study
{
    public class Student
    {
        //Свойства студента
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public int Group { get; set; }


        public Student(int id, string name, string surname, string patronymic, int group)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Patronymic = patronymic;
            Group = group;
        }

        public Student()
        {
        }
    }
}
