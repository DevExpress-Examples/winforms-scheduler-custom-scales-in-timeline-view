Imports System
Imports System.Collections.Generic
Imports System.Text
Imports DevExpress.XtraScheduler
Imports DevExpress.XtraScheduler.Native


Namespace TimelineTimeScales

    Public Class TimeScaleLessThanDayTests

        Public Shared Sub Run()
            Dim test As New TimeScaleLessThanDayTests()

            For hour As Integer = 1 To 9
                test.BordersTests(New TimeScaleLessThanDay(TimeSpan.FromHours(hour)))
                test.TestGeneralScale(New TimeScaleLessThanDay(TimeSpan.FromHours(hour)))
            Next hour

        End Sub

        Private Sub TestGeneralScale(ByVal scale As TimeScale)
            ElasticityTest(scale)
            TestSpecialIntervals(scale, New Date(2009, 2, 2, 20, 30, 0), New Date(2009, 2, 2, 22, 0, 0))
        End Sub

        Public Sub TestSpecialIntervals(ByVal scale As TimeScale, ByVal start As Date, ByVal [end] As Date)
            Dim prevStart As Date = scale.Floor(start)
            Dim prevEnd As Date = scale.Floor([end])
            System.Diagnostics.Debug.Assert(prevStart <= prevEnd, String.Format("Obtain wrong interval on start={0} end={1}", start, [end]))
        End Sub

        Public Sub ElasticityTest(ByVal scale As TimeScale)
            Dim count As Integer = 1000
            Dim [date] As Date = scale.GetPrevDate(Date.Now)

            Dim borders(count - 1) As Date
            For i As Integer = 0 To count - 1
                borders(i) = [date]
                [date] = scale.GetNextDate([date])
            Next i
            For i As Integer = count - 1 To 0 Step -1
                Dim prevDate As Date = [date]
                [date] = scale.GetPrevDate(prevDate)
                System.Diagnostics.Debug.Assert([date] = borders(i), String.Format("step {0}: {1} = scale.GetPrevDate({2}). Expected {3}", i, [date], prevDate, borders(i)))
            Next i
        End Sub

        Public Sub BordersTests(ByVal scale As TimeScaleLessThanDay)
            Dim count As Integer = 1000
            Dim [date] As Date = scale.GetPrevDate(Date.Now)

            Dim borders(count - 1) As Date
            For i As Integer = 0 To count - 1
                borders(i) = [date]
                Dim prevDate As Date = [date]
                [date] = scale.GetNextDate(prevDate)
                Dim prevDate2 As Date = scale.GetPrevDate([date])
                System.Diagnostics.Debug.Assert(prevDate2 = prevDate, String.Format("step {0}: {1} = scale.GetPrevDate({2}). Cant return to prevDate!", i, prevDate2, [date]))
                If scale.StartTime > [date].TimeOfDay OrElse [date].TimeOfDay > scale.EndTime Then
                    System.Diagnostics.Debug.Assert([date] = borders(i), String.Format("step {0}: {1} = scale.GetNextDate({2}). Borders fail!", i, [date], borders(i)))
                End If
            Next i
            For i As Integer = count - 1 To 0 Step -1
                Dim prevDate As Date = [date]
                [date] = scale.GetPrevDate(prevDate)
                System.Diagnostics.Debug.Assert([date] = borders(i), String.Format("step {0}: {1} = scale.GetPrevDate({2}). Expected {3}", i, [date], prevDate, borders(i)))
            Next i
        End Sub
    End Class
End Namespace
