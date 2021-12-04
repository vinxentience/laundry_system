Imports MySql.Data.MySqlClient
Public Class frmDisplayCustomer
    Private Sub frmDisplayCustomer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        funcConnectToDB()
        prcDisplayAllCustomer()
    End Sub

    'Private Sub prcDisplayAllCustomer()
    '    sqlLaundryAdapter = New MySqlDataAdapter
    '    datLaundry = New DataTable
    '    Try
    '        With command
    '            .Parameters.Clear()
    '            .CommandText = "SELECT * FROM tblcustomer ORDER BY fullname"
    '            .CommandType = CommandType.Text
    '            sqlLaundryAdapter.SelectCommand = command
    '            datLaundry.Clear()
    '            sqlLaundryAdapter.Fill(datLaundry)
    '            dgvCustomer.DataSource = datLaundry
    '            lblTotalRecord.Text = "Total Record: " & datLaundry.Rows.Count.ToString
    '        End With
    '    Catch ex As Exception
    '        MessageBox.Show("" + ex.Message)
    '    End Try
    'End Sub

    Private Sub prcDisplayAllCustomer()
        sqlLaundryAdapter = New MySqlDataAdapter
        datLaundry = New DataTable
        Try
            With command
                .Parameters.Clear()
                .CommandText = "prcDisplayCustomer"
                .CommandType = CommandType.StoredProcedure
                sqlLaundryAdapter.SelectCommand = command
                datLaundry.Clear()
                sqlLaundryAdapter.Fill(datLaundry)
                lblTotalRecord.Text = "Total Record: " & datLaundry.Rows.Count.ToString

                If datLaundry.Rows.Count > 0 Then
                    dgvCustomer.RowCount = datLaundry.Rows.Count
                    row = 0
                    While Not datLaundry.Rows.Count - 1 < row
                        With dgvCustomer
                            .Rows(row).Cells(0).Value = datLaundry.Rows(row).Item("id").ToString
                            .Rows(row).Cells(1).Value = datLaundry.Rows(row).Item("cust_id").ToString
                            .Rows(row).Cells(2).Value = datLaundry.Rows(row).Item("fullname").ToString
                            .Rows(row).Cells(3).Value = datLaundry.Rows(row).Item("address").ToString
                            .Rows(row).Cells(4).Value = datLaundry.Rows(row).Item("contactno").ToString
                            .Rows(row).Cells(5).Value = datLaundry.Rows(row).Item("birthdate").ToString
                            .Rows(row).Cells(6).Value = datLaundry.Rows(row).Item("gender").ToString
                            .Rows(row).Cells(7).Value = datLaundry.Rows(row).Item("email").ToString
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
        With frmCustomer
            .txtCustomerID.Focus()
            .btnUpdate.Visible = False
            .btnSave.Visible = True
            .ShowDialog()

        End With
        prcDisplayAllCustomer()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            With command
                .Parameters.Clear()
                .CommandText = "prcDeleteCustomer"
                .CommandType = CommandType.StoredProcedure
                .Parameters.AddWithValue("@cid", CInt(dgvCustomer.CurrentRow.Cells(0).Value))
                .ExecuteNonQuery()
            End With
            MessageBox.Show("Record Successfully Deleted")
            prcDisplayAllCustomer()
        Catch ex As Exception
            MessageBox.Show("" + ex.Message)
        End Try
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        With frmCustomer
            .txtCustomerID.Text = dgvCustomer.CurrentRow.Cells(1).Value
            .txtFullname.Text = dgvCustomer.CurrentRow.Cells(2).Value
            .txtAddress.Text = dgvCustomer.CurrentRow.Cells(3).Value
            .txtContact.Text = dgvCustomer.CurrentRow.Cells(4).Value
            .dpBirth.Value = dgvCustomer.CurrentRow.Cells(5).Value
            .cmbGender.Text = dgvCustomer.CurrentRow.Cells(6).Value
            .txtEmail.Text = dgvCustomer.CurrentRow.Cells(7).Value
            rid = CInt(dgvCustomer.CurrentRow.Cells(0).Value)
            .btnUpdate.Visible = True
            .btnSave.Visible = False
            .ShowDialog()

        End With
        prcDisplayAllCustomer()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        prcDisplayAutoCompleteCustomer(cmbSearchBy.Text, txtSearch.Text)
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            btnSearch.Enabled = False
        Else
            btnSearch.Enabled = True
        End If

    End Sub

    Private Sub prcDisplayAutoCompleteCustomer(ByVal v_searchtype As String, ByVal v_search As String)
        sqlLaundryAdapter = New MySqlDataAdapter
        datLaundry = New DataTable
        Try
            With command
                .Parameters.Clear()
                .CommandText = "prcDisplayCustomerFilter"
                .CommandType = CommandType.StoredProcedure
                .Parameters.AddWithValue("@v_searchtype", v_searchtype)
                .Parameters.AddWithValue("@v_search", v_search)
                sqlLaundryAdapter.SelectCommand = command
                datLaundry.Clear()
                sqlLaundryAdapter.Fill(datLaundry)
                If datLaundry.Rows.Count > 0 Then
                    dgvCustomer.RowCount = datLaundry.Rows.Count
                    row = 0
                    While Not datLaundry.Rows.Count - 1 < row
                        With dgvCustomer
                            .Rows(row).Cells(0).Value = datLaundry.Rows(row).Item("id").ToString
                            .Rows(row).Cells(1).Value = datLaundry.Rows(row).Item("cust_id").ToString
                            .Rows(row).Cells(2).Value = datLaundry.Rows(row).Item("fullname").ToString
                            .Rows(row).Cells(3).Value = datLaundry.Rows(row).Item("address").ToString
                            .Rows(row).Cells(4).Value = datLaundry.Rows(row).Item("contactno").ToString
                            .Rows(row).Cells(5).Value = datLaundry.Rows(row).Item("birthdate").ToString
                            .Rows(row).Cells(6).Value = datLaundry.Rows(row).Item("gender").ToString
                            .Rows(row).Cells(7).Value = datLaundry.Rows(row).Item("email").ToString
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

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        If CheckBox1.Checked = True Then
            prcDisplayAutoCompleteCustomer(cmbSearchBy.Text, txtSearch.Text)
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Application.Exit()
    End Sub
End Class