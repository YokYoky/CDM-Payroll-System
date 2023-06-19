Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Windows
Imports MySql.Data.MySqlClient
Imports Mysqlx.XDevAPI.Common

Public Class Form1

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        cmd = New MySqlCommand
        cmd.Connection = conn
        query = "SELECT * FROM login where username='" & TextBox1.Text & "' and password='" & TextBox2.Text & "'"
        da = New MySqlDataAdapter(query, conn)
        dt = New DataTable()
        Dim hrysys As New hrsys()
        Try
            da.Fill(dt)
            If dt.Rows.Count <= 0 Then
                MsgBox("Login Failed !", vbInformation)
            Else

                hrsys.Show()
                'Me.Hide()
            End If
        Catch ex As Exception
            MessageBox.Show("ERROR : " & ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            conn.Open() 'here you open the conn object from module1
            MessageBox.Show("Connected.") 'if no error w/ try, show connected
        Catch ex As Exception 'if error w/ try, catch the exception
            MessageBox.Show(ex.ToString()) 'then show the error details
        End Try
        conn.Close() 'after trying, you close the connection.
    End Sub
End Class
