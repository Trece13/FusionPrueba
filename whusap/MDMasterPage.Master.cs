using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using whusa.Utilidades;
using System.Diagnostics;
using System.Reflection;
using System.Collections;
using System.Web.Configuration;
using whusa.Interfases;
using System.Data;


namespace whusap
{
    public partial class MDMasterPage : System.Web.UI.MasterPage
    {
        protected static InterfazDAL_ttccol303 idal = new InterfazDAL_ttccol303();
        private static whusa.Utilidades.Seguimiento log = new Seguimiento();
        private static StackTrace stackTrace = new StackTrace();
        private static MethodBase method = MethodBase.GetCurrentMethod();
        private static Mensajes _mensajesForm = new Mensajes();
        private static LabelsText _textoLabels = new LabelsText();
        private static string metodo = method.Name;
        private static string _idioma = String.Empty;
        private static string formName;
        public string namePage = string.Empty;

        string rutaServ = string.Empty;
        string rutaRetorno = String.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            log.escribirError("entra al page load","master","Load","master");
            string url = Request.Url.AbsolutePath.ToString().Trim();
            //string url = "/fusionpub/WebPages/InvReceipts/whInvReceiptRawMaterialNew.aspx";
            
            
            DataTable menupages = idal.datosMenu_Param(Session["user"].ToString(), url);

            if (menupages.Rows.Count > 0)
            {
                if (menupages.Rows.Count > 1)
                {
                    foreach (DataRow page in menupages.Rows)
                    {
                        if (page["PROGRAMA"].ToString().Trim().IndexOf("tipoFormulario="+Request.QueryString["tipoFormulario"].ToString().Trim()) != -1)
                        {
                            namePage = page["MENG"].ToString() + " - ";
                        }
                    }
                }
                else
                {
                    namePage = menupages.Rows[0]["MENG"].ToString() + " - ";
                }
            }

