Imports System
Imports System.Windows.Forms
Imports DevExpress.XtraScheduler
Imports DevExpress.XtraEditors
Imports DevExpress.XtraScheduler.Drawing
Imports System.Drawing

Namespace TimelineTimeScales

    Public Partial Class Form1
        Inherits Form

        Public Sub New()
            InitializeComponent()
            schedulerControl1.OptionsView.FirstDayOfWeek = FirstDayOfWeek.Monday
            HideWeekends(False)
        End Sub

        Private Sub schedulerControl1_SelectionChanged(ByVal sender As Object, ByVal e As EventArgs)
            Text = "Selected interval: " & schedulerControl1.SelectedInterval.ToString() & " " & schedulerControl1.SelectedInterval.Start.DayOfWeek.ToString()
        End Sub

        Private Sub checkEdit1_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
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
                    Dim customWorkWeekScale As TimeScaleWorkWeekDay = New TimeScaleWorkWeekDay()
                    Dim customTimeScaleHour As TimeScaleLessThanDay = New TimeScaleLessThanDay(TimeSpan.FromHours(1))
                    Dim customTimeScaleMinutes As TimeScaleLessThanDay = New TimeScaleLessThanDay(TimeSpan.FromMinutes(30))
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
                    Dim dayScale As TimeScaleDay = New TimeScaleDay()
                    scales.Add(dayScale)
                    Dim hourScale As TimeScaleHour = New TimeScaleHour()
                    hourScale.Width = 125
                    scales.Add(hourScale)
                Finally
                    scales.EndUpdate()
                End Try

                checkEdit2.Enabled = False
            End If
        End Sub

        Private Sub schedulerControl1_CustomDrawTimeCell(ByVal sender As Object, ByVal e As CustomDrawObjectEventArgs)
            Dim control As SchedulerControl = CType(sender, SchedulerControl)
            If control.ActiveViewType <> SchedulerViewType.Timeline Then Return
            Dim cell As DevExpress.XtraScheduler.Drawing.TimeCell = CType(e.ObjectInfo, DevExpress.XtraScheduler.Drawing.TimeCell)
            If cell.Selected AndAlso (control.Focused OrElse Not control.OptionsView.HideSelection) Then
                cell.SelectionAppearance.FillRectangle(cell.Cache, cell.ContentBounds)
                e.Handled = True
                Return
            End If

            Dim containsWeekDays As Boolean = schedulerControl1.WorkDays.IsWorkDay(cell.Interval.Start)
            Dim color As Color = If(containsWeekDays, schedulerControl1.ResourceColorSchemas(0).CellLight, schedulerControl1.ResourceColorSchemas(0).Cell)
            cell.Cache.FillRectangle(cell.Cache.GetSolidBrush(color), cell.ContentBounds)
            Dim borderColor As Color
            If containsWeekDays Then
                borderColor = If(cell.EndOfHour, schedulerControl1.ResourceColorSchemas(0).CellBorderDark, schedulerControl1.ResourceColorSchemas(0).CellBorder)
            Else
                borderColor = If(cell.EndOfHour, schedulerControl1.ResourceColorSchemas(0).CellLightBorderDark, schedulerControl1.ResourceColorSchemas(0).CellLightBorder)
            End If

            cell.Cache.FillRectangle(cell.Cache.GetSolidBrush(borderColor), cell.BottomBorderBounds)
            cell.Cache.FillRectangle(cell.Cache.GetSolidBrush(borderColor), cell.LeftBorderBounds)
            cell.Cache.FillRectangle(cell.Cache.GetSolidBrush(borderColor), cell.RightBorderBounds)
            e.Handled = True
        End Sub

        Private Sub checkEdit2_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
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
