using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using whusa.Interfases;
using whusa.Utilidades;
using System.Threading;
using System.Configuration;
using whusa.Entidades;
using System.Globalization;
using System.Web.Configuration;

namespace whusap.WebPages.Migration
{
    public partial class whInvReprintLabelMaterialRejectedM : System.Web.UI.Page
    {
        #region Propiedades
            private static InterfazDAL_ttccol301 _idalttccol301 = new InterfazDAL_ttccol301();
            private static InterfazDAL_tticol101 _idaltticol101 = new InterfazDAL_tticol101();
            private static Mensajes _mensajesForm = new Mensajes();
            private static LabelsText _textoLabels = new LabelsText();
            private static string _operator;
            public static string _idioma;
            private static string strError;
            private static string formName;
            private static string globalMessages = "GlobalMessages";
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

                string strTitulo = mensajes("encabezado");
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


        private string addZero(int MyNum ){
            
            string MyNumReturn = MyNum.ToString(); 

            if(MyNum < 10){
                        MyNumReturn = "00"+ MyNumReturn;
            }
            else if(MyNum >= 10 && MyNum <100){
                MyNumReturn = "0"+ MyNumReturn;
            }

            return MyNumReturn;
        }

        protected void btnConsultar_Click(object sender, EventArgs e) 
        {
            var pdno = txtOrder.Text.Trim().ToUpper();
            var clot = txtLot.Text.Trim().ToUpper();
            var seqn = addZero(Convert.ToInt32(txtSecuence.Text.Trim().ToUpper()));

            if (pdno == String.Empty || clot == String.Empty || seqn == String.Empty)
            {
                lblError.Text = mensajes("formempty");
                return;
            }

            var consultaInformacion = _idaltticol101.findRecordByPdnoClotSeqn(ref pdno, ref clot, ref seqn, ref strError).Rows;

            if (consultaInformacion.Count > 0)
            {
                lblError.Text = "";

                var pono = consultaInformacion[0]["PONO"].ToString().Trim();
                var item = consultaInformacion[0]["ITEM"].ToString().Trim();
                var descitem = consultaInformacion[0]["DSCA"].ToString().Trim();
                var quantity = consultaInformacion[0]["QTYR"].ToString().Trim();
                var unit = consultaInformacion[0]["CUNI"].ToString().Trim();
                var user = consultaInformacion[0]["LOGR"].ToString().Trim();
                var fecha = consultaInformacion[0]["FECHA"].ToString().Trim();
                var machine = consultaInformacion[0]["MCNO"].ToString().Trim();
                var reasons = consultaInformacion[0]["DSCACDIS"].ToString().Trim();
                var comments = consultaInformacion[0]["OBSE"].ToString().Trim();

                lblOrdPonoSeqn.Text = String.Concat(pdno,'-',pono,'-',seqn);

                // imgOrdPonoSeqn
                var rutaServOrdPonoSeqn = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + lblOrdPonoSeqn.Text.Trim().ToUpper() + "&code=Code128&dpi=96";
                imgOrdPonoSeqn.Src = !string.IsNullOrEmpty(lblOrdPonoSeqn.Text) ? rutaServOrdPonoSeqn : "";

                // imgItem
                var rutaServItem = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + item.ToUpper() + "&code=Code128&dpi=96";
                imgItem.Src = !string.IsNullOrEmpty(item) ? rutaServItem : "";

                lblValueDescripcion.Text = descitem;
                lblValueQuantity.Text = quantity;
                lblValueUnit.Text = unit;
                
                //imgQuantity
                var rutaServQuant = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + quantity.ToUpper() + "&code=Code128&dpi=96";
                imgQuantity.Src = !string.IsNullOrEmpty(quantity) ? rutaServQuant : "";

                //imgLot
                var rutaServLot = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + clot.ToUpper() + "&code=Code128&dpi=96";
                imgLot.Src = !string.IsNullOrEmpty(clot) ? rutaServLot : "";

                lblValueWorkOrder.Text = pdno;

                //imgLot
                var rutaServWorkOrder = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + pdno.ToUpper() + "&code=Code128&dpi=96";
                imgWorkOrder.Src = !string.IsNullOrEmpty(pdno) ? rutaServWorkOrder : "";


                lblValuePrintedBy.Text = _operator;
                lblValueFecha.Text = fecha;
                lblValueMachine.Text = machine;
                lblValueReason.Text = reasons;
                lblValueComments.Text = comments;

                divTable.Visible = true;
                divBotones.Visible = true;
            }
            else 
            {
                divBotones.Visible = false;
                divTable.Visible = false;
                lblError.Text = mensajes("errorprint");
                return;
            }
        }

        #endregion

        #region Metodos

        protected void CargarIdioma()
        {
            lblOrder.Text = _textoLabels.readStatement(formName, _idioma, "lblOrder");
            lblSecuence.Text = _textoLabels.readStatement(formName, _idioma, "lblSecuence");
            lblLot.Text = _textoLabels.readStatement(formName, _idioma, "lblLot");
            btnConsultar.Text = _textoLabels.readStatement(formName, _idioma, "btnConsultar");
            lblDmtNumber.Text = _textoLabels.readStatement(formName, _idioma, "lblDmtNumber");
            lblDescription.Text = _textoLabels.readStatement(formName, _idioma, "lblDescription");
            lblDescLot.Text = _textoLabels.readStatement(formName, _idioma, "lblDescLot");
            lblRejected.Text = _textoLabels.readStatement(formName, _idioma, "lblRejected"); ;
            lblWorkOrder.Text = _textoLabels.readStatement(formName, _idioma, "lblWorkOrder");
            lblPrintedBy.Text = _textoLabels.readStatement(formName, _idioma, "lblPrintedBy");
            lblFecha.Text = _textoLabels.readStatement(formName, _idioma, "lblFecha");
            lblMachine.Text = _textoLabels.readStatement(formName, _idioma, "lblMachine"); ;
            lblReason.Text = _textoLabels.readStatement(formName, _idioma, "lblReason");
            lblComments.Text = _textoLabels.readStatement(formName, _idioma, "lblComments");
            btnPrint.InnerText = _textoLabels.readStatement(formName, _idioma, "linkPrint");
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