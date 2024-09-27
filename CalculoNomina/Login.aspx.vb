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
            Dim ObjData As New DataControl(0)
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
                        'Session("conexion") = "Data Source=localhost; Initial Catalog=" & row("appKey") & "; Persist Security Info=True; Trusted_Connection=yes; Max Pool Size=360;"
                        'Session("conexion") = "Data Source=U7T24V; Initial Catalog=" & row("appKey") & "; Persist Security Info=True; Trusted_Connection=yes; Max Pool Size=360;"
                        'Session("conexion") = "Data Source=.\SQLEXPRESS03; Initial Catalog=" & row("appKey") & "; Persist Security Info=True; Trusted_Connection=yes; Max Pool Size=360;"
                        Session("conexion") = "Data Source=" & System.Configuration.ConfigurationManager.AppSettings("server").ToString & "; Initial Catalog=" & row("appKey") & "; Persist Security Info=True; Trusted_Connection=yes; Max Pool Size=360;"

                        Session("clienteid") = row("clienteid")
                        Session("usuarioid") = row("usuarioid")
                        Session("perfilid") = row("perfilid")
                        Session("nombre") = row("nombre")

                        ClienteValido = True

                        Dim cConfiguracion As New Configuracion
                        cConfiguracion.IdUsuario = row("usuarioid")
                        cConfiguracion.GuadarConfiguracion()
                        cConfiguracion = Nothing

                        'Dim DataControl As New DataControl(1)
                        '    ds = New DataSet
                        '    p.Clear()
                        '    p.Add(New SqlParameter("@cmd", 1))
                        '    p.Add(New SqlParameter("@email", txtEmail.Text))
                        '    p.Add(New SqlParameter("@contrasena", txtContrasena.Text))
                        '    ds = DataControl.FillDataSet("pConfiguracion", p)
                        '    DataControl = Nothing

                        '    If ds.Tables(0).Rows.Count > 0 Then
                        '        For Each r As DataRow In ds.Tables(0).Rows
                        '            ClienteValido = True
                        '            Session("admin") = r("admin")
                        '            Session("razonsocial") = r("razonsocial")
                        '            Session("contacto") = r("contacto")
                        '            Session("logo") = r("logo")
                        '            Session("logo_formato") = r("logo_formato")
                        '        Next
                        'Response.Redirect("~/portalcfd/Home.aspx")
                        '        If chkRemember.Checked = True Then
                        '            CookieUtil.SetTripleDESEncryptedCookie("email", txtEmail.Text, Now.AddDays(30))
                        '            CookieUtil.SetTripleDESEncryptedCookie("contrasena", txtContrasena.Text, Now.AddDays(30))
                        '        Else
                        '            CookieUtil.SetTripleDESEncryptedCookie("email", "", Now.AddDays(-1))
                        '            CookieUtil.SetTripleDESEncryptedCookie("contrasena", "", Now.AddDays(-1))
                        '        End If
                        'End If

                    Else
                        lblMensaje.Text = "Pongase en contacto con el administrador"
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
                    Response.Redirect("~/Empleado.aspx")
                Case 3, 23
                    Response.Redirect("~/Empleado.aspx")
                Case Else
                    Response.Redirect("~/Empleado.aspx")
            End Select
        Else
            rwAlerta.RadAlert("Datos de acceso incorrectos.", 330, 180, "Alerta", "", "")
        End If

    End Sub

End Class