Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions

Public Class Form1
    Private WrapperList As List(Of List(Of String))
    Private packageName As String = String.Empty
    Private minSdkVersion As String
    Private targetSdkVersion As String
    Private mainActivity As String
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
        'For Each assembly In System.Reflection.Assembly.GetExecutingAssembly.GetManifestResourceNames()
        '    If Not assembly.EndsWith(".exe") Then
        '        Continue For
        '    End If
        '    Using resource = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(assembly)
        '        If assembly.Contains("aapt") Then
        '            If File.Exists(My.Computer.FileSystem.SpecialDirectories.Temp + "\aapt.exe") = False Then
        '                Using file = New FileStream(My.Computer.FileSystem.SpecialDirectories.Temp + "\aapt.exe", FileMode.Create, FileAccess.Write)
        '                    resource.CopyTo(file)
        '                End Using
        '            End If
        '        ElseIf assembly.Contains("unzip") Then
        '            If File.Exists(My.Computer.FileSystem.SpecialDirectories.Temp + "\unzip.exe") = False Then
        '                Using file = New FileStream(My.Computer.FileSystem.SpecialDirectories.Temp + "\unzip.exe", FileMode.Create, FileAccess.Write)
        '                    resource.CopyTo(file)
        '                End Using
        '            End If
        '        End If
        '    End Using
        '    File.SetAttributes(My.Computer.FileSystem.SpecialDirectories.Temp + "\aapt.exe", vbArchive + vbHidden + vbSystem)
        '    File.SetAttributes(My.Computer.FileSystem.SpecialDirectories.Temp + "\unzip.exe", vbArchive + vbHidden + vbSystem)
        'Next
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

        If TextBox1.Text = "" Or TextBox1.Text.Contains("\") = False Then Return
        If TextBox2.Text = "" Or TextBox2.Text.Contains("\") = False Then
            TextBox2.Text = String.Empty
            TextBox2.ForeColor = Color.Black
            TextBox2.TextAlign = HorizontalAlignment.Left
            TextBox2.Text = Application.StartupPath
        End If
        ComboBox1.Items.Clear()
        ComboBox1.Text = ""
        Dim directories As List(Of String) = (From x In Directory.EnumerateDirectories(TextBox1.Text, "*", SearchOption.AllDirectories) Select x.Substring(TextBox1.Text.Length)).ToList()
        Dim srcPath As String = directories.Where(Function(x) x.Contains("src") And x.Contains("res")).FirstOrDefault()
        Dim buildFiles() As String = System.IO.Directory.GetFiles(TextBox1.Text, "*build.gradle*", SearchOption.AllDirectories)
        Dim dependenciesList As New List(Of String)
        If buildFiles.Count > 0 Then
            For Each buildFile In buildFiles
                Dim fileContent As String = File.ReadAllText(buildFile)
                Dim lines = File.ReadAllLines(buildFile).ToList()
                Dim list As List(Of String) = lines.Where(Function(x) x.Contains("minSdkVersion")).Select(Function(x) x.Trim.Replace("minSdkVersion", "").Trim).ToList()
                If list.Count > 0 Then minSdkVersion = list(0)
                list = lines.Where(Function(x) x.Contains("targetSdkVersion")).Select(Function(x) x.Trim.Replace("targetSdkVersion", "").Trim).ToList()
                If list.Count > 0 Then targetSdkVersion = list(0)
                Dim matches As MatchCollection = Regex.Matches(fileContent, "dependencies.[\s\S]*?}", RegexOptions.Multiline Or RegexOptions.IgnoreCase)
                For Each match As Match In matches
                    For Each line In match.Value.Split({vbCrLf, vbLf, vbCr}, StringSplitOptions.RemoveEmptyEntries)
                        If line.Contains("implementation") Then
                            dependenciesList.Add(New Regex("'([^']*)'").Match(line).Value.Trim.TrimStart("'").TrimEnd("'"))
                            ComboBox1.Invoke(New MethodInvoker(Sub() ComboBox1.Items.Add(New Regex("'([^']*)'").Match(line).Value.Trim.TrimStart("'").TrimEnd("'"))))
                        ElseIf line.Contains("compile") Then
                            dependenciesList.Add(New Regex("'([^']*)'").Match(line).Value.Trim.TrimStart("'").TrimEnd("'"))
                            ComboBox1.Invoke(New MethodInvoker(Sub() ComboBox1.Items.Add(New Regex("'([^']*)'").Match(line).Value.Trim.TrimStart("'").TrimEnd("'"))))
                        End If
                    Next
                Next
            Next
        End If
        Dim matchFiles() As String = System.IO.Directory.GetFiles(TextBox1.Text, "*AndroidManifest.xml*", SearchOption.AllDirectories)
        If matchFiles.Count > 0 Then
            Dim ManifestPath As String = matchFiles.Where(Function(x) x.Contains("src")).FirstOrDefault()           
            Dim fileContent As String = File.ReadAllText(ManifestPath)
            Dim lines = File.ReadAllLines(ManifestPath).ToList()
            Dim list As List(Of String) = lines.Where(Function(x) x.Contains("package")).Select(Function(x) New Regex("(?<=package=\"").[\s\S]*?(?=[\p{P}\p{S}-[._]])").Match(x).Value.Trim).ToList()
            If list.Count > 0 Then
                packageName = list(0)
            End If
             If BackgroundWorker2.IsBusy = False Then
                Dim arguments As New List(Of String)
                arguments.Add(ManifestPath)
                BackgroundWorker2.RunWorkerAsync(arguments)
            End If
        End If
        Dim javafiles() As String = Directory.GetFiles(TextBox1.Text, "*.*", SearchOption.AllDirectories).Where(Function(f) New List(Of String) From {".kt", ".java"}.IndexOf(Path.GetExtension(f)) >= 0).ToArray()
        If javafiles.Length > 0 Then
            If BackgroundWorker1.IsBusy = False Then
                Dim arguments As New List(Of Object)
                arguments.Add(javafiles)
                BackgroundWorker1.RunWorkerAsync(arguments)
            End If
        Else
            MsgBox("No java source folder found", vbInformation + vbMsgBoxSetForeground, "Error") : Return
        End If

    End Sub

    Private Sub GeneratorRfile(ByVal aaptPath As String, ByVal savePath As String, ByVal srcPath As String, ByVal ManifestPath As String, ByVal androidPath As String)
        Using p1 As New Process
            p1.StartInfo.CreateNoWindow = True
            p1.StartInfo.Verb = "runas"
            p1.StartInfo.FileName = "cmd.exe"
            p1.StartInfo.Arguments = String.Format("{0}  package -v -f -m  -S ""{1}"" -J ""{2}"" -M ""{3}"" -I ""{4}"" ", aaptPath, srcPath, savePath, ManifestPath, androidPath)
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

    Private Sub BackgroundWorker2_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker2.DoWork
        Dim arguments As List(Of String) = TryCast(e.Argument, List(Of String))
        Dim PermissionList As New List(Of String)
        Dim fileContent As String = File.ReadAllText(arguments(0))
        Dim lines = File.ReadAllLines(arguments(0)).ToList()
        If fileContent.Contains("uses-permission") Then
            Dim list As List(Of String) = lines.Where(Function(x) x.Contains("uses-permission")).Select(Function(x) New Regex("(?<=android:name=\"").[\s\S]*?(?=[\p{P}\p{S}-[._]])").Match(x).Value.Trim).ToList()
            PermissionList.AddRange(list)
        End If
        Dim supports_screens As String = New Regex("<supports-screens[\s\S]*?/>").Match(fileContent).Value.Trim
        Dim application As String = New Regex("<application[\s\S]*?>").Match(fileContent).Value.Trim
        Dim applicationList As New List(Of String)
        For Each line As String In application.Split({vbCrLf, vbLf, vbCr}, StringSplitOptions.RemoveEmptyEntries)
            If line.Contains("android:icon") Then
                applicationList.Add("SetApplicationAttribute(android:icon, ""@drawable/icon"")  'add your own app icon")
            ElseIf line.Contains("android:label") Then
                applicationList.Add("SetApplicationAttribute(android:label, ""$LABEL$"")  'add your own app label name")
            ElseIf line.Contains("android:theme") Then
                applicationList.Add("CreateResourceFromFile(Macro, Themes.DarkTheme)  'SetApplicationAttribute(android:theme,""@style/AppTheme""")
            ElseIf line.Contains("android:") Then
                applicationList.Add("SetApplicationAttribute(" + line.Replace("=", ",").Trim + ")")
            End If
        Next
        Dim activityList As New List(Of String)
        Dim matches As MatchCollection = Regex.Matches(fileContent, "<activity.[\s\S]*?</activity>", RegexOptions.Multiline Or RegexOptions.IgnoreCase)
        For Each match As Match In matches
            If match.Value.Contains("intent-filter") Then
                mainActivity = New Regex("(?<=activity android:name=\"").*?(?=[\p{P}\p{S}-[._]])").Match(match.Value).Value.Trim.TrimStart(".")
                ComboBox1.Invoke(New MethodInvoker(Sub() ComboBox1.Text = "mainActivity: " + mainActivity))
                activityList.Add("        " + Regex.Replace(match.Value, "<intent-filter[\s\S]*?intent-filter>", "").ToString.Replace(""".", """" + packageName + "."))
            Else
                activityList.Add("        " + match.Value.ToString.Replace(""".", """" + packageName + "."))
            End If
        Next
        Dim serviceList As New List(Of String)
        matches = Regex.Matches(fileContent, "<service[\s\S]*?\/service>", RegexOptions.IgnoreCase)
        For Each match As Match In matches
            If match.Value.Contains("android:resource") = False Then
                Dim serviceItem As String = Regex.Replace(match.Value, "android:icon=.*?(?=\n|\r|\r\n)", "")
                serviceItem = Regex.Replace(serviceItem, "android:label=.*?(?=\n|\r|\r\n)", "")
                serviceList.Add("        " + serviceItem.Replace(""".", """" + packageName + "."))
            End If
        Next

        Dim receiverList As New List(Of String)
        matches = Regex.Matches(fileContent, "<receiver[\s\S]*?<\/receiver>", RegexOptions.IgnoreCase)
        For Each match As Match In matches
            If match.Value.Contains("android:resource") = False Then
                Dim receiverItem As String = Regex.Replace(match.Value, "android:icon=.*?(?=\n|\r|\r\n)", "")
                receiverItem = Regex.Replace(receiverItem, "android:label=.*?(?=\n|\r|\r\n)", "")
                receiverList.Add("        " + receiverItem.Replace(""".", """" + packageName + "."))
            End If
        Next

        Dim metadataList As New List(Of String)
        matches = Regex.Matches(fileContent, "<meta-data[\s\S]*?\/>", RegexOptions.IgnoreCase)
        For Each match As Match In matches
            If match.Value.Contains("android:resource") = False Then metadataList.Add("        " + match.Value.ToString.Replace(""".", """" + packageName + "."))
        Next

        Dim filePath As String = TextBox2.Text + "\B4X_Manifest.txt"
        Using outputFile As New StreamWriter(filePath, False, Encoding.UTF8)
            outputFile.WriteLine("AddManifestText(")
            If minSdkVersion <> "" Then outputFile.WriteLine("<uses-sdk android:minSdkVersion=""" + minSdkVersion + """ android:targetSdkVersion=""" + targetSdkVersion + """/>") Else outputFile.WriteLine("<uses-sdk android:minSdkVersion=""19"" android:targetSdkVersion=""28""/>")
            outputFile.WriteLine(supports_screens)
            outputFile.WriteLine(")")
            If applicationList.Count > 0 Then outputFile.WriteLine(String.Join(vbNewLine, applicationList))
            For Each item In PermissionList
                outputFile.WriteLine("AddPermission(" + item + ")")
            Next
            outputFile.WriteLine(vbNewLine + "AddApplicationText(")
            If activityList.Count > 0 Then outputFile.WriteLine(String.Join(vbNewLine, activityList))
            If serviceList.Count > 0 Then outputFile.WriteLine(String.Join(vbNewLine, serviceList))
            If receiverList.Count > 0 Then outputFile.WriteLine(String.Join(vbNewLine, receiverList))
            If metadataList.Count > 0 Then outputFile.WriteLine(String.Join(vbNewLine, metadataList))
            outputFile.WriteLine(")")
        End Using

    End Sub
    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Dim arguments As List(Of Object) = TryCast(e.Argument, List(Of Object))
        Dim styleableList As New List(Of String)
        Dim styleableListArray As New List(Of String)
        Dim sourceList As New List(Of String)
        For Each java As String In arguments(0)
            Dim fileContent As String = File.ReadAllText(java)
            If fileContent.Contains("R.") Then
                If fileContent.Contains("obtainStyledAttributes") And fileContent.Contains("R.styleable.") Then
                    Dim lines = File.ReadAllLines(java).ToList()
                    Dim list As List(Of String) = lines.Where(Function(x) x.Contains("obtainStyledAttributes") And x.Contains("R.styleable.")).Select(Function(x) New Regex("(?<=R.styleable.).[\s\S]*?(?=[\p{P}\p{S}-[_]])").Match(x).Value.Trim).ToList()
                    styleableListArray.AddRange(list)
                    list = Regex.Matches(fileContent, "(?<=R.styleable.).[\s\S]*?(?=[\p{P}\p{S}-[_]])").Cast(Of Match)().Select(Function(m) m.Value).ToList()
                    styleableList.AddRange(list)
                ElseIf fileContent.Contains("R.styleable.") Then
                    Dim list As List(Of String) = Regex.Matches(fileContent, "(?<=R.styleable.).[\s\S]*?(?=[\p{P}\p{S}-[_]])").Cast(Of Match)().Select(Function(m) m.Value).ToList()
                    styleableList.AddRange(list)
                Else
                    Dim list As List(Of String) = Regex.Matches(fileContent, "(?<=R\.).[\s\S]*?(?=[\p{P}\p{S}-[._]])").Cast(Of Match)().Select(Function(m) m.Value).ToList()
                    list = list.Where(Function(x) x.Contains(".")).ToList()
                    sourceList.AddRange(list)
                End If
            End If
        Next
        sourceList.Sort()
        sourceList = sourceList.Distinct().ToList()
        Debug.Print(String.Join(vbNewLine, sourceList))
        styleableList = styleableList.Distinct().ToList()
        styleableListArray = styleableListArray.Distinct().ToList()
        If styleableListArray.Count > 0 Then styleableList = styleableList.Except(styleableListArray).Concat(styleableListArray.Except(styleableList)).ToList()

        Dim filePath As String = TextBox2.Text + "\R.java"
        Dim utf8WithoutBom As Encoding = Encoding.GetEncoding("ISO-8859-1")
        Using outputFile As New StreamWriter(filePath, False, utf8WithoutBom)
            outputFile.WriteLine("package " + packageName + ";")
            outputFile.WriteLine("import anywheresoftware.b4a.BA;")
            outputFile.WriteLine(vbNewLine + "public class R {")
            If sourceList.Count > 0 Then
                Dim className As String = ""
                For Each item In sourceList
                    If item.Split(".")(0) <> className Then
                        If className <> "" Then outputFile.WriteLine("	}")
                        className = item.Split(".")(0)
                        outputFile.WriteLine("	public static final class " + item.Split(".")(0) + " {")
                        outputFile.WriteLine("		public static int " + item.Split(".")(1) + " = BA.applicationContext.getResources().getIdentifier(""" + item.Split(".")(1) + """, ""styleable"", BA.packageName);")
                    Else
                        outputFile.WriteLine("		public static int " + item.Split(".")(1) + " = BA.applicationContext.getResources().getIdentifier(""" + item.Split(".")(1) + """, ""styleable"", BA.packageName);")
                    End If
                Next
                outputFile.WriteLine("	}")
            End If
            If styleableListArray.Count > 0 Then
                outputFile.WriteLine("	public static final class styleable {")
                If styleableList.Count > 0 Then
                    For Each item In styleableList
                        outputFile.WriteLine("		public static int " + item + " = BA.applicationContext.getResources().getIdentifier(""" + item + """, ""styleable"", BA.packageName);")
                    Next
                End If
                For Each items In styleableListArray
                    If styleableList.Count > 0 Then outputFile.WriteLine("		public static int[] " + items + " = {" + String.Join(",", styleableList) + "};")
                Next
                outputFile.WriteLine("	}")
            ElseIf styleableList.Count > 0 Then
                For Each item In styleableList
                    outputFile.WriteLine("		public static int " + item + " = BA.applicationContext.getResources().getIdentifier(""" + item + """, ""styleable"", BA.packageName);")
                Next
            End If
            outputFile.WriteLine("}")
        End Using
    End Sub
End Class
