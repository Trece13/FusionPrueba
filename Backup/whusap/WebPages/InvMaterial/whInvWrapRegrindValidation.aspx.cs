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

namespace whusap.WebPages.InvMaterial
{
    public partial class whInvWrapRegrindValidation : System.Web.UI.Page
    {
        string strError = string.Empty;
        DataTable resultado = new DataTable();

        protected static InterfazDAL_tticol042 idal042 = new InterfazDAL_tticol042();
        Ent_tticol042 obj042 = new Ent_tticol042();

        protected static InterfazDAL_ttccol301 idal301 = new InterfazDAL_ttccol301();
        Ent_ttccol301 obj301 = new Ent_ttccol301();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Cambiar cultura para manejo de separador decimal
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-CO");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-CO");
            base.InitializeCulture();

            if (!IsPostBack)
            {
                lblError.Visible = false;
                if (Session["IsPreviousPage"] == null) { Session.Clear(); }

                string strTitulo = "Wrap Regrind Validation";
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
            InsertarIngresoPagina();
                        
            lblError.Visible = false;
            lblError.Text = string.Empty;
            lblResult.Visible = false;
            lblResult.Text = string.Empty;

            obj042 = new Ent_tticol042();

            strError = string.Empty;
            obj042.sqnb = txtSQNB.Text.Trim().ToUpperInvariant();
            resultado = idal042.listaRegistroXSQNB(ref obj042, ref strError);

            if (resultado.Rows.Count < 1)
            {
                lblError.Visible = true;
                lblError.Text = strError + obj042.sqnb.Trim();
                return;
            }
            List<Ent_tticol042> parameterCollection042 = new List<Ent_tticol042>();

            obj042.dele = 4;
            parameterCollection042.Add(obj042);

            int retorno = idal042.wrapRegrind_ActualizaRegistro(ref parameterCollection042, ref strError);

            if (!string.IsNullOrEmpty(strError))
            {
                lblError.Visible = true;
                lblError.Text = strError;
                return;
            }

            if (retorno > 0)
                lblResult.Visible = true;
                lblResult.Text = "Pallet was saved succesfully.";

            txtSQNB.Text = string.Empty;
        }

        void InsertarIngresoPagina()
        {
            if (lblIngreso.Text == "1")
            {
                lblIngreso.Text = "0";

                List<Ent_ttccol301> parameterCollection301 = new List<Ent_ttccol301>();

                obj301.user = Session["user"].ToString();
                obj301.come = "Wrap Regrind Validation";
                obj301.refcntd = 0;
                obj301.refcntu = 0;

                parameterCollection301.Add(obj301);
                int retorno2 = idal301.insertarRegistro(ref parameterCollection301, ref strError);

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