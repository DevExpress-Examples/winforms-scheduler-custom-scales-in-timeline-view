' Developer Express Code Central Example:
' Three discontinuous custom time scales for the Timeline view
' 
' This example illustrates three discontinuous custom scales for the Timeline view
' which are properly aligned. The first scale is a day scale which skips Saturdays
' and Sundays. The second scale is an hour scale which contains only working hours
' from 6 to 21. The third scale with increments of 30 minutes displays hours from
' 6 to 21, like the second scale.
' Note the paint correction code which amends the
' color for the columns situated by the scale discontinuities.
' 
' You can find sample updates and versions for different programming languages here:
' http://www.devexpress.com/example=E1480
Imports System
Imports System.Windows.Forms

Namespace TimelineTimeScales

    Friend Module Program

        ''' <summary>
        ''' The main entry point for the application.
        ''' </summary>
        <STAThread>
        Sub Main()
            Call Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            Call Application.Run(New Form1())
        End Sub
    End Module
End Namespace
