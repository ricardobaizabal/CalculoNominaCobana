Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Web

Public Class DataBase

    Private conn As New SqlConnection
    Public parms As String = String.Empty
    Public parmsC As String = String.Empty
    Private p_conexion As String = ""

    Sub New()
        conn = New SqlConnection(ConfigurationManager.ConnectionStrings("conn").ConnectionString)
    End Sub
    Public Sub OpenConnection()
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub CloseConnection()
        Try
            If conn.State <> ConnectionState.Closed Then
                conn.Close()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function ExecuteSP(ByVal spName As String, ByVal params As ArrayList) As DataTable
        Try

            Dim command As New SqlCommand
            command.CommandType = CommandType.StoredProcedure
            command.CommandText = spName
            command.CommandTimeout = 60
            command.Connection = conn

            parms = String.Empty
            parmsC = String.Empty

            For Each param As SqlParameter In params
                command.Parameters.Add(param)
                parms += param.ParameterName.ToString() + "='" + param.SqlValue.ToString() + "',"
            Next

            parmsC = parms

            'metodo de ejecución
            Dim ds As New DataSet 'objeto creado vacio
            Dim da As New SqlDataAdapter(command)

            da.Fill(ds)

            'asegurarnos de que se obtuvo una respuesta de la BD
            If ds.Tables.Count > 0 Then
                Return ds.Tables(0)
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ExecuteQuery(ByVal sql As String) As DataTable
        Try

            Dim command As New SqlCommand
            command.CommandType = CommandType.Text
            command.CommandText = sql
            command.CommandTimeout = 60
            command.Connection = conn

            Dim ds As New DataSet
            Dim da As New SqlDataAdapter(command)
            da.Fill(ds)

            If ds.Tables.Count > 0 Then
                Return ds.Tables(0)
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub ExecuteSPWithParams(ByVal spName As String, ByVal params As ArrayList)
        Try

            If conn.State = ConnectionState.Closed Then
                OpenConnection()
            End If

            Dim command As New SqlCommand
            command.CommandType = CommandType.StoredProcedure
            command.CommandText = spName
            command.CommandTimeout = 60
            command.Connection = conn

            parms = String.Empty
            parmsC = String.Empty

            For Each param As SqlParameter In params
                command.Parameters.Add(param)
                parms += param.ParameterName.ToString() + "='" + param.SqlValue.ToString() + "',"
            Next

            parmsC = parms

            command.ExecuteNonQuery()
            command.Parameters.Clear()
            command.Dispose()

        Catch ex As Exception
            Throw ex
        Finally
            CloseConnection()
        End Try
    End Sub
    Public Sub ExecuteNonQuery(ByVal sql As String)
        Try

            Dim command As New SqlCommand
            command.CommandType = CommandType.Text
            command.CommandText = sql
            command.CommandTimeout = 60
            command.Connection = conn

            command.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function ExecuteSPWithTransaction(ByVal spName As String, ByVal params As ArrayList) As DataTable
        Dim transaction As SqlTransaction = conn.BeginTransaction("SampleTransaction")
        Try

            Dim command As New SqlCommand
            command.CommandType = CommandType.StoredProcedure
            command.CommandText = spName
            command.CommandTimeout = 60
            command.Connection = conn
            For Each param As SqlParameter In params
                command.Parameters.Add(param)
            Next

            command.Transaction = transaction

            Dim ds As New DataSet
            Dim da As New SqlDataAdapter
            da.Fill(ds)
            transaction.Commit()

            If ds.Tables.Count > 0 Then
                Return ds.Tables(0)
            Else
                Return Nothing
            End If
        Catch ex As Exception
            transaction.Rollback()
            Return Nothing
        End Try
    End Function
    Public Function ExecuteSP_Insert_Transaction(ByVal ds As DataSet, ByVal spName As String, ByVal params As ArrayList) As Boolean
        Dim transaction As SqlTransaction = conn.BeginTransaction(IsolationLevel.Snapshot)
        Try

            Dim command As New SqlCommand
            command.CommandType = CommandType.StoredProcedure
            command.CommandText = spName
            command.CommandTimeout = 60
            command.Connection = conn

            For Each param As SqlParameter In params
                command.Parameters.Add(param)
            Next

            command.Transaction = transaction

            transaction.Commit()
            Return True
        Catch ex As Exception
            transaction.Rollback()
            Return False
        End Try
    End Function
    Public Function DSExecuteSP(ByVal spName As String, ByVal params As ArrayList) As DataSet
        Try

            Dim comando As SqlCommand = New SqlCommand()
            Dim adaptador As SqlDataAdapter = New SqlDataAdapter()
            Dim tabla As DataSet = New DataSet()

            If Not conn.State = System.Data.ConnectionState.Closed Then
                conn.Close()
            End If

            conn.Open()
            comando.Connection = conn
            comando.CommandTimeout = 600
            comando.CommandType = CommandType.StoredProcedure
            comando.CommandText = spName
            parms = String.Empty
            parmsC = String.Empty

            For i As Int32 = 0 To params.Count - 1
                comando.Parameters.Add(params(i))
                parms += params(i).ParameterName.ToString() + "=" + params(i).SqlValue.ToString() + ","
            Next

            parmsC = parms

            adaptador.SelectCommand = comando
            adaptador.Fill(tabla)
            comando.Parameters.Clear()
            adaptador.Dispose()
            comando.Dispose()
            Return tabla
        Catch ex As Exception
            Throw New Exception(" -CC.1: " + ex.Message.ToString())
        Finally
            conn.Close()
        End Try

    End Function
    Public Function ExecuteSPWithOutParam(ByVal spName As String, ByVal params As ArrayList, ByVal indexParamSalida As Int16) As Object
        Try
            Dim command As New SqlCommand
            command.CommandType = CommandType.StoredProcedure
            command.CommandText = spName
            command.CommandTimeout = 60
            command.Connection = conn
            For Each param As SqlParameter In params
                command.Parameters.Add(param)
            Next

            Dim ds As New DataSet
            Dim da As New SqlDataAdapter(command)
            da.Fill(ds)

            Dim obj As Object = command.Parameters.Item(indexParamSalida).Value

            Return obj
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ExecuteNonQueryScalar(ByVal spName As String, ByVal params As ArrayList) As Long
        Try

            If conn.State = ConnectionState.Closed Then
                OpenConnection()
            End If
            'crear comando
            Dim value As Long
            Dim command As New SqlCommand
            command.CommandType = CommandType.StoredProcedure
            command.CommandText = spName
            command.CommandTimeout = 60
            command.Connection = conn

            For Each param As SqlParameter In params
                command.Parameters.Add(param)
            Next

            value = command.ExecuteScalar()
            command.Parameters.Clear()
            command.Dispose()

            Return value
        Catch ex As Exception
            Throw ex
        Finally
            CloseConnection()
        End Try
    End Function
    Public Function ExecuteNonQueryScalarDecimal(ByVal spName As String, ByVal params As ArrayList) As Decimal
        Try

            If conn.State = ConnectionState.Closed Then
                OpenConnection()
            End If
            'crear comando
            Dim value As Decimal
            Dim command As New SqlCommand
            command.CommandType = CommandType.StoredProcedure
            command.CommandText = spName
            command.CommandTimeout = 60
            command.Connection = conn

            parms = String.Empty
            parmsC = String.Empty

            For i As Int32 = 0 To params.Count - 1
                command.Parameters.Add(params(i))
                parms += params(i).ParameterName.ToString() + "=" + params(i).SqlValue.ToString() + ","
            Next

            parmsC = parms

            value = command.ExecuteScalar()
            command.Parameters.Clear()
            command.Dispose()

            Return value
        Catch ex As Exception
            Throw ex
        Finally
            CloseConnection()
        End Try
    End Function
    Public Function ExecuteNonQueryScalarString(ByVal spName As String, ByVal params As ArrayList) As String
        Try

            If conn.State = ConnectionState.Closed Then
                OpenConnection()
            End If
            'crear comando
            Dim value As String
            Dim command As New SqlCommand
            command.CommandType = CommandType.StoredProcedure
            command.CommandText = spName
            command.CommandTimeout = 60
            command.Connection = conn

            For Each param As SqlParameter In params
                command.Parameters.Add(param)
            Next

            value = command.ExecuteScalar()
            command.Parameters.Clear()
            command.Dispose()

            Return value
        Catch ex As Exception
            Throw ex
        Finally
            CloseConnection()
        End Try
    End Function

End Class