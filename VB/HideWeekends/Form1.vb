Imports System
Imports System.Windows.Forms
Imports DevExpress.XtraScheduler
Imports DevExpress.XtraEditors
Imports DevExpress.XtraScheduler.Drawing
Imports System.Drawing

Namespace TimelineTimeScales
    Partial Public Class Form1
        Inherits Form

        Public Sub New()
            TimeScaleLessThanDayTests.Run()

            InitializeComponent()
            schedulerControl1.OptionsView.FirstDayOfWeek = FirstDayOfWeek.Monday
            HideWeekends(False)
        End Sub

        Private Sub schedulerControl1_SelectionChanged(ByVal sender As Object, ByVal e As EventArgs) Handles schedulerControl1.SelectionChanged
            Text = "Selected interval: " & schedulerControl1.SelectedInterval.ToString() & " " & schedulerControl1.SelectedInterval.Start.DayOfWeek.ToString()
        End Sub

        Private Sub checkEdit1_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles checkEdit1.CheckedChanged
            Dim editor As CheckEdit = TryCast(sender, CheckEdit)
            HideWeekends(editor.Checked)
        End Sub

        Private Sub HideWeekends(ByVal hide As Boolean)
            Dim scales As TimeScaleCollection = schedulerControl1.TimelineView.Scales
            If hide Then
                scales.BeginUpdate()
                Try
                    scales.Clear()
                    scales.Add(New TimeScaleMonth())
                    Dim customWorkWeekScale As New TimeScaleWorkWeekDay()
                    Dim customTimeScaleHour As New TimeScaleLessThanDay(TimeSpan.FromHours(1))
                    Dim customTimeScaleMinutes As New TimeScaleLessThanDay(TimeSpan.FromMinutes(30))
                    customWorkWeekScale.Width = 125
                    customTimeScaleHour.Width = 125
                    customTimeScaleMinutes.Width = 125
                    scales.Add(customWorkWeekScale)
                    scales.Add(customTimeScaleHour)
                    scales.Add(customTimeScaleMinutes)

                Finally
                    scales.EndUpdate()
                End Try

                checkEdit2.Enabled = True

            Else
                scales.BeginUpdate()
                Try
                    scales.Clear()
                    scales.Add(New TimeScaleMonth())
                    Dim dayScale As New TimeScaleDay()
                    scales.Add(dayScale)
                    Dim hourScale As New TimeScaleHour()
                    hourScale.Width = 125
                    scales.Add(hourScale)
                Finally
                    scales.EndUpdate()
                End Try

                checkEdit2.Enabled = False
            End If

        End Sub

        Private Sub schedulerControl1_CustomDrawTimeCell(ByVal sender As Object, ByVal e As CustomDrawObjectEventArgs)
            Dim control As DevExpress.XtraScheduler.SchedulerControl = DirectCast(sender, DevExpress.XtraScheduler.SchedulerControl)
            If control.ActiveViewType <> DevExpress.XtraScheduler.SchedulerViewType.Timeline Then
                Return
            End If

            Dim cell As DevExpress.XtraScheduler.Drawing.TimeCell = CType(e.ObjectInfo, DevExpress.XtraScheduler.Drawing.TimeCell)
            If cell.Selected AndAlso (control.Focused OrElse Not control.OptionsView.HideSelection) Then
                cell.SelectionAppearance.FillRectangle(cell.Cache, cell.ContentBounds)
                e.Handled = True
                Return
            End If

            Dim containsWeekDays As Boolean = schedulerControl1.WorkDays.IsWorkDay(cell.Interval.Start)

            Dim color As Color = If(containsWeekDays, Me.schedulerControl1.ResourceColorSchemas(0).CellLight, Me.schedulerControl1.ResourceColorSchemas(0).Cell)
            cell.Cache.FillRectangle(cell.Cache.GetSolidBrush(color), cell.ContentBounds)
            Dim borderColor As Color
            If containsWeekDays Then
                borderColor = If(cell.EndOfHour, Me.schedulerControl1.ResourceColorSchemas(0).CellBorderDark, Me.schedulerControl1.ResourceColorSchemas(0).CellBorder)
            Else
                borderColor = If(cell.EndOfHour, Me.schedulerControl1.ResourceColorSchemas(0).CellLightBorderDark, Me.schedulerControl1.ResourceColorSchemas(0).CellLightBorder)
            End If
            cell.Cache.FillRectangle(cell.Cache.GetSolidBrush(borderColor), cell.BottomBorderBounds)
            cell.Cache.FillRectangle(cell.Cache.GetSolidBrush(borderColor), cell.LeftBorderBounds)
            cell.Cache.FillRectangle(cell.Cache.GetSolidBrush(borderColor), cell.RightBorderBounds)

            e.Handled = True
        End Sub

        Private Sub checkEdit2_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles checkEdit2.CheckedChanged
            Dim editor As CheckEdit = TryCast(sender, CheckEdit)
            If editor.Checked Then
                AddHandler schedulerControl1.CustomDrawTimeCell, AddressOf schedulerControl1_CustomDrawTimeCell
                schedulerControl1.Refresh()
            Else
                RemoveHandler schedulerControl1.CustomDrawTimeCell, AddressOf schedulerControl1_CustomDrawTimeCell
                schedulerControl1.Refresh()
            End If


        End Sub


    End Class
End Namespace
