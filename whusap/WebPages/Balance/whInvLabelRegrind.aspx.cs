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
    public partial class whInvLabelRegrind : System.Web.UI.Page
    {
        #region Propiedades
        protected static InterfazDAL_tticol011 idal = new InterfazDAL_tticol011();
        protected static InterfazDAL_tticol022 idal022 = new InterfazDAL_tticol022();
        protected static InterfazDAL_tticol080 idal080 = new InterfazDAL_tticol080();
        protected static InterfazDAL_tticol042 idal042 = new InterfazDAL_tticol042();
        protected static InterfazDAL_ttccol301 idal301 = new InterfazDAL_ttccol301();
        Ent_tticol020 obj020 = new Ent_tticol020();
        Ent_tticol080 obj080 = new Ent_tticol080();
        Ent_tticol042 obj042 = new Ent_tticol042();
        Ent_ttccol301 obj301 = new Ent_ttccol301();
        Ent_tticol011 obj = new Ent_tticol011();
        DataTable resultado = new DataTable();
        DataTable cantidadRegrind = new DataTable();
        string strError = string.Empty;

        //Manejo idioma
        public static decimal MAXR = 0;
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
                printerDiv.Visible = false;
                if (Session["IsPreviousPage"] == null) { Session.Clear(); }

                string strTitulo = mensajes("encabezado");
                Label control = (Label)Page.Controls[0].FindControl("lblPageTitle");
                control.Text = strTitulo;
                Page.Form.DefaultButton = btnSend.UniqueID;

                if (lblIngreso.Text == "")
                {
                    lblIngreso.Text = "1";
                }

            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            btnSave.Visible = false;
            lblError.Visible = true;
            lblError.Text = string.Empty;
            printerDiv.Visible = false;
            obj.pdno = this.txtWorkOrder.Text.Trim().ToUpperInvariant();
            txtWorkOrder.Text = obj.pdno;
            strError = string.Empty;

            resultado = idal.invLabelRegrind_listaRegistrosOrdenMaquina_Param(ref obj, ref strError);
            if (!String.IsNullOrEmpty(strError)) { lblError.Text = resultado.Rows.Count <= 0 ? mensajes("ordermachine") : mensajes("moreoneorder"); return; }
            else
            {
                if (resultado.Rows.Count > 0)
                {
                    MAXR = Convert.ToDecimal(resultado.Rows[0]["MAXG"].ToString());
                }
            }

            resultado = idal.invLabelRegrind_listaRegistrosOrdenParam(ref obj, ref strError);
            if (!String.IsNullOrEmpty(strError)) { lblError.Text = String.Format(mensajes("ordernotexists"), obj.pdno); return; }
            
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
            btnSave.Visible = true;
        }

        protected void listRegrind_SelectedIndexChanged(object sender, EventArgs e)
        {
            resultado = ViewState["resultado"] as DataTable;
            //DataRow fila = resultado.Rows[listRegrind.SelectedIndex];
            DataRow fila = resultado.Select("ITEMR = '" + listRegrind.SelectedItem.Value + "'").First();

            ConsecPos = fila["POSICION"].ToString();
            unityItem = fila["UNIDAD"].ToString().Trim();
            BodegaItem = fila["ALMACEN"].ToString();
            LoteItem = fila["LOTE"].ToString();
            descItem = fila["DESCAR"].ToString();
            machineItem = fila["MAQUINA"].ToString();
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


            decimal cantidad;
            string cantidads;
            cantidads = txtQuantity.Text.Replace(".", ",");
            //bool convert = decimal.TryParse(txtQuantity.Text.Trim(), out cantidad);
            bool convert = decimal.TryParse(cantidads, out cantidad);
            var numberFormatInfo = new NumberFormatInfo { NumberDecimalSeparator = "," };
            var value = Decimal.Parse(cantidads, numberFormatInfo);
            decimal valueP = Convert.ToDecimal(value.ToString().Replace(",", "."));

            if (MAXR < cantidad)
            {
                lblError.Text = "Regrind announced weight cannot be higher than " + MAXR + "Kg"; return; 
            }

            if (String.IsNullOrEmpty(listRegrind.SelectedValue.Trim())) { lblError.Text = mensajes("fillform"); return; }
            if (cantidad <= 1) { lblError.Text = mensajes("quantityzero"); return; }

            strError = string.Empty;
            //obj.pdno = hidden.Value;
            cantidad = cantidad * (-1);
            obj080.pono = Convert.ToInt32(ConsecPos);
            obj080.orno = txtWorkOrder.Text;
            obj080.item = "         " + listRegrind.SelectedValue;
            obj080.qune = cantidad;
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
            obj042.pdno = txtWorkOrder.Text;
            cantidadRegrind = idal042.listaCantidadRegrind(ref obj042, ref strError);
            strTagId = txtWorkOrder.Text + "-R" + cantidadRegrind.Rows[0]["CANT"].ToString();
            //GRRC Fin

            int retorno = idal080.actualizarRegistro_Param(ref parameterCollection, ref strError, ref strTagId);
            if (retorno < 1 && string.IsNullOrEmpty(strError))
                retorno = idal080.insertarRegistro(ref parameterCollection, ref strError, ref strTagId);

            if (!string.IsNullOrEmpty(strError)) { lblError.Text = mensajes("errorsave"); return; }


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
            lblWorkOrder.Text = _textoLabels.readStatement(formName, _idioma, "lblWorkOrder");
            btnSend.Text = _textoLabels.readStatement(formName, _idioma, "btnSend");
            lblRegrind.Text = _textoLabels.readStatement(formName, _idioma, "lblRegrind");
            lblToIssue.Text = _textoLabels.readStatement(formName, _idioma, "lblToIssue");
            lblMaxDigits.Text = _textoLabels.readStatement(formName, _idioma, "lblMaxDigits");
            btnSave.Text = _textoLabels.readStatement(formName, _idioma, "btnSave");
            minlenght.ErrorMessage = _textoLabels.readStatement(formName, _idioma, "regularWorkOrder");
            RequiredField.ErrorMessage = _textoLabels.readStatement(formName, _idioma, "requiredWorkOrder");
            OrderError.ErrorMessage = _textoLabels.readStatement(formName, _idioma, "customWorkOrder");
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
}