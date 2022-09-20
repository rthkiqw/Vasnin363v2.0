using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace class7
{
    public class NamesWithPrice : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public int Price { get; set; }
        private int _count { get; set; }
        public int Count
        {
            get { return _count; }
            set 
            { 
                _count = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Count"));
            } 
        }
        public NamesWithPrice(string name, int price)
        {
            Name = name;
            Price = price;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }
    }
}
