Imports MySql.Data.MySqlClient
Module Module1

    'xampp
    'Public conn As New MySqlConnection("host=localhost;port=3307;user=root;password='';database=payroll")
    'mysql
    Public conn As New MySqlConnection("host=127.0.0.1;user=root;password=T0oRd@t$Kr@Zy!;database=payroll")
    Public query As String
    Public cmd As MySqlCommand
    Public da As MySqlDataAdapter
    Public dt As DataTable
    Public dr As MySqlDataReader

End Module
