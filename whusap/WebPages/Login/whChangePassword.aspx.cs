using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using whusa.Interfases;
using whusa.Utilidades;
using System.Threading;
using System.Globalization;
using whusa.Entidades;
using System.Configuration;

namespace whusap.WebPages.Login
{
    public partial class whChangePassword : System.Web.UI.Page
    {
        #region Propiedades

            protected static InterfazDAL_ttccol300 _idalttcol300 = new InterfazDAL_ttccol300();
            private static Mensajes _mensajesForm = new Mensajes();
            private static LabelsText _textoLabels = new LabelsText();
            Ent_ttccol300 obj = new Ent_ttccol300();
            private static string formName;
            private static string globalMessages = "GlobalMessages";
            private static string _idioma = String.Empty;
            private static string strError = string.Empty;

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
                    if (Session["IsPreviousPage"] == null) { Session.Clear(); }

                    formName = Request.Url.AbsoluteUri.Split('/').Last();
                    if (formName.Contains('?'))
                    {
                        formName = formName.Split('?')[0];
                    }

                    string strTitulo = "BAAN FUSION [Mobile Applications]";
                    Label control = (Label)Page.Controls[0].FindControl("lblPageTitle");
                    control.Text = strTitulo;

                    if (Session["ddlIdioma"] == null)
                    {
                        Session["ddlIdioma"] = "INGLES";
                    }

                    _idioma = Session["ddlIdioma"].ToString();

                    txtCurrentPassword.Text = String.Empty;
                    txtNewPassword.Text = String.Empty;
                    txtConfirmPassword.Text = String.Empty;

                    CargarIdioma();
                }
            }

        protected void btnUpdate_Click(object sender, EventArgs e)
            {
                var user = Session["user"] == null ? Request.QueryString["user"] : Session["user"].ToString();


                if (txtCurrentPassword.Text.Trim() == String.Empty || txtNewPassword.Text.Trim() == String.Empty || txtConfirmPassword.Text.Trim() == String.Empty)
                {
                    lblErrorMsg.Text = mensajes("formempty");
                    return;
                }

                if (txtNewPassword.Text.Trim() != txtConfirmPassword.Text.Trim())
                {
                    lblErrorMsg.Text = mensajes("diferentnewpassword");
                    return;
                }

                if (txtCurrentPassword.Text.Trim() != Session["pswd"].ToString().Trim())
                {
                    lblErrorMsg.Text = mensajes("passwordincorrect");
                    return;
                }

                if (txtCurrentPassword.Text.Trim() == txtNewPassword.Text.Trim())
                {
                    lblErrorMsg.Text = mensajes("equalpassword");
                    return;
                }

                var pass = txtNewPassword.Text.Trim();

                var validaUpdate = _idalttcol300.updateUFIN(ref user, ref pass, ref strError);

                if (validaUpdate)
                {
                    Response.Redirect(ConfigurationManager.AppSettings["UrlBase"] + "/WebPages/Login/whMenuI.aspx");
                }
                else
                {
                    lblErrorMsg.Text = mensajes("update");
                    return;
                }
            }

        protected void ddlIdioma_OnSelectedIndexChanged(object sender, EventArgs e)
            {
                switch (ddlIdioma.SelectedValue)
                {
                    case "INGLES":
                        Session["ddlIdioma"] = "INGLES";
                        _idioma = "INGLES";
                        CargarIdioma();
                        break;
                    case "ESPAÑOL":
                        Session["ddlIdioma"] = "ESPAÑOL";
                        _idioma = "ESPAÑOL";
                        CargarIdioma();
                        break;
                }
            }

        #endregion

        #region Metodos

        protected void CargarIdioma()
        {
            lblInformation.Text = _textoLabels.readStatement(formName, _idioma, "lblInformation");
            lblEncLogin.Text = _textoLabels.readStatement(formName, _idioma, "lblEncLogin");
            lblCurrentPassword.Text = _textoLabels.readStatement(formName, _idioma, "lblCurrentPassword");
            lblNewPassword.Text = _textoLabels.readStatement(formName, _idioma, "lblNewPassword");
            lblConfirmPassword.Text = _textoLabels.readStatement(formName, _idioma, "lblConfirmPassword");
            LoginButton.Text = _textoLabels.readStatement(formName, _idioma, "LoginButton");
            ddlIdioma.SelectedValue = _idioma;
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