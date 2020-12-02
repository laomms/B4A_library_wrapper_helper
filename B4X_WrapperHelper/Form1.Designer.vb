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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.BackgroundWorker2 = New System.ComponentModel.BackgroundWorker()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ComboBox2 = New System.Windows.Forms.ComboBox()
        Me.btn_Download = New System.Windows.Forms.Button()
        Me.btn_Wrapper = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btn_listener = New System.Windows.Forms.Button()
        Me.CheckBox_CLASSPATH = New System.Windows.Forms.CheckBox()
        Me.CheckBox_MAVEN_HOME = New System.Windows.Forms.CheckBox()
        Me.CheckBox_ANDROID_SDK_HOME = New System.Windows.Forms.CheckBox()
        Me.CheckBox_JAVA_HOME = New System.Windows.Forms.CheckBox()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AddToRjavaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.ContextMenuStrip2 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.WrapperMethodToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ComboBox3 = New System.Windows.Forms.ComboBox()
        Me.btn_Compile = New System.Windows.Forms.Button()
        Me.btn_androidjar = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btn_B4A = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txt_b4a = New System.Windows.Forms.TextBox()
        Me.txt_androidjar = New System.Windows.Forms.TextBox()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.ContextMenuStrip2.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.SuspendLayout()
        '
        'TextBox1
        '
        Me.TextBox1.ForeColor = System.Drawing.SystemColors.GrayText
        Me.TextBox1.Location = New System.Drawing.Point(10, 19)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(401, 20)
        Me.TextBox1.TabIndex = 1
        Me.TextBox1.Text = "Drag and drop or open java project folder"
        Me.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btn_Open
        '
        Me.btn_Open.Location = New System.Drawing.Point(429, 15)
        Me.btn_Open.Name = "btn_Open"
        Me.btn_Open.Size = New System.Drawing.Size(68, 28)
        Me.btn_Open.TabIndex = 2
        Me.btn_Open.Text = "Open"
        Me.btn_Open.UseVisualStyleBackColor = True
        '
        'btn_Save
        '
        Me.btn_Save.Location = New System.Drawing.Point(429, 52)
        Me.btn_Save.Name = "btn_Save"
        Me.btn_Save.Size = New System.Drawing.Size(68, 28)
        Me.btn_Save.TabIndex = 5
        Me.btn_Save.Text = "Save"
        Me.btn_Save.UseVisualStyleBackColor = True
        '
        'TextBox2
        '
        Me.TextBox2.ForeColor = System.Drawing.SystemColors.GrayText
        Me.TextBox2.Location = New System.Drawing.Point(10, 55)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(401, 20)
        Me.TextBox2.TabIndex = 4
        Me.TextBox2.Text = "Select the directory to save"
        Me.TextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btn_Generator
        '
        Me.btn_Generator.Location = New System.Drawing.Point(205, 94)
        Me.btn_Generator.Name = "btn_Generator"
        Me.btn_Generator.Size = New System.Drawing.Size(96, 40)
        Me.btn_Generator.TabIndex = 6
        Me.btn_Generator.Text = "Generator"
        Me.btn_Generator.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btn_Save)
        Me.GroupBox1.Controls.Add(Me.TextBox2)
        Me.GroupBox1.Controls.Add(Me.btn_Open)
        Me.GroupBox1.Controls.Add(Me.TextBox1)
        Me.GroupBox1.Controls.Add(Me.btn_Generator)
        Me.GroupBox1.Location = New System.Drawing.Point(2, 37)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(506, 149)
        Me.GroupBox1.TabIndex = 7
        Me.GroupBox1.TabStop = False
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
        Me.ComboBox1.Location = New System.Drawing.Point(83, 17)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(326, 21)
        Me.ComboBox1.TabIndex = 8
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(5, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(67, 13)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "DependsOn:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(5, 56)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(67, 13)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "MainActivity:"
        '
        'ComboBox2
        '
        Me.ComboBox2.FormattingEnabled = True
        Me.ComboBox2.Location = New System.Drawing.Point(83, 53)
        Me.ComboBox2.Name = "ComboBox2"
        Me.ComboBox2.Size = New System.Drawing.Size(326, 21)
        Me.ComboBox2.TabIndex = 10
        '
        'btn_Download
        '
        Me.btn_Download.Location = New System.Drawing.Point(427, 13)
        Me.btn_Download.Name = "btn_Download"
        Me.btn_Download.Size = New System.Drawing.Size(68, 28)
        Me.btn_Download.TabIndex = 12
        Me.btn_Download.Text = "Download"
        Me.btn_Download.UseVisualStyleBackColor = True
        '
        'btn_Wrapper
        '
        Me.btn_Wrapper.Location = New System.Drawing.Point(203, 83)
        Me.btn_Wrapper.Name = "btn_Wrapper"
        Me.btn_Wrapper.Size = New System.Drawing.Size(96, 39)
        Me.btn_Wrapper.TabIndex = 13
        Me.btn_Wrapper.Text = "Wrapper"
        Me.btn_Wrapper.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btn_listener)
        Me.GroupBox2.Controls.Add(Me.btn_Download)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.btn_Wrapper)
        Me.GroupBox2.Controls.Add(Me.ComboBox2)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.ComboBox1)
        Me.GroupBox2.Location = New System.Drawing.Point(4, 186)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(503, 132)
        Me.GroupBox2.TabIndex = 15
        Me.GroupBox2.TabStop = False
        '
        'btn_listener
        '
        Me.btn_listener.Location = New System.Drawing.Point(427, 50)
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
        Me.CheckBox_CLASSPATH.Location = New System.Drawing.Point(286, 12)
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
        Me.CheckBox_MAVEN_HOME.Location = New System.Drawing.Point(405, 12)
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
        Me.CheckBox_ANDROID_SDK_HOME.Location = New System.Drawing.Point(120, 12)
        Me.CheckBox_ANDROID_SDK_HOME.Name = "CheckBox_ANDROID_SDK_HOME"
        Me.CheckBox_ANDROID_SDK_HOME.Size = New System.Drawing.Size(142, 17)
        Me.CheckBox_ANDROID_SDK_HOME.TabIndex = 1
        Me.CheckBox_ANDROID_SDK_HOME.Text = "ANDROID_SDK_HOME"
        Me.CheckBox_ANDROID_SDK_HOME.UseVisualStyleBackColor = True
        '
        'CheckBox_JAVA_HOME
        '
        Me.CheckBox_JAVA_HOME.AutoSize = True
        Me.CheckBox_JAVA_HOME.Enabled = False
        Me.CheckBox_JAVA_HOME.Location = New System.Drawing.Point(13, 12)
        Me.CheckBox_JAVA_HOME.Name = "CheckBox_JAVA_HOME"
        Me.CheckBox_JAVA_HOME.Size = New System.Drawing.Size(90, 17)
        Me.CheckBox_JAVA_HOME.TabIndex = 0
        Me.CheckBox_JAVA_HOME.Text = "JAVA_HOME"
        Me.CheckBox_JAVA_HOME.UseVisualStyleBackColor = True
        '
        'ListView1
        '
        Me.ListView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ListView1.ContextMenuStrip = Me.ContextMenuStrip1
        Me.ListView1.HideSelection = False
        Me.ListView1.Location = New System.Drawing.Point(7, 18)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(416, 162)
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
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.RichTextBox1)
        Me.GroupBox3.Controls.Add(Me.ComboBox3)
        Me.GroupBox3.Location = New System.Drawing.Point(516, 186)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(426, 318)
        Me.GroupBox3.TabIndex = 21
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Event List (Right click to modify the code)"
        '
        'RichTextBox1
        '
        Me.RichTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.RichTextBox1.ContextMenuStrip = Me.ContextMenuStrip2
        Me.RichTextBox1.Location = New System.Drawing.Point(4, 42)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.Size = New System.Drawing.Size(416, 268)
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
        Me.ComboBox3.Location = New System.Drawing.Point(4, 16)
        Me.ComboBox3.Name = "ComboBox3"
        Me.ComboBox3.Size = New System.Drawing.Size(416, 21)
        Me.ComboBox3.TabIndex = 0
        '
        'btn_Compile
        '
        Me.btn_Compile.Location = New System.Drawing.Point(200, 124)
        Me.btn_Compile.Name = "btn_Compile"
        Me.btn_Compile.Size = New System.Drawing.Size(96, 39)
        Me.btn_Compile.TabIndex = 22
        Me.btn_Compile.Text = "Compile"
        Me.btn_Compile.UseVisualStyleBackColor = True
        '
        'btn_androidjar
        '
        Me.btn_androidjar.Location = New System.Drawing.Point(424, 9)
        Me.btn_androidjar.Name = "btn_androidjar"
        Me.btn_androidjar.Size = New System.Drawing.Size(68, 28)
        Me.btn_androidjar.TabIndex = 25
        Me.btn_androidjar.Text = "Select"
        Me.btn_androidjar.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(13, 17)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(59, 13)
        Me.Label4.TabIndex = 24
        Me.Label4.Text = "android.jar:"
        '
        'btn_B4A
        '
        Me.btn_B4A.Location = New System.Drawing.Point(424, 46)
        Me.btn_B4A.Name = "btn_B4A"
        Me.btn_B4A.Size = New System.Drawing.Size(68, 28)
        Me.btn_B4A.TabIndex = 28
        Me.btn_B4A.Text = "Select"
        Me.btn_B4A.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(17, 55)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(50, 13)
        Me.Label5.TabIndex = 27
        Me.Label5.Text = "B4A.exe:"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.TextBox3)
        Me.GroupBox4.Controls.Add(Me.Button2)
        Me.GroupBox4.Controls.Add(Me.Label3)
        Me.GroupBox4.Controls.Add(Me.txt_b4a)
        Me.GroupBox4.Controls.Add(Me.txt_androidjar)
        Me.GroupBox4.Controls.Add(Me.btn_B4A)
        Me.GroupBox4.Controls.Add(Me.Label5)
        Me.GroupBox4.Controls.Add(Me.btn_androidjar)
        Me.GroupBox4.Controls.Add(Me.Label4)
        Me.GroupBox4.Controls.Add(Me.btn_Compile)
        Me.GroupBox4.Location = New System.Drawing.Point(7, 324)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(501, 180)
        Me.GroupBox4.TabIndex = 29
        Me.GroupBox4.TabStop = False
        '
        'TextBox3
        '
        Me.TextBox3.ForeColor = System.Drawing.SystemColors.GrayText
        Me.TextBox3.Location = New System.Drawing.Point(80, 86)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(326, 20)
        Me.TextBox3.TabIndex = 36
        Me.TextBox3.Text = "Please select kotlin compiler location"
        Me.TextBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(424, 84)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(68, 28)
        Me.Button2.TabIndex = 35
        Me.Button2.Text = "Select"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(25, 90)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(36, 13)
        Me.Label3.TabIndex = 34
        Me.Label3.Text = "Kotlin:"
        '
        'txt_b4a
        '
        Me.txt_b4a.ForeColor = System.Drawing.SystemColors.GrayText
        Me.txt_b4a.Location = New System.Drawing.Point(80, 50)
        Me.txt_b4a.Name = "txt_b4a"
        Me.txt_b4a.Size = New System.Drawing.Size(326, 20)
        Me.txt_b4a.TabIndex = 30
        Me.txt_b4a.Text = "Please select B4A executable file location"
        Me.txt_b4a.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txt_androidjar
        '
        Me.txt_androidjar.ForeColor = System.Drawing.SystemColors.GrayText
        Me.txt_androidjar.Location = New System.Drawing.Point(80, 14)
        Me.txt_androidjar.Name = "txt_androidjar"
        Me.txt_androidjar.Size = New System.Drawing.Size(326, 20)
        Me.txt_androidjar.TabIndex = 29
        Me.txt_androidjar.Text = "Please select android platforms"
        Me.txt_androidjar.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.ListView1)
        Me.GroupBox5.Location = New System.Drawing.Point(513, 0)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(428, 186)
        Me.GroupBox5.TabIndex = 30
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Right click to add resource ID"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(951, 511)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.CheckBox_CLASSPATH)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.CheckBox_MAVEN_HOME)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.CheckBox_JAVA_HOME)
        Me.Controls.Add(Me.CheckBox_ANDROID_SDK_HOME)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "B4X_WrapperHelper V1.4"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.ContextMenuStrip2.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents btn_Open As Button
    Friend WithEvents btn_Save As Button
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents btn_Generator As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents BackgroundWorker2 As System.ComponentModel.BackgroundWorker
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents ComboBox2 As ComboBox
    Friend WithEvents btn_Download As Button
    Friend WithEvents btn_Wrapper As Button
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents CheckBox_MAVEN_HOME As CheckBox
    Friend WithEvents CheckBox_ANDROID_SDK_HOME As CheckBox
    Friend WithEvents CheckBox_JAVA_HOME As CheckBox
    Friend WithEvents FolderBrowserDialog1 As FolderBrowserDialog
    Friend WithEvents CheckBox_CLASSPATH As CheckBox
    Friend WithEvents ListView1 As ListView
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents RichTextBox1 As RichTextBox
    Friend WithEvents ComboBox3 As ComboBox
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents AddToRjavaToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ContextMenuStrip2 As ContextMenuStrip
    Friend WithEvents WrapperMethodToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents btn_listener As Button
    Friend WithEvents btn_Compile As Button
    Friend WithEvents btn_androidjar As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents btn_B4A As Button
    Friend WithEvents Label5 As Label
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents txt_b4a As TextBox
    Friend WithEvents txt_androidjar As TextBox
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents Button2 As Button
    Friend WithEvents Label3 As Label
End Class
