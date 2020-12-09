Imports System.Globalization
Imports System.IO
Imports System.IO.Compression
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
        Dim javafiles As String = ""
        Dim cp As String = ""
        Dim cp_javadoc As String = ""
        If ProjectPath = "" Or ProjectPath.Contains("\") = False Then Return
        If B4AShared = "" Then
            MsgBox("No B4AShared.jar path specified", vbInformation + vbMsgBoxSetForeground, "Error") : Return
        ElseIf Core = "" Then
            MsgBox("No Core.jar path specified", vbInformation + vbMsgBoxSetForeground, "Error") : Return
        ElseIf androidjarPath = "" Then
            MsgBox("No android platforms path specified", vbInformation + vbMsgBoxSetForeground, "Error") : Return
        End If


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
            Dim cpList = Directory.GetFiles(ProjectPath + "\libs", "*.*", SearchOption.TopDirectoryOnly).Where(Function(f) New List(Of String) From {".jar", ".aar"}.IndexOf(Path.GetExtension(f)) >= 0).ToArray()
            If cpList.Count > 0 Then
                'cp = """" + androidjarPath + """;""" + B4AShared + """;""" + Core + """;" +  String.Join(";", cpList).Replace(ProjectPath + "\", "").Replace("\", "/")
                cp = """" + androidjarPath + """;""" + B4AShared + """;""" + Core + """;" + "libs/*;libs;"
                cp_javadoc = """" + B4AShared + """;""" + Core + """;" + "libs/*;libs;"
            Else
                cp = """" + androidjarPath + """;""" + B4AShared + """;""" + Core + """;"
                cp_javadoc = """" + B4AShared + """;""" + Core + """;"
            End If
        Else
            cp = """" + androidjarPath + """;""" + B4AShared + """;""" + Core + """;"
            cp_javadoc = """" + B4AShared + """;""" + Core + """;"
        End If

        Dim aidlfile As String = ""
        Dim aidlList = Directory.GetFiles(ProjectPath, "*.*", SearchOption.AllDirectories).Where(Function(f) New List(Of String) From {".aidl"}.IndexOf(Path.GetExtension(f)) >= 0).ToArray()
        If aidlList.Count > 0 Then
            For Each item In aidlList
                aidlfile = aidlfile + " " + item
            Next
            Dim aidlPath = My.Computer.FileSystem.SpecialDirectories.Temp + "\B4X\aidl.exe"
            Dim frameworkPath = Path.GetDirectoryName(androidjarPath) + "\framework.aidl"
            Using p1 As New Process
                p1.StartInfo.CreateNoWindow = True
                p1.StartInfo.Verb = "runas"
                p1.StartInfo.FileName = "cmd.exe"
                p1.StartInfo.WorkingDirectory = ProjectPath
                p1.StartInfo.Arguments = String.Format(" /c {0} -I{1} -p{2} {3} 2>>&1", aidlPath, "src", frameworkPath, aidlfile)
                Debug.Print(p1.StartInfo.Arguments)
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
        End If

        Dim Kotlinfile As String = ""
        'Dim cpKotlin As String = ""
        'Dim cpKotlinList = Directory.GetFiles(ProjectPath + "\libs", "*.*", SearchOption.TopDirectoryOnly).Where(Function(f) New List(Of String) From {".jar", ".aar"}.IndexOf(Path.GetExtension(f)) >= 0).ToArray()
        'If cpKotlinList.Count > 0 Then
        '    cpKotlin = """" + androidjarPath + """:" + String.Join(":", cpKotlinList)
        'Else
        '    cpKotlin = """" + androidjarPath + """:"
        'End If
        Dim KotlinList = Directory.GetFiles(ProjectPath, "*.*", SearchOption.AllDirectories).Where(Function(f) New List(Of String) From {".kt"}.IndexOf(Path.GetExtension(f)) >= 0).ToArray()
        If KotlinList.Count > 0 Then
            For Each item In KotlinList
                Dim kotlinName = Path.GetFileName(item).Split(".")(0).Trim
                If AndroidProjectPath <> "" Then
                    Dim KotlinSource = Directory.GetFiles(AndroidProjectPath, "*.*", SearchOption.AllDirectories).Where(Function(f) New List(Of String) From {"" + kotlinName + ".java"}.IndexOf(Path.GetFileName(f)) >= 0).ToArray()
                    If KotlinSource.Count > 0 Then
                        File.Copy(KotlinSource(0), Path.GetDirectoryName(item) + "\" + kotlinName + ".java", True)
                        'File.Delete(item)
                    End If
                End If
            Next
            '    If KotlinPath = "" Then MsgBox("No kotlin compiler found", vbInformation + vbMsgBoxSetForeground, "Error") : Return
            '    Dim kotlin_stdlib_jdk7 = Path.GetDirectoryName(Kotlinfile) + "\lib\kotlin-stdlib-jdk7.jar"
            '    Using p1 As New Process
            '        p1.StartInfo.CreateNoWindow = True
            '        p1.StartInfo.Verb = "runas"
            '        p1.StartInfo.FileName = "cmd.exe"
            '        p1.StartInfo.WorkingDirectory = ProjectPath
            '        p1.StartInfo.Arguments = String.Format(" /c {0} -classpath {1} {2} 2>>&1", KotlinPath, androidjarPath, Kotlinfile)
            '        Debug.Print(p1.StartInfo.Arguments)
            '        p1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
            '        p1.StartInfo.UseShellExecute = False
            '        p1.StartInfo.RedirectStandardOutput = True
            '        p1.Start()
            '        Dim output As String
            '        Using streamreader As System.IO.StreamReader = p1.StandardOutput
            '            output = streamreader.ReadToEnd()
            '            Debug.Print(output)
            '            RichTextBox1.Text = output
            '        End Using
            '    End Using
        End If
        Dim javaList = Directory.GetDirectories(ProjectPath + "\src", "*.*", SearchOption.AllDirectories).Where(Function(f) Directory.GetFiles(f, "*.java", SearchOption.TopDirectoryOnly).Count > 0).Select(Function(x) x + "\*.java").ToList.ToArray()
        If javaList.Count > 0 Then
            javafiles = String.Join(" ", javaList).Replace(ProjectPath + "\", "").Replace("\", "/")
        End If

        If SysEnvironment.CheckSysEnvironmentExist("JAVA_HOME") = False Then MsgBox("The JAVA_HOME environment has not been set", vbInformation + vbMsgBoxSetForeground, "Error") : Return
        Dim javac = SysEnvironment.GetSysEnvironmentByName("JAVA_HOME") + "\bin\javac"

        Using p1 As New Process
            p1.StartInfo.CreateNoWindow = True
            p1.StartInfo.Verb = "runas"
            p1.StartInfo.FileName = "cmd.exe"
            p1.StartInfo.WorkingDirectory = ProjectPath
            p1.StartInfo.Arguments = String.Format(" /c {0} -Xmaxerrs 1 -Xlint:none -J-Xmx512m  -version -encoding UTF-8 -d {1} -sourcepath {2} -cp {3} {4}  2>>&1", javac, "bin/classes", "src", cp, javafiles)
            Debug.Print(p1.StartInfo.Arguments)
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
        Dim jarfile As String = AdditionalLibrariesPath + "\" + New CultureInfo("en-US").TextInfo.ToTitleCase(Path.GetFileName(ProjectPath)) + ".jar"
        If File.Exists(jarfile) Then File.Delete(jarfile)
        If HasSubfoldersAlternate(ProjectPath + "\bin\classes") And RichTextBox1.Text.Contains("error") = False Then
            Dim startInfo = New ProcessStartInfo(My.Computer.FileSystem.SpecialDirectories.Temp + "\B4X\jar.exe")
            Dim args As String = String.Format(" cvf ""{0}"" .", jarfile)
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

            'Dim docletfiles = ""
            'javaList = Directory.GetFiles(ProjectPath + "\src", "*.*", SearchOption.AllDirectories).Where(Function(f) New List(Of String) From {".java"}.IndexOf(Path.GetExtension(f)) >= 0).ToArray()
            'If javaList.Count > 0 Then
            '    docletfiles = String.Join(" ", javaList)
            'End If

            If SysEnvironment.CheckSysEnvironmentExist("JAVA_HOME") = False Then MsgBox("The JAVA_HOME environment has not been set", vbInformation + vbMsgBoxSetForeground, "Error") : Return
            Dim javadoc = SysEnvironment.GetSysEnvironmentByName("JAVA_HOME") + "\bin\javadoc"
            Dim savefile = AdditionalLibrariesPath + "\" + New CultureInfo("en-US").TextInfo.ToTitleCase(Path.GetFileName(ProjectPath)) + ".xml"
            If File.Exists(savefile) Then File.Delete(savefile)
            Using p1 As New Process
                p1.StartInfo.CreateNoWindow = True
                p1.StartInfo.Verb = "runas"
                p1.StartInfo.FileName = "cmd.exe"
                p1.StartInfo.WorkingDirectory = ProjectPath
                p1.StartInfo.Arguments = String.Format(" /c {0} -doclet BADoclet -docletpath {1} -sourcepath {2} -classpath {3} -b4atarget ""{4}"" -b4aignore org,com.android,com.example,com.hoho {5}  2>>&1", javadoc, My.Computer.FileSystem.SpecialDirectories.Temp + "\B4X\", "src", cp_javadoc, savefile, javafiles)
                Debug.Print(p1.StartInfo.Arguments)
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
            RichTextBox1.AppendText("Compilation is complete")
            RichTextBox1.SelectionStart = RichTextBox1.Text.Length
            RichTextBox1.ScrollToCaret()
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

        If RichTextBox1.Text = "" Then Return
        If BackgroundWorker2.IsBusy = False Then
            Dim arguments As New List(Of Object)
            arguments.Add(RichTextBox1.Text)
            BackgroundWorker2.RunWorkerAsync(arguments)
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
        If errorInfo.Contains("does not exist") Or errorInfo.Contains("not found") Then
            Dim packageName = ""
            If errorInfo.Contains("does not exist") Then
                packageName = New Regex("(?<=error: package).[\s\S]*?(?=does not exist)").Match(arguments(0)).Value.Trim
            Else
                packageName = New Regex("(?<=class file for ).[\s\S]*?(?= not found)").Match(arguments(0)).Value.Trim
            End If
            If packageName = "" Then Return
            If packageName.Contains(".") Then
                packageName = packageName.Substring(0, packageName.LastIndexOf("."))
            End If
            ItemsDictionary = New Dictionary(Of String, String)
            ItemsDictionary.Clear()
            ItemsDictionary = Await DownloadLib.SearchItem(packageName)
            If ItemsDictionary.Count > 0 Then
                Dim frm As New SelectForm
                frm.ShowDialog()
                If SelectItem <> "" Then
                    Dim url = ItemsDictionary(SelectItem.Split("-")(0).Trim)
                    ItemsDictionary.Clear()
                    ItemsDictionary = Await DownloadLib.GetVersion(url)
                    If ItemsDictionary.Count > 0 Then
                        Dim frm2 As New SelectForm
                        frm2.ShowDialog()
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
                                    ElseIf keyPair.Key.Contains("jar") Then
                                        downlink = keyPair.Value
                                        targetPath = ProjectPath + "\libs\" + Path.GetFileName(keyPair.Value)
                                        needSelect = False
                                        Dim frm3 As New DownloadForm
                                        frm3.ShowDialog()
                                    ElseIf keyPair.Key.Contains("aar") Then
                                        downlink = keyPair.Value
                                        targetPath = ProjectPath + "\libs\" + Path.GetFileName(keyPair.Value)
                                        needSelect = False
                                        Dim frm3 As New DownloadForm
                                        frm3.ShowDialog()
                                        Using archive As ZipArchive = ZipFile.OpenRead(targetPath)
                                            For Each entry As ZipArchiveEntry In archive.Entries
                                                If entry.FullName = "classes.jar" Then
                                                    Dim destinationPath As String = Path.GetFullPath(Path.Combine(ProjectPath + "\libs", Path.GetFileNameWithoutExtension(keyPair.Value) + ".jar"))
                                                    If destinationPath.StartsWith(ProjectPath + "\libs", StringComparison.Ordinal) Then
                                                        entry.ExtractToFile(destinationPath)
                                                    End If
                                                End If
                                            Next entry
                                        End Using
                                    End If
                                Next
                            End If
                    End If
                End If
            End If
        End If

        ElseIf errorInfo.Contains("cannot find symbol") Then
        '...
        ElseIf errorInfo.Contains("cannot be accessed from outside package") Then
        Dim variableName = New Regex("(?<=error\:).*?(?=is not public in)").Match(arguments(0)).Value.Trim
        Dim fileContent As String = File.ReadAllText(filepath)
        fileContent = fileContent.Insert(fileContent.IndexOf("public class") - 1, vbNewLine + "import java.util.HashMap;" + vbNewLine)
        fileContent = fileContent.Replace("(" + variableName, "(HashMap." + variableName)
        File.WriteAllText(filepath, fileContent)
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim folderDlg As FolderBrowserDialog = New FolderBrowserDialog()
        folderDlg.ShowNewFolderButton = True
        If TextBox1.Text <> "" Or TextBox1.Text.Contains("\") = False Then
            folderDlg.SelectedPath = TextBox1.Text
        End If
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

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        RichTextBox1.Text = ""
        If SysEnvironment.CheckSysEnvironmentExist("GRADLE_HOME") = False Then MsgBox("The GRADLE_HOME environment has not been set", vbInformation + vbMsgBoxSetForeground, "Error") : Return
        If File.Exists(ProjectPath + "\gradlew") Then
            'Using p1 As New Process
            '    p1.StartInfo.CreateNoWindow = True
            '    p1.StartInfo.Verb = "runas"
            '    p1.StartInfo.FileName = "cmd.exe"
            '    p1.StartInfo.WorkingDirectory = ProjectPath
            '    p1.StartInfo.Arguments = String.Format(" /c gradlew build 2>>&1")
            '    Debug.Print(p1.StartInfo.Arguments)
            '    p1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
            '    p1.StartInfo.UseShellExecute = False
            '    p1.StartInfo.RedirectStandardOutput = True
            '    p1.Start()
            '    Dim output As String
            '    Using streamreader As System.IO.StreamReader = p1.StandardOutput
            '        output = streamreader.ReadToEnd()
            '        Debug.Print(output)
            '        RichTextBox1.AppendText(vbNewLine + output)
            '    End Using
            'End Using

            Using p1 As New Process
                p1.StartInfo.CreateNoWindow = True
                p1.StartInfo.Verb = "runas"
                p1.StartInfo.FileName = "cmd.exe"
                p1.StartInfo.WorkingDirectory = ProjectPath
                p1.StartInfo.Arguments = String.Format(" /c gradlew clean 2>>&1")
                Debug.Print(p1.StartInfo.Arguments)
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

            Using p1 As New Process
                p1.StartInfo.CreateNoWindow = True
                p1.StartInfo.Verb = "runas"
                p1.StartInfo.FileName = "cmd.exe"
                p1.StartInfo.WorkingDirectory = ProjectPath
                p1.StartInfo.Arguments = String.Format(" /c gradlew build 2>>&1")
                Debug.Print(p1.StartInfo.Arguments)
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
        ElseIf File.Exists(ProjectPath + "\gradlew.bat") Then
            Dim command = ProjectPath + "\gradlew.bat"
            Dim processInfo = New ProcessStartInfo("cmd.exe", "/c " + command)
            processInfo.CreateNoWindow = True
            processInfo.UseShellExecute = False
            processInfo.RedirectStandardError = True
            processInfo.RedirectStandardOutput = True
            Dim process As Process = Process.Start(processInfo)
            RichTextBox1.Invoke(New MethodInvoker(Sub() RichTextBox1.AppendText(process.StandardOutput.ReadToEnd())))
        End If
    End Sub
End Class