Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Xml
Imports Microsoft.Win32

Public Class CompileForm
    Private Sub TextBox1_DragEnter(sender As Object, e As DragEventArgs) Handles TextBox1.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub
    Private Sub TextBox1_DragDrop(sender As Object, e As DragEventArgs) Handles TextBox1.DragDrop
        Dim files() As String = e.Data.GetData(DataFormats.FileDrop)
        For Each path In files
            TextBox1.Text = String.Empty
            TextBox1.ForeColor = Color.Black
            TextBox1.TextAlign = HorizontalAlignment.Left
            TextBox1.Text = path
            ProjectPath = path
            Dim OpenCUKey As RegistryKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, IIf(Environment.Is64BitOperatingSystem, RegistryView.Registry64, RegistryView.Registry32))
            Using subRegKey = OpenCUKey.OpenSubKey("Software\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers", True)
                If subRegKey Is Nothing Then
                    Using subKey = OpenCUKey.CreateSubKey("Software\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers", RegistryKeyPermissionCheck.Default)
                        subKey.SetValue("ProjectPath", path, RegistryValueKind.String)
                    End Using
                Else
                    subRegKey.SetValue("ProjectPath", path, RegistryValueKind.String)
                End If
            End Using
        Next
    End Sub
    Private Sub CompileForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MaximizeBox = False
        Me.AllowDrop = True
        TextBox1.AllowDrop = True
        RichTextBox1.Text = ""
        If ProjectPath <> "" Then
            TextBox1.TextAlign = HorizontalAlignment.Left
            TextBox1.Text = ProjectPath
            Dim javaList = Directory.GetFiles(ProjectPath, "*.*", SearchOption.AllDirectories).Where(Function(f) New List(Of String) From {".kt", ".java"}.IndexOf(Path.GetExtension(f)) >= 0).ToArray()
            If javaList.Count > 0 Then
                For Each javaFile In javaList
                    Dim fileContent As String = File.ReadAllText(javaFile)
                    Using writer As New StreamWriter(javaFile, False, Encoding.GetEncoding("Windows-1252"))
                        writer.Write(fileContent)
                    End Using
                Next
            End If
        Else
            Dim OpenCUKey As RegistryKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, IIf(Environment.Is64BitOperatingSystem, RegistryView.Registry64, RegistryView.Registry32))
            Using subRegKey = OpenCUKey.OpenSubKey("Software\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers", True)
                If Not subRegKey Is Nothing Then
                    If Not subRegKey.GetValue("ProjectPath") Is Nothing Then
                        TextBox1.TextAlign = HorizontalAlignment.Left
                        TextBox1.Text = subRegKey.GetValue("ProjectPath")
                        ProjectPath = TextBox1.Text
                    End If
                End If
            End Using
        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        RichTextBox1.Text = ""
        Dim java As String = ""
        Dim cp As String = ""
        If ProjectPath = "" Or ProjectPath.Contains("\") = False Then Return
        If B4AShared = "" Or Core = "" Or androidjarPath = "" Then Return
        If Directory.Exists(ProjectPath + "\bin\classes") = False Then
            Directory.CreateDirectory(ProjectPath + "\bin\classes")
        Else
            Dim dir = New DirectoryInfo(ProjectPath + "\bin\classes")
            For Each subdir As DirectoryInfo In dir.GetDirectories()
                Try
                    subdir.Delete(True)
                Catch ex As Exception

                End Try
            Next
            For Each file As FileInfo In dir.GetFiles()
                Try
                    file.Delete()
                Catch ex As Exception

                End Try
            Next
        End If
        If Directory.Exists(ProjectPath + "\libs") Then
            Dim cpList = Directory.GetFiles(ProjectPath + "\libs", "*.*", SearchOption.TopDirectoryOnly).Where(Function(f) New List(Of String) From {".aar"}.IndexOf(Path.GetExtension(f)) >= 0).ToArray()
            If cpList.Count > 0 Then
                'cp = """" + androidjarPath + """;""" + B4AShared + """;""" + Core + """;" +  String.Join(";", cpList).Replace(ProjectPath + "\", "").Replace("\", "/")
                cp = """" + androidjarPath + """;""" + B4AShared + """;""" + Core + """;" + "libs/*;libs;"
            Else
                cp = """" + androidjarPath + """;""" + B4AShared + """;""" + Core + """;"
            End If
        Else
            cp = """" + androidjarPath + """;""" + B4AShared + """;""" + Core + """;"
        End If
        Dim javaList = Directory.GetFiles(ProjectPath, "*.*", SearchOption.AllDirectories).Where(Function(f) New List(Of String) From {".java"}.IndexOf(Path.GetExtension(f)) >= 0).ToArray()  '{".kt", ".java"}
        If javaList.Count > 0 Then
            java = String.Join(" ", javaList).Replace(ProjectPath + "\", "").Replace("\", "/")
            'Debug.Print(java)
        End If
        Dim javac = SysEnvironment.GetSysEnvironmentByName("JAVA_HOME") + "\bin\javac"

        Using p1 As New Process
            p1.StartInfo.CreateNoWindow = True
            p1.StartInfo.Verb = "runas"
            p1.StartInfo.FileName = "cmd.exe"
            p1.StartInfo.WorkingDirectory = ProjectPath
            p1.StartInfo.Arguments = String.Format(" /c {0} -Xmaxerrs 1 -nowarn -Xlint:none -J-Xmx512m  -version -encoding UTF-8 -d {1} -cp {2} {3}  2>>&1", javac, "bin/classes", cp, java)
            p1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
            p1.StartInfo.UseShellExecute = False
            p1.StartInfo.RedirectStandardOutput = True
            p1.Start()
            Dim output As String
            Using streamreader As System.IO.StreamReader = p1.StandardOutput
                output = streamreader.ReadToEnd()
                Debug.Print(output)
                RichTextBox1.Text = output
            End Using
        End Using

        If HasSubfoldersAlternate(ProjectPath + "\bin\classes") Then
            Dim startInfo = New ProcessStartInfo(My.Computer.FileSystem.SpecialDirectories.Temp + "\B4X\jar.exe")
            Dim args As String = String.Format(" cvf {0} .", Path.GetDirectoryName(ProjectPath) + "\" + Path.GetFileName(ProjectPath) + ".jar")
            startInfo.Arguments = args
            startInfo.UseShellExecute = False
            startInfo.RedirectStandardOutput = True
            startInfo.WorkingDirectory = ProjectPath + "\bin\classes"
            startInfo.CreateNoWindow = True
            startInfo.WindowStyle = ProcessWindowStyle.Hidden
            Using process As System.Diagnostics.Process = System.Diagnostics.Process.Start(startInfo)
                Dim sr = process.StandardOutput
                Debug.Print(sr.ReadToEnd)
            End Using

            Dim javadoc = SysEnvironment.GetSysEnvironmentByName("JAVA_HOME") + "\bin\javadoc"
            Using p1 As New Process
                p1.StartInfo.CreateNoWindow = True
                p1.StartInfo.Verb = "runas"
                p1.StartInfo.FileName = "cmd.exe"
                p1.StartInfo.WorkingDirectory = ProjectPath
                p1.StartInfo.Arguments = String.Format(" /c {0} -doclet BADoclet -docletpath {1} -classpath {2} -ProjectPath {3} -b4atarget {4} -b4aignore b4a.java.library.first {5}  2>>&1", javadoc, My.Computer.FileSystem.SpecialDirectories.Temp + "\B4X", cp, "src", Path.GetDirectoryName(ProjectPath) + "\" + Path.GetFileName(ProjectPath) + ".xml", java)
                p1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
                p1.StartInfo.UseShellExecute = False
                p1.StartInfo.RedirectStandardOutput = True
                p1.Start()
                Dim output As String
                Using streamreader As System.IO.StreamReader = p1.StandardOutput
                    output = streamreader.ReadToEnd()
                    Debug.Print(output)
                    RichTextBox1.AppendText(vbNewLine + output)
                End Using
            End Using
        End If

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        RichTextBox1.Text = ""
        If ProjectPath = "" Or ProjectPath.Contains("\") = False Then Return
        If CheckBox1.Checked = True Then
            If BackgroundWorker1.IsBusy = False Then
                BackgroundWorker1.RunWorkerAsync()
            End If
        End If
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Dim javaList = Directory.GetFiles(ProjectPath, "*.*", SearchOption.AllDirectories).Where(Function(f) New List(Of String) From {".java"}.IndexOf(Path.GetExtension(f)) >= 0).ToArray()
        If javaList.Count > 0 Then
            For Each javaFile In javaList
                Dim fileContent As String = File.ReadAllText(javaFile)
                Dim importList = Regex.Matches(fileContent, "(?<=import ).*?(?=;)", RegexOptions.Multiline Or RegexOptions.IgnoreCase).Cast(Of Match)().Select(Function(m) m.Value).ToList()
                If importList.Count > 0 Then
                    For Each import In importList
                        If AndroidXMigrationDic.ContainsKey(import.Trim) Then
                            fileContent = fileContent.Replace(import.Trim, AndroidXMigrationDic(import).Trim)
                        End If
                    Next
                    Using writer As New StreamWriter(javaFile, False, Encoding.GetEncoding("Windows-1252"))
                        writer.Write(fileContent)
                    End Using
                End If
            Next
            RichTextBox1.Invoke(New MethodInvoker(Sub() RichTextBox1.Text = "MigrationAndroidX finished"))
        End If
        Dim xmlfiles = Directory.GetFiles(ProjectPath, SearchOption.AllDirectories).Where(Function(f) New List(Of String) From {".xml"}.IndexOf(Path.GetExtension(f)) >= 0).ToArray()
        If xmlfiles.Count > 0 Then
            For Each xmlfile In xmlfiles
                Dim fileContent As String = File.ReadAllText(xmlfile)
                If fileContent.Contains("android.support.constraint.ConstraintLayout") Then
                    fileContent = fileContent.Replace("android.support.constraint.ConstraintLayout", "androidx.constraintlayout.widget.ConstraintLayout")
                    Using writer As New StreamWriter(xmlfile, False, Encoding.GetEncoding("ISO-8859-1"))
                        writer.Write(fileContent)
                    End Using
                End If
            Next
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If ProjectPath = "" Or ProjectPath.Contains("\") = False Then Return
        If CheckBox1.Checked = True Then
            If RichTextBox1.Text = "" Then Return
            If BackgroundWorker2.IsBusy = False Then
                Dim arguments As New List(Of Object)
                arguments.Add(RichTextBox1.Text)
                BackgroundWorker2.RunWorkerAsync(arguments)
            End If
        End If
    End Sub

    Private Async Sub BackgroundWorker2_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker2.DoWork
        Dim arguments As List(Of Object) = TryCast(e.Argument, List(Of Object))
        Dim filepath As String = ""
        Dim files As String = New Regex("javac.[\s\S]*?(?=error)").Match(arguments(0)).Value.Trim
        If files <> "" Then
            filepath = ProjectPath + "\" + New Regex("src\\.[\s\S]*?(?=\:)").Match(files).Value.Trim
        End If
        Dim errorInfo = New Regex("(?<=error:).[\s\S]*?(?=error)").Match(arguments(0)).Value.Trim
        If errorInfo.Contains("does not exist") Then
            Dim packageName = New Regex("(?<=error: package).[\s\S]*?(?=does not exist)").Match(arguments(0)).Value.Trim
            If packageName = "" Then Return
            ItemsDictionary = New Dictionary(Of String, String)
            ItemsDictionary.Clear()
            ItemsDictionary = Await DownloadLib.SearchItem(packageName)
            If ItemsDictionary.Count > 0 Then
                SelectForm.ShowDialog()
                If SelectItem <> "" Then
                    Dim url = ItemsDictionary(SelectItem.Split("-")(0).Trim)
                    ItemsDictionary.Clear()
                    ItemsDictionary = Await DownloadLib.GetVersion(url)
                    If ItemsDictionary.Count > 0 Then
                        SelectForm.ShowDialog()
                        If SelectItem <> "" Then
                            url = url + "/" + SelectItem.Split("-")(0).Trim
                            ItemsDictionary.Clear()
                            ItemsDictionary = Await DownloadLib.GetPomFile(url)
                            If ItemsDictionary.Count > 0 Then
                                For Each keyPair In ItemsDictionary
                                    If keyPair.Key.Contains("pom") Then
                                        Dim document As New XmlDocument()
                                        document.Load(keyPair.Value)
                                        If File.Exists(packageName + "\pom.xml") Then File.Delete(packageName + "\pom.xml")
                                        File.WriteAllText(packageName + "\pom.xml", document.InnerXml)
                                    ElseIf keyPair.Key.Contains("aar") Or keyPair.Key.Contains("jar") Then
                                        downlink = keyPair.Value
                                        targetPath = ProjectPath + "\libs\" + Path.GetFileName(keyPair.Value)
                                        needSelect = False
                                        DownloadForm.ShowDialog()
                                    End If
                                Next
                            End If
                        End If
                    End If
                End If
            End If
            If packageName = "com.android.vending.billing" Then

            End If
        ElseIf errorInfo.Contains("cannot find symbol") Then

        ElseIf errorInfo.Contains("cannot be accessed from outside package") Then

        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim folderDlg As FolderBrowserDialog = New FolderBrowserDialog()
        folderDlg.ShowNewFolderButton = True
        Dim result As DialogResult = folderDlg.ShowDialog()
        If result = DialogResult.OK Then
            TextBox1.ForeColor = Color.Black
            TextBox1.TextAlign = HorizontalAlignment.Left
            TextBox1.Text = folderDlg.SelectedPath
            ProjectPath = folderDlg.SelectedPath
            Dim OpenCUKey As RegistryKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, IIf(Environment.Is64BitOperatingSystem, RegistryView.Registry64, RegistryView.Registry32))
            Using subRegKey = OpenCUKey.OpenSubKey("Software\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers", True)
                If subRegKey Is Nothing Then
                    Using subKey = OpenCUKey.CreateSubKey("Software\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers", RegistryKeyPermissionCheck.Default)
                        subKey.SetValue("ProjectPath", folderDlg.SelectedPath, RegistryValueKind.String)
                    End Using
                Else
                    subRegKey.SetValue("ProjectPath", folderDlg.SelectedPath, RegistryValueKind.String)
                End If
            End Using
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub TextBox1_Click(sender As Object, e As EventArgs) Handles TextBox1.Click
        Dim clipstring As String = GetText()
        If New Regex("^(?:[c-zC-Z]\:|\\)(\\[a-zA-Z_\-\s0-9\.]+)+").Match(clipstring).Success Then
            TextBox1.ForeColor = Color.Black
            TextBox1.TextAlign = HorizontalAlignment.Left
            TextBox1.Text = clipstring
            Dim OpenCUKey As RegistryKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, IIf(Environment.Is64BitOperatingSystem, RegistryView.Registry64, RegistryView.Registry32))
            Using subRegKey = OpenCUKey.OpenSubKey("Software\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers", True)
                If subRegKey Is Nothing Then
                    Using subKey = OpenCUKey.CreateSubKey("Software\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers", RegistryKeyPermissionCheck.Default)
                        subKey.SetValue("ProjectPath", clipstring, RegistryValueKind.String)
                    End Using
                Else
                    subRegKey.SetValue("ProjectPath", clipstring, RegistryValueKind.String)
                End If
            End Using
        End If
    End Sub
End Class