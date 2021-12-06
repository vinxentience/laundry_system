Imports MySql.Data.MySqlClient
Module ModGlobalVariable
    Public servername As String
    Public databasename As String
    Public databaseuserid As String
    Public databaseuserpassword As String
    Public port As String
    Public conLaundry As New MySqlConnection
    Public strConnection As String
    Public command As New MySqlCommand
    Public sqlLaundryAdapter As New MySqlDataAdapter
    Public datLaundry As New DataTable
    Public row As Integer
    Public rid As Integer
    Public gid As Integer
    Public sid As Integer
End Module
