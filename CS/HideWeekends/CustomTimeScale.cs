using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Native;


namespace TimelineTimeScales {
    public class TimeScaleWorkWeekDay : TimeScale {
        static readonly TimeSpan StartTime = TimeSpan.FromHours(6);

        public TimeScaleWorkWeekDay() {
            DaysToIgnore = new List<DayOfWeek>();
            DaysToIgnore.Add(DayOfWeek.Saturday);
            DaysToIgnore.Add(DayOfWeek.Sunday);
        }

        protected override string DefaultDisplayFormat {
            get { return "d ddd"; }
        }
        protected override string DefaultDisplayName { get { return "Custom Day"; } }
        protected override string DefaultMenuCaption { get { return "Custom Day"; } }
        protected override TimeSpan SortingWeight {
            get { return TimeSpan.FromDays(1); }
        }
        List<DayOfWeek> DaysToIgnore { get; set; }

        public override DateTime Floor(DateTime date) {
            TimeSpan time = date.TimeOfDay;

            if (time >= StartTime) {
                date = RoundToHour(date, StartTime);
            } else {
                date = RoundToHour(date.AddDays(-1), StartTime);
            }

            date = SkipSomeDays(date, -1);

            return date;
        }

        protected override bool HasNextDate(DateTime date) {
            return true;
        }

        public override DateTime GetNextDate(DateTime date) {
            TimeSpan time = date.TimeOfDay;

            if (time < StartTime) {
                date = RoundToHour(date, StartTime);
            } else {
                date = RoundToHour(date.AddDays(1), StartTime);
            }

            date = SkipSomeDays(date, 1);

            return date;
        }
      
        protected DateTime RoundToHour(DateTime date, TimeSpan timeOfDay) {
            return date.Date + timeOfDay;
        }

        DateTime SkipSomeDays(DateTime date, int skipDayCount) {
            int count = DaysToIgnore.Count;
            for (int i = 0; i < count; i++) {
                if (!DaysToIgnore.Contains(date.DayOfWeek))
                    return date;
                date = date.AddDays(skipDayCount);
            }
            return date;
        }
    }
    

    public class TimeScaleLessThanDay : TimeScaleFixedInterval {
        static readonly TimeSpan StartTimeLimitation = TimeSpan.FromHours(6);
        static readonly TimeSpan EndTimeLimitation = TimeSpan.FromHours(21);

        
        public TimeScaleLessThanDay(TimeSpan scaleValue)
            : base(scaleValue) {
            DaysToIgnore = new List<DayOfWeek>();
            DaysToIgnore.Add(DayOfWeek.Saturday);
            DaysToIgnore.Add(DayOfWeek.Sunday);
        }

        public TimeSpan StartTime { get { return StartTimeLimitation; } }
        public TimeSpan EndTime { get { return EndTimeLimitation; } }
        protected override string DefaultDisplayFormat { get { return "HH:mm"; } }
        List<DayOfWeek> DaysToIgnore { get; set; }

        protected override TimeSpan SortingWeight {
            get { return Value; }
        }

        public override DateTime Floor(DateTime date) {
            if (date == DateTime.MinValue || date == DateTime.MaxValue)
                return date;

            date = DateTimeHelper.Floor(date, Value, RoundToHour(date, StartTime));
            
            TimeSpan time = date.TimeOfDay;
            if (time < StartTime) {
                date = RoundToHour(date.AddDays(-1), EndTime);
            } else if (time > EndTime) {
                date = RoundToHour(date, EndTime);
            }
                        
            DateTime newDate = SkipSomeDays(date, -1);
            if (newDate != date) {
                date = RoundToHour(newDate, EndTime);
            }

            date = DateTimeHelper.Floor(date, Value, RoundToHour(date, StartTime));

            System.Diagnostics.Debug.Assert((StartTime <= date.TimeOfDay) && (date.TimeOfDay <= EndTime));
            return date;
        }

        public override DateTime GetNextDate(DateTime date) {
            date = HasNextDate(date) ? date + Value : date;

            TimeSpan time = date.TimeOfDay;
            if (time < StartTime)
                date = RoundToHour(date, StartTime);
            else if (time > EndTime)
                date = RoundToHour(date.AddDays(1), StartTime);

            DateTime newDate = SkipSomeDays(date, 1);
            if (newDate != date) {
                date = RoundToHour(newDate, StartTime);                
            }

            System.Diagnostics.Debug.Assert((StartTime <= date.TimeOfDay) && (date.TimeOfDay <= EndTime));
            return date;
        }

        DateTime SkipSomeDays(DateTime date, int skipDayCount) {
            int count = DaysToIgnore.Count;
            for (int i = 0; i < count; i++) {
                if (!DaysToIgnore.Contains(date.DayOfWeek))
                    return date;
                date = date.AddDays(skipDayCount);
            }
            return date;
        }
       
        protected DateTime RoundToHour(DateTime date, TimeSpan timeOfDay) {
            return date.Date + timeOfDay;
        }

        protected override bool HasNextDate(DateTime date) {
            return true;
        }
    }


    

}
