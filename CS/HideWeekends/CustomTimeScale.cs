using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Native;


namespace TimelineTimeScales {
    public class MyTimeScaleWorkHours: TimeScaleHour
    {
        public MyTimeScaleWorkHours() { }

        public override string DisplayName { get => "Custom Work Hours"; set => base.DisplayName = value; }
        public override string MenuCaption { get => "Custom Work Hours"; set => base.MenuCaption = value; }

        public override string FormatCaption(DateTime start, DateTime end)
        {
            if (start.Hour <= 12) return start.Hour.ToString() + " AM";
            else return (start.Hour - 12).ToString() + " PM";
        }
        public override bool IsDateVisible(DateTime date)
        {
            if (date.Hour >= 6 && date.Hour <= 21)
                return !(date.Hour == 14);
            else return false;
        }
    }

    public class MyTimeScaleWorkWeekDays : TimeScaleDay
    {
        public MyTimeScaleWorkWeekDays() { }

        public override string DisplayName { get => "Custom Week Days"; set => base.DisplayName = value; }
        public override string MenuCaption { get => "Custom Week Days"; set => base.MenuCaption = value; }
        public override bool IsDateVisible(DateTime date)
        {
            return !(date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday);
        }
    }
}
