Imports System.IO
Imports System.IO.Compression
Imports System.Net
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Web
Imports System.Xml

Public Class Form1
    Private WrapperList As List(Of List(Of String))
    Private packageName As String = String.Empty
    Private minSdkVersion As String
    Private targetSdkVersion As String
    Private ProjectPath As String
    Private RPath As String
    Private srcPath As String
    Private javaPath As String
    Private mycookiecontainer As CookieContainer = New CookieContainer()

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
    Private Sub TextBox3_DragEnter(sender As Object, e As DragEventArgs) Handles TextBox3.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub
    Private Sub TextBox3_DragDrop(sender As Object, e As DragEventArgs) Handles TextBox3.DragDrop
        Dim files() As String = e.Data.GetData(DataFormats.FileDrop)
        For Each path In files
            TextBox3.Text = String.Empty
            TextBox3.ForeColor = Color.Black
            TextBox3.TextAlign = HorizontalAlignment.Left
            TextBox3.Text = path
            If TextBox3.Text.Contains("\") And TextBox3.Text <> "" Then
                If File.Exists(TextBox3.Text + "\R.txt") = False Or File.Exists(TextBox3.Text + "\AndroidManifest.xml") = False Then Return
                If Directory.Exists(TextBox3.Text + "\jars") = False Or Directory.Exists(TextBox3.Text + "\res") = False Then Return
                Dim libname = IO.Path.GetFileName(TextBox3.Text) + ".aar"
                PackAAR(My.Computer.FileSystem.SpecialDirectories.Temp + "\jar.exe", Application.StartupPath + "\" + libname, TextBox3.Text)
            End If
        Next
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MaximizeBox = False
        Me.AllowDrop = True
        TextBox1.AllowDrop = True
        TextBox2.AllowDrop = True
        TextBox3.AllowDrop = True
        For Each assembly In System.Reflection.Assembly.GetExecutingAssembly.GetManifestResourceNames()
            If Not assembly.EndsWith(".exe") And Not assembly.EndsWith(".dll") Then
                Continue For
            End If
            Using resource = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(assembly)
                Dim resourcefile = assembly.Replace(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name + ".", "")
                If File.Exists(My.Computer.FileSystem.SpecialDirectories.Temp + "\" + resourcefile) = False Then
                    Using file = New FileStream(My.Computer.FileSystem.SpecialDirectories.Temp + "\" + resourcefile, FileMode.Create, FileAccess.Write)
                        resource.CopyTo(file)
                    End Using
                    File.SetAttributes(My.Computer.FileSystem.SpecialDirectories.Temp + "\" + resourcefile, vbArchive + vbHidden + vbSystem)
                End If
            End Using
        Next
        If SysEnvironment.CheckSysEnvironmentExist("JAVA_HOME") Then CheckBox_JAVA_HOME.Checked = True : CheckBox_JAVA_HOME.Enabled = False
        If SysEnvironment.CheckSysEnvironmentExist("ANDROID_SDK_HOME") Then CheckBox_ANDROID_SDK_HOME.Checked = True : CheckBox_ANDROID_SDK_HOME.Enabled = False
        If SysEnvironment.CheckSysEnvironmentExist("MAVEN_HOME") Then CheckBox_MAVEN_HOME.Checked = True : CheckBox_MAVEN_HOME.Enabled = False
        If SysEnvironment.CheckSysEnvironmentExist("CLASSPATH") Then CheckBox_CLASSPATH.Checked = True : CheckBox_CLASSPATH.Enabled = False

        With ListView1
            .Items.Clear()
            .GridLines = True
            .View = View.Details
            .FullRowSelect = True
            .Columns.Add("id", 0, HorizontalAlignment.Center)
            .Columns.Add("view type", 120, HorizontalAlignment.Center)
            .Columns.Add("view name", 150, HorizontalAlignment.Center)
            .Columns.Add("other", ListView1.Width - 120 - 150 - 5, HorizontalAlignment.Center)
        End With

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
                                              .Filter = "All files (*.*) Then|*.*",
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
        ComboBox3.Items.Clear()
        RichTextBox1.Text = ""
        ListView1.Items.Clear()

        Try
            If ComboBox2.Items.Count > 0 Then ComboBox2.Items.Clear()
        Catch ex As Exception

        End Try
        ComboBox2.Text = ""
        Try
            If Directory.GetFiles(TextBox1.Text, "*.*", SearchOption.AllDirectories).Where(Function(f) New List(Of String) From {".java"}.IndexOf(Path.GetExtension(f)) >= 0).ToArray().Count = 0 Then Return
            If Directory.GetFiles(TextBox1.Text, "*.*", SearchOption.AllDirectories).Where(Function(f) New List(Of String) From {".xml"}.IndexOf(Path.GetExtension(f)) >= 0).ToArray().Count = 0 Then Return
            Dim directories As List(Of String) = (From x In Directory.EnumerateDirectories(TextBox1.Text, "*", SearchOption.AllDirectories) Select x.Substring(TextBox1.Text.Length)).ToList()
            Dim srcPath As String = directories.Where(Function(x) x.Contains("src") And x.Contains("res")).FirstOrDefault()
            Dim buildFiles() As String = System.IO.Directory.GetFiles(TextBox1.Text, "*build.gradle*", SearchOption.AllDirectories)
            Dim buildFilePath As String = ""
            Dim dependenciesList As New List(Of String)
            If buildFiles.Count > 0 Then
                For Each buildFile In buildFiles
                    Dim fileContent As String = File.ReadAllText(buildFile)
                    Dim lines = File.ReadAllLines(buildFile).ToList()
                    Dim list As List(Of String) = lines.Where(Function(x) x.Contains("minSdkVersion")).Select(Function(x) x.Trim.Replace("minSdkVersion", "").Trim).ToList()
                    If list.Count > 0 Then
                        If fileContent.Contains("com.android.library") Then
                            buildFilePath = buildFile
                            ComboBox1.Items.Clear()
                            ComboBox1.Text = ""
                            Try
                                If ComboBox2.Items.Count > 0 Then ComboBox2.Items.Clear()
                            Catch ex As Exception

                            End Try
                            ComboBox2.Text = ""
                            dependenciesList.Clear()
                            minSdkVersion = list(0)
                            list = lines.Where(Function(x) x.Contains("targetSdkVersion")).Select(Function(x) x.Trim.Replace("targetSdkVersion", "").Trim).ToList()
                            If list.Count > 0 Then targetSdkVersion = list(0)
                            Dim matches As MatchCollection = Regex.Matches(fileContent, "dependencies.[\s\S]*?}", RegexOptions.Multiline Or RegexOptions.IgnoreCase)
                            For Each match As Match In matches
                                For Each line In match.Value.Split({vbCrLf, vbLf, vbCr}, StringSplitOptions.RemoveEmptyEntries)
                                    If line.Contains("implementation") And line.Contains("*.jar") = False And line.Contains("libs/") = False Then
                                        dependenciesList.Add(New Regex("'([^']*)'").Match(line).Value.Trim.TrimStart("'").TrimEnd("'"))
                                        ComboBox1.Invoke(New MethodInvoker(Sub() ComboBox1.Items.Add(New Regex("'([^']*)'").Match(line).Value.Trim.TrimStart("'").TrimEnd("'"))))
                                    ElseIf line.Contains("compile") And line.Contains("*.jar") = False And line.Contains("libs/") = False Then
                                        dependenciesList.Add(New Regex("'([^']*)'").Match(line).Value.Trim.TrimStart("'").TrimEnd("'"))
                                        ComboBox1.Invoke(New MethodInvoker(Sub() ComboBox1.Items.Add(New Regex("'([^']*)'").Match(line).Value.Trim.TrimStart("'").TrimEnd("'"))))
                                    ElseIf line.Contains("androidTestImplementation") And line.Contains("*.jar") = False And line.Contains("libs/") = False Then
                                        dependenciesList.Add(New Regex("'([^']*)'").Match(line).Value.Trim.TrimStart("'").TrimEnd("'"))
                                        ComboBox1.Invoke(New MethodInvoker(Sub() ComboBox1.Items.Add(New Regex("'([^']*)'").Match(line).Value.Trim.TrimStart("'").TrimEnd("'"))))
                                    ElseIf line.Contains("testImplementation") And line.Contains("*.jar") = False And line.Contains("libs/") = False Then
                                        dependenciesList.Add(New Regex("'([^']*)'").Match(line).Value.Trim.TrimStart("'").TrimEnd("'"))
                                        ComboBox1.Invoke(New MethodInvoker(Sub() ComboBox1.Items.Add(New Regex("'([^']*)'").Match(line).Value.Trim.TrimStart("'").TrimEnd("'"))))
                                    ElseIf line.Contains("implementation") And line.Contains("*.jar") = False And line.Contains("libs/") Then
                                        dependenciesList.Add(New Regex("'([^']*)'").Match(line).Value.Trim.TrimStart("'").TrimEnd("'"))
                                        ComboBox1.Invoke(New MethodInvoker(Sub() ComboBox1.Items.Add(New Regex("'([^']*)'").Match(line).Value.Trim.TrimStart("'").TrimEnd("'").Replace("libs/", ""))))
                                    End If
                                Next
                            Next
                            Exit For
                        ElseIf fileContent.Contains("com.android.application") Then
                            buildFilePath = buildFile
                            minSdkVersion = list(0)
                            list = lines.Where(Function(x) x.Contains("targetSdkVersion")).Select(Function(x) x.Trim.Replace("targetSdkVersion", "").Trim).ToList()
                            If list.Count > 0 Then targetSdkVersion = list(0)
                            Dim matches As MatchCollection = Regex.Matches(fileContent, "dependencies.[\s\S]*?}", RegexOptions.Multiline Or RegexOptions.IgnoreCase)
                            For Each match As Match In matches
                                For Each line In match.Value.Split({vbCrLf, vbLf, vbCr}, StringSplitOptions.RemoveEmptyEntries)
                                    If line.Contains("implementation") And line.Contains("*.jar") = False And line.Contains("libs/") = False Then
                                        dependenciesList.Add(New Regex("'([^']*)'").Match(line).Value.Trim.TrimStart("'").TrimEnd("'"))
                                        ComboBox1.Invoke(New MethodInvoker(Sub() ComboBox1.Items.Add(New Regex("'([^']*)'").Match(line).Value.Trim.TrimStart("'").TrimEnd("'"))))
                                    ElseIf line.Contains("compile") And line.Contains("*.jar") = False And line.Contains("libs/") = False Then
                                        dependenciesList.Add(New Regex("'([^']*)'").Match(line).Value.Trim.TrimStart("'").TrimEnd("'"))
                                        ComboBox1.Invoke(New MethodInvoker(Sub() ComboBox1.Items.Add(New Regex("'([^']*)'").Match(line).Value.Trim.TrimStart("'").TrimEnd("'"))))
                                    ElseIf line.Contains("androidTestImplementation") And line.Contains("*.jar") = False And line.Contains("libs/") = False Then
                                        dependenciesList.Add(New Regex("'([^']*)'").Match(line).Value.Trim.TrimStart("'").TrimEnd("'"))
                                        ComboBox1.Invoke(New MethodInvoker(Sub() ComboBox1.Items.Add(New Regex("'([^']*)'").Match(line).Value.Trim.TrimStart("'").TrimEnd("'"))))
                                    ElseIf line.Contains("testImplementation") And line.Contains("*.jar") = False And line.Contains("libs/") = False Then
                                        dependenciesList.Add(New Regex("'([^']*)'").Match(line).Value.Trim.TrimStart("'").TrimEnd("'"))
                                        ComboBox1.Invoke(New MethodInvoker(Sub() ComboBox1.Items.Add(New Regex("'([^']*)'").Match(line).Value.Trim.TrimStart("'").TrimEnd("'"))))
                                    ElseIf line.Contains("implementation") And line.Contains("*.jar") = False And line.Contains("libs/") Then
                                        dependenciesList.Add(New Regex("'([^']*)'").Match(line).Value.Trim.TrimStart("'").TrimEnd("'"))
                                        ComboBox1.Invoke(New MethodInvoker(Sub() ComboBox1.Items.Add(New Regex("'([^']*)'").Match(line).Value.Trim.TrimStart("'").TrimEnd("'").Replace("libs/", ""))))
                                    End If
                                Next
                            Next
                        End If
                    End If
                Next
            End If
            If ComboBox1.Items.Count > 0 Then ComboBox1.Invoke(New MethodInvoker(Sub() ComboBox1.SelectedIndex = 0))
            Dim matchFiles() As String
            If buildFilePath <> "" Then
                matchFiles = Directory.GetFiles(Path.GetDirectoryName(buildFilePath), "*AndroidManifest.xml*", SearchOption.AllDirectories)
            Else
                matchFiles = Directory.GetFiles(TextBox1.Text, "*AndroidManifest.xml*", SearchOption.AllDirectories)
            End If
            If matchFiles.Count > 0 Then
                Dim ManifestPath As String = matchFiles.Where(Function(x) x.Contains("src")).FirstOrDefault()
                If Not ManifestPath Is Nothing Then
                    Dim lines = File.ReadAllLines(ManifestPath).ToList()
                    Dim list As List(Of String) = lines.Where(Function(x) x.Contains("package")).Select(Function(x) New Regex("(?<=package=\"").[\s\S]*?(?=[\p{P}\p{S}-[._]])").Match(x).Value.Trim).ToList()
                    If list.Count > 0 Then
                        packageName = list(0)
                        If packageName.Contains(".") Then
                            Dim projectName As String = packageName.Split(".")(packageName.Split(".").Count - 1)
                            ProjectPath = TextBox2.Text + "\" + projectName + "lib"
                        Else
                            Dim projectName As String = packageName
                            ProjectPath = TextBox2.Text + "\" + projectName + "lib"
                        End If
                        If Not Directory.Exists(ProjectPath) Then
                            Directory.CreateDirectory(ProjectPath)
                            'Else
                            '    For Each subDirectory In New DirectoryInfo(ProjectPath).GetDirectories
                            '        subDirectory.Delete(True)
                            '    Next subDirectory
                            '    Directory.Delete(ProjectPath)
                            '    Directory.CreateDirectory(ProjectPath)
                        End If
                    End If
                    If ManifestPath.Contains("src") Then
                        srcPath = ManifestPath.Substring(0, ManifestPath.IndexOf("src") + 3)
                        FileIO.FileSystem.CopyDirectory(srcPath, ProjectPath + "\src", True)
                        If Directory.Exists(srcPath.Replace("src", "libs")) Then
                            FileIO.FileSystem.CopyDirectory(srcPath.Replace("src", "libs"), ProjectPath + "\libs", True)
                        Else
                            Directory.CreateDirectory(ProjectPath + "\libs")
                        End If
                        'Button7.Invoke(New MethodInvoker(Sub() Button7.Enabled = True))
                    End If
                    Dim newthread As New Thread(Sub()
                                                    extractResource(ManifestPath)
                                                End Sub)
                    newthread.Start()
                    javaPath = Path.GetDirectoryName(ManifestPath) + "\java\" + packageName.Replace(".", "\")
                    RPath = javaPath.Replace(javaPath.Substring(0, javaPath.IndexOf("src")), ProjectPath + "\")
                    If BackgroundWorker2.IsBusy = False Then
                        Dim arguments As New List(Of String)
                        arguments.Add(ManifestPath)
                        BackgroundWorker2.RunWorkerAsync(arguments)
                    End If

                End If
            End If
            Dim javafiles() As String
            If buildFilePath <> "" Then
                javafiles = Directory.GetFiles(Path.GetDirectoryName(buildFilePath), "*.*", SearchOption.AllDirectories).Where(Function(f) New List(Of String) From {".kt", ".java"}.IndexOf(Path.GetExtension(f)) >= 0).ToArray()
            Else
                javafiles = Directory.GetFiles(TextBox1.Text, "*.*", SearchOption.AllDirectories).Where(Function(f) New List(Of String) From {".kt", ".java"}.IndexOf(Path.GetExtension(f)) >= 0).ToArray()
            End If
            If javafiles.Length > 0 Then
                If BackgroundWorker1.IsBusy = False Then
                    Dim arguments As New List(Of Object)
                    arguments.Add(javafiles)
                    BackgroundWorker1.RunWorkerAsync(arguments)
                End If
            Else
                MsgBox("No java source folder found", vbInformation + vbMsgBoxSetForeground, "Error") : Return
            End If
        Catch ex As Exception

        End Try



    End Sub

    Private Sub BackgroundWorker2_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker2.DoWork
        Dim arguments As List(Of String) = TryCast(e.Argument, List(Of String))
        Dim ManifestPath As String = arguments(0)
        Dim PermissionList As New List(Of String)
        Dim fileContent As String = File.ReadAllText(ManifestPath)
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
        Dim mainActivity As String = ""
        Dim matches As MatchCollection = Regex.Matches(fileContent, "<activity[\s\S]*?activity>", RegexOptions.Multiline Or RegexOptions.IgnoreCase)
        For Each match As Match In matches
            If match.Value.Contains("intent-filter") Then
                mainActivity = New Regex("(?<=android:name=\"").*?(?=[\p{P}\p{S}-[._]])").Match(match.Value).Value.Trim.TrimStart(".")
                activityList.Add("        " + Regex.Replace(match.Value, "<intent-filter[\s\S]*?intent-filter>", "").ToString.Replace(""".", """" + packageName + "."))
            Else
                If mainActivity = "" Then mainActivity = New Regex("(?<=android:name=\"").*?(?=[\p{P}\p{S}-[._]])").Match(match.Value).Value.Trim.TrimStart(".")
                activityList.Add("        " + match.Value.ToString.Replace(""".", """" + packageName + "."))
            End If
        Next
        javaPath = Path.GetDirectoryName(ManifestPath) + "\java\" + packageName.Replace(".", "\")
        Dim javafiles = Directory.GetFiles(javaPath, "*.*", SearchOption.AllDirectories).Where(Function(f) New List(Of String) From {".java"}.IndexOf(Path.GetExtension(f)) >= 0).ToArray()
        If javafiles.Count > 0 Then
            Dim bs As New BindingSource()
            bs.DataSource = javafiles
            ComboBox2.Invoke(New MethodInvoker(Sub() ComboBox2.DataSource = bs))
            If mainActivity <> "" Then
                mainActivity = mainActivity.Replace(".", "\")
                ComboBox2.Invoke(New MethodInvoker(Sub() ComboBox2.Text = javaPath + "\" + mainActivity + ".java"))
            Else
                If javafiles.Count = 1 Then
                    ComboBox2.Invoke(New MethodInvoker(Sub() ComboBox2.Text = javafiles(0)))
                End If
            End If
        End If

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
        'Debug.Print(String.Join(vbNewLine, sourceList))
        styleableList = styleableList.Distinct().ToList()
        styleableListArray = styleableListArray.Distinct().ToList()
        If styleableListArray.Count > 0 Then styleableList = styleableList.Except(styleableListArray).Concat(styleableListArray.Except(styleableList)).ToList()

        Dim filePath As String = RPath + "\R.java"
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
        MsgBox("Done!", vbInformation + vbMsgBoxSetForeground, "finished")
    End Sub
    Private Sub PackAAR(ByVal jarPath As String, ByVal savePath As String, ByVal sourcePath As String)
        Dim startInfo = New ProcessStartInfo(jarPath)
        Dim args As String = String.Format(" cvf ""{0}"" -C ""{1}""/ .", savePath, sourcePath)
        startInfo.Arguments = args
        startInfo.UseShellExecute = False
        startInfo.RedirectStandardOutput = True
        startInfo.CreateNoWindow = True
        startInfo.WindowStyle = ProcessWindowStyle.Hidden
        Using process As System.Diagnostics.Process = System.Diagnostics.Process.Start(startInfo)
            Dim sr = process.StandardOutput
            Debug.Print(sr.ReadToEnd)
            MsgBox("The aar file has been packaged. Please check that directory:" + vbNewLine + savePath, vbInformation + vbMsgBoxSetForeground, "finished")
        End Using
    End Sub
    Private Sub GeneratorRfile(ByVal aaptPath As String, ByVal savePath As String, ByVal srcPath As String, ByVal ManifestPath As String, ByVal androidPath As String)
        Dim startInfo = New ProcessStartInfo(aaptPath)
        Dim args As String = String.Format("{0}  package -v -f -m  -S {1} -J {2} -M {3} -I {4} ", aaptPath, srcPath, savePath, ManifestPath, androidPath)
        startInfo.Arguments = args
        startInfo.UseShellExecute = False
        startInfo.RedirectStandardOutput = True
        startInfo.CreateNoWindow = True
        startInfo.WindowStyle = ProcessWindowStyle.Hidden
        Using process As System.Diagnostics.Process = System.Diagnostics.Process.Start(startInfo)
            Dim sr = process.StandardOutput
            Debug.Print(sr.ReadToEnd)

        End Using
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        TextBox3.ForeColor = Color.Black
        TextBox3.TextAlign = HorizontalAlignment.Left
        If TextBox3.Text <> "" And TextBox3.Text.Contains("\") = True Then
            If File.Exists(TextBox3.Text + "\R.txt") = False Or File.Exists(TextBox3.Text + "\AndroidManifest.xml") = False Then Return
            If Directory.Exists(TextBox3.Text + "\jars") = False Or Directory.Exists(TextBox3.Text + "\res") = False Then Return
            Dim libname = Path.GetFileName(TextBox3.Text) + ".aar"
            PackAAR(My.Computer.FileSystem.SpecialDirectories.Temp + "\jar.exe", Application.StartupPath + "\" + libname, TextBox3.Text)
        Else
            TextBox3.Text = String.Empty
            Using dlg As New OpenFileDialog With {.AddExtension = True,
                                              .ValidateNames = False,
                                              .CheckFileExists = False,
                                              .CheckPathExists = True,
                                              .FileName = "Folder Selection"
                                              }
                If dlg.ShowDialog = DialogResult.OK Then
                    TextBox1.Text = System.IO.Path.GetDirectoryName(dlg.FileName)
                    If File.Exists(TextBox3.Text + "\R.txt") = False Or File.Exists(TextBox3.Text + "\AndroidManifest.xml") = False Then Return
                    If Directory.Exists(TextBox3.Text + "\jars") = False Or Directory.Exists(TextBox3.Text + "\res") = False Then Return
                    Dim libname = Path.GetFileName(TextBox3.Text) + ".aar"
                    PackAAR(My.Computer.FileSystem.SpecialDirectories.Temp + "\jar.exe", Application.StartupPath + "\" + libname, TextBox3.Text)
                End If
            End Using
        End If

    End Sub
    Public Shared Function GetText() As String
        Dim ReturnValue As String = String.Empty
        Dim STAThread As Thread = New Thread(Sub()
                                                 ReturnValue = System.Windows.Forms.Clipboard.GetText()
                                             End Sub)
        STAThread.SetApartmentState(ApartmentState.STA)
        STAThread.Start()
        STAThread.Join()
        Return ReturnValue
    End Function

    Private Sub TextBox3_Click(sender As Object, e As EventArgs) Handles TextBox3.Click
        Dim clipstring As String = GetText()
        If New Regex("^(?:[c-zC-Z]\:|\\)(\\[a-zA-Z_\-\s0-9\.]+)+").Match(clipstring).Success Then
            TextBox3.ForeColor = Color.Black
            TextBox3.TextAlign = HorizontalAlignment.Left
            TextBox3.Text = clipstring
        End If

    End Sub

    Private Async Function Button5_ClickAsync(sender As Object, e As EventArgs) As Task Handles Button5.Click
        ItemsDictionary.Clear()
        ItemsDictionary = Await DownloadLib.SearchIemt(ComboBox1.Text)
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
                                    If File.Exists(TextBox2.Text + "\pom.xml") Then File.Delete(TextBox2.Text + "\pom.xml")
                                    File.WriteAllText(TextBox2.Text + "\pom.xml", document.InnerXml)
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
    End Function
    Private Async Sub DownWithPom()
        If SysEnvironment.CheckSysEnvironmentExist("MAVEN_HOME") = False Then
            If MessageBox.Show("MAVEN_HOME environment variable is not detected, do you want to download and install !", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) = DialogResult.Yes Then
                'System.Diagnostics.Process.Start("http://maven.apache.org/download.cgi")
                needSelect = True
                DownloadForm.ShowDialog()
                If File.Exists(downloadPath + "\apache-maven.zip") Then
                    Dim ExtractName As String = ""
                    Using archive As ZipArchive = ZipFile.OpenRead(downloadPath + "\apache-maven.zip")
                        For Each entry As ZipArchiveEntry In archive.Entries
                            ExtractName = entry.FullName.Replace("/", "")
                            Exit For
                        Next entry
                        If Directory.Exists("D:\apache-maven") = False Then
                            ZipFile.ExtractToDirectory(downloadPath + "\apache-maven.zip", "D:\apache-maven")
                        End If
                        SysEnvironment.SetSysEnvironment("MAVEN_HOME", "D:\apache-maven\" + ExtractName)
                        SysEnvironment.SetPathAfter("%MAVEN_HOME%\bin")
                        SysEnvironment.SetPathAfter("%SystemRoot%\system32;%SystemRoot%;%SystemRoot%\System32\Wbem")
                        MsgBox("The MAVEN_HOME environment variables have been set, please restart the system!", vbInformation + vbMsgBoxSetForeground, "finished")
                        Return
                    End Using
                Else
                    Return
                End If
            Else
                Return
            End If
        End If
        ItemsDictionary = Await DownloadLib.SearchIemt(ComboBox1.Text)
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
                        Dim Maven = Await DownloadLib.GetMaven(url)
                        If Maven <> "" Then
                            If File.Exists(TextBox2.Text + "\pom.xml") Then File.Delete(TextBox2.Text + "\pom.xml")
                            WriteXML(Maven, TextBox2.Text + "\pom.xml")
                            If File.Exists(TextBox2.Text + "\pom.xml") Then
                                WriteBatch(TextBox2.Text + "\pom.xml", TextBox2.Text + "\download.bat")
                                System.Diagnostics.Process.Start(TextBox2.Text + "\download.bat")
                            End If
                        End If
                    End If
                End If
            End If
        End If
    End Sub
    Private Sub WriteXML(Maven As String, outPath As String)
        Dim xmlDoc As New XmlDocument()
        Dim xmlDeclaration As XmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", Nothing)
        xmlDoc.InsertBefore(xmlDeclaration, xmlDoc.DocumentElement)
        Dim rootNode As XmlNode = xmlDoc.CreateElement("project")
        Dim attribute As XmlAttribute
        attribute = xmlDoc.CreateAttribute("xmlns")
        attribute.Value = "http://maven.apache.org/POM/4.0.0"
        rootNode.Attributes.Append(attribute)

        attribute = xmlDoc.CreateAttribute("xsi", "schemaLocation", " ")
        attribute.Value = "http://maven.apache.org/POM/4.0.0 http://maven.apache.org/xsd/maven-4.0.0.xsd"
        rootNode.Attributes.Append(attribute)

        attribute = xmlDoc.CreateAttribute("xmlns:xsi")
        attribute.Value = "http://www.w3.org/2001/XMLSchema-instance"
        rootNode.Attributes.Append(attribute)
        xmlDoc.AppendChild(rootNode)

        Dim userNode As XmlNode = xmlDoc.CreateElement("modelVersion", xmlDoc.DocumentElement.NamespaceURI)
        userNode.InnerText = "4.0.0"
        rootNode.AppendChild(userNode)

        userNode = xmlDoc.CreateElement("groupId", xmlDoc.DocumentElement.NamespaceURI)
        userNode.InnerText = "test.download"
        rootNode.AppendChild(userNode)

        userNode = xmlDoc.CreateElement("artifactId", xmlDoc.DocumentElement.NamespaceURI)
        userNode.InnerText = "test.download"
        rootNode.AppendChild(userNode)

        userNode = xmlDoc.CreateElement("version", xmlDoc.DocumentElement.NamespaceURI)
        userNode.InnerText = "1.0.0"
        rootNode.AppendChild(userNode)

        userNode = xmlDoc.CreateElement("dependencies", xmlDoc.DocumentElement.NamespaceURI)
        userNode.InnerXml = Maven
        rootNode.AppendChild(userNode)
        xmlDoc.Save(outPath)
    End Sub
    Private Sub WriteBatch(xmlPath As String, outPath As String) 'Need to set %MAVEN_HOME%\bin environment variables  see: https://www.youtube.com/watch?v=RQ_Z859Hd7Q
        If File.Exists(outPath) Then
            Try
                File.Delete(outPath)
            Catch ex As Exception

            End Try
        End If
        Using w As New StreamWriter(outPath)
            w.WriteLine("call mvn -f " + xmlPath + " dependency:copy-dependencies")
            w.WriteLine("@pause")
            w.Close()
        End Using


    End Sub
    Private Sub extractResource(szPath As String)
        Dim filename As String = ""
        Dim viewtype As String = ""
        Dim viewname As String = ""
        Dim itemlist As New List(Of List(Of String))
        Me.Invoke(New MethodInvoker(Sub() ListView1.Items.Clear()))
        If Directory.Exists(Path.GetDirectoryName(szPath) + "\res") = True Then
            Dim xmlfiles = Directory.GetFiles(Path.GetDirectoryName(szPath) + "\res", "*.*", SearchOption.AllDirectories).Where(Function(f) New List(Of String) From {".xml"}.IndexOf(Path.GetExtension(f)) >= 0).ToArray()
            If xmlfiles.Count > 0 Then
                For Each xmlfile In xmlfiles
                    If xmlfile.Contains("styles.xml") Then
                        Dim fileContent As String = File.ReadAllText(xmlfile)
                        For Each line In fileContent.Split({vbCrLf, vbLf, vbCr}, StringSplitOptions.RemoveEmptyEntries)
                            If line.Contains("style") And line.Contains("name") Then
                                filename = New Regex("(?<=parent=\"").*?(?=[\p{P}\p{S}-[._]])").Match(line).Value.Trim
                                viewtype = "style"
                                viewname = New Regex("(?<=name=\"").*?(?=[\p{P}\p{S}-[._]])").Match(line).Value.Trim
                                itemlist.Add(New List(Of String) From {viewtype, viewname, filename})
                                Exit For
                            End If
                        Next
                    ElseIf xmlfile.Contains("colors.xml") Then
                        Dim fileContent As String = File.ReadAllText(xmlfile)
                        For Each line In fileContent.Split({vbCrLf, vbLf, vbCr}, StringSplitOptions.RemoveEmptyEntries)
                            If line.Contains("color") Then
                                filename = New Regex("#.*?(?=[\p{P}\p{S}])").Match(line).Value.Trim
                                viewtype = "color"
                                viewname = New Regex("(?<=name=\"").*?(?=[\p{P}\p{S}-[._]])").Match(line).Value.Trim
                                itemlist.Add(New List(Of String) From {viewtype, viewname, filename})
                            End If
                        Next
                    ElseIf xmlfile.Contains("res\xml") Then
                        Dim fileContent As String = File.ReadAllText(xmlfile)
                        For Each line In fileContent.Split({vbCrLf, vbLf, vbCr}, StringSplitOptions.RemoveEmptyEntries)
                            If line.Contains("@") Then
                                filename = Path.GetFileName(xmlfile)
                                viewtype = New Regex("(?<=:).*?(?=[\p{P}\p{S}])").Match(line).Value.Trim
                                viewname = New Regex("(?<=@).*?(?=[\p{P}\p{S}-[/_]])").Match(line).Value.Trim
                                itemlist.Add(New List(Of String) From {viewtype, viewname, filename})
                            End If
                        Next
                    ElseIf xmlfile.Contains("res\values\") And xmlfile.Contains("strings.xml") Then
                        Dim fileContent As String = File.ReadAllText(xmlfile)
                        Dim matches As MatchCollection = Regex.Matches(fileContent, "<string.[\s\S]*?</string>", RegexOptions.Multiline Or RegexOptions.IgnoreCase)
                        For Each match As Match In matches
                            viewtype = "strings"
                            viewname = New Regex("(?<=name=\"").*?(?=[\p{P}\p{S}-[/_]])").Match(match.Value).Value.Trim
                            filename = New Regex("(?<=>).*?(?=<)").Match(match.Value).Value.Trim
                            itemlist.Add(New List(Of String) From {viewtype, viewname, filename})
                        Next
                    ElseIf xmlfile.Contains("strings.xml") = False Then
                        Dim fileContent As String = File.ReadAllText(xmlfile)
                        Dim matches As MatchCollection = Regex.Matches(fileContent, "<([^<]*)\/>", RegexOptions.Multiline Or RegexOptions.IgnoreCase)
                        For Each match As Match In matches
                            If match.Value.Contains("android:id") Then
                                filename = Path.GetFileName(xmlfile)
                                For Each line In match.Value.Split({vbCrLf, vbLf, vbCr}, StringSplitOptions.RemoveEmptyEntries)
                                    If line.Contains("<") Then
                                        viewtype = line.Replace("<", "").Trim
                                    ElseIf line.Contains("android:id") Then
                                        viewname = New Regex("(?<=id\/).*?(?=[\p{P}\p{S}-[._]])").Match(line).Value.Trim
                                    End If
                                Next
                                itemlist.Add(New List(Of String) From {viewtype, viewname, filename})
                            End If
                        Next
                    End If
                Next
            End If
        End If

        Me.Invoke(New MethodInvoker(Sub() ListView1.Items.AddRange(itemlist.Select(Function(row, index) New ListViewItem({index.ToString()}.Concat(row).ToArray())).ToArray())))
    End Sub

    Private Sub CheckBox_JAVA_HOME_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_JAVA_HOME.CheckedChanged
        If CheckBox_JAVA_HOME.Checked = False Then
            Dim folderDlg As FolderBrowserDialog = New FolderBrowserDialog()
            folderDlg.ShowNewFolderButton = True
            Dim result As DialogResult = folderDlg.ShowDialog()
            If result = DialogResult.OK Then
                Dim path = folderDlg.SelectedPath
                If MessageBox.Show(path + vbNewLine + " for JAVA environment! Please confirm again!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) = DialogResult.Yes Then
                    SysEnvironment.SetSysEnvironment("JAVA_HOME", path)
                    SysEnvironment.SetPathAfter("%JAVA_HOME%\bin")
                    SysEnvironment.SetPathAfter("%JAVA_HOME%\jre\bin")
                    CheckBox_JAVA_HOME.Checked = True
                    CheckBox_JAVA_HOME.Enabled = False
                    MsgBox("The JAVA_HOME environment variables have been set, please restart the system!", vbInformation + vbMsgBoxSetForeground, "finished")
                End If
            End If
        End If
    End Sub
    Private Sub CheckBox_ANDROID_SDK_HOME_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_ANDROID_SDK_HOME.CheckedChanged
        If CheckBox_ANDROID_SDK_HOME.Checked = False Then
            Dim folderDlg As FolderBrowserDialog = New FolderBrowserDialog()
            folderDlg.ShowNewFolderButton = True
            Dim result As DialogResult = folderDlg.ShowDialog()
            If result = DialogResult.OK Then
                Dim path = folderDlg.SelectedPath
                If MessageBox.Show(path + vbNewLine + " for ANDROID_SDK environment! Please confirm again!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) = DialogResult.Yes Then
                    SysEnvironment.SetSysEnvironment("ANDROID_SDK_HOME", path)
                    SysEnvironment.SetPathAfter("%ANDROID_SDK_HOME%\tools")
                    SysEnvironment.SetPathAfter("%ANDROID_SDK_HOME%\platform-tools")
                    CheckBox_ANDROID_SDK_HOME.Checked = True
                    CheckBox_ANDROID_SDK_HOME.Enabled = False
                    MsgBox("The ANDROID_SDK_HOME environment variables have been set, please restart the system!", vbInformation + vbMsgBoxSetForeground, "finished")
                End If
            End If
        End If
    End Sub
    Private Sub CheckBox_CLASSPATH_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_CLASSPATH.CheckedChanged
        If CheckBox_CLASSPATH.Checked = False Then
            Dim folderDlg As FolderBrowserDialog = New FolderBrowserDialog()
            folderDlg.ShowNewFolderButton = True
            Dim result As DialogResult = folderDlg.ShowDialog()
            If result = DialogResult.OK Then
                Dim path = folderDlg.SelectedPath
                If MessageBox.Show(path + vbNewLine + " for CLASSPATH environment! Please confirm again!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) = DialogResult.Yes Then
                    SysEnvironment.SetSysEnvironment("CLASSPATH", path)
                    SysEnvironment.SetPathAfter(".;%JAVA_HOME%\lib\dt.jar;%JAVA_HOME%\lib\tools.jar;")
                    CheckBox_MAVEN_HOME.Checked = True
                    CheckBox_CLASSPATH.Enabled = False
                    MsgBox("The MAVEN_HOME environment variables have been set, please restart the system!", vbInformation + vbMsgBoxSetForeground, "finished")
                End If
            End If
        End If
    End Sub
    Private Sub CheckBox_MAVEN_HOME_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_MAVEN_HOME.CheckedChanged
        If CheckBox_MAVEN_HOME.Checked = False Then
            Dim folderDlg As FolderBrowserDialog = New FolderBrowserDialog()
            folderDlg.ShowNewFolderButton = True
            Dim result As DialogResult = folderDlg.ShowDialog()
            If result = DialogResult.OK Then
                Dim path = folderDlg.SelectedPath
                If MessageBox.Show(path + vbNewLine + " for MAVEN environment! Please confirm again!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) = DialogResult.Yes Then
                    SysEnvironment.SetSysEnvironment("MAVEN_HOME", path)
                    SysEnvironment.SetPathAfter("%MAVEN_HOME%\bin")
                    SysEnvironment.SetPathAfter("%SystemRoot%\system32;%SystemRoot%;%SystemRoot%\System32\Wbem")
                    CheckBox_MAVEN_HOME.Checked = True
                    CheckBox_MAVEN_HOME.Enabled = False
                    MsgBox("The MAVEN_HOME environment variables have been set, please restart the system!", vbInformation + vbMsgBoxSetForeground, "finished")
                End If
            End If
        End If
    End Sub
    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        If ComboBox2.Text = "" Then Return
        ComboBox3.Items.Clear()
        RichTextBox1.Text = ""
        Dim codePath = ComboBox2.Text
        Dim newthread As New Thread(Sub()
                                        extractMethod(codePath)
                                    End Sub)
        newthread.Start()
    End Sub
    Private Sub ComboBox2_Click(sender As Object, e As EventArgs) Handles ComboBox2.Click
        If ComboBox2.Text = "" Then Return
        ComboBox3.Items.Clear()
        RichTextBox1.Text = ""
        Dim codePath = ComboBox2.Text
        Dim newthread As New Thread(Sub()
                                        extractMethod(codePath)
                                    End Sub)
        newthread.Start()

    End Sub
    Private Sub extractMethod(szContent As String)
        codeDictionary.Clear()
        Dim fileContent As String = File.ReadAllText(szContent)
        Dim matches As MatchCollection = Regex.Matches(fileContent, "((public|private|protected|static|final|native|synchronized|abstract|transient)+\s)+[\$_\w\<\>\w\s\[\]]*\s+[\$_\w]+\([^\)]*\)?\s*(?<method_body>\{(?>[^{}]+|\{(?<n>)|}(?<-n>))*(?(n)(?!))})", RegexOptions.Multiline Or RegexOptions.IgnoreCase)
        'Dim matches As MatchCollection = Regex.Matches(fileContent, "((public|private|protected|static|final|native|synchronized|abstract|transient)+\s)+[\$_\w\<\>\w\s\[\]]*\s+[\$_\w]+\([^\)]*\)?\s*", RegexOptions.Multiline Or RegexOptions.IgnoreCase) '\b(public|private|internal|protected|void)\s*s*\b(async)?\s*\b(static|virtual|abstract|void)?\s*\b(async)?\b(Task)?\s*[a-zA-Z]*(?<method>\s[A-Za-z_][A-Za-z_0-9]*\s*)\((([a-zA-Z\[\]\<\>]*\s*[A-Za-z_][A-Za-z_0-9]*\s*)[,]?\s*)+\)
        For Each match As Match In matches
            Dim methodName = match.Value.Trim.Split({vbCrLf, vbLf, vbCr}, StringSplitOptions.RemoveEmptyEntries)(0).Replace("{", "")
            codeDictionary.Add(methodName.Trim, match.Value)
            Try
                ComboBox3.Invoke(New MethodInvoker(Sub() ComboBox3.Items.Add(methodName.Trim)))
                ComboBox3.Invoke(New MethodInvoker(Sub() ComboBox3.SelectedIndex = 0))
            Catch ex As Exception

            End Try

        Next
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        If ComboBox3.Text = "" Then Return
        Try
            RichTextBox1.Text = codeDictionary(ComboBox3.Text)
        Catch ex As Exception

        End Try

    End Sub

    Private Sub ListView1_ColumnClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles ListView1.ColumnClick
        On Error Resume Next
        ListView1.ListViewItemSorter = New ListViewItemComparerByString(e.Column)
    End Sub
End Class

