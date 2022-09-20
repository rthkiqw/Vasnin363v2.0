using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Study
{
    public class Group
    {
        // свойства группы
        public int Id { get; set; }
        public Speciality speciality { get; set; }
        public Course course { get; set; }
        public Group(int id, Speciality speciality, Course course)
        {
            Id = id;
            this.speciality = speciality;
            this.course = course;
        }

        public Group()
        {
        }
    }
}
