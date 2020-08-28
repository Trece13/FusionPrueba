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
    public partial class whInvConfirmedRegrind : System.Web.UI.Page
    {
        string strError = string.Empty;
        string strConteo = string.Empty;
        DataTable resultado = new DataTable();

        protected static InterfazDAL_tticol042 idal042 = new InterfazDAL_tticol042();
        Ent_tticol042 obj042 = new Ent_tticol042();
        protected static InterfazDAL_twhcol072 idal072 = new InterfazDAL_twhcol072();
        Ent_twhcol072 obj072 = new Ent_twhcol072();
        protected static InterfazDAL_ttccol301 idal301 = new InterfazDAL_ttccol301();
        Ent_ttccol301 obj301 = new Ent_ttccol301();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-CO");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-CO");
            base.InitializeCulture();

            if (!IsPostBack)
            {
                lblError.Visible = false;
                lblResult.Visible = false;
                printerDiv.Visible = false;
                if (Session["IsPreviousPage"] == null) { Session.Clear(); }

                string strTitulo = "Confirm Regrind Sequence";
                Label control = (Label)Page.Controls[0].FindControl("lblPageTitle");
                control.Text = strTitulo;
                Page.Form.DefaultButton = btnQuery.UniqueID;

                if (lblIngreso.Text == "")
                {
                    lblIngreso.Text = "1";
                }
            }
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            Clear();
            obj042 = new Ent_tticol042();

            strError = string.Empty;
            strConteo = string.Empty;
            obj042.sqnb = txtSQNB.Text.Trim().ToUpperInvariant();
            resultado = idal042.listaRegistroXSQNB_ConfirmedRegrind(ref obj042, ref strError);

            if (resultado.Rows.Count < 1)
            {
                lblError.Visible = true;
                lblError.Text = strError;
                return;
            }

            if (resultado.Rows[0]["T$DELE"].ToString() != "4")
            {
                lblError.Visible = true;
                lblError.Text = "Regrind has not been wrapped";
                return;
            }

            if (resultado.Rows[0]["T$PRO1"].ToString() == "1")
            {
                lblError.Visible = true;
                lblError.Text = "Regrind sequence already confirmed, process cannot continue";
                return;
            }

            lblWorkOrder.Text = resultado.Rows[0]["T$PDNO"].ToString();
            lblRegrindSequence.Text = resultado.Rows[0]["T$SQNB"].ToString();
            lblRegrindCode.Text = resultado.Rows[0]["T$MITM"].ToString();
            lblRegrindDesc.Text = resultado.Rows[0]["T$DSCA"].ToString();
            lblQuantity.Text = resultado.Rows[0]["T$QTDL"].ToString();
            lblUnit.Text = resultado.Rows[0]["T$CUNI"].ToString();

            Session["PONO"] = resultado.Rows[0]["T$PONO"].ToString();
            Session["CWAR"] = resultado.Rows[0]["T$CWAR"].ToString();

            printerDiv.Visible = true;
            btnSave.Visible = true;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            InsertarIngresoPagina();

            List<Ent_tticol042> parameterCollection042 = new List<Ent_tticol042>();
            List<Ent_twhcol072> parameterCollection072 = new List<Ent_twhcol072>();
            
            obj042.log1 = Session["user"].ToString();
            obj042.qtd1 = Convert.ToDecimal(lblQuantity.Text.Trim());
            obj042.pro1 = 1;
            obj042.sqnb = lblRegrindSequence.Text.Trim();

            parameterCollection042.Add(obj042);
            int retorno = idal042.actualizaRegistro_ConfirmedRegrind(ref parameterCollection042, ref strError);

            if (!string.IsNullOrEmpty(strError))
            {
                lblError.Visible = true;
                lblError.Text = strError;
                return;
            }

            obj072.sour = 4;
            obj072.orno = lblWorkOrder.Text;
            obj072.pono = Convert.ToInt16(Session["PONO"].ToString());
            obj072.dwms = lblWorkOrder.Text;
            obj072.pwms = Convert.ToInt16(Session["PONO"].ToString());
            obj072.cwar = Session["CWAR"].ToString();
            obj072.item = lblRegrindCode.Text;
            obj072.qana = Convert.ToDecimal(lblQuantity.Text);
            obj072.cuni = lblUnit.Text;
            obj072.clot = " ";
            obj072.proc = 2;
            obj072.rcno = " ";
            obj072.rwms = " ";
            obj072.logn = Session["user"].ToString(); ;
            obj072.erro = " ";
            obj072.seqn = Convert.ToInt32(lblRegrindSequence.Text.Substring(lblRegrindSequence.Text.Trim().Length - 2, 2));
            obj072.refcntd = 0;
            obj072.refcntu = 0;
            parameterCollection072.Add(obj072);
            
            int retornoRegrind = idal072.insertarRegistro(ref parameterCollection072, ref strError);

            if (!string.IsNullOrEmpty(strError))
            {
                lblError.Text = strError;
                lblError.Visible = true;
                return;
            }

            Clear();
            lblResult.Text = "Confirm regrind succesfully.";
            lblResult.Visible = true;
            txtSQNB.Text = string.Empty;
        }

        void Clear()
        {
            Session.Remove("PONO");
            Session.Remove("CWAR");

            lblError.Text = "";
            lblError.Visible = false;
            lblResult.Text = "";
            lblResult.Visible = false;

            lblWorkOrder.Text = string.Empty;
            lblRegrindSequence.Text = string.Empty;
            lblRegrindCode.Text = string.Empty;
            lblRegrindDesc.Text = string.Empty;
            lblQuantity.Text = string.Empty;
            lblUnit.Text = string.Empty;

            printerDiv.Visible = false;
            btnSave.Visible = false;
        }

        void InsertarIngresoPagina()
        {
            if (lblIngreso.Text == "1")
            {
                lblIngreso.Text = "0";

                List<Ent_ttccol301> parameterCollection301 = new List<Ent_ttccol301>();

                obj301.user = Session["user"].ToString();
                obj301.come = "Confirm Regrind Sequence";
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
    }
}
