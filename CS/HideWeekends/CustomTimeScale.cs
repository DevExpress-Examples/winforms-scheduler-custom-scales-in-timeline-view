using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Native;

namespace TimelineTimeScales {

    public class TimeScaleWorkWeekDay : TimeScale {
        protected override string DefaultDisplayFormat { get { return "d ddd"; } }
        protected override string DefaultMenuCaption { get { return "WorkWeek"; } }

        protected override TimeSpan SortingWeight {
            get { return TimeSpan.FromDays(1); }

        }
        public override DateTime Floor(DateTime date)
        {
            DateTime start = FloorByDate(date);
            return start.AddHours(6);
        }

        DateTime FloorByDate(DateTime date) {
            DateTime start = date.Date;
            if(start.DayOfWeek == DayOfWeek.Saturday)
                return start.AddDays(-1);
            if (start.DayOfWeek == DayOfWeek.Sunday)
                return start.AddDays(-2);
            return start;
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
            if(dayOfWeek == DayOfWeek.Friday)
                return 3;
            if(dayOfWeek == DayOfWeek.Saturday)
                return 2;
            return 1;
        }
    }
    public class CustomTimeScaleHour : TimeScale
    {
        private const int StartHour = 6;
        private const int FinishHour = 20;

        protected override string DefaultDisplayFormat { get { return "HH:mm"; } }
        protected override string DefaultMenuCaption { get { return "6:00-21:00"; } }

        protected override TimeSpan SortingWeight
        {
            get { return TimeSpan.FromHours(FinishHour - StartHour + 1); }
        }
        public override DateTime Floor(DateTime date)
        {
            if (date == DateTime.MinValue || date == DateTime.MaxValue)
                return RoundToHour(date, date.Hour);

            if (date.Hour < StartHour)
                // Round down to the end of the previous working day.
                return RoundToHour(date.AddDays(GetPreviousDayOffset(date.DayOfWeek)), FinishHour);

            if (date.Hour > FinishHour)
            {
                // Round down to the end of the current working day.
                DateTime date1 = date.AddDays(GetPreviousDayOffset1(date.DayOfWeek));
                return RoundToHour(date1, FinishHour);
            }

            return RoundToHour(date, date.Hour);
        }
        protected DateTime RoundToHour(DateTime date, int hour)
        {
            return new DateTime(date.Year, date.Month, date.Day, hour, 0, 0);
        }
        protected override bool HasNextDate(DateTime date)
        {
            int days = GetNextDayOffset(date.DayOfWeek);
            return date.AddDays(days) <= RoundToHour(DateTime.MaxValue, FinishHour);
        }
        protected int GetNextDayOffset(DayOfWeek dayOfWeek)
        {
            if (dayOfWeek == DayOfWeek.Friday)
                return 3;
            if (dayOfWeek == DayOfWeek.Saturday)
                return 2;
            return 1;
        }
        protected int GetPreviousDayOffset1(DayOfWeek dayOfWeek)
        {
            if (dayOfWeek == DayOfWeek.Monday)
                return -3;
            if (dayOfWeek == DayOfWeek.Sunday)
                return -2;
            if (dayOfWeek == DayOfWeek.Saturday)
                return -1;

            return 0;
        }

        protected int GetPreviousDayOffset(DayOfWeek dayOfWeek)
        {
            if (dayOfWeek == DayOfWeek.Monday)
                return -3;
            if (dayOfWeek == DayOfWeek.Sunday)
                return -2;
            return -1;
        }
        public override DateTime GetNextDate(DateTime date)
        {
            return (date.Hour > FinishHour - 1) ? RoundToHour(date.AddDays(GetNextDayOffset(date.DayOfWeek)), StartHour) : date.AddHours(1);
        }
    }



    public class CustomTimeScaleMinutes : TimeScale
    {
        private const int FirstIntervalStart = 6 * 60; // minutes
        private const int LastIntervalStart = 21 * 60 - 30; // minutes
        private const int ScaleInterval = 30; // minutes

        protected override string DefaultDisplayFormat { get { return "HH:mm"; } }
        protected override string DefaultMenuCaption { get { return "6:00-21:00"; } }

        protected override TimeSpan SortingWeight
        {
            get { return TimeSpan.FromMinutes(30 /*LastIntervalStart - FirstIntervalStart + 1*/); }
        }
        public override DateTime Floor(DateTime date)
        {
            if (date == DateTime.MinValue || date == DateTime.MaxValue)
                return RoundToScaleInterval(date);


            if (date.TimeOfDay.TotalMinutes < FirstIntervalStart)
                // Round down to the end of the previous working day.
                return RoundToVisibleIntervalEdge(date.AddDays(GetPreviousDayOffset(date.DayOfWeek)), LastIntervalStart);


            if (date.TimeOfDay.TotalMinutes > LastIntervalStart)
                // Round down to the end of the current working day.
                return RoundToVisibleIntervalEdge(date.AddDays(GetPreviousDayOffset1(date.DayOfWeek)), LastIntervalStart);


            return RoundToScaleInterval(date);
        }

        protected DateTime RoundToVisibleIntervalEdge(DateTime dateTime, double minutes)
        {
            return dateTime.Date.AddMinutes(minutes);
        }
        protected DateTime RoundToScaleInterval(DateTime date)
        {
            return DateTimeHelper.Floor(date, TimeSpan.FromMinutes(ScaleInterval));
        }
        protected override bool HasNextDate(DateTime date)
        {
            return date <= RoundToVisibleIntervalEdge(DateTime.MaxValue, LastIntervalStart);
        }
        public override DateTime GetNextDate(DateTime date)
        {
            return (date.TimeOfDay.TotalMinutes > LastIntervalStart - ScaleInterval) ?
                RoundToVisibleIntervalEdge(date.AddDays(GetNextDayOffset(date.DayOfWeek)), FirstIntervalStart) : date.AddMinutes((double)ScaleInterval);
        }

        protected int GetPreviousDayOffset1(DayOfWeek dayOfWeek)
        {
           //if (dayOfWeek == DayOfWeek.Monday)
           //  return -3;
            if (dayOfWeek == DayOfWeek.Sunday)
                return -2;
            if (dayOfWeek == DayOfWeek.Saturday)
                return -1;
            return 0;
        }

        protected int GetPreviousDayOffset(DayOfWeek dayOfWeek)
        {
            if (dayOfWeek == DayOfWeek.Monday)
                return -3;
            if (dayOfWeek == DayOfWeek.Sunday)
                return -2;
            return -1;
        }

        protected int GetNextDayOffset(DayOfWeek dayOfWeek)
        {
            if (dayOfWeek == DayOfWeek.Friday)
                return 3;
            if (dayOfWeek == DayOfWeek.Saturday)
                return 2;
            return 1;
        }
    }
}
