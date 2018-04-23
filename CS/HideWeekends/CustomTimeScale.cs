using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraScheduler;


namespace TimelineTimeScales {
    public class TimeScaleWorkWeekDay : TimeScale {
        static readonly TimeSpan StartTime = TimeSpan.FromHours(6);
        static readonly TimeSpan EndTime = TimeSpan.FromHours(21 - 1);

        protected override string DefaultDisplayFormat {
            get { return "d ddd"; }
        }
        protected override string DefaultDisplayName { get { return "Custom Day"; } }
        protected override string DefaultMenuCaption { get { return "Custom Day"; } }
        protected override TimeSpan SortingWeight {
            get { return TimeSpan.FromDays(1); }
        }

        public override DateTime Floor(DateTime date) {
            DateTime start = date.Date;
            if (start.DayOfWeek == DayOfWeek.Monday && date.TimeOfDay < StartTime)
                start = start.AddDays(-3);
            else if (start.DayOfWeek == DayOfWeek.Saturday)
                start = start.AddDays(-1);
            else if (start.DayOfWeek == DayOfWeek.Sunday)
                start = start.AddDays(-2);
            else if (date.TimeOfDay < StartTime)
                start = start.AddDays(-1);
            return start + StartTime;
        }
        protected override bool HasNextDate(DateTime date) {
            int days = GetNextDayOffset(date.DayOfWeek);
            return date <= DateTime.MaxValue.AddDays(-days);
        }
        public override DateTime GetNextDate(DateTime date) {
            int days = GetNextDayOffset(date.DayOfWeek);
            return date.AddDays(days);
        }
        protected int GetNextDayOffset(DayOfWeek dayOfWeek) {
            if (dayOfWeek == DayOfWeek.Friday)
                return 3;
            if (dayOfWeek == DayOfWeek.Saturday)
                return 2;
            return 1;
        }
    }
    public class TimeScaleLessThanDay : TimeScaleFixedInterval {
        static readonly TimeSpan StartTimeLimitation = TimeSpan.FromHours(6);
        static readonly TimeSpan EndTimeLimitation = TimeSpan.FromHours(21);

        public TimeScaleLessThanDay(TimeSpan scaleValue)
            : base(scaleValue) {
        }

        TimeSpan StartTime { get { return StartTimeLimitation; } }
        TimeSpan EndTime { get { return EndTimeLimitation - Value; } }
        protected override string DefaultDisplayFormat { get { return "HH:mm"; } }

        protected override TimeSpan SortingWeight {
            get { return Value; }
        }
        public override DateTime Floor(DateTime date) {
            if (date == DateTime.MinValue || date == DateTime.MaxValue)
                return date;

            if (date.TimeOfDay < StartTime)
                // Round down to the end of the previous working day.
                return RoundToHour(date.AddDays(GetPreviousDayOffset(date.DayOfWeek)), EndTime);

            if (date.TimeOfDay > EndTime) {
                // Round down to the end of the current working day.
                DateTime date1 = date.AddDays(GetPreviousDayOffset1(date.DayOfWeek));
                return RoundToHour(date1, EndTime);
            }
            return base.Floor(date);
        }
        protected override bool HasNextDate(DateTime date) {
            int days = GetNextDayOffset(date.DayOfWeek);
            return date.AddDays(days) <= RoundToHour(DateTime.MaxValue, EndTime);
        }
        protected int GetNextDayOffset(DayOfWeek dayOfWeek) {
            if (dayOfWeek == DayOfWeek.Friday)
                return 3;
            if (dayOfWeek == DayOfWeek.Saturday)
                return 2;
            return 1;
        }
        protected int GetPreviousDayOffset1(DayOfWeek dayOfWeek) {
            if (dayOfWeek == DayOfWeek.Monday)
                return -3;
            if (dayOfWeek == DayOfWeek.Sunday)
                return -2;
            if (dayOfWeek == DayOfWeek.Saturday)
                return -1;

            return 0;
        }
        protected int GetPreviousDayOffset(DayOfWeek dayOfWeek) {
            if (dayOfWeek == DayOfWeek.Monday)
                return -3;
            if (dayOfWeek == DayOfWeek.Sunday)
                return -2;
            return -1;
        }
        public override DateTime GetNextDate(DateTime date) {
            return (date.TimeOfDay >= EndTime) ? RoundToHour(date.AddDays(GetNextDayOffset(date.DayOfWeek)), StartTime) : base.GetNextDate(date);
        }
        protected DateTime RoundToHour(DateTime date, TimeSpan timeOfDay) {
            return date.Date + timeOfDay;
        }
    }
}
