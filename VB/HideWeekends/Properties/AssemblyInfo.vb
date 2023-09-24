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
Imports System.Reflection
Imports System.Runtime.InteropServices

' General Information about an assembly is controlled through the following 
' set of attributes. Change these attribute values to modify the information
' associated with an assembly.
<Assembly:AssemblyTitle("WindowsApplication1")>
<Assembly:AssemblyDescription("")>
<Assembly:AssemblyConfiguration("")>
<Assembly:AssemblyCompany("-")>
<Assembly:AssemblyProduct("WindowsApplication1")>
<Assembly:AssemblyCopyright("Copyright © - 2007")>
<Assembly:AssemblyTrademark("")>
<Assembly:AssemblyCulture("")>
' Setting ComVisible to false makes the types in this assembly not visible 
' to COM components.  If you need to access a type in this assembly from 
' COM, set the ComVisible attribute to true on that type.
<Assembly:ComVisible(False)>
' The following GUID is for the ID of the typelib if this project is exposed to COM
<Assembly:Guid("5c313c10-5a1f-4266-a540-e6f445dc8c39")>
' Version information for an assembly consists of the following four values:
'
'      Major Version
'      Minor Version 
'      Build Number
'      Revision
'
<Assembly:AssemblyVersion("1.0.0.0")>
<Assembly:AssemblyFileVersion("1.0.0.0")>
