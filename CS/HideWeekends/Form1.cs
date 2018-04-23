using System;
using System.Windows.Forms;
using DevExpress.XtraScheduler;
using DevExpress.XtraEditors;
using DevExpress.XtraScheduler.Drawing;
using System.Drawing;

namespace TimelineTimeScales {
    public partial class Form1 : Form {
        public Form1() {
            TimeScaleLessThanDayTests.Run();
            
            InitializeComponent();
            schedulerControl1.OptionsView.FirstDayOfWeek = FirstDayOfWeek.Monday;
            HideWeekends(false);
        }

        private void schedulerControl1_SelectionChanged(object sender, EventArgs e) {
            Text = "Selected interval: " + schedulerControl1.SelectedInterval.ToString() + " " + schedulerControl1.SelectedInterval.Start.DayOfWeek.ToString();
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e) {
            CheckEdit editor = sender as CheckEdit;
            HideWeekends(editor.Checked);
        }

        private void HideWeekends(bool hide) {
            TimeScaleCollection scales = schedulerControl1.TimelineView.Scales;
            if(hide) {
                scales.BeginUpdate();
                try {
                    scales.Clear();
                    scales.Add(new TimeScaleMonth());
                    TimeScaleWorkWeekDay customWorkWeekScale = new TimeScaleWorkWeekDay();
                    TimeScaleLessThanDay customTimeScaleHour = new TimeScaleLessThanDay(TimeSpan.FromHours(1));
                    TimeScaleLessThanDay customTimeScaleMinutes = new TimeScaleLessThanDay(TimeSpan.FromMinutes(30));
                    customWorkWeekScale.Width = 125;
                    customTimeScaleHour.Width = 125;
                    customTimeScaleMinutes.Width = 125;
                    scales.Add(customWorkWeekScale);
                    scales.Add(customTimeScaleHour);
                    scales.Add(customTimeScaleMinutes);
                    
                }
                finally {
                    scales.EndUpdate();
                }

                checkEdit2.Enabled = true;

            }
            else {
                scales.BeginUpdate();
                try {
                    scales.Clear();
                    scales.Add(new TimeScaleMonth());
                    TimeScaleDay dayScale = new TimeScaleDay();
                    scales.Add(dayScale);
                    TimeScaleHour hourScale = new TimeScaleHour();
                    hourScale.Width = 125;
                    scales.Add(hourScale);
                }
                finally {
                    scales.EndUpdate();
                }

                checkEdit2.Enabled = false;
            }
        
        }

        private void schedulerControl1_CustomDrawTimeCell(object sender, CustomDrawObjectEventArgs e) {
            DevExpress.XtraScheduler.SchedulerControl control = (DevExpress.XtraScheduler.SchedulerControl)sender;
            if(control.ActiveViewType != DevExpress.XtraScheduler.SchedulerViewType.Timeline)
                return;

            DevExpress.XtraScheduler.Drawing.TimeCell cell = (DevExpress.XtraScheduler.Drawing.TimeCell)e.ObjectInfo;
            if(cell.Selected && (control.Focused || !control.OptionsView.HideSelection)) {
                cell.SelectionAppearance.FillRectangle(cell.Cache, cell.ContentBounds);
                e.Handled = true;
                return;
            }

            bool containsWeekDays = schedulerControl1.WorkDays.IsWorkDay(cell.Interval.Start);

            Color color = containsWeekDays ? this.schedulerControl1.ResourceColorSchemas[0].CellLight : this.schedulerControl1.ResourceColorSchemas[0].Cell;
            cell.Cache.FillRectangle(cell.Cache.GetSolidBrush(color), cell.ContentBounds);
            Color borderColor;
            if(containsWeekDays)
                borderColor = cell.EndOfHour ? this.schedulerControl1.ResourceColorSchemas[0].CellBorderDark : this.schedulerControl1.ResourceColorSchemas[0].CellBorder;
            else
                borderColor = cell.EndOfHour ? this.schedulerControl1.ResourceColorSchemas[0].CellLightBorderDark : this.schedulerControl1.ResourceColorSchemas[0].CellLightBorder;
            cell.Cache.FillRectangle(cell.Cache.GetSolidBrush(borderColor), cell.BottomBorderBounds);
            cell.Cache.FillRectangle(cell.Cache.GetSolidBrush(borderColor), cell.LeftBorderBounds);
            cell.Cache.FillRectangle(cell.Cache.GetSolidBrush(borderColor), cell.RightBorderBounds);

            e.Handled = true;
        }

        private void checkEdit2_CheckedChanged(object sender, EventArgs e) {
            CheckEdit editor = sender as CheckEdit;
            if(editor.Checked) {
                schedulerControl1.CustomDrawTimeCell += schedulerControl1_CustomDrawTimeCell;
                schedulerControl1.Refresh();
            }
            else {
                schedulerControl1.CustomDrawTimeCell -= schedulerControl1_CustomDrawTimeCell;
                schedulerControl1.Refresh();
            }


        }


    }
}
