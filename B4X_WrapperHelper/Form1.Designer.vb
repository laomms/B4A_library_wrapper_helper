<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.btn_Open = New System.Windows.Forms.Button()
        Me.btn_Save = New System.Windows.Forms.Button()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.btn_Generator = New System.Windows.Forms.Button()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.BackgroundWorker2 = New System.ComponentModel.BackgroundWorker()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ComboBox2 = New System.Windows.Forms.ComboBox()
        Me.btn_Download = New System.Windows.Forms.Button()
        Me.btn_Wrapper = New System.Windows.Forms.Button()
        Me.btn_listener = New System.Windows.Forms.Button()
        Me.CheckBox_CLASSPATH = New System.Windows.Forms.CheckBox()
        Me.CheckBox_MAVEN_HOME = New System.Windows.Forms.CheckBox()
        Me.CheckBox_ANDROID_SDK_HOME = New System.Windows.Forms.CheckBox()
        Me.CheckBox_JAVA_HOME = New System.Windows.Forms.CheckBox()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AddToRjavaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.ContextMenuStrip2 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.WrapperMethodToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ComboBox3 = New System.Windows.Forms.ComboBox()
        Me.btn_androidjar = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btn_B4A = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txt_b4a = New System.Windows.Forms.TextBox()
        Me.txt_androidjar = New System.Windows.Forms.TextBox()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.lbl_Status = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.lbl_KOTLIN_HOME = New System.Windows.Forms.Label()
        Me.lbl_GRADLE_HOME = New System.Windows.Forms.Label()
        Me.lbl_MAVEN_HOME = New System.Windows.Forms.Label()
        Me.lbl_CLASSPATH = New System.Windows.Forms.Label()
        Me.lbl_ANDROID_SDK_HOME = New System.Windows.Forms.Label()
        Me.lbl_JAVA_HOME = New System.Windows.Forms.Label()
        Me.CheckBox_KOTLIN_HOME = New System.Windows.Forms.CheckBox()
        Me.CheckBox_GRADLE_HOME = New System.Windows.Forms.CheckBox()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.RichTextBoxGenerate = New System.Windows.Forms.RichTextBox()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.ListView2 = New System.Windows.Forms.ListView()
        Me.ContextMenuStrip3 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.DownloadToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.TabPage6 = New System.Windows.Forms.TabPage()
        Me.RichTextBoxManifest = New System.Windows.Forms.RichTextBox()
        Me.ContextMenuStrip4 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.CompileToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.TabPage7 = New System.Windows.Forms.TabPage()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.btn_Compile = New System.Windows.Forms.Button()
        Me.RichTextBoxCompile = New System.Windows.Forms.RichTextBox()
        Me.RichTextBox2 = New System.Windows.Forms.RichTextBox()
        Me.CompileWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.GradleWorker = New System.ComponentModel.BackgroundWorker()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.ContextMenuStrip2.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.ContextMenuStrip3.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.TabPage5.SuspendLayout()
        Me.TabPage6.SuspendLayout()
        Me.ContextMenuStrip4.SuspendLayout()
        Me.TabPage7.SuspendLayout()
        Me.SuspendLayout()
        '
        'TextBox1
        '
        Me.TextBox1.ForeColor = System.Drawing.SystemColors.GrayText
        Me.TextBox1.Location = New System.Drawing.Point(12, 23)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(468, 20)
        Me.TextBox1.TabIndex = 1
        Me.TextBox1.Text = "Drag and drop or open java project folder"
        Me.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btn_Open
        '
        Me.btn_Open.Location = New System.Drawing.Point(486, 18)
        Me.btn_Open.Name = "btn_Open"
        Me.btn_Open.Size = New System.Drawing.Size(68, 28)
        Me.btn_Open.TabIndex = 2
        Me.btn_Open.Text = "Open"
        Me.btn_Open.UseVisualStyleBackColor = True
        '
        'btn_Save
        '
        Me.btn_Save.Location = New System.Drawing.Point(486, 56)
        Me.btn_Save.Name = "btn_Save"
        Me.btn_Save.Size = New System.Drawing.Size(68, 28)
        Me.btn_Save.TabIndex = 5
        Me.btn_Save.Text = "Save"
        Me.btn_Save.UseVisualStyleBackColor = True
        '
        'TextBox2
        '
        Me.TextBox2.ForeColor = System.Drawing.SystemColors.GrayText
        Me.TextBox2.Location = New System.Drawing.Point(12, 61)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(468, 20)
        Me.TextBox2.TabIndex = 4
        Me.TextBox2.Text = "Select the directory to save"
        Me.TextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btn_Generator
        '
        Me.btn_Generator.Location = New System.Drawing.Point(201, 300)
        Me.btn_Generator.Name = "btn_Generator"
        Me.btn_Generator.Size = New System.Drawing.Size(139, 40)
        Me.btn_Generator.TabIndex = 6
        Me.btn_Generator.Text = "Generate Project"
        Me.btn_Generator.UseVisualStyleBackColor = True
        '
        'BackgroundWorker1
        '
        '
        'BackgroundWorker2
        '
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(86, 20)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(390, 21)
        Me.ComboBox1.TabIndex = 8
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(67, 13)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "DependsOn:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(7, 21)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(67, 13)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "MainActivity:"
        '
        'ComboBox2
        '
        Me.ComboBox2.FormattingEnabled = True
        Me.ComboBox2.Location = New System.Drawing.Point(80, 18)
        Me.ComboBox2.Name = "ComboBox2"
        Me.ComboBox2.Size = New System.Drawing.Size(398, 21)
        Me.ComboBox2.TabIndex = 10
        '
        'btn_Download
        '
        Me.btn_Download.Location = New System.Drawing.Point(487, 15)
        Me.btn_Download.Name = "btn_Download"
        Me.btn_Download.Size = New System.Drawing.Size(68, 28)
        Me.btn_Download.TabIndex = 12
        Me.btn_Download.Text = "Search"
        Me.btn_Download.UseVisualStyleBackColor = True
        '
        'btn_Wrapper
        '
        Me.btn_Wrapper.Location = New System.Drawing.Point(489, 47)
        Me.btn_Wrapper.Name = "btn_Wrapper"
        Me.btn_Wrapper.Size = New System.Drawing.Size(68, 27)
        Me.btn_Wrapper.TabIndex = 13
        Me.btn_Wrapper.Text = "Wrapper"
        Me.btn_Wrapper.UseVisualStyleBackColor = True
        '
        'btn_listener
        '
        Me.btn_listener.Location = New System.Drawing.Point(489, 13)
        Me.btn_listener.Name = "btn_listener"
        Me.btn_listener.Size = New System.Drawing.Size(68, 28)
        Me.btn_listener.TabIndex = 15
        Me.btn_listener.Text = "listener"
        Me.btn_listener.UseVisualStyleBackColor = True
        '
        'CheckBox_CLASSPATH
        '
        Me.CheckBox_CLASSPATH.AutoSize = True
        Me.CheckBox_CLASSPATH.Enabled = False
        Me.CheckBox_CLASSPATH.Location = New System.Drawing.Point(28, 75)
        Me.CheckBox_CLASSPATH.Name = "CheckBox_CLASSPATH"
        Me.CheckBox_CLASSPATH.Size = New System.Drawing.Size(89, 17)
        Me.CheckBox_CLASSPATH.TabIndex = 20
        Me.CheckBox_CLASSPATH.Text = "CLASSPATH"
        Me.CheckBox_CLASSPATH.UseVisualStyleBackColor = True
        '
        'CheckBox_MAVEN_HOME
        '
        Me.CheckBox_MAVEN_HOME.AutoSize = True
        Me.CheckBox_MAVEN_HOME.Enabled = False
        Me.CheckBox_MAVEN_HOME.Location = New System.Drawing.Point(28, 105)
        Me.CheckBox_MAVEN_HOME.Name = "CheckBox_MAVEN_HOME"
        Me.CheckBox_MAVEN_HOME.Size = New System.Drawing.Size(102, 17)
        Me.CheckBox_MAVEN_HOME.TabIndex = 2
        Me.CheckBox_MAVEN_HOME.Text = "MAVEN_HOME"
        Me.CheckBox_MAVEN_HOME.UseVisualStyleBackColor = True
        '
        'CheckBox_ANDROID_SDK_HOME
        '
        Me.CheckBox_ANDROID_SDK_HOME.AutoSize = True
        Me.CheckBox_ANDROID_SDK_HOME.Enabled = False
        Me.CheckBox_ANDROID_SDK_HOME.Location = New System.Drawing.Point(28, 45)
        Me.CheckBox_ANDROID_SDK_HOME.Name = "CheckBox_ANDROID_SDK_HOME"
        Me.CheckBox_ANDROID_SDK_HOME.Size = New System.Drawing.Size(142, 17)
        Me.CheckBox_ANDROID_SDK_HOME.TabIndex = 1
        Me.CheckBox_ANDROID_SDK_HOME.Text = "ANDROID_SDK_HOME"
        Me.ToolTip1.SetToolTip(Me.CheckBox_ANDROID_SDK_HOME, "X:\Android\Sdk")
        Me.CheckBox_ANDROID_SDK_HOME.UseVisualStyleBackColor = True
        '
        'CheckBox_JAVA_HOME
        '
        Me.CheckBox_JAVA_HOME.AutoSize = True
        Me.CheckBox_JAVA_HOME.Enabled = False
        Me.CheckBox_JAVA_HOME.Location = New System.Drawing.Point(29, 15)
        Me.CheckBox_JAVA_HOME.Name = "CheckBox_JAVA_HOME"
        Me.CheckBox_JAVA_HOME.Size = New System.Drawing.Size(90, 17)
        Me.CheckBox_JAVA_HOME.TabIndex = 0
        Me.CheckBox_JAVA_HOME.Text = "JAVA_HOME"
        Me.ToolTip1.SetToolTip(Me.CheckBox_JAVA_HOME, "X:\..\jdk-xx")
        Me.CheckBox_JAVA_HOME.UseVisualStyleBackColor = True
        '
        'ListView1
        '
        Me.ListView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ListView1.ContextMenuStrip = Me.ContextMenuStrip1
        Me.ListView1.HideSelection = False
        Me.ListView1.Location = New System.Drawing.Point(7, 9)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(555, 323)
        Me.ListView1.TabIndex = 0
        Me.ListView1.UseCompatibleStateImageBehavior = False
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddToRjavaToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(145, 26)
        '
        'AddToRjavaToolStripMenuItem
        '
        Me.AddToRjavaToolStripMenuItem.Name = "AddToRjavaToolStripMenuItem"
        Me.AddToRjavaToolStripMenuItem.Size = New System.Drawing.Size(144, 22)
        Me.AddToRjavaToolStripMenuItem.Text = "Add to R.java"
        '
        'RichTextBox1
        '
        Me.RichTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.RichTextBox1.ContextMenuStrip = Me.ContextMenuStrip2
        Me.RichTextBox1.ForeColor = System.Drawing.SystemColors.MenuHighlight
        Me.RichTextBox1.Location = New System.Drawing.Point(7, 89)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.Size = New System.Drawing.Size(550, 254)
        Me.RichTextBox1.TabIndex = 1
        Me.RichTextBox1.Text = ""
        '
        'ContextMenuStrip2
        '
        Me.ContextMenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.WrapperMethodToolStripMenuItem})
        Me.ContextMenuStrip2.Name = "ContextMenuStrip2"
        Me.ContextMenuStrip2.Size = New System.Drawing.Size(162, 26)
        '
        'WrapperMethodToolStripMenuItem
        '
        Me.WrapperMethodToolStripMenuItem.Name = "WrapperMethodToolStripMenuItem"
        Me.WrapperMethodToolStripMenuItem.Size = New System.Drawing.Size(161, 22)
        Me.WrapperMethodToolStripMenuItem.Text = "WrapperMethod"
        '
        'ComboBox3
        '
        Me.ComboBox3.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.ComboBox3.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.ComboBox3.FormattingEnabled = True
        Me.ComboBox3.Location = New System.Drawing.Point(81, 51)
        Me.ComboBox3.Name = "ComboBox3"
        Me.ComboBox3.Size = New System.Drawing.Size(398, 21)
        Me.ComboBox3.TabIndex = 0
        '
        'btn_androidjar
        '
        Me.btn_androidjar.Location = New System.Drawing.Point(494, 232)
        Me.btn_androidjar.Name = "btn_androidjar"
        Me.btn_androidjar.Size = New System.Drawing.Size(68, 28)
        Me.btn_androidjar.TabIndex = 25
        Me.btn_androidjar.Text = "Select"
        Me.btn_androidjar.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(26, 240)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(59, 13)
        Me.Label4.TabIndex = 24
        Me.Label4.Text = "android.jar:"
        '
        'btn_B4A
        '
        Me.btn_B4A.Location = New System.Drawing.Point(494, 294)
        Me.btn_B4A.Name = "btn_B4A"
        Me.btn_B4A.Size = New System.Drawing.Size(68, 28)
        Me.btn_B4A.TabIndex = 28
        Me.btn_B4A.Text = "Select"
        Me.btn_B4A.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(28, 300)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(50, 13)
        Me.Label5.TabIndex = 27
        Me.Label5.Text = "B4A.exe:"
        '
        'txt_b4a
        '
        Me.txt_b4a.ForeColor = System.Drawing.SystemColors.GrayText
        Me.txt_b4a.Location = New System.Drawing.Point(93, 297)
        Me.txt_b4a.Name = "txt_b4a"
        Me.txt_b4a.Size = New System.Drawing.Size(385, 20)
        Me.txt_b4a.TabIndex = 30
        Me.txt_b4a.Text = "Please select B4A executable file location"
        Me.txt_b4a.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txt_androidjar
        '
        Me.txt_androidjar.ForeColor = System.Drawing.SystemColors.GrayText
        Me.txt_androidjar.Location = New System.Drawing.Point(93, 237)
        Me.txt_androidjar.Name = "txt_androidjar"
        Me.txt_androidjar.Size = New System.Drawing.Size(385, 20)
        Me.txt_androidjar.TabIndex = 29
        Me.txt_androidjar.Text = "Please select android platforms"
        Me.txt_androidjar.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'ToolTip1
        '
        Me.ToolTip1.ToolTipTitle = "X:\..\jdk-xx"
        '
        'lbl_Status
        '
        Me.lbl_Status.ForeColor = System.Drawing.Color.MediumPurple
        Me.lbl_Status.Location = New System.Drawing.Point(4, 381)
        Me.lbl_Status.Name = "lbl_Status"
        Me.lbl_Status.Size = New System.Drawing.Size(567, 24)
        Me.lbl_Status.TabIndex = 31
        Me.lbl_Status.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Controls.Add(Me.TabPage5)
        Me.TabControl1.Controls.Add(Me.TabPage6)
        Me.TabControl1.Controls.Add(Me.TabPage7)
        Me.TabControl1.Location = New System.Drawing.Point(3, 6)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(574, 372)
        Me.TabControl1.TabIndex = 32
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.lbl_KOTLIN_HOME)
        Me.TabPage1.Controls.Add(Me.lbl_GRADLE_HOME)
        Me.TabPage1.Controls.Add(Me.lbl_MAVEN_HOME)
        Me.TabPage1.Controls.Add(Me.lbl_CLASSPATH)
        Me.TabPage1.Controls.Add(Me.lbl_ANDROID_SDK_HOME)
        Me.TabPage1.Controls.Add(Me.lbl_JAVA_HOME)
        Me.TabPage1.Controls.Add(Me.CheckBox_KOTLIN_HOME)
        Me.TabPage1.Controls.Add(Me.CheckBox_GRADLE_HOME)
        Me.TabPage1.Controls.Add(Me.CheckBox_JAVA_HOME)
        Me.TabPage1.Controls.Add(Me.CheckBox_ANDROID_SDK_HOME)
        Me.TabPage1.Controls.Add(Me.CheckBox_CLASSPATH)
        Me.TabPage1.Controls.Add(Me.CheckBox_MAVEN_HOME)
        Me.TabPage1.Controls.Add(Me.txt_b4a)
        Me.TabPage1.Controls.Add(Me.btn_B4A)
        Me.TabPage1.Controls.Add(Me.txt_androidjar)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.btn_androidjar)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(566, 346)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Setup"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'lbl_KOTLIN_HOME
        '
        Me.lbl_KOTLIN_HOME.AutoSize = True
        Me.lbl_KOTLIN_HOME.Enabled = False
        Me.lbl_KOTLIN_HOME.Location = New System.Drawing.Point(207, 171)
        Me.lbl_KOTLIN_HOME.Name = "lbl_KOTLIN_HOME"
        Me.lbl_KOTLIN_HOME.Size = New System.Drawing.Size(0, 13)
        Me.lbl_KOTLIN_HOME.TabIndex = 36
        '
        'lbl_GRADLE_HOME
        '
        Me.lbl_GRADLE_HOME.AutoSize = True
        Me.lbl_GRADLE_HOME.Enabled = False
        Me.lbl_GRADLE_HOME.Location = New System.Drawing.Point(207, 139)
        Me.lbl_GRADLE_HOME.Name = "lbl_GRADLE_HOME"
        Me.lbl_GRADLE_HOME.Size = New System.Drawing.Size(0, 13)
        Me.lbl_GRADLE_HOME.TabIndex = 35
        '
        'lbl_MAVEN_HOME
        '
        Me.lbl_MAVEN_HOME.AutoSize = True
        Me.lbl_MAVEN_HOME.Enabled = False
        Me.lbl_MAVEN_HOME.Location = New System.Drawing.Point(207, 106)
        Me.lbl_MAVEN_HOME.Name = "lbl_MAVEN_HOME"
        Me.lbl_MAVEN_HOME.Size = New System.Drawing.Size(0, 13)
        Me.lbl_MAVEN_HOME.TabIndex = 34
        '
        'lbl_CLASSPATH
        '
        Me.lbl_CLASSPATH.AutoSize = True
        Me.lbl_CLASSPATH.Enabled = False
        Me.lbl_CLASSPATH.Location = New System.Drawing.Point(207, 76)
        Me.lbl_CLASSPATH.Name = "lbl_CLASSPATH"
        Me.lbl_CLASSPATH.Size = New System.Drawing.Size(0, 13)
        Me.lbl_CLASSPATH.TabIndex = 33
        '
        'lbl_ANDROID_SDK_HOME
        '
        Me.lbl_ANDROID_SDK_HOME.AutoSize = True
        Me.lbl_ANDROID_SDK_HOME.Enabled = False
        Me.lbl_ANDROID_SDK_HOME.Location = New System.Drawing.Point(207, 45)
        Me.lbl_ANDROID_SDK_HOME.Name = "lbl_ANDROID_SDK_HOME"
        Me.lbl_ANDROID_SDK_HOME.Size = New System.Drawing.Size(0, 13)
        Me.lbl_ANDROID_SDK_HOME.TabIndex = 32
        '
        'lbl_JAVA_HOME
        '
        Me.lbl_JAVA_HOME.AutoSize = True
        Me.lbl_JAVA_HOME.Enabled = False
        Me.lbl_JAVA_HOME.Location = New System.Drawing.Point(207, 15)
        Me.lbl_JAVA_HOME.Name = "lbl_JAVA_HOME"
        Me.lbl_JAVA_HOME.Size = New System.Drawing.Size(0, 13)
        Me.lbl_JAVA_HOME.TabIndex = 31
        '
        'CheckBox_KOTLIN_HOME
        '
        Me.CheckBox_KOTLIN_HOME.AutoSize = True
        Me.CheckBox_KOTLIN_HOME.Enabled = False
        Me.CheckBox_KOTLIN_HOME.Location = New System.Drawing.Point(28, 170)
        Me.CheckBox_KOTLIN_HOME.Name = "CheckBox_KOTLIN_HOME"
        Me.CheckBox_KOTLIN_HOME.Size = New System.Drawing.Size(103, 17)
        Me.CheckBox_KOTLIN_HOME.TabIndex = 22
        Me.CheckBox_KOTLIN_HOME.Text = "KOTLIN_HOME"
        Me.CheckBox_KOTLIN_HOME.UseVisualStyleBackColor = True
        '
        'CheckBox_GRADLE_HOME
        '
        Me.CheckBox_GRADLE_HOME.AutoSize = True
        Me.CheckBox_GRADLE_HOME.Enabled = False
        Me.CheckBox_GRADLE_HOME.Location = New System.Drawing.Point(29, 138)
        Me.CheckBox_GRADLE_HOME.Name = "CheckBox_GRADLE_HOME"
        Me.CheckBox_GRADLE_HOME.Size = New System.Drawing.Size(108, 17)
        Me.CheckBox_GRADLE_HOME.TabIndex = 21
        Me.CheckBox_GRADLE_HOME.Text = "GRADLE_HOME"
        Me.CheckBox_GRADLE_HOME.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.RichTextBoxGenerate)
        Me.TabPage2.Controls.Add(Me.btn_Save)
        Me.TabPage2.Controls.Add(Me.TextBox1)
        Me.TabPage2.Controls.Add(Me.TextBox2)
        Me.TabPage2.Controls.Add(Me.btn_Generator)
        Me.TabPage2.Controls.Add(Me.btn_Open)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(566, 346)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "GenerateProject"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'RichTextBoxGenerate
        '
        Me.RichTextBoxGenerate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.RichTextBoxGenerate.Enabled = False
        Me.RichTextBoxGenerate.Location = New System.Drawing.Point(12, 90)
        Me.RichTextBoxGenerate.Name = "RichTextBoxGenerate"
        Me.RichTextBoxGenerate.Size = New System.Drawing.Size(542, 199)
        Me.RichTextBoxGenerate.TabIndex = 7
        Me.RichTextBoxGenerate.Text = ""
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.ListView2)
        Me.TabPage3.Controls.Add(Me.ComboBox1)
        Me.TabPage3.Controls.Add(Me.btn_Download)
        Me.TabPage3.Controls.Add(Me.Label1)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(566, 346)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "DependsOn"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'ListView2
        '
        Me.ListView2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ListView2.ContextMenuStrip = Me.ContextMenuStrip3
        Me.ListView2.HideSelection = False
        Me.ListView2.Location = New System.Drawing.Point(11, 56)
        Me.ListView2.Name = "ListView2"
        Me.ListView2.Size = New System.Drawing.Size(544, 279)
        Me.ListView2.TabIndex = 13
        Me.ListView2.UseCompatibleStateImageBehavior = False
        '
        'ContextMenuStrip3
        '
        Me.ContextMenuStrip3.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DownloadToolStripMenuItem1})
        Me.ContextMenuStrip3.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip3.Size = New System.Drawing.Size(129, 26)
        '
        'DownloadToolStripMenuItem1
        '
        Me.DownloadToolStripMenuItem1.Name = "DownloadToolStripMenuItem1"
        Me.DownloadToolStripMenuItem1.Size = New System.Drawing.Size(128, 22)
        Me.DownloadToolStripMenuItem1.Text = "Download"
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.Label7)
        Me.TabPage4.Controls.Add(Me.ComboBox3)
        Me.TabPage4.Controls.Add(Me.RichTextBox1)
        Me.TabPage4.Controls.Add(Me.btn_listener)
        Me.TabPage4.Controls.Add(Me.btn_Wrapper)
        Me.TabPage4.Controls.Add(Me.Label2)
        Me.TabPage4.Controls.Add(Me.ComboBox2)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(566, 346)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "WrapperCode"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(13, 54)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(54, 13)
        Me.Label7.TabIndex = 16
        Me.Label7.Text = "EventList:"
        '
        'TabPage5
        '
        Me.TabPage5.Controls.Add(Me.ListView1)
        Me.TabPage5.Location = New System.Drawing.Point(4, 22)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Size = New System.Drawing.Size(566, 346)
        Me.TabPage5.TabIndex = 4
        Me.TabPage5.Text = "Resource"
        Me.TabPage5.UseVisualStyleBackColor = True
        '
        'TabPage6
        '
        Me.TabPage6.Controls.Add(Me.RichTextBoxManifest)
        Me.TabPage6.Location = New System.Drawing.Point(4, 22)
        Me.TabPage6.Name = "TabPage6"
        Me.TabPage6.Size = New System.Drawing.Size(566, 346)
        Me.TabPage6.TabIndex = 7
        Me.TabPage6.Text = "ManifestEditor"
        Me.TabPage6.UseVisualStyleBackColor = True
        '
        'RichTextBoxManifest
        '
        Me.RichTextBoxManifest.ContextMenuStrip = Me.ContextMenuStrip4
        Me.RichTextBoxManifest.ForeColor = System.Drawing.Color.Gray
        Me.RichTextBoxManifest.Location = New System.Drawing.Point(3, 3)
        Me.RichTextBoxManifest.Name = "RichTextBoxManifest"
        Me.RichTextBoxManifest.ReadOnly = True
        Me.RichTextBoxManifest.Size = New System.Drawing.Size(557, 340)
        Me.RichTextBoxManifest.TabIndex = 13
        Me.RichTextBoxManifest.Text = ""
        '
        'ContextMenuStrip4
        '
        Me.ContextMenuStrip4.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CompileToolStripMenuItem1})
        Me.ContextMenuStrip4.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip4.Size = New System.Drawing.Size(120, 26)
        '
        'CompileToolStripMenuItem1
        '
        Me.CompileToolStripMenuItem1.Name = "CompileToolStripMenuItem1"
        Me.CompileToolStripMenuItem1.Size = New System.Drawing.Size(119, 22)
        Me.CompileToolStripMenuItem1.Text = "Compile"
        '
        'TabPage7
        '
        Me.TabPage7.Controls.Add(Me.Button3)
        Me.TabPage7.Controls.Add(Me.CheckBox2)
        Me.TabPage7.Controls.Add(Me.CheckBox1)
        Me.TabPage7.Controls.Add(Me.btn_Compile)
        Me.TabPage7.Controls.Add(Me.RichTextBoxCompile)
        Me.TabPage7.Controls.Add(Me.RichTextBox2)
        Me.TabPage7.Location = New System.Drawing.Point(4, 22)
        Me.TabPage7.Name = "TabPage7"
        Me.TabPage7.Size = New System.Drawing.Size(566, 346)
        Me.TabPage7.TabIndex = 6
        Me.TabPage7.Text = "Compile"
        Me.TabPage7.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(312, 304)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(107, 37)
        Me.Button3.TabIndex = 16
        Me.Button3.Text = "Compile(gradle)"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = True
        Me.CheckBox2.Location = New System.Drawing.Point(501, 315)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(61, 17)
        Me.CheckBox2.TabIndex = 15
        Me.CheckBox2.Text = "AutoFix"
        Me.CheckBox2.UseVisualStyleBackColor = True
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(10, 315)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(118, 17)
        Me.CheckBox1.TabIndex = 14
        Me.CheckBox1.Text = "Android X Migration"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'btn_Compile
        '
        Me.btn_Compile.Location = New System.Drawing.Point(144, 304)
        Me.btn_Compile.Name = "btn_Compile"
        Me.btn_Compile.Size = New System.Drawing.Size(107, 37)
        Me.btn_Compile.TabIndex = 13
        Me.btn_Compile.Text = "Compile(cmd)"
        Me.btn_Compile.UseVisualStyleBackColor = True
        '
        'RichTextBoxCompile
        '
        Me.RichTextBoxCompile.ContextMenuStrip = Me.ContextMenuStrip4
        Me.RichTextBoxCompile.Location = New System.Drawing.Point(5, 5)
        Me.RichTextBoxCompile.Name = "RichTextBoxCompile"
        Me.RichTextBoxCompile.ReadOnly = True
        Me.RichTextBoxCompile.Size = New System.Drawing.Size(557, 293)
        Me.RichTextBoxCompile.TabIndex = 12
        Me.RichTextBoxCompile.Text = ""
        '
        'RichTextBox2
        '
        Me.RichTextBox2.Location = New System.Drawing.Point(5, 5)
        Me.RichTextBox2.Name = "RichTextBox2"
        Me.RichTextBox2.Size = New System.Drawing.Size(558, 340)
        Me.RichTextBox2.TabIndex = 0
        Me.RichTextBox2.Text = ""
        '
        'CompileWorker1
        '
        '
        'GradleWorker
        '
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(581, 411)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.lbl_Status)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "B4X_WrapperHelper V1.5"
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ContextMenuStrip2.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        Me.ContextMenuStrip3.ResumeLayout(False)
        Me.TabPage4.ResumeLayout(False)
        Me.TabPage4.PerformLayout()
        Me.TabPage5.ResumeLayout(False)
        Me.TabPage6.ResumeLayout(False)
        Me.ContextMenuStrip4.ResumeLayout(False)
        Me.TabPage7.ResumeLayout(False)
        Me.TabPage7.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents btn_Open As Button
    Friend WithEvents btn_Save As Button
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents btn_Generator As Button
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents BackgroundWorker2 As System.ComponentModel.BackgroundWorker
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents ComboBox2 As ComboBox
    Friend WithEvents btn_Download As Button
    Friend WithEvents btn_Wrapper As Button
    Friend WithEvents CheckBox_MAVEN_HOME As CheckBox
    Friend WithEvents CheckBox_ANDROID_SDK_HOME As CheckBox
    Friend WithEvents CheckBox_JAVA_HOME As CheckBox
    Friend WithEvents FolderBrowserDialog1 As FolderBrowserDialog
    Friend WithEvents CheckBox_CLASSPATH As CheckBox
    Friend WithEvents ListView1 As ListView
    Friend WithEvents RichTextBox1 As RichTextBox
    Friend WithEvents ComboBox3 As ComboBox
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents AddToRjavaToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ContextMenuStrip2 As ContextMenuStrip
    Friend WithEvents WrapperMethodToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents btn_listener As Button
    Friend WithEvents btn_androidjar As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents btn_B4A As Button
    Friend WithEvents Label5 As Label
    Friend WithEvents txt_b4a As TextBox
    Friend WithEvents txt_androidjar As TextBox
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents lbl_Status As Label
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents CheckBox_GRADLE_HOME As CheckBox
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents CheckBox_KOTLIN_HOME As CheckBox
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents TabPage4 As TabPage
    Friend WithEvents Label7 As Label
    Friend WithEvents TabPage5 As TabPage
    Friend WithEvents ListView2 As ListView
    Friend WithEvents ContextMenuStrip3 As ContextMenuStrip
    Friend WithEvents DownloadToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents ContextMenuStrip4 As ContextMenuStrip
    Friend WithEvents CompileToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents CompileWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents GradleWorker As System.ComponentModel.BackgroundWorker
    Friend WithEvents lbl_KOTLIN_HOME As Label
    Friend WithEvents lbl_GRADLE_HOME As Label
    Friend WithEvents lbl_MAVEN_HOME As Label
    Friend WithEvents lbl_CLASSPATH As Label
    Friend WithEvents lbl_ANDROID_SDK_HOME As Label
    Friend WithEvents lbl_JAVA_HOME As Label
    Friend WithEvents TabPage7 As TabPage
    Friend WithEvents TabPage6 As TabPage
    Friend WithEvents Button3 As Button
    Friend WithEvents CheckBox2 As CheckBox
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents btn_Compile As Button
    Friend WithEvents RichTextBoxCompile As RichTextBox
    Friend WithEvents RichTextBox2 As RichTextBox
    Friend WithEvents RichTextBoxManifest As RichTextBox
    Friend WithEvents RichTextBoxGenerate As RichTextBox
End Class
