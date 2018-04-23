Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports DevExpress.XtraScheduler
Imports DevExpress.XtraScheduler.Native

Namespace TimelineTimeScales

	Public Class TimeScaleWorkWeekDay
		Inherits TimeScale
		Protected Overrides ReadOnly Property DefaultDisplayFormat() As String
			Get
				Return "d ddd"
			End Get
		End Property
		Protected Overrides ReadOnly Property DefaultMenuCaption() As String
			Get
				Return "WorkWeek"
			End Get
		End Property

		Protected Overrides ReadOnly Property SortingWeight() As TimeSpan
			Get
				Return TimeSpan.FromDays(1)
			End Get

		End Property
		Public Overrides Function Floor(ByVal [date] As DateTime) As DateTime
			Dim start As DateTime = FloorByDate([date])
			Return start.AddHours(6)
		End Function

		Private Function FloorByDate(ByVal [date] As DateTime) As DateTime
			Dim start As DateTime = [date].Date
			If start.DayOfWeek = DayOfWeek.Saturday Then
				Return start.AddDays(-1)
			End If
			If start.DayOfWeek = DayOfWeek.Sunday Then
				Return start.AddDays(-2)
			End If
			Return start
		End Function
		Protected Overrides Function HasNextDate(ByVal [date] As DateTime) As Boolean
			Dim days As Integer = GetNextDayOffset([date].DayOfWeek)
			Return [date] <= DateTime.MaxValue.AddDays(-days)
		End Function
		Public Overrides Function GetNextDate(ByVal [date] As DateTime) As DateTime
			Dim days As Integer = GetNextDayOffset([date].DayOfWeek)
			Return [date].AddDays(days)
		End Function
		Protected Function GetNextDayOffset(ByVal dayOfWeek As DayOfWeek) As Integer
			If dayOfWeek = DayOfWeek.Friday Then
				Return 3
			End If
			If dayOfWeek = DayOfWeek.Saturday Then
				Return 2
			End If
			Return 1
		End Function
	End Class
	Public Class CustomTimeScaleHour
		Inherits TimeScale
		Private Const StartHour As Integer = 6
		Private Const FinishHour As Integer = 20

		Protected Overrides ReadOnly Property DefaultDisplayFormat() As String
			Get
				Return "HH:mm"
			End Get
		End Property
		Protected Overrides ReadOnly Property DefaultMenuCaption() As String
			Get
				Return "6:00-21:00"
			End Get
		End Property

		Protected Overrides ReadOnly Property SortingWeight() As TimeSpan
			Get
				Return TimeSpan.FromHours(FinishHour - StartHour + 1)
			End Get
		End Property
		Public Overrides Function Floor(ByVal [date] As DateTime) As DateTime
			If [date] = DateTime.MinValue OrElse [date] = DateTime.MaxValue Then
				Return RoundToHour([date], [date].Hour)
			End If

			If [date].Hour < StartHour Then
				' Round down to the end of the previous working day.
				Return RoundToHour([date].AddDays(GetPreviousDayOffset([date].DayOfWeek)), FinishHour)
			End If

			If [date].Hour > FinishHour Then
				' Round down to the end of the current working day.
				Dim date1 As DateTime = [date].AddDays(GetPreviousDayOffset1([date].DayOfWeek))
				Return RoundToHour(date1, FinishHour)
			End If

			Return RoundToHour([date], [date].Hour)
		End Function
		Protected Function RoundToHour(ByVal [date] As DateTime, ByVal hour As Integer) As DateTime
			Return New DateTime([date].Year, [date].Month, [date].Day, hour, 0, 0)
		End Function
		Protected Overrides Function HasNextDate(ByVal [date] As DateTime) As Boolean
			Dim days As Integer = GetNextDayOffset([date].DayOfWeek)
			Return [date].AddDays(days) <= RoundToHour(DateTime.MaxValue, FinishHour)
		End Function
		Protected Function GetNextDayOffset(ByVal dayOfWeek As DayOfWeek) As Integer
			If dayOfWeek = DayOfWeek.Friday Then
				Return 3
			End If
			If dayOfWeek = DayOfWeek.Saturday Then
				Return 2
			End If
			Return 1
		End Function
		Protected Function GetPreviousDayOffset1(ByVal dayOfWeek As DayOfWeek) As Integer
			If dayOfWeek = DayOfWeek.Monday Then
				Return -3
			End If
			If dayOfWeek = DayOfWeek.Sunday Then
				Return -2
			End If
			If dayOfWeek = DayOfWeek.Saturday Then
				Return -1
			End If

			Return 0
		End Function

		Protected Function GetPreviousDayOffset(ByVal dayOfWeek As DayOfWeek) As Integer
			If dayOfWeek = DayOfWeek.Monday Then
				Return -3
			End If
			If dayOfWeek = DayOfWeek.Sunday Then
				Return -2
			End If
			Return -1
		End Function
		Public Overrides Function GetNextDate(ByVal [date] As DateTime) As DateTime
			If ([date].Hour > FinishHour - 1) Then
				Return RoundToHour([date].AddDays(GetNextDayOffset([date].DayOfWeek)), StartHour)
			Else
				Return [date].AddHours(1)
			End If
		End Function
	End Class



	Public Class CustomTimeScaleMinutes
		Inherits TimeScale
		Private Const FirstIntervalStart As Integer = 6 * 60 ' minutes
		Private Const LastIntervalStart As Integer = 21 * 60 - 30 ' minutes
		Private Const ScaleInterval As Integer = 30 ' minutes

		Protected Overrides ReadOnly Property DefaultDisplayFormat() As String
			Get
				Return "HH:mm"
			End Get
		End Property
		Protected Overrides ReadOnly Property DefaultMenuCaption() As String
			Get
				Return "6:00-21:00"
			End Get
		End Property

		Protected Overrides ReadOnly Property SortingWeight() As TimeSpan
