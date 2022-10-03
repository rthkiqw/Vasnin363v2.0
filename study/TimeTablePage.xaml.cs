using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для TimeTablePage.xaml
    /// </summary>
    public partial class TimeTablePage : Page
    {

        public ObservableCollection<TimePeriodData> TimeTable { get; set; } = new ObservableCollection<TimePeriodData>();
        public TimePeriodData newTimetable { get; set; }

        public TimeTablePage()
        {
            InitializeComponent();

            DataContext = this;

            DataLoader.LoadTimeTable();

            foreach (var timePeriodData in DataLoader.timePeriods)
            {
                TimePeriod timePeriod = new TimePeriod(timePeriodData);
                TimetableBlock timetableBlock = TimetableBlockWith(timePeriod);
                timetableBlock.TimePeriods.Children.Add(timePeriod);
            }

            lbTimeTable.SetBinding(ListBox.ItemsSourceProperty, new Binding()
            {
                Source = DataLoader.timePeriods
            });
        }

        private TimetableBlock TimetableBlockWith(TimePeriod timePeriod)
        {

            foreach(var children in timeTable.Children)
            {
                TimetableBlock timetableBlock = children as TimetableBlock;
                if(timetableBlock.Date.Day == timePeriod.TimePeriodData.Date.Day &&
                    timetableBlock.Date.Month == timePeriod.TimePeriodData.Date.Month &&
                    timetableBlock.Date.Year == timePeriod.TimePeriodData.Date.Year)
                {
                    return timetableBlock;
                }
            }
            TimetableBlock newTimetableBlock = new TimetableBlock(new DateTime(timePeriod.TimePeriodData.Date.Year, timePeriod.TimePeriodData.Date.Month, timePeriod.TimePeriodData.Date.Day));
            timeTable.Children.Add(newTimetableBlock);
            return newTimetableBlock;
        }
    }
}
