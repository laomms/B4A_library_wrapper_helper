Public Class SelectForm
    Friend Shared MyInstance As SelectForm
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MaximizeBox = False
        MyInstance = Me
        ComboBox1.Items.Clear()

        For Each KeyPair As KeyValuePair(Of String, String) In ItemsDictionary
            ComboBox1.Items.Add(KeyPair.Key + " - " + KeyPair.Value.Replace("/artifact/", ""))
            ComboBox1.SelectedIndex = 0
        Next
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        SelectItem = ComboBox1.Text
        Me.Close()
    End Sub
End Class