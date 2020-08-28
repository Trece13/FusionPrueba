using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace whusap
{
    public partial class MDMasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["user"] = "ELOAIZA";
            Session["username"] = "EDWING LOAIZA";
            if (Session["logok"] != "OKYes")
            {
                Session["Message"] = "You must login first, before use this session.";
                //Response.Redirect("~/whlogini.aspx");
            }
           
        }

        protected void btnSessionOut_Click(object sender, EventArgs e)
        {
            Session["logok"] = string.Empty;
            Session["username"] = "";
            Session["Message"] = "";
            Response.Redirect("~/whlogini.aspx");
        }

        protected void btnMainMenu_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Hola");
            Response.Redirect("~/whMenui.aspx");
        }
    }
}