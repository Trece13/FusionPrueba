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
    public partial class whInvLocatedRegrind : System.Web.UI.Page
    {
        string strError = string.Empty;
        string strConteo = string.Empty;
        DataTable resultado = new DataTable();
        DataTable resultadoLocated = new DataTable();

        protected static InterfazDAL_tticol042 idal042 = new InterfazDAL_tticol042();
        Ent_tticol042 obj042 = new Ent_tticol042();
        protected static InterfazDAL_tticol030 idal030 = new InterfazDAL_tticol030();
        Ent_tticol030 obj030 = new Ent_tticol030();
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

                string strTitulo = "Location Regrind Sequence";
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
            resultado = idal042.listaRegistroXSQNB_LocatedRegrind(ref obj042, ref strError);

            if (resultado.Rows.Count < 1)
            {
                lblError.Visible = true;
                lblError.Text = strError;
                return;
            }

            if (resultado.Rows[0]["T$PRO2"].ToString() == "1")
            {
                lblError.Visible = true;
                lblError.Text = "Regrind sequence already located, process cannot continue";
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

            List<Ent_tticol030> parameterCollection030 = new List<Ent_tticol030>();
            List<Ent_tticol042> parameterCollection042 = new List<Ent_tticol042>();

            obj042.sqnb = txtSQNB.Text.Trim().ToUpperInvariant();
            obj042.loca = txtLocated.Text.Trim().ToUpperInvariant();
            resultadoLocated = idal042.listaRegistroXSQNB_FindLocation(ref obj042, ref strError);

            if (resultadoLocated.Rows.Count < 1)
            {
                lblError.Visible = true;
                lblError.Text = strError;
                return;
            }

            obj042.log2 = Session["user"].ToString();
            obj042.qtd2 = Convert.ToDecimal(lblQuantity.Text.Trim());
            obj042.pro2 = 1;


            parameterCollection042.Add(obj042);
            int retorno = idal042.actualizaRegistro_LocationRegrind(ref parameterCollection042, ref strError);

            if (!string.IsNullOrEmpty(strError))
            {
                lblError.Visible = true;
                lblError.Text = strError;
                return;
            }

            obj030.pdno = resultadoLocated.Rows[0]["T$PDNO"].ToString();
            obj030.sqnb = Convert.ToInt16(resultadoLocated.Rows[0]["T$SQNB"].ToString());
            obj030.mitm = resultadoLocated.Rows[0]["T$MITM"].ToString();
            obj030.dsca = resultadoLocated.Rows[0]["T$DSCA"].ToString();
            obj030.cwar = resultadoLocated.Rows[0]["T$CWAR"].ToString();
            obj030.loca = resultadoLocated.Rows[0]["T$LOCA"].ToString();
            obj030.qtdl = Convert.ToDecimal(resultadoLocated.Rows[0]["T$QTDL"].ToString());
            obj030.cuni = resultadoLocated.Rows[0]["T$CUNI"].ToString();
            obj030.mess = " ";
            obj030.user = Session["user"].ToString();
            parameterCollection030.Add(obj030);

            int retornoRegrind = idal030.insertarRegistro(ref parameterCollection030, ref strError);

            if (!string.IsNullOrEmpty(strError))
            {
                lblError.Text = strError;
                lblError.Visible = true;
                return;

            }

            Clear();
            lblResult.Text = "Location regrind succesfully.";
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
            txtLocated.Text = string.Empty;

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
                obj301.come = "Location Regrind Sequence";
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
