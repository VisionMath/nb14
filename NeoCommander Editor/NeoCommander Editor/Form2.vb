Public Class Form2

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

        Me.Hide()
        Form1.Show()

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Form1.min = Val(TextBox1.Text)
        Form1.max = Val(TextBox2.Text)

        Form1.expan(Form1.min, Form1.max)

        Me.Hide()
        Dispose()

        Form1.loadpos()
        Form1.Show()

    End Sub
End Class