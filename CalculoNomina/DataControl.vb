
Imports System.Data.SqlClient
Public Class DataControl
    Private p_conexion As String = ""

    Sub New()
        p_conexion = ConfigurationManager.ConnectionStrings("conn").ConnectionString
    End Sub

    Public Sub CatalogoId(ByVal combo As Web.UI.WebControls.DropDownList, ByVal ds As DataSet, ByVal sel As Integer, Optional ByVal todo As Boolean = False)
        Try
            combo.DataSource = ds
            combo.DataValueField = ds.Tables(0).Columns(0).ColumnName
            combo.DataTextField = ds.Tables(0).Columns(1).ColumnName
            combo.DataBind()

            If todo Then
                combo.Items.Insert(0, New ListItem("-- Ver todo --", "0"))
            Else
                combo.Items.Insert(0, New ListItem("-- Seleccione --", "0"))
            End If

            If sel > 0 Then
                combo.SelectedIndex = combo.Items.IndexOf(combo.Items.FindByValue(sel))
            End If

        Catch ex As Exception

        Finally
            '
        End Try

    End Sub

    Public Sub CatalogoId(ByVal combo As Web.UI.WebControls.DropDownList, ByVal sql As String, ByVal sel As Integer, Optional ByVal todo As Boolean = False)
        '
        Dim conn As New SqlConnection(p_conexion)
        conn.Open()
        Dim cmd As New SqlDataAdapter(sql, conn)
        Dim ds As DataSet = New DataSet
        cmd.Fill(ds)
        combo.DataSource = ds
        combo.DataValueField = ds.Tables(0).Columns(0).ColumnName
        combo.DataTextField = ds.Tables(0).Columns(1).ColumnName
        combo.DataBind()
        '
        If todo Then
            combo.Items.Insert(0, New ListItem("--Todos--", "0"))
        Else
            combo.Items.Insert(0, New ListItem("--Seleccione--", "0"))
        End If
        If sel > 0 Then
            combo.SelectedIndex = combo.Items.IndexOf(combo.Items.FindByValue(sel))
        End If
        conn.Close()
        conn.Dispose()
        conn = Nothing
        '
    End Sub

    Public Sub Catalogo(ByVal combo As Web.UI.WebControls.DropDownList, ByVal sel As String, ByVal dt As DataTable, Optional ByVal todo As Boolean = False, Optional ByVal sin As Boolean = False, Optional ByVal sintexto As String = "")
        Dim msg As String = ""
        Try
            combo.DataSource = dt
            combo.DataValueField = dt.Columns(0).ColumnName
            combo.DataTextField = dt.Columns(1).ColumnName
            combo.DataBind()

            If todo Then
                combo.Items.Insert(0, New ListItem("--Todos--", "0"))
            Else
                combo.Items.Insert(0, New ListItem("--Seleccione--", "0"))
            End If

            If sin = True Then
                combo.Items.Insert(1, New ListItem(sintexto, "-1"))
            End If
            '
            If sel.Length > 0 Then
                combo.SelectedIndex = combo.Items.IndexOf(combo.Items.FindByValue(sel))
            End If
        Catch ex As Exception
            msg = ex.Message.ToString
        End Try
    End Sub

    Public Sub CatalogoRad(ByVal combo As Telerik.Web.UI.RadComboBox, ByVal dt As DataTable, Optional ByVal textIni As Boolean = False, Optional ByVal todo As Boolean = False)

        combo.DataSource = dt
        combo.DataValueField = dt.Columns(0).ColumnName
        combo.DataTextField = dt.Columns(1).ColumnName
        combo.DataBind()

        If textIni Then
            'If todo Then
            '    combo.Items.Insert(0, New RadComboBoxItem("--Todos--", "0"))
            'Else
            '    combo.Items.Insert(0, New RadComboBoxItem("--Seleccione--", "0"))
            'End If
        End If

    End Sub

    Public Function getSHA1Hash(ByVal strToHash As String) As String

        Dim sha1Obj As New System.Security.Cryptography.SHA1CryptoServiceProvider

        Dim bytesToHash() As Byte = System.Text.Encoding.ASCII.GetBytes(strToHash)

        bytesToHash = sha1Obj.ComputeHash(bytesToHash)

        Dim strResult As String = ""

        For Each b As Byte In bytesToHash
            strResult += b.ToString("x2")
        Next

        Return strResult

    End Function

    Public Function FillDataSet(ByVal sql As String) As DataSet

        Dim conn As New SqlConnection(p_conexion)
        Dim cmd As New SqlDataAdapter(sql, conn)
        cmd.SelectCommand.CommandTimeout = 3600
        Dim ds As DataSet = New DataSet

        Try
            conn.Open()
            cmd.Fill(ds)

        Catch ex As Exception

        Finally
            conn.Close()
            conn.Dispose()
            conn = Nothing

        End Try

        Return ds

    End Function

    Public Function GetBytes(str As String) As Byte()
        Dim bytes As Byte() = New Byte(str.Length * 2 - 1) {}
        System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length)
        Return bytes
    End Function

    Public Function GetString(bytes As Byte()) As String
        Dim chars As Char() = New Char(bytes.Length / 2 - 1) {}
        System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length)
        Return New String(chars)
    End Function

    Public Function RunSQLScalarQueryString(ByVal SQL As String) As String
        Dim value As String = ""
        Dim conn As New SqlConnection(p_conexion)

        Try
            conn.Open()
            Dim cmd As New SqlCommand(SQL, conn)
            value = cmd.ExecuteScalar
        Catch ex As Exception
        Finally
            conn.Close()
            conn.Dispose()
            conn = Nothing
        End Try

        Return value

    End Function

End Class
