using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Native;


namespace TimelineTimeScales {

    public class TimeScaleLessThanDayTests {

        public static void Run() {
            TimeScaleLessThanDayTests test = new TimeScaleLessThanDayTests();

            for (int hour = 1; hour < 10; hour++) {
                test.BordersTests(new TimeScaleLessThanDay(TimeSpan.FromHours(hour)));
                test.TestGeneralScale(new TimeScaleLessThanDay(TimeSpan.FromHours(hour)));
            }

        }

        void TestGeneralScale(TimeScale scale) {
            ElasticityTest(scale);
            TestSpecialIntervals(scale, new DateTime(2009, 2, 2, 20, 30, 0), new DateTime(2009, 2, 2, 22, 0, 0));
        }

        public void TestSpecialIntervals(TimeScale scale, DateTime start, DateTime end) {
            DateTime prevStart = scale.Floor(start);
            DateTime prevEnd = scale.Floor(end);
            System.Diagnostics.Debug.Assert(prevStart <= prevEnd, String.Format("Obtain wrong interval on start={0} end={1}", start, end));
        }

        public void ElasticityTest(TimeScale scale) {
            int count = 1000;
            DateTime date = scale.GetPrevDate(DateTime.Now);

            DateTime[] borders = new DateTime[count];
            for (int i = 0; i < count; i++) {
                borders[i] = date;
                date = scale.GetNextDate(date);
            }
            for (int i = count - 1; i >= 0; i--) {
                DateTime prevDate = date;
                date = scale.GetPrevDate(prevDate);
                System.Diagnostics.Debug.Assert(date == borders[i], String.Format("step {0}: {1} = scale.GetPrevDate({2}). Expected {3}", i, date, prevDate, borders[i]));
            }
        }

        public void BordersTests(TimeScaleLessThanDay scale) {
            int count = 1000;
            DateTime date = scale.GetPrevDate(DateTime.Now);

            DateTime[] borders = new DateTime[count];
            for (int i = 0; i < count; i++) {
                borders[i] = date;
                DateTime prevDate = date;
                date = scale.GetNextDate(prevDate);
                DateTime prevDate2 = scale.GetPrevDate(date);
                System.Diagnostics.Debug.Assert(prevDate2 == prevDate, String.Format("step {0}: {1} = scale.GetPrevDate({2}). Cant return to prevDate!", i, prevDate2, date));
                if (scale.StartTime > date.TimeOfDay || date.TimeOfDay > scale.EndTime)
                    System.Diagnostics.Debug.Assert(date == borders[i], String.Format("step {0}: {1} = scale.GetNextDate({2}). Borders fail!", i, date, borders[i]));
            }
            for (int i = count - 1; i >= 0; i--) {
                DateTime prevDate = date;
                date = scale.GetPrevDate(prevDate);
                System.Diagnostics.Debug.Assert(date == borders[i], String.Format("step {0}: {1} = scale.GetPrevDate({2}). Expected {3}", i, date, prevDate, borders[i]));
            }
        }
    }
}
