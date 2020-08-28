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


namespace whusap
{
    public partial class MDMasterPage : System.Web.UI.MasterPage
    {
        private static whusa.Utilidades.Seguimiento log = new Seguimiento();
        private static StackTrace stackTrace = new StackTrace();
        private static MethodBase method = MethodBase.GetCurrentMethod();
        private static string metodo = method.Name;
        
        string rutaServ = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            //base.OnLoad(e);
            if (!IsPostBack)
            {
                Response.Cache.SetExpires(DateTime.UtcNow.AddSeconds(-10));
                Response.Cache.SetNoServerCaching();
                Response.Cache.SetAllowResponseInBrowserHistory(false);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetNoStore(); 
                Page.Header.DataBind();
            }

            string accessFrom = "true";//string.Empty;
            string userReq = string.Empty;
            string usernameReq = string.Empty;
            string logokReq = string.Empty;
            String siteName = ConfigurationManager.AppSettings["enlaceRet"].ToString();
            String bodyColor = string.Empty;

            rutaServ = "http://" + Request.Url.Authority + "/" + siteName + "/";

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


                if (Request.QueryString.Count > 0 || Session.Keys.Count > 0 )                  
                {
                    userReq = Request.QueryString["Valor1"];
                    usernameReq = Request["Valor3"];
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

                lblUserLogin.Text = Session["user"].ToString();
                lblUserName.Text = Session["username"].ToString();
                Session["openForm"] = "true";

            }
            catch (Exception ex)
            { log.escribirError(ex.Message, stackTrace.GetFrame(2).GetMethod().Name, metodo, method.ReflectedType.Name); }//throw ex.InnerException; }

        }

        protected void btnSessionOut_Click(object sender, EventArgs e)
        {
            //To enable/disable all validators of a validation group    
            foreach (BaseValidator val in Page.Validators) { val.Enabled = false; }  

            Session["logok"] = string.Empty;
            Session["username"] = "";
            Session["Message"] = "";
            Request.Cookies.Remove("userAppInit");
            Response.Redirect(rutaServ + "whlogini.aspx", true);
            unLoadPoolApp();
        }

        protected void btnMainMenu_Click(object sender, EventArgs e)
        {
            //To enable/disable all validators of a validation group    
            foreach (BaseValidator val in Page.Validators) { val.Enabled = false; }

            Session["logok"] = string.Empty;
            Session["username"] = "";
            Session["Message"] = "";
            Response.Redirect(rutaServ + "whMenui.aspx", false);

            //Server.Transfer("~/WebPages/Login/whMenuI.aspx");

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

    }
}