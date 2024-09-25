Public Class EditorConceptos
    Inherits System.Web.UI.Page
    Dim ObjData As New DataControl()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Request("id") Is Nothing Then
                registroId.Value = 0
                llenaComboTipoConcepto(0)
                llenaComboTipo(0)
            Else
                registroId.Value = Request.QueryString("id").ToString

                Dim cConcepto As New Entities.Concepto
                'cConcepto.IdConcepto = registroId.Value
                cConcepto.ConsultarConceptoID()
                If cConcepto.Cvo > 0 Then
                    'registroId.Value = cConcepto.IdConcepto
                    txtConcepto.Text = cConcepto.Concepto
                    txtClave.Text = cConcepto.Clave
                    llenaComboTipo(cConcepto.Tipo)
                    llenaComboTipoConcepto(cConcepto.Cvo)
                    'txtTipo.Text = cConcepto.Tipo
                End If
            End If
        End If
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim cConcepto As New Entities.Concepto

        If registroId.Value > 0 Then
            'cConcepto.IdConcepto = registroId.Value
            cConcepto.Concepto = txtConcepto.Text.Trim
            cConcepto.Clave = txtClave.Text.Trim
            cConcepto.Tipo = cmbTipo.SelectedValue
            cConcepto.Cvo = cmbTipoConcepto.SelectedValue
            cConcepto.UpdateConcepto()
        Else
            cConcepto.Concepto = txtConcepto.Text.Trim
            cConcepto.Clave = txtClave.Text.Trim
            cConcepto.Tipo = cmbTipo.SelectedValue
            cConcepto.Cvo = cmbTipoConcepto.SelectedValue
            cConcepto.GuadarConcepto()
        End If
        cConcepto = Nothing
        resetControles()
        Response.Redirect("~/Conceptos.aspx")
        'CargarGrid()
    End Sub
    Private Sub resetControles()
        txtConcepto.Text = ""
        txtClave.Text = ""
        cmbTipo.SelectedValue = 0
        'txtTipo.Text = ""
        registroId.Value = 0
    End Sub
    Private Sub llenaComboTipo(ByVal sel As Integer)
        Dim cConcepto As New Entities.Concepto
        objData.Catalogo(cmbTipo, sel, cConcepto.ConsultarTipos())
        cConcepto = Nothing
    End Sub
    Private Sub llenaComboTipoConcepto(ByVal sel As Integer)
        Dim cConcepto As New Entities.Concepto
        objData.Catalogo(cmbTipoConcepto, sel, cConcepto.ConsultarTipoConceptos())
        cConcepto = Nothing
    End Sub
    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Response.Redirect("~/Conceptos.aspx")
    End Sub
End Class