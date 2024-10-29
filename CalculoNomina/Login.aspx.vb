Imports System.Data
Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports Entities

Public Class Login
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'If Not CookieUtil.GetTripleDESEncryptedCookieValue("usuario") Is Nothing Then
            '    chkRemember.Checked = True
            '    txtEmail.Text = CookieUtil.GetTripleDESEncryptedCookieValue("usuario")
            '    txtContrasena.Attributes.Add("value", CookieUtil.GetTripleDESEncryptedCookieValue("contrasena"))
            'End If
        End If
    End Sub
    Protected Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        lblMensaje.Visible = False
        Dim ClienteValido As Boolean = False
        Try
            Dim ObjData As New DataControl()
            Dim ds As New DataSet
            Dim p As New ArrayList
            p.Add(New SqlParameter("@email", txtEmail.Text))
            p.Add(New SqlParameter("@contrasena", txtContrasena.Text))
            ds = ObjData.FillDataSet("pLogin", p)
            ObjData = Nothing

            If ds.Tables(0).Rows.Count > 0 Then
                For Each row As DataRow In ds.Tables(0).Rows
                    If row("error") = 1 Then
                        lblMensaje.Text = row("mensaje")
                        ClienteValido = False
                    ElseIf row("error") = 0 Then
                        'Session("conexion") = "Data Source=" & System.Configuration.ConfigurationManager.AppSettings("server").ToString & "; Initial Catalog=" & row("appKey") & "; Persist Security Info=True; Trusted_Connection=yes; Max Pool Size=360;"
                        'Session("conexion") = "Data Source=" & System.Configuration.ConfigurationManager.AppSettings("server").ToString & "; Initial Catalog=" & row("appKey") & "; User ID=sa; Password=q<PAQkt5(49(8!R; Max Pool Size=360;"
                        'Session("clienteid") = row("clienteid")
                        Session("usuarioid") = row("usuarioid")
                        Session("perfilid") = row("perfilid")
                        Session("nombre") = row("nombre")
                        Session("email") = row("email")

                        'ClienteValido = True
                        'Dim cConfiguracion As New Configuracion
                        'cConfiguracion.IdUsuario = row("usuarioid")
                        'cConfiguracion.GuadarConfiguracion()
                        'cConfiguracion = Nothing

                    Else
                        lblMensaje.Text = "Pónganse en contacto con el administrador de sistema."
                        ClienteValido = False
                    End If
                Next
            End If

        Catch ex As Exception
            Response.Write(ex.ToString)
            Response.End()
        End Try

        If Session("usuarioid") > 0 Then
            Select Case Session("perfilid")
                Case 1
                    Response.Redirect("~/Empresa.aspx")
                Case 3, 23
                    Response.Redirect("~/Empresa.aspx")
                Case Else
                    Response.Redirect("~/Empresa.aspx")
            End Select
        Else
            rwAlerta.RadAlert("Datos de acceso incorrectos.", 330, 180, "Alerta", "", "")
        End If

    End Sub

End Class