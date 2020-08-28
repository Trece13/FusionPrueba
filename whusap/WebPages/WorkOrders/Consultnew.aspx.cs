using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using whusa.Entidades;
using System.Data;
using whusa.Interfases;
using Newtonsoft.Json;

namespace whusap.WebPages.WorkOrders
{
    public partial class Consultnew : System.Web.UI.Page
    {
        public static IntefazDAL_tticol082 Itticol082 = new IntefazDAL_tticol082();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string ClickQuery()
        {
            Console.WriteLine("Entro en ClickQuery...");
            string strError = string.Empty;
            
            if (strError == string.Empty)
            {
                DataTable mydt = Itticol082.ConsultarOtrosRegistros();
                return JsonConvert.SerializeObject(mydt);
               
            }
            else
            {
                return strError;
            }

        }
    }
}