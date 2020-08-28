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
    public partial class whInvLabelRegrindm : System.Web.UI.Page
    {
        #region Propiedades
        protected static InterfazDAL_tticol022 idal022 = new InterfazDAL_tticol022();
        protected static InterfazDAL_ttccol301 idal301 = new InterfazDAL_ttccol301();
        protected static InterfazDAL_tticol011 idal011 = new InterfazDAL_tticol011();
        protected static InterfazDAL_tticol080 idal080 = new InterfazDAL_tticol080();
        protected static InterfazDAL_tticol042 idal042 = new InterfazDAL_tticol042();
        Ent_tticol042 obj042 = new Ent_tticol042();
        Ent_tticol080 obj080 = new Ent_tticol080();
        Ent_tticol011 obj011 = new Ent_tticol011();
        Ent_ttccol301 obj301 = new Ent_ttccol301();
        Ent_tticol022 obj022 = new Ent_tticol022();
        DataTable resultado = new DataTable();
        DataTable cantidadRegrind = new DataTable();
        string strError = string.Empty;

        //Manejo idioma
        private static Mensajes _mensajesForm = new Mensajes();
        private static LabelsText _textoLabels = new LabelsText();
        private static string formName;
        private static string globalMessages = "GlobalMessages";
        public static string _idioma;
        public static decimal MAXG = 0;
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
                //if (ConfigurationManager.AppSettings.AllKeys.Contains("BalanceMachinesRetail"))
                //{
                //    string machines = ConfigurationManager.AppSettings["BalanceMachinesRetail"];

                //    var machineArray = machines.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries).ToList<string>();
                //    machineArray.Insert(0, "-- Select --");

                //    listMachine.DataSource = machineArray;
                //    listMachine.DataBind();
                //}
                //else { lblError.Visible = true; lblError.Text = "Machine List not found"; }

                if (Session["IsPreviousPage"] == null) { Session.Clear(); }

                string strTitulo = mensajes("encabezado");
                Label control = (Label)Page.Controls[0].FindControl("lblPageTitle");
                control.Text = strTitulo;
                txtMachine.TextChanged += new EventHandler(txtMachine_listMachine);
                Page.Form.DefaultButton = btnChangeMac.UniqueID;

                if (lblIngreso.Text == "")
                {
                    lblIngreso.Text = "1";
                }
            }

        }

        protected void txtMachine_listMachine(object sender, EventArgs e)
        {

            btnSave.Visible = false;
            lblError.Visible = true;
            lblError.Text = string.Empty;
            //printerDiv.Visible = false;

            listRegrind.DataSource = "";
            listRegrind.DataBind();

            txtQuantity.Enabled = true;
            btnChangeMac.Enabled = true;
            txtMachine.Enabled = false;
            txtMachine.Text = txtMachine.Text.ToUpperInvariant();
            lblError.Text = string.Empty; ;
            obj011.mcno = ((TextBox)sender).Text.Trim().ToUpperInvariant(); ;  //listMachine.SelectedValue.Trim().ToUpperInvariant();

            resultado = idal011.invLabel_listaRegistrosOrdenMaquina_Param(ref obj011, ref strError);

            // Para este caso strError muestra los mensajes de resutado de la consulta para cuando no hay filas 
            // o cuando existe mas de una fila

            if (!string.IsNullOrEmpty(strError))
            {
                txtQuantity.Enabled = false;
                btnChangeMac.Enabled = false;
                txtMachine.Enabled = true;
                lblError.Text = resultado.Rows.Count <= 0 ? mensajes("ordermachine") : mensajes("moreoneorder");

                return;
            }

            obj011.pdno = resultado.Rows[0]["ORDEN"].ToString().Trim().ToUpperInvariant();
            MAXG = Convert.ToDecimal(resultado.Rows[0]["MAXG"].ToString().Trim());
            resultado = idal011.invLabelRegrind_listaRegistrosOrdenMaquina_Param(ref obj011, ref strError);
            if (!String.IsNullOrEmpty(strError)) { lblError.Text = resultado.Rows.Count <= 0 ? mensajes("ordermachine") : mensajes("moreoneorder"); return; }

            ActiveMachine = obj011.mcno.Trim().ToUpperInvariant(); // 
            ActiveOrderMachine = resultado.Rows[0]["ORDEN"].ToString();

            resultado = idal011.invLabelRegrind_listaRegistrosOrdenParam(ref obj011, ref strError);
            if (!String.IsNullOrEmpty(strError)) { lblError.Text = mensajes("ordernotexists"); return; }

            printerDiv.Visible = true;
            DataRow fila = resultado.NewRow();

            fila[0] = " ";
            fila[7] = _idioma == "INGLES" ? "-- Select the Regrind Item --" : "-- Seleccione el articulo molido --";
            resultado.Rows.InsertAt(fila, 0);

            resultado.DefaultView.Sort = "itemr ASC";
            ViewState["resultado"] = resultado;

            listRegrind.DataSource = ViewState["resultado"] as DataTable;
            listRegrind.DataTextField = "COMB";
            listRegrind.DataValueField = "ITEMR";
            listRegrind.DataBind();

            listRegrind.SelectedIndex = 0;
            listRegrind.Focus();

            lblError.Text = string.Empty;
            lblError.Visible = false;
            btnSave.Visible = true;
        }

        protected void listRegrind_SelectedIndexChanged(object sender, EventArgs e)
        {
            resultado = ViewState["resultado"] as DataTable;
            DataRow[] cons = resultado.Select("ITEMR = '" + listRegrind.SelectedValue.Trim() + "'");
            //cons[0];
            DataRow fila = cons[0]; // resultado.Rows[listRegrind.SelectedIndex];
            ConsecPos = fila["POSICION"].ToString();
            unityItem = fila["UNIDAD"].ToString().Trim();
            BodegaItem = fila["ALMACEN"].ToString();
            LoteItem = fila["LOTE"].ToString();
            descItem = listRegrind.SelectedItem.Text; // fila["DESCAR"].ToString();
            machineItem = fila["MAQUINA"].ToString();
        }

        protected void btnChangeMac_Click(object sender, EventArgs e)
        {
            this.txtMachine.Enabled = true;
            this.txtMachine.Text = string.Empty;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            InsertarIngresoPagina();

            string strTagId = string.Empty;
            List<Ent_tticol080> parameterCollection = new List<Ent_tticol080>();
            List<Ent_tticol042> parameterCollectionRegrind = new List<Ent_tticol042>();
            obj080 = new Ent_tticol080();
            obj042 = new Ent_tticol042();

            lblError.Visible = true;
            lblError.Text = string.Empty;
            NumberStyles style = NumberStyles.AllowDecimalPoint;

            //decimal cantidad decimal.TryParse(txtQuantity.Text.Trim().Replace(".", ","), style, System.Globalization.CultureInfo.InvariantCulture, out cantidad);
            decimal cantidad;
            string cantidads;
            cantidads = txtQuantity.Text.Replace(".", ",");
            //bool convert = decimal.TryParse(txtQuantity.Text.Trim(), out cantidad);
            bool convert = decimal.TryParse(cantidads, out cantidad);
            var numberFormatInfo = new NumberFormatInfo { NumberDecimalSeparator = "," };
            var value = Decimal.Parse(cantidads, numberFormatInfo);
            decimal valueP = Convert .ToDecimal(value.ToString().Replace(",", "."));

            if (String.IsNullOrEmpty(listRegrind.SelectedValue.Trim())) { lblError.Text = mensajes("fillform"); return; }
            if (cantidad <= 0) { lblError.Text = mensajes("quantityzero"); return; }
            if (cantidad > MAXG) { lblError.Text = "Regrind announced weight cannot be higher than " + MAXG + "Kg"; return; }
            
            strError = string.Empty;
            //obj.pdno = hidden.Value;
            cantidad = cantidad * (-1);
            obj080.pono = Convert.ToInt32(ConsecPos);
            obj080.orno = ActiveOrderMachine;
            obj080.item = listRegrind.SelectedValue;
            obj080.qune = value * -1;
            obj080.cwar = BodegaItem;
            obj080.logn = Session["user"].ToString();
            obj080.clot = LoteItem;
            obj080.proc = 2;
            obj080.refcntd = 0;
            obj080.refcntu = 0;
            obj080.idrecord = unityItem;
            obj080.oorg = "4";
            obj080.pick = 0;
            parameterCollection.Add(obj080);

            //GRRC Inicio
            obj042.pdno = ActiveOrderMachine;
            cantidadRegrind = idal042.listaCantidadRegrind(ref obj042, ref strError);
            strTagId = ActiveOrderMachine + "-R" + cantidadRegrind.Rows[0]["CANT"].ToString();
            // GRRC Fin

            int retorno = idal080.actualizarRegistro_Param(ref parameterCollection, ref strError, ref strTagId);
            if (retorno < 1 && string.IsNullOrEmpty(strError))
            {
                retorno = idal080.insertarRegistro_regrindM(ref parameterCollection, ref strError, ref strTagId);

            }
            if (!string.IsNullOrEmpty(strError))
                lblError.Text = mensajes("errorsave");


            //GRRC Inicio
            strError = string.Empty;
            obj042.sqnb = strTagId;
            obj042.proc = 1;
            obj042.logn = Session["user"].ToString();
            obj042.mitm = "         " + listRegrind.SelectedValue;
            obj042.pono = Convert.ToInt32(ConsecPos);
            obj042.qtdl = value;
            obj042.cuni = unityItem;
            obj042.log1 = " ";
            obj042.qtd1 = 0;
            obj042.pro1 = 2;
            obj042.log2 = " ";
            obj042.qtd2 = 0;
            obj042.pro2 = 2;
            obj042.loca = " ";
            obj042.norp = 0;
            obj042.dele = 2;
            obj042.logd = " ";
            obj042.refcntd = 0;
            obj042.refcntu = 0;
            obj042.drpt = DateTime.Now;
            obj042.urpt = " ";
            obj042.acqt = Convert.ToDouble(value);
            obj042.cwaf = BodegaItem;
            obj042.cwat = " ";
            obj042.aclo = " ";
            obj042.allo = 0;

            parameterCollectionRegrind.Add(obj042);

            int retornoRegrind = idal042.insertarRegistro(ref parameterCollectionRegrind, ref strError);
            bool retornoRegrindTticon242 = idal042.insertarRegistroTticon242(ref parameterCollectionRegrind, ref strError);

            if (!string.IsNullOrEmpty(strError)) { lblError.Text = mensajes("errorsave"); return; }
            //GRRC Fin


            txtQuantity.Text = string.Empty;
            listRegrind.SelectedIndex = 0;

            DataRow filaImprimir = ((DataTable)idal080.listaRegistroImprimir_Param(ref obj080, ref strError)).Rows[0];
            filaImprimir["T$QUNE"] = Math.Abs(cantidad).ToString();

            Session["FilaImprimir"] = filaImprimir;
            Session["descItem"] = descItem;
            Session["unidad"] = unityItem;
            Session["machineItem"] = machineItem;
            Session["strTagid"] = strTagId;

            StringBuilder script = new StringBuilder();
            script.Append("ventanaImp = window.open('../Labels/whInvLabelRegrind.aspx', ");
            script.Append("'ventanaImp', 'menubar=0,resizable=0,width=580,height=450');");
            script.Append("ventanaImp.moveTo(30, 0);");
            //script.Append("setTimeout (ventanaImp.close(), 20000);");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "printTag", script.ToString(), true);

            listRegrind.Focus();
        }

        #endregion

        #region Metodos

        void InsertarIngresoPagina()
        {
            if (lblIngreso.Text == "1")
            {
                lblIngreso.Text = "0";
                List<Ent_ttccol301> parameterCollection301 = new List<Ent_ttccol301>();

                obj301.user = Session["user"].ToString();
                obj301.come = mensajes("encabezado");
                obj301.refcntd = 0;
                obj301.refcntu = 0;

                parameterCollection301.Add(obj301);
                int retorno = idal301.insertarRegistro(ref parameterCollection301, ref strError);

                if (!string.IsNullOrEmpty(strError))
                {
                    lblError.Visible = true;
                    lblError.Text = strError;
                    return;
                }
            }

        }

        protected String ActiveMachine
        {
            get { return HiddenField1.Value; }
            set { HiddenField1.Value = value; }
        }

        protected String ActiveOrderMachine
        {
            get { return hOrdenMachine.Value; }
            set { hOrdenMachine.Value = value; }
        }

        protected String ConsecPos
        {
            get { return hidden.Value; }
            set { hidden.Value = value; }
        }

        protected String BodegaItem
        {
            get { return hi_indBodega.Value; }
            set { hi_indBodega.Value = value; }
        }

        protected String LoteItem
        {
            get { return hi_indLote.Value; }
            set { hi_indLote.Value = value; }
        }

        protected String descItem
        {
            get { return hi_descItem.Value; }
            set { hi_descItem.Value = value; }
        }

        protected String unityItem
        {
            get { return hi_unityItem.Value; }
            set { hi_unityItem.Value = value; }
        }

        protected String machineItem
        {
            get { return hi_machineItem.Value; }
            set { hi_machineItem.Value = value; }
        }

        protected void CargarIdioma()
        {
            lblMachine.Text = _textoLabels.readStatement(formName, _idioma, "lblMachine");
            lblRegrind.Text = _textoLabels.readStatement(formName, _idioma, "lblRegrind");
            lblToIssue.Text = _textoLabels.readStatement(formName, _idioma, "lblToIssue");
            lblMaxDigits.Text = _textoLabels.readStatement(formName, _idioma, "lblMaxDigits");
            btnChangeMac.Text = _textoLabels.readStatement(formName, _idioma, "btnChangeMac");
            btnSave.Text = _textoLabels.readStatement(formName, _idioma, "btnSave");
            //validateReturn.ErrorMessage = _textoLabels.readStatement(formName, _idioma, "regularQuantity");
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

        //protected void listMachine_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (txtMachine.Text == string.Empty)
        //        return;

        //    lblError.Visible = true;
        //    lblError.Text = string.Empty;
        //    printerDiv.Visible = false;

        //    txtQuantity.Enabled = true;
        //    btnChangeMac.Enabled = true;
        //    txtMachine.Enabled = false;
        //    lblError.Text = string.Empty; ;
        //    obj011.mcno = txtMachine.Text.Trim().ToUpperInvariant();

        //    resultado = idal011.invLabel_listaRegistrosOrdenMaquina_Param(ref obj011, ref strError);

        //    // Para este caso strError muestra los mensajes de resutado de la consulta para cuando no hay filas 
        //    // o cuando existe mas de una fila

        //    if (!string.IsNullOrEmpty(strError))
        //    {
        //        txtQuantity.Enabled = false;
        //        btnChangeMac.Enabled = false;
        //        txtMachine.Enabled = true;
        //        lblError.Text = strError;
        //        return;
        //    }

        //    obj011.pdno = resultado.Rows[0]["ORDEN"].ToString().Trim().ToUpperInvariant();
        //    resultado = idal011.invLabelRegrind_listaRegistrosOrdenMaquina_Param(ref obj011, ref strError);
        //    if (!String.IsNullOrEmpty(strError)) { lblError.Text = strError; return; }

        //    ActiveMachine = obj011.mcno.Trim().ToUpperInvariant(); // 
        //    ActiveOrderMachine = resultado.Rows[0]["ORDEN"].ToString();

        //    resultado = idal011.invLabelRegrind_listaRegistrosOrdenParam(ref obj011, ref strError);
        //    if (!String.IsNullOrEmpty(strError)) { lblError.Text = strError; return; }

        //    printerDiv.Visible = true;
        //    DataRow fila = resultado.NewRow();

        //    fila[0] = " ";
        //    fila[1] = "-- Select the Regrind Item --";
        //    resultado.Rows.InsertAt(fila, 0);

        //    resultado.DefaultView.Sort = "itemr ASC";
        //    ViewState["resultado"] = resultado;

        //    listRegrind.DataSource = ViewState["resultado"] as DataTable;
        //    listRegrind.DataTextField = "DESCAR";
        //    listRegrind.DataValueField = "ITEMR";
        //    listRegrind.DataBind();

        //    listRegrind.SelectedIndex = 0;
        //    listRegrind.Focus();

        //    lblError.Text = string.Empty;
        //    lblError.Visible = false;
        //}
    }
}