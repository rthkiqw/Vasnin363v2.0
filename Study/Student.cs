using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Study
{
    public class Student : INotifyPropertyChanged
    {
        private int id;
        private string name;
        private string surname;
        private string patronymic;
        private int group;
        //Свойства студента
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs("Id"));
            }
        }
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged(Name, new PropertyChangedEventArgs("Name"));
            }
        }
        public string Surname
        {
            get
            {
                return surname;
            }
            set
            {
                surname = value;
                OnPropertyChanged(Surname, new PropertyChangedEventArgs("Surname"));
            }
        }
        public string Patronymic
        {
            get
            {
                return patronymic;
            }
            set
            {
                patronymic = value;
                OnPropertyChanged(Patronymic, new PropertyChangedEventArgs("Patronymic"));
            }
        }
        public int Group
        {
            get
            {
                return group;
            }
            set
            {
                group = value;
                OnPropertyChanged(Group, new PropertyChangedEventArgs("Group"));
            }
        }


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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (PropertyChanged == null) return;
            PropertyChanged(this, e);
        }
    }
}
