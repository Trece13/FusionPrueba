

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

namespace whusap.WebPages.Balance
{
    public partial class whInvLabel : System.Web.UI.Page
    {
        #region Propiedades
        protected static InterfazDAL_tticol011 idal = new InterfazDAL_tticol011();
        protected static InterfazDAL_tticol022 idal022 = new InterfazDAL_tticol022();
        Ent_tticol020 obj020 = new Ent_tticol020();
        Ent_tticol022 obj022 = new Ent_tticol022();
        Ent_tticol011 obj = new Ent_tticol011();
        DataTable resultado = new DataTable();
        string strError = string.Empty;

        ////Manejo idioma
        public string Pleaseselectrollwinder = string.Empty;
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

                formName += "?Regrind";

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
                string[] opciones = new string [5];
                opciones[0] = (_idioma == "INGLES" ? "-- Select --" : "-- Seleccione --");
                opciones[1] = "1";
                opciones[2] = "2";
                opciones[3] = "3";
                opciones[4] = "4";

                ddRollWinder.DataSource = opciones;
                ddRollWinder.DataBind();
                lblError.Visible = false;
                if (ConfigurationManager.AppSettings.AllKeys.Contains("BalanceMachines"))
                {
                    string machines = ConfigurationManager.AppSettings["BalanceMachines"];

                    var machineArray = machines.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries).ToList<string>();
                    machineArray.Insert(0, _idioma == "INGLES" ? "-- Select --" : "-- Seleccione --");

                    listMachine.DataSource = machineArray;
                    listMachine.DataBind();
                }
                else { lblError.Visible = true; lblError.Text = mensajes("machinelist"); }

                if (Session["IsPreviousPage"] == null) { Session.Clear(); }

                string strTitulo = mensajes("encabezado");
                Label control = (Label)Page.Controls[0].FindControl("lblPageTitle");
                control.Text = strTitulo;
                Page.Form.DefaultButton = btnSend.UniqueID;

            }
        }

        protected void listMachine_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtQuantity.Enabled = true;
            btnSend.Enabled = true;

            lblError.Text = string.Empty; ;
            obj.mcno = listMachine.SelectedValue.Trim().ToUpperInvariant();
            resultado = idal.invLabel_listaRegistrosOrdenMaquina_Param(ref obj, ref strError);
            // para este caso strError muestra los mensajes de resutado de la consulta para cuando no hay filas 
            // o cuando existe mas de una fila
            if (!string.IsNullOrEmpty(strError))
            {
                txtQuantity.Enabled = false;
                btnSend.Enabled = false;
                lblError.Visible = true;
                lblError.Text = resultado.Rows.Count < 1 ? string.Concat(mensajes("ordernotinitiated"), obj.mcno.Trim()) : string.Concat(mensajes("moreorders"), obj.mcno.Trim());
                return;
            }
            ActiveMachine = obj.mcno.Trim().ToUpperInvariant(); // 
            ActiveOrderMachine = resultado.Rows[0]["ORDEN"].ToString();
            label35.Text = "Active Order : " + ActiveOrderMachine;
            lblError.Text = string.Empty;
            lblError.Visible = false;
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            lblError.Visible = true;
            decimal cantidad;
            string cantidads;

            cantidads = txtQuantity.Text.Contains(",") == true ? txtQuantity.Text.Replace(",", "."):txtQuantity.Text.Replace(".", ",");
            //bool convert = decimal.TryParse(txtQuantity.Text.Trim(), out cantidad);
            bool convert = decimal.TryParse(cantidads, out cantidad);
            cantidads = cantidad.ToString();
            var numberFormatInfo = new NumberFormatInfo { NumberDecimalSeparator = "," };
            var value = Decimal.Parse(cantidads);

            lblError.Text = string.Empty;

            //string RollWinder = txtRollWinder.Text.ToString().ToUpper();
            string RollWinder = ddRollWinder.Text;
            if (RollWinder.Trim() == "1" || RollWinder.Trim() == "2" || RollWinder.Trim() == "3" || RollWinder.Trim() == "4")
            {

            }
            else
            {
                lblError.Text = Pleaseselectrollwinder;
                lblError.ForeColor = System.Drawing.Color.Red;
                return;
            }
