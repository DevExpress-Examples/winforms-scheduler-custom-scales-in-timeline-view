<!-- default file list -->
*Files to look at*:

* [CustomTimeScale.cs](./CS/HideWeekends/CustomTimeScale.cs) (VB: [CustomTimeScale.vb](./VB/HideWeekends/CustomTimeScale.vb))
* [Form1.cs](./CS/HideWeekends/Form1.cs) (VB: [Form1.vb](./VB/HideWeekends/Form1.vb))
* [Program.cs](./CS/HideWeekends/Program.cs) (VB: [Program.vb](./VB/HideWeekends/Program.vb))
* [TimeScaleLessThanDayTests.cs](./CS/HideWeekends/TimeScaleLessThanDayTests.cs) (VB: [TimeScaleLessThanDayTests.vb](./VB/HideWeekends/TimeScaleLessThanDayTests.vb))
<!-- default file list end -->
# Three discontinuous custom time scales for the Timeline view


<p>This example illustrates three discontinuous custom scales for the Timeline view which are properly aligned. The first scale is a day scale which skips Saturdays and Sundays. The second scale is an hour scale which contains only working hours from 6 to 21. The third scale with increments of 30 minutes displays hours from 6 to 21, like the second scale.<br />
Note the paint correction code which amends the color for the columns situated by the scale discontinuities.</p>


<h3>Description</h3>

To&nbsp;test a&nbsp;custom time scale, update the TimeScaleLessThanDayTests class with your custom time scale and call the static&nbsp;TimeScaleLessThanDayTests.Run method.to run the tests.

<br/>


