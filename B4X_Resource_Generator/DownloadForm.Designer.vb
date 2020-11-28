<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DownloadForm
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DownloadForm))
        Me.labelSpeed = New System.Windows.Forms.Label()
        Me.groupBox1 = New System.Windows.Forms.GroupBox()
        Me.labelDownloaded = New System.Windows.Forms.Label()
        Me.labelPerc = New System.Windows.Forms.Label()
        Me.progressBar = New System.Windows.Forms.ProgressBar()
        Me.saveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.groupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'labelSpeed
        '
        Me.labelSpeed.Location = New System.Drawing.Point(170, 16)
        Me.labelSpeed.Name = "labelSpeed"
        Me.labelSpeed.Size = New System.Drawing.Size(137, 24)
        Me.labelSpeed.TabIndex = 2
        Me.labelSpeed.Text = "0 kb/s"
        Me.labelSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'groupBox1
        '
        Me.groupBox1.Controls.Add(Me.labelDownloaded)
        Me.groupBox1.Controls.Add(Me.labelSpeed)
        Me.groupBox1.Controls.Add(Me.labelPerc)
        Me.groupBox1.Controls.Add(Me.progressBar)
        Me.groupBox1.Location = New System.Drawing.Point(4, 2)
        Me.groupBox1.Name = "groupBox1"
        Me.groupBox1.Size = New System.Drawing.Size(320, 95)
        Me.groupBox1.TabIndex = 27
        Me.groupBox1.TabStop = False
        '
        'labelDownloaded
        '
        Me.labelDownloaded.Location = New System.Drawing.Point(72, 68)
        Me.labelDownloaded.Name = "labelDownloaded"
        Me.labelDownloaded.Size = New System.Drawing.Size(158, 24)
        Me.labelDownloaded.TabIndex = 3
        Me.labelDownloaded.Text = "0MB / 0MB"
        Me.labelDownloaded.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'labelPerc
        '
        Me.labelPerc.Location = New System.Drawing.Point(6, 16)
        Me.labelPerc.Name = "labelPerc"
        Me.labelPerc.Size = New System.Drawing.Size(158, 24)
        Me.labelPerc.TabIndex = 1
        Me.labelPerc.Text = "0 %"
        Me.labelPerc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'progressBar
        '
        Me.progressBar.Location = New System.Drawing.Point(6, 43)
        Me.progressBar.Name = "progressBar"
        Me.progressBar.Size = New System.Drawing.Size(308, 20)
        Me.progressBar.TabIndex = 0
        '
        'DownloadForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(332, 104)
        Me.Controls.Add(Me.groupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "DownloadForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Download"
        Me.TopMost = True
        Me.groupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents labelSpeed As Label
    Private WithEvents groupBox1 As GroupBox
    Private WithEvents labelDownloaded As Label
    Private WithEvents labelPerc As Label
    Private WithEvents progressBar As ProgressBar
    Private WithEvents saveFileDialog1 As SaveFileDialog
End Class
