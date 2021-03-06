Public Class frmCustomer

    Private Sub frmCustomer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        checkConnection()
        txtCustomerID.Focus()

    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            With command
                .Parameters.Clear()
                .CommandText = "prcInsertCustomer"
                .CommandType = CommandType.StoredProcedure
                .Parameters.AddWithValue("@v_custid", txtCustomerID.Text)
                .Parameters.AddWithValue("@v_fullname", txtFullname.Text)
                .Parameters.AddWithValue("@v_bdate", Format(dpBirth.Value, "yyyy-MM-dd"))
                .Parameters.AddWithValue("@v_gender", cmbGender.Text)
                .Parameters.AddWithValue("@v_contactno", txtContact.Text)
                .Parameters.AddWithValue("@v_address", txtAddress.Text)
                .Parameters.AddWithValue("@v_emailadd", txtEmail.Text)
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
        txtCustomerID.Clear()
        txtFullname.Clear()
        cmbGender.SelectedIndex = -1
        txtContact.Clear()
        txtAddress.Clear()
        txtEmail.Clear()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Dispose()
        btnUpdate.Visible = False
        btnSave.Visible = True
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Try
            With command
                .Parameters.Clear()
                .CommandText = "prcUpdateCustomer"
                .CommandType = CommandType.StoredProcedure
                .Parameters.AddWithValue("@cid", rid)
                .Parameters.AddWithValue("@v_custid", txtCustomerID.Text)
                .Parameters.AddWithValue("@v_fullname", txtFullname.Text)
                .Parameters.AddWithValue("@v_bdate", Format(dpBirth.Value, "yyyy-MM-dd"))
                .Parameters.AddWithValue("@v_gender", cmbGender.Text)
                .Parameters.AddWithValue("@v_contactno", txtContact.Text)
                .Parameters.AddWithValue("@v_address", txtAddress.Text)
                .Parameters.AddWithValue("@v_emailadd", txtEmail.Text)
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
