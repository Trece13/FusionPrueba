using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using whusa.Interfases;
using whusa.Utilidades;
using System.Threading;
using System.Globalization;
using System.Configuration;
using whusa.Entidades;
using System.Data;
using System.Web.Configuration;

namespace whusap.WebPages.Migration
{
    public partial class whInvReprintLabelDmt : System.Web.UI.Page
    {
        #region Propiedades
            private static InterfazDAL_tticol100 _idaltticol100 = new InterfazDAL_tticol100();
            private static InterfazDAL_tticol101 _idaltticol101 = new InterfazDAL_tticol101();
            private static InterfazDAL_ttccol301 _idalttccol301 = new InterfazDAL_ttccol301();
            private static Mensajes _mensajesForm = new Mensajes();
            private static LabelsText _textoLabels = new LabelsText();
            private static string _operator;
            public static string _idioma;
            private static string strError;
            private static string formName;
            private static string globalMessages = "GlobalMessages";
            public static string _tipoFormulario;
            public string UrlBaseBarcode = WebConfigurationManager.AppSettings["UrlBaseBarcode"].ToString();
        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            // Cambiar cultura para manejo de separador decimal
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-CO");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-CO");
            base.InitializeCulture();

            if (!IsPostBack)
            {
                formName = Request.Url.AbsoluteUri.Split('/').Last();
                if (formName.Contains('?'))
                {
                    formName = formName.Split('?')[0];
                }
                Label control = (Label)Page.Controls[0].FindControl("lblPageTitle");
                lblError.Text = "";
                lblConfirm.Text = "";

                if (Session["user"] == null)
                {
                    Response.Redirect(ConfigurationManager.AppSettings["UrlBase"] + "/WebPages/Login/whLogIni.aspx");
                }

                _operator = Session["user"].ToString();

                try
                {
                    _idioma = Session["ddlIdioma"].ToString();
                }
                catch (Exception)
                {
                    _idioma = "INGLES";
                }

                CargarIdioma();

                if (Request.QueryString["tipoFormulario"] != null)
                {
                    _tipoFormulario = Request.QueryString["tipoFormulario"].ToString().ToUpper();
                }
                else
                {
                    _tipoFormulario = "FINISH";
                }

                string strTitulo = mensajes(_tipoFormulario == "FINISH" ? "encabezadofinish" : "encabezadoraw");
                control.Text = strTitulo;

                Ent_ttccol301 data = new Ent_ttccol301()
                {
                    user = _operator,
                    come = strTitulo,
                    refcntd = 0,
                    refcntu = 0
                };

                List<Ent_ttccol301> datalog = new List<Ent_ttccol301>();
                datalog.Add(data);

                _idalttccol301.insertarRegistro(ref datalog, ref strError);
            }
        }

        protected void btnConsultar_Click(object sender, EventArgs e) 
        {
            var pdno = txtWorkOrder.Text.Trim().ToUpper();
            var seqn = txtSequence.Text.Trim().ToUpper();
            var comparative = String.Empty;

            if (pdno == String.Empty || seqn == String.Empty)
            {
                lblError.Text = mensajes("formempty");
                return;
            }

            DataTable consultaInformacion = new DataTable();

            if (_tipoFormulario == "FINISH")
            {
                comparative = "=";
            }
            else 
            {
                comparative = "<>";
            }

            consultaInformacion = _idaltticol100.findRecordByPdnoSeqnAndPono(ref pdno, ref seqn, ref comparative, ref strError);

            if (consultaInformacion.Rows.Count <= 0)
            {
                consultaInformacion = _idaltticol101.findRecordByPdnoSeqnAndPono(ref pdno, ref seqn, ref comparative, ref strError);
            }

            if (consultaInformacion.Rows.Count <= 0)
            {
                lblError.Text = mensajes("ordernotexists");
                return;
            }

            //data print
            if (_tipoFormulario == "FINISH")
            {
                lblOrdPonoSeqn.Text = String.Concat(pdno, "-", "0", "-", seqn);

                // imgOrdPonoSeqn
                var rutaServOrdPonoSeqn = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + lblOrdPonoSeqn.Text.Trim().ToUpper() + "&code=Code128&dpi=96";
                imgOrdPonoSeqn.Src = !string.IsNullOrEmpty(lblOrdPonoSeqn.Text) ? rutaServOrdPonoSeqn : "";

                //////////////////////

                //var rutaServOrdPonoSeqn = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + lblOrdPonoSeqn.Text.Trim().ToUpper() + "&code=Code128&dpi=96";
                //imgOrdPonoSeqn.Src = !string.IsNullOrEmpty(lblOrdPonoSeqn.Text) ? rutaServOrdPonoSeqn : "";
                ////

                //var rutaServOrdPonoSeqn = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + lblOrdPonoSeqn.Text.Trim().ToUpper() + "&code=Code128&dpi=96";
                //imgOrdPonoSeqn.Src = !string.IsNullOrEmpty(lblOrdPonoSeqn.Text) ? rutaServOrdPonoSeqn : "";



                lblValueProductCode2.Text = consultaInformacion.Rows[0]["ITEM"].ToString().Trim().ToUpper();
                lblValueDescripcion.Text = consultaInformacion.Rows[0]["DSCA"].ToString().Trim().ToUpper(); ;

                // imgItem
                var rutaServItem = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + consultaInformacion.Rows[0]["ITEM"].ToString().Trim() + "&code=Code128&dpi=96";
                imgItem.Src = !string.IsNullOrEmpty(consultaInformacion.Rows[0]["ITEM"].ToString().Trim()) ? rutaServItem : "";

                

                

                lblValueQuantity.Text = consultaInformacion.Rows[0]["QTYR"].ToString().Trim().ToUpper();
                // imgItem
                var ValueQuantity = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + lblValueQuantity.Text.Trim() + "&code=Code128&dpi=96";
                imgQuantity.Src = !string.IsNullOrEmpty(lblValueQuantity.Text.Trim()) ? ValueQuantity : ""; 

                lblValueUnit.Text = consultaInformacion.Rows[0]["UNIT"].ToString().Trim().ToUpper();
                // imgItem
                var ValueUnit = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + lblValueUnit.Text.Trim() + "&code=Code128&dpi=96";
                imgWorkOrder.Src = !string.IsNullOrEmpty(lblValueUnit.Text.Trim()) ? ValueUnit : "";

                //lblValueOperCom.Text = consultaInformacion.Rows[0]["MCNO"].ToString().Trim().ToUpper();
                //lblValueDescripcion.Text = consultaInformacion.Rows[0]["ITEM"].ToString().Trim().ToUpper();
                //lblValueTotalQty.Text = consultaInformacion.Rows[0]["QTYR"].ToString().Trim().ToUpper();
                lblValueWorkOrder.Text = consultaInformacion.Rows[0]["PDNO"].ToString().Trim().ToUpper();
                //lblValueProductDescription.Text = consultaInformacion.Rows[0]["DSCA"].ToString().Trim().ToUpper();
                //lblValueDate.Text = DateTime.Now.ToString();
                lblValuePrintedBy.Text = _operator;
                lblValueReason.Text = consultaInformacion.Rows[0]["DSCACDIS"].ToString().Trim().ToUpper();
                //lblValueObs.Text = consultaInformacion.Rows[0]["OBSE"].ToString().Trim().ToUpper();

                LblMachineId.Text = consultaInformacion.Rows[0]["MCNO"].ToString().Trim().ToUpper(); ;
                LblDispositionValue.Text = "Review" ;
                lblValueComments.Text = consultaInformacion.Rows[0]["OBSE"].ToString().Trim().ToUpper(); ;

                divTableFinish.Visible = true;
                divTableRaw.Visible = false;
                divBotones.Visible = true;





            }
            else 
            {
                lblOrderSeqn2.Text = String.Concat(pdno, "-", "0", "-", seqn);
                lblValueProductCode2.Text = consultaInformacion.Rows[0]["ITEM"].ToString().Trim().ToUpper();
                lblValueProductDescription2.Text = consultaInformacion.Rows[0]["DSCA"].ToString().Trim().ToUpper();
                lblValueDate2.Text = DateTime.Now.ToString();
                lblValuePrintedBy2.Text = _operator;
                lblValueWorkOrder2.Text = consultaInformacion.Rows[0]["PDNO"].ToString().Trim().ToUpper();
                lblValueInternalMaterial.Text = consultaInformacion.Rows[0]["CLOT"].ToString().Trim().ToUpper();
                lblValueReason2.Text = consultaInformacion.Rows[0]["DSCACDIS"].ToString().Trim().ToUpper();
                lblValueObs2.Text = consultaInformacion.Rows[0]["OBSE"].ToString().Trim().ToUpper();

                divTableFinish.Visible = false;
                divTableRaw.Visible = true;
                divBotones.Visible = true;
            }
        }

        #endregion

        #region Metodos

        protected void CargarIdioma()
        {
            lblWorkOrder.Text = _textoLabels.readStatement(formName, _idioma, "lblWorkOrder");
            lblSequence.Text = _textoLabels.readStatement(formName, _idioma, "lblSequence");
            btnConsultar.Text = _textoLabels.readStatement(formName, _idioma, "btnConsultar");
            //lblDefectiveMaterial.Text = _textoLabels.readStatement(formName, _idioma, "lblDefectiveMaterial");
            lblDefectiveMaterial2.Text = _textoLabels.readStatement(formName, _idioma, "lblDefectiveMaterial");
            lblDmtNumber.Text = _textoLabels.readStatement(formName, _idioma, "lblDmtNumber");
            lblDmtNumber2.Text = _textoLabels.readStatement(formName, _idioma, "lblDmtNumber");
            //lblInhouse.Text = _textoLabels.readStatement(formName, _idioma, "lblInhouse");
            lblPurchased.Text = _textoLabels.readStatement(formName, _idioma, "lblPurchased");
            //lblOperCom.Text = _textoLabels.readStatement(formName, _idioma, "lblOperCom");
            //lblProductCode.Text = _textoLabels.readStatement(formName, _idioma, "lblProductCode");
            lblProductCode2.Text = _textoLabels.readStatement(formName, _idioma, "lblProductCode");
            //lblTotalQty.Text = _textoLabels.readStatement(formName, _idioma, "lblTotalQty");
            //lblWorkOrderFinish.Text = _textoLabels.readStatement(formName, _idioma, "lblWorkOrder");
            lblWorkOrder2.Text = _textoLabels.readStatement(formName, _idioma, "lblWorkOrder");
            //lblProductDescription.Text = _textoLabels.readStatement(formName, _idioma, "lblProductDescription");
            lblProductDescription2.Text = _textoLabels.readStatement(formName, _idioma, "lblProductDescription");
            //lblDate.Text = _textoLabels.readStatement(formName, _idioma, "lblDate");
            lblDate2.Text = _textoLabels.readStatement(formName, _idioma, "lblDate");
            lblPrintedBy.Text = _textoLabels.readStatement(formName, _idioma, "lblPrintedBy");
            lblPrintedBy2.Text = _textoLabels.readStatement(formName, _idioma, "lblPrintedBy");
            lblReason.Text = _textoLabels.readStatement(formName, _idioma, "lblReason");
            lblReason2.Text = _textoLabels.readStatement(formName, _idioma, "lblReason");
            lblInternalMaterial.Text = _textoLabels.readStatement(formName, _idioma, "lblInternalMaterial");
            btnPrint.InnerText = _textoLabels.readStatement(formName, _idioma, "btnPrint");
            ///Lbl jc
            lblDescription.Text = _textoLabels.readStatement(formName, _idioma, "lblValueDescripcion");
            //-------------------------------------
            lblMachineTitle.Text = _textoLabels.readStatement(formName, _idioma, "lblMachineTitle");
            //LblMachineId.val();
            //
            lblRejected.Text = _textoLabels.readStatement(formName, _idioma, "lblRejected");
            //--------------------------------------
            LblDisposition.Text = _textoLabels.readStatement(formName, _idioma, "LblDisposition");
            //LblDispositionValue.val();
            //
            LblWorkOrderTitle.Text = _textoLabels.readStatement(formName, _idioma, "lblWorkOrder");
            //---------------------------------------
            lblValueFecha.Text = _textoLabels.readStatement(formName, _idioma, "lblDate");
            //---------------------------------------
            lblReason.Text = _textoLabels.readStatement(formName, _idioma, "lblReason");
            //---------------------------------------
            lblComments.Text = _textoLabels.readStatement(formName, _idioma, "lblComments");


        }

        protected string mensajes(string tipoMensaje)
        {
            var retorno = _mensajesForm.readStatement(formName, _idioma, ref tipoMensaje);

            if (retorno.Trim() == String.Empty)
            {
                retorno = _mensajesForm.readStatement(globalMessages, _idioma, ref tipoMensaje);
            }

            return retorno;
        }

        #endregion
    }
}