//            Session["rollwinder"] = txtRollWinder.Text.ToString();
            Session["rollwinder"] = RollWinder;

            if (!convert || cantidad < 1)
            {
                lblError.Visible = true;
                lblError.Text = mensajes("quantitymust");
                lblError.ForeColor = System.Drawing.Color.Red;
                return;
            }
            strError = string.Empty;
            obj.pdno = hOrdenMachine.Value;  //hidden.Value;
            var pdno = obj.pdno;
            resultado = idal.invLabel_listaRegistrosOrdenParam(ref obj, ref strError);

            if (resultado.Rows.Count < 1)
            {
                lblError.Visible = true;
                lblError.Text = String.Concat(mensajes("noitemsfound"), obj.pdno.Trim());
                lblError.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (Convert.ToDecimal(resultado.Rows[0]["maxr"].ToString()) < Convert.ToDecimal(cantidads))
            {
                lblError.Visible = true;
                lblError.Text = "Roll weight cannot be higher than "+Convert.ToDecimal(resultado.Rows[0]["maxr"].ToString())+" Kg";
                lblError.ForeColor = System.Drawing.Color.Red;
                return;
            }

            var mbrl = idal022.consultambrl(ref strError).Rows[0]["MBRL"];//ConfigurationManager.AppSettings["MBRL"].ToString();

            var tiempo = Convert.ToInt32(mbrl);

            var validaTiempoGuardado = idal022.validateTimeSaveRecord(ref pdno, ref tiempo,  ref strError);

            if (validaTiempoGuardado.Rows.Count > 0)
            {
                lblError.Visible = true;
                lblError.Text = String.Format(mensajes("rollannounced"), tiempo);
                lblError.ForeColor = System.Drawing.Color.Red;
                return;
            }

            List<Ent_tticol020> parameterCollection020 = new List<Ent_tticol020>();
            List<Ent_tticol022> parameterCollection022 = new List<Ent_tticol022>();

            obj020.pdno = resultado.Rows[0]["ORDEN"].ToString();
            obj020.mitm = resultado.Rows[0]["ITEM"].ToString();
            obj020.dsca = resultado.Rows[0]["DESCI"].ToString();
            obj020.cuni = resultado.Rows[0]["UNIDAD"].ToString();
            //obj020.qtdl = Decimal.Parse(txtQuantity.Text, System.Globalization.CultureInfo.InvariantCulture);  //Convert.ToDecimal(txtQuantity.Text);
            obj020.qtdl = value;
            obj020.mess = " ";
            obj020.refcntd = 0;
            obj020.refcntu = 0;
            obj020.user = Session["user"].ToString();
            parameterCollection020.Add(obj020);


            obj022.cuni = resultado.Rows[0]["UNIDAD"].ToString();
            obj022.pdno = resultado.Rows[0]["ORDEN"].ToString();
            obj022.sqnb = idal022.invLabel_generaSecuenciaOrden(ref obj022, ref strError);
            obj022.mitm = resultado.Rows[0]["ITEM"].ToString();
            //obj022.qtdl = Decimal.Parse(txtQuantity.Text, System.Globalization.CultureInfo.InvariantCulture);  //Convert.ToDecimal(txtQuantity.Text);
            obj022.qtdl = cantidad;
            obj022.logn = Session["user"].ToString();
            obj022.proc = 2;
            obj022.pro1 = 2;
            obj022.pro2 = 2;
            obj022.log1 = "NONE";
            obj022.log2 = "NONE";
            obj022.logd = "NONE";
            obj022.dele = 2;
            obj022.qtd1 = 0;
            obj022.norp = 1;
            obj022.loca = " ";
            obj022.qtd2 = 0;
            obj022.refcntd = 0;
            obj022.refcntu = 0;
            obj022.drpt = DateTime.Now;
            obj022.urpt = " ";
            //obj022.acqt = Convert.ToDouble(obj022.qtdl);
            obj022.acqt = value;
            obj022.cwaf = idal022.WharehouseTisfc001(resultado.Rows[0]["ORDEN"].ToString(), ref strError);
            obj022.cwat = " ";
            obj022.aclo = " ";
            parameterCollection022.Add(obj022);

            //ActiveOrderMachine = obj022.sqnb;


            int retorno = idal022.insertarRegistro(ref parameterCollection022, ref parameterCollection020, ref strError);

            if (!string.IsNullOrEmpty(strError))
            {
                lblError.Text = strError;
                return;
            }

            if (retorno > 0)
                lblError.Visible = true;
                lblError.Text = mensajes("rollsaved");


            txtQuantity.Text = string.Empty;

            resultado = idal022.invLabel_registroImprimir_Param(ref obj022, ref strError);

            DataRow reg = resultado.Rows[0];
            Session["FilaImprimir"] = reg;
            Session["descItem"] = obj020.dsca;
            Session["unidad"] = hidden.Value;

            StringBuilder script = new StringBuilder();
            script.Append("ventanaImp = window.open('../Labels/whInvLabel.aspx', ");
            script.Append("'ventanaImp', 'menubar=0,resizable=0,width=580,height=450');");
            script.Append("ventanaImp.moveTo(30, 0);");
            //script.Append("setTimeout (ventanaImp.close(), 20000);");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "printTag", script.ToString(), true);

        }

        #endregion

        #region Metodos

        protected String ActiveMachine
        {
            get { return hidden.Value; }
            set { hidden.Value = value; }
        }

        protected String ActiveOrderMachine
        {
            get { return hOrdenMachine.Value; }
            set { hOrdenMachine.Value = value; }
        }

        protected void CargarIdioma()
        {

            Pleaseselectrollwinder = _textoLabels.readStatement("whInvLabel.aspx", _idioma, "Pleaseselectrollwinder");
            lblMachine.Text = _textoLabels.readStatement("whInvLabel.aspx", _idioma, "lblMachine");
            lblWeight.Text = _textoLabels.readStatement("whInvLabel.aspx", _idioma, "lblWeight");
            lblRollWinder.Text = _textoLabels.readStatement("whInvLabel.aspx", _idioma, "lblRollWinder");
            btnSend.Text = _textoLabels.readStatement("whInvLabel.aspx", _idioma, "btnSend");
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