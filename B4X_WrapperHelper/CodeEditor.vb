Public Class CodeEditor
    Private Sub CodeEditor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RichTextBox1.Text = CodeString
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        WrapperList.Add(RichTextBox1.Text)
        Me.Close()
    End Sub
End Class