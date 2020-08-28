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
using System.Configuration;

namespace whusap.WebPages.WorkOrders
{
    public partial class Consulta : System.Web.UI.Page
    {
        public static IntefazDAL_tticol082 Itticol082 = new IntefazDAL_tticol082();
        public int CicloPaginacion = 0;
        public int CicloActualizacion = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            CicloPaginacion = Convert.ToInt32(ConfigurationManager.AppSettings["CicloPaginacion"].ToString());
            CicloActualizacion = Convert.ToInt32(ConfigurationManager.AppSettings["CicloActualizacion"].ToString());
        }

        [WebMethod]
        public static string ClickQuery()
        {
            Console.WriteLine("Entro en ClickQuery...");
            string strError = string.Empty;
            DataTable ListaRegistroCustomer = Itticol082.ConsultarTticol082();
            if (strError == string.Empty)
            {
                return JsonConvert.SerializeObject(ListaRegistroCustomer);
            }
            else
            {
                return strError;
            }

        }
    }
}