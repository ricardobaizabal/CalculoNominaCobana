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

                Dim lineErrors As New List(Of String)() ' Para almacenar errores de cada fila

                Using reader As New StreamReader(filePath)
                    Dim lines As String() = File.ReadAllLines(filePath, Encoding.UTF8)
                    Dim headers() As String = reader.ReadLine().Split(","c)
                    Dim i As Integer = 0
                    For Each line As String In lines
                        If i = 0 Then
                            ' Saltar encabezado
                        Else
                            Dim values() As String = line.Split(","c)

                            ' Validar campos requeridos
                            Dim errores As New List(Of String)()

                            Dim nombre As String = values(0).Trim()
                            Dim apellido_paterno As String = values(1).Trim()
                            Dim apellido_materno As String = values(2).Trim()
                            Dim sexo As String = values(3).Trim()
                            Dim fecha_nacimiento_str As String = values(4).Trim()
                            Dim rfc As String = values(5).Trim()
                            Dim curp As String = values(6).Trim()
                            Dim no_imss As String = values(7).Trim()
                            Dim cliente As String = values(8).Trim()
                            Dim pais As String = values(9).Trim()
                            Dim estadoid As String = values(10).Trim()
                            Dim codigo_postal As String = values(11).Trim()
                            Dim fecha_alta_str As String = values(12).Trim()
                            Dim puestoid As String = values(13).Trim()
                            Dim riesgopuestoid As String = values(14).Trim()
                            Dim periodopagoid As String = values(15).Trim()
                            Dim salario_base As String = values(16).Trim()
                            Dim salario_diario_integrado As String = values(17).Trim()
                            Dim sueldos_y_salarios As String = values(18).Trim()
                            Dim excedente As String = values(19).Trim()
                            'Dim excedente_dinero As String = values(19).Trim()
                            Dim tiponominaid As String = values(20).Trim()
                            Dim tipo_jornadanominaid As String = values(21).Trim()
                            Dim tipo_contratonominaid As String = values(22).Trim()
                            Dim regimencontratacionnominaid As String = values(23).Trim()

                            ' Verificar cada campo requerido
                            If String.IsNullOrEmpty(nombre) Then errores.Add("Nombre")
                            If String.IsNullOrEmpty(apellido_paterno) Then errores.Add("Apellido_Paterno")
                            If String.IsNullOrEmpty(apellido_materno) Then errores.Add("Apellido_Materno")
                            If String.IsNullOrEmpty(sexo) Then errores.Add("Sexo")
                            If String.IsNullOrEmpty(fecha_nacimiento_str) Then errores.Add("Fecha_Nacimiento")
                            If String.IsNullOrEmpty(rfc) Then errores.Add("RFC")
                            If String.IsNullOrEmpty(curp) Then errores.Add("Curp")
                            If String.IsNullOrEmpty(no_imss) Then errores.Add("NSS")
                            If String.IsNullOrEmpty(cliente) Then errores.Add("Cliente")
                            If String.IsNullOrEmpty(pais) Then errores.Add("Pais")
                            If String.IsNullOrEmpty(estadoid) Then errores.Add("Estado")
                            If String.IsNullOrEmpty(codigo_postal) Then errores.Add("CP")
                            If String.IsNullOrEmpty(fecha_alta_str) Then errores.Add("Fecha_Alta")
                            If String.IsNullOrEmpty(puestoid) Then errores.Add("Puesto")
                            If String.IsNullOrEmpty(periodopagoid) Then errores.Add("Período_Pago")
                            If String.IsNullOrEmpty(salario_base) Then errores.Add("SB")
                            If String.IsNullOrEmpty(salario_diario_integrado) Then errores.Add("SDI")
                            If String.IsNullOrEmpty(sueldos_y_salarios) Then errores.Add("Sueldos_Salarios")
                            If String.IsNullOrEmpty(excedente) Then errores.Add("Excedente")
                            'If String.IsNullOrEmpty(excedente_dinero) Then errores.Add("Cantidad_Excedente")
                            If String.IsNullOrEmpty(tipo_jornadanominaid) Then errores.Add("Tipo_Jornada")
                            If String.IsNullOrEmpty(tipo_contratonominaid) Then errores.Add("Tipo_Contrato")
                            If String.IsNullOrEmpty(regimencontratacionnominaid) Then errores.Add("Regimen_Fiscal")

                            ' Si hay errores, almacenar en la lista con la fila
                            If errores.Count > 0 Then
                                Dim mensajeFila As String = "En la fila " & i & " con Nombre: " & nombre & " y CURP: " & curp & ", los siguientes campos son requeridos:<br />" & String.Join("<br />", errores)
                                lineErrors.Add(mensajeFila)
                            Else
                                ' Continuar con la lógica de guardado
                                Dim fecha_nacimiento As DateTime
                                Dim formatos As String() = {"dd/MM/yyyy", "MM/dd/yyyy", "yyyy-MM-dd"}
                                DateTime.TryParseExact(fecha_nacimiento_str, formatos, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, fecha_nacimiento)

                                Dim fecha_alta As DateTime
                                DateTime.TryParseExact(fecha_alta_str, formatos, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, fecha_alta)


                                sexo = sexo.Trim.ToLower()
                                excedente = excedente.Trim.ToLower()
                                sueldos_y_salarios = sueldos_y_salarios.Trim.ToLower()

                                If sexo = "F" Or sexo = "f" Or sexo = "Femenino" Or sexo = "femenino" Or sexo = "FEMENINO" Then
                                    sexo = 1
                                ElseIf sexo = "M" Or sexo = "m" Or sexo = "Masculino" Or sexo = "masculino" Or sexo = "MASCULINO" Then
                                    sexo = 0
                                Else
                                    ErrorExcedente = "Favor de colocar un valor válido en Sexo, ejemplo: Femenino, Masculino"
                                End If

                                If excedente = "si" Or excedente = "correcto" Or excedente = "sí" Or excedente = "SI" Or excedente = "Si" Or excedente = "Sí" Then
                                    excedente = 1
                                ElseIf excedente = "no" Or excedente = "falso" Or excedente = "NO" Or excedente = "No" Then
                                    excedente = 0
                                Else
                                    ErrorExcedente = "Favor de colocar un valor válido en excendente, ejemplo: SI, NO"
                                End If

                                If sueldos_y_salarios = "si" Or sueldos_y_salarios = "correcto" Or sueldos_y_salarios = "sí" Or sueldos_y_salarios = "SI" Or sueldos_y_salarios = "Si" Or sueldos_y_salarios = "Sí" Then
                                    sueldos_y_salarios = 1
                                ElseIf sueldos_y_salarios = "no" Or sueldos_y_salarios = "falso" Or sueldos_y_salarios = "NO" Or sueldos_y_salarios = "No" Then
                                    sueldos_y_salarios = 0
                                Else
                                    ErrorSueldosSalarios = "Favor de colocar un valor válido en sueldos y salarios, ejemplo: SI, NO"
                                End If



                                ' Llamar al método para agregar empleado
                                AgregarEmpleado(nombre, apellido_paterno, apellido_materno, sexo, fecha_nacimiento, rfc, curp, no_imss, cliente, pais, estadoid, codigo_postal, fecha_alta, puestoid, riesgopuestoid, periodopagoid, salario_base, salario_diario_integrado, sueldos_y_salarios, excedente, tiponominaid, tipo_jornadanominaid, tipo_contratonominaid, regimencontratacionnominaid)
                            End If

                        End If
                        i += 1
                    Next
                End Using

                ' Mostrar todos los errores si existen
                If lineErrors.Count > 0 Then
                    ' Crear mensaje con todos los errores
                    Dim mensajeError As String = String.Join("<br /><br />", lineErrors) & "<br /><br /> Valida nuevamente y corrige en el archivo de Excel."

                    ' Calcular el ancho y alto responsivos
                    Dim maxWidth As Integer = 800 ' Ancho máximo predeterminado
                    Dim charCount As Integer = mensajeError.Length
                    Dim lineCount As Integer = mensajeError.Split("<br />").Length

                    ' Estimación del ancho basado en el número de caracteres
                    Dim calculatedWidth As Integer = Math.Min(maxWidth, 300 + charCount * 4) ' Ajusta el multiplicador según sea necesario
                    Dim calculatedHeight As Integer = 200 + (lineCount * 20) ' Ajusta el alto según la cantidad de líneas

                    ' Establecer valores finales de screenWidth y screenHeight
                    Dim screenWidth As Integer = Math.Max(330, calculatedWidth)
                    Dim screenHeight As Integer = Math.Max(180, calculatedHeight)

                    ' Mostrar mensaje de alerta
                    rwAlerta.RadAlert(mensajeError, screenWidth, screenHeight, "Alerta", "", "")
                End If


            Catch ex As Exception
                Response.Write(ex.Message.ToString)
            End Try
        Else
            rwAlerta.RadAlert("Favor de subir el documento de Excel .csv", 330, 180, "Alerta", "", "")
        End If
    End Sub


    'Private Sub AgregarEmpleado(clave As String, nombre As String, apellidoPaterno As String, apellidoMaterno As String, rfc As String, curp As String, pais As String, estado As String, municipio As String, codigo_postal As String, email As String, sexo As String, fecha_nacimiento As DateTime, estado_civil As String, cliente As String, sueldos_y_salarios As String, excedente As String, excedente_dinero As String, no_imss As String, puesto As String, periodo As String, salario_base As String, salario_diario_integrado As String)
    Private Sub AgregarEmpleado(nombre As String, apellido_paterno As String, apellido_materno As String, sexo As String, fecha_nacimiento As DateTime, rfc As String, curp As String, no_imss As String, cliente As String, pais As String, estadoid As String, codigo_postal As String, fecha_alta As DateTime, puestoid As String, riesgopuestoid As String, periodopagoid As String, salario_base As String, salario_diario_integrado As String, sueldos_y_salarios As String, excedente As String, tiponominaid As String, tipo_jornadanominaid As String, tipo_contratonominaid As String, regimencontratacionnominaid As String)
        Dim conn As New SqlConnection(Session("conexion"))
        Dim sql As String = ""
        Dim objData As New DataControl(1)
        Dim errorFlag As Integer
        Try
            'sql = "EXEC pPersonalAdministrado @cmd=27, @no_empleado='" & clave & "', @nombre='" & nombre & "', @apellido_paterno='" & apellidoPaterno & "', @apellido_materno='" & apellidoMaterno & "', @rfc='" & rfc & "', @curp='" & curp & "', @pais='" & pais & "', @estado_nombre='" & estado & "', @municipio='" & municipio & "', @codigo_postal='" & codigo_postal & "', @email1='" & email & "', @sexo='" & sexo & "', @fecha_nacimiento='" & fecha_nacimiento.ToString("yyyyMMdd") & "', @estado_civil='" & estado_civil & "',@userid='" & Session("usuarioid") & "', @cliente_nombre = '" & cliente & "', @sueldos_y_salarios='" & sueldos_y_salarios & "', @excedente='" & excedente & "', @excedente_dinero='" & excedente_dinero & "', @no_imss='" & no_imss & "', @periodo_nombre='" & Periodo & "', @puesto_nombre='" & Puesto & "', @salario_base='" & salario_base & "', @salario_diario_integrado=" & salario_diario_integrado
            sql = "EXEC pPersonalAdministrado @cmd=27, @nombre='" & nombre & "', @apellido_paterno='" & apellido_paterno & "', @apellido_materno='" & apellido_materno & "', @sexo='" & sexo & "', @fecha_nacimiento='" & fecha_nacimiento.ToString("yyyyMMdd") & "', @rfc='" & rfc & "', @curp='" & curp & "', @no_imss='" & no_imss & "', @mi_cliente_id='" & cliente & "', @pais='" & pais & "', @estadoid='" & estadoid & "', @codigo_postal='" & codigo_postal & "', @fecha_alta='" & fecha_alta.ToString("yyyyMMdd") & "', @puestoid='" & puestoid & "', @riesgopuestoid='" & riesgopuestoid & "', @periodopagoid='" & periodopagoid & "', @salario_base='" & salario_base & "', @salario_diario_integrado=" & salario_diario_integrado & "', @sueldos_y_salarios='" & sueldos_y_salarios & "', @excedente='" & excedente & "', @tiponominaid='" & tiponominaid & "', @tipo_jornadanominaid='" & tipo_jornadanominaid & "', @tipo_contratonominaid=" & tipo_contratonominaid & "', @regimencontratacionnominaid=" & regimencontratacionnominaid
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
        'csvContent.Append("Clave Empleado,")
        csvContent.Append("Nombre,")
        csvContent.Append("Apellido_Paterno,")
        csvContent.Append("Apellido_Materno,")
        csvContent.Append("Sexo,")
        csvContent.Append("Fecha_Nacimiento,")
        csvContent.Append("RFC,")
        csvContent.Append("Curp,")
        csvContent.Append("NSS,")
        csvContent.Append("Cliente,") 'CATALOGO tblMisClientes
        'csvContent.Append("Estado Civil,")
        csvContent.Append("Pais,")
        csvContent.Append("Estado,") 'CATALOGO tblEstado
        'csvContent.Append("Municipio,")
        csvContent.Append("CP,")
        'csvContent.Append("Email,")
        csvContent.Append("Fecha_alta,")
        csvContent.Append("Puesto,") 'CATALOGO tblPuesto
        csvContent.Append("Riesgo_Puesto,") 'CATALOGO tblRiesgoPuesto
        csvContent.Append("Periodo_Pago,") 'CATALOGO tblCPeriodoPago
        csvContent.Append("SB,")
        csvContent.Append("SDI,")
        csvContent.Append("Sueldos_Salarios,")
        csvContent.Append("Excendente,")
        'csvContent.Append("Cantidad_Excedente,")
        csvContent.Append("Tipo_Nomina,") 'CATALOGO tblCTipoNomina
        csvContent.Append("Tipo_Jornada,") 'CATALOGO tblCTipoJornada
        csvContent.Append("Tipo_Contrato,") 'CATALOGO tblCTipoContrato
        csvContent.Append("Regimen_Fiscal,") 'CATALOGO tblCRegimenContratacion
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
