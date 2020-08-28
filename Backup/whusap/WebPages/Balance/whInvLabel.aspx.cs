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

namespace whusap.WebPages.Balance
{
    public partial class whInvLabel : System.Web.UI.Page
    {
        string strError = string.Empty;
        // string Aplicacion = "WEBAPP";
        DataTable resultado = new DataTable();

        protected static InterfazDAL_tticol011 idal = new InterfazDAL_tticol011();
        protected static InterfazDAL_tticol022 idal022 = new InterfazDAL_tticol022();

        Ent_tticol020 obj020 = new Ent_tticol020();
        Ent_tticol022 obj022 = new Ent_tticol022();
        Ent_tticol011 obj = new Ent_tticol011();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Cambiar cultura para manejo de separador decimal
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-CO");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-CO");
            base.InitializeCulture(); 
            
            if (!IsPostBack)
            {
                // Determinar existencia de las maquinas en web.config

                lblError.Visible = false;
                if (ConfigurationManager.AppSettings.AllKeys.Contains("BalanceMachines"))
                {
                    string machines = ConfigurationManager.AppSettings["BalanceMachines"];

                    var machineArray = machines.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries).ToList<string>();
                    machineArray.Insert(0, "-- Select --");
                                                        
                    listMachine.DataSource = machineArray;
                    listMachine.DataBind();
                }
                else { lblError.Visible = true;  lblError.Text = "Machine List not found"; }

                if (Session["IsPreviousPage"] == null) { Session.Clear(); }

                string strTitulo = "Print Tags";
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
                lblError.Text = strError; 
                return; 
            }
            ActiveMachine = obj.mcno.Trim().ToUpperInvariant(); // 
            ActiveOrderMachine =  resultado.Rows[0]["ORDEN"].ToString();
            lblError.Text = string.Empty;
            lblError.Visible = false;
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            lblError.Visible = true;
            decimal cantidad;
            bool convert = decimal.TryParse(txtQuantity.Text.Trim(), out cantidad);
            lblError.Text = string.Empty;

            if (!convert || cantidad < 1)
            {
                lblError.Text = "Quantity must be greater than zero";
                lblError.CssClass = "errorMsg";
                return;
            }
            strError = string.Empty;
            obj.pdno = hOrdenMachine.Value;  //hidden.Value;
            resultado = idal.invLabel_listaRegistrosOrdenParam(ref obj, ref strError);

            if (resultado.Rows.Count < 1)
            {
                lblError.Text = "Not item's found for order " + obj.pdno.Trim();
                lblError.CssClass = "errorMsg";
                return;            
            }
            List<Ent_tticol020> parameterCollection020 = new List<Ent_tticol020>();
            List<Ent_tticol022> parameterCollection022 = new List<Ent_tticol022>();

            obj020.pdno = resultado.Rows[0]["ORDEN"].ToString();
            obj020.mitm = resultado.Rows[0]["ITEM"].ToString();
            obj020.dsca = resultado.Rows[0]["DESCI"].ToString();
            obj020.cuni = resultado.Rows[0]["UNIDAD"].ToString();
            obj020.qtdl = Decimal.Parse(txtQuantity.Text, System.Globalization.CultureInfo.InvariantCulture);  //Convert.ToDecimal(txtQuantity.Text);
            obj020.mess = " ";
            obj020.refcntd = 0;
            obj020.refcntu = 0;
            obj020.user = Session["user"].ToString();
            parameterCollection020.Add(obj020);


            obj022.cuni = resultado.Rows[0]["UNIDAD"].ToString();
            obj022.pdno = resultado.Rows[0]["ORDEN"].ToString();
            obj022.sqnb = idal022.invLabel_generaSecuenciaOrden(ref obj022, ref strError);
            obj022.mitm = resultado.Rows[0]["ITEM"].ToString();
            obj022.qtdl = Decimal.Parse(txtQuantity.Text, System.Globalization.CultureInfo.InvariantCulture);  //Convert.ToDecimal(txtQuantity.Text);
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
            parameterCollection022.Add(obj022);

            //ActiveOrderMachine = obj022.sqnb;


            int retorno = idal022.insertarRegistro(ref parameterCollection022, ref parameterCollection020, ref strError);

            if (!string.IsNullOrEmpty(strError))
            {
                lblError.Text = strError;
                return;
            }

            if (retorno > 0)
                lblError.Text = "Roll was saved succesfully.";

                
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

    }
}