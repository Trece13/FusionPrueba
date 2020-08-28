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
using System.Diagnostics;
using whusa.Utilidades;

namespace whusap.WebPages.Archivos
{
    public partial class whInvOpenFiles : System.Web.UI.Page
    {
        #region Propiedades

            protected static InterfazDAL_tticol011 idal011 = new InterfazDAL_tticol011();
            Ent_tticol011 obj011 = new Ent_tticol011();
            string strError = string.Empty;
            DataTable resultado = new DataTable();
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
            // Cambiar cultura para manejo de separador decimal
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-CO");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-CO");
            base.InitializeCulture();
            //btnSave.Visible = false;

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
                txtMachine.TextChanged += new EventHandler(txtMachine_listMachine);
                Page.Form.DefaultButton = btnChangeMac.UniqueID;
            }
        }

        protected void txtMachine_listMachine(object sender, EventArgs e)
        {
            btnSave.Visible = false;
            lblError.Visible = true;
            lblError.Text = string.Empty;
            btnChangeMac.Enabled = true;
            btnChangeDir.Enabled = true;
            txtMachine.Enabled = false;
            txtMachine.Text = txtMachine.Text.ToUpperInvariant();
            lblError.Text = string.Empty; ;
            obj011.mcno = ((TextBox)sender).Text.Trim().ToUpperInvariant(); ;  //listMachine.SelectedValue.Trim().ToUpperInvariant();

            resultado = idal011.listaRegistrosMaquina_Param(ref obj011, ref strError);

            // Para este caso strError muestra los mensajes de resutado de la consulta para cuando no hay filas 
            // o cuando existe mas de una fila

            if (!string.IsNullOrEmpty(strError))
            {
                btnChangeMac.Enabled = false;
                txtMachine.Enabled = true;
                lblError.Text = String.Format(mensajes("machinenotexists"), obj011.mcno);

                return;
            }

            lblError.Text = string.Empty;
            lblError.Visible = false;
            btnSave.Visible = true;
        }

        protected void btnChangeMac_Click(object sender, EventArgs e)
        {
            this.txtMachine.Enabled = true;
            this.txtMachine.Text = string.Empty;
        }

        protected void btnChangeDir_Click(object sender, EventArgs e)
        {
            this.txtDirectory.Enabled = true;
            this.txtDirectory.Text = string.Empty;
            btnSave.Visible = true;
        }

        protected void btnSave_Click(object sender, EventArgs e)
            {
                lblError.Visible = true;
                lblError.Text = string.Empty;
                strError = string.Empty;
                string ruta = string.Empty;
                ruta = txtDirectory.Text.Trim() + _idioma == "INGLES" ? "Maquina" : "Machine";

                try
                {
                    System.Diagnostics.Process.Start(ruta + txtMachine.Text.Trim() + ".PDF");
                }
                catch (Exception ex)
                {
                    lblError.Text = String.Format(mensajes("filenotfound"), ruta);
                }
            }

        #endregion

        #region Metodos

        protected void CargarIdioma()
        {
            lblMachine.Text = _textoLabels.readStatement(formName, _idioma, "lblMachine");
            btnChangeMac.Text = _textoLabels.readStatement(formName, _idioma, "btnChangeMac");
            lblDirectory.Text = _textoLabels.readStatement(formName, _idioma, "lblDirectory");
            btnChangeDir.Text = _textoLabels.readStatement(formName, _idioma, "btnChangeDir");
            btnSave.Text = _textoLabels.readStatement(formName, _idioma, "btnSave");
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