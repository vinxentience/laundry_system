Imports MySql.Data.MySqlClient

Public Class frmGarments
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            With command
                .Parameters.Clear()
                .CommandText = "prcInsertGarment"
                .CommandType = CommandType.StoredProcedure
                .Parameters.AddWithValue("@g_desc", txtDesc.Text)
                .ExecuteNonQuery()
            End With
            MessageBox.Show("Records Successfully Saved", "Save Record", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtDesc.Text = ""
            Me.Dispose()
        Catch ex As Exception
            MessageBox.Show("" + ex.Message)
        End Try
    End Sub

    Private Sub frmGarments_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        checkConnection()
        txtDesc.Focus()
    End Sub

    Public Sub getID()
        sqlLaundryAdapter = New MySqlDataAdapter
        datLaundry = New DataTable
        Try
            With command
                .Parameters.Clear()
                .CommandText = "prcGetGarmentID"
                .CommandType = CommandType.StoredProcedure
                sqlLaundryAdapter.SelectCommand = command
                datLaundry.Clear()
                sqlLaundryAdapter.Fill(datLaundry)
                lblGarmentID.Text = "Total Record: " & datLaundry.Rows(0).Item(0).ToString
            End With
            sqlLaundryAdapter.Dispose()
            datLaundry.Dispose()
        Catch ex As Exception
            MessageBox.Show("" + ex.Message)
        End Try
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Try
            With command
                .Parameters.Clear()
                .CommandText = "prcUpdateGarment"
                .CommandType = CommandType.StoredProcedure

                .Parameters.AddWithValue("@gid", gid)
                .Parameters.AddWithValue("@g_desc", txtDesc.Text)
                .ExecuteNonQuery()
            End With
            MessageBox.Show("Records Successfully Updated", "Update Record", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Dispose()
            txtDesc.Text = ""
        Catch ex As Exception
            MessageBox.Show("" + ex.Message)
        End Try
    End Sub
End Class