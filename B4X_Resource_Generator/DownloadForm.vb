Imports System.ComponentModel
Imports System.Net
Imports System.Windows.Forms

Public Class DownloadForm
    Private sw As New Stopwatch()

    Public Sub DownloadFile(ByVal urlAddress As String, ByVal location As String)
        Using WebClient = New WebClient()
            AddHandler WebClient.DownloadFileCompleted, AddressOf Completed
            AddHandler WebClient.DownloadProgressChanged, AddressOf ProgressChanged
            'Uri URL = urlAddress.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ? new Uri(urlAddress) : new Uri(urlAddress);
            Dim URL As New Uri(urlAddress)
            sw.Start()

            Try
                WebClient.DownloadFileAsync(URL, location)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End Using
    End Sub
    Private Sub ProgressChanged(ByVal sender As Object, ByVal e As DownloadProgressChangedEventArgs)
        labelSpeed.Text = String.Format("{0} kb/s", (e.BytesReceived / 1024.0R / sw.Elapsed.TotalSeconds).ToString("0.00"))
        progressBar.Value = e.ProgressPercentage
        labelPerc.Text = e.ProgressPercentage.ToString() & "%"
        labelDownloaded.Text = String.Format("{0} MB's / {1} MB's", (e.BytesReceived / 1024.0R / 1024.0R).ToString("0.00"), (e.TotalBytesToReceive / 1024.0R / 1024.0R).ToString("0.00"))
    End Sub
    Private Sub Completed(ByVal sender As Object, ByVal e As AsyncCompletedEventArgs)
        sw.Reset()
        If e.Cancelled = True Then
            'MessageBox.Show("下载被取消.");
        Else
            'MessageBox.Show(New Form With {.TopMost = True}, "下载完毕!")
            Me.Close()
        End If
    End Sub
    Private Sub DownloadForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If needSelect = False Then
            If targetPath = "" Then Me.Close()
            DownloadFile(downlink, targetPath)
        Else
            Dim sfd As New SaveFileDialog()
            sfd.Filter = "Zip Files|*.zip|All files|*.*"
            sfd.FilterIndex = 1
            sfd.RestoreDirectory = True
            sfd.FileName = "apache-maven.zip"
            If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                downloadPath = System.IO.Path.GetDirectoryName(sfd.FileName)
                DownloadFile("https://downloads.apache.org/maven/maven-3/3.6.3/binaries/apache-maven-3.6.3-bin.zip", sfd.FileName)
                Return
            Else
                Me.Close()
            End If
        End If
    End Sub

    Private Sub button1_Click(sender As Object, e As EventArgs)
        Dim sfd As New SaveFileDialog()
        sfd.Filter = "解压缩文件|*.rar|所有文件类型|*.*"
        sfd.FilterIndex = 1
        sfd.RestoreDirectory = True
        sfd.FileName = "dotNetFx452_Full_setup.exe"
        If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            downloadPath = System.IO.Path.GetDirectoryName(sfd.FileName)
            Dim filename As String = System.IO.Path.GetFileNameWithoutExtension(sfd.FileName)
            DownloadFile(downlink, sfd.FileName)
            Return
        End If
    End Sub


End Class