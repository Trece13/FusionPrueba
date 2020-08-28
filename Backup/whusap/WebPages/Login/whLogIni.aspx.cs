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

namespace whusap.WebPages.Login
{
    public partial class whLogIni : System.Web.UI.Page
    {

        string strError = string.Empty;
        // string Aplicacion = "WEBAPP";
        DataTable resultado = new DataTable();

        protected static InterfazDAL_ttccol300 idal = new InterfazDAL_ttccol300();
        Ent_ttccol300 obj = new Ent_ttccol300();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Cambiar cultura para manejo de separador decimal
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-CO");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-CO");
            base.InitializeCulture();

            if (!IsPostBack)
            {
                if (Session["IsPreviousPage"] == null) { Session.Clear(); }

                string strTitulo = "BAAN FUSION [Mobile Applications]";
                Label control = (Label)Page.Controls[0].FindControl("lblPageTitle");
                control.Text = strTitulo;
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
                    LoginUser.UserName = LoginUser.UserName.Trim().ToUpperInvariant();
                    obj.user = LoginUser.UserName.Trim();
                    obj.pass = LoginUser.Password.Trim();
                    resultado = idal.listaRegistro_Param(ref obj, ref strError);
                    break;

                case "Submit":

                    if ((String)e.CommandArgument == "")
                    {
                       // Message.Text += ".";
                    }
                    break;

                default:

                    // The command name is not recognized. Display an error message.
                     lblErrorMsg.Text = "Command name not recogized. Login Failure";
                    break;
            }
            if (resultado.Rows.Count < 1)
            { 
                lblErrorMsg.Text = "Invalid User. Please Try Again.";
                return; 
            }
            else
            {
                if (resultado.Rows[0]["pswd"].ToString() != obj.pass)
                {
                    lblErrorMsg.Text = "Invalid Password, try again.";
                    return;
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

            Session["user"] = obj.user.Trim();
            Session["username"] = resultado.Rows[0]["Nombre"].ToString();
            Session["logok"] = "OKYes";

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
    }
}