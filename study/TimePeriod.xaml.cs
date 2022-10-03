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
    /// Логика взаимодействия для TimePeriod.xaml
    /// </summary>
    public partial class TimePeriod : UserControl
    {
        public TimePeriodData TimePeriodData { get; set; }
        public TimePeriod(TimePeriodData timePeriodData)
        {
            TimePeriodData = timePeriodData;
            StartTime.Content = timePeriodData.Date;
            Subject.Content = timePeriodData.Lesson;
            Teacher.Content = timePeriodData.teacher;
            InitializeComponent();
        }
    }
}
