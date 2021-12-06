Imports MySql.Data.MySqlClient

Public Class frmDisplayService
    Private Sub frmDisplayService_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        funcConnectToDB()
        prcDisplayAllService()
    End Sub

    Private Sub prcDisplayAllService()
        sqlLaundryAdapter = New MySqlDataAdapter
        datLaundry = New DataTable
        Try
            With command
                .Parameters.Clear()
                .CommandText = "prcDisplayService"
                .CommandType = CommandType.StoredProcedure
                sqlLaundryAdapter.SelectCommand = command
                datLaundry.Clear()
                sqlLaundryAdapter.Fill(datLaundry)
                lblTotalRecord.Text = "Total Record: " & datLaundry.Rows.Count.ToString

                If datLaundry.Rows.Count > 0 Then
                    dgvService.RowCount = datLaundry.Rows.Count
                    row = 0
                    While Not datLaundry.Rows.Count - 1 < row
                        With dgvService
                            .Rows(row).Cells(0).Value = datLaundry.Rows(row).Item("id").ToString
                            .Rows(row).Cells(1).Value = datLaundry.Rows(row).Item("service").ToString
                            .Rows(row).Cells(2).Value = datLaundry.Rows(row).Item("service_type").ToString
                            .Rows(row).Cells(3).Value = datLaundry.Rows(row).Item("service_cost").ToString
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

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        With frmService
            .txtService.Focus()
            .btnUpdate.Visible = False
            .btnSave.Visible = True
            .ShowDialog()
        End With
        prcDisplayAllService()
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        With frmService
            .txtService.Text = dgvService.CurrentRow.Cells(1).Value
            .cmbServiceType.Text = dgvService.CurrentRow.Cells(2).Value
            .txtCost.Text = dgvService.CurrentRow.Cells(3).Value
            sid = CInt(dgvService.CurrentRow.Cells(0).Value)
            .btnUpdate.Visible = True
            .btnSave.Visible = False
            .ShowDialog()
        End With
        prcDisplayAllService()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            With command
                .Parameters.Clear()
                .CommandText = "prcDeleteService"
                .CommandType = CommandType.StoredProcedure
                .Parameters.AddWithValue("@sid", CInt(dgvService.CurrentRow.Cells(0).Value))
                .ExecuteNonQuery()
            End With
            MessageBox.Show("Record Successfully Deleted")
            prcDisplayAllService()
        Catch ex As Exception
            MessageBox.Show("" + ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Application.Exit()
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        prcDisplayAutoCompleteService(cmbSearchBy.Text, txtSearch.Text)
    End Sub

    Private Sub prcDisplayAutoCompleteService(ByVal s_searchtype As String, ByVal s_search As String)
        sqlLaundryAdapter = New MySqlDataAdapter
        datLaundry = New DataTable
        Try
            With command
                .Parameters.Clear()
                .CommandText = "prcDisplayServiceFilter"
                .CommandType = CommandType.StoredProcedure
                .Parameters.AddWithValue("@s_searchtype", s_searchtype)
                .Parameters.AddWithValue("@s_search", s_search)
                sqlLaundryAdapter.SelectCommand = command
                datLaundry.Clear()
                sqlLaundryAdapter.Fill(datLaundry)
                If datLaundry.Rows.Count > 0 Then
                    dgvService.RowCount = datLaundry.Rows.Count
                    row = 0
                    While Not datLaundry.Rows.Count - 1 < row
                        With dgvService
                            .Rows(row).Cells(0).Value = datLaundry.Rows(row).Item("id").ToString
                            .Rows(row).Cells(1).Value = datLaundry.Rows(row).Item("service").ToString
                            .Rows(row).Cells(2).Value = datLaundry.Rows(row).Item("service_type").ToString
                            .Rows(row).Cells(3).Value = datLaundry.Rows(row).Item("service_cost").ToString
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
            prcDisplayAutoCompleteService(cmbSearchBy.Text, txtSearch.Text)
        End If
    End Sub
End Class