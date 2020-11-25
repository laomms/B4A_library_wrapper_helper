Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions

Public Class Form1
    Private WrapperList As List(Of List(Of String))
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
        Next
    End Sub

    Private Sub TextBox2_DragEnter(sender As Object, e As DragEventArgs) Handles TextBox2.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub
    Private Sub TextBox2_DragDrop(sender As Object, e As DragEventArgs) Handles TextBox2.DragDrop
        Dim files() As String = e.Data.GetData(DataFormats.FileDrop)
        For Each path In files
            TextBox2.Text = String.Empty
            TextBox2.ForeColor = Color.Black
            TextBox2.TextAlign = HorizontalAlignment.Left
            TextBox2.Text = path
        Next
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MaximizeBox = False
        Me.AllowDrop = True
        TextBox1.AllowDrop = True
        TextBox2.AllowDrop = True
        For Each assembly In System.Reflection.Assembly.GetExecutingAssembly.GetManifestResourceNames()
            If Not assembly.EndsWith(".exe") Then
                Continue For
            End If
            Using resource = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(assembly)
                If assembly.Contains("aapt") Then
                    If File.Exists(My.Computer.FileSystem.SpecialDirectories.Temp + "\aapt.exe") = False Then
                        Using file = New FileStream(My.Computer.FileSystem.SpecialDirectories.Temp + "\aapt.exe", FileMode.Create, FileAccess.Write)
                            resource.CopyTo(file)
                        End Using
                    End If
                End If
            End Using
            File.SetAttributes(My.Computer.FileSystem.SpecialDirectories.Temp + "\aapt.exe", vbArchive + vbHidden + vbSystem)
        Next
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TextBox1.Text = String.Empty
        TextBox1.ForeColor = Color.Black
        TextBox1.TextAlign = HorizontalAlignment.Left
        Using dlg As New OpenFileDialog With {.AddExtension = True,
                                              .ValidateNames = False,
                                              .CheckFileExists = False,
                                              .CheckPathExists = True,
                                              .FileName = "Folder Selection"
                                              }
            If dlg.ShowDialog = DialogResult.OK Then
                TextBox1.Text = System.IO.Path.GetDirectoryName(dlg.FileName)
            End If
        End Using
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        TextBox2.Text = String.Empty
        TextBox2.ForeColor = Color.Black
        TextBox2.TextAlign = HorizontalAlignment.Left
        Using dlg As New SaveFileDialog With {.AddExtension = True,
                                              .AutoUpgradeEnabled = True,
                                              .CreatePrompt = False,
                                              .OverwritePrompt = False,
                                              .CheckFileExists = False,
                                              .CheckPathExists = True,
                                              .FileName = "Folder selection",
                                              .Filter = "All files (*.*)|*.*",
                                              .FilterIndex = 1,
                                              .InitialDirectory = Application.StartupPath,
                                              .SupportMultiDottedExtensions = True,
                                              .Title = "Select folder",
                                              .ValidateNames = True}
            If dlg.ShowDialog = DialogResult.OK Then
                TextBox2.Text = System.IO.Path.GetDirectoryName(dlg.FileName)
            End If
        End Using
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        If TextBox1.Text = "" Then Return
        Dim directories As List(Of String) = (From x In Directory.EnumerateDirectories(TextBox1.Text, "*", SearchOption.AllDirectories) Select x.Substring(TextBox1.Text.Length)).ToList()
        Dim srcPath As String = directories.Where(Function(x) x.Contains("src") And x.Contains("res")).FirstOrDefault()
        Dim matchFiles() As String = System.IO.Directory.GetFiles(TextBox1.Text, "*AndroidManifest.xml*", SearchOption.AllDirectories)
        Dim ManifestPath As String = matchFiles.Where(Function(x) x.Contains("src")).FirstOrDefault()
        Dim javafiles() As String = Directory.GetFiles(TextBox1.Text, "*.*", SearchOption.AllDirectories).Where(Function(f) New List(Of String) From {".kt", ".java"}.IndexOf(Path.GetExtension(f)) >= 0).ToArray()
        If javafiles.Length > 0 Then
            If BackgroundWorker1.IsBusy = False Then
                Dim arguments As New List(Of Object)
                arguments.Add(javafiles)
                BackgroundWorker1.RunWorkerAsync(javafiles)
            End If
        Else
            MsgBox("No java source folder found", vbInformation + vbMsgBoxSetForeground, "Error") : Return
        End If
        'If Not srcPath Is Nothing And Not ManifestPath Is Nothing Then
        '    srcPath = TextBox1.Text + srcPath
        'Else
        '    MsgBox("No java source folder found", vbInformation + vbMsgBoxSetForeground, "Error") : Return
        'End If

    End Sub

    Private Sub GeneratorRfile(ByVal aaptPath As String, ByVal srcPath As String, ByVal ManifestPath As String, ByVal androidPath As String)
        If TextBox2.Text.Contains("\") = False Or TextBox2.Text = "" Then TextBox2.Text = Application.StartupPath
        Using p1 As New Process
            p1.StartInfo.CreateNoWindow = True
            p1.StartInfo.Verb = "runas"
            p1.StartInfo.FileName = "cmd.exe"
            p1.StartInfo.Arguments = String.Format("{0}  package -v -f -m  -S ""{1}"" -J ""{2}"" -M ""{3}"" -I ""{4}"" ", aaptPath, srcPath, TextBox2.Text, ManifestPath, androidPath)
            p1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
            p1.StartInfo.UseShellExecute = False
            p1.StartInfo.RedirectStandardOutput = True
            p1.Start()
            p1.WaitForExit()
            Dim output As String
            Using streamreader As System.IO.StreamReader = p1.StandardOutput
                output = streamreader.ReadToEnd()
                Debug.Print(output)
            End Using
        End Using
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Dim arguments() As String = TryCast(e.Argument, String())
        Dim idList As New List(Of String)
        Dim layoutList As New List(Of String)
        Dim stringList As New List(Of String)
        Dim colorList As New List(Of String)
        Dim animList As New List(Of String)
        Dim drawableList As New List(Of String)
        Dim menuList As New List(Of String)
        Dim rawList As New List(Of String)
        Dim dimenList As New List(Of String)
        For Each java As String In arguments
            Dim fileContent As String = File.ReadAllText(java)
            Dim lines = File.ReadAllLines(java).ToList()
            If fileContent.Contains("R.id.") Then
                Dim list As List(Of String) = lines.Where(Function(x) x.Contains("R.id.")).Select(Function(x) New Regex("(?<=R.id.).[\s\S]*?(?=[\p{P}\p{S}-[_]])").Match(x).Value.Trim).ToList()
                idList.AddRange(list)
            End If
            If fileContent.Contains("R.layout.") Then
                Dim list As List(Of String) = lines.Where(Function(x) x.Contains("R.layout.")).Select(Function(x) New Regex("(?<=R.layout.).[\s\S]*?(?=[\p{P}\p{S}-[_]])").Match(x).Value.Trim).ToList()
                layoutList.AddRange(list)
            End If
            If fileContent.Contains("R.string.") Then
                Dim list As List(Of String) = lines.Where(Function(x) x.Contains("R.string.")).Select(Function(x) New Regex("(?<=R.string.).[\s\S]*?(?=[\p{P}\p{S}-[_]])").Match(x).Value.Trim).ToList()
                stringList.AddRange(list)
            End If
            If fileContent.Contains("R.color.") Then
                Dim list As List(Of String) = lines.Where(Function(x) x.Contains("R.color.")).Select(Function(x) New Regex("(?<=R.color.).[\s\S]*?(?=[\p{P}\p{S}-[_]])").Match(x).Value.Trim).ToList()
                colorList.AddRange(list)
            End If
            If fileContent.Contains("R.anim.") Then
                Dim list As List(Of String) = lines.Where(Function(x) x.Contains("R.anim.")).Select(Function(x) New Regex("(?<=R.anim.).[\s\S]*?(?=[\p{P}\p{S}-[_]])").Match(x).Value.Trim).ToList()
                animList.AddRange(list)
            End If
            If fileContent.Contains("R.drawable.") Then
                Dim list As List(Of String) = lines.Where(Function(x) x.Contains("R.drawable.")).Select(Function(x) New Regex("(?<=R.drawable.).[\s\S]*?(?=[\p{P}\p{S}-[_]])").Match(x).Value.Trim).ToList()
                drawableList.AddRange(list)
            End If
            If fileContent.Contains("R.menu.") Then
                Dim list As List(Of String) = lines.Where(Function(x) x.Contains("R.menu.")).Select(Function(x) New Regex("(?<=R.menu.).[\s\S]*?(?=[\p{P}\p{S}-[_]])").Match(x).Value.Trim).ToList()
                menuList.AddRange(list)
            End If
            If fileContent.Contains("R.raw.") Then
                Dim list As List(Of String) = lines.Where(Function(x) x.Contains("R.raw.")).Select(Function(x) New Regex("(?<=R.raw.).[\s\S]*?(?=[\p{P}\p{S}-[_]])").Match(x).Value.Trim).ToList()
                rawList.AddRange(list)
            End If
            If fileContent.Contains("R.dimen.") Then
                Dim list As List(Of String) = lines.Where(Function(x) x.Contains("R.dimen.")).Select(Function(x) New Regex("(?<=R.dimen.).[\s\S]*?(?=[\p{P}\p{S}-[_]])").Match(x).Value.Trim).ToList()
                dimenList.AddRange(list)
            End If
        Next
        idList = idList.Distinct().ToList()
        layoutList = layoutList.Distinct().ToList()
        stringList = stringList.Distinct().ToList()
        colorList = colorList.Distinct().ToList()
        animList = animList.Distinct().ToList()
        drawableList = drawableList.Distinct().ToList()
        menuList = menuList.Distinct().ToList()
        rawList = rawList.Distinct().ToList()
        dimenList = dimenList.Distinct().ToList()
    End Sub
End Class
