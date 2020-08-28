using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using whusa.Interfases;
using whusa.Utilidades;
using whusa.Entidades;
using System.Configuration;
using System.Globalization;
using System.Threading;
using System.Web.Configuration;

namespace whusap.WebPages.Migration
{
    public partial class whInvReprintLabelMaterialRejectedD : System.Web.UI.Page
    {
        #region Propiedades
            private static InterfazDAL_tticol118 _idaltticol118 = new InterfazDAL_tticol118();
            private static InterfazDAL_ttccol301 _idalttccol301 = new InterfazDAL_ttccol301();
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

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            var item = txtItem.Text.Trim().ToUpper();
            var lot = txtLot.Text.Trim().ToUpper();
            var cwar = txtWarehouse.Text.Trim().ToUpper();
            var qtyr = txtQuantity.Text.Trim().ToUpper();

            if (item == String.Empty || lot == String.Empty || cwar == String.Empty || qtyr == String.Empty)
            {
                lblError.Text = mensajes("formempty");
                return;
            }

            Ent_tticol118 data118 = new Ent_tticol118() 
            {
                item = item,
                clot = lot,
                cwar = cwar,
                qtyr = Convert.ToInt32(qtyr)
            };

            var consultaInformacion = _idaltticol118.findRecordByItemClotCwarQtyr(ref data118, ref strError);

            if (consultaInformacion.Rows.Count > 0)
            {
                //imgItem
                var rutaServItem = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + item.ToUpper() + "&code=Code128&dpi=96";
                imgItem.Src = !string.IsNullOrEmpty(item) ? rutaServItem : "";

                lblItemTab.Text = consultaInformacion.Rows[0]["DSCA"].ToString().Trim().ToUpper();

                //imgItem
                var rutaServQuant = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + qtyr.ToUpper() + "&code=Code128&dpi=96";
                imgTotalQyt.Src = !string.IsNullOrEmpty(item) ? rutaServQuant : "";

                lblValueTotalQty.Text = qtyr;
                lblValueUnit.Text = consultaInformacion.Rows[0]["CUNI"].ToString().Trim().ToUpper();

                lblValuePrintedBy.Text = _operator;
                lblValueDate.Text = DateTime.Now.ToString();

                divTable.Visible = true;
                divBotones.Visible = true;
                lblError.Text = String.Empty;
                lblConfirm.Text = String.Empty;
            }
            else 
            {
                lblError.Text = mensajes("Order doesn't register to MRB.");
                return;
            }
        }

        #endregion

        #region Metodos

        protected void CargarIdioma()
        {
            lblItem.Text = _textoLabels.readStatement(formName, _idioma, "lblItem");
            lblLot.Text = _textoLabels.readStatement(formName, _idioma, "lblLot");
            lblWarehouse.Text = _textoLabels.readStatement(formName, _idioma, "lblWarehouse");
            lblQuantity.Text = _textoLabels.readStatement(formName, _idioma, "lblQuantity");
            btnConsultar.Text = _textoLabels.readStatement(formName, _idioma, "btnConsultar");
            lblTotalQty.Text = _textoLabels.readStatement(formName, _idioma, "lblTotalQty");
            lblUnit.Text = _textoLabels.readStatement(formName, _idioma, "lblUnit");
            lblWorkOrder.Text = _textoLabels.readStatement(formName, _idioma, "lblWorkOrder");
            lblDate.Text = _textoLabels.readStatement(formName, _idioma, "lblDate");
            lblPrintedBy.Text = _textoLabels.readStatement(formName, _idioma, "lblPrintedBy");
            lblCopy.Text = _textoLabels.readStatement(formName, _idioma, "lblPrintedBy");
            btnPrint.InnerText = _textoLabels.readStatement(formName, _idioma, "btnPrint");
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