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
using System.Security.Cryptography;
using whusa.Utilidades;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace whusap.WebPages.Login
{
    public partial class whLogIni : System.Web.UI.Page
    {
        #region Propiedades

        public string BaanFusionTittle = string.Empty;
        public string CommandnamenotrecogizedLoginFailure = string.Empty;
        public string Incorrectuserorpassword = string.Empty;

        protected static InterfazDAL_ttccol300 _idalttcol300 = new InterfazDAL_ttccol300();
        private static Mensajes _mensajesForm = new Mensajes();
        private static LabelsText _textoLabels = new LabelsText();
        private static Utils _util = new Utils();
        private static Ent_ttccol300 obj = new Ent_ttccol300();
        private static DataTable resultado = new DataTable();
        private static string formName;
        private static string globalMessages = "GlobalMessages";
        public static string _idioma = String.Empty;
        string strError = string.Empty;

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {

            // Cambiar cultura para manejo de separador decimal
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-CO");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-CO");
            base.InitializeCulture();
            LblErrorLogin.Text = String.Empty;
            if (!IsPostBack)
            {
                if (Session["IsPreviousPage"] == null) { Session.Clear(); }

                formName = "whLogIni.aspx";



                if (Session["ddlIdioma"] == null)
                {
                    Session["ddlIdioma"] = "INGLES";
                }

                _idioma = Session["ddlIdioma"].ToString();

                CargarIdioma();
                string strTitulo = BaanFusionTittle;

                Label control = (Label)Page.Controls[0].FindControl("lblPageTitle");
                control.Text = strTitulo;
                Label Shif = (Label)Page.Controls[0].FindControl("lblShif");
                Shif.Visible = true;
                Shif.Text = "Shift : " + (DateTime.Now.Hour >= 7 && DateTime.Now.Hour < 19 ? "A" : "B");
                Label Date = (Label)Page.Controls[0].FindControl("lblDate");
                IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);
                //Date.Text = "Date: " + DateTime.Now.AddHours(5).ToString("dd MMMM yyyy, hh:mm tt", culture);
                Date.Visible = true;
            }
        }

        protected void LoginButton_Command(object sender, CommandEventArgs e)
        {
            obj = new Ent_ttccol300();
            List<Ent_ttccol300> parameterCollection = new List<Ent_ttccol300>();
            strError = string.Empty;
            switch (e.CommandName)
            {
                case "Login":
                    UserName.Text = UserName.Text.Trim().ToUpperInvariant();
                    obj.user = UserName.Text.Trim();
                    obj.pass = Password.Text.Trim();
                    resultado = _idalttcol300.listaRegistro_Param(ref obj, ref strError);
                    break;

                case "Submit":

                    if ((String)e.CommandArgument == "")
                    {

                    }
                    break;

                default:
                    lblErrorMsg.Text = CommandnamenotrecogizedLoginFailure;
                    break;
            }


            if (resultado.Rows.Count < 1 && !string.IsNullOrEmpty(Password.Text))
            {
                lblErrorMsg.Text = mensajes("user");
                LblErrorLogin.Text = Incorrectuserorpassword;
                return;
            }
            else
            {
                string barCode = UserName.Text;
                var fechaCambioPass = Convert.ToDateTime(resultado.Rows[0]["UFIN"].ToString());
                var dateDiff = (DateTime.Now - fechaCambioPass).Days;

                if (dateDiff >= Convert.ToInt32(ConfigurationManager.AppSettings["tiempoCambioContrasena"].ToString()))
                {
                    divLogin.Visible = false;
                    divChangePassword.Visible = true;
                    return;
                }
                string OldEncrypted = Utils.encodePassword(obj.pass);
                if (resultado.Rows[0]["pswd"].ToString() != Utils.EncryptStringToBytes_Aes(obj.pass))
                {
                    if (resultado.Rows[0]["pswd"].ToString() != obj.pass)
                    {
                        if (resultado.Rows[0]["pswd"].ToString() != OldEncrypted)
                        {
                            lblErrorMsg.Text = mensajes("password");
                            LblErrorLogin.Text = "Incorrect user or password.";
                            return;
                        }
                    }
                }
            }

            obj.nama = resultado.Rows[0]["Nombre"].ToString();
            obj.refcntd = 0;
            obj.refcntu = 0;
            parameterCollection.Add(obj);
            //int retorno = idal.actualizarRegistro(ref parameterCollection, ref strError);

            if (!string.IsNullOrEmpty(strError))
            {
                lblErrorMsg.Text = strError;
                return;
            }

            var user = obj.user.Trim();
            Session["user"] = user;
            Session["username"] = resultado.Rows[0]["Nombre"].ToString();
            Session["logok"] = "OKYes";
            Session["dateSession"] = DateTime.Now.ToShortDateString().ToString();
            Session["psw"] = resultado.Rows[0]["pswd"].ToString().Trim();
            Session["role"] = resultado.Rows[0]["role"].ToString().Trim();
            Session["shif"] = resultado.Rows[0]["shif"].ToString().Trim();


            foreach (var _session in Session)
            {
                Console.WriteLine(_session);
            }
            Response.Redirect(ConfigurationManager.AppSettings["UrlBase"] + "/WebPages/Login/whMenuI.aspx");
        }


        protected void btnForgotPass_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtUserForgotPass.Text))
            {
                obj = new Ent_ttccol300();
                obj.user = TxtUserForgotPass.Text;
                string strError1 = String.Empty;
                _idalttcol300.RecordarContraseña(obj, ref strError1);
            }
        }

        protected void LoginUser_Authenticate(object sender, AuthenticateEventArgs e)
        {

            if (Session["user"] != null && Session["logok"] != null)
            {
                e.Authenticated = true;
                AspSession.Set("user", Session["user"].ToString());
                AspSession.Set("username", Session["username"].ToString());
                AspSession.Set("logok", Session["logok"].ToString());
                lblErrorMsg.Text = "Access granted";
            }
            else
            {
                e.Authenticated = false;
            }

        }


        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            var user = obj.user.Trim();
            var pss = resultado.Rows[0]["pswd"].ToString().Trim();

            if (txtCurrentPassword.Text.Trim() == String.Empty || txtNewPassword.Text.Trim() == String.Empty || txtConfirmPassword.Text.Trim() == String.Empty)
            {
                lblErrorMessage1.Text = mensajes("formempty");
                return;
            }

            if (txtNewPassword.Text.Trim() != txtConfirmPassword.Text.Trim())
            {
                lblErrorMessage1.Text = mensajes("diferentnewpassword");
                return;
            }

            if (txtCurrentPassword.Text.Trim() != pss)
            {
                if (Utils.EncryptStringToBytes_Aes(txtCurrentPassword.Text.Trim()) != pss)
                {
                    lblErrorMessage1.Text = mensajes("passwordincorrect");
                    return;
                }
            }

            if (txtCurrentPassword.Text.Trim() == txtNewPassword.Text.Trim())
            {
                lblErrorMessage1.Text = mensajes("equalpassword");
                return;
            }

            var pass = Utils.EncryptStringToBytes_Aes(txtNewPassword.Text.Trim());

            var validaUpdate = _idalttcol300.updateUFIN(ref user, ref pass, ref strError);

            if (validaUpdate)
            {
                var userlogn = obj.user.Trim();
                Session["user"] = userlogn;
                Session["username"] = resultado.Rows[0]["Nombre"].ToString();
                Session["logok"] = "OKYes";
                Session["dateSession"] = DateTime.Now.ToShortDateString().ToString();
                Session["psw"] = resultado.Rows[0]["pswd"].ToString().Trim();

                Response.Redirect(ConfigurationManager.AppSettings["UrlBase"] + "/WebPages/login/whLogIni.aspx");
            }
            else
            {
                lblErrorMessage1.Text = mensajes("update1");
                return;
            }
        }

        protected void ddlIdioma_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ddlIdioma.SelectedValue)
            {
                case "INGLES":
                    IdiomaIngles();
                    break;
                case "ESPAÑOL":
                    IdiomaEspañol();
                    break;
            }
        }

        #endregion

        #region Metodos

        protected void IdiomaIngles()
        {
            Session["ddlIdioma"] = "INGLES";
            _idioma = "INGLES";
            lblInformation.Text = "Please enter your username and password.";
            lblEncLogin.Text = "Account Information";
            UserNameLabel.Text = "Username";
            PasswordLabel.Text = "Password";
            LoginButton.Text = "Log In";
            ddlIdioma.SelectedValue = "INGLES";
        }

        protected void IdiomaEspañol()
        {
            Session["ddlIdioma"] = "ESPAÑOL";
            _idioma = "ESPAÑOL";
            lblInformation.Text = "Por favor ingrese usuario y contraseña.";
            lblEncLogin.Text = "Inicio de sesión";
            UserNameLabel.Text = "Usuario";
            PasswordLabel.Text = "Contraseña";
            LoginButton.Text = "Iniciar";
            ddlIdioma.SelectedValue = "ESPAÑOL";
        }

        protected string mensajesError(string tipoMensaje)
        {
            string mensaje = String.Empty;

            switch (tipoMensaje)
            {
                case "user":
                    mensaje = _idioma == "INGLES" ? "Invalid User. Please Try Again." : "Usuario incorrecto. Intente nuevamente.";
                    break;
                case "password":
                    mensaje = _idioma == "INGLES" ? "Invalid Password, try again." : "Contraseña incorrecta. Intente nuevamente";
                    break;
                case "update":
                    mensaje = _idioma == "INGLES" ? "Error occurred when updating the information, please contact Administrator"
                                                        : "Ha ocurrido un error mientras se actualizaba la información, contacte al Administrador";
                    break;
            }

            return mensaje;
        }

        protected void CargarIdioma()
        {
            BaanFusionTittle = _textoLabels.readStatement(formName, _idioma, "BaanFusionTittle");
            CommandnamenotrecogizedLoginFailure = _textoLabels.readStatement(formName, _idioma, "CommandnamenotrecogizedLoginFailure");
            Incorrectuserorpassword = _textoLabels.readStatement(formName, _idioma, "Incorrectuserorpassword");

            lblInformation.Text = _textoLabels.readStatement(formName, _idioma, "lblInformation");
            lblEncLogin.Text = _textoLabels.readStatement(formName, _idioma, "lblEncLogin");
            UserNameLabel.Text = _textoLabels.readStatement(formName, _idioma, "UserNameLabel");
            PasswordLabel.Text = _textoLabels.readStatement(formName, _idioma, "PasswordLabel");
            LoginButton.Text = _textoLabels.readStatement(formName, _idioma, "LoginButton");

            lblInformation1.Text = _textoLabels.readStatement(formName, _idioma, "lblInformation1");
            lblEncLogin1.Text = _textoLabels.readStatement(formName, _idioma, "lblEncLogin1");
            ddlIdioma1.Text = _textoLabels.readStatement(formName, _idioma, "ddlIdioma1");
            lblCurrentPassword.Text = _textoLabels.readStatement(formName, _idioma, "lblCurrentPassword");
            lblNewPassword.Text = _textoLabels.readStatement(formName, _idioma, "lblNewPassword");
            lblConfirmPassword.Text = _textoLabels.readStatement(formName, _idioma, "lblConfirmPassword");
            btnChangePassword.Text = _textoLabels.readStatement(formName, _idioma, "btnChangePassword");
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
