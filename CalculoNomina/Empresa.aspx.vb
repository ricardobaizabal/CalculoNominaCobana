Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports Telerik.Web.UI
Imports System.Web
Imports System.Web.UI
Imports System.Web.Security
Imports System.Web.UI.WebControls
Imports System.Collections.Generic
Imports System.Web.Services
Imports System.Web.Script.Services
Imports Newtonsoft.Json.Linq


Public Class Empresa
    Inherits System.Web.UI.Page

    Private Sub AgregarCliente_Load(sender As Object, e As EventArgs) Handles Me.Load

        Me.wndEmpresa.VisibleOnPageLoad = False

        If Not IsPostBack Then

            Dim cConcepto As New Entities.Catalogos
            Dim objData As New DataControl
            objData.CatalogoRad(cmbEmpresa, "select id, razon_social as razonsocial from tblCliente where isnull(estatusid,0)=1 order by razon_social", True, False)
            objData.CatalogoRad(tipoContribuyenteid, cConcepto.ConsultarContribuyente, True, False)
            objData.CatalogoRad(estadoid, cConcepto.ConsultarEstado, True, False)
            objData = Nothing
            cConcepto = Nothing

            If Session("IdEmpresa") Is Nothing Then
                Me.wndEmpresa.VisibleOnPageLoad = True
            Else
                Dim cEmpresa As New Entities.Empresa
                cEmpresa.IdEmpresa = Session("IdEmpresa")
                cEmpresa.ConsultarEmpresaID()
                rwAlerta.RadAlert("<span style=""font-weight: 500;"">SELECCIONASTE LA EMPRESA:</span> <span style=""font-weight: 700; text-transform: uppercase;"">" & cEmpresa.Nombre.ToString() & "</span>", 330, 180, "Alerta", "", "")

                Call CargarCliente()

            End If
        End If

    End Sub

    Private Sub CargarCliente()
        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmd As New SqlCommand("EXEC pCliente @cmd=3, @clienteid='" & Session("IdEmpresa") & "'", conn)

        conn.Open()

        Dim rs As SqlDataReader
        rs = cmd.ExecuteReader()

        If rs.Read Then
            Session("Empresa") = rs("razonsocial")
            Session("representante_legal") = rs("representante_legal")
            txtSocialReason.Text = rs("razonsocial")
            txtNombreComercial.Text = rs("nombre_comercial")
            txtContact.Text = rs("contacto")
            txtContactEmail.Text = rs("email_contacto")
            txtContactPhone.Text = rs("telefono_contacto")
            txtStreet.Text = rs("fac_calle")
            txtExtNumber.Text = rs("fac_num_ext")
            txtIntNumber.Text = rs("fac_num_int")
            txtColony.Text = rs("fac_colonia")
            txtCountry.Text = rs("fac_pais")
            txtTownship.Text = rs("fac_municipio")
            txtZipCode.Text = rs("fac_cp")
            txtRFC.Text = rs("rfc")
            estadoid.SelectedValue = rs("fac_estadoid")
            tipoContribuyenteid.SelectedValue = rs("contribuyenteid")
            lblLlavesPrivadas.Text = rs("llave_privada")
            lblCertificados.Text = rs("certificado")
            txtRegistroPatronal.Text = rs("registro_patronal")
            txtContrasena.Text = rs("contrasena")
            txtRepresentanteLegal.Text = rs("representante_legal")
        End If

        conn.Close()
        conn.Dispose()

    End Sub

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function MuestraMenu() As String

        Dim oResponse As New JObject()

        Try
            Dim JEmpresas As New JArray()

            If HttpContext.Current.Session("IdEmpresa") = 0 Then
                Dim JEmpresa As New JObject()
                JEmpresa.Add(New JProperty("IdEmpresa", 0))
                JEmpresa.Add(New JProperty("Nombre", ""))
                JEmpresa.Add(New JProperty("Activo", 0))
                JEmpresas.Add(JEmpresa)
            Else
                Dim cMaster As New Entities.Master
                cMaster.IdEmpresa = HttpContext.Current.Session("IdEmpresa")
                cMaster.ConsultarEmpresaID()
                If cMaster.IdEmpresa > 0 Then
                    Dim JEmpresa As New JObject()
                    JEmpresa.Add(New JProperty("IdEmpresa", cMaster.IdEmpresa))
                    JEmpresa.Add(New JProperty("Nombre", cMaster.Nombre))
                    JEmpresa.Add(New JProperty("Activo", cMaster.Activo))
                    JEmpresas.Add(JEmpresa)
                End If
            End If

            oResponse.Add(New JProperty("Empresa", JEmpresas))
            oResponse.Add(New JProperty("Error", 0))
        Catch ex As Exception
            oResponse.Add(New JProperty("Error", 1))
            oResponse.Add(New JProperty("Descripcion", ex.Message.ToString))
        End Try

        Return oResponse.ToString

    End Function

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)

        Try
            Dim archivoSys As String = ""
            archivoSys = Path.GetFileName(Documento1.FileName)

            Dim archivoSys2 As String = ""
            archivoSys2 = Path.GetFileName(Documento2.FileName)

            If archivoSys = "" Then
                archivoSys = lblLlavesPrivadas.Text
            End If

            If archivoSys2 = "" Then
                archivoSys2 = lblCertificados.Text
            End If

            Dim cmd As New SqlCommand("EXEC pCliente @cmd=4, @clienteid='" & Session("IdEmpresa") & "', @razonsocial='" & txtSocialReason.Text & "', @nombre_comercial='" & txtNombreComercial.Text & "', @contacto='" & txtContact.Text &
                                          "', @email_contacto='" & txtContactEmail.Text & "', @telefono_contacto='" & txtContactPhone.Text &
                                          "', @fac_calle='" & txtStreet.Text & "', @fac_num_int='" & txtIntNumber.Text & "', @fac_num_ext='" & txtExtNumber.Text &
                                          "', @fac_colonia='" & txtColony.Text & "',@contrasena='" & txtContrasena.Text & "', @fac_pais='" & txtCountry.Text & "', @fac_municipio='" & txtTownship.Text &
                                          "', @fac_estadoid='" & estadoid.SelectedValue.ToString & "', @fac_cp='" & txtZipCode.Text & "', @fac_rfc='" & txtRFC.Text &
                                          "', @registro_patronal='" & txtRegistroPatronal.Text & "', @llave_privada='" & archivoSys &
                                          "', @certificado='" & archivoSys2 & "', @contribuyenteid='" & tipoContribuyenteid.SelectedValue.ToString & "', @representante_legal='" & txtRepresentanteLegal.Text & "'", conn)

            conn.Open()

            cmd.ExecuteReader()

            conn.Close()
            conn.Dispose()

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
            Response.End()
        Finally
            conn.Close()
            conn.Dispose()
        End Try

        Call CargarCliente()

        rwAlerta.RadAlert("Datos actualizados", 330, 180, "Alerta", "", "")
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Response.Redirect("~/Empleado.aspx")
    End Sub

    Private Sub btnGuardarEmpresa_Click(sender As Object, e As EventArgs) Handles btnGuardarEmpresa.Click

        Dim cEmpresa As New Entities.Empresa
        cEmpresa.IdEmpresa = cmbEmpresa.SelectedValue
        cEmpresa.ConsultarEmpresaID()

        If cEmpresa.IdEmpresa > 0 Then

            Session("IdEmpresa") = cEmpresa.IdEmpresa.ToString()
            Session("Empresa") = cEmpresa.Nombre.ToString()

            Dim cConfiguracion As New Entities.Configuracion
            cConfiguracion.IdEmpresa = Session("IdEmpresa")
            cConfiguracion.IdUsuario = Session("usuarioid")
            cConfiguracion.GuadarConfiguracion()
            cConfiguracion = Nothing

            rwAlerta.RadAlert("<span style=""font-weight: 500;"">SELECCIONASTE LA EMPRESA:</span> <span style=""font-weight: 700; text-transform: uppercase;"">" & cEmpresa.Nombre.ToString() & "</span>", 330, 180, "Alerta", "", "")

            Call CargarCliente()

        End If
        cEmpresa = Nothing
        'Response.Redirect("~/Empresa.aspx")
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Me.wndEmpresa.VisibleOnPageLoad = False
    End Sub

End Class