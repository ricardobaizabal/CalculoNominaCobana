Public Class MasterPage
    Inherits System.Web.UI.MasterPage
    Dim FechaEjercicio As String
    Private Sub form1_Init(sender As Object, e As EventArgs) Handles form1.Init
        If Session("usuarioid") Is Nothing Then
            Session.Abandon()
            Response.Redirect("~/Login.aspx")
        End If
        If Not IsPostBack Then
            Try
                lblUsuario.Text = "Usuario: " & Session("nombre")
                FechaEjercicio = Date.Now.Year
            Catch ex As Exception
            End Try
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("Empresa") = "" Then
                Empresa.Text = ""
            Else
                Dim cEmpresa As New Entities.Empresa
                cEmpresa.ConsultarEjercicioID()
                If cEmpresa.IdEjercicio > 0 Then
                    cEmpresa.Ejercicio = cEmpresa.IdEjercicio
                    cEmpresa.GuadarEjercicio()

                    IdEjercicio.Value = cEmpresa.IdEjercicio
                    Empresa.Text = Session("Empresa") & "  Ejercicio  " & cEmpresa.IdEjercicio
                Else
                    cEmpresa.ConsultarEjercicioID()
                    If cEmpresa.IdEjercicio > 0 Then

                        cEmpresa.Ejercicio = cEmpresa.IdEjercicio
                        cEmpresa.GuadarEjercicio()

                        IdEjercicio.Value = cEmpresa.IdEjercicio
                        Empresa.Text = Session("Empresa") & "  Ejercicio  " & cEmpresa.IdEjercicio
                    End If
                End If

                Dim cConfiguracion As New Entities.Configuracion
                cConfiguracion.IdEmpresa = Session("IdEmpresa")
                cConfiguracion.IdUsuario = Session("usuarioid")
                cConfiguracion.GuadarConfiguracion()
                cConfiguracion = Nothing
                cEmpresa = Nothing
            End If
        End If
    End Sub

End Class