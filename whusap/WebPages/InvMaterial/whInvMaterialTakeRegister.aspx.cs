using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Script.Serialization;
using System.Web.UI.HtmlControls;
using System.Text;
using whusa.Entidades;
using whusa.Interfases;
using System.Configuration;
using System.Threading;
using System.Globalization;
using whusa.Utilidades;

namespace whusap.WebPages.InvMaterial
{
    public partial class whInvMaterialTakeRegister : System.Web.UI.Page
    {
        #region Propiedades

            protected static InterfazDAL_ttwhcol017 idal = new InterfazDAL_ttwhcol017();
            protected static InterfazDAL_ttwhcol016 idal016 = new InterfazDAL_ttwhcol016();
            Ent_ttwhcol017 obj = new Ent_ttwhcol017();
            DataTable resultado = new DataTable();
            string DefaultZone = string.Empty;
            string strError = string.Empty;

            //Manejo idioma
            private static Mensajes _mensajesForm = new Mensajes();
            private static LabelsText _textoLabels = new LabelsText();
            private static string formName;
            private static string globalMessages = "GlobalMessages";
            public static string _idioma;

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
            {
                Type csType = this.GetType();
                ClientScriptManager scriptBlock = Page.ClientScript;

                txtLabelId.Focus();
                this.SetFocus(Page.Form.UniqueID);

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

                    if (Session["ddlIdioma"] != null)
                    {
                        _idioma = Session["ddlIdioma"].ToString();
                    }
                    else
                    {
                        _idioma = "INGLES";
                    }

                    CargarIdioma();

                    //txtLabelId.Attributes.Add("onblur", "if(this.value != ''){ validaInfo(this.value, '1', 'Contenido_" + lblCwar.ID + "', this);}");

                    try { DefaultZone = ConfigurationManager.AppSettings["DefaultZone"].ToString(); }
                    catch { }

                    if (String.IsNullOrEmpty(DefaultZone) && Session["DefaultZone"] == null)
                    {

                    }

                    string strTitulo = mensajes("encabezado");

                    if (Session["IsPreviousPage"] == null) { Session.Clear(); }

                    Label control = (Label)Page.Controls[0].FindControl("lblPageTitle");
                    control.Text = strTitulo;
                    Page.Form.DefaultButton = btnSearch.UniqueID;
                    btnSearch.Enabled = false;
                }
                btnSend.Enabled = false;
                StringBuilder script = new StringBuilder();
                // Crear el script para la ejecucion de la forma
                script.Append("<script type=\"text/javascript\">function button_click(objTextBox,objBtnID) {");
                script.Append("if(window.event.keyCode==13)");
                script.Append("{");
                script.Append("document.getElementById(objBtnID).focus();");
                script.Append("document.getElementById(objBtnID).click();");
                script.Append("}}");
                script.Append("</script>");

                scriptBlock.RegisterClientScriptBlock(csType, "button_click", script.ToString(), false);
                txtLabelId.Attributes.Add("onkeypress", "button_click(this," + this.btnSearch.ClientID + ")");
                txtQuantity.Attributes.Add("onkeypress", "button_click(this," + this.btnSend.ClientID + ")");


            }

        protected void btnSearchZone_Click(object sender, EventArgs e)
            {
                lblError.Text = string.Empty;
                strError = string.Empty;

                Ent_ttwhcol016 objZone = new Ent_ttwhcol016();
                objZone.zone = txtZone.Text.ToUpperInvariant().Trim();
                resultado = idal016.TakeMaterialInv_verificaZona_Param(ref objZone, ref strError);

                if (!string.IsNullOrEmpty(strError))
                {
                    lblZone.Text = mensajes("notexistszone");
                    txtZone.Focus();
                    strError = string.Empty;
                    divTabla.Visible = false;
                    return;
                }

                //lblZone.Text = string.Empty;
                divTabla.Visible = true;
                lblZone.Text = resultado.Rows[0]["DESCRIPCION"].ToString();
                resultado = idal.TakeMaterialInv_verificaBodegaZone_Param(ref objZone, ref strError);

                txtCwar.Text = resultado.Rows[0]["T$CWAR"].ToString();
                lblCwar.Text = resultado.Rows[0]["DESCRIPCION"].ToString();
                btnSearch.Enabled = true;
                Page.Form.DefaultButton = btnSearch.UniqueID;
                txtLabelId.Focus();

            }

