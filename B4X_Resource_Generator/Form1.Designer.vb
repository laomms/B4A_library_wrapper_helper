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
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.BackgroundWorker2 = New System.ComponentModel.BackgroundWorker()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ComboBox2 = New System.Windows.Forms.ComboBox()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.CheckBox_CLASSPATH = New System.Windows.Forms.CheckBox()
        Me.CheckBox_MAVEN_HOME = New System.Windows.Forms.CheckBox()
        Me.CheckBox_ANDROID_SDK_HOME = New System.Windows.Forms.CheckBox()
        Me.CheckBox_JAVA_HOME = New System.Windows.Forms.CheckBox()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.ComboBox3 = New System.Windows.Forms.ComboBox()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ContextMenuStrip2 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AddToRjavaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.WrapperMethodToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.ContextMenuStrip2.SuspendLayout()
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
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(429, 15)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(68, 28)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "Open"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(429, 52)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(68, 28)
        Me.Button2.TabIndex = 5
        Me.Button2.Text = "Save"
        Me.Button2.UseVisualStyleBackColor = True
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
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(205, 91)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(96, 40)
        Me.Button3.TabIndex = 6
        Me.Button3.Text = "Generator"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Button2)
        Me.GroupBox1.Controls.Add(Me.TextBox2)
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.TextBox1)
        Me.GroupBox1.Controls.Add(Me.Button3)
        Me.GroupBox1.Location = New System.Drawing.Point(2, 37)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(506, 143)
        Me.GroupBox1.TabIndex = 7
        Me.GroupBox1.TabStop = False
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(427, 88)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(68, 28)
        Me.Button4.TabIndex = 7
        Me.Button4.Text = "Pack"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'TextBox3
        '
        Me.TextBox3.ForeColor = System.Drawing.SystemColors.GrayText
        Me.TextBox3.Location = New System.Drawing.Point(83, 92)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(326, 20)
        Me.TextBox3.TabIndex = 6
        Me.TextBox3.Text = "Drag and drop or open or paste aar source folder"
        Me.TextBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
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
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(427, 13)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(68, 28)
        Me.Button5.TabIndex = 12
        Me.Button5.Text = "Download"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.Enabled = False
        Me.Button6.Location = New System.Drawing.Point(427, 49)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(68, 28)
        Me.Button6.TabIndex = 13
        Me.Button6.Text = "Wrapper"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'Button7
        '
        Me.Button7.Enabled = False
        Me.Button7.Location = New System.Drawing.Point(207, 329)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(96, 40)
        Me.Button7.TabIndex = 14
        Me.Button7.Text = "Compile"
        Me.Button7.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.Button4)
        Me.GroupBox2.Controls.Add(Me.Button6)
        Me.GroupBox2.Controls.Add(Me.TextBox3)
        Me.GroupBox2.Controls.Add(Me.Button5)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.ComboBox2)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.ComboBox1)
        Me.GroupBox2.Location = New System.Drawing.Point(4, 186)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(503, 124)
        Me.GroupBox2.TabIndex = 15
        Me.GroupBox2.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 95)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(54, 13)
        Me.Label3.TabIndex = 14
        Me.Label3.Text = "Pack Aar:"
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
        Me.ListView1.Location = New System.Drawing.Point(520, 12)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(416, 168)
        Me.ListView1.TabIndex = 0
        Me.ListView1.UseCompatibleStateImageBehavior = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.RichTextBox1)
        Me.GroupBox3.Controls.Add(Me.ComboBox3)
        Me.GroupBox3.Location = New System.Drawing.Point(516, 186)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(426, 182)
        Me.GroupBox3.TabIndex = 21
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Event List"
        '
        'RichTextBox1
        '
        Me.RichTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.RichTextBox1.ContextMenuStrip = Me.ContextMenuStrip2
        Me.RichTextBox1.Location = New System.Drawing.Point(4, 42)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.Size = New System.Drawing.Size(416, 139)
        Me.RichTextBox1.TabIndex = 1
        Me.RichTextBox1.Text = ""
        '
        'ComboBox3
        '
        Me.ComboBox3.FormattingEnabled = True
        Me.ComboBox3.Location = New System.Drawing.Point(4, 16)
        Me.ComboBox3.Name = "ComboBox3"
        Me.ComboBox3.Size = New System.Drawing.Size(416, 21)
        Me.ComboBox3.TabIndex = 0
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddToRjavaToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(145, 26)
        '
        'ContextMenuStrip2
        '
        Me.ContextMenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.WrapperMethodToolStripMenuItem})
        Me.ContextMenuStrip2.Name = "ContextMenuStrip2"
        Me.ContextMenuStrip2.Size = New System.Drawing.Size(162, 26)
        '
        'AddToRjavaToolStripMenuItem
        '
        Me.AddToRjavaToolStripMenuItem.Name = "AddToRjavaToolStripMenuItem"
        Me.AddToRjavaToolStripMenuItem.Size = New System.Drawing.Size(144, 22)
        Me.AddToRjavaToolStripMenuItem.Text = "Add to R.java"
        '
        'WrapperMethodToolStripMenuItem
        '
        Me.WrapperMethodToolStripMenuItem.Name = "WrapperMethodToolStripMenuItem"
        Me.WrapperMethodToolStripMenuItem.Size = New System.Drawing.Size(161, 22)
        Me.WrapperMethodToolStripMenuItem.Text = "WrapperMethod"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(954, 379)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.CheckBox_CLASSPATH)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.CheckBox_MAVEN_HOME)
        Me.Controls.Add(Me.Button7)
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
        Me.GroupBox3.ResumeLayout(False)
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ContextMenuStrip2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents Button3 As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents BackgroundWorker2 As System.ComponentModel.BackgroundWorker
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Button4 As Button
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents ComboBox2 As ComboBox
    Friend WithEvents Button5 As Button
    Friend WithEvents Button6 As Button
    Friend WithEvents Button7 As Button
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Label3 As Label
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
End Class
