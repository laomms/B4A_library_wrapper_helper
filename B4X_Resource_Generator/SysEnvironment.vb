Imports System.Runtime.InteropServices
Imports Microsoft.Win32

Public Class SysEnvironment
    <DllImport("Kernel32.DLL ", SetLastError:=True)>
    Public Shared Function SetEnvironmentVariable(ByVal lpName As String, ByVal lpValue As String) As Boolean
    End Function

    Public Shared Sub SetPathAPI(ByVal pathValue As String)
        Dim pathlist As String
        pathlist = SysEnvironment.GetSysEnvironmentByName("PATH")
        Dim list() As String = pathlist.Split(";"c)
        Dim isPathExist As Boolean = False


        For Each item As String In list
            If item = pathValue Then
                isPathExist = True
            End If
        Next item
        If Not isPathExist Then
            SetEnvironmentVariable("PATH", pathlist & pathValue & ";")
        End If
    End Sub

    ''' <summary>
    ''' 获取系统环境变量
    ''' </summary>
    ''' <param name="name"></param>
    ''' <returns></returns>
    Public Shared Function GetSysEnvironmentByName(ByVal name As String) As String
        Dim result As String = String.Empty
        Try
            result = OpenSysEnvironment().GetValue(name).ToString() '读取
        Catch e1 As Exception

            Return String.Empty
        End Try
        Return result
    End Function


    ''' <summary>
    ''' 打开系统环境变量注册表
    ''' </summary>
    ''' <returns>RegistryKey</returns>
    Private Shared Function OpenSysEnvironment() As RegistryKey
        Dim regLocalMachine As RegistryKey = Registry.LocalMachine
        Dim regSYSTEM As RegistryKey = regLocalMachine.OpenSubKey("SYSTEM", True) '打开HKEY_LOCAL_MACHINE下的SYSTEM
        Dim regControlSet001 As RegistryKey = regSYSTEM.OpenSubKey("ControlSet001", True) '打开ControlSet001
        Dim regControl As RegistryKey = regControlSet001.OpenSubKey("Control", True) '打开Control
        Dim regManager As RegistryKey = regControl.OpenSubKey("Session Manager", True) '打开Control


        Dim regEnvironment As RegistryKey = regManager.OpenSubKey("Environment", True)
        Return regEnvironment
    End Function
    ''' <summary>
    ''' 设置系统环境变量
    ''' </summary>
    ''' <param name="name">变量名</param>
    ''' <param name="strValue">值</param>
    Public Shared Sub SetSysEnvironment(ByVal name As String, ByVal strValue As String)
        OpenSysEnvironment().SetValue(name, strValue)
    End Sub

    ''' <summary>
    ''' 检测系统环境变量是否存在
    ''' </summary>
    ''' <param name="name"></param>
    ''' <returns></returns>
    Public Function CheckSysEnvironmentExist(ByVal name As String) As Boolean
        If Not String.IsNullOrEmpty(GetSysEnvironmentByName(name)) Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' 添加到PATH环境变量（会检测路径是否存在，存在就不重复）
    ''' </summary>
    ''' <param name="strHome"></param>
    Public Shared Sub SetPathAfter(ByVal strHome As String)
        Dim pathlist As String
        pathlist = GetSysEnvironmentByName("PATH")
        '检测是否以;结尾
        If pathlist.Substring(pathlist.Length - 1, 1) <> ";" Then
            SetSysEnvironment("PATH", pathlist & ";")
            pathlist = GetSysEnvironmentByName("PATH")
        End If
        Dim list() As String = pathlist.Split(";"c)
        Dim isPathExist As Boolean = False
        For Each item As String In list
            If item = strHome Then
                isPathExist = True
            End If
        Next item
        If Not isPathExist Then
            SetSysEnvironment("PATH", pathlist & strHome & ";")
        End If


    End Sub

    Public Shared Sub SetPathBefore(ByVal strHome As String)
        Dim pathlist As String
        pathlist = GetSysEnvironmentByName("PATH")
        Dim list() As String = pathlist.Split(";"c)
        Dim isPathExist As Boolean = False
        For Each item As String In list
            If item = strHome Then
                isPathExist = True
            End If
        Next item
        If Not isPathExist Then
            SetSysEnvironment("PATH", strHome & ";" & pathlist)
        End If
    End Sub


    Public Shared Sub SetPath(ByVal strHome As String)

        Dim pathlist As String
        pathlist = GetSysEnvironmentByName("PATH")
        Dim list() As String = pathlist.Split(";"c)
        Dim isPathExist As Boolean = False

        For Each item As String In list
            If item = strHome Then
                isPathExist = True
            End If
        Next item
        If Not isPathExist Then
            SetSysEnvironment("PATH", pathlist & strHome & ";")
        End If
    End Sub

End Class

