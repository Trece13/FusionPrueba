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

namespace whusap.WebPages.InvMaterial
{
    public partial class whInvMaterialTakeRegister : System.Web.UI.Page
    {
        string strError = string.Empty;
        //string Aplicacion = "WEBAPP";
        DataTable resultado = new DataTable();
        protected static InterfazDAL_ttwhcol017 idal = new InterfazDAL_ttwhcol017();
        protected static InterfazDAL_ttwhcol016 idal016 = new InterfazDAL_ttwhcol016();
        Ent_ttwhcol017 obj = new Ent_ttwhcol017();
        string DefaultZone = string.Empty;
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
                //txtLabelId.Attributes.Add("onblur", "if(this.value != ''){ validaInfo(this.value, '1', 'Contenido_" + lblCwar.ID + "', this);}");

                try { DefaultZone = ConfigurationManager.AppSettings["DefaultZone"].ToString(); }
                catch { }

                if (String.IsNullOrEmpty(DefaultZone) && Session["DefaultZone"] == null)
                { 
                
                }

                string strTitulo = "Physical Inventory Take";

                if (Session["IsPreviousPage"] == null) { Session.Clear(); }

                Label control = (Label)Page.Controls[0].FindControl("lblPageTitle");
                control.Text = strTitulo;
                Page.Form.DefaultButton = btnSearch.UniqueID;
            }

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
            btnSearch.Enabled = false;

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
                lblZone.Text = strError;
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
            Page.Form.DefaultButton = btnSearch.UniqueID;
            txtLabelId.Focus();
         
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lblError.Text = string.Empty;
            strError = string.Empty;

            obj = new Ent_ttwhcol017();
            obj.labl = txtLabelId.Text.Trim();
            resultado = idal.TakeMaterialInv_listaConsLabel_Param(ref obj, ref strError);

            if (!string.IsNullOrEmpty(strError))
            {
                lblError.Text = strError;
                txtLabelId.Focus();
                btnSearch.Enabled = false;
                strError = string.Empty;
                return;
            }

            CultureInfo decimalPunto = new CultureInfo("en-US");
            decimalPunto.NumberFormat.NumberDecimalSeparator = ".";


            // txtCwar.Text = resultado.Rows[0]["T$CWAR"].ToString().Trim();
            // lblCwar.Text = resultado.Rows[0]["BODEGA"].ToString();
            txtItem.Text = resultado.Rows[0]["T$ITEM"].ToString().Trim();
            lblItem.Text = resultado.Rows[0]["ARTICULO"].ToString();
            txtLote.Text = resultado.Rows[0]["T$CLOT"].ToString();
            txtQuantity.Text = (Decimal.Parse(resultado.Rows[0]["T$QTYR"].ToString())).ToString(decimalPunto);  //Convert.ToDecimal(txtQuantity.Text);
            lblUnity.Text = hi_unityItem.Value = resultado.Rows[0]["UNIDAD"].ToString();
            hi_unityItem.Value = lblUnity.Text;
        
            Page.Form.DefaultButton = btnSend.UniqueID;
            txtQuantity.Focus();
            btnSearch.Enabled = true;
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            List<Ent_ttwhcol017> parameterCollection = new List<Ent_ttwhcol017>();
            obj = new Ent_ttwhcol017();
            obj.labl = txtLabelId.Text.Trim().ToUpperInvariant();;
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
                lblError.Text = "Error on save";
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


            lblError.Text = "Count has been saved sucessfully";
            Page.Form.DefaultButton = btnSearch.UniqueID;
            txtLabelId.Focus();
            btnSearchZone.Enabled = false;
            txtZone.Enabled = false;
        }

    }
}