Imports Telerik.Web.UI
Imports Entities
Imports System.IO
Imports System.Data.SqlClient

Public Class AltaMasivaEmpleados
    Inherits System.Web.UI.Page

    Dim ErrorExcedente As String = ""
    Dim ErrorSueldosSalarios As String = ""
    Dim dt As New DataTable()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            dt.Columns.Add("clave_empleado", GetType(String))
            dt.Columns.Add("Error_excedente", GetType(String))
            dt.Columns.Add("Error_sueldos_salarios", GetType(String))
            dt.Columns.Add("Error_sexo", GetType(String))
            dt.Columns.Add("Error_cliente", GetType(String))
            dt.Columns.Add("Error_estadocivil", GetType(String))
            dt.Columns.Add("Error_estado", GetType(String))
            dt.Columns.Add("Error_municipio", GetType(String))
            dt.Columns.Add("Error_puesto", GetType(String))
            dt.Columns.Add("Error_periodo", GetType(String))
            ViewState("dt") = dt
        Else
            dt = CType(ViewState("dt"), DataTable)
        End If
    End Sub

    Protected Sub btnCargarEmpleados_Click(sender As Object, e As EventArgs) Handles btnCargarEmpleados.Click
        If ImportarFile.HasFile Then
            Try
                dt.Clear()
                ViewState("dt") = dt
                Dim filePath As String = Server.MapPath("~/UploadsMontos/") & ImportarFile.FileName
                ImportarFile.SaveAs(filePath)
                Using reader As New StreamReader(filePath)
                    Dim lines As String() = File.ReadAllLines(filePath, Encoding.UTF8)
                    Dim headers() As String = reader.ReadLine().Split(","c)
                    Dim i As Integer = 0
                    For Each line As String In lines
                        If i = 0 Then

                        Else
                            'line = reader.ReadLine()
                            Dim values() As String = line.Split(","c)
                            Dim clave As String = values(0).Trim()
                            Dim nombre As String = values(1).Trim()
                            Dim apellido_paterno As String = values(2).Trim()
                            Dim apellido_materno As String = values(3).Trim()
                            Dim sexo As String = values(4).Trim()
                            Dim rfc As String = values(5).Trim()
                            Dim curp As String = values(6).Trim()
                            Dim no_imss As String = values(7).Trim()
                            Dim cliente As String = values(8).Trim()
                            Dim estado_civil As String = values(9).Trim()
                            Dim pais As String = values(10).Trim()
                            Dim estado As String = values(11).Trim()
                            Dim municipio As String = values(12).Trim()
                            Dim codigo_postal As String = values(13).Trim()
                            Dim email As String = values(14).Trim()
                            Dim fecha_nacimiento As DateTime
                            Dim fecha_nacimiento_str As String = values(15).Trim()
                            Dim formatos As String() = {"dd/MM/yyyy", "MM/dd/yyyy", "yyyy-MM-dd"}
                            DateTime.TryParseExact(fecha_nacimiento_str, formatos, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, fecha_nacimiento)
                            Dim puesto As String = values(16).Trim()
                            Dim periodo As String = values(17).Trim()
                            Dim salario_base As String = values(18).Trim()
                            Dim salario_diario_integrado As String = values(19).Trim()
                            Dim sueldos_y_salarios As String = values(20).Trim()
                            Dim excedente As String = values(21).Trim()
                            Dim excedente_dinero As String = values(22).Trim()

                            excedente = excedente.Trim.ToLower()
                            sueldos_y_salarios = sueldos_y_salarios.Trim.ToLower()

                            If excedente = "si" Or excedente = "correcto" Or excedente = "sí" Then
                                excedente = 1
                            ElseIf excedente = "no" Or excedente = "falso" Then
                                excedente = 0
                            Else
                                ErrorExcedente = "Favor de colocar un valor válido en excendente, ejemplo: SI, NO"
                            End If

                            If sueldos_y_salarios = "si" Or sueldos_y_salarios = "correcto" Or sueldos_y_salarios = "sí" Then
                                sueldos_y_salarios = 1
                            ElseIf sueldos_y_salarios = "no" Or sueldos_y_salarios = "falso" Then
                                sueldos_y_salarios = 0
                            Else
                                ErrorSueldosSalarios = "Favor de colocar un valor válido en sueldos y salarios, ejemplo: SI, NO"
                            End If

                            AgregarEmpleado(clave, nombre, apellido_paterno, apellido_materno, rfc, curp, pais, estado, municipio, codigo_postal, email, sexo, fecha_nacimiento, estado_civil, cliente, sueldos_y_salarios, excedente, excedente_dinero, no_imss, puesto, periodo, salario_base, salario_diario_integrado)

                        End If
                        i += 1
                    Next
                End Using
            Catch ex As Exception
                Response.Write(ex.Message.ToString)
            End Try
        Else
            rwAlerta.RadAlert("Favor de subir el documento .csv", 330, 180, "Alerta", "", "")
        End If
    End Sub

    Private Sub AgregarEmpleado(clave As String, nombre As String, apellidoPaterno As String, apellidoMaterno As String, rfc As String, curp As String, pais As String, estado As String, municipio As String, codigo_postal As String, email As String, sexo As String, fecha_nacimiento As DateTime, estado_civil As String, cliente As String, sueldos_y_salarios As String, excedente As String, excedente_dinero As String, no_imss As String, puesto As String, periodo As String, salario_base As String, salario_diario_integrado As String)
        Dim conn As New SqlConnection(Session("conexion"))
        Dim sql As String = ""
        Dim objData As New DataControl(1)
        Dim errorFlag As Integer
        Try
            sql = "EXEC pPersonalAdministrado @cmd=27, @no_empleado='" & clave & "', @nombre='" & nombre & "', @apellido_paterno='" & apellidoPaterno & "', @apellido_materno='" & apellidoMaterno & "', @rfc='" & rfc & "', @curp='" & curp & "', @pais='" & pais & "', @estado_nombre='" & estado & "', @municipio='" & municipio & "', @codigo_postal='" & codigo_postal & "', @email1='" & email & "', @sexo='" & sexo & "', @fecha_nacimiento='" & fecha_nacimiento.ToString("yyyyMMdd") & "', @estado_civil='" & estado_civil & "',@userid='" & Session("usuarioid") & "', @cliente_nombre = '" & cliente & "', @sueldos_y_salarios='" & sueldos_y_salarios & "', @excedente='" & excedente & "', @excedente_dinero='" & excedente_dinero & "', @no_imss='" & no_imss & "', @periodo_nombre='" & periodo & "', @puesto_nombre='" & puesto & "', @salario_base='" & salario_base & "', @salario_diario_integrado=" & salario_diario_integrado
            Dim cmd As New SqlCommand(sql, conn)
            conn.Open()
            Dim rs As SqlDataReader = cmd.ExecuteReader()
            Mensaje_Resultado.Visible = True

            If rs.Read Then

                If Not IsDBNull(rs("error")) Then
                    errorFlag = Convert.ToInt32(rs("error"))
                Else
                    errorFlag = 0
                End If

                If errorFlag = 1 Or ErrorExcedente.Length > 0 Or ErrorSueldosSalarios.Length > 0 Then

                    'Dim dt As New DataTable()
                    dt.Rows.Add(
                        If(rs("clave_empleado") Is DBNull.Value, String.Empty, rs("clave_empleado").ToString()),
                        ErrorExcedente, ErrorSueldosSalarios,
                        If(rs("Error_sexo") Is DBNull.Value, String.Empty, rs("Error_sexo").ToString()),
                        If(rs("Error_cliente") Is DBNull.Value, String.Empty, rs("Error_cliente").ToString()),
                        If(rs("Error_estadocivil") Is DBNull.Value, String.Empty, rs("Error_estadocivil").ToString()),
                        If(rs("Error_estado") Is DBNull.Value, String.Empty, rs("Error_estado").ToString()),
                        If(rs("Error_municipio") Is DBNull.Value, String.Empty, rs("Error_municipio").ToString()),
                        If(rs("Error_puesto") Is DBNull.Value, String.Empty, rs("Error_puesto").ToString()),
                        If(rs("Error_periodo") Is DBNull.Value, String.Empty, rs("Error_periodo").ToString())
                    )
                    ViewState("dt") = dt
                    GridErrors.DataSource = dt
                    GridErrors.DataBind()
                    GridErrors.Visible = True
                    Mensaje_Resultado.Text = "No se insertaron estos datos, favor de corregir los errores:"
                Else
                    Mensaje_Resultado.Text = "Datos de empleados registrados correctamente."
                    Mensaje_Resultado.Font.Bold = True
                    GridErrors.Visible = False
                    dt.Clear()
                    ViewState("dt") = dt
                End If
                rs.Close()
            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        Finally
            conn.Close()
        End Try
    End Sub

    Protected Sub btnDescargarCSV_Click(sender As Object, e As EventArgs) Handles btnDescargarCSV.Click
        Dim csvContent As New StringBuilder()
        csvContent.Append("Clave Empleado,")
        csvContent.Append("Nombre,")
        csvContent.Append("Apellido Paterno,")
        csvContent.Append("Apellido Materno,")
        csvContent.Append("Sexo,")
        csvContent.Append("RFC,")
        csvContent.Append("Curp,")
        csvContent.Append("No IMSS,")
        csvContent.Append("Cliente,")
        csvContent.Append("Estado Civil,")
        csvContent.Append("Pais,")
        csvContent.Append("Estado,")
        csvContent.Append("Municipio,")
        csvContent.Append("Codigo Postal,")
        csvContent.Append("Email,")
        csvContent.Append("Fecha de Nacimiento,")
        csvContent.Append("Puesto de Trabajo,")
        csvContent.Append("Periodo de Pago,")
        csvContent.Append("Salario Base,")
        csvContent.Append("Salario Diario Integrado,")
        csvContent.Append("Sueldos y Salarios,")
        csvContent.Append("Excendente,")
        csvContent.Append("Cantidad Excedente,")
        csvContent.AppendLine()
        Response.Clear()
        Response.Buffer = True
        Response.BufferOutput = True
        Response.ContentType = "text/csv"
        Response.AddHeader("Content-Disposition", "attachment;filename=AltaMasivaEmpleados.csv")
        Response.Charset = ""
        Response.ContentEncoding = Encoding.UTF8
        Response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble())
        Response.Write(csvContent.ToString())
        Response.Flush()
        Response.End()
    End Sub
End Class
