Imports System.IO

Public Class CodeEditor
    Private Sub CodeEditor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RichTextBox2.Text = ""
        RichTextBox1.Text = CodeString
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        WrapperList.Clear()
        WrapperList.Add(RichTextBox1.Text)
        If wrapperText <> "" Then
            If File.Exists(MainActivityPath + "\" + Path.GetFileName(ProjectPath) + "Wrapper.java") Then
                If Not WrapperList Is Nothing Then
                    If WrapperList.Count > 0 Then
                        wrapperText = wrapperText.Insert(wrapperText.trim.LastIndexOf("}") - 1, vbNewLine + String.Join(vbNewLine + vbNewLine, WrapperList) + vbNewLine)
                    End If
                End If
                File.WriteAllText(MainActivityPath + "\" + Path.GetFileName(ProjectPath) + "Wrapper.java", wrapperText)
            End If
        Else
            If File.Exists(MainActivityPath + "\" + Path.GetFileName(ProjectPath) + "Wrapper.java") Then
                wrapperText = File.ReadAllText(MainActivityPath + "\" + Path.GetFileName(ProjectPath) + "Wrapper.java")
                If Not WrapperList Is Nothing Then
                    If WrapperList.Count > 0 Then
                        wrapperText = wrapperText.Insert(wrapperText.trim.LastIndexOf("}") - 1, vbNewLine + String.Join(vbNewLine + vbNewLine, WrapperList) + vbNewLine)
                    End If
                End If
                File.WriteAllText(MainActivityPath + "\" + Path.GetFileName(ProjectPath) + "Wrapper.java", wrapperText)
            End If
        End If
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