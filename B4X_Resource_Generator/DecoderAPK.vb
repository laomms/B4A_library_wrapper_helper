Imports System.IO
Imports System.Text

Public Class DecoderAPK
    Public Structure APK_Info
        Dim usesfeatureList As List(Of String)
        Dim PermissionsList As List(Of String)
        Dim ScreenSolutions As String
        Dim ScreenSupport As String
        Dim AppName As String
        Dim IconPath As String
        Dim package As String
        Dim AppVersion As String
        Dim AppVersionCode As String
        Dim MinSdk As String
        Dim MinVersion As String
    End Structure
    Private Shared Sub Decoder_APK(ByVal aaptPath As String, ByVal APKPath As String, ByVal dumpPath As String)
        Dim startInfo = New ProcessStartInfo(aaptPath)
        Dim args As String = String.Format("/k {0} dump badging ""{1}"" > ""{2}"" &exit", aaptPath, APKPath, dumpPath)
        startInfo.Arguments = args
        startInfo.UseShellExecute = False
        startInfo.RedirectStandardOutput = True
        startInfo.CreateNoWindow = True
        startInfo.WindowStyle = ProcessWindowStyle.Hidden
        Using process As System.Diagnostics.Process = System.Diagnostics.Process.Start(startInfo)
            Dim sr = process.StandardOutput
            Debug.Print(sr.ReadToEnd)
        End Using
        If File.Exists(dumpPath) Then
            Dim infosList As New List(Of String)
            Dim apkinfo As New APK_Info
            Using sr = New StreamReader(dumpPath, Encoding.UTF8)
                Dim line As String
                line = sr.ReadLine()
                Do While line IsNot Nothing
                    infosList.Add(line)
                    line = sr.ReadLine()
                Loop
                If infosList.Count > 0 Then
                    For Each info In infosList
                        If info.IndexOf("application:") = 0 Then
                            apkinfo.AppName = GetKeyValue(info, "label=")
                            apkinfo.IconPath = GetKeyValue(info, "icon=")
                            GetAppIcon(APKPath, apkinfo.IconPath)
                        End If
                        If info.IndexOf("package:") = 0 Then
                            apkinfo.package = GetKeyValue(info, "name=")
                            apkinfo.AppVersion = GetKeyValue(info, "versionName=")
                            apkinfo.AppVersionCode = GetKeyValue(info, "versionCode=")
                        End If
                        If info.IndexOf("sdkVersion:") = 0 Then
                            apkinfo.MinSdk = GetKeyValue(info, "sdkVersion:")
                        End If
                        If info.IndexOf("supports-screens:") = 0 Then
                            apkinfo.ScreenSupport = info.Replace("supports-screens:", "").TrimStart().Replace("' '", ", ").Replace("'", "")
                        End If
                        If info.IndexOf("densities:") = 0 Then
                            apkinfo.ScreenSolutions = info.Replace("densities:", "").TrimStart().Replace("' '", ", ").Replace("'", "")
                        End If
                        If info.IndexOf("uses-permission:") = 0 Then
                            Dim permission As String = info.Substring(info.LastIndexOf(".") + 1).Replace("'", "")
                            apkinfo.PermissionsList.Add(permission)
                        End If
                        If info.IndexOf("uses-feature:") = 0 Then
                            Dim feature As String = info.Substring(info.LastIndexOf("."c) + 1).Replace("'", "")
                            apkinfo.usesfeatureList.Add(feature)
                        End If
                    Next info
                End If
            End Using
            File.Delete(dumpPath)
        End If

    End Sub
    Private Shared Function GetAppIcon(ByVal APKPath As String, ByVal iconPath As String) As Image
        If String.IsNullOrEmpty(iconPath) Then
            Return Nothing
        End If
        Dim unzipPath As String = Path.Combine(APKPath, "tools\unzip.exe")
        If Not File.Exists(unzipPath) Then
            unzipPath = Path.Combine(APKPath, "unzip.exe")
        End If
        If Not File.Exists(unzipPath) Then
            Return Nothing
        End If

        Dim destPath As String = Path.Combine(Path.GetTempPath(), Path.GetFileName(iconPath))
        If File.Exists(destPath) Then
            File.Delete(destPath)
        End If
        Dim startInfo = New ProcessStartInfo(unzipPath)
        Dim args As String = String.Format("-j ""{0}"" ""{1}"" -d ""{2}""", APKPath, iconPath, Path.GetDirectoryName(Path.GetTempPath()))
        startInfo.Arguments = args
        startInfo.UseShellExecute = False
        startInfo.CreateNoWindow = True
        Using process As System.Diagnostics.Process = System.Diagnostics.Process.Start(startInfo)
            process.WaitForExit(2000)
        End Using

        If File.Exists(destPath) Then
            Using fs = New FileStream(destPath, FileMode.Open, FileAccess.Read)
                Return Image.FromStream(fs)
            End Using
            File.Delete(destPath)
        End If
        Return Nothing
    End Function

    Private Shared Function GetKeyValue(ByVal info As String, ByVal key As String) As String
        If info.IndexOf(key) <> -1 Then
            Dim start As Integer = info.IndexOf(key) + key.Length + 1
            Return info.Substring(start, info.IndexOf("'", start) - start)
        End If
        Return String.Empty
    End Function
End Class
