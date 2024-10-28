'------------------------------------------------------------------------------
' <generado automáticamente>
'     Este código fue generado por una herramienta.
'
'     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
'     se vuelve a generar el código. 
' </generado automáticamente>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Partial Public Class FiniquitosGeneradosMensual

    '''<summary>
    '''Control registroId.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents registroId As Global.System.Web.UI.WebControls.HiddenField

    '''<summary>
    '''Control empleadoId.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents empleadoId As Global.System.Web.UI.WebControls.HiddenField

    '''<summary>
    '''Control serie.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents serie As Global.System.Web.UI.WebControls.HiddenField

    '''<summary>
    '''Control folio.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents folio As Global.System.Web.UI.WebControls.HiddenField

    '''<summary>
    '''Control lblEjercicio.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblEjercicio As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control cmbPeriodo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents cmbPeriodo As Global.Telerik.Web.UI.RadComboBox

    '''<summary>
    '''Control lnkReporte.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lnkReporte As Global.System.Web.UI.WebControls.LinkButton

    '''<summary>
    '''Control btnGenerarNominaElectronica.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnGenerarNominaElectronica As Global.Telerik.Web.UI.RadButton

    '''<summary>
    '''Control btnTimbrarNomina.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnTimbrarNomina As Global.Telerik.Web.UI.RadButton

    '''<summary>
    '''Control btnGenerarPDF.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnGenerarPDF As Global.Telerik.Web.UI.RadButton

    '''<summary>
    '''Control btnEnvioComprobantes.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnEnvioComprobantes As Global.Telerik.Web.UI.RadButton

    '''<summary>
    '''Control RadProgressManager1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RadProgressManager1 As Global.Telerik.Web.UI.RadProgressManager

    '''<summary>
    '''Control RadProgressArea1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RadProgressArea1 As Global.Telerik.Web.UI.RadProgressArea

    '''<summary>
    '''Control panelDatos.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents panelDatos As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Control grdEmpleados.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents grdEmpleados As Global.Telerik.Web.UI.RadGrid

    '''<summary>
    '''Control rwAlerta.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents rwAlerta As Global.Telerik.Web.UI.RadWindowManager

    '''<summary>
    '''Control rwConfirm.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents rwConfirm As Global.Telerik.Web.UI.RadWindowManager

    '''<summary>
    '''Control RadWindowManager1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RadWindowManager1 As Global.Telerik.Web.UI.RadWindowManager

    '''<summary>
    '''Control wndReporte.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents wndReporte As Global.Telerik.Web.UI.RadWindow

    '''<summary>
    '''Control btnConfirmarGeneraNominaElectronica.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnConfirmarGeneraNominaElectronica As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control btnConfirmarTimbraNomina.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnConfirmarTimbraNomina As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control btnConfirmarGeneraPDF.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnConfirmarGeneraPDF As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control btnConfirmarEnvioPDF.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnConfirmarEnvioPDF As Global.System.Web.UI.WebControls.Button
End Class
