Imports MySql.Data.MySqlClient

Public Class frmDisplayGarments

    Private Sub frmDisplayGarments_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        funcConnectToDB()
        prcDisplayAllGarments()
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        With frmGarments
            .txtDesc.Focus()
            .btnUpdate.Visible = False
            .btnSave.Visible = True
            .getID()
            .ShowDialog()
        End With
        prcDisplayAllGarments()
    End Sub

    Private Sub prcDisplayAllGarments()
        sqlLaundryAdapter = New MySqlDataAdapter
        datLaundry = New DataTable
        Try
            With command
                .Parameters.Clear()
                .CommandText = "prcDisplayGarment"
                .CommandType = CommandType.StoredProcedure
                sqlLaundryAdapter.SelectCommand = command
                datLaundry.Clear()
                sqlLaundryAdapter.Fill(datLaundry)
                lblTotalRecord.Text = "Total Record: " & datLaundry.Rows.Count.ToString

                If datLaundry.Rows.Count > 0 Then
                    dgvGarments.RowCount = datLaundry.Rows.Count
                    row = 0
                    While Not datLaundry.Rows.Count - 1 < row
                        With dgvGarments
                            .Rows(row).Cells(0).Value = datLaundry.Rows(row).Item("id").ToString
                            .Rows(row).Cells(1).Value = datLaundry.Rows(row).Item("description").ToString
                        End With
                        row = row + 1
                    End While
                Else
                    MessageBox.Show("No record available", "Record Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            End With
            sqlLaundryAdapter.Dispose()
            datLaundry.Dispose()
        Catch ex As Exception
            MessageBox.Show("" + ex.Message)
        End Try
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        With frmGarments
            .lblGarmentID.Text = "Selected ID: " + dgvGarments.CurrentRow.Cells(0).Value
            .txtDesc.Text = dgvGarments.CurrentRow.Cells(1).Value
            gid = CInt(dgvGarments.CurrentRow.Cells(0).Value)
            .btnUpdate.Visible = True
            .btnSave.Visible = False
            .ShowDialog()
        End With
        prcDisplayAllGarments()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            With command
                .Parameters.Clear()
                .CommandText = "prcDeleteGarment"
                .CommandType = CommandType.StoredProcedure
                .Parameters.AddWithValue("@gid", CInt(dgvGarments.CurrentRow.Cells(0).Value))
                .ExecuteNonQuery()
            End With
            MessageBox.Show("Record Successfully Deleted")
            prcDisplayAllGarments()
        Catch ex As Exception
            MessageBox.Show("" + ex.Message)
        End Try
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        prcDisplayAutoCompleteGarment(cmbSearchBy.Text, txtSearch.Text)
    End Sub

    Private Sub prcDisplayAutoCompleteGarment(ByVal g_searchtype As String, ByVal g_search As String)
        sqlLaundryAdapter = New MySqlDataAdapter
        datLaundry = New DataTable
        Try
            With command
                .Parameters.Clear()
                .CommandText = "prcDisplayGarmentFilter"
                .CommandType = CommandType.StoredProcedure
                .Parameters.AddWithValue("@g_searchtype", g_searchtype)
                .Parameters.AddWithValue("@g_search", g_search)
                sqlLaundryAdapter.SelectCommand = command
                datLaundry.Clear()
                sqlLaundryAdapter.Fill(datLaundry)
                If datLaundry.Rows.Count > 0 Then
                    dgvGarments.RowCount = datLaundry.Rows.Count
                    row = 0
                    While Not datLaundry.Rows.Count - 1 < row
                        With dgvGarments
                            .Rows(row).Cells(0).Value = datLaundry.Rows(row).Item("id").ToString
                            .Rows(row).Cells(1).Value = datLaundry.Rows(row).Item("description").ToString
                        End With
                        row = row + 1
                    End While
                Else
                    MessageBox.Show("No record available", "Record Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            End With
            sqlLaundryAdapter.Dispose()
            datLaundry.Dispose()
        Catch ex As Exception
            MessageBox.Show("" + ex.Message)
        End Try
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            btnSearch.Enabled = False
        Else
            btnSearch.Enabled = True
        End If
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        If CheckBox1.Checked = True Then
            prcDisplayAutoCompleteGarment(cmbSearchBy.Text, txtSearch.Text)
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Application.Exit()
    End Sub
End Class