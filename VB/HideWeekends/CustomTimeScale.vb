Imports System
Imports System.Collections.Generic
Imports System.Text
Imports DevExpress.XtraScheduler
Imports DevExpress.XtraScheduler.Native


Namespace TimelineTimeScales
    Public Class MyTimeScaleWorkHours
        Inherits TimeScaleHour
        Public Sub New()
        End Sub

        Public Overrides Property DisplayName() As String
            Get
                Return "Custom Work Hours"
            End Get
            Set(ByVal value As String)
                MyBase.DisplayName = value
            End Set
        End Property
        Public Overrides Property MenuCaption() As String
            Get
                Return "Custom Work Hours"
            End Get
            Set(ByVal value As String)
                MyBase.MenuCaption = value
            End Set
        End Property

        Public Overrides Function FormatCaption(ByVal start As Date, ByVal [end] As Date) As String
            If start.Hour <= 12 Then
                Return start.Hour.ToString() & " AM"
            Else
                Return (start.Hour - 12).ToString() & " PM"
            End If
        End Function
        Public Overrides Function IsDateVisible(ByVal [date] As Date) As Boolean
            If [date].Hour >= 6 AndAlso [date].Hour <= 21 Then
                Return Not ([date].Hour = 14)
            Else
                Return False
            End If
        End Function
    End Class

    Public Class MyTimeScaleWorkWeekDays
        Inherits TimeScaleDay

        Public Sub New()
        End Sub

        Public Overrides Property DisplayName() As String
            Get
                Return "Custom Week Days"
            End Get
            Set(ByVal value As String)
                MyBase.DisplayName = value
            End Set
        End Property
        Public Overrides Property MenuCaption() As String
            Get
                Return "Custom Week Days"
            End Get
            Set(ByVal value As String)
                MyBase.MenuCaption = value
            End Set
        End Property
        Public Overrides Function IsDateVisible(ByVal [date] As Date) As Boolean
            Return Not ([date].DayOfWeek = DayOfWeek.Saturday OrElse [date].DayOfWeek = DayOfWeek.Sunday)
        End Function
    End Class
End Namespace
