Imports Microsoft.VisualBasic
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
			InitializeComponent()
			schedulerControl1.OptionsView.FirstDayOfWeek = DevExpress.XtraScheduler.FirstDayOfWeek.Monday
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
					Dim customTimeScaleHour As New CustomTimeScaleHour()
					Dim customTimeScaleMinutes As New CustomTimeScaleMinutes()
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
			Dim control As DevExpress.XtraScheduler.SchedulerControl = CType(sender, DevExpress.XtraScheduler.SchedulerControl)
			If Not control.ActiveViewType.Equals(DevExpress.XtraScheduler.SchedulerViewType.Timeline) Then
				Return
			End If

			Dim cell As DevExpress.XtraScheduler.Drawing.TimeCell = CType(e.ObjectInfo, DevExpress.XtraScheduler.Drawing.TimeCell)
			If cell.Selected AndAlso (control.Focused OrElse (Not control.OptionsView.HideSelection)) Then
				cell.SelectionAppearance.FillRectangle(cell.Cache, cell.ContentBounds)
				e.Handled = True
				Return
			End If

			Dim containsWeekDays As Boolean = schedulerControl1.WorkDays.IsWorkDay(cell.Interval.Start)

			Dim color As Color
			If containsWeekDays Then
				color = Me.schedulerControl1.ResourceColorSchemas(0).CellLight
			Else
				color = Me.schedulerControl1.ResourceColorSchemas(0).Cell
			End If
			cell.Cache.FillRectangle(cell.Cache.GetSolidBrush(color), cell.ContentBounds)
			Dim borderColor As Color
			If containsWeekDays Then
				If cell.EndOfHour Then
					borderColor = Me.schedulerControl1.ResourceColorSchemas(0).CellBorderDark
				Else
					borderColor = Me.schedulerControl1.ResourceColorSchemas(0).CellBorder
				End If
			Else
				If cell.EndOfHour Then
					borderColor = Me.schedulerControl1.ResourceColorSchemas(0).CellLightBorderDark
				Else
					borderColor = Me.schedulerControl1.ResourceColorSchemas(0).CellLightBorder
				End If
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
