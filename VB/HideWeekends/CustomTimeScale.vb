Imports System
Imports System.Collections.Generic
Imports DevExpress.XtraScheduler
Imports DevExpress.XtraScheduler.Native

Namespace TimelineTimeScales

    Public Class TimeScaleWorkWeekDay
        Inherits TimeScale

        Private Shared ReadOnly StartTime As TimeSpan = TimeSpan.FromHours(6)

        Public Sub New()
            DaysToIgnore = New List(Of DayOfWeek)()
            DaysToIgnore.Add(DayOfWeek.Saturday)
            DaysToIgnore.Add(DayOfWeek.Sunday)
        End Sub

        Protected Overrides ReadOnly Property DefaultDisplayFormat As String
            Get
                Return "d ddd"
            End Get
        End Property

        Protected Overrides ReadOnly Property DefaultDisplayName As String
            Get
                Return "Custom Day"
            End Get
        End Property

        Protected Overrides ReadOnly Property DefaultMenuCaption As String
            Get
                Return "Custom Day"
            End Get
        End Property

        Protected Overrides ReadOnly Property SortingWeight As TimeSpan
            Get
                Return TimeSpan.FromDays(1)
            End Get
        End Property

        Private Property DaysToIgnore As List(Of DayOfWeek)

        Public Overrides Function Floor(ByVal [date] As Date) As Date
            Dim time As TimeSpan = [date].TimeOfDay
            If time >= StartTime Then
                [date] = RoundToHour([date], StartTime)
            Else
                [date] = RoundToHour([date].AddDays(-1), StartTime)
            End If

            [date] = SkipSomeDays([date], -1)
            Return [date]
        End Function

        Protected Overrides Function HasNextDate(ByVal [date] As Date) As Boolean
            Return True
        End Function

        Public Overrides Function GetNextDate(ByVal [date] As Date) As Date
            Dim time As TimeSpan = [date].TimeOfDay
            If time < StartTime Then
                [date] = RoundToHour([date], StartTime)
            Else
                [date] = RoundToHour([date].AddDays(1), StartTime)
            End If

            [date] = SkipSomeDays([date], 1)
            Return [date]
        End Function

        Protected Function RoundToHour(ByVal [date] As Date, ByVal timeOfDay As TimeSpan) As Date
            Return [date].Date + timeOfDay
        End Function

        Private Function SkipSomeDays(ByVal [date] As Date, ByVal skipDayCount As Integer) As Date
            Dim count As Integer = DaysToIgnore.Count
            For i As Integer = 0 To count - 1
                If Not DaysToIgnore.Contains([date].DayOfWeek) Then Return [date]
                [date] = [date].AddDays(skipDayCount)
            Next

            Return [date]
        End Function
    End Class

    Public Class TimeScaleLessThanDay
        Inherits TimeScaleFixedInterval

        Private Shared ReadOnly StartTimeLimitation As TimeSpan = TimeSpan.FromHours(6)

        Private Shared ReadOnly EndTimeLimitation As TimeSpan = TimeSpan.FromHours(21)

        Public Sub New(ByVal scaleValue As TimeSpan)
            MyBase.New(scaleValue)
            DaysToIgnore = New List(Of DayOfWeek)()
            DaysToIgnore.Add(DayOfWeek.Saturday)
            DaysToIgnore.Add(DayOfWeek.Sunday)
        End Sub

        Public ReadOnly Property StartTime As TimeSpan
            Get
                Return StartTimeLimitation
            End Get
        End Property

        Public ReadOnly Property EndTime As TimeSpan
            Get
                Return EndTimeLimitation
            End Get
        End Property

        Protected Overrides ReadOnly Property DefaultDisplayFormat As String
            Get
                Return "HH:mm"
            End Get
        End Property

        Private Property DaysToIgnore As List(Of DayOfWeek)

        Protected Overrides ReadOnly Property SortingWeight As TimeSpan
            Get
                Return Value
            End Get
        End Property

        Public Overrides Function Floor(ByVal [date] As Date) As Date
            If [date] = Date.MinValue OrElse [date] = Date.MaxValue Then Return [date]
            [date] = DateTimeHelper.Floor([date], Value, RoundToHour([date], StartTime))
            Dim time As TimeSpan = [date].TimeOfDay
            If time < StartTime Then
                [date] = RoundToHour([date].AddDays(-1), EndTime)
            ElseIf time > EndTime Then
                [date] = RoundToHour([date], EndTime)
            End If

            Dim newDate As Date = SkipSomeDays([date], -1)
            If newDate <> [date] Then
                [date] = RoundToHour(newDate, EndTime)
            End If

            [date] = DateTimeHelper.Floor([date], Value, RoundToHour([date], StartTime))
            System.Diagnostics.Debug.Assert(StartTime <= [date].TimeOfDay AndAlso [date].TimeOfDay <= EndTime)
            Return [date]
        End Function

        Public Overrides Function GetNextDate(ByVal [date] As Date) As Date
            [date] = If(HasNextDate([date]), [date] + Value, [date])
            Dim time As TimeSpan = [date].TimeOfDay
            If time < StartTime Then
                [date] = RoundToHour([date], StartTime)
            ElseIf time > EndTime Then
                [date] = RoundToHour([date].AddDays(1), StartTime)
            End If

            Dim newDate As Date = SkipSomeDays([date], 1)
            If newDate <> [date] Then
                [date] = RoundToHour(newDate, StartTime)
            End If

            System.Diagnostics.Debug.Assert(StartTime <= [date].TimeOfDay AndAlso [date].TimeOfDay <= EndTime)
            Return [date]
        End Function

        Private Function SkipSomeDays(ByVal [date] As Date, ByVal skipDayCount As Integer) As Date
            Dim count As Integer = DaysToIgnore.Count
            For i As Integer = 0 To count - 1
                If Not DaysToIgnore.Contains([date].DayOfWeek) Then Return [date]
                [date] = [date].AddDays(skipDayCount)
            Next

            Return [date]
        End Function

        Protected Function RoundToHour(ByVal [date] As Date, ByVal timeOfDay As TimeSpan) As Date
            Return [date].Date + timeOfDay
        End Function

        Protected Overrides Function HasNextDate(ByVal [date] As Date) As Boolean
            Return True
        End Function
    End Class
End Namespace