            //namePage = ( idal.datosMenu_Param(Session["user"].ToString(), url).Trim() == "" ? "" : idal.datosMenu_Param(Session["user"].ToString(), url).Trim() + " - ") + "Phoenix  Operation Portal";
            LblHome.Text = namePage + "Phoenix  Operation Portal";
            if (Session.IsNewSession == true)
            //if (Session["SessionID"] == null || !System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                string UrlBase = WebConfigurationManager.AppSettings["UrlBase"].ToString();
                Response.Redirect(UrlBase+"/Webpages/Login/WhLogIni.aspx");

            }
            Label Shif = (Label)Page.Controls[0].FindControl("lblShif1");
            Shif.Visible = true;
            Shif.Text = "Shift: " + Session["shif"].ToString();
            Label User = (Label)Page.Controls[0].FindControl("lblName1");
            User.Text = Session["username"].ToString();
            User.Visible = true;
            Label Date = (Label)Page.Controls[0].FindControl("lblDate1");
            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);
            //Date.Text = "Date: " + DateTime.Now.AddHours(5).ToString("dd MMMM yyyy, hh:mm tt", culture);
            Date.Visible = true;

            string accessFrom = "true";//string.Empty;
            string userReq = string.Empty;
            string usernameReq = string.Empty;
            string logokReq = string.Empty;
            String siteName = ConfigurationManager.AppSettings["enlaceRet"].ToString();
            String bodyColor = string.Empty;

            //rutaServ = UrlBaseBarcode + "/" + siteName + "/";
            rutaServ = ConfigurationManager.AppSettings["UrlBase"];
            rutaRetorno = ConfigurationManager.AppSettings["enlaceRet"];

            if (Request.Cookies["userAppInit"] != null) { accessFrom = Request.Cookies["userAppInit"]["initForm"].ToString(); }

            if (string.IsNullOrEmpty(accessFrom))
            {
                Session["Message"] = "You must login first, before use this session.";
                Response.Redirect(rutaServ + "whlogini.aspx", false);
                return;
            }

            try
            { bodyColor =  ConfigurationManager.AppSettings["bodyColor"].ToString(); }
            catch(Exception) { bodyColor = string.Empty; }

            try
            {
                if (!string.IsNullOrEmpty(bodyColor.Trim()))
                {
                    Cuerpo.Attributes.Add("style", "background-color: " + bodyColor + "");
                }

                var user = Session["user"];
                var name = Session["username"];
                var dateSession = Session["dateSession"];

                lblUserLogin.Text = user != null ? user.ToString() : Request.QueryString["Valor1"];
                lblUserName.Text = name != null ? name.ToString() : Request.QueryString["Valor3"];
                lblDateSession.Text = dateSession != null ? dateSession.ToString() : DateTime.Now.ToShortDateString().ToString();
                Session["openForm"] = "true";


                if (Request.QueryString.Count > 0 || Session.Keys.Count > 0 )                  
                {
                    //Cambio Javier 20 de Marzo, porque la cadena de conexion lleva 
                    //un ? y no comprende el valor 1 y genera error al ingresar al
                    //ingresar la primera vez.
                    //userReq = Request.QueryString["Valor1"];
                    //usernameReq = Request["Valor3"];
                    userReq = user.ToString();
                    usernameReq = name.ToString();
                    logokReq = Request["Valor2"];
                    if (Session["user"] == null)
                        Session["user"] = userReq.Trim();
                    if (Session["username"] == null)
                        Session["username"] = usernameReq.Trim();
                    if (Session["logok"] == null)
                        Session["logok"] = logokReq.Trim();

                    if (logokReq.ToString() != "OKYes" || string.IsNullOrEmpty(accessFrom))
                    {
                        Session["Message"] = "You must login first, before use this session.";
                        Response.Redirect(rutaServ + "whlogini.aspx", false);
                    }
                }
                else { Response.Redirect(rutaServ + "whMenui.aspx", false); }


            }
            catch (Exception ex)
            { log.escribirError(ex.Message, stackTrace.GetFrame(2).GetMethod().Name, metodo, method.ReflectedType.Name); }//throw ex.InnerException; }

            //base.OnLoad(e);
            if (!IsPostBack)
            {
                Response.Cache.SetExpires(DateTime.UtcNow.AddSeconds(-10));
                Response.Cache.SetNoServerCaching();
                Response.Cache.SetAllowResponseInBrowserHistory(false);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetNoStore();
                Page.Header.DataBind();

                if (Session["ddlIdioma"] != null)
                {
                    _idioma = Session["ddlIdioma"].ToString();
                }
                else
                {
                    _idioma = "INGLES";
                }

                ddlIdioma.SelectedValue = _idioma;
                formName = "MDMasterPage.Master";

                CargarIdioma();
            }
        }

        protected void btnSessionOut_Click(object sender, EventArgs e)
        {
            //To enable/disable all validators of a validation group    
            foreach (BaseValidator val in Page.Validators) { val.Enabled = false; }  

            Session["logok"] = string.Empty;
            Session["username"] = "";
            Session["Message"] = "";
            Request.Cookies.Remove("userAppInit");

            if (Request.Url.AbsoluteUri.Contains(ConfigurationManager.AppSettings["sitioConRetorno"].ToString()))
            {
                Response.Redirect(rutaRetorno + "whLogIni.aspx", true);
            }

            Response.Redirect(rutaServ + "/WebPages/Login/whLogIni.aspx", true);
            //Response.Redirect(/*rutaServ + "/WebPages/Login*/"http://gpbfnew.grupophoenix.com:82/whusa/whLogIni.aspx", true);
            unLoadPoolApp();
        }

        protected void btnMainMenu_Click(object sender, EventArgs e)
        {
            //To enable/disable all validators of a validation group    
            foreach (BaseValidator val in Page.Validators) { val.Enabled = false; }

            if (Request.Url.AbsoluteUri.Contains(ConfigurationManager.AppSettings["sitioConRetorno"].ToString()))
            {
                Response.Redirect(rutaRetorno + "whMenui.aspx", true);
            }

            Response.Redirect(rutaServ + "/WebPages/Login/whMenui.aspx", false);
            
            unLoadPoolApp();
        }

        protected void unLoadPoolApp()
        {
            
            if (AppDomain.CurrentDomain.IsDefaultAppDomain())
                AppDomain.CurrentDomain.ProcessExit += ProcessQuitHandler;
            else
                AppDomain.CurrentDomain.DomainUnload += ProcessQuitHandler;
        }

        private void ProcessQuitHandler(object sender, EventArgs e)
        {
            this.Dispose();
        }

        protected String openForm
        {
            get { return hOpen.Value; }
            set { hOpen.Value = value; }
        }

        protected void CargarIdioma()
        {
            lblDescUser.Text = _textoLabels.readStatement(formName, _idioma, "lblDescUser");
            lblDescUsername.Text = _textoLabels.readStatement(formName, _idioma, "lblDescUsername");
            btnMainMenu.Text = _textoLabels.readStatement(formName, _idioma, "btnMainMenu");
            btnCloseSession.Text = _textoLabels.readStatement(formName, _idioma, "btnCloseSession");
            lblDescDateSession.Text = _textoLabels.readStatement(formName, _idioma, "lblDescDateSession");
            lblIdiom.Text = _textoLabels.readStatement(formName, _idioma, "lblIdiom");
        }

        protected void ddlIdioma_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlIdioma.SelectedValue != "")
            {
                switch (ddlIdioma.SelectedValue)
                {
                    case "INGLES":
                        Session["ddlIdioma"] = "INGLES";
                        CargarIdioma();
                        break;
                    case "ESPAÑOL":
                        Session["ddlIdioma"] = "ESPAÑOL";
                        CargarIdioma();
                        break;
                }
                Response.Redirect(Request.Url.AbsoluteUri);
            }
        }

        public string strError { get; set; }
    }
}