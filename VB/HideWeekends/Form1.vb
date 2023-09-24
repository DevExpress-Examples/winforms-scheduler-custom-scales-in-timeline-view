Imports System
Imports DevExpress.XtraScheduler
Imports DevExpress.XtraEditors
Imports DevExpress.XtraScheduler.Drawing
Imports System.Drawing

Namespace TimelineTimeScales

    Public Partial Class Form1
        Inherits XtraForm

        Public Sub New()
            InitializeComponent()
            schedulerControl1.OptionsView.FirstDayOfWeek = FirstDayOfWeek.Monday
            schedulerControl1.Views.TimelineView.WorkTime.End = TimeSpan.Parse("21:00:00")
            schedulerControl1.Views.TimelineView.WorkTime.Start = TimeSpan.Parse("06:00:00")
            HideWeekends(False)
        End Sub

        Private Sub schedulerControl1_SelectionChanged(ByVal sender As Object, ByVal e As EventArgs)
            Text = "Selected interval: " & schedulerControl1.SelectedInterval.ToString() & " " & schedulerControl1.SelectedInterval.Start.DayOfWeek.ToString()
        End Sub

        Private Sub checkEdit1_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            Dim editor = CType(sender, CheckEdit)
            HideWeekends(editor.Checked)
        End Sub

        Private Sub HideWeekends(ByVal hide As Boolean)
            Dim scales = schedulerControl1.TimelineView.Scales
            Try
                scales.BeginUpdate()
                scales.Clear()
                scales.Add(New TimeScaleMonth())
                If hide Then
                    Dim customWorkWeekScale = New MyTimeScaleWorkWeekDays()
                    Dim customTimeScaleHour = New MyTimeScaleWorkHours()
                    Dim customTimeScaleMinutes = New TimeScaleFixedInterval(TimeSpan.FromMinutes(30))
                    customWorkWeekScale.Width = 125
                    customTimeScaleHour.Width = 125
                    customTimeScaleMinutes.Width = 125
                    scales.Add(customWorkWeekScale)
                    scales.Add(customTimeScaleHour)
                    scales.Add(customTimeScaleMinutes)
                Else
                    scales.Add(New TimeScaleDay())
                    Dim hourScale = New TimeScaleHour()
                    hourScale.Width = 125
                    scales.Add(hourScale)
                End If
            Finally
                scales.EndUpdate()
            End Try
        End Sub
    End Class
End Namespace
