Imports System.Collections.ObjectModel
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySql.Data.MySqlClient
Imports Mysqlx

Public Class hrsys
    Const SSS_RATE As Decimal = 0.045
    Const SSS_EMPLOYER_RATE As Decimal = 0.045
    Const PAGIBIG_RATE As Decimal = 0.01
    Const PAGIBIG_EMPLOYER_RATE As Decimal = 0.01
    Const PHILHEALTH_RATE As Decimal = 0.015
    Const PHILHEALTH_EMPLOYER_RATE As Decimal = 0.015

    Function CalculateSalary(position As String) As Decimal
        Dim salary As Decimal = 0

        Select Case position
            Case "CEO"
                salary = TextBoxSalary.Text
            Case "Director"
                salary = TextBoxSalary.Text
            Case "Manager"
                salary = TextBoxSalary.Text
            Case "Team Lead"
                salary = TextBoxSalary.Text
            Case "Regular"
                salary = TextBoxSalary.Text
            Case "Security"
                salary = TextBoxSalary.Text
            Case "Maintenance"
                salary = TextBoxSalary.Text
        End Select

        ' selected employee
        'Dim sssDeduction As Decimal = 0
        'Dim pagibigDeduction As Decimal = 0
        'Dim philhealthDeduction As Decimal = 0

        'If salary <= 35000 Then
        'sssDeduction = salary * SSS_RATE
        'End If

        'If salary <= 30000 Then
        'pagibigDeduction = salary * PAGIBIG_RATE
        'End If

        'If salary <= 80000 Then
        'philhealthDeduction = salary * PHILHEALTH_RATE
        'End If
        'Dim months As Integer = 12
        'Dim annualSalary = salary * months
        'Dim tax = CalculateTax(annualSalary) ' percent tax calcution based on salary

        'tax = tax / months ' divide back to months

        'Dim allDeductions As Decimal = tax + (sssDeduction + pagibigDeduction + philhealthDeduction)
        'Dim calculatedSalary = salary - allDeductions
        'Return calculatedSalary


        'all employee
        'Apply deductions For SSS, Pag - Ibig, And PhilHealth
        Dim sss As Decimal = salary * SSS_RATE
        Dim pagibig As Decimal = salary * PAGIBIG_RATE
        Dim philhealth As Decimal = salary * PHILHEALTH_RATE
        Dim months As Integer = 12
        Dim annualSalary = salary * months ' multiply to 12 months
        Dim phTax = sss + pagibig + philhealth

        Dim tax = CalculateTax(annualSalary) ' percent tax calcution based on salary

        tax = tax / months ' divide back to months

        ' add tax and sss, pagibig, philhealth

        Dim allDeductions = tax + phTax

        ' minus monthly salary to allDeductions
        Dim calculatedSalary = salary - allDeductions
        Return calculatedSalary
    End Function

    Function CalculateTax(income As Decimal) As Decimal
        ' Implement your tax calculation logic here
        ' Example tax brackets:
        ' 0 - 250,000: 0% tax
        ' 250,001 - 400,000: 15% tax
        ' 400,001 - 550,000: 20% tax
        ' ...

        ' Replace this with your own tax calculation logic
        If income <= 250000 Then
            Return 0
        ElseIf income <= 400000 Then
            Return income * 0.15
        ElseIf income <= 550000 Then
            Return income * 0.2
        ElseIf income <= 700000 Then
            Return income * 0.25
        ElseIf income <= 850000 Then
            Return income * 0.3
        ElseIf income <= 1000000 Then
            Return income * 0.35
        ElseIf income <= 1150000 Then
            Return income * 0.4
        ElseIf income <= 1300000 Then
            Return income * 0.45
        ElseIf income <= 1450000 Then
            Return income * 0.5
        ElseIf income <= 1600000 Then
            Return income * 0.55
        ElseIf income <= 1750000 Then
            Return income * 0.6
        ElseIf income <= 1900000 Then
            Return income * 0.65
        ElseIf income <= 2050000 Then
            Return income * 0.7
        ElseIf income <= 2200000 Then
            Return income * 0.75
        Else
            ' Add more tax brackets if needed
            ' ...
        End If
    End Function

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initialize the ComboBoxPosition control and add positions
        ComboBoxPosition.Items.AddRange({"CEO", "Director", "Manager", "Team Lead", "Regular", "Security", "Maintenance"})
    End Sub
    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            ' Uncheck other checkboxes
            CheckBox2.Checked = False
            CheckBox3.Checked = False

            ' Update combobox items
            ComboBoxPosition.Items.Clear()
            ComboBoxPosition.Items.Add("CEO")
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked Then
            ' Uncheck other checkboxes
            CheckBox1.Checked = False
            CheckBox3.Checked = False

            ' Update combobox items
            ComboBoxPosition.Items.Clear()
            ComboBoxPosition.Items.AddRange({"Director", "Manager", "Team Lead", "Regular"})
        End If
    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked Then
            ' Uncheck other checkboxes
            CheckBox1.Checked = False
            CheckBox2.Checked = False

            ' Update combobox items
            ComboBoxPosition.Items.Clear()
            ComboBoxPosition.Items.AddRange({"Security", "Maintenance"})
        End If
    End Sub
    Private Sub ComboBoxPosition_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxPosition.SelectedIndexChanged
        If ComboBoxPosition.SelectedItem IsNot Nothing Then
            ' Display the salary based on the selected position
            Select Case ComboBoxPosition.SelectedItem.ToString()
                Case "CEO"
                    TextBoxSalary.Text = "180000.00"
                Case "Director"
                    TextBoxSalary.Text = "120000.00"
                Case "Manager"
                    TextBoxSalary.Text = "60000.00"
                Case "Team Lead"
                    TextBoxSalary.Text = "40000.00"
                Case "Regular"
                    TextBoxSalary.Text = "25000.00"
                Case "Security"
                    TextBoxSalary.Text = "15000.00"
                Case "Maintenance"
                    TextBoxSalary.Text = "10000.00"
                Case Else
                    TextBoxSalary.Text = ""
            End Select
        End If
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        AddEmployee()
    End Sub
    Private Sub AddEmployee()
        query = "INSERT INTO employee (fname, lname, ID, position, salary, CalculatedSalary) VALUES (@fname, @lname, @ID, @position, @salary, @calculatedsalary)"
        cmd = New MySqlCommand
        cmd.Connection = conn
        Using command As New MySqlCommand(query, conn)
            ' Set parameter values from textboxes
            command.Parameters.AddWithValue("@fname", TextBoxFirstName.Text)
            command.Parameters.AddWithValue("@lname", TextBoxLastName.Text)
            command.Parameters.AddWithValue("@ID", TextBoxID.Text)
            command.Parameters.AddWithValue("@position", ComboBoxPosition.SelectedItem.ToString())

            ' Calculate the net salary
            Dim salary As Decimal = Decimal.Parse(TextBoxSalary.Text)
            Dim netSalary As Decimal = CalculateSalary(ComboBoxPosition.SelectedItem.ToString())

            command.Parameters.AddWithValue("@salary", salary)
            command.Parameters.AddWithValue("@calculatedsalary", netSalary)

            Try
                conn.Open()
                command.ExecuteNonQuery()
                MessageBox.Show("Employee inserted successfully.")

                ' Clear textboxes after successful insertion
                TextBoxFirstName.Clear()
                TextBoxLastName.Clear()
                TextBoxID.Clear()
                ComboBoxPosition.SelectedIndex = -1
                TextBoxSalary.Clear()
            Catch ex As Exception
                MessageBox.Show("Error: " + ex.Message)
            Finally
                conn.Close()
            End Try
        End Using
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        UpdateEmployee()
    End Sub

    Private Sub UpdateEmployee()
        cmd = New MySqlCommand
        cmd.Connection = conn
        query = "UPDATE employee SET fname = @fname, lname = @lname, position = @position, salary = @salary, CalculatedSalary = @calculatedsalary WHERE ID = @ID"
        Using command As New MySqlCommand(query, conn)
            ' Set parameter values from textboxes
            command.Parameters.AddWithValue("@fname", TextBoxFirstName.Text)
            command.Parameters.AddWithValue("@lname", TextBoxLastName.Text)
            command.Parameters.AddWithValue("@ID", TextBoxID.Text)
            command.Parameters.AddWithValue("@position", ComboBoxPosition.SelectedItem.ToString())

            ' Calculate the net salary
            Dim salary As Decimal = Decimal.Parse(TextBoxSalary.Text)
            Dim netSalary As Decimal = CalculateSalary(ComboBoxPosition.SelectedItem.ToString())

            command.Parameters.AddWithValue("@salary", salary)
            command.Parameters.AddWithValue("@calculatedsalary", netSalary)

            Try
                conn.Open()
                command.ExecuteNonQuery()
                MessageBox.Show("Employee updated successfully.")
                ' Clear textboxes after successful insertion
                TextBoxFirstName.Clear()
                TextBoxLastName.Clear()
                TextBoxID.Clear()
                ComboBoxPosition.SelectedIndex = -1
                TextBoxSalary.Clear()
            Catch ex As Exception
                MessageBox.Show("Error: " + ex.Message)
            Finally
                conn.Close()
            End Try
        End Using
    End Sub

    Private Sub CheckEmployee()
        cmd = New MySqlCommand
        cmd.Connection = conn
        query = "SELECT * FROM employee"
        Using command As New MySqlCommand(query, conn)
            Try
                conn.Open()
                Dim da As New MySqlDataAdapter(query, conn)

                'the MySqlDataAdapter here is used to retrieve data from database

                Dim dt As DataTable = New DataTable

                'the DataTable is a non-visible component to hold table data
                da.Fill(dt) 'this Fills the retrieve data from database to datatable
                DataGridView1.DataSource = dt
                'this set the datasource of datagridview1 as dt



            Catch ex As Exception
                ' Handle any errors that occur during the process
                MessageBox.Show("An error occurred: " + ex.Message)
            Finally
                ' Close the connection
                conn.Close()
            End Try
        End Using
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        CheckEmployee()
    End Sub

    Private Sub TextBoxFirstName_TextChanged(sender As Object, e As EventArgs) Handles TextBoxFirstName.TextChanged

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        DeleteEmployee()
    End Sub
    Private Sub DeleteEmployee()
        cmd = New MySqlCommand
        cmd.Connection = conn
        query = "DELETE FROM employee WHERE ID = @ID"
        Using command As New MySqlCommand(query, conn)
            ' Set parameter value from textbox
            command.Parameters.AddWithValue("@ID", TextBoxID.Text)

            Try
                conn.Open()
                command.ExecuteNonQuery()
                MessageBox.Show("Employee deleted successfully.")
                ' Clear textbox after successful deletion
                TextBoxID.Clear()
            Catch ex As Exception
                MessageBox.Show("Error: " + ex.Message)
            Finally
                conn.Close()
            End Try
        End Using
    End Sub

    Public Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
            ' Get the selected row
            Dim selectedRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)

            ' Assuming the data in the DataGridView is bound to a DataTable,
            ' you can access the cell values using the column index or column name.

            ' Retrieve the values from the selected row
            Dim firstName As String = selectedRow.Cells("fname").Value.ToString()
            Dim lastName As String = selectedRow.Cells("lname").Value.ToString()
            Dim id As Integer = Convert.ToInt32(selectedRow.Cells("id").Value)
            Dim position As String = selectedRow.Cells("position").Value.ToString()
            Dim salary As Decimal = Convert.ToDecimal(selectedRow.Cells("salary").Value)


            ' Set the values in the textboxes and combobox
            TextBoxFirstName.Text = firstName
            TextBoxLastName.Text = lastName
            TextBoxID.Text = id.ToString()
            ComboBoxPosition.SelectedItem = position
            TextBoxSalary.Text = salary.ToString()
        End If
    End Sub


End Class