using System;
using System.Windows.Forms;
using DevExpress.XtraScheduler;
using DevExpress.XtraEditors;
using DevExpress.XtraScheduler.Drawing;
using System.Drawing;

namespace TimelineTimeScales {
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public Form1() {
            InitializeComponent();

            schedulerControl1.OptionsView.FirstDayOfWeek = FirstDayOfWeek.Monday;
            schedulerControl1.Views.TimelineView.WorkTime.End = System.TimeSpan.Parse("21:00:00");
            schedulerControl1.Views.TimelineView.WorkTime.Start = System.TimeSpan.Parse("06:00:00");

            HideWeekends(false);
        }

        private void schedulerControl1_SelectionChanged(object sender, EventArgs e) {
            Text = "Selected interval: " + schedulerControl1.SelectedInterval.ToString() + " " + schedulerControl1.SelectedInterval.Start.DayOfWeek.ToString();
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e) {
            var editor = (CheckEdit)sender;
            HideWeekends(editor.Checked);
        }

        private void HideWeekends(bool hide)
        {
            var scales = schedulerControl1.TimelineView.Scales;
            try
            {
                scales.BeginUpdate();
                scales.Clear();
                scales.Add(new TimeScaleMonth());
                if (hide)
                {
                    var customWorkWeekScale = new MyTimeScaleWorkWeekDays();
                    var customTimeScaleHour = new MyTimeScaleWorkHours();
                    var customTimeScaleMinutes = new TimeScaleFixedInterval(TimeSpan.FromMinutes(30));

                    customWorkWeekScale.Width = 125;
                    customTimeScaleHour.Width = 125;
                    customTimeScaleMinutes.Width = 125;

                    scales.Add(customWorkWeekScale);
                    scales.Add(customTimeScaleHour);
                    scales.Add(customTimeScaleMinutes);
                }
                else
                {
                    scales.Add(new TimeScaleDay());
                    var hourScale = new TimeScaleHour();
                    hourScale.Width = 125;
                    scales.Add(hourScale);
                }
            }
            finally
            {
                scales.EndUpdate();
            }
        }
    }
}
