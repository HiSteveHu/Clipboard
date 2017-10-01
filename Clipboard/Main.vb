Public Class Main
    Public Declare Auto Function RegisterHotKey Lib "user32.dll" Alias "RegisterHotKey" (ByVal hwnd As IntPtr, ByVal id As Integer, ByVal fsModifiers As Integer, ByVal vk As Integer) As Boolean
    Public Declare Auto Function UnRegisterHotKey Lib "user32.dll" Alias "UnregisterHotKey" (ByVal hwnd As IntPtr, ByVal id As Integer) As Boolean
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        mainshow = True
        Button1.Hide()
        Label2.Text = "0" + " / " + Str(total)
        '注册全局热键 
        RegisterHotKey(Handle, 1, 2, Keys.Space)
        RegisterHotKey(Handle, 2, 6, Keys.C)
        '第3个参数意义： 0=nothing 1 -alt 2-ctrl 3-ctrl+alt 4-shift 5-alt+shift 6-ctrl+shift 7-ctrl+shift+alt
    End Sub
    Private Sub Form1_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        UnRegisterHotKey(Handle, 1)
        UnRegisterHotKey(Handle, 2)
    End Sub
    Protected Overrides Sub WndProc(ByRef m As Message)
        If m.Msg = 786 Then
            Select Case m.WParam.ToInt32 '判断热键消息的注册ID  
                Case 1
                    If mainshow = False Then
                        Me.Show()
                        mainshow = True
                    Else
                        Me.Hide()
                        mainshow = False
                    End If

                Case 2
                    cache = My.Computer.Clipboard.GetText
                    If num <= total And cache <> "" Then
                        data(num) = cache
                        ListBox1.Items.Add(cache)
                        Label2.Text = Str(num) + " / " + Str(total)
                        num = num + 1
                        Button1.Show()
                        If num > total Then
                            Button2.Enabled = False
                            ListBox1.AllowDrop = False
                            Label2.Text = "Array is full. Plaese clear the history"
                            info.Button3.Text = "Array is full. Plaese clear the history"
                            Me.Show()
                            info.Show()
                        ElseIf mainshow = False Then
                            info.Show()
                            mainshow = True
                        End If
                    End If
            End Select
        End If

        MyBase.WndProc(m)
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Help.ShowDialog()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Hide()
        mainshow = False
    End Sub

    Private Sub NotifyIcon1_DoubleClick(sender As Object, e As EventArgs) Handles NotifyIcon1.DoubleClick
        Me.Show()
    End Sub

    Private Sub NotifyIcon1_Click(sender As Object, e As EventArgs) Handles NotifyIcon1.Click
        Me.Show()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        UnRegisterHotKey(Handle, 1)
        UnRegisterHotKey(Handle, 2)
        Me.Dispose 
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        cache = My.Computer.Clipboard.GetText
        If num <= total And cache <> "" Then
            data(num) = cache
            ListBox1.Items.Add(cache)
            Label2.Text = Str(num) + " / " + Str(total)
            num = num + 1
            Button1.Show()
        End If
        If num > total Then
            Button2.Enabled = False
            ListBox1.AllowDrop = False
            Label2.Text = "Array is full. Plaese clear the history"
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        My.Computer.Clipboard.Clear()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        For i = 1 To total
            data(i) = ""
        Next
        ListBox1.Items.Clear()
        num = 1
        Label2.Text = "0" + " / " + Str(total)
        Button1.Hide()
        Button2.Enabled = True
        ListBox1.AllowDrop = True
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ListBox1.SelectedIndex >= 0 Then
            My.Computer.Clipboard.SetText(ListBox1.SelectedItem)
        End If
    End Sub

    Private Sub ClipboardToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClipboardToolStripMenuItem.Click
        Me.Show()
        mainshow = True
    End Sub

    Private Sub ListBox1_DragEnter(sender As Object, e As DragEventArgs) Handles ListBox1.DragEnter
        e.Effect = DragDropEffects.Copy

    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged

    End Sub

    Private Sub ListBox1_DragDrop(sender As Object, e As DragEventArgs) Handles ListBox1.DragDrop

        If num <= total Then
            Dim Files As String() = DirectCast(e.Data.GetData(DataFormats.FileDrop), String())
            For Each File As String In Files
                path = File
            Next
            ListBox1.Items.Add(path)
            data(num) = path
            Label2.Text = Str(num) + " / " + Str(total)
            num = num + 1
            Button1.Show()
            If num > total Then
                Button2.Enabled = False
                ListBox1.AllowDrop = False
                Label2.Text = "Array is full. Plaese clear the history"
            End If
        End If
    End Sub

    Private Sub Panel1_MouseMove(sender As Object, e As MouseEventArgs) Handles Panel1.MouseMove
        If X = e.X And Y = e.Y Then Exit Sub
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Me.Left = Me.Left + e.X - X
            Me.Top = Me.Top + e.Y - Y
        End If
        formx = Me.Location.X
        formy = Me.Location.Y
    End Sub

    Private Sub Panel1_MouseDown(sender As Object, e As MouseEventArgs) Handles Panel1.MouseDown
        X = e.X : Y = e.Y
    End Sub

    Private Sub Label1_MouseDown(sender As Object, e As MouseEventArgs) Handles Label1.MouseDown
        x = e.X : y = e.Y
    End Sub

    Private Sub Label1_MouseMove(sender As Object, e As MouseEventArgs) Handles Label1.MouseMove
        If x = e.X And y = e.Y Then Exit Sub
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Me.Left = Me.Left + e.X - x
            Me.Top = Me.Top + e.Y - y
        End If
        formx = Me.Location.X
        formy = Me.Location.Y
    End Sub

    Private Sub Main_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        If x = e.X And y = e.Y Then Exit Sub
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Me.Left = Me.Left + e.X - x
            Me.Top = Me.Top + e.Y - y
        End If
        formx = Me.Location.X
        formy = Me.Location.Y
    End Sub

    Private Sub Main_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        x = e.X : y = e.Y
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        UnRegisterHotKey(Handle, 1)
        UnRegisterHotKey(Handle, 2)
        Me.Dispose()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs)
        Feedback.ShowDialog()
    End Sub
End Class