Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions

Public Class CompileForm
    Private Sub CompileForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RichTextBox1.Text = ""
        Dim javaList = Directory.GetFiles(ProjectPath, "*.*", SearchOption.AllDirectories).Where(Function(f) New List(Of String) From {".kt", ".java"}.IndexOf(Path.GetExtension(f)) >= 0).ToArray()
        If javaList.Count > 0 Then
            For Each javaFile In javaList
                Dim fileContent As String = File.ReadAllText(javaFile)
                Using writer As New StreamWriter(javaFile, False, Encoding.GetEncoding("Windows-1252"))
                    writer.Write(fileContent)
                End Using
            Next
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        RichTextBox1.Text = ""
        Dim java As String = ""
        Dim cp As String = ""
        If Directory.Exists(ProjectPath + "\bin\classes") = False Then
            Directory.CreateDirectory(ProjectPath + "\bin\classes")
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
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        RichTextBox1.Text = ""
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
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox1.Checked = True Then
            If RichTextBox1.Text = "" Then Return
            Dim errorInfo = New Regex("(?<=error:).[\s\S]*?(?=error)").Match(RichTextBox1.Text).Value.Trim
            If errorInfo.Contains("does not exist") Then
                Dim packageName = New Regex("(?<=error: package).[\s\S]*?(?=does not exist)").Match(RichTextBox1.Text).Value.Trim

            End If
        End If
    End Sub
End Class