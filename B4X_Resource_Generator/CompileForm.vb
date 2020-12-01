Imports System.IO

Public Class CompileForm
    Private Sub CompileForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RichTextBox1.Text = ""
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
                cp = """" + androidjarPath + """;""" + B4AShared + """;""" + Core + """;" + "libs/*;" + String.Join(";", cpList).Replace(ProjectPath + "\", "").Replace("\", "/")
                'cp = """" + androidjarPath + """;""" + B4AShared + """;""" + Core + """;" + "libs/*;libs;"
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
End Class