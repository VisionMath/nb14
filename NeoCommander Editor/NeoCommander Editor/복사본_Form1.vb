Imports System
Imports System.IO

Public Class Form1

    Public header64() As Byte = {&H42, &H4D, &H36, &H40, &H0, &H0, &H0, &H0, &H0, &H0, &H36, &H0, &H0, &H0, &H28, &H0, &H0, &H0, &H40, &H0, &H0, &H0, &HC0, &HFF, &HFF, &HFF, &H1, &H0, &H20, &H0, &H0, &H0, &H0, &H0, &H0, &H40, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0}

    Private Sub Form_Start() Handles MyBase.Activated

    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

    End Sub


    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

        Dim rf As Stream
        Dim gt64(&H4035) As Byte
        Dim bit64 As Bitmap

        Array.ConstrainedCopy(header64, 0, gt64, 0, &H36)
        rf = File.Open("resfiles_01.bin", FileMode.Open)
        rf.Position = &H9CCC
        rf.Read(gt64, &H36, &H4000)
        rf.Close()

        Dim picmem As MemoryStream = New MemoryStream(gt64)

        bit64 = Bitmap.FromStream(picmem)


        PictureBox1.Image = bit64

    End Sub

    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim rf As Stream
        Dim filename As String

        filename = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) & "\test.txt"

        rf = File.Open(filename, FileMode.Open)
        rf.Position = 10

        For i = &H32 To &H36
            rf.WriteByte(i)
        Next

        rf.Flush()
        rf.Close()

    End Sub
End Class
