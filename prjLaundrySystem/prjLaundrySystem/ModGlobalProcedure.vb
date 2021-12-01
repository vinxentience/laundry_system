Imports MySql.Data.MySqlClient
Imports System.Data
Module ModGlobalProcedure
    Public Function funcConnectToDB() As Boolean
        Try
            servername = "localhost"
            databasename = "laundry"
            databaseuserid = "root"
            databaseuserpassword = "1234"
            port = "3306"

            If conLaundry.state = ConnectionState.Closed Then
                conLaundry = New MySqlConnection
                strConnection = "server= " & servername & ";" _
                                               & "port=" & port & ";" _
                                               & "database=" & databasename & ";" _
                                               & "username=" & databaseuserid & ";" _
                                               & "password=" & databaseuserpassword & ";" _
                                               & "connection timeout=" & 20 * 60
                conLaundry.ConnectionString = strConnection
                conLaundry.Open()
                command.Connection = conLaundry
                Return True
            Else
                conLaundry.Close()
                Return False
            End If

        Catch ex As Exception
            MessageBox.Show("" + ex.Message)
        End Try
        Return False
    End Function

    Public Sub checkConnection()
        If funcConnectToDB() = True Then
        Else
            conLaundry.Open()
        End If
    End Sub
End Module
