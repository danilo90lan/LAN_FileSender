Public Class Form1
    Dim WithEvents ReceiveSock As New Winsock2005DLL.Winsock
    Dim WithEvents SendSock As New Winsock2005DLL.Winsock

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            ReceiveSock.Listen(Val(TextBox1.Text))
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            SendSock.Connect(TextBox2.Text, Val(TextBox3.Text))
        Catch ex As Exception

        End Try

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        OpenFileDialog1.ShowDialog()
    End Sub

    Private Sub OpenFileDialog1_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk
        TextBox4.Text = OpenFileDialog1.FileName
    End Sub

    Private Sub ReceiveSock_ConnectionRequest(sender As Object, e As Winsock2005DLL.WinsockClientReceivedEventArgs) Handles ReceiveSock.ConnectionRequest
        ReceiveSock.Accept(e.Client)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        SendSock.Send(OpenFileDialog1.SafeFileName)
        SendSock.SendFile(OpenFileDialog1.FileName)
    End Sub
    Dim ReceivingFile As Boolean = False
    Dim Filename As String
    Private Sub ReceiveSock_DataArrival(sender As Object, e As Winsock2005DLL.WinsockDataArrivalEventArgs) Handles ReceiveSock.DataArrival
        If ReceivingFile = False Then
            ReceiveSock.Get(Filename)
        Else
            ReceiveSock.GetFile(InputBox("Enter the path of the received file...", "New File Received", My.Computer.FileSystem.SpecialDirectories.Desktop & "\" & Filename))
        End If
        If ReceivingFile = False Then
            ReceivingFile = True
        Else
            ReceivingFile = False
        End If
    End Sub

    Private Sub SendSock_SendProgress(sender As Object, e As Winsock2005DLL.WinsockSendEventArgs) Handles SendSock.SendProgress
        ProgressBar1.Maximum = e.BytesTotal
        ProgressBar1.Value = e.BytesSent
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
