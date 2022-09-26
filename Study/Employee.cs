using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Study
{
    public class Employee
    {
        public string login { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Position { get; set; }
        public string Password { get; set; }

        public Employee(string login, string name, string surname, string position, string password)
        {
            this.login = login;
            Name = name;
            Surname = surname;
            Position = position;
            Password = password;
        }



    }
}
