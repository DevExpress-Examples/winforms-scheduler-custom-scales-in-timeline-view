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

Namespace TimelineTimeScales
    Partial Public Class Form1
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.IContainer = Nothing

        ''' <summary>
        ''' Clean up any resources being used.
        ''' </summary>
        ''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing AndAlso (components IsNot Nothing) Then
                components.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        #Region "Windows Form Designer generated code"

        ''' <summary>
        ''' Required method for Designer support - do not modify
        ''' the contents of this method with the code editor.
        ''' </summary>
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Dim timeRuler1 As New DevExpress.XtraScheduler.TimeRuler()
            Dim timeScaleYear1 As New DevExpress.XtraScheduler.TimeScaleYear()
            Dim timeScaleQuarter1 As New DevExpress.XtraScheduler.TimeScaleQuarter()
            Dim timeScaleMonth1 As New DevExpress.XtraScheduler.TimeScaleMonth()
            Dim timeScaleWeek1 As New DevExpress.XtraScheduler.TimeScaleWeek()
            Dim timeScaleDay1 As New DevExpress.XtraScheduler.TimeScaleDay()
            Dim timeScaleHour1 As New DevExpress.XtraScheduler.TimeScaleHour()
            Dim timeScaleFixedInterval1 As New DevExpress.XtraScheduler.TimeScaleFixedInterval()
            Dim timeRuler2 As New DevExpress.XtraScheduler.TimeRuler()
            Me.schedulerControl1 = New DevExpress.XtraScheduler.SchedulerControl()
            Me.schedulerStorage1 = New DevExpress.XtraScheduler.SchedulerStorage(Me.components)
            Me.panelControl1 = New DevExpress.XtraEditors.PanelControl()
            Me.checkEdit2 = New DevExpress.XtraEditors.CheckEdit()
            Me.checkEdit1 = New DevExpress.XtraEditors.CheckEdit()
            DirectCast(Me.schedulerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
            DirectCast(Me.schedulerStorage1, System.ComponentModel.ISupportInitialize).BeginInit()
            DirectCast(Me.panelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.panelControl1.SuspendLayout()
            DirectCast(Me.checkEdit2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
            DirectCast(Me.checkEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            ' 
            ' schedulerControl1
            ' 
            Me.schedulerControl1.ActiveViewType = DevExpress.XtraScheduler.SchedulerViewType.Timeline
            Me.schedulerControl1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.schedulerControl1.Location = New System.Drawing.Point(0, 30)
            Me.schedulerControl1.Name = "schedulerControl1"
            Me.schedulerControl1.Size = New System.Drawing.Size(649, 438)
            Me.schedulerControl1.Start = New Date(2009, 2, 6, 0, 0, 0, 0)
            Me.schedulerControl1.Storage = Me.schedulerStorage1
            Me.schedulerControl1.TabIndex = 0
            Me.schedulerControl1.Text = "schedulerControl1"
            Me.schedulerControl1.Views.DayView.TimeRulers.Add(timeRuler1)
            timeScaleYear1.Enabled = False
            timeScaleQuarter1.Enabled = False
            timeScaleMonth1.Enabled = False
            timeScaleFixedInterval1.Enabled = False
            Me.schedulerControl1.Views.TimelineView.Scales.Add(timeScaleYear1)
            Me.schedulerControl1.Views.TimelineView.Scales.Add(timeScaleQuarter1)
            Me.schedulerControl1.Views.TimelineView.Scales.Add(timeScaleMonth1)
            Me.schedulerControl1.Views.TimelineView.Scales.Add(timeScaleWeek1)
            Me.schedulerControl1.Views.TimelineView.Scales.Add(timeScaleDay1)
            Me.schedulerControl1.Views.TimelineView.Scales.Add(timeScaleHour1)
            Me.schedulerControl1.Views.TimelineView.Scales.Add(timeScaleFixedInterval1)
            Me.schedulerControl1.Views.TimelineView.WorkTime.End = System.TimeSpan.Parse("21:00:00")
            Me.schedulerControl1.Views.TimelineView.WorkTime.Start = System.TimeSpan.Parse("06:00:00")
            Me.schedulerControl1.Views.WorkWeekView.TimeRulers.Add(timeRuler2)
            ' 
            ' panelControl1
            ' 
            Me.panelControl1.Controls.Add(Me.checkEdit2)
            Me.panelControl1.Controls.Add(Me.checkEdit1)
            Me.panelControl1.Dock = System.Windows.Forms.DockStyle.Top
            Me.panelControl1.Location = New System.Drawing.Point(0, 0)
            Me.panelControl1.Name = "panelControl1"
            Me.panelControl1.Size = New System.Drawing.Size(649, 30)
            Me.panelControl1.TabIndex = 1
            ' 
            ' checkEdit2
            ' 
            Me.checkEdit2.Enabled = False
            Me.checkEdit2.Location = New System.Drawing.Point(178, 5)
            Me.checkEdit2.Name = "checkEdit2"
            Me.checkEdit2.Properties.Caption = "Paint Correction"
            Me.checkEdit2.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Style6
            Me.checkEdit2.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far
            Me.checkEdit2.Size = New System.Drawing.Size(102, 22)
            Me.checkEdit2.TabIndex = 1
            ' 
            ' checkEdit1
            ' 
            Me.checkEdit1.Location = New System.Drawing.Point(12, 5)
            Me.checkEdit1.Name = "checkEdit1"
            Me.checkEdit1.Properties.Caption = "Enable Custom Scales"
            Me.checkEdit1.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Style6
            Me.checkEdit1.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far
            Me.checkEdit1.Size = New System.Drawing.Size(130, 22)
            Me.checkEdit1.TabIndex = 0
            ' 
            ' Form1
            ' 
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(649, 468)
            Me.Controls.Add(Me.schedulerControl1)
            Me.Controls.Add(Me.panelControl1)
            Me.Name = "Form1"
            Me.Text = "Form1"
            DirectCast(Me.schedulerControl1, System.ComponentModel.ISupportInitialize).EndInit()
            DirectCast(Me.schedulerStorage1, System.ComponentModel.ISupportInitialize).EndInit()
            DirectCast(Me.panelControl1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.panelControl1.ResumeLayout(False)
            DirectCast(Me.checkEdit2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
            DirectCast(Me.checkEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub

        #End Region

        Private WithEvents schedulerControl1 As DevExpress.XtraScheduler.SchedulerControl
        Private schedulerStorage1 As DevExpress.XtraScheduler.SchedulerStorage
        Private panelControl1 As DevExpress.XtraEditors.PanelControl
        Private WithEvents checkEdit1 As DevExpress.XtraEditors.CheckEdit
        Private WithEvents checkEdit2 As DevExpress.XtraEditors.CheckEdit
    End Class
End Namespace

