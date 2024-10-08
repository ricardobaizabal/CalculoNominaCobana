Imports Telerik.Web.UI
Imports Entities
Imports System.IO
Imports System.Data.SqlClient

Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Runtime.InteropServices
Imports System.Globalization

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

    Function QuitarAcentos(texto As String) As String
        If String.IsNullOrEmpty(texto) Then
            Return texto
        End If
        Return System.Text.RegularExpressions.Regex.Replace(texto.Normalize(System.Text.NormalizationForm.FormD), "\p{IsCombiningDiacriticalMarks}+", "")
    End Function

    Protected Sub btnCargarEmpleados_Click(sender As Object, e As EventArgs) Handles btnCargarEmpleados.Click
        If ImportarFile.HasFile Then

            Dim excelApp As New Excel.Application
            Dim workbook As Excel.Workbook = Nothing
            Dim worksheet As Excel.Worksheet = Nothing
            Try


                dt.Clear()
                ViewState("dt") = dt

                ' Construir la ruta del archivo
                Dim filePath As String = Server.MapPath("~/UploadsMontos/")

                ' Se crea un concatenado para el nombre del archivo
                Dim filename As String = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "_" + ImportarFile.FileName.ToString

                If Not Directory.Exists(filePath) Then
                    Directory.CreateDirectory(filePath)
                End If



                ' Guardar el archivo en la ruta especificada
                'ImportarFile.SaveAs(filePath)
                ImportarFile.SaveAs(filePath & "\" & filename)


                ' Se ingresa al archivo
                Dim filePathDownload As String = Server.MapPath("~/UploadsMontos/") & filename
                If Not System.IO.File.Exists(filePathDownload) Then
                    rwAlerta.RadAlert("El archivo no se encuentra en la ruta especificada.", 300, 300, "Alerta", Nothing)
                    Return
                End If


                workbook = excelApp.Workbooks.Open(filePathDownload)
                worksheet = workbook.Sheets(1) ' Abre la primera hoja del archivo

                Dim lineErrors As New List(Of String)() ' Para almacenar errores de cada fila
                Dim lineErrorsCatalogos As New List(Of String)() ' Para almacenar errores de cada fila
                Dim empleados As New List(Of Object)() ' Lista de empleados con datos completos

                ' Definir una variable para almacenar la fecha convertida
                Dim fecha_nacimientoEmp As DateTime


                ' Definir una variable para almacenar la fecha convertida
                Dim fecha_altaEmp As DateTime


                Dim rowCount As Integer = worksheet.UsedRange.Rows.Count
                Dim columnCount As Integer = worksheet.UsedRange.Columns.Count

                Dim empleadosCount As Integer = 0

                Dim cargaid As Long = 0

                Dim ObjData As New DataControl
                Dim p As New ArrayList
                p.Add(New SqlParameter("@cmd", 2))
                'p.Add(New SqlParameter("@anexo", filename))
                'p.Add(New SqlParameter("@usuarioid", CInt(System.Web.HttpContext.Current.Session("usuarioid"))))
                'cargaid = ObjData.SentenceScalarLong("pCargaMasivaActor", 1, p)

                Dim ds As New DataSet
                p.Clear()
                p.Add(New SqlParameter("@cmd", 28))
                ds = ObjData.FillDataSetMasivo("pPersonalAdministrado", CommandType.StoredProcedure, p)


                ' Se declaran las tablas que contiene el DataSet
                Dim tblPersonalAdministradoTemp As New DataTable
                Dim tblSexoTemp As New DataTable
                Dim tblMunicipioTemp As New DataTable
                Dim tblEstadoTemp As New DataTable
                Dim tblEstadoCivilTemp As New DataTable
                Dim tblMisClientesTemp As New DataTable
                Dim tblRegistroPatronalTemp As New DataTable
                Dim tblDepartamentoClienteTemp As New DataTable
                Dim tblPuestoTemp As New DataTable
                Dim tblRiesgoPuestoTemp As New DataTable
                Dim tblRegimenContratacionTemp As New DataTable
                Dim tblPeriodoPagoTemp As New DataTable
                Dim tblCTipoNominaTemp As New DataTable
                Dim tblRegimenFiscalTemp As New DataTable
                Dim tblCTipoJornadaTemp As New DataTable
                Dim tblCTipoContratoTemp As New DataTable

                ' Se asginas los datos a cada tabla correspondiente
                If ds.Tables.Count > 0 Then

                    tblPersonalAdministradoTemp = ds.Tables(0)
                    tblSexoTemp = ds.Tables(1)
                    tblMunicipioTemp = ds.Tables(2)
                    tblEstadoTemp = ds.Tables(3)
                    tblEstadoCivilTemp = ds.Tables(4)
                    tblMisClientesTemp = ds.Tables(5)
                    tblRegistroPatronalTemp = ds.Tables(6)
                    tblDepartamentoClienteTemp = ds.Tables(7)
                    tblPuestoTemp = ds.Tables(8)
                    tblRiesgoPuestoTemp = ds.Tables(9)
                    tblRegimenContratacionTemp = ds.Tables(10)
                    tblPeriodoPagoTemp = ds.Tables(11)
                    tblCTipoNominaTemp = ds.Tables(12)
                    tblRegimenFiscalTemp = ds.Tables(13)
                    tblCTipoJornadaTemp = ds.Tables(14)
                    tblCTipoContratoTemp = ds.Tables(15)
                End If



                ' Se inicia el proceso de lectura del archivo de excel
                For i As Integer = 2 To rowCount ' Comienza desde 2 para saltar el encabezado
                    Dim errores As New List(Of String)()
                    Dim erroresCatalogos As New List(Of String)()

                    Dim excedente As Integer = 0
                    Dim sueldos_y_salarios As Integer = 0

                    empleadosCount = empleadosCount + 1

                    ' Identificadores de los CATALOGOS utilizados
                    Dim sexoid As Integer = 0
                    Dim municipioid As Integer = 0
                    Dim estadoid As Integer = 0
                    Dim estadocivilid As Integer = 0
                    Dim mi_cliente_id As Integer = 0
                    Dim registropatronalid As Integer = 0
                    Dim departamentoid As Integer = 0
                    Dim puestoid As Integer = 0
                    Dim riesgopuestoid As Integer = 0
                    Dim regimencontratacionid As Integer = 0
                    Dim periodopagoid As Integer = 0
                    Dim tiponominaid As String = ""
                    Dim regimencontratacionnominaid As Integer = 0
                    Dim tipo_jornadanominaid As String = ""
                    Dim tipo_contratonominaid As String = ""


                    ' Leer los valores de las celdas
                    'Dim clave As String = worksheet.Cells(i, 1).Value?.ToString().Trim().ToUpper
                    Dim clave As String = If(worksheet.Cells(i, 1).Value?.ToString().Trim().ToUpper(), Nothing)
                    clave = If(clave IsNot Nothing, QuitarAcentos(clave), Nothing)

                    Dim nombre As String = If(worksheet.Cells(i, 2).Value?.ToString().Trim().ToUpper(), Nothing)
                    nombre = If(nombre IsNot Nothing, QuitarAcentos(nombre), Nothing)

                    Dim apellido_paterno As String = If(worksheet.Cells(i, 3).Value?.ToString().Trim().ToUpper(), Nothing)
                    apellido_paterno = If(apellido_paterno IsNot Nothing, QuitarAcentos(apellido_paterno), Nothing)

                    Dim apellido_materno As String = If(worksheet.Cells(i, 4).Value?.ToString().Trim().ToUpper(), Nothing)
                    apellido_materno = If(apellido_materno IsNot Nothing, QuitarAcentos(apellido_materno), Nothing)

                    Dim sexo As String = If(worksheet.Cells(i, 5).Value?.ToString().Trim().ToUpper(), Nothing)
                    sexo = If(sexo IsNot Nothing, QuitarAcentos(sexo), Nothing)

                    Dim fecha_nacimiento As String = If(worksheet.Cells(i, 6).Value?.ToString().Trim().ToUpper(), Nothing)
                    fecha_nacimiento = If(fecha_nacimiento IsNot Nothing, QuitarAcentos(fecha_nacimiento), Nothing)

                    Dim rfc As String = If(worksheet.Cells(i, 7).Value?.ToString().Trim().ToUpper(), Nothing)
                    rfc = If(rfc IsNot Nothing, QuitarAcentos(rfc), Nothing)

                    Dim curp As String = If(worksheet.Cells(i, 8).Value?.ToString().Trim().ToUpper(), Nothing)
                    curp = If(curp IsNot Nothing, QuitarAcentos(curp), Nothing)

                    Dim no_imss As String = If(worksheet.Cells(i, 9).Value?.ToString().Trim().ToUpper(), Nothing)
                    no_imss = If(no_imss IsNot Nothing, QuitarAcentos(no_imss), Nothing)

                    Dim calle As String = If(worksheet.Cells(i, 10).Value?.ToString().Trim().ToUpper(), Nothing)
                    calle = If(calle IsNot Nothing, QuitarAcentos(calle), Nothing)

                    Dim no_ext As String = If(worksheet.Cells(i, 11).Value?.ToString().Trim().ToUpper(), Nothing)
                    no_ext = If(no_ext IsNot Nothing, QuitarAcentos(no_ext), Nothing)

                    Dim no_int As String = If(worksheet.Cells(i, 12).Value?.ToString().Trim().ToUpper(), Nothing)
                    no_int = If(no_int IsNot Nothing, QuitarAcentos(no_int), Nothing)

                    Dim colonia As String = If(worksheet.Cells(i, 13).Value?.ToString().Trim().ToUpper(), Nothing)
                    colonia = If(colonia IsNot Nothing, QuitarAcentos(colonia), Nothing)

                    Dim municipio As String = If(worksheet.Cells(i, 14).Value?.ToString().Trim().ToUpper(), Nothing)
                    municipio = If(municipio IsNot Nothing, QuitarAcentos(municipio), Nothing)

                    Dim codigo_postal As String = If(worksheet.Cells(i, 15).Value?.ToString().Trim().ToUpper(), Nothing)
                    codigo_postal = If(codigo_postal IsNot Nothing, QuitarAcentos(codigo_postal), Nothing)

                    Dim estado As String = If(worksheet.Cells(i, 16).Value?.ToString().Trim().ToUpper(), Nothing)
                    estado = If(estado IsNot Nothing, QuitarAcentos(estado), Nothing)

                    Dim pais As String = If(worksheet.Cells(i, 17).Value?.ToString().Trim().ToUpper(), Nothing)
                    pais = If(pais IsNot Nothing, QuitarAcentos(pais), Nothing)

                    Dim celular As String = If(worksheet.Cells(i, 18).Value?.ToString().Trim().ToUpper(), Nothing)
                    celular = If(celular IsNot Nothing, QuitarAcentos(celular), Nothing)

                    Dim email As String = If(worksheet.Cells(i, 19).Value?.ToString().Trim().ToUpper(), Nothing)
                    email = If(email IsNot Nothing, QuitarAcentos(email), Nothing)

                    Dim lugar_nacimiento As String = If(worksheet.Cells(i, 20).Value?.ToString().Trim().ToUpper(), Nothing)
                    lugar_nacimiento = If(lugar_nacimiento IsNot Nothing, QuitarAcentos(lugar_nacimiento), Nothing)

                    Dim estado_civil As String = If(worksheet.Cells(i, 21).Value?.ToString().Trim().ToUpper(), Nothing)
                    estado_civil = If(estado_civil IsNot Nothing, QuitarAcentos(estado_civil), Nothing)

                    Dim cliente As String = If(worksheet.Cells(i, 22).Value?.ToString().Trim().ToUpper(), Nothing)
                    cliente = If(cliente IsNot Nothing, QuitarAcentos(cliente), Nothing)

                    Dim sueldos_y_salarios_str As String = If(worksheet.Cells(i, 23).Value?.ToString().Trim().ToUpper(), Nothing)
                    sueldos_y_salarios_str = If(sueldos_y_salarios_str IsNot Nothing, QuitarAcentos(sueldos_y_salarios_str), Nothing)

                    Dim excedente_str As String = If(worksheet.Cells(i, 24).Value?.ToString().Trim().ToUpper(), Nothing)
                    excedente_str = If(excedente_str IsNot Nothing, QuitarAcentos(excedente_str), Nothing)

                    Dim fecha_alta As String = If(worksheet.Cells(i, 25).Value?.ToString().Trim().ToUpper(), Nothing)
                    fecha_alta = If(fecha_alta IsNot Nothing, QuitarAcentos(fecha_alta), Nothing)

                    Dim registropatronal As String = If(worksheet.Cells(i, 26).Value?.ToString().Trim().ToUpper(), Nothing)
                    registropatronal = If(registropatronal IsNot Nothing, QuitarAcentos(registropatronal), Nothing)

                    Dim departamento As String = If(worksheet.Cells(i, 27).Value?.ToString().Trim().ToUpper(), Nothing)
                    departamento = If(departamento IsNot Nothing, QuitarAcentos(departamento), Nothing)

                    Dim puesto As String = If(worksheet.Cells(i, 28).Value?.ToString().Trim().ToUpper(), Nothing)
                    puesto = If(puesto IsNot Nothing, QuitarAcentos(puesto), Nothing)

                    Dim salario_base As String = If(worksheet.Cells(i, 29).Value?.ToString().Trim().ToUpper(), Nothing)
                    salario_base = If(salario_base IsNot Nothing, QuitarAcentos(salario_base), Nothing)

                    Dim salario_diario_integrado As String = If(worksheet.Cells(i, 30).Value?.ToString().Trim().ToUpper(), Nothing)
                    salario_diario_integrado = If(salario_diario_integrado IsNot Nothing, QuitarAcentos(salario_diario_integrado), Nothing)

                    Dim riesgopuesto As String = If(worksheet.Cells(i, 31).Value?.ToString().Trim().ToUpper(), Nothing)
                    riesgopuesto = If(riesgopuesto IsNot Nothing, QuitarAcentos(riesgopuesto), Nothing)

                    Dim regimendecontratacion As String = If(worksheet.Cells(i, 32).Value?.ToString().Trim().ToUpper(), Nothing)
                    regimendecontratacion = If(regimendecontratacion IsNot Nothing, QuitarAcentos(regimendecontratacion), Nothing)

                    Dim periodopago As String = If(worksheet.Cells(i, 33).Value?.ToString().Trim().ToUpper(), Nothing)
                    periodopago = If(periodopago IsNot Nothing, QuitarAcentos(periodopago), Nothing)

                    Dim tiponomina As String = If(worksheet.Cells(i, 34).Value?.ToString().Trim().ToUpper(), Nothing)
                    tiponomina = If(tiponomina IsNot Nothing, QuitarAcentos(tiponomina), Nothing)

                    Dim regimencontratacionnomina As String = If(worksheet.Cells(i, 35).Value?.ToString().Trim().ToUpper(), Nothing)
                    regimencontratacionnomina = If(regimencontratacionnomina IsNot Nothing, QuitarAcentos(regimencontratacionnomina), Nothing)

                    Dim tipo_jornadanomina As String = If(worksheet.Cells(i, 36).Value?.ToString().Trim().ToUpper(), Nothing)
                    tipo_jornadanomina = If(tipo_jornadanomina IsNot Nothing, QuitarAcentos(tipo_jornadanomina), Nothing)

                    Dim tipo_contratonomina As String = If(worksheet.Cells(i, 37).Value?.ToString().Trim().ToUpper(), Nothing)
                    tipo_contratonomina = If(tipo_contratonomina IsNot Nothing, QuitarAcentos(tipo_contratonomina), Nothing)




                    ' Verificar cada campo requerido
                    If String.IsNullOrEmpty(nombre) Then errores.Add("Nombre")
                    If String.IsNullOrEmpty(apellido_paterno) Then errores.Add("Apellido Paterno")
                    If String.IsNullOrEmpty(apellido_materno) Then errores.Add("Apellido Materno")
                    If String.IsNullOrEmpty(sexo) Then errores.Add("Sexo")
                    If String.IsNullOrEmpty(fecha_nacimiento) Then errores.Add("Fecha de Nacimiento")
                    If String.IsNullOrEmpty(rfc) Then errores.Add("RFC")
                    If String.IsNullOrEmpty(curp) Then errores.Add("Curp")
                    If String.IsNullOrEmpty(no_imss) Then errores.Add("NSS")
                    If String.IsNullOrEmpty(codigo_postal) Then errores.Add("Codigo Postal")
                    If String.IsNullOrEmpty(estado) Then errores.Add("Estado")
                    If String.IsNullOrEmpty(pais) Then errores.Add("Pais")
                    If String.IsNullOrEmpty(cliente) Then errores.Add("Cliente")
                    If String.IsNullOrEmpty(sueldos_y_salarios_str) Then errores.Add("Sueldos y Salarios")
                    If String.IsNullOrEmpty(excedente_str) Then errores.Add("Excedente")
                    If String.IsNullOrEmpty(fecha_alta) Then errores.Add("Fecha de Alta")
                    If String.IsNullOrEmpty(registropatronal) Then errores.Add("Registro Patronal")
                    If String.IsNullOrEmpty(departamento) Then errores.Add("Departamento")
                    If String.IsNullOrEmpty(puesto) Then errores.Add("Puesto")
                    If String.IsNullOrEmpty(salario_base) Then errores.Add("SB")
                    If String.IsNullOrEmpty(salario_diario_integrado) Then errores.Add("SDI")
                    If String.IsNullOrEmpty(riesgopuesto) Then errores.Add("Riesgo de Puesto")
                    If String.IsNullOrEmpty(regimendecontratacion) Then errores.Add("Regimen de Contratacion")
                    If String.IsNullOrEmpty(periodopago) Then errores.Add("Periodo de Pago")
                    If String.IsNullOrEmpty(tiponomina) Then errores.Add("Tipo de Nomina")
                    If String.IsNullOrEmpty(regimencontratacionnomina) Then errores.Add("Regimen Fiscal")
                    If String.IsNullOrEmpty(tipo_jornadanomina) Then errores.Add("Tipo de Jornada")
                    If String.IsNullOrEmpty(tipo_contratonomina) Then errores.Add("Tipo de Contrato")



                    ' Si hay errores, almacenar en la lista con la fila
                    If errores.Count > 0 Then
                        Dim mensajeFila As String = "<br />En la fila " & i & " con Nombre: " & nombre & " y CURP: " & curp & ", los siguientes campos son requeridos:<br />" & String.Join("<br />", errores)
                        lineErrors.Add(mensajeFila)
                    ElseIf errores.Count = 0 Then


                        ' ----------- Se valida el parámetro de cliente ----------- 
                        Try
                            ' Limpiamos el nombre del cliente
                            Dim clienteLimpiado As String = LTrim(RTrim(cliente)).ToString()

                            ' Buscamos el cliente en la tabla temporal
                            Dim rowCliente() As DataRow = tblMisClientesTemp.Select("razonsocial = '" & clienteLimpiado & "'")

                            ' Verificamos si se encontró alguna fila
                            If rowCliente.Length > 0 Then
                                mi_cliente_id = CInt(rowCliente(0)("id")) ' Asigna el ID encontrado
                            Else
                                Dim msjErr_cliente As String = "- Cliente: favor de ingresar un cliente existente del catálogo"
                                errores.Add(msjErr_cliente)
                                mi_cliente_id = 0 ' Asigna un valor por defecto si no se encuentra
                            End If
                        Catch ex As Exception
                            Dim msjErr_cliente As String = "- Cliente: Error al validar el cliente: " & ex.Message
                            errores.Add(msjErr_cliente)
                            mi_cliente_id = 0 ' Asigna un valor por defecto en caso de error
                        End Try


                        ' ----------- Se valida si el empleado existe en la base de datos con el mismo cliente -----------
                        Try
                            ' Limpiamos los datos del empleado
                            Dim nombreLimpiado As String = LTrim(RTrim(nombre)).ToString()
                            Dim apellido_paternoLimpiado As String = LTrim(RTrim(apellido_paterno)).ToString()
                            Dim curpLimpiado As String = LTrim(RTrim(curp)).ToString()
                            Dim no_imssLimpiado As String = LTrim(RTrim(no_imss)).ToString()
                            Dim mi_cliente_idLimpiado As String = LTrim(RTrim(mi_cliente_id)).ToString()

                            ' Construimos el filtro para la búsqueda del empleado en la tabla temporal
                            Dim filtro As String = String.Format("nombre = '{0}' AND apellido_paterno = '{1}' AND curp = '{2}' AND no_imss = '{3}' AND mi_cliente_id = '{4}'",
                                         nombreLimpiado, apellido_paternoLimpiado, curpLimpiado, no_imssLimpiado, mi_cliente_idLimpiado)

                            ' Buscamos el empleado en la tabla temporal
                            Dim rowExistente() As DataRow = tblPersonalAdministradoTemp.Select(filtro)

                            ' Verificamos si se encontró alguna fila
                            If rowExistente.Length > 0 Then
                                Dim msjErr_existente As String = "- El empleado ya está registrado con el cliente " & cliente & ", verificalo e intenta de nuevo."
                                errores.Add(msjErr_existente)
                            End If
                        Catch ex As Exception
                            Dim msjErr_existente As String = "- Error al validar si existe el empleado: " & ex.Message
                            errores.Add(msjErr_existente)
                        End Try




                        ' ----------- cambio en las fechas -----------
                        Try

                            ' Eliminar el formato AM/PM
                            fecha_nacimiento = fecha_nacimiento.Replace(" A. M.", "").Replace(" P. M.", "")
                            fecha_alta = fecha_alta.Replace(" A. M.", "").Replace(" P. M.", "")

                            ' Intentar convertir el string a DateTime
                            If DateTime.TryParseExact(fecha_nacimiento, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, fecha_nacimientoEmp) Then
                                ' Si la conversión es exitosa, puedes ahora usar fecha_nacimiento como DateTime
                                ' Envíalo a la base de datos o úsalo como necesites
                            Else
                                ' Manejar el error si la conversión falla
                                Throw New FormatException("La fecha no tiene un formato válido.")
                            End If


                            ' Intentar convertir el string a DateTime
                            If DateTime.TryParseExact(fecha_alta, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, fecha_altaEmp) Then
                                ' Si la conversión es exitosa, puedes ahora usar fecha_nacimiento como DateTime
                                ' Envíalo a la base de datos o úsalo como necesites
                            Else
                                ' Manejar el error si la conversión falla
                                Throw New FormatException("La fecha no tiene un formato válido.")
                            End If


                        Catch ex As Exception
                            Dim msjErr_fecha As String = "- Error al cambiar el dato de fecha: " & ex.Message
                            errores.Add(msjErr_fecha)
                        End Try




                        ' ----------- Se valida el parámetro de sexo ----------- 
                        Try

                            Dim sexoLimpiado As String = LTrim(RTrim(sexo)).ToString()


                            If sexoLimpiado.Equals("F", StringComparison.OrdinalIgnoreCase) OrElse sexoLimpiado.Equals("FEMENINO", StringComparison.OrdinalIgnoreCase) Then
                                sexoid = 2
                            ElseIf sexoLimpiado.Equals("M", StringComparison.OrdinalIgnoreCase) OrElse sexoLimpiado.Equals("MASCULINO", StringComparison.OrdinalIgnoreCase) Then
                                sexoid = 1
                            Else
                                Dim msjErr_sexo As String = "- Sexo: favor de ingresar un valor válido. Opciones: FEMENINO o MASCULINO."
                                errores.Add(msjErr_sexo)
                                sexoid = 0 ' Indicador de valor no válido
                            End If

                            ' Solo realizamos la búsqueda si el valor de sexoid es válido
                            If sexoid <> -1 Then
                                Dim rowSexo() As DataRow = tblSexoTemp.Select("id = " & sexoid)

                                ' Verificamos si se encontró alguna fila
                                If rowSexo.Length > 0 Then
                                    sexoid = CInt(rowSexo(0)("id")) ' Asigna el ID encontrado
                                Else
                                    Dim msjErr_sexo As String = "- Sexo: no se encontró un registro válido para el id proporcionado"
                                    errores.Add(msjErr_sexo)
                                    sexoid = 0 ' Asigna un valor por defecto si no se encuentra
                                End If
                            End If
                        Catch ex As Exception
                            Dim msjErr_sexo As String = "- Sexo: Error al validar el sexo: " & ex.Message
                            errores.Add(msjErr_sexo)
                            sexoid = 0 ' Asigna un valor por defecto en caso de error
                        End Try



                        ' ----------- Se valida el parámetro de municipio ----------- 
                        Try
                            ' Limpiamos el nombre del municipio
                            Dim municipioLimpiado As String = LTrim(RTrim(municipio)).ToString()

                            ' Buscamos el municipio en la tabla temporal
                            Dim rowMunicipio() As DataRow = tblMunicipioTemp.Select("nombre = '" & municipioLimpiado & "'")

                            ' Verificamos si se encontró alguna fila
                            If rowMunicipio.Length > 0 Then
                                municipioid = CInt(rowMunicipio(0)("id")) ' Asigna el ID encontrado
                            Else
                                Dim msjErr_municipio As String = "- Municipio: favor de ingresar un municipio existente del catalogo"
                                errores.Add(msjErr_municipio)
                                municipioid = 0 ' Asigna un valor por defecto si no se encuentra
                            End If
                        Catch ex As Exception
                            Dim msjErr_municipio As String = "- Municipio: Error al validar el municipio: " & ex.Message
                            errores.Add(msjErr_municipio)
                            municipioid = 0 ' Asigna un valor por defecto en caso de error
                        End Try



                        ' ----------- Se valida el parametro de estado -----------
                        Try
                            ' Utiliza la función "Select" para buscar el estado en la tabla
                            Dim rowEstado() As DataRow = tblEstadoTemp.Select("nombre = '" & estado.Trim() & "'")

                            ' Verifica si encontró algún resultado
                            If rowEstado.Length > 0 Then
                                ' Si se encontró, se toma el id del estado
                                estadoid = CInt(rowEstado(0)("id"))
                            Else
                                ' Si no se encontró, se agrega el mensaje de error
                                estadoid = 0
                                Dim msjErr_estado As String = "- Estado: favor de ingresar un estado existente del catálogo"
                                errores.Add(msjErr_estado)
                            End If
                        Catch ex As Exception
                            ' En caso de algún otro error inesperado
                            estadoid = 0
                            Dim msjErr_estado As String = "- Estado: Error al buscar el estado: " & ex.Message
                            errores.Add(msjErr_estado)
                        End Try


                        ' ----------- Se valida el parámetro de estado civil ----------- 
                        Try
                            ' Limpiamos el nombre del estado civil
                            Dim estadoCivilLimpiado As String = LTrim(RTrim(estado_civil)).ToString()

                            ' Buscamos el estado civil en la tabla temporal
                            Dim rowEstadoCivil() As DataRow = tblEstadoCivilTemp.Select("nombre = '" & estadoCivilLimpiado & "'")

                            ' Verificamos si se encontró alguna fila
                            If rowEstadoCivil.Length > 0 Then
                                estadocivilid = CInt(rowEstadoCivil(0)("id")) ' Asigna el ID encontrado
                            Else
                                Dim msjErr_estadocivil As String = "- Estado Civil: favor de ingresar un valor válido. Opciones: soltero, casado, divorciado, viudo, unión libre o separado"
                                errores.Add(msjErr_estadocivil)
                                estadocivilid = 0 ' Asigna un valor por defecto si no se encuentra
                            End If
                        Catch ex As Exception
                            Dim msjErr_estadocivil As String = "- Estado Civil: Error al validar el estado civil: " & ex.Message
                            errores.Add(msjErr_estadocivil)
                            estadocivilid = 0 ' Asigna un valor por defecto en caso de error
                        End Try



                        ' ----------- Se valida el parámetro de registro patronal ----------- 
                        Try
                            ' Limpiamos el nombre del registro patronal
                            Dim registroPatronalLimpiado As String = LTrim(RTrim(registropatronal)).ToString()

                            ' Buscamos el registro patronal en la tabla temporal
                            Dim rowRegistroPatronal() As DataRow = tblRegistroPatronalTemp.Select("nombre = '" & registroPatronalLimpiado & "'")

                            ' Verificamos si se encontró alguna fila
                            If rowRegistroPatronal.Length > 0 Then
                                registropatronalid = CInt(rowRegistroPatronal(0)("id")) ' Asigna el ID encontrado
                            Else
                                Dim msjErr_registroPatronal As String = "- Registro Patronal: favor de ingresar un valor válido. Opcion: F09-16327-1-0"
                                errores.Add(msjErr_registroPatronal)
                                registropatronalid = 0 ' Asigna un valor por defecto si no se encuentra
                            End If
                        Catch ex As Exception
                            Dim msjErr_registroPatronal As String = "- Registro Patronal: Error al validar el registro patronal: " & ex.Message
                            errores.Add(msjErr_registroPatronal)
                            registropatronalid = 0 ' Asigna un valor por defecto en caso de error
                        End Try



                        ' ----------- Se valida el parámetro de departamento ----------- 
                        Try
                            ' Limpiamos el nombre del departamento
                            Dim departamentoLimpiado As String = LTrim(RTrim(departamento)).ToString()

                            ' Buscamos el departamento en la tabla temporal
                            Dim rowDepartamento() As DataRow = tblDepartamentoClienteTemp.Select("nombre = '" & departamentoLimpiado & "'")

                            ' Verificamos si se encontró alguna fila
                            If rowDepartamento.Length > 0 Then
                                departamentoid = CInt(rowDepartamento(0)("id")) ' Asigna el ID encontrado
                            Else
                                Dim msjErr_departamento As String = "- Departamento: favor de ingresar un departamento existente del catalogo"
                                errores.Add(msjErr_departamento)
                                departamentoid = 0 ' Asigna un valor por defecto si no se encuentra
                            End If
                        Catch ex As Exception
                            Dim msjErr_departamento As String = "- Departamento: Error al validar el departamento: " & ex.Message
                            errores.Add(msjErr_departamento)
                            departamentoid = 0 ' Asigna un valor por defecto en caso de error
                        End Try



                        ' ----------- Se valida el parámetro de puesto ----------- 
                        Try
                            ' Limpiamos el nombre del puesto
                            Dim puestoLimpiado As String = LTrim(RTrim(puesto)).ToString()

                            ' Buscamos el puesto en la tabla temporal
                            Dim rowPuesto() As DataRow = tblPuestoTemp.Select("nombre = '" & puestoLimpiado & "'")

                            ' Verificamos si se encontró alguna fila
                            If rowPuesto.Length > 0 Then
                                puestoid = CInt(rowPuesto(0)("id")) ' Asigna el ID encontrado
                            Else
                                Dim msjErr_puesto As String = "- Puesto: favor de ingresar un puesto existente del catalogo"
                                errores.Add(msjErr_puesto)
                                puestoid = 0 ' Asigna un valor por defecto si no se encuentra
                            End If
                        Catch ex As Exception
                            Dim msjErr_puesto As String = "- Puesto: Error al validar el puesto: " & ex.Message
                            errores.Add(msjErr_puesto)
                            puestoid = 0 ' Asigna un valor por defecto en caso de error
                        End Try



                        ' ----------- Se valida el parámetro de Riesgo Puesto ----------- 
                        Try
                            ' Limpiamos el nombre del riesgo de puesto
                            Dim riesgoPuestoLimpiado As String = LTrim(RTrim(riesgopuesto)).ToString()

                            ' Buscamos el riesgo de puesto en la tabla temporal
                            Dim rowRiesgoPuesto() As DataRow = tblRiesgoPuestoTemp.Select("nombre = '" & riesgoPuestoLimpiado & "'")

                            ' Verificamos si se encontró alguna fila
                            If rowRiesgoPuesto.Length > 0 Then
                                riesgopuestoid = CInt(rowRiesgoPuesto(0)("id")) ' Asigna el ID encontrado
                            Else
                                Dim msjErr_RiesgoPuesto As String = "- Riesgo de Puesto: favor de ingresar un valor válido. Opciones: Clase I, Clase II, Clase III, Clase IV o Clase V"
                                errores.Add(msjErr_RiesgoPuesto)
                                riesgopuestoid = 0 ' Asigna un valor por defecto si no se encuentra
                            End If
                        Catch ex As Exception
                            Dim msjErr_RiesgoPuesto As String = "- Riesgo de Puesto: Error al validar el Riesgo de Puesto: " & ex.Message
                            errores.Add(msjErr_RiesgoPuesto)
                            riesgopuestoid = 0 ' Asigna un valor por defecto en caso de error
                        End Try



                        ' ----------- Se valida el parámetro de Régimen Contratación ----------- 
                        Try
                            ' Limpiamos el nombre del régimen de contratación
                            Dim regimenContratacionLimpiado As String = LTrim(RTrim(regimendecontratacion)).ToString()

                            ' Buscamos el régimen de contratación en la tabla temporal
                            Dim rowRegimenContratacion() As DataRow = tblRegimenContratacionTemp.Select("nombre = '" & regimenContratacionLimpiado & "'")

                            ' Verificamos si se encontró alguna fila
                            If rowRegimenContratacion.Length > 0 Then
                                regimencontratacionid = CInt(rowRegimenContratacion(0)("id")) ' Asigna el ID encontrado
                            Else
                                Dim msjErr_RegimenContratacion As String = "- Régimen de contratación: favor de ingresar un régimen existente del catálogo"
                                errores.Add(msjErr_RegimenContratacion)
                                regimencontratacionid = 0 ' Asigna un valor por defecto si no se encuentra
                            End If
                        Catch ex As Exception
                            Dim msjErr_RegimenContratacion As String = "- Régimen de contratación: Error al validar el Régimen de Contratación: " & ex.Message
                            errores.Add(msjErr_RegimenContratacion)
                            regimencontratacionid = 0 ' Asigna un valor por defecto en caso de error
                        End Try



                        ' ----------- Se valida el parámetro de Período de Pago ----------- 
                        Try
                            ' Limpiamos el nombre del período de pago
                            Dim periodoPagoLimpiado As String = LTrim(RTrim(periodopago)).ToString()

                            ' Buscamos el período de pago en la tabla temporal
                            Dim rowPeriodoPago() As DataRow = tblPeriodoPagoTemp.Select("nombre = '" & periodoPagoLimpiado & "'")

                            ' Verificamos si se encontró alguna fila
                            If rowPeriodoPago.Length > 0 Then
                                periodopagoid = CInt(rowPeriodoPago(0)("id")) ' Asigna el ID encontrado
                            Else
                                Dim msjErr_periodoPago As String = "- Período de Pago: favor de ingresar un valor válido. Opciones: Semanal, Catorcenal, Quincenal o Mensual"
                                errores.Add(msjErr_periodoPago)
                                periodopagoid = 0 ' Asigna un valor por defecto si no se encuentra
                            End If
                        Catch ex As Exception
                            Dim msjErr_periodoPago As String = "- Período de Pago: Error al validar el Período de Pago: " & ex.Message
                            errores.Add(msjErr_periodoPago)
                            periodopagoid = 0 ' Asigna un valor por defecto en caso de error
                        End Try



                        ' ----------- Se valida el parámetro de Contrato Tipo de Nómina ----------- 
                        Try
                            ' Limpiamos el tipo de nómina
                            Dim tipoNominaLimpiado As String = LTrim(RTrim(tiponomina)).ToString()

                            ' Buscamos el tipo de nómina en la tabla temporal
                            Dim rowCTipoNomina() As DataRow = tblCTipoNominaTemp.Select("tipo = '" & tipoNominaLimpiado & "'")

                            ' Verificamos si se encontró alguna fila
                            If rowCTipoNomina.Length > 0 Then
                                tiponominaid = CStr(rowCTipoNomina(0)("tipo")) ' Asigna el ID encontrado
                            Else
                                Dim msjErr_tipoNomina As String = "- Tipo de Nómina: favor de ingresar un valor válido. Opciones: O (Ordinaria) o E (Extraordinaria)"
                                errores.Add(msjErr_tipoNomina)
                                tiponominaid = "" ' Asigna un valor por defecto si no se encuentra
                            End If
                        Catch ex As Exception
                            Dim msjErr_tipoNomina As String = "- Tipo de Nómina: Error al validar el Tipo de Nómina: " & ex.Message
                            errores.Add(msjErr_tipoNomina)
                            tiponominaid = "" ' Asigna un valor por defecto en caso de error
                        End Try



                        ' ----------- Se valida el parámetro de régimen fiscal ----------- 
                        Try
                            ' Limpiamos el régimen fiscal
                            Dim regimenFiscalLimpiado As String = LTrim(RTrim(regimencontratacionnomina)).ToString()

                            ' Buscamos el régimen fiscal en la tabla temporal
                            Dim rowRegimenFiscal() As DataRow = tblRegimenFiscalTemp.Select("nombre = '" & regimenFiscalLimpiado & "'")

                            ' Verificamos si se encontró alguna fila
                            If rowRegimenFiscal.Length > 0 Then
                                regimencontratacionnominaid = CInt(rowRegimenFiscal(0)("id")) ' Asigna el ID encontrado
                            Else
                                Dim msjErr_regimenFiscal As String = "- Régimen Fiscal: favor de ingresar un régimen existente del catálogo"
                                errores.Add(msjErr_regimenFiscal)
                                regimencontratacionnominaid = 0 ' Asigna un valor por defecto si no se encuentra
                            End If
                        Catch ex As Exception
                            Dim msjErr_regimenFiscal As String = "- Régimen Fiscal: Error al validar el Régimen Fiscal: " & ex.Message
                            errores.Add(msjErr_regimenFiscal)
                            regimencontratacionnominaid = 0 ' Asigna un valor por defecto en caso de error
                        End Try



                        ' ----------- Se valida el parámetro de contrato tipo de jornada ----------- 
                        Try
                            ' Limpiamos el tipo de jornada
                            Dim tipoJornadaLimpiado As String = LTrim(RTrim(tipo_jornadanomina)).ToString()

                            ' Buscamos el tipo de jornada en la tabla temporal
                            Dim rowCTipoJornada() As DataRow = tblCTipoJornadaTemp.Select("nombre = '" & tipoJornadaLimpiado & "'")

                            ' Verificamos si se encontró alguna fila
                            If rowCTipoJornada.Length > 0 Then
                                tipo_jornadanominaid = CStr(rowCTipoJornada(0)("id")) ' Asigna el ID encontrado
                            Else
                                Dim msjErr_tipoJornada As String = "- Tipo de Jornada: favor de ingresar una jornada existente del catálogo"
                                errores.Add(msjErr_tipoJornada)
                                tipo_jornadanominaid = "" ' Asigna un valor por defecto si no se encuentra
                            End If
                        Catch ex As Exception
                            Dim msjErr_tipoJornada As String = "- Tipo de Jornada: Error al validar el Tipo de Jornada: " & ex.Message
                            errores.Add(msjErr_tipoJornada)
                            tipo_jornadanominaid = "" ' Asigna un valor por defecto en caso de error
                        End Try



                        ' ----------- Se valida el parámetro de contrato tipo de contrato ----------- 
                        Try
                            ' Limpiamos el tipo de contrato
                            Dim tipoContratoLimpiado As String = LTrim(RTrim(tipo_contratonomina)).ToString()

                            ' Buscamos el tipo de contrato en la tabla temporal
                            Dim rowCTipoContrato() As DataRow = tblCTipoContratoTemp.Select("nombre = '" & tipoContratoLimpiado & "'")

                            ' Verificamos si se encontró alguna fila
                            If rowCTipoContrato.Length > 0 Then
                                tipo_contratonominaid = CStr(rowCTipoContrato(0)("tipo")) ' Asigna el ID encontrado
                            Else
                                Dim msjErr_tipoContrato As String = "- Tipo de Contrato: favor de ingresar un contrato existente del catálogo"
                                errores.Add(msjErr_tipoContrato)
                                tipo_contratonominaid = 0 ' Asigna un valor por defecto si no se encuentra
                            End If
                        Catch ex As Exception
                            Dim msjErr_tipoContrato As String = "- Tipo de Contrato: Error al validar el Tipo de Contrato: " & ex.Message
                            errores.Add(msjErr_tipoContrato)
                            tipo_contratonominaid = 0 ' Asigna un valor por defecto en caso de error
                        End Try



                        ' ----------- Se valida el parametro de excedente -----------
                        If excedente_str = "1" Or excedente_str = "SI" Then
                            excedente = 1
                        ElseIf excedente_str = "FALSO" Or excedente_str = "NO" Then
                            excedente = 0
                        Else
                            Dim msjErr_excedente As String = "- Excedentes: Favor de colocar un valor válido en sueldos y salarios. Opciones: SI, NO"
                            errores.Add(msjErr_excedente)
                        End If


                        ' ----------- Se valida el parametro de sueldos y salarios -----------
                        If sueldos_y_salarios_str = "SI" Or sueldos_y_salarios_str = "SUELDOS" Then
                            sueldos_y_salarios = 1
                        ElseIf sueldos_y_salarios_str = "NO" Then
                            sueldos_y_salarios = 0
                        Else

                            Dim msjErr_sueldos As String = "- Sueldos y Salarios: Favor de colocar un valor válido en sueldos y salarios. Opciones: SI, NO o SUELDOS"
                            errores.Add(msjErr_sueldos)

                        End If


                        If erroresCatalogos.Count > 0 Then
                            Dim mensajeFilaCatalogos As String = "<br />En la fila " & i & " con Nombre: " & nombre & " y CURP: " & curp & ", los siguientes campos son requeridos:<br />" & String.Join("<br />", erroresCatalogos)
                            lineErrorsCatalogos.Add(mensajeFilaCatalogos)
                        End If

                        ' Se valida si no hubo errores en los datos que hacen referencia a un Catalogo
                        If errores.Count > 0 Then
                            Dim mensajeFila As String = "<br />En la fila " & i & " con Nombre: " & nombre & " y CURP: " & curp & ", los siguientes campos son requeridos:<br />" & String.Join("<br />", errores)
                            lineErrors.Add(mensajeFila)

                        Else



                            ' Agregar el empleado a la lista si no hay errores
                            Dim empleado As New With {
                                .clave = clave,
                                .nombre = nombre,
                                .apellido_paterno = apellido_paterno,
                                .apellido_materno = apellido_materno,
                                .sexo = sexoid,
                                .fecha_nacimiento_str = fecha_nacimientoEmp,
                                .rfc = rfc,
                                .curp = curp,
                                .no_imss = no_imss,
                                .calle = calle,
                                .no_ext = no_ext,
                                .no_int = no_int,
                                .colonia = colonia,
                                .municipio = municipioid,
                                .codigo_postal = codigo_postal,
                                .estado = estadoid,
                                .pais = pais,
                                .celular = celular,
                                .email = email,
                                .lugar_nacimiento = lugar_nacimiento,
                                .estado_civil = estadocivilid,
                                .cliente = mi_cliente_id,
                                .sueldos_y_salarios = sueldos_y_salarios,
                                .excedente = excedente,
                                .fecha_alta_str = fecha_altaEmp,
                                .registropatronal = registropatronalid,
                                .departamento = departamentoid,
                                .puesto = puestoid,
                                .salario_base = salario_base,
                                .salario_diario_integrado = salario_diario_integrado,
                                .riesgopuesto = riesgopuestoid,
                                .regimendecontratacion = regimencontratacionid,
                                .periodopago = periodopagoid,
                                .tiponomina = tiponominaid,
                                .regimencontratacionnomina = regimencontratacionnominaid,
                                .tipo_jornadanomina = tipo_jornadanominaid,
                                .tipo_contratonomina = tipo_contratonominaid
                            }

                            empleados.Add(empleado)

                            ' Llamar al método para agregar empleado
                            'AgregarEmpleado(clave, nombre, apellido_paterno, apellido_materno, sexo, fecha_nacimiento_str, rfc, curp, no_imss, calle, no_ext, no_int, colonia, municipio, codigo_postal, estado, pais, celular, email, lugar_nacimiento, estado_civil, cliente, sueldos_y_salarios, excedente, fecha_alta_str, registropatronal, departamento, puesto, salario_base, salario_diario_integrado, riesgopuesto, regimendecontratacion, periodopago, tiponomina, regimencontratacionnomina, tipo_jornadanomina, tipo_contratonomina)
                        End If

                    End If
                Next



                'Agregar empleados con exito y mostrar mensaje
                If empleados.Count > 0 Then

                    Dim errorFlag As Boolean = False ' Inicializamos errorFlag en False

                    ' Si no hay errores, agregar empleados
                    AgregarEmpleado(empleados, errorFlag)

                    If errorFlag Then
                        ' Mostrar alerta en caso de error
                        Dim mensajeErrorEmpleados As String = "Ocurrió un error interno al dar de alta los empleados."
                        rwAlerta.RadAlert(mensajeErrorEmpleados, 330, 180, "Alerta", "", "")
                    Else
                        ' Hacer visible el Panel de éxito
                        panelSuccess.Visible = True
                        lblEmpleadosSuccess.Visible = True
                        lblEmpleadosSuccess.Text = "Se dieron de alta con éxito " & empleados.Count & " empleados de " & empleadosCount & " que se intentaron dar de alta."
                    End If
                End If


                ' Mostrar todos los errores si existen
                If lineErrors.Count > 0 Then

                    Dim erroresTotal As Integer = lineErrors.Count + lineErrorsCatalogos.Count

                    ' Hacer visible el Panel de errores
                    panelErrores.Visible = True

                    ' Hacer visible el Panel de errores
                    lblEmpleadosError.Visible = True

                    ' Crear un mensaje de error que se mostrará en el Literal
                    Dim errores As New StringBuilder()
                    errores.Append("<ul>")
                    For Each error2 As String In lineErrors
                        errores.AppendFormat("<li>{0}</li>", error2)
                    Next
                    errores.Append("</ul>")

                    ' Asignar los errores al Literal
                    litErrores.Text = errores.ToString()


                    ' Establecer el mensaje informativo
                    lblEmpleadosError.Text = "Se obtuvo un error en " & lineErrors.Count & " empleados de " & empleadosCount & " que se intentaron dar de alta."

                End If



                'Mostrar todos los errores si existen
                If lineErrorsCatalogos.Count > 0 Then

                    ' Hacer visible el Panel de errores
                    panelErroresCatalogos.Visible = True

                    ' Crear un mensaje de error que se mostrará en el Literal
                    Dim erroresCatalogos As New StringBuilder()
                    erroresCatalogos.Append("<ul>")
                    For Each error2 As String In lineErrorsCatalogos
                        erroresCatalogos.AppendFormat("<li>{0}</li>", error2)
                    Next
                    erroresCatalogos.Append("</ul>")

                    ' Asignar los errores al Literal
                    litErroresCatalogo.Text = erroresCatalogos.ToString()

                End If


                ' Limpiar las tablas y el DataSet al final del método
                If ds IsNot Nothing Then
                    ' Limpiar cada tabla si hay datos
                    tblPersonalAdministradoTemp.Clear()
                    tblSexoTemp.Clear()
                    tblMunicipioTemp.Clear()
                    tblEstadoTemp.Clear()
                    tblEstadoCivilTemp.Clear()
                    tblMisClientesTemp.Clear()
                    tblRegistroPatronalTemp.Clear()
                    tblDepartamentoClienteTemp.Clear()
                    tblPuestoTemp.Clear()
                    tblRiesgoPuestoTemp.Clear()
                    tblRegimenContratacionTemp.Clear()
                    tblPeriodoPagoTemp.Clear()
                    tblCTipoNominaTemp.Clear()
                    tblRegimenFiscalTemp.Clear()
                    tblCTipoJornadaTemp.Clear()
                    tblCTipoContratoTemp.Clear()

                    ' Limpiar el DataSet en sí
                    ds.Clear()
                End If



            Catch ex As Exception
                Response.Write(ex.Message.ToString)
                rwAlerta.RadAlert(ex.Message.ToString, 330, 180, "Alerta", "", "")
            Finally
                ' Cerrar el archivo y liberar recursos
                If Not worksheet Is Nothing Then
                    Marshal.ReleaseComObject(worksheet)
                End If
                If Not workbook Is Nothing Then
                    workbook.Close(False)
                    Marshal.ReleaseComObject(workbook)
                End If
                excelApp.Quit()
                Marshal.ReleaseComObject(excelApp)
            End Try
        Else
            rwAlerta.RadAlert("Favor de subir el documento de Excel .csv", 330, 180, "Alerta", "", "")
        End If
    End Sub





    Private Sub AgregarEmpleado(empleados As List(Of Object), ByRef errorFlag As Boolean)
        Dim conn As New SqlConnection(Session("conexion"))
        Dim dt As New DataTable()

        Try
            conn.Open()

            For Each empleado In empleados
                ' Usamos parámetros para evitar inyecciones SQL
                Dim sql As String = "pPersonalAdministrado"

                Using cmd As New SqlCommand(sql, conn)
                    cmd.CommandType = CommandType.StoredProcedure

                    ' Asignación de parámetros desde el objeto empleado
                    cmd.Parameters.AddWithValue("@cmd", 29)

                    'Personal Administrado
                    cmd.Parameters.AddWithValue("@clave", empleado.clave)
                    cmd.Parameters.AddWithValue("@nombre", empleado.nombre)
                    cmd.Parameters.AddWithValue("@apellido_paterno", empleado.apellido_paterno)
                    cmd.Parameters.AddWithValue("@apellido_materno", empleado.apellido_materno)
                    cmd.Parameters.AddWithValue("@sexo", empleado.sexo)
                    cmd.Parameters.AddWithValue("@fecha_nacimiento", empleado.fecha_nacimiento_str)
                    cmd.Parameters.AddWithValue("@rfc", empleado.rfc)
                    cmd.Parameters.AddWithValue("@curp", empleado.curp)
                    cmd.Parameters.AddWithValue("@no_imss", empleado.no_imss)
                    cmd.Parameters.AddWithValue("@calle", empleado.calle)
                    cmd.Parameters.AddWithValue("@no_ext", empleado.no_ext)
                    cmd.Parameters.AddWithValue("@no_int", empleado.no_int)
                    cmd.Parameters.AddWithValue("@colonia", empleado.colonia)
                    cmd.Parameters.AddWithValue("@municipioid", empleado.municipio)
                    cmd.Parameters.AddWithValue("@codigo_postal", empleado.codigo_postal)
                    cmd.Parameters.AddWithValue("@estadoid", empleado.estado)
                    cmd.Parameters.AddWithValue("@pais", empleado.pais)
                    cmd.Parameters.AddWithValue("@celular", empleado.celular)
                    cmd.Parameters.AddWithValue("@email1", empleado.email)
                    cmd.Parameters.AddWithValue("@lugar_nacimiento", empleado.lugar_nacimiento)
                    cmd.Parameters.AddWithValue("@estado_civil", empleado.estado_civil)
                    cmd.Parameters.AddWithValue("@mi_cliente_id", empleado.cliente)
                    cmd.Parameters.AddWithValue("@sueldos_y_salarios", empleado.sueldos_y_salarios)
                    cmd.Parameters.AddWithValue("@excedente", empleado.excedente)

                    'Persona Contrato
                    cmd.Parameters.AddWithValue("@fecha_alta", empleado.fecha_alta_str)
                    cmd.Parameters.AddWithValue("@registropatronalid", empleado.registropatronal)
                    cmd.Parameters.AddWithValue("@departamentoid", empleado.departamento)
                    cmd.Parameters.AddWithValue("@puestoid", empleado.puesto)
                    cmd.Parameters.AddWithValue("@salario_base", empleado.salario_base)
                    cmd.Parameters.AddWithValue("@salario_diario_integrado", empleado.salario_diario_integrado)
                    cmd.Parameters.AddWithValue("@riesgopuestoid", empleado.riesgopuesto)
                    cmd.Parameters.AddWithValue("@regimencontratacionid", empleado.regimendecontratacion)
                    cmd.Parameters.AddWithValue("@periodopagoid", empleado.periodopago)
                    cmd.Parameters.AddWithValue("@tiponominaid", empleado.tiponomina)
                    cmd.Parameters.AddWithValue("@regimencontratacionnominaid", empleado.regimencontratacionnomina)
                    cmd.Parameters.AddWithValue("@tipo_jornadanominaid", empleado.tipo_jornadanomina)
                    cmd.Parameters.AddWithValue("@tipo_contratonominaid", empleado.tipo_contratonomina)

                    cmd.ExecuteNonQuery()
                End Using
            Next
        Catch ex As Exception
            ' Mejorar el manejo de errores, loggear de ser necesario
            Response.Write("Error: " & ex.Message.ToString())
            errorFlag = True
        Finally
            conn.Close()
        End Try
    End Sub



    Protected Sub btnDescargarExcel_Click(sender As Object, e As EventArgs) Handles btnDescargarXlsx.Click
        Dim app As Excel.Application = Nothing
        Dim workbook As Excel.Workbook = Nothing
        Dim worksheet As Excel.Worksheet = Nothing
        Dim tempFilePath As String = Path.GetTempPath() & "AltaMasivaEmpleados.xlsx"

        Try
            app = New Excel.Application
            workbook = app.Workbooks.Add()
            worksheet = CType(workbook.Sheets(1), Excel.Worksheet)


            'Campos de tblPersonalAdministrado
            worksheet.Cells(1, 1).Value = "Clave del Empleado"
            worksheet.Cells(1, 2).Value = "Nombre"
            worksheet.Cells(1, 3).Value = "Apellido Paterno"
            worksheet.Cells(1, 4).Value = "Apellido Materno"
            worksheet.Cells(1, 5).Value = "Sexo" 'CATALOGO tblSexo
            worksheet.Cells(1, 6).Value = "Fecha Nacimiento"
            worksheet.Cells(1, 7).Value = "RFC"
            worksheet.Cells(1, 8).Value = "Curp"
            worksheet.Cells(1, 9).Value = "NSS"
            worksheet.Cells(1, 10).Value = "Calle"
            worksheet.Cells(1, 11).Value = "Num Exterior"
            worksheet.Cells(1, 12).Value = "Num Interior"
            worksheet.Cells(1, 13).Value = "Colonia"
            worksheet.Cells(1, 14).Value = "Municipio" 'CATALOGO tblMunicipio
            worksheet.Cells(1, 15).Value = "Codigo Postal"
            worksheet.Cells(1, 16).Value = "Estado" 'CATALOGO tblEstado
            worksheet.Cells(1, 17).Value = "Pais"
            worksheet.Cells(1, 18).Value = "Celular"
            worksheet.Cells(1, 19).Value = "Email"
            worksheet.Cells(1, 20).Value = "Lugar de Nacimiento"
            worksheet.Cells(1, 21).Value = "Estado Civil" 'CATALOGO tblEstadoCivil
            worksheet.Cells(1, 22).Value = "Cliente" 'CATALOGO tblMisClientes
            worksheet.Cells(1, 23).Value = "Sueldos y Salarios"
            worksheet.Cells(1, 24).Value = "Excendente"

            'Campos de tblPersonaContrato
            worksheet.Cells(1, 25).Value = "Fecha de alta"
            worksheet.Cells(1, 26).Value = "Registro Patronal" 'CATALOGO tblRegistroPatronal
            worksheet.Cells(1, 27).Value = "Departamento" 'CATALOGO tblDepartamentoCliente
            worksheet.Cells(1, 28).Value = "Puesto" 'CATALOGO tblPuesto
            worksheet.Cells(1, 29).Value = "SB"
            worksheet.Cells(1, 30).Value = "SDI"
            worksheet.Cells(1, 31).Value = "Riesgo de Puesto" 'CATALOGO tblRiesgoPuesto
            worksheet.Cells(1, 32).Value = "Regimen de contratacion" 'CATALOGO tbltblRegimenContratacion
            worksheet.Cells(1, 33).Value = "Periodo de Pago" 'CATALOGO tblPeriodoPago
            worksheet.Cells(1, 34).Value = "Tipo de Nomina" 'CATALOGO tblCTipoNomina
            worksheet.Cells(1, 35).Value = "Regimen Fiscal" 'CATALOGO tblRegimenFiscal
            worksheet.Cells(1, 36).Value = "Tipo de Jornada" 'CATALOGO tblCTipoContrato
            worksheet.Cells(1, 37).Value = "Tipo de Contrato" 'CATALOGO tblCTipoJornada

            ' Aplicar color de relleno amarillo a las columnas especificadas
            worksheet.Range("B1:E1").Interior.Color = RGB(255, 255, 0) ' Nombre hasta Sexo
            worksheet.Range("F1:I1").Interior.Color = RGB(255, 255, 0) ' Fecha Nacimiento hasta NSS
            worksheet.Range("N1:Q1").Interior.Color = RGB(255, 255, 0) ' Codigo Postal hasta Pais
            worksheet.Range("U1:AK1").Interior.Color = RGB(255, 255, 0) ' Cliente hasta Tipo de contrato


            ' Si el archivo existe, eliminarlo
            If File.Exists(tempFilePath) Then
                File.Delete(tempFilePath)
            End If

            ' Guardar el archivo temporalmente
            workbook.SaveAs(tempFilePath)

        Catch ex As Exception
            ' Manejar el error si es necesario
            Throw ex
        Finally
            ' Liberar objetos COM
            If workbook IsNot Nothing Then
                workbook.Close(False)
                Marshal.ReleaseComObject(workbook)
            End If
            If app IsNot Nothing Then
                app.Quit()
                Marshal.ReleaseComObject(app)
            End If
        End Try

        ' Descargar el archivo
        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
        Response.AddHeader("Content-Disposition", "attachment; filename=AltaMasivaEmpleados.xlsx")
        Response.TransmitFile(tempFilePath)
        Response.Flush()

        ' Eliminar el archivo temporal después de descargarlo
        If File.Exists(tempFilePath) Then
            File.Delete(tempFilePath)
        End If

        Response.End()
    End Sub

End Class
