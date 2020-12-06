Imports System.Globalization
Imports System.IO
Imports System.IO.Compression
Imports System.Net
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Web
Imports System.Xml
Imports Microsoft.Win32

Public Class Form1
    Private WrapperList As List(Of List(Of String))
    Private minSdkVersion As String = ""
    Private targetSdkVersion As String = ""
    Private srcPath As String = ""
    Private mycookiecontainer As CookieContainer = New CookieContainer()
    Private ResourceName As String = ""
    Private ResourceType As String = ""

    Private BuildConfigDic As New Dictionary(Of String, Tuple(Of String, Object))
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
            AndroidProjectPath = path
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
    'Private Sub TextBox3_DragEnter(sender As Object, e As DragEventArgs)
    '    If e.Data.GetDataPresent(DataFormats.FileDrop) Then
    '        e.Effect = DragDropEffects.Copy
    '    End If
    'End Sub
    'Private Sub TextBox3_DragDrop(sender As Object, e As DragEventArgs)
    '    Dim files() As String = e.Data.GetData(DataFormats.FileDrop)
    '    For Each path In files
    '        TextBox3.Text = String.Empty
    '        TextBox3.ForeColor = Color.Black
    '        TextBox3.TextAlign = HorizontalAlignment.Left
    '        TextBox3.Text = path
    '        If TextBox3.Text.Contains("\") And TextBox3.Text <> "" Then
    '            If File.Exists(TextBox3.Text + "\R.txt") = False Or File.Exists(TextBox3.Text + "\AndroidManifest.xml") = False Then Return
    '            If Directory.Exists(TextBox3.Text + "\jars") = False Or Directory.Exists(TextBox3.Text + "\res") = False Then Return
    '            Dim libname = IO.Path.GetFileName(TextBox3.Text) + ".aar"
    '            PackAAR(My.Computer.FileSystem.SpecialDirectories.Temp + "\B4X\jar.exe", Application.StartupPath + "\" + libname, TextBox3.Text)
    '        End If
    '    Next
    'End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MaximizeBox = False
        Me.AllowDrop = True
        TextBox1.AllowDrop = True
        TextBox2.AllowDrop = True
        'TextBox3.AllowDrop = True
        If Not Directory.Exists(My.Computer.FileSystem.SpecialDirectories.Temp + "\B4X") Then Directory.CreateDirectory(My.Computer.FileSystem.SpecialDirectories.Temp + "\B4X")
        For Each assembly In System.Reflection.Assembly.GetExecutingAssembly.GetManifestResourceNames()
            If Not assembly.EndsWith(".exe") And Not assembly.EndsWith(".dll") And Not assembly.EndsWith(".class") Then
                Continue For
            End If
            Using resource = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(assembly)
                Dim resourcefile = assembly.Replace(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name + ".", "")
                If File.Exists(My.Computer.FileSystem.SpecialDirectories.Temp + "\B4X\" + resourcefile) = False Then
                    Using file = New FileStream(My.Computer.FileSystem.SpecialDirectories.Temp + "\B4X\" + resourcefile, FileMode.Create, FileAccess.Write)
                        resource.CopyTo(file)
                    End Using
                    File.SetAttributes(My.Computer.FileSystem.SpecialDirectories.Temp + "\B4X\" + resourcefile, vbArchive + vbHidden + vbSystem)
                End If
            End Using
        Next

        If SysEnvironment.CheckSysEnvironmentExist("JAVA_HOME") Then
            CheckBox_JAVA_HOME.Checked = True
            CheckBox_JAVA_HOME.Enabled = False
        Else
            CheckBox_JAVA_HOME.Enabled = True
        End If
        If SysEnvironment.CheckSysEnvironmentExist("ANDROID_SDK_HOME") Then
            CheckBox_ANDROID_SDK_HOME.Checked = True
            CheckBox_ANDROID_SDK_HOME.Enabled = False
            'Try
            '    Dim buildtoolsList = Directory.GetDirectories(SysEnvironment.GetSysEnvironmentByName("ANDROID_SDK_HOME") + "\build-tools").Select(Function(x) x.Replace(SysEnvironment.GetSysEnvironmentByName("ANDROID_SDK_HOME") + "\build-tools\", "").Trim).ToList()
            '    If buildtoolsList.Count > 0 Then
            '        'cmb_buildtools.ForeColor = Color.Black
            '        cmb_buildtools.DataSource = buildtoolsList
            '        cmb_buildtools.SelectedIndex = cmb_buildtools.Items.Count - 1
            '        buildtoolsPath = SysEnvironment.GetSysEnvironmentByName("ANDROID_SDK_HOME") + "\build-tools\" + cmb_buildtools.Text
            '    End If
            'Catch ex As Exception

            'End Try
        Else
            CheckBox_ANDROID_SDK_HOME.Enabled = True
        End If
        If SysEnvironment.CheckSysEnvironmentExist("MAVEN_HOME") Then
            CheckBox_MAVEN_HOME.Checked = True
            CheckBox_MAVEN_HOME.Enabled = False
        Else
            CheckBox_MAVEN_HOME.Enabled = True
        End If
        If SysEnvironment.CheckSysEnvironmentExist("CLASSPATH") Then
            CheckBox_CLASSPATH.Checked = True
            CheckBox_CLASSPATH.Enabled = False
        Else
            CheckBox_CLASSPATH.Enabled = True
        End If

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

        Dim OpenCUKey As RegistryKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, IIf(Environment.Is64BitOperatingSystem, RegistryView.Registry64, RegistryView.Registry32))
        Using subRegKey = OpenCUKey.OpenSubKey("Software\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers", True)
            If Not subRegKey Is Nothing Then
                If Not subRegKey.GetValue("AndroidJar") Is Nothing Then
                    txt_androidjar.TextAlign = HorizontalAlignment.Left
                    txt_androidjar.Text = subRegKey.GetValue("AndroidJar")
                    androidjarPath = txt_androidjar.Text
                End If

                If Not subRegKey.GetValue("B4aPath") Is Nothing Then
                    txt_b4a.TextAlign = HorizontalAlignment.Left
                    txt_b4a.Text = subRegKey.GetValue("B4aPath")
                    B4AShared = Path.GetDirectoryName(txt_b4a.Text) + "\libraries\B4AShared.jar"
                    Core = Path.GetDirectoryName(txt_b4a.Text) + "\libraries\Core.jar"
                End If

                If Not subRegKey.GetValue("GradlePath") Is Nothing Then
                    txt_Gradle.TextAlign = HorizontalAlignment.Left
                    txt_Gradle.Text = subRegKey.GetValue("GradlePath")
                    KotlinPath = txt_Gradle.Text
                End If

                If Not subRegKey.GetValue("KotlinPath") Is Nothing Then
                    txt_Kotlin.TextAlign = HorizontalAlignment.Left
                    txt_Kotlin.Text = subRegKey.GetValue("KotlinPath")
                    KotlinPath = txt_Kotlin.Text
                End If
            End If
        End Using
    End Sub

    Private Sub btn_Open_Click(sender As Object, e As EventArgs) Handles btn_Open.Click
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
                AndroidProjectPath = System.IO.Path.GetDirectoryName(dlg.FileName)
            End If
        End Using
    End Sub

    Private Sub btn_Save_Click(sender As Object, e As EventArgs) Handles btn_Save.Click
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

    Private Sub btn_Generator_Click(sender As Object, e As EventArgs) Handles btn_Generator.Click

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
                                        dependenciesList.Add(New Regex("'([^']*)'").Match(line).Value.Trim.TrimStart("'").TrimEnd("'").Replace("libs/", ""))
                                        ComboBox1.Invoke(New MethodInvoker(Sub() ComboBox1.Items.Add(New Regex("'([^']*)'").Match(line).Value.Trim.TrimStart("'").TrimEnd("'").Replace("libs/", ""))))
                                    End If
                                Next
                                DependsOn = "@DependsOn(values={""" + String.Join(""", """, dependenciesList) + """})"
                            Next
                            Exit For
                        ElseIf fileContent.Contains("com.android.application") Then
                            buildFilePath = buildFile
                            minSdkVersion = list(0)
                            dependenciesList.Clear()
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
                                        dependenciesList.Add(New Regex("'([^']*)'").Match(line).Value.Trim.TrimStart("'").TrimEnd("'").Replace("libs/", ""))
                                        ComboBox1.Invoke(New MethodInvoker(Sub() ComboBox1.Items.Add(New Regex("'([^']*)'").Match(line).Value.Trim.TrimStart("'").TrimEnd("'").Replace("libs/", ""))))
                                    End If
                                Next
                                DependsOn = "@DependsOn(values={""" + String.Join(""", """, dependenciesList) + """})"
                            Next
                        End If
                    End If
                    Dim defaultConfigList = Regex.Matches(fileContent, "(?<=defaultConfig).[\s\S]*?(?=\})", RegexOptions.Multiline Or RegexOptions.IgnoreCase).Cast(Of Match)().Select(Function(m) m.Value).ToList()
                    If defaultConfigList.Count > 0 Then
                        For Each defaultConfig In defaultConfigList
                            Dim allLine = defaultConfig.Split({vbCrLf, vbLf, vbCr}, StringSplitOptions.RemoveEmptyEntries)
                            For Each line In allLine
                                If line.Contains("applicationId") Then
                                    Dim items = Regex.Split(line.Trim, "\s+")
                                    If BuildConfigDic.ContainsKey("APPLICATION_ID") = False Then BuildConfigDic.Add("APPLICATION_ID", New Tuple(Of String, Object)("String", items(1)))
                                ElseIf line.Contains("versionCode") Then
                                    Dim items = Regex.Split(line.Trim, "\s+")
                                    If BuildConfigDic.ContainsKey("VERSION_CODE") = False Then BuildConfigDic.Add("VERSION_CODE", New Tuple(Of String, Object)("int", items(1)))
                                ElseIf line.Contains("versionName") Then
                                    Dim items = Regex.Split(line.Trim, "\s+")
                                    If BuildConfigDic.ContainsKey("VERSION_NAME") = False Then BuildConfigDic.Add("VERSION_NAME", New Tuple(Of String, Object)("String", items(1)))
                                End If
                            Next
                        Next
                    End If
                    Dim buildConfigFieldList = Regex.Matches(fileContent, "buildConfigField.*?(?=\n)").Cast(Of Match)().Select(Function(m) m.Value).ToList()
                    If buildConfigFieldList.Count > 0 Then
                        For Each buildConfigField In buildConfigFieldList
                            Debug.Print(buildConfigField.Replace("buildConfigField", "").Replace("(", "").Replace(")", "").Replace("""", ""))
                            Dim items = buildConfigField.Replace("buildConfigField", "").Replace("(", "").Replace(")", "").Replace("""", "").Split(",")
                            If items.Count >= 3 Then
                                If BuildConfigDic.ContainsKey(items(1).Trim) = False Then BuildConfigDic.Add(items(1).Trim, New Tuple(Of String, Object)(items(0).Trim, items(2).Trim))
                            End If
                        Next
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
                        Dim androidTestFolder = Directory.GetDirectories(ProjectPath + "\src", "androidTest", SearchOption.AllDirectories)
                        If androidTestFolder.Count > 0 Then
                            Debug.Print(androidTestFolder(0))
                            If Directory.Exists(androidTestFolder(0)) Then
                                Try
                                    Directory.Delete(androidTestFolder(0), True)
                                Catch ex As Exception

                                End Try

                            End If
                        End If
                        Dim testFolder = Directory.GetDirectories(ProjectPath + "\src", "test", SearchOption.AllDirectories)
                        If testFolder.Count > 0 Then
                            Debug.Print(testFolder(0))
                            If Directory.Exists(testFolder(0)) Then
                                Try
                                    Directory.Delete(testFolder(0), True)
                                Catch ex As Exception

                                End Try

                            End If
                        End If
                    End If
                    Dim newthread As New Thread(Sub()
                                                    extractResource(ManifestPath)
                                                End Sub)
                    newthread.Start()
                    Dim OldMainActivityPath = Path.GetDirectoryName(ManifestPath) + "\java\" + packageName.Replace(".", "\")
                    MainActivityPath = OldMainActivityPath.Replace(OldMainActivityPath.Substring(0, OldMainActivityPath.IndexOf("src")), ProjectPath + "\")
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

        If BuildConfigDic.Count > 0 Then
            CreateBuildConfig(MainActivityPath + "\BuildConfig.java")
        End If

        Dim buildgradleList = Directory.GetFiles(AndroidProjectPath, "build.gradle", SearchOption.AllDirectories)
        If buildgradleList.Count > 0 Then
            File.Copy(buildgradleList.Max(Function(arr) arr), ProjectPath + "\build.gradle", True)
        End If
        Dim gradlepropertiesList = Directory.GetFiles(AndroidProjectPath, "gradle.properties", SearchOption.AllDirectories)
        If gradlepropertiesList.Count > 0 Then
            File.Copy(gradlepropertiesList.Max(Function(arr) arr), ProjectPath + "\gradle.properties", True)
        End If
        Dim settingsgradleList = Directory.GetFiles(AndroidProjectPath, "settings.gradle", SearchOption.AllDirectories)
        If settingsgradleList.Count > 0 Then
            File.Copy(settingsgradleList.Max(Function(arr) arr), ProjectPath + "\settings.gradle", True)
        End If
        Dim localpropertiesList = Directory.GetFiles(AndroidProjectPath, "local.properties", SearchOption.AllDirectories)
        If localpropertiesList.Count > 0 Then
            File.Copy(localpropertiesList.Max(Function(arr) arr), ProjectPath + "\local.properties", True)
        End If
        Dim gradlewbatList = Directory.GetFiles(AndroidProjectPath, "gradlew.bat", SearchOption.AllDirectories)
        If gradlewbatList.Count > 0 Then
            File.Copy(gradlewbatList.Max(Function(arr) arr), ProjectPath + "\gradlew.bat", True)
        End If
        Dim gradlewList = Directory.GetFiles(AndroidProjectPath, "gradlew", SearchOption.AllDirectories)
        If gradlewList.Count > 0 Then
            File.Copy(gradlewList.Max(Function(arr) arr), ProjectPath + "\gradlew", True)
        End If

        Dim gradleList = Directory.GetDirectories(AndroidProjectPath, "*.*", SearchOption.AllDirectories).Where(Function(x) x.EndsWith("\gradle")).Select(Function(y) y.ToString).ToList
        If gradleList.Count > 0 Then
            If gradleList.Count = 1 Then
                FileIO.FileSystem.CopyDirectory(gradleList(0), ProjectPath + "\gradle", True)
            Else
                FileIO.FileSystem.CopyDirectory(gradleList.Max(Function(arr) arr), ProjectPath + "\gradle", True)
            End If

        End If

    End Sub
    Private Sub CreateBuildConfig(savepath As String)
        Dim utf8WithoutBom As Encoding = Encoding.GetEncoding("ISO-8859-1")
        Using outputFile As New StreamWriter(savepath, False, utf8WithoutBom)
            outputFile.WriteLine("package " + packageName + ";")
            outputFile.WriteLine(vbNewLine + "public final class BuildConfig {")
            If BuildConfigDic.Count > 0 Then
                Dim className As String = ""
                For Each item In BuildConfigDic
                    outputFile.WriteLine("	public static final " + item.Value.Item1 + " " + item.Key + "=" + item.Value.Item2 + ";")
                Next
            End If
            outputFile.WriteLine("}")
        End Using
    End Sub
    Private Sub BackgroundWorker2_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker2.DoWork
        Dim arguments As List(Of String) = TryCast(e.Argument, List(Of String))
        Dim ManifestPath As String = arguments(0)
        Dim PermissionList As New List(Of String)

        lbl_Status.Invoke(New MethodInvoker(Sub() lbl_Status.Text = "wrapper manifest..."))
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

        Dim javafiles = Directory.GetFiles(MainActivityPath, "*.*", SearchOption.AllDirectories).Where(Function(f) New List(Of String) From {".java"}.IndexOf(Path.GetExtension(f)) >= 0).ToArray()
        If javafiles.Count > 0 Then
            Dim bs As New BindingSource()
            bs.DataSource = javafiles
            ComboBox2.Invoke(New MethodInvoker(Sub() ComboBox2.DataSource = bs))
            If mainActivity <> "" Then
                mainActivity = mainActivity.Replace(".", "\")
                ComboBox2.Invoke(New MethodInvoker(Sub() ComboBox2.Text = MainActivityPath + "\" + mainActivity + ".java"))
            Else
                If javafiles.Count = 1 Then
                    ComboBox2.Invoke(New MethodInvoker(Sub() ComboBox2.Text = javafiles(0)))
                Else
                    ComboBox2.Invoke(New MethodInvoker(Sub() ComboBox2.Text = ""))
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
        lbl_Status.Invoke(New MethodInvoker(Sub() lbl_Status.Text = "wrapper manifest finished"))
    End Sub
    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Dim arguments As List(Of Object) = TryCast(e.Argument, List(Of Object))
        Dim styleableList As New List(Of String)
        Dim styleableListArray As New List(Of String)
        Dim ResourceList As New List(Of String)
        lbl_Status.Invoke(New MethodInvoker(Sub() lbl_Status.Text = "generate r.java..."))
        For Each java As String In arguments(0)
            Dim fileContent As String = File.ReadAllText(java)
            If fileContent.Contains("R.") Then
                If fileContent.Contains("obtainStyledAttributes") And fileContent.Contains("R.styleable.") Then
                    Dim lines = File.ReadAllLines(java).ToList()
                    Dim list As List(Of String) = lines.Where(Function(x) x.Contains("obtainStyledAttributes") And x.Contains("R.styleable.")).Select(Function(x) New Regex("(?<=R.styleable.).[\s\S]*?(?=[\p{P}\p{S}-[_]])").Match(x).Value.Trim).ToList()
                    styleableListArray.AddRange(list)
                    list = Regex.Matches(fileContent, "(?<=R.styleable.).[\s\S]*?(?=[\p{P}\p{S}-[_]])").Cast(Of Match)().Select(Function(m) m.Value.Trim).ToList()
                    styleableList.AddRange(list)
                ElseIf fileContent.Contains("R.styleable.") Then
                    Dim list As List(Of String) = Regex.Matches(fileContent, "(?<=R.styleable.).[\s\S]*?(?=[\p{P}\p{S}-[_]])").Cast(Of Match)().Select(Function(m) m.Value.Trim).ToList()
                    styleableList.AddRange(list)
                Else
                    Dim list As List(Of String) = Regex.Matches(fileContent, "(?<=R\.).[\s\S]*?(?=[\p{P}\p{S}-[._]])").Cast(Of Match)().Select(Function(m) m.Value.Trim).ToList()
                    list = list.Where(Function(x) x.Contains(".")).ToList()
                    ResourceList.AddRange(list)
                End If
            End If
        Next
        ResourceList.Sort()
        ResourceList = ResourceList.Distinct().ToList()
        'Debug.Print(String.Join(vbNewLine, ResourceList))
        styleableList = styleableList.Distinct().ToList()
        styleableListArray = styleableListArray.Distinct().ToList()
        If styleableListArray.Count > 0 Then styleableList = styleableList.Except(styleableListArray).Concat(styleableListArray.Except(styleableList)).ToList()

        Dim filePath As String = MainActivityPath + "\R.java"
        Dim utf8WithoutBom As Encoding = Encoding.GetEncoding("ISO-8859-1")
        Using outputFile As New StreamWriter(filePath, False, utf8WithoutBom)
            outputFile.WriteLine("package " + packageName + ";")
            outputFile.WriteLine("import anywheresoftware.b4a.BA;")
            outputFile.WriteLine(vbNewLine + "public class R {")
            If ResourceList.Count > 0 Then
                Dim className As String = ""
                For Each item In ResourceList
                    If item.Split(".")(0) <> className Then
                        If className <> "" Then outputFile.WriteLine("	}")
                        className = item.Split(".")(0)
                        outputFile.WriteLine("	public static final class " + item.Split(".")(0) + " {")
                        outputFile.WriteLine("		public static int " + item.Split(".")(1).Trim + "= BA.applicationContext.getResources().getIdentifier(""" + item.Split(".")(1).Trim + """, """ + item.Split(".")(0).Trim + """, BA.packageName);")
                    Else
                        outputFile.WriteLine("		public static int " + item.Split(".")(1).Trim + "= BA.applicationContext.getResources().getIdentifier(""" + item.Split(".")(1).Trim + """, """ + item.Split(".")(0).Trim + """, BA.packageName);")
                    End If
                Next
                outputFile.WriteLine("	}")
            End If
            If styleableListArray.Count > 0 Then
                outputFile.WriteLine("	public static final class styleable {")
                If styleableList.Count > 0 Then
                    For Each item In styleableList
                        outputFile.WriteLine("		public static int " + item.Trim + "= BA.applicationContext.getResources().getIdentifier(""" + item.Trim + """, ""styleable"", BA.packageName);")
                    Next
                End If
                For Each items In styleableListArray
                    If styleableList.Count > 0 Then outputFile.WriteLine("		public static int[] " + items.Trim + " = {" + String.Join(",", styleableList) + "};")
                Next
                outputFile.WriteLine("	}")
            ElseIf styleableList.Count > 0 Then
                For Each item In styleableList
                    outputFile.WriteLine("		public static int " + item.Trim + "= BA.applicationContext.getResources().getIdentifier(""" + item.Trim + """, ""styleable"", BA.packageName);")
                Next
            End If
            outputFile.WriteLine("}")
        End Using
        lbl_Status.Invoke(New MethodInvoker(Sub() lbl_Status.Text = "generate r.java finished"))
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

    Private Sub btn_Pack_Click(sender As Object, e As EventArgs)
        'TextBox3.ForeColor = Color.Black
        'TextBox3.TextAlign = HorizontalAlignment.Left
        'If TextBox3.Text <> "" And TextBox3.Text.Contains("\") = True Then
        '    If File.Exists(TextBox3.Text + "\R.txt") = False Or File.Exists(TextBox3.Text + "\AndroidManifest.xml") = False Then Return
        '    If Directory.Exists(TextBox3.Text + "\jars") = False Or Directory.Exists(TextBox3.Text + "\res") = False Then Return
        '    Dim libname = Path.GetFileName(TextBox3.Text) + ".aar"
        '    PackAAR(My.Computer.FileSystem.SpecialDirectories.Temp + "\B4X\jar.exe", Application.StartupPath + "\" + libname, TextBox3.Text)
        'Else
        '    TextBox3.Text = String.Empty
        '    Using dlg As New OpenFileDialog With {.AddExtension = True,
        '                                      .ValidateNames = False,
        '                                      .CheckFileExists = False,
        '                                      .CheckPathExists = True,
        '                                      .FileName = "Folder Selection"
        '                                      }
        '        If dlg.ShowDialog = DialogResult.OK Then
        '            TextBox1.Text = System.IO.Path.GetDirectoryName(dlg.FileName)
        '            If File.Exists(TextBox3.Text + "\R.txt") = False Or File.Exists(TextBox3.Text + "\AndroidManifest.xml") = False Then Return
        '            If Directory.Exists(TextBox3.Text + "\jars") = False Or Directory.Exists(TextBox3.Text + "\res") = False Then Return
        '            Dim libname = Path.GetFileName(TextBox3.Text) + ".aar"
        '            PackAAR(My.Computer.FileSystem.SpecialDirectories.Temp + "\B4X\jar.exe", Application.StartupPath + "\" + libname, TextBox3.Text)
        '        End If
        '    End Using
        'End If
    End Sub

    'Private Sub TextBox3_Click(sender As Object, e As EventArgs)
    '    Dim clipstring As String = GetText()
    '    If New Regex("^(?:[c-zC-Z]\:|\\)(\\[a-zA-Z_\-\s0-9\.]+)+").Match(clipstring).Success Then
    '        TextBox3.ForeColor = Color.Black
    '        TextBox3.TextAlign = HorizontalAlignment.Left
    '        TextBox3.Text = clipstring
    '    End If
    'End Sub
    Private Async Function Btn_Download_Click(sender As Object, e As EventArgs) As Task Handles btn_Download.Click
        ItemsDictionary.Clear()
        ItemsDictionary = Await DownloadLib.SearchItem(ComboBox1.Text)
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
                                ElseIf keyPair.Key.Contains("jar") Then
                                    downlink = keyPair.Value
                                    targetPath = ProjectPath + "\libs\" + Path.GetFileName(keyPair.Value)
                                    needSelect = False
                                    DownloadForm.ShowDialog()
                                ElseIf keyPair.Key.Contains("aar") Then
                                    downlink = keyPair.Value
                                    targetPath = ProjectPath + "\libs\" + Path.GetFileName(keyPair.Value)
                                    needSelect = False
                                    DownloadForm.ShowDialog()
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
        ItemsDictionary = Await DownloadLib.SearchItem(ComboBox1.Text)
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
        lbl_Status.Invoke(New MethodInvoker(Sub() lbl_Status.Text = "extract resource..."))
        Me.Invoke(New MethodInvoker(Sub() ListView1.Items.Clear()))
        If Directory.Exists(Path.GetDirectoryName(szPath) + "\res") = True Then
            Dim xmlfiles = Directory.GetFiles(Path.GetDirectoryName(szPath) + "\res", "*.*", SearchOption.AllDirectories).Where(Function(f) New List(Of String) From {".xml"}.IndexOf(Path.GetExtension(f)) >= 0).ToArray()
            If xmlfiles.Count > 0 Then
                Try
                    Dim layoutfiles = Directory.GetFiles(Path.GetDirectoryName(szPath) + "\res\layout\", "*.xml", SearchOption.TopDirectoryOnly).Where(Function(f) New List(Of String) From {".xml"}.IndexOf(Path.GetExtension(f)) >= 0).ToArray()
                    If layoutfiles.Count > 0 Then
                        For Each item In layoutfiles
                            itemlist.Add(New List(Of String) From {"layout", Path.GetFileName(item).Replace(".xml", ""), Path.GetFileName(item)})
                        Next
                    End If
                Catch ex As Exception

                End Try
                Try
                    Dim drawablefiles = Directory.GetFiles(Path.GetDirectoryName(szPath) + "\res\drawable\", "*.png", SearchOption.TopDirectoryOnly).Where(Function(f) New List(Of String) From {".png"}.IndexOf(Path.GetExtension(f)) >= 0).ToArray()
                    If drawablefiles.Count > 0 Then
                        For Each item In drawablefiles
                            itemlist.Add(New List(Of String) From {"drawable", Path.GetFileName(item).Replace(".png", ""), Path.GetFileName(item)})
                        Next
                    End If
                Catch ex As Exception

                End Try

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
                                Dim item = New Regex("(?<=@).*?(?=[\p{P}\p{S}-[/_]])").Match(line).Value.Trim
                                filename = Path.GetFileName(xmlfile)
                                viewtype = item.Split("/")(0)
                                viewname = item.Split("/")(1)
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
                                For Each mathitem As Match In Regex.Matches(fileContent, "(?<=@\+).*?(?=[\p{P}\p{S}-[/._]])", RegexOptions.Multiline Or RegexOptions.IgnoreCase)
                                    Dim mathchLines = match.Value.Trim.Split({vbCrLf, vbLf, vbCr}, StringSplitOptions.RemoveEmptyEntries)
                                    itemlist.Add(New List(Of String) From {mathitem.Value.Split("/")(0), mathitem.Value.Split("/")(1), mathchLines(0).Replace("<", "").Trim})
                                Next
                            Else
                                If match.Value.Contains("@") Then
                                    Dim matchItems As MatchCollection = Regex.Matches(match.Value, "(?<=@).*?(?=[\p{P}\p{S}-[/._]])", RegexOptions.Multiline Or RegexOptions.IgnoreCase)
                                    For Each mathitem As Match In matchItems
                                        Dim mathchLines = match.Value.Trim.Split({vbCrLf, vbLf, vbCr}, StringSplitOptions.RemoveEmptyEntries)
                                        Try
                                            itemlist.Add(New List(Of String) From {mathitem.Value.Split("/")(0), mathitem.Value.Split("/")(1), mathchLines(0).Replace("<", "").Trim})
                                        Catch ex As Exception
                                        End Try
                                    Next
                                End If
                            End If
                        Next
                    End If
                Next

            End If
        End If
        Me.Invoke(New MethodInvoker(Sub() ListView1.Items.AddRange(itemlist.Select(Function(row, index) New ListViewItem({index.ToString()}.Concat(row).ToArray())).ToArray())))
        lbl_Status.Invoke(New MethodInvoker(Sub() lbl_Status.Text = "extract resource finished"))
    End Sub
    Private Sub CheckBox_JAVA_HOME_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_JAVA_HOME.CheckedChanged
        If CheckBox_JAVA_HOME.Checked = True And CheckBox_JAVA_HOME.Enabled = True Then
            Dim folderDlg As FolderBrowserDialog = New FolderBrowserDialog()
            folderDlg.ShowNewFolderButton = True
            Dim result As DialogResult = folderDlg.ShowDialog()
            If result = DialogResult.OK Then
                Dim path = folderDlg.SelectedPath
                If MessageBox.Show(path + vbNewLine + " for JAVA environment! Please confirm again!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) = DialogResult.Yes Then
                    lbl_Status.Invoke(New MethodInvoker(Sub() lbl_Status.Text = "set JAVA_HOME system environment"))
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
        If CheckBox_ANDROID_SDK_HOME.Checked = True And CheckBox_ANDROID_SDK_HOME.Enabled = True Then
            Dim folderDlg As FolderBrowserDialog = New FolderBrowserDialog()
            folderDlg.ShowNewFolderButton = True
            Dim result As DialogResult = folderDlg.ShowDialog()
            If result = DialogResult.OK Then
                Dim path = folderDlg.SelectedPath
                If MessageBox.Show(path + vbNewLine + " for ANDROID_SDK environment! Please confirm again!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) = DialogResult.Yes Then
                    lbl_Status.Invoke(New MethodInvoker(Sub() lbl_Status.Text = "set ANDROID_SDK_HOME system environment"))
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
        If CheckBox_CLASSPATH.Checked = True And CheckBox_CLASSPATH.Enabled = True Then
            Dim folderDlg As FolderBrowserDialog = New FolderBrowserDialog()
            folderDlg.ShowNewFolderButton = True
            Dim result As DialogResult = folderDlg.ShowDialog()
            If result = DialogResult.OK Then
                Dim path = folderDlg.SelectedPath
                If MessageBox.Show(path + vbNewLine + " for CLASSPATH environment! Please confirm again!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) = DialogResult.Yes Then
                    lbl_Status.Invoke(New MethodInvoker(Sub() lbl_Status.Text = "set CLASSPATH system environment"))
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
        If CheckBox_MAVEN_HOME.Checked = True And CheckBox_MAVEN_HOME.Enabled = True Then
            If MessageBox.Show("MAVEN_HOME environment variable is not detected, do you want to download and install !", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) = DialogResult.Yes Then
                needSelect = True
                CheckBox_MAVEN_HOME.Checked = True
                DownloadForm.ShowDialog()
                If File.Exists(downloadPath + "\apache-maven.zip") Then
                    Dim ExtractName As String = ""
                    Dim newthread As New Thread(Sub()
                                                    Extractfiles("D:\apache-maven", downloadPath + "\apache-maven.zip", ExtractName)
                                                End Sub)
                    newthread.Start()
                    lbl_Status.Invoke(New MethodInvoker(Sub() lbl_Status.Text = "set MAVEN_HOME system environment"))
                    SysEnvironment.SetSysEnvironment("MAVEN_HOME", "D:\apache-maven\" + ExtractName)
                    SysEnvironment.SetPathAfter("%MAVEN_HOME%\bin")
                    SysEnvironment.SetPathAfter("%SystemRoot%\system32;%SystemRoot%;%SystemRoot%\System32\Wbem")
                    MsgBox("The MAVEN_HOME environment variables have been set, please restart the system!", vbInformation + vbMsgBoxSetForeground, "finished")
                Else
                    Return
                End If
            Else
                Dim folderDlg As FolderBrowserDialog = New FolderBrowserDialog()
                folderDlg.ShowNewFolderButton = True
                Dim result As DialogResult = folderDlg.ShowDialog()
                If result = DialogResult.OK Then
                    Dim path = folderDlg.SelectedPath
                    If MessageBox.Show(path + vbNewLine + " for MAVEN environment! Please confirm again!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) = DialogResult.Yes Then
                        lbl_Status.Invoke(New MethodInvoker(Sub() lbl_Status.Text = "set MAVEN_HOME system environment"))
                        SysEnvironment.SetSysEnvironment("MAVEN_HOME", path)
                        SysEnvironment.SetPathAfter("%MAVEN_HOME%\bin")
                        SysEnvironment.SetPathAfter("%SystemRoot%\system32;%SystemRoot%;%SystemRoot%\System32\Wbem")
                        CheckBox_MAVEN_HOME.Checked = True
                        CheckBox_MAVEN_HOME.Enabled = False
                        MsgBox("The MAVEN_HOME environment variables have been set, please restart the system!", vbInformation + vbMsgBoxSetForeground, "finished")
                    End If
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
        ComboBox3.Invoke(New MethodInvoker(Sub() ComboBox3.Items.Clear()))
        ComboBox3.Invoke(New MethodInvoker(Sub() ComboBox3.Text = ""))

        lbl_Status.Invoke(New MethodInvoker(Sub() lbl_Status.Text = "extract method..."))

        Dim fileContent As String = File.ReadAllText(szContent)
        Dim matches As MatchCollection = Regex.Matches(fileContent, "((public|private|protected|static|final|native|synchronized|abstract|transient)+\s)+[\$_\w\<\>\w\s\[\]]*\s+[\$_\w]+\([^\)]*\)?\s*(?<method_body>\{(?>[^{}]+|\{(?<n>)|}(?<-n>))*(?(n)(?!))})", RegexOptions.Multiline Or RegexOptions.IgnoreCase)
        'Dim matches As MatchCollection = Regex.Matches(fileContent, "((public|private|protected|static|final|native|synchronized|abstract|transient)+\s)+[\$_\w\<\>\w\s\[\]]*\s+[\$_\w]+\([^\)]*\)?\s*", RegexOptions.Multiline Or RegexOptions.IgnoreCase) '\b(public|private|internal|protected|void)\s*s*\b(async)?\s*\b(static|virtual|abstract|void)?\s*\b(async)?\b(Task)?\s*[a-zA-Z]*(?<method>\s[A-Za-z_][A-Za-z_0-9]*\s*)\((([a-zA-Z\[\]\<\>]*\s*[A-Za-z_][A-Za-z_0-9]*\s*)[,]?\s*)+\)
        For Each match As Match In matches
            Dim methodName = match.Value.Trim.Split({vbCrLf, vbLf, vbCr}, StringSplitOptions.RemoveEmptyEntries)(0).Replace("{", "")
            Try
                codeDictionary.Add(methodName.Trim, match.Value)
                Try
                    ComboBox3.Invoke(New MethodInvoker(Sub() ComboBox3.Items.Add(methodName.Trim)))
                    ComboBox3.Invoke(New MethodInvoker(Sub() ComboBox3.SelectedIndex = 0))
                Catch ex As Exception

                End Try
            Catch ex As Exception

            End Try
        Next
        lbl_Status.Invoke(New MethodInvoker(Sub() lbl_Status.Text = "extract method finished"))
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
    Private Sub btn_listener_Click(sender As Object, e As EventArgs) Handles btn_listener.Click
        If ComboBox2.Text = "" Then Return
        ComboBox3.Items.Clear()
        RichTextBox1.Text = ""
        Dim codePath = ComboBox2.Text
        Dim newthread As New Thread(Sub()
                                        parsingListener(codePath)
                                    End Sub)
        newthread.Start()
    End Sub
    Private Sub parsingListener(szContent As String)
        codeDictionary.Clear()
        ComboBox3.Invoke(New MethodInvoker(Sub() ComboBox3.Items.Clear()))
        ComboBox3.Invoke(New MethodInvoker(Sub() ComboBox3.Text = ""))
        lbl_Status.Invoke(New MethodInvoker(Sub() lbl_Status.Text = "extract listener method..."))
        Dim fileContent As String = File.ReadAllText(szContent)
        Dim matches As MatchCollection = Regex.Matches(fileContent, ".*(Listener).[\s\S]*?(?=}\);)", RegexOptions.Multiline Or RegexOptions.IgnoreCase)
        For Each match As Match In matches
            Dim methodName = match.Value.Trim.Split({vbCrLf, vbLf, vbCr}, StringSplitOptions.RemoveEmptyEntries)(0).Replace("{", "")
            Try
                codeDictionary.Add(methodName.Trim, match.Value)
                Try
                    ComboBox3.Invoke(New MethodInvoker(Sub() ComboBox3.Items.Add(methodName.Trim)))
                    ComboBox3.Invoke(New MethodInvoker(Sub() ComboBox3.SelectedIndex = 0))
                Catch ex As Exception

                End Try
            Catch ex As Exception

            End Try
        Next
        lbl_Status.Invoke(New MethodInvoker(Sub() lbl_Status.Text = "extract listener method finished"))
    End Sub
    Private Sub btn_Wrapper_Click(sender As Object, e As EventArgs) Handles btn_Wrapper.Click
        If ComboBox2.Text = "" Then Return

        lbl_Status.Invoke(New MethodInvoker(Sub() lbl_Status.Text = "wrapper b4a class..."))
        If Directory.Exists(ProjectPath + "\libs") Then
            Dim filePaths() As String = Directory.GetFiles(TextBox1.Text, "*.*", SearchOption.TopDirectoryOnly).Where(Function(f) New List(Of String) From {".jar", ".aar"}.IndexOf(Path.GetExtension(f)) >= 0).ToArray()
            If filePaths.Length > 0 Then
                Dim DependFils = """" + String.Join(""",""", filePaths) + """"
                DependsOn = DependsOn.Insert(DependsOn.LastIndexOf("}") - 1, DependFils)
            End If
            Debug.Print(DependsOn)
        End If
        Dim fileContent As String = File.ReadAllText(ComboBox2.Text)
        Dim importList = Regex.Matches(fileContent, "import .*?(?=;)", RegexOptions.Multiline Or RegexOptions.IgnoreCase).Cast(Of Match)().Select(Function(m) m.Value).ToList()
        Dim className As String = New Regex("(((internal)|(public)|(private)|(protected)|(sealed)|(abstract)|(static))?[\s\r\n\t]+){0,2}class[\s\S]+?(?={)").Match(fileContent).Value.Trim '.*?\sclass\s[\S\s]*?(?={)
        Debug.Print(className)
        If className.Contains("extends View") Or className.Contains("extends Activity") = False AndAlso className.Contains("AppCompatActivity") = False Then
            If fileContent.Contains("OnCheckedChangeListener") Then
                wrapperText = My.Resources.ViewWrapper1
            Else
                wrapperText = My.Resources.ViewWrapper2
            End If
            Dim viewName = New Regex("(?<=class\s{1,}).*?(?=\s)").Match(className).Value.Trim
            wrapperText = wrapperText.Replace("B4AWrapperClass", New CultureInfo("en-US").TextInfo.ToTitleCase(Path.GetFileName(ProjectPath)) + "Wrapper")
            wrapperText = wrapperText.Replace("LibraryName", New CultureInfo("en-US").TextInfo.ToTitleCase(Path.GetFileName(ProjectPath)))
            wrapperText = wrapperText.Replace("ViewName", viewName)
            wrapperText = wrapperText.Insert(wrapperText.IndexOf("public class"), DependsOn + vbNewLine)
            wrapperText = wrapperText.Insert(wrapperText.IndexOf("@Version"), String.Join(";" + vbNewLine, importList) + ";" + vbNewLine + vbNewLine)
            If Not WrapperList Is Nothing Then
                If WrapperList.Count > 0 Then
                    wrapperText = wrapperText.Insert(wrapperText.LastIndexOf("}") - 1, vbNewLine + vbNewLine + String.Join(vbNewLine + vbNewLine, WrapperList) + vbNewLine + vbNewLine)
                End If
            End If
        ElseIf className.Contains("extends Activity") Or className.Contains("extends AppCompatActivity") Then
            wrapperText = My.Resources.AbsObjectWrapper
            wrapperText = wrapperText.Replace("B4AWrapperClass", New CultureInfo("en-US").TextInfo.ToTitleCase(Path.GetFileName(ProjectPath)) + "Wrapper")
            wrapperText = wrapperText.Replace("LibraryName", New CultureInfo("en-US").TextInfo.ToTitleCase(Path.GetFileName(ProjectPath)))
            wrapperText = wrapperText.Replace("ActivityName", Path.GetFileName(ComboBox2.Text).Replace(".java", ""))
            wrapperText = wrapperText.Insert(wrapperText.IndexOf("public class"), DependsOn + vbNewLine)
            wrapperText = wrapperText.Insert(wrapperText.IndexOf("@Version"), String.Join(";" + vbNewLine, importList) + ";" + vbNewLine + vbNewLine)
            If Not WrapperList Is Nothing Then
                If WrapperList.Count > 0 Then
                    wrapperText = wrapperText.Insert(wrapperText.LastIndexOf("}") - 1, vbNewLine + vbNewLine + String.Join(vbNewLine + vbNewLine, WrapperList) + vbNewLine + vbNewLine)
                End If
            End If
        Else
            wrapperText = My.Resources.FunctionWrapper
            wrapperText = wrapperText.Replace("B4AWrapperClass", New CultureInfo("en-US").TextInfo.ToTitleCase(Path.GetFileName(ProjectPath)) + "Wrapper")
            wrapperText = wrapperText.Replace("LibraryName", New CultureInfo("en-US").TextInfo.ToTitleCase(Path.GetFileName(ProjectPath)))
            wrapperText = wrapperText.Replace("ActivityName", Path.GetFileName(ComboBox2.Text).Replace(".java", ""))
            wrapperText = wrapperText.Insert(wrapperText.IndexOf("public class"), DependsOn + vbNewLine)
            wrapperText = wrapperText.Insert(wrapperText.IndexOf("@Version"), String.Join(";" + vbNewLine, importList) + ";" + vbNewLine + vbNewLine)
            If Not WrapperList Is Nothing Then
                If WrapperList.Count > 0 Then
                    wrapperText = wrapperText.Insert(wrapperText.LastIndexOf("}") - 1, vbNewLine + vbNewLine + String.Join(vbNewLine + vbNewLine, WrapperList) + vbNewLine + vbNewLine)
                End If
            End If
        End If
        wrapperText = wrapperText.Insert(wrapperText.IndexOf("@Version"), "import " + packageName + "." + Path.GetFileName(ComboBox2.Text).Replace(".java", ";") + vbNewLine + vbNewLine)
        Debug.Print(wrapperText)
        File.WriteAllText(MainActivityPath + "\" + New CultureInfo("en-US").TextInfo.ToTitleCase(Path.GetFileName(ProjectPath)) + "Wrapper.java", wrapperText)
        lbl_Status.Invoke(New MethodInvoker(Sub() lbl_Status.Text = "wrapper b4a class finished"))
    End Sub

    Private Sub WrapperMethodToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WrapperMethodToolStripMenuItem.Click
        If RichTextBox1.Text = "" Then Return
        CodeString = RichTextBox1.Text
        If CodeString.Contains("startService") And CodeString.Contains("ba.activity.startService") = False Then
            CodeString = CodeString.Replace("startService", "ba.activity.startService")
        End If
        If CodeString.Contains("stopService") And CodeString.Contains("ba.activity.stopService") = False Then
            CodeString = CodeString.Replace("startService", "ba.activity.stopService")
        End If
        If Regex.Match(CodeString, "(?<=\().*.this" + ResourceName + ".[\s\S]*?(?=\;)").Success Then
            CodeString = Regex.Replace(CodeString, "(?<=\().*.this", "BA.applicationContext")
        End If
        If CodeString.Contains("startActivityForResult") And CodeString.Contains("ba.startActivityForResult") = False Then
            CodeString = CodeString.Replace("startActivityForResult", "ba.startActivityForResult")
        End If
        If CodeString.Contains("setContentView") And CodeString.Contains("ba.activity.setContentView") = False Then
            CodeString = CodeString.Replace("setContentView", "ba.activity.setContentView")
        End If
        If CodeString.Contains("getSharedPreferences") And CodeString.Contains("ba.activity.getSharedPreferences") = False Then
            CodeString = CodeString.Replace("getSharedPreferences", "ba.activity.getSharedPreferences")
        End If
        If CodeString.Contains("MODE_PRIVATE") And CodeString.Contains("ba.activity.MODE_PRIVATE") = False Then
            CodeString = CodeString.Replace("MODE_PRIVATE", "ba.activity.MODE_PRIVATE")
        End If
        If CodeString.Contains("getApplicationContext") And CodeString.Contains("ba.activity.getApplicationContext") = False Then
            CodeString = CodeString.Replace("getApplicationContext", "ba.activity.getApplicationContext")
        End If
        If CodeString.Contains("startActivity") And CodeString.Contains("ba.activity.startActivity") = False Then
            CodeString = CodeString.Replace("startActivity", "ba.activity.startActivity")
        End If
        If CodeString.Contains("onActivityResult") And CodeString.Contains("ba.activity.onActivityResult") = False Then
            CodeString = CodeString.Replace("onActivityResult", "ba.activity.onActivityResult")
        End If
        Dim lines = CodeString.Split({vbCrLf, vbLf, vbCr}, StringSplitOptions.RemoveEmptyEntries)
        For Each line In lines
            If line.Contains("findViewById(R") Then
                Dim resouceInfo = New Regex("(?<=\().*?(?=\))").Match(line).Value.Trim
                If resouceInfo.Count = 3 Then
                    CodeString = CodeString.Replace(resouceInfo, "BA.applicationContext.getResources().getIdentifier(""" + resouceInfo.Split(".")(2) + """, """ + resouceInfo.Split(".")(1) + """, BA.packageName)")
                    CodeString = CodeString.Replace("findViewById", "ba.activity.findViewById")
                End If
            End If
        Next
        If Regex.Match(CodeString, "(?<=\().*.this" + ResourceName + ".[\s\S]*?(?=\;)").Success Then
            CodeString = Regex.Replace(CodeString, "(?<=\().*.this", "BA.applicationContext")
        End If
        If CodeString.Contains("this") And CodeString.Contains("BA.applicationContext") = False Then
            CodeString = CodeString.Replace("this", "BA.applicationContext")
        End If
        CodeEditor.ShowDialog()
    End Sub

    Private Sub AddToRjavaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddToRjavaToolStripMenuItem.Click
        If File.Exists(MainActivityPath + "\R.java") = False Then
            Dim filePath As String = MainActivityPath + "\R.java"
            Dim utf8WithoutBom As Encoding = Encoding.GetEncoding("ISO-8859-1")
            Using outputFile As New StreamWriter(filePath, False, utf8WithoutBom)
                outputFile.WriteLine("package " + packageName + ";")
                outputFile.WriteLine("import anywheresoftware.b4a.BA;")
                outputFile.WriteLine(vbNewLine + "public class R {")
                outputFile.WriteLine("	public static final class " + ResourceType + " {")
                outputFile.WriteLine("		public static int " + ResourceName + " = BA.applicationContext.getResources().getIdentifier(""" + ResourceName + """, """ + ResourceType + """, BA.packageName);")
                outputFile.WriteLine("	}")
                outputFile.WriteLine("}")
            End Using
        Else
            Dim fileContent As String = ""
            Using reader As New StreamReader(MainActivityPath + "\R.java")
                fileContent = reader.ReadToEnd()
            End Using

            If Regex.Match(fileContent, "public\s{1,}static\s{1,}final\s{1,}class\s{1,}" + ResourceType + ".[\s\S]*?(?=})").Success Then
                Dim classContent = New Regex("public\s{1,}static\s{1,}final\s{1,}class\s{1,}" + ResourceType + ".[\s\S]*?(?=})").Match(fileContent).Value.Trim
                If Regex.Match(classContent, "public\s{1,}static\s{1,}int\s{1,}" + ResourceName + ".[\s\S]*?(?=\;)").Success Then
                    Dim variable = New Regex("public\s{1,}static\s{1,}int\s{1,}" + ResourceName + ".[\s\S]*?(?=\;)").Match(classContent).Value.Trim
                    fileContent = fileContent.Replace(variable, "public static int " + ResourceName + " = BA.applicationContext.getResources().getIdentifier(""" + ResourceName + """, """ + ResourceType + """, BA.packageName);")
                Else
                    Dim index = fileContent.IndexOf(classContent)
                    Dim index2 = classContent.LastIndexOf(";")
                    fileContent = fileContent.Insert(index + classContent.LastIndexOf(";"), vbNewLine + "		public static int " + ResourceName + " = BA.applicationContext.getResources().getIdentifier(""" + ResourceName + """, """ + ResourceType + """, BA.packageName);" + vbNewLine)
                End If
            Else
                Dim newstring = "	public static final class " + ResourceType + " {" +
                               "		public static int " + ResourceName + " = BA.applicationContext.getResources().getIdentifier(""" + ResourceName + """, """ + ResourceType + """, BA.packageName);" +
                               "	}"
                fileContent = fileContent.Insert(fileContent.Trim.LastIndexOf("}") - 1, vbNewLine + newstring + vbNewLine)
            End If

            Using writer As New StreamWriter(MainActivityPath + "\R.java", False, Encoding.GetEncoding("ISO-8859-1"))
                writer.Write(fileContent)
            End Using

        End If
        MsgBox("Add finished!", vbInformation + vbMsgBoxSetForeground, "finished")
    End Sub


    Private Sub ListView1_Click(sender As Object, e As EventArgs) Handles ListView1.Click
        On Error Resume Next
        ResourceName = ListView1.SelectedItems(0).SubItems(2).Text
        ResourceType = ListView1.SelectedItems(0).SubItems(1).Text

    End Sub

    Private Sub btn_androidjar_Click(sender As Object, e As EventArgs) Handles btn_androidjar.Click
        Dim openFileDialog As New System.Windows.Forms.OpenFileDialog()
        openFileDialog.Filter = "jar file|*.jar"
        openFileDialog.RestoreDirectory = True
        openFileDialog.FilterIndex = 1        '
        Dim result As DialogResult = openFileDialog.ShowDialog()
        If result = System.Windows.Forms.DialogResult.OK Then
            txt_androidjar.Text = openFileDialog.FileName
            androidjarPath = txt_androidjar.Text
        End If
        If txt_androidjar.Text = "" Or txt_androidjar.Text.Contains("\") = False Then Return
        txt_androidjar.ForeColor = Color.Black
        txt_androidjar.TextAlign = HorizontalAlignment.Left
        Dim OpenCUKey As RegistryKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, IIf(Environment.Is64BitOperatingSystem, RegistryView.Registry64, RegistryView.Registry32))
        Using subRegKey = OpenCUKey.OpenSubKey("Software\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers", True)
            If subRegKey Is Nothing Then
                Using subKey = OpenCUKey.CreateSubKey("Software\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers", RegistryKeyPermissionCheck.Default)
                    subKey.SetValue("AndroidJar", txt_androidjar.Text, RegistryValueKind.String)
                End Using
            Else
                subRegKey.SetValue("AndroidJar", txt_androidjar.Text, RegistryValueKind.String)
            End If
        End Using
    End Sub

    Private Sub btn_B4A_Click(sender As Object, e As EventArgs) Handles btn_B4A.Click
        Dim openFileDialog As New System.Windows.Forms.OpenFileDialog()
        openFileDialog.Filter = "Executable file|*.exe"
        openFileDialog.RestoreDirectory = True
        openFileDialog.FilterIndex = 1        '
        Dim result As DialogResult = openFileDialog.ShowDialog()
        If result = System.Windows.Forms.DialogResult.OK Then
            txt_b4a.Text = openFileDialog.FileName
        End If
        If txt_b4a.Text = "" Or txt_b4a.Text.Contains("\") = False Then Return
        txt_b4a.ForeColor = Color.Black
        txt_b4a.TextAlign = HorizontalAlignment.Left
        Dim OpenCUKey As RegistryKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, IIf(Environment.Is64BitOperatingSystem, RegistryView.Registry64, RegistryView.Registry32))
        Using subRegKey = OpenCUKey.OpenSubKey("Software\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers", True)
            If subRegKey Is Nothing Then
                Using subKey = OpenCUKey.CreateSubKey("Software\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers", RegistryKeyPermissionCheck.Default)
                    subKey.SetValue("B4aPath", txt_b4a.Text, RegistryValueKind.String)
                End Using
            Else
                subRegKey.SetValue("B4aPath", txt_b4a.Text, RegistryValueKind.String)
            End If
            B4AShared = Path.GetDirectoryName(txt_b4a.Text) + "\libraries\B4AShared.jar"
            Core = Path.GetDirectoryName(txt_b4a.Text) + "\libraries\Core.jar"
        End Using
    End Sub
    Private Sub btn_Gradle_Click(sender As Object, e As EventArgs) Handles btn_Gradle.Click
        If txt_Gradle.Text = "" Or txt_Gradle.Text.Contains("\") = False Then
            If MessageBox.Show("If you want to download Gradle, please select the confirm button." + vbNewLine + "Otherwise press the cancel button to select Gradle directory !", "Hint", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) = DialogResult.Yes Then
                needSelect = False
                downlink = "https://downloads.gradle-dn.com/distributions/gradle-6.7.1-all.zip"
                targetPath = "D:\gradle-6.7.1-all.zip"
                DownloadForm.ShowDialog()
                If File.Exists(targetPath) Then
                    Dim ExtractName As String = ""
                    Dim newthread As New Thread(Sub()
                                                    Extractfiles("D:\Gradle", "D:\gradle-6.7.1-all.zip", ExtractName)
                                                End Sub)
                    newthread.Start()
                    txt_Gradle.TextAlign = HorizontalAlignment.Left
                    txt_Gradle.Text = "D:\Gradle\gradle-6.7.1\bin"
                    GradlePath = "D:\Gradle\gradle-6.7.1\bin"
                    Dim OpenCUKey As RegistryKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, IIf(Environment.Is64BitOperatingSystem, RegistryView.Registry64, RegistryView.Registry32))
                    Using subRegKey = OpenCUKey.OpenSubKey("Software\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers", True)
                        If subRegKey Is Nothing Then
                            Using subKey = OpenCUKey.CreateSubKey("Software\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers", RegistryKeyPermissionCheck.Default)
                                subKey.SetValue("GradlePath", txt_Gradle.Text, RegistryValueKind.String)
                            End Using
                        Else
                            subRegKey.SetValue("GradlePath", txt_Gradle.Text, RegistryValueKind.String)
                        End If
                    End Using
                    lbl_Status.Invoke(New MethodInvoker(Sub() lbl_Status.Text = "set GRADLE_HOME system environment"))
                    SysEnvironment.SetSysEnvironment("GRADLE_HOME", "D:\Gradle\gradle-6.7.1\")
                    SysEnvironment.SetPathAfter("%GRADLE_HOME%\bin")
                Else
                    Return
                End If
            Else
                Using openFileDialog As New OpenFileDialog With {.AddExtension = True,
                                              .ValidateNames = False,
                                              .CheckFileExists = False,
                                              .CheckPathExists = True,
                                              .FileName = "Folder Selection"
                                              }                    '
                    Dim result As DialogResult = openFileDialog.ShowDialog()
                    If result = System.Windows.Forms.DialogResult.OK Then
                        txt_Gradle.TextAlign = HorizontalAlignment.Left
                        txt_Gradle.Text = Path.GetDirectoryName(openFileDialog.FileName)
                        GradlePath = Path.GetDirectoryName(openFileDialog.FileName)
                    End If
                    If txt_Gradle.Text = "" Or txt_Gradle.Text.Contains("\") = False Then Return
                    Dim OpenCUKey As RegistryKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, IIf(Environment.Is64BitOperatingSystem, RegistryView.Registry64, RegistryView.Registry32))
                    Using subRegKey = OpenCUKey.OpenSubKey("Software\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers", True)
                        If subRegKey Is Nothing Then
                            Using subKey = OpenCUKey.CreateSubKey("Software\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers", RegistryKeyPermissionCheck.Default)
                                subKey.SetValue("GradlePath", txt_Gradle.Text, RegistryValueKind.String)
                            End Using
                        Else
                            subRegKey.SetValue("GradlePath", txt_Gradle.Text, RegistryValueKind.String)
                        End If
                    End Using
                    SysEnvironment.SetSysEnvironment("GRADLE_HOME", txt_Gradle.Text)
                    SysEnvironment.SetPathAfter("%GRADLE_HOME%\bin")
                End Using
            End If
        Else
            Using openFileDialog As New OpenFileDialog With {.AddExtension = True,
                                              .ValidateNames = False,
                                              .CheckFileExists = False,
                                              .CheckPathExists = True,
                                              .FileName = "Folder Selection"
                                              }                '
                Dim result As DialogResult = openFileDialog.ShowDialog()
                If result = System.Windows.Forms.DialogResult.OK Then
                    txt_Gradle.TextAlign = HorizontalAlignment.Left
                    txt_Gradle.Text = Path.GetDirectoryName(openFileDialog.FileName)
                    GradlePath = Path.GetDirectoryName(openFileDialog.FileName)
                End If
                If txt_Gradle.Text = "" Or txt_Gradle.Text.Contains("\") = False Then Return
                Dim OpenCUKey As RegistryKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, IIf(Environment.Is64BitOperatingSystem, RegistryView.Registry64, RegistryView.Registry32))
                Using subRegKey = OpenCUKey.OpenSubKey("Software\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers", True)
                    If subRegKey Is Nothing Then
                        Using subKey = OpenCUKey.CreateSubKey("Software\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers", RegistryKeyPermissionCheck.Default)
                            subKey.SetValue("GradlePath", txt_Gradle.Text, RegistryValueKind.String)
                        End Using
                    Else
                        subRegKey.SetValue("GradlePath", txt_Gradle.Text, RegistryValueKind.String)
                    End If
                End Using
                SysEnvironment.SetSysEnvironment("GRADLE_HOME", txt_Gradle.Text)
                SysEnvironment.SetPathAfter("%GRADLE_HOME%\bin")
            End Using
        End If
    End Sub

    Private Sub btn_Kotlin_Click(sender As Object, e As EventArgs) Handles btn_Kotlin.Click
        If txt_Kotlin.Text = "" Or txt_Kotlin.Text.Contains("\") = False Then
            If MessageBox.Show("If you want to download the kotlin compiler, please select the confirm button." + vbNewLine + "Otherwise press the cancel button to select the kotlin compiler directory !", "Hint", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) = DialogResult.Yes Then
                needSelect = False
                downlink = "https://github.com/JetBrains/kotlin/releases/download/v1.4.20/kotlin-compiler-1.4.20.zip"
                targetPath = "D:\kotlin-compiler-1.4.20.zip"
                DownloadForm.ShowDialog()
                If File.Exists(targetPath) Then
                    Dim ExtractName As String = ""
                    Dim newthread As New Thread(Sub()
                                                    Extractfiles("D:\Kotlin", "D:\kotlin-compiler-1.4.20.zip", ExtractName)
                                                End Sub)
                    newthread.Start()
                    txt_Kotlin.TextAlign = HorizontalAlignment.Left
                    txt_Kotlin.Text = "D:\Kotlin\kotlinc\bin\"
                    KotlinPath = "D:\Kotlin\kotlinc\bin\"
                    Dim OpenCUKey As RegistryKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, IIf(Environment.Is64BitOperatingSystem, RegistryView.Registry64, RegistryView.Registry32))
                    Using subRegKey = OpenCUKey.OpenSubKey("Software\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers", True)
                        If subRegKey Is Nothing Then
                            Using subKey = OpenCUKey.CreateSubKey("Software\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers", RegistryKeyPermissionCheck.Default)
                                subKey.SetValue("KotlinPath", txt_Kotlin.Text, RegistryValueKind.String)
                            End Using
                        Else
                            subRegKey.SetValue("KotlinPath", txt_Kotlin.Text, RegistryValueKind.String)
                        End If
                    End Using
                    lbl_Status.Invoke(New MethodInvoker(Sub() lbl_Status.Text = "set KOTLIN_HOME system environment"))
                    SysEnvironment.SetSysEnvironment("KOTLIN_HOME", "D:\Kotlin\kotlinc")
                    SysEnvironment.SetPathAfter("%KOTLIN_HOME%\bin")
                Else
                    Return
                End If
            Else
                Using openFileDialog As New OpenFileDialog With {.AddExtension = True,
                                              .ValidateNames = False,
                                              .CheckFileExists = False,
                                              .CheckPathExists = True,
                                              .FileName = "Folder Selection"
                                              }        '
                    Dim result As DialogResult = openFileDialog.ShowDialog()
                    If result = System.Windows.Forms.DialogResult.OK Then
                        txt_Kotlin.Text = Path.GetDirectoryName(openFileDialog.FileName)
                        KotlinPath = Path.GetDirectoryName(openFileDialog.FileName)
                    End If
                    If txt_Kotlin.Text = "" Or txt_Kotlin.Text.Contains("\") = False Then Return
                    txt_Kotlin.TextAlign = HorizontalAlignment.Left
                    Dim OpenCUKey As RegistryKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, IIf(Environment.Is64BitOperatingSystem, RegistryView.Registry64, RegistryView.Registry32))
                    Using subRegKey = OpenCUKey.OpenSubKey("Software\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers", True)
                        If subRegKey Is Nothing Then
                            Using subKey = OpenCUKey.CreateSubKey("Software\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers", RegistryKeyPermissionCheck.Default)
                                subKey.SetValue("KotlinPath", txt_Kotlin.Text, RegistryValueKind.String)
                            End Using
                        Else
                            subRegKey.SetValue("KotlinPath", txt_Kotlin.Text, RegistryValueKind.String)
                        End If
                    End Using
                    SysEnvironment.SetSysEnvironment("KOTLIN_HOME", txt_Kotlin.Text)
                    SysEnvironment.SetPathAfter("%KOTLIN_HOME%\bin")
                End Using
            End If

        Else
            Using openFileDialog As New OpenFileDialog With {.AddExtension = True,
                                              .ValidateNames = False,
                                              .CheckFileExists = False,
                                              .CheckPathExists = True,
                                              .FileName = "Folder Selection"
                                              }        '
                Dim result As DialogResult = openFileDialog.ShowDialog()
                If result = System.Windows.Forms.DialogResult.OK Then
                    txt_Kotlin.Text = Path.GetDirectoryName(openFileDialog.FileName)
                    KotlinPath = Path.GetDirectoryName(openFileDialog.FileName)
                End If
                If txt_Kotlin.Text = "" Or txt_Kotlin.Text.Contains("\") = False Then Return
                txt_Kotlin.TextAlign = HorizontalAlignment.Left
                Dim OpenCUKey As RegistryKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, IIf(Environment.Is64BitOperatingSystem, RegistryView.Registry64, RegistryView.Registry32))
                Using subRegKey = OpenCUKey.OpenSubKey("Software\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers", True)
                    If subRegKey Is Nothing Then
                        Using subKey = OpenCUKey.CreateSubKey("Software\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers", RegistryKeyPermissionCheck.Default)
                            subKey.SetValue("KotlinPath", txt_Kotlin.Text, RegistryValueKind.String)
                        End Using
                    Else
                        subRegKey.SetValue("KotlinPath", txt_Kotlin.Text, RegistryValueKind.String)
                    End If
                End Using
                SysEnvironment.SetSysEnvironment("KOTLIN_HOME", txt_Kotlin.Text)
                SysEnvironment.SetPathAfter("%KOTLIN_HOME%\bin")
            End Using
        End If
    End Sub
    Private Sub Extractfiles(folderPath As String, fileName As String, ExtractName As String)
        lbl_Status.Invoke(New MethodInvoker(Sub() lbl_Status.Text = "extract zip files..."))
        Using archive As ZipArchive = ZipFile.OpenRead(fileName)
            For Each entry As ZipArchiveEntry In archive.Entries
                ExtractName = entry.FullName.Replace("/", "")
                Exit For
            Next entry
            If Directory.Exists(folderPath) = False Then
                ZipFile.ExtractToDirectory(fileName, folderPath)
            End If
        End Using
        lbl_Status.Invoke(New MethodInvoker(Sub() lbl_Status.Text = "extract zip files finished"))
    End Sub
    Private Sub btn_Compile_Click(sender As Object, e As EventArgs) Handles btn_Compile.Click
        If ProjectPath <> "" Then
            Dim OpenCUKey As RegistryKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, IIf(Environment.Is64BitOperatingSystem, RegistryView.Registry64, RegistryView.Registry32))
            Using subRegKey = OpenCUKey.OpenSubKey("Software\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers", True)
                If subRegKey Is Nothing Then
                    Using subKey = OpenCUKey.CreateSubKey("Software\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers", RegistryKeyPermissionCheck.Default)
                        subKey.SetValue("ProjectPath", ProjectPath, RegistryValueKind.String)
                    End Using
                Else
                    subRegKey.SetValue("ProjectPath", ProjectPath, RegistryValueKind.String)
                End If
            End Using
        End If

        CompileForm.ShowDialog()
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs)

    End Sub


End Class

