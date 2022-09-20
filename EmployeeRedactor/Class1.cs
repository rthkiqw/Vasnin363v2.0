using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeRedactor
{
    public class Employee
    {
        public Employee(string name, string surname, string middleName, string position)
        {
            Name = name;
            Surname = surname;
            MiddleName = middleName;
            Position = position;
        }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string MiddleName { get; set; }
        public string Position { get; set; }

    }
}
