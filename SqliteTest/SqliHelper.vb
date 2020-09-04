Imports System.Data.SQLite
Imports System.Text
Imports System.Threading
Imports System.Windows.Forms

Module SqliHelper

    Public ConnectionString As String = "Data Source=" & Application.StartupPath & "\test.db"
    Public Sub CheckImporlistview(ByVal ListView1 As ListView, ByVal tableName As String, ByVal condition As String)

        ListView1.Items.Clear()
        Dim ITM As ListViewItem

        Using myconnection As New SQLiteConnection(ConnectionString)
            myconnection.Open()
            Using mytransaction As SQLiteTransaction = myconnection.BeginTransaction(IsolationLevel.Serializable)
                Try
                    Using mycommand As New SQLiteCommand(myconnection)
                        mycommand.CommandText = "Select * from " & tableName & condition
                        Dim i As Integer = 0
                        Using dr As SQLiteDataReader = mycommand.ExecuteReader()
                            While dr.Read()
                                i = i + 1
                                ITM = ListView1.Items.Add(i.ToString)
                                For n As Integer = 1 To dr.FieldCount - 1
                                    If dr.GetString(n) = "" Then
                                        ITM.SubItems.Add(dr.GetString(n))
                                    ElseIf dr.GetString(n).GetType Is GetType(System.DateTime) Then
                                        ITM.SubItems.Add(Format(Convert.ToDateTime(dr.GetString(n)), "dd/MM/yyyy"))
                                    ElseIf dr.GetString(n).GetType Is GetType(System.String) Then
                                        ITM.SubItems.Add(dr.GetString(n))
                                    ElseIf dr.GetString(n).GetType Is GetType(System.Int32) Then
                                        ITM.SubItems.Add(dr.GetString(n))
                                    ElseIf dr.GetString(n).GetType Is GetType(System.Double) Then
                                        ITM.SubItems.Add(Format(Val(dr.GetString(n)), "######,###,##0.00"))
                                    ElseIf dr.GetString(n).GetType Is GetType(System.Decimal) Then
                                        ITM.SubItems.Add(Format(Val(dr.GetString(n)), "######,###,##0.00"))
                                    Else
                                        ITM.SubItems.Add(dr.GetString(n))
                                    End If
                                Next
                            End While
                            dr.Close()
                        End Using
                        mycommand.Dispose()
                    End Using
                    mytransaction.Commit()
                Catch ex As SQLiteException
                    Debug.Print("Commit Exception Type: {0}", ex.GetType())
                    Debug.Print("  Message: {0}", ex.Message)
                    Try
                        mytransaction.Rollback()
                    Catch ex2 As Exception
                        Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType())
                        Console.WriteLine("  Message: {0}", ex2.Message)
                    End Try
                End Try
            End Using
            If myconnection.State <> System.Data.ConnectionState.Closed Then
                myconnection.Close()
            End If
            myconnection.Dispose()
        End Using

        'For i As Integer = 0 To ListView1.Columns.Count - 1
        '    ListView1.Columns(i).Width = -2
        'Next i
        'ListView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
    End Sub

    Public Function CreateTable(ByVal KeyNames() As String, ByVal Values As List(Of String())) As Boolean
        Using myconnection As New SQLiteConnection(ConnectionString)
            myconnection.Open()
            Using mytransaction As SQLiteTransaction = myconnection.BeginTransaction(IsolationLevel.Serializable)
                Try
                    Using mycommand As New SQLiteCommand(myconnection)
                        For i As Integer = 0 To KeyNames.Count() - 1
                            mycommand.CommandText = "CREATE TABLE IF NOT EXISTS `" & KeyNames(i) & "` (ID INTEGER PRIMARY KEY AUTOINCREMENT, " & String.Join(",", Values(i)) & ")"
                            mycommand.ExecuteNonQuery()
                        Next i
                    End Using
                    mytransaction.Commit()
                Catch ex As SQLiteException
                    Debug.Print("Commit Exception Type: {0}", ex.GetType())
                    Debug.Print("  Message: {0}", ex.Message)
                    Try
                        mytransaction.Rollback()
                    Catch ex2 As Exception
                        Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType())
                        Console.WriteLine("  Message: {0}", ex2.Message)
                    End Try
                End Try
            End Using
        End Using
        Return False
    End Function

    'CheckDataExsit("KeyStore", "Keys", RetKey) = True
    Function CheckDataExsit(ByVal tableName As String, ByVal columnName As String, ByVal columnValue As String) As Boolean
        Using myconnection As New SQLiteConnection(ConnectionString)
            myconnection.Open()
            Using mytransaction As SQLiteTransaction = myconnection.BeginTransaction(IsolationLevel.Serializable)
                Try
                    Using mycommand As New SQLiteCommand(myconnection)
                        mycommand.CommandText = "Select * from " & tableName & " where " & columnName & " like '%" & columnValue & "'"
                        Using dr As SQLiteDataReader = mycommand.ExecuteReader()
                            While dr.Read()
                                Return True
                            End While
                        End Using
                    End Using
                    mytransaction.Commit()
                Catch ex As SQLiteException
                    Debug.Print("Commit Exception Type: {0}", ex.GetType())
                    Debug.Print("  Message: {0}", ex.Message)
                    Try
                        mytransaction.Rollback()
                    Catch ex2 As Exception
                        Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType())
                        Console.WriteLine("  Message: {0}", ex2.Message)
                    End Try
                End Try
            End Using
        End Using
        Return False
    End Function
    'InsertData("Activation", New String() {"QQGroup", "QQNumber", "MsgType"}, New String() {szGruopId, szQQId, "GroupMessage"})
    Public Sub InsertData(ByVal tableName As String, ByVal columnName() As String, ByVal columnValue() As String)
        Using myconnection As New SQLiteConnection(ConnectionString)
            myconnection.Open()
            Using mytransaction As SQLiteTransaction = myconnection.BeginTransaction(IsolationLevel.Serializable)
                Try
                    Using mycommand As New SQLiteCommand(myconnection)
                        mycommand.CommandText = "Insert Or Ignore Into " + tableName + "(" + String.Join(",", columnName) + ") VALUES('" + String.Join("','", columnValue) + "')"
                        mycommand.ExecuteNonQuery()
                    End Using
                    mytransaction.Commit()
                Catch ex As SQLiteException
                    Debug.Print("Commit Exception Type: {0}", ex.GetType())
                    Debug.Print("  Message: {0}", ex.Message)
                    Try
                        mytransaction.Rollback()
                    Catch ex2 As Exception
                        Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType())
                        Console.WriteLine("  Message: {0}", ex2.Message)
                    End Try
                End Try
            End Using
        End Using
    End Sub
    'InsertSingleData("AuthorizeAll", "QQ", m.Value)
    Sub InsertSingleData(ByVal tableName As String, ByVal columnName As String, ByVal columnValue As String)
        Using myconnection As New SQLiteConnection(ConnectionString)
            myconnection.Open()
            Using mytransaction As SQLiteTransaction = myconnection.BeginTransaction(IsolationLevel.Serializable)
                Try
                    Using mycommand As New SQLiteCommand(myconnection)
                        mycommand.CommandText = "Insert Or Ignore Into " + tableName + "(" + columnName + ") VALUES('" + columnValue + "')"
                        mycommand.ExecuteNonQuery()
                    End Using
                    mytransaction.Commit()
                Catch ex As SQLiteException
                    Debug.Print("Commit Exception Type: {0}", ex.GetType())
                    Debug.Print("  Message: {0}", ex.Message)
                    Try
                        mytransaction.Rollback()
                    Catch ex2 As Exception
                        Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType())
                        Console.WriteLine("  Message: {0}", ex2.Message)
                    End Try
                End Try
            End Using
        End Using
    End Sub
    'UpdateSingleData("Activation", "UserID", matchUserID.Value, Keys, KeysValue)
    Sub UpdateSingleData(ByVal tableName As String, ByVal itemName As String, ByVal itemValue As String, ByVal columnName As String, ByVal columnValue As String)
        Using myconnection As New SQLiteConnection(ConnectionString)
            myconnection.Open()
            Using mytransaction As SQLiteTransaction = myconnection.BeginTransaction(IsolationLevel.Serializable)
                Try
                    Using mycommand As New SQLiteCommand(myconnection)
                        mycommand.CommandText = "UPDATE " + tableName + " SET " + columnName + "='" + columnValue + "' WHERE " + itemName + " Like '" + itemValue + "'"
                        mycommand.ExecuteNonQuery()
                    End Using
                    mytransaction.Commit()
                Catch ex As SQLiteException
                    Debug.Print("Commit Exception Type: {0}", ex.GetType())
                    Debug.Print("  Message: {0}", ex.Message)
                    Try
                        mytransaction.Rollback()
                    Catch ex2 As Exception
                        Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType())
                        Console.WriteLine("  Message: {0}", ex2.Message)
                    End Try
                End Try
            End Using
        End Using
    End Sub
    'UpdateData("Activation", "UserID", matchUserID.Value, "QQGroup='" + szGruopId + "'", "QQNumber='" + szQQId+ "'", "MsgType='GroupMessage'")
    Sub UpdateData(ByVal tableName As String, ByVal itemName As String, ByVal itemValue As String, ByVal ParamArray columnAgr() As String)
        Using myconnection As New SQLiteConnection(ConnectionString)
            myconnection.Open()
            Using mytransaction As SQLiteTransaction = myconnection.BeginTransaction(IsolationLevel.Serializable)
                Try
                    Using mycommand As New SQLiteCommand(myconnection)
                        mycommand.CommandText = "UPDATE " + tableName + " SET " + String.Join(",", columnAgr) + " WHERE " + itemName + " like '" + itemValue + "'"
                        mycommand.ExecuteNonQuery()
                    End Using
                    mytransaction.Commit()
                Catch ex As SQLiteException
                    Debug.Print("Commit Exception Type: {0}", ex.GetType())
                    Debug.Print("  Message: {0}", ex.Message)
                    Try
                        mytransaction.Rollback()
                    Catch ex2 As Exception
                        Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType())
                        Console.WriteLine("  Message: {0}", ex2.Message)
                    End Try
                End Try
            End Using
        End Using
    End Sub
    'Dim ActivationList As List(Of String) = ReadMultiData("Activation", New String() {"UserID", "Keys", "KeyType", "ActType"}, "QQGroup like '" & QQgroup.ToString & "'", "QQNumber like '" & QQnumber.ToString & "'")
    Function ReadMultiData(ByVal tableName As String, ByVal columnSearch() As String, ByVal ParamArray columnAgr() As String) As List(Of String)
        Dim ItemList As New List(Of String)
        Using myconnection As New SQLiteConnection(ConnectionString)
            myconnection.Open()
            Using mytransaction As SQLiteTransaction = myconnection.BeginTransaction(IsolationLevel.Serializable)
                Try
                    Using mycommand As New SQLiteCommand(myconnection)
                        mycommand.CommandText = "Select * from " & tableName & " where " & String.Join(" AND ", columnAgr) '& " ORDER BY ID LIMIT 1 OFFSET 0"
                        Using dr As SQLiteDataReader = mycommand.ExecuteReader()
                            Dim dt As New DataTable()
                            dt.Load(dr)
                            For Each row As DataRow In dt.Rows
                                Dim szText As String = ""
                                For Each column As DataColumn In dt.Columns
                                    For Each columns As String In columnSearch
                                        If column.ColumnName = columns Then ItemList.Add(row(column).ToString().Replace(vbCr, ""))
                                    Next
                                Next
                            Next
                        End Using
                    End Using
                    mytransaction.Commit()
                Catch ex As SQLiteException
                    Debug.Print("Commit Exception Type: {0}", ex.GetType())
                    Debug.Print("  Message: {0}", ex.Message)
                    Try
                        mytransaction.Rollback()
                    Catch ex2 As Exception
                        Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType())
                        Console.WriteLine("  Message: {0}", ex2.Message)
                    End Try
                End Try
            End Using
        End Using
        Return ItemList
    End Function
    'Dim KeysList As List(Of String) = ReadMultiData2("KeyStore", "Keys", "Description like '%" & KeySign1 & "%'", "Description like '%" & KeySign2 & "%'", "Description like '%" & KeySign3 & "%'", "Description like '%" & KeySign4 & "%'")
    Function ReadMultiData2(ByVal tableName As String, ByVal columnSearch As String, ByVal ParamArray columnAgr() As String) As List(Of String)
        Dim ItemList As New List(Of String)
        Using myconnection As New SQLiteConnection(ConnectionString)
            myconnection.Open()
            Using mytransaction As SQLiteTransaction = myconnection.BeginTransaction(IsolationLevel.Serializable)
                Try
                    Using mycommand As New SQLiteCommand(myconnection)
                        mycommand.CommandText = "Select " & columnSearch & " from " & tableName & " where " & String.Join(" AND ", columnAgr) & " ORDER BY ID LIMIT 1"
                        Using dr As SQLiteDataReader = mycommand.ExecuteReader()
                            While dr.Read()
                                Debug.Print(dr("Keys").ToString.Replace(vbCr, ""))
                                ItemList.Add(dr(columnSearch).ToString.Replace(vbCr, ""))
                            End While
                            dr.Close()
                        End Using
                    End Using
                    mytransaction.Commit()
                Catch ex As SQLiteException
                    Debug.Print("Commit Exception Type: {0}", ex.GetType())
                    Debug.Print("  Message: {0}", ex.Message)
                    Try
                        mytransaction.Rollback()
                    Catch ex2 As Exception
                        Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType())
                        Console.WriteLine("  Message: {0}", ex2.Message)
                    End Try
                End Try
            End Using
        End Using
        Return ItemList
    End Function
    'Dim VersionList As List(Of String) = ReadCustomColumn("VersionList", "VersionID", "VersionName")
    Function ReadCustomColumn(ByVal tableName As String, ByVal ParamArray columnAgr() As String) As List(Of String)
        Dim ItemList As New List(Of String)
        Using myconnection As New SQLiteConnection(ConnectionString)
            myconnection.Open()
            Using mytransaction As SQLiteTransaction = myconnection.BeginTransaction(IsolationLevel.Serializable)
                Try
                    Using mycommand As New SQLiteCommand(myconnection)
                        mycommand.CommandText = "Select * from " & tableName
                        Using dr As SQLiteDataReader = mycommand.ExecuteReader()
                            Dim dt As New DataTable()
                            dt.Load(dr)
                            For Each row As DataRow In dt.Rows
                                Dim szText As String = ""
                                For Each column As DataColumn In dt.Columns
                                    For Each columns As String In columnAgr
                                        If column.ColumnName = columns Then szText = szText & " " & row(column).ToString().Replace(vbCr, "")
                                    Next
                                Next column
                                ItemList.Add(szText)
                            Next
                        End Using
                        mycommand.Dispose()
                    End Using
                    mytransaction.Commit()
                Catch ex As SQLiteException
                    Debug.Print("Commit Exception Type:  {0}", ex.GetType())
                    Debug.Print("  Message: {0}", ex.Message)
                    Try
                        mytransaction.Rollback()
                    Catch ex2 As Exception
                        Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType())
                        Console.WriteLine("  Message: {0}", ex2.Message)
                    End Try
                End Try
            End Using
        End Using
        Return ItemList
    End Function
    'Dim itemlist = ReadCustomColumn2("ErrorCodeList", "LOWER(Errorcode)", UCase(mcErrorCode.Value), "HRESULT", "Reson", "ResonCN")
    Function ReadCustomColumn2(ByVal tableName As String, ByVal itemName As String, ByVal itemValue As String, ByVal ParamArray columnAgr() As String) As List(Of String)
        Dim ItemList As New List(Of String)
        Using myconnection As New SQLiteConnection(ConnectionString)
            myconnection.Open()
            Using mytransaction As SQLiteTransaction = myconnection.BeginTransaction(IsolationLevel.Serializable)
                Try
                    Using mycommand As New SQLiteCommand(myconnection)
                        mycommand.CommandText = "Select * from " & tableName + " WHERE " + itemName + " like '" + itemValue + "'"
                        Using dr As SQLiteDataReader = mycommand.ExecuteReader()
                            Dim dt As New DataTable()
                            dt.Load(dr)
                            For Each row As DataRow In dt.Rows
                                Dim szText As String = ""
                                For Each column As DataColumn In dt.Columns
                                    For Each columns As String In columnAgr
                                        If column.ColumnName = columns Then ItemList.Add(row(column).ToString().Replace(vbCr, ""))
                                    Next
                                Next column
                            Next
                        End Using
                        mycommand.Dispose()
                    End Using
                    mytransaction.Commit()
                Catch ex As SQLiteException
                    Debug.Print("Commit Exception Type:  {0}", ex.GetType())
                    Debug.Print("  Message: {0}", ex.Message)
                    Try
                        mytransaction.Rollback()
                    Catch ex2 As Exception
                        Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType())
                        Console.WriteLine("  Message: {0}", ex2.Message)
                    End Try
                End Try
            End Using
        End Using
        Return ItemList
    End Function
    'Dim itemlist = ReadData("MSDNDownload", "VerNameCN", VerName, "DownUrl")
    Function ReadSingleData(ByVal tableName As String, ByVal columnName As String, ByVal columnValue As String, ByVal columnSearch As String) As List(Of String)
        Dim ItemList As New List(Of String)
        Using myconnection As New SQLiteConnection(ConnectionString)
            myconnection.Open()
            Using mytransaction As SQLiteTransaction = myconnection.BeginTransaction(IsolationLevel.Serializable)
                Try
                    Using mycommand As New SQLiteCommand(myconnection)
                        mycommand.CommandText = "Select " & columnSearch & " from " & tableName & " where " & columnName & " like '%" & columnValue & "%' "
                        Using dr As SQLiteDataReader = mycommand.ExecuteReader()
                            While dr.Read()
                                ItemList.Add(dr(0).ToString.Replace(vbCr, ""))
                            End While
                            dr.Close()
                        End Using
                        mycommand.Dispose()
                    End Using
                    mytransaction.Commit()
                Catch ex As SQLiteException
                    Debug.Print("Commit Exception Type: {0}", ex.GetType())
                    Debug.Print("  Message: {0}", ex.Message)
                    Try
                        mytransaction.Rollback()
                    Catch ex2 As Exception
                        Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType())
                        Console.WriteLine("  Message: {0}", ex2.Message)
                    End Try
                End Try
            End Using
        End Using
        Return ItemList
    End Function
    Function ReadData(ByVal tableName As String, ByVal columnName As String, ByVal columnValue As String, ByVal columnSearch As String) As List(Of String)
        Dim ItemList As New List(Of String)
        Using myconnection As New SQLiteConnection(ConnectionString)
            myconnection.Open()
            Using mytransaction As SQLiteTransaction = myconnection.BeginTransaction(IsolationLevel.Serializable)
                Try
                    Using mycommand As New SQLiteCommand(myconnection)
                        mycommand.CommandText = "Select VerNameCN,DownUrl from " & tableName & " where " & columnName & " like '%" & columnValue & "%' "
                        Using dr As SQLiteDataReader = mycommand.ExecuteReader()
                            While dr.Read()
                                ItemList.Add(dr(0).ToString.Replace(vbCr, "") & vbNewLine & dr(1).ToString.Replace(vbCr, "") & vbNewLine)
                            End While
                            dr.Close()
                        End Using
                        mycommand.Dispose()
                    End Using
                    mytransaction.Commit()
                Catch ex As SQLiteException
                    Debug.Print("Commit Exception Type: {0}", ex.GetType())
                    Debug.Print("  Message: {0}", ex.Message)
                    Try
                        mytransaction.Rollback()
                    Catch ex2 As Exception
                        Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType())
                        Console.WriteLine("  Message: {0}", ex2.Message)
                    End Try
                End Try
            End Using
        End Using
        Return ItemList
    End Function
    'ActivatorList = ReadAllColumn("Activation", "UserID", matchUserID.Value)
    Function ReadAllColumn(ByVal tableName As String, ByVal columnName As String, ByVal columnValue As String) As List(Of String)
        Dim ItemList As New List(Of String)
        Using myconnection As New SQLiteConnection(ConnectionString)
            myconnection.Open()
            Using mytransaction As SQLiteTransaction = myconnection.BeginTransaction(IsolationLevel.Serializable)
                Try
                    Using mycommand As New SQLiteCommand(myconnection)
                        mycommand.CommandText = "Select * from " & tableName & " where " & columnName & " like '%" & columnValue & "%' ORDER BY ID DESC LIMIT 1 OFFSET 0"
                        Using dr As SQLiteDataReader = mycommand.ExecuteReader()
                            Dim dt As New DataTable()
                            dt.Load(dr)
                            For Each row As DataRow In dt.Rows
                                For Each column As DataColumn In dt.Columns
                                    If column.ColumnName <> "ID" Then ItemList.Add(row(column).ToString().Replace(vbCr, ""))
                                Next column
                            Next
                        End Using
                        mycommand.Dispose()
                    End Using
                    mytransaction.Commit()
                Catch ex As SQLiteException
                    Debug.Print("Commit Exception Type: {0}", ex.GetType())
                    Debug.Print("  Message: {0}", ex.Message)
                    Try
                        mytransaction.Rollback()
                    Catch ex2 As Exception
                        Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType())
                        Console.WriteLine("  Message: {0}", ex2.Message)
                    End Try
                End Try
            End Using
        End Using
        Return ItemList
    End Function
    'DeleteData("KeyStore", "Keys", matchPartialKey.Value)
    Public Function DeleteData(ByVal tableName As String, ByVal columnName As String, ByVal columnValue As String) As Boolean                 '（数据库名，字段名，查找值）
        If columnValue = "" Then Return False
        Using myconnection As New SQLiteConnection(ConnectionString)
            myconnection.Open()
            Using mytransaction As SQLiteTransaction = myconnection.BeginTransaction(IsolationLevel.Serializable)
                Try
                    Using mycommand As New SQLiteCommand(myconnection)
                        mycommand.CommandText = "DELETE FROM " & tableName & " WHERE " & columnName & "  Like '%" & columnValue & "%' "
                        mycommand.ExecuteNonQuery()
                    End Using
                    mytransaction.Commit()
                Catch ex As SQLiteException
                    Debug.Print("Commit Exception Type: {0}", ex.GetType())
                    Debug.Print("  Message: {0}", ex.Message)
                    Try
                        mytransaction.Rollback()
                    Catch ex2 As Exception
                        Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType())
                        Console.WriteLine("  Message: {0}", ex2.Message)
                    End Try
                    Return False
                End Try
            End Using
            If myconnection.State <> System.Data.ConnectionState.Closed Then
                myconnection.Close()
            End If
            myconnection.Dispose()
        End Using
        Return True
    End Function
    'DeleteMultiData("Activation", "QQGroup Like '" & RetQQgroup & "'", "QQGroup like '" & RetQQnumber & "'")
    Public Function DeleteMultiData(ByVal tableName As String, ByVal ParamArray columnAgr() As String) As Boolean            '（数据库名，字段名，查找值）
        Using myconnection As New SQLiteConnection(ConnectionString)
            myconnection.Open()
            Using mytransaction As SQLiteTransaction = myconnection.BeginTransaction(IsolationLevel.Serializable)
                Try
                    Using mycommand As New SQLiteCommand(myconnection)
                        mycommand.CommandText = "DELETE FROM " & tableName & " WHERE " & String.Join(" AND ", columnAgr)
                        mycommand.ExecuteNonQuery()

                    End Using
                    mytransaction.Commit()
                Catch ex As SQLiteException
                    Debug.Print("Commit Exception Type: {0}", ex.GetType())
                    Debug.Print("  Message: {0}", ex.Message)
                    Try
                        mytransaction.Rollback()
                    Catch ex2 As Exception
                        Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType())
                        Console.WriteLine("  Message: {0}", ex2.Message)
                    End Try
                    Return False
                End Try
            End Using
            If myconnection.State <> System.Data.ConnectionState.Closed Then
                myconnection.Close()
            End If
            myconnection.Dispose()
        End Using
        Return True
    End Function

End Module
