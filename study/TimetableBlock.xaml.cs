using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Study
{
    /// <summary>
    /// Логика взаимодействия для TimetableBlock.xaml
    /// </summary>
    public partial class TimetableBlock : UserControl
    {
        public DateTime Date { get; set; }
        public TimetableBlock(DateTime date)
        {
            Date = date;
            InitializeComponent();
        }
    }
}
