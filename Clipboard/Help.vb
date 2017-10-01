Imports System.Windows.Forms

Public Class Help
    Dim lastyear = 2017
    Private Sub Help_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label6.Text = "Ver. " + My.Application.Info.Version.ToString
        If lastyear < Now.Year Then
            Label7.Text = "© " + Now.Year.ToString + " Desktop Inc."
        Else
            Label7.Text = "© " + lastyear.ToString + " Desktop Inc."
        End If
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

    End Sub
End Class