'INSTANT VB NOTE: Embedded comments are not maintained by Instant VB
'ORIGINAL LINE: get { return TimeSpan.FromMinutes(30 /*LastIntervalStart - FirstIntervalStart + 1*/); }
			Get
				Return TimeSpan.FromMinutes(30)
			End Get
		End Property
		Public Overrides Function Floor(ByVal [date] As DateTime) As DateTime
			If [date] = DateTime.MinValue OrElse [date] = DateTime.MaxValue Then
				Return RoundToScaleInterval([date])
			End If


			If [date].TimeOfDay.TotalMinutes < FirstIntervalStart Then
				' Round down to the end of the previous working day.
				Return RoundToVisibleIntervalEdge([date].AddDays(GetPreviousDayOffset([date].DayOfWeek)), LastIntervalStart)
			End If


			If [date].TimeOfDay.TotalMinutes > LastIntervalStart Then
				' Round down to the end of the current working day.
				Return RoundToVisibleIntervalEdge([date].AddDays(GetPreviousDayOffset1([date].DayOfWeek)), LastIntervalStart)
			End If


			Return RoundToScaleInterval([date])
		End Function

		Protected Function RoundToVisibleIntervalEdge(ByVal dateTime As DateTime, ByVal minutes As Double) As DateTime
			Return dateTime.Date.AddMinutes(minutes)
		End Function
		Protected Function RoundToScaleInterval(ByVal [date] As DateTime) As DateTime
			Return DateTimeHelper.Floor([date], TimeSpan.FromMinutes(ScaleInterval))
		End Function
		Protected Overrides Function HasNextDate(ByVal [date] As DateTime) As Boolean
			Return [date] <= RoundToVisibleIntervalEdge(DateTime.MaxValue, LastIntervalStart)
		End Function
		Public Overrides Function GetNextDate(ByVal [date] As DateTime) As DateTime
			If ([date].TimeOfDay.TotalMinutes > LastIntervalStart - ScaleInterval) Then
				Return RoundToVisibleIntervalEdge([date].AddDays(GetNextDayOffset([date].DayOfWeek)), FirstIntervalStart)
			Else
				Return [date].AddMinutes(CDbl(ScaleInterval))
			End If
		End Function

		Protected Function GetPreviousDayOffset1(ByVal dayOfWeek As DayOfWeek) As Integer
			'If dayOfWeek = DayOfWeek.Monday Then
			'	Return -3
			'End If
			If dayOfWeek = DayOfWeek.Sunday Then
				Return -2
			End If
			If dayOfWeek = DayOfWeek.Saturday Then
				Return -1
			End If
			Return 0
		End Function

		Protected Function GetPreviousDayOffset(ByVal dayOfWeek As DayOfWeek) As Integer
			If dayOfWeek = DayOfWeek.Monday Then
				Return -3
			End If
			If dayOfWeek = DayOfWeek.Sunday Then
				Return -2
			End If
			Return -1
		End Function

		Protected Function GetNextDayOffset(ByVal dayOfWeek As DayOfWeek) As Integer
			If dayOfWeek = DayOfWeek.Friday Then
				Return 3
			End If
			If dayOfWeek = DayOfWeek.Saturday Then
				Return 2
			End If
			Return 1
		End Function
	End Class
End Namespace
