Public Class CodeEditor
    Private Sub CodeEditor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RichTextBox2.Text = ""
        RichTextBox1.Text = CodeString
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        WrapperList.Add(RichTextBox1.Text)
        Me.Close()
    End Sub

    Private Sub RichTextBox1_TextChanged(sender As Object, e As EventArgs) Handles RichTextBox1.TextChanged
        If RichTextBox1.Text.Contains("ActivityResult") Or RichTextBox1.Text.Contains("ActivityForResult") Or RichTextBox1.Text.Contains(".setOnClickListener") Then
            RichTextBox2.Text = My.Resources.onActivityResultTip
        Else
            '...
        End If
    End Sub
End Class