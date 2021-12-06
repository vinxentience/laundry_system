Public Class frmService
    Private Sub frmService_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        checkConnection()
        txtService.Focus()
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            With command
                .Parameters.Clear()
                .CommandText = "prcInsertService"
                .CommandType = CommandType.StoredProcedure
                .Parameters.AddWithValue("@s_name", txtService.Text)
                .Parameters.AddWithValue("@s_type", cmbServiceType.Text)
                .Parameters.AddWithValue("@s_cost", txtCost.Text)
                .ExecuteNonQuery()
            End With
            MessageBox.Show("Records Successfully Saved", "Save Record", MessageBoxButtons.OK, MessageBoxIcon.Information)
            clearText()
            Me.Dispose()
        Catch ex As Exception
            MessageBox.Show("" + ex.Message)
        End Try
    End Sub

    Private Sub clearText()
        txtCost.Text = ""
        txtService.Text = ""
        cmbServiceType.SelectedIndex = -1
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Try
            With command
                .Parameters.Clear()
                .CommandText = "prcUpdateService"
                .CommandType = CommandType.StoredProcedure

                .Parameters.AddWithValue("@sid", sid)
                .Parameters.AddWithValue("@s_name", txtService.Text)
                .Parameters.AddWithValue("@s_type", cmbServiceType.Text)
                .Parameters.AddWithValue("@s_cost", txtCost.Text)
                .ExecuteNonQuery()
            End With
            MessageBox.Show("Records Successfully Updated", "Update Record", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Dispose()
            clearText()
        Catch ex As Exception
            MessageBox.Show("" + ex.Message)
        End Try
    End Sub
End Class