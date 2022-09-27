using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Study
{
    public class Employee : INotifyPropertyChanged
    {
        public string Login
        {
            get
            {
                return _login;
            }
            set
            {
                _login = value;
                OnPropertyChanged(Login, new PropertyChangedEventArgs("Login"));
            }
        }
        private string _login;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged(Name, new PropertyChangedEventArgs("Name"));
            }
        }
        private string _name;
        public string Surname
        {
            get
            {
                return _surname;
            }
            set
            {
                _surname = value;
                OnPropertyChanged(Surname, new PropertyChangedEventArgs("Surname"));
            }
        }
        private string _surname;
        public string Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
                OnPropertyChanged(Position, new PropertyChangedEventArgs("Position"));
            }
        }
        private string _position;
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged(Password, new PropertyChangedEventArgs("Password"));
            }
        }
        private string _password;

        public Employee(string login, string name, string surname, string position, string password)
        {
            Login = login;
            Name = name;
            Surname = surname;
            Position = position;
            Password = password;
        }

        public Employee()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (PropertyChanged == null) return;
            PropertyChanged(this, e);
        }
    }
}
