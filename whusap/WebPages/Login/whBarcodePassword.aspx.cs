using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Web.Configuration;
using whusa.Utilidades;
using whusa.Entidades;
using whusa.Interfases;

using System.Drawing;


namespace whusap.WebPages.Login
{
    public partial class whBarcodePassword : System.Web.UI.Page
    {
        protected static string _idioma;
        protected static InterfazDAL_ttccol300 _idalttcol300 = new InterfazDAL_ttccol300();
        protected static InterfazDAL_ttccol301 idal301 = new InterfazDAL_ttccol301();
        private static LabelsText _textoLabels = new LabelsText();
        private static string formName;
        private static Ent_ttccol300 obj = new Ent_ttccol300();
        Ent_ttccol301 obj301 = new Ent_ttccol301();
        private static DataTable resultado = new DataTable();
        public string UrlBaseBarcode = WebConfigurationManager.AppSettings["UrlBaseBarcode"].ToString();
        string strError = string.Empty;
        private static string globalMessages = "GlobalMessages";
        private static Mensajes _mensajesForm = new Mensajes();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["IsPreviousPage"] == null) { Session.Clear(); }

                formName = "whBarcodePassword.aspx";
                if (Session["ddlIdioma"] == null)
                {
                    Session["ddlIdioma"] = "INGLES";
                }

                _idioma = Session["ddlIdioma"].ToString();

                CargarIdioma();
                if (lblIngreso.Text == "")
                {
                    lblIngreso.Text = "1";
                }
            }
        }
        protected void btnVerificate_Click(object sender, EventArgs e)
        {
            if (UserName.Text.Trim() == String.Empty)
            {

                lblErrorMessage1.Text = _idioma == "INGLES" ? "UserName is required" : "el usuario es requerido"; ;
                return;
            }
            obj = new Ent_ttccol300();
            obj.user = UserName.Text.Trim().ToUpperInvariant();
            resultado = _idalttcol300.listaRegistro_Param(ref obj, ref strError);
            if (resultado.Rows.Count < 1 && !string.IsNullOrEmpty(UserName.Text))
            {
                LblUserNameDontExist.Visible = true;
                return;
            }
            else {
                UserName.Text = resultado.Rows[0]["userN"].ToString().Trim().ToUpperInvariant();
                LblUserNameDontExist.Visible = false;   
                UserName.Enabled = false; 
                btnValidate.Visible = false;
                btnGeneratePassword.Visible = true;
                LblUserNameExist.Visible = true;
                LblUserNameExist.Text = resultado.Rows[0]["Nombre"].ToString();
                LblUserNameExist.Attributes["style"] = "font-weight:bold;border:solid 1px;font-size:20px";
                
                return;
            }

        
        }

        protected void btnGeneratePassword_Click(object sender, EventArgs e)
        {
            var generator = new  GeneradorPassword(minimumLengthPassword: 8,
                                      maximumLengthPassword: 15,
                                      minimumUpperCaseChars: 1,
                                      minimumNumericChars: 1,
                                      minimumSpecialChars: 1);
            string password = generator.Generate();
            var pass = Utils.EncryptStringToBytes_Aes(password);
            var user = UserName.Text.Trim().ToUpperInvariant();
            var validaUpdate = _idalttcol300.updateUFIN(ref user, ref pass, ref strError);
            if (validaUpdate)
            {
                string siteName = ConfigurationManager.AppSettings["enlaceRet"].ToString();
                string rutaServUserName = string.Empty;
                string rutaServPassword = string.Empty;
                rutaServUserName = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + UserName.Text.Trim().ToUpperInvariant() + "&code=Code128&dpi=96";
                imgUniqueUserName.Src = !string.IsNullOrEmpty(UserName.Text.Trim().ToUpperInvariant()) ? rutaServUserName : "";
                imgUniqueUserName.Style.Add("width", "1.8in");
                imgUniqueUserName.Style.Add("height", ".6in");
                rutaServPassword = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + password + "&code=Code128&dpi=96&printDataText=false";
                imgUniquePassword.Src = !string.IsNullOrEmpty(password) ? rutaServPassword : "";
                imgUniquePassword.Style.Add("width", "1.8in");
                imgUniquePassword.Style.Add("height", ".6in");
                Page.ClientScript.RegisterStartupScript(GetType(), "none", "<script>printContent('uno');</script>", false);
                btnGeneratePassword.Enabled = false;
                return;
                
            } 
        }
        protected void ddlIdioma_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ddlIdioma1.SelectedValue)
            {
                case "INGLES":
                    IdiomaIngles();
                    break;
                case "ESPAÑOL":
                    IdiomaEspañol();
                    break;
            }
        }
        protected void CargarIdioma()
        {
            UserNameLabel.Text = _textoLabels.readStatement(formName, _idioma, "UserNameLabel");
            LblUserNameDontExist.Text = _textoLabels.readStatement(formName, _idioma, "LblUserNameDontExist");
            ddlIdioma1.Text = _textoLabels.readStatement(formName, _idioma, "ddlIdioma1");
            btnValidate.Text = _textoLabels.readStatement(formName, _idioma, "btnValidate");
            btnGeneratePassword.Text = _textoLabels.readStatement(formName, _idioma, "btnGeneratePassword");
        }
        protected void IdiomaIngles()
        {
            Session["ddlIdioma"] = "INGLES";
            _idioma = "INGLES";
            UserNameLabel.Text = "Username:";
            btnValidate.Text = "Query";
            btnGeneratePassword.Text = "Password";
            LblUserNameDontExist.Text = "UserName doesn't exist";
        }

        protected void IdiomaEspañol()
        {
            Session["ddlIdioma"] = "ESPAÑOL";
            _idioma = "ESPAÑOL";
            LblUserNameDontExist.Text = "Usuario no existe";
            UserNameLabel.Text = "Usuario";
            btnValidate.Text = "Consultar";
            btnGeneratePassword.Text = "Contraseña";
        }
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
                    lblErrorMessage1.Visible = true;
                    lblErrorMessage1.Text = strError;
                    return;
                }
            }
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
    }
}