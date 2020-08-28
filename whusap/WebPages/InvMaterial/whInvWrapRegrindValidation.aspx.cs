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
    public partial class whInvWrapRegrindValidation : System.Web.UI.Page
    {
        #region Propiedades

        protected static InterfazDAL_tticol042 idal042 = new InterfazDAL_tticol042();
        protected static InterfazDAL_ttccol301 idal301 = new InterfazDAL_ttccol301();
        Ent_ttccol301 obj301 = new Ent_ttccol301();
        Ent_tticol042 obj042 = new Ent_tticol042();
        DataTable resultado = new DataTable();
        string strError = string.Empty;
        public string Therecordhasbeenupdatedsuccessfully = string.Empty;

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
            //Cambiar cultura para manejo de separador decimal
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

                lblError.Visible = false;
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
                lblError.Text = String.Format(mensajes("notfounditems"), obj042.sqnb.Trim());
                return;
            }
            List<Ent_tticol042> parameterCollection042 = new List<Ent_tticol042>();

            obj042.dele = 4;
            parameterCollection042.Add(obj042);

            int retorno = idal042.wrapRegrind_ActualizaRegistro(ref parameterCollection042, ref strError);

            if (!string.IsNullOrEmpty(strError))
            {
                lblError.Visible = true;
                lblError.Text = mensajes("errorupdt");
                return;
            }

            if (retorno > 0)
                lblResult.Visible = true;
            lblResult.Text = Therecordhasbeenupdatedsuccessfully;

            txtSQNB.Text = string.Empty;
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
                int retorno2 = idal301.insertarRegistro(ref parameterCollection301, ref strError);

                if (!string.IsNullOrEmpty(strError))
                {
                    lblError.Visible = true;
                    lblError.Text = strError;
                    return;
                }
            }
        }

        protected void CargarIdioma()
        {
            Therecordhasbeenupdatedsuccessfully = _textoLabels.readStatement(formName, _idioma, "Therecordhasbeenupdatedsuccessfully");
            lblDescRegrind.Text = _textoLabels.readStatement(formName, _idioma, "lblDescRegrind");
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
//