        protected void btnSearch_Click(object sender, EventArgs e)
            {
                lblError.Text = string.Empty;
                strError = string.Empty;

                txtItem.Text = string.Empty;
                lblItem.Text = string.Empty;
                txtLote.Text = string.Empty;
                txtQuantity.Text = string.Empty;
                lblUnity.Text = string.Empty;
                hi_unityItem.Value = string.Empty;

                obj = new Ent_ttwhcol017();
                obj.labl = txtLabelId.Text.Trim();
                resultado = idal.TakeMaterialInv_listaConsLabel_Param(ref obj, ref strError);
                btnSend.Enabled = false;
                if (!string.IsNullOrEmpty(strError))
                {
                    lblError.Text = mensajes("labelnotexists");
                    txtLabelId.Focus();
                    strError = string.Empty;
                    return;
                }

                CultureInfo decimalPunto = new CultureInfo("en-US");
                decimalPunto.NumberFormat.NumberDecimalSeparator = ".";


                // txtCwar.Text = resultado.Rows[0]["T$CWAR"].ToString().Trim();
                // lblCwar.Text = resultado.Rows[0]["BODEGA"].ToString();
                int txtKitm = Convert.ToInt32(resultado.Rows[0]["T$KITM"].ToString().Trim());
                txtItem.Text = resultado.Rows[0]["T$ITEM"].ToString().Trim();
                lblItem.Text = resultado.Rows[0]["ARTICULO"].ToString();
                txtLote.Text = resultado.Rows[0]["T$CLOT"].ToString();
                txtQuantity.Text = (Decimal.Parse(resultado.Rows[0]["T$QTYR"].ToString())).ToString(decimalPunto);  //Convert.ToDecimal(txtQuantity.Text);
                lblUnity.Text = hi_unityItem.Value = resultado.Rows[0]["UNIDAD"].ToString();
                hi_unityItem.Value = lblUnity.Text;

                Page.Form.DefaultButton = btnSend.UniqueID;
                txtQuantity.Focus();
                btnSearch.Enabled = true;
                btnSend.Enabled = true;
            }

        protected void btnSend_Click(object sender, EventArgs e)
            {
                obj = new Ent_ttwhcol017();
                obj.labl = txtLabelId.Text.Trim();
                DataTable valida = idal.TakeMaterialInv_listaConsLabel_Param(ref obj, ref strError);
                btnSend.Enabled = false;

                if (!string.IsNullOrEmpty(strError))
                {
                    lblError.Text = String.Format(mensajes("labelnotexists"), obj.labl);
                    txtLabelId.Focus();
                    strError = string.Empty;
                    return;
                }

                lblError.Text = string.Empty;
                strError = string.Empty;

                List<Ent_ttwhcol017> parameterCollection = new List<Ent_ttwhcol017>();

                obj.labl = txtLabelId.Text.Trim().ToUpperInvariant(); ;
                obj.sqnb = 0;
                obj.cwar = txtCwar.Text.ToUpperInvariant();
                obj.item = txtItem.Text.ToUpperInvariant();
                obj.clot = string.IsNullOrEmpty(txtLote.Text.Trim()) ? " " : txtLote.Text.Trim().ToUpperInvariant();
                obj.qtdl = Decimal.Parse(txtQuantity.Text, System.Globalization.CultureInfo.InvariantCulture);  //Convert.ToDecimal(txtQuantity.Text);
                obj.cuni = hi_unityItem.Value;
                obj.logn = Session["user"].ToString();
                obj.coun = 0;
                obj.proc = 2;
                obj.refcntd = 0;
                obj.refcntu = 0;
                obj.zone = txtZone.Text.Trim().ToUpperInvariant();

                parameterCollection.Add(obj);
                int resultado = idal.insertarRegistro(ref parameterCollection, ref strError);
                if (resultado < 1)
                {
                    lblError.Text = mensajes("errorsave");
                    return;
                }

                //txtCwar.Text = string.Empty;
                //lblCwar.Text = string.Empty;
                txtLabelId.Text = string.Empty;
                txtItem.Text = string.Empty;
                lblItem.Text = string.Empty;
                txtLote.Text = string.Empty;
                txtQuantity.Text = string.Empty;
                lblUnity.Text = string.Empty;
                hi_unityItem.Value = string.Empty;


                lblError.Text = mensajes("msjsave");
                Page.Form.DefaultButton = btnSearch.UniqueID;
                txtLabelId.Focus();

                // btnSearchZone.Enabled = false;
                txtZone.Enabled = false;
                btnSend.Enabled = false;
            }

        #endregion

        #region Metodos

        protected void CargarIdioma()
        {
            lblZoneCode.Text = _textoLabels.readStatement(formName, _idioma, "lblZoneCode");
            btnSearchZone.Text = _textoLabels.readStatement(formName, _idioma, "btnSearchZone");
            lblId.Text = _textoLabels.readStatement(formName, _idioma, "lblId");
            btnSearch.Text = _textoLabels.readStatement(formName, _idioma, "btnSearch");
            lblDescWarehouse.Text = _textoLabels.readStatement(formName, _idioma, "lblDescWarehouse");
            lblDescItem.Text = _textoLabels.readStatement(formName, _idioma, "lblDescItem");
            lblDescLotCode.Text = _textoLabels.readStatement(formName, _idioma, "lblDescLotCode");
            lblDescQuantity.Text = _textoLabels.readStatement(formName, _idioma, "lblDescQuantity");
            btnSend.Text = _textoLabels.readStatement(formName, _idioma, "btnSend");
            validateReturn.ErrorMessage = _textoLabels.readStatement(formName, _idioma, "regularQuantity");
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
}//
//