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
using System.Configuration;
using System.Globalization;
using System.Threading;
using whusa.Entidades;
using whusa.Interfases;
using whusa.Utilidades;

namespace whusap.WebPages.InvMaterial
{
    public partial class whInvWrapValidation : System.Web.UI.Page
    {
        #region Propiedades

        protected static InterfazDAL_tticol022 idal = new InterfazDAL_tticol022();
        Ent_tticol022 obj022 = new Ent_tticol022();
        DataTable resultado = new DataTable();
        string strError = string.Empty;

        //Manejo idioma
        public string therecordhasbeenupdatedsuccessfully = string.Empty;
        private static Mensajes _mensajesForm = new Mensajes();
        private static LabelsText _textoLabels = new LabelsText();
        private static string formName;
        private static string globalMessages = "GlobalMessages";
        public static string _idioma;

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

                if (Session["ddlIdioma"] != null)
                {
                    _idioma = Session["ddlIdioma"].ToString();
                }
                else
                {
                    _idioma = "INGLES";
                }

                CargarIdioma();

                // Determinar existencia de las maquinas en web.config
                lblError.Visible = false;


                if (Session["IsPreviousPage"] == null) { Session.Clear(); }

                string strTitulo = mensajes("encabezado");
                Label control = (Label)Page.Controls[0].FindControl("lblPageTitle");
                control.Text = strTitulo;
                Page.Form.DefaultButton = btnSend.UniqueID;

            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            lblError.Visible = true;
            obj022 = new Ent_tticol022();
            strError = string.Empty;
            obj022.sqnb = txtSQNB.Text.Trim().ToUpperInvariant();  //hidden.Value;
            resultado = idal.wrapValidation_listaRegistroSec_param(ref obj022, ref strError);

            if (resultado.Rows.Count < 1)
            {
                lblError.Text = String.Format(mensajes("notfounditems"), obj022.sqnb.Trim());
                lblError.ForeColor = System.Drawing.Color.Red;
                //lblError.CssClass = "errorMsg";
                return;
            }
            List<Ent_tticol020> parameterCollection020 = new List<Ent_tticol020>();
            List<Ent_tticol022> parameterCollection022 = new List<Ent_tticol022>();


            obj022.cuni = resultado.Rows[0]["UNIDAD"].ToString();
            obj022.pdno = resultado.Rows[0]["ORDEN"].ToString().Trim();
            obj022.sqnb = resultado.Rows[0]["SECUENCIA"].ToString().Trim();
            obj022.mitm = resultado.Rows[0]["ITEM"].ToString().Trim();
            obj022.qtdl = Convert.ToDecimal(resultado.Rows[0]["PESO"].ToString());
            obj022.logn = resultado.Rows[0]["USUARIO"].ToString().Trim();  //Session["user"].ToString();
            obj022.proc = int.Parse(resultado.Rows[0]["T$PROC"].ToString());
            obj022.pro1 = int.Parse(resultado.Rows[0]["T$PRO1"].ToString());
            obj022.pro2 = int.Parse(resultado.Rows[0]["T$PRO2"].ToString());
            obj022.log1 = resultado.Rows[0]["T$LOG1"].ToString();
            obj022.log2 = resultado.Rows[0]["T$LOG2"].ToString();
            obj022.logd = resultado.Rows[0]["T$LOGD"].ToString();
            obj022.dele = 4;
            obj022.qtd1 = int.Parse(resultado.Rows[0]["T$QTD1"].ToString());
            obj022.norp = int.Parse(resultado.Rows[0]["T$NORP"].ToString());
            obj022.loca = resultado.Rows[0]["T$LOCA"].ToString();
            obj022.qtd2 = int.Parse(resultado.Rows[0]["T$QTD2"].ToString());
            obj022.refcntd = 0;
            obj022.refcntu = 0;
            obj022.idrecord = resultado.Rows[0]["IDRECORD"].ToString();

            parameterCollection022.Add(obj022);

            int retorno = idal.actualizarRegistro_Param(ref parameterCollection022, ref strError);

            if (!string.IsNullOrEmpty(strError))
            {
                lblError.Text = mensajes("errorupdt");
                lblError.ForeColor = System.Drawing.Color.Red;

                return;
            }

            if (retorno > 0)
                lblError.Text = therecordhasbeenupdatedsuccessfully;// mensajes("msjudpt");
            lblError.ForeColor = System.Drawing.Color.Green;
            lblError.CssClass = "";
            //lblMessage

            txtSQNB.Text = string.Empty;


            //StringBuilder script = new StringBuilder();
            //script.Append("ventanaImp = window.open('../Labels/whInvLabel.aspx', ");
            //script.Append("'ventanaImp', 'menubar=0,resizable=0,width=580,height=450');");
            //script.Append("ventanaImp.moveTo(30, 0);");
            ////script.Append("setTimeout (ventanaImp.close(), 20000);");
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "printTag", script.ToString(), true);

        }

        #endregion

        #region Metodos

        protected void CargarIdioma()
        {
            therecordhasbeenupdatedsuccessfully = _textoLabels.readStatement(formName, _idioma, "therecordhasbeenupdatedsuccessfully");
            lblDescPalletId.Text = _textoLabels.readStatement(formName, _idioma, "lblDescPalletId");
            btnSend.Text = _textoLabels.readStatement(formName, _idioma, "btnSend");
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
//
//
