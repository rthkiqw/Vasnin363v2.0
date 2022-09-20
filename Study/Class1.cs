using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Study
{
    public class Speciality
    {
        // свойства специальности
        public int Id { get; set; }
        public string Name { get; set; }

        public Speciality(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public Speciality()
        {
        }
    }
}
