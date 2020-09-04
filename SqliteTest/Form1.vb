Imports System.Globalization
Imports System.IO

Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.listView1.Items.Clear()
        Me.listView1.GridLines = True
        Me.listView1.View = View.Details
        Me.listView1.FullRowSelect = True
        Me.listView1.Columns.Add("ID", 30, HorizontalAlignment.Center)
        Me.listView1.Columns.Add("用户ID", 100, HorizontalAlignment.Left)
        Me.listView1.Columns.Add("用户名", 100, HorizontalAlignment.Left)
        Me.listView1.Columns.Add("密码", 100, HorizontalAlignment.Left)
        Me.listView1.Columns.Add("时间", listView1.Width - 30 - 300, HorizontalAlignment.Center)
    End Sub

    Private Sub button1_Click(sender As Object, e As EventArgs) Handles button1.Click
        If Not Me.button1.IsHandleCreated Then Return

        Dim tablevalue = New List(Of String())() From {
            New String() {"`Name` TEXT", "`EMAIL` TEXT", "`CODE` TEXT"},
            New String() {"`用户ID` TEXT", "`用户名` TEXT", "`密码` TEXT", "`时间` TEXT"}
        }
        CreateTable(New String() {"table1", "表2"}, tablevalue)

    End Sub

    Private Sub button5_Click(sender As Object, e As EventArgs) Handles button5.Click
        If CheckDataExsit("table1", "Name", "aaa") = False Then
            InsertData("table1", New String() {"Name", "EMAIL", "CODE"}, New String() {"aaa", "aaa@.com", "1234"})
            InsertData("table1", New String() {"Name", "EMAIL", "CODE"}, New String() {"bbb", "aaa@.com", "5678"})
            InsertData("table1", New String() {"Name", "EMAIL", "CODE"}, New String() {"ccc", "aaa@.com", "6666"})
        End If
        If CheckDataExsit("table1", "用户ID", "用户1") = False Then
            InsertData("表2", New String() {"用户ID", "用户名", "密码", "时间"}, New String() {"用户1", "网中行", "1234", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss tt", CultureInfo.InvariantCulture)})
        End If
        If CheckDataExsit("table1", "用户ID", "用户2") = False Then
            InsertData("表2", New String() {"用户ID", "用户名", "密码", "时间"}, New String() {"用户2", "小水", "8888", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss tt", CultureInfo.InvariantCulture)})
            InsertData("表2", New String() {"用户ID", "用户名", "密码", "时间"}, New String() {"用户3", "阿男达", "8888", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss tt", CultureInfo.InvariantCulture)})
        End If
    End Sub

    Private Sub button2_Click(sender As Object, e As EventArgs) Handles button2.Click
        Try
            Dim List As List(Of String) = ReadMultiData("table1", New String() {"Name", "EMAIL", "CODE"}, "Name like 'aaa'", "CODE like '1234'")
            If List.Count > 0 Then
                textBox1.Text = List(0)
                textBox2.Text = List(1)
                textBox3.Text = List(2)
            End If
            CheckImporlistview(Me.listView1, "表2", "")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub button3_Click(sender As Object, e As EventArgs) Handles button3.Click
        UpdateData("table1", "Name", "aaa", "EMAIL='修改@.com'", "CODE='修改'")
        Try
            Dim List As List(Of String) = ReadMultiData("table1", New String() {"Name", "EMAIL", "CODE"}, "Name like 'aaa'")
            If List.Count > 0 Then
                textBox1.Text = List(0)
                textBox2.Text = List(1)
                textBox3.Text = List(2)
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub button4_Click(sender As Object, e As EventArgs) Handles button4.Click
        If DeleteMultiData("表2", "用户名 Like '阿男达'", "密码 like '8888'") = True Then
            CheckImporlistview(Me.listView1, "表2", "")
        End If
    End Sub
End Class