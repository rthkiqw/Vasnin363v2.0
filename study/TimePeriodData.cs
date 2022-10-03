using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Study
{
    public class TimePeriodData
    {
        //public int Billing_id;
        public DateTime Date { get; set; }
        public string Cabinet { get; set; }
        public string Lesson { get; set; }
        public int Id { get; set; }
        public int groupId { get; set; }
        public string teacher { get; set; }

        public TimePeriodData(/*int billing_id,*/ DateTime date, string cabinet, string lesson, int id, int _groupId, string _teacher)
        {
            //Billing_id = billing_id;
            Date = date;
            Cabinet = cabinet;
            Lesson = lesson;
            Id = id;
            groupId = _groupId;
            teacher = _teacher;
        }
    }
}
