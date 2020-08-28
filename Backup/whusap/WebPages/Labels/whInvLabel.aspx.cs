using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;

namespace whusap.WebPages.Labels
{
    public partial class whInvLabel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strError = string.Empty;

            string rutaServ = string.Empty;
            DataRow fila = (DataRow)Session["FilaImprimir"];
            if (fila == null)
                return;

            string strOrden = fila.ItemArray[0].ToString();
            string strSecuen = fila.ItemArray[1].ToString();
            string itemId = fila.ItemArray[2].ToString().Trim();
            string cantidad = fila.ItemArray[3].ToString().Trim();
            string usuario = fila.ItemArray[4].ToString().Trim();
            string fecha = fila.ItemArray[5].ToString().Trim();
            string descItem = Session["descItem"].ToString();
            string strMachine = Session["unidad"].ToString();
            
            string siteName = ConfigurationManager.AppSettings["enlaceRet"].ToString();

            rutaServ = "http://" + Request.Url.Authority + "/Barcode/BarcodeHandler.ashx?data=" + itemId + "&code=Code128&dpi=96";
            imgItem.Src = !string.IsNullOrEmpty(itemId) ? rutaServ : "";

            rutaServ = "http://" + Request.Url.Authority + "/Barcode/BarcodeHandler.ashx?data=" + strSecuen + "&code=Code128&dpi=96";
            imgConsec.Src = !string.IsNullOrEmpty(strSecuen) ? rutaServ : "";

            lblWorkOrderLot.Text = strOrden;
            lblWeight.Text = cantidad ;
            lblRollNumber.Text = strSecuen.Remove(1, strSecuen.Trim().LastIndexOf("-") + 1);
            lblDate.Text = fecha;
            lblUsuario.Text = usuario;
            lblArticulo.Text = descItem;
            lblMachine.Text = strMachine;


            ScriptManager.RegisterStartupScript(this, this.GetType(), "printDiv", "javascript:printDiv('Label');", true);
            Session.Remove("descItem");
            Session.Remove("FilaImprimir");
            Session.Remove("descItem");
            Session.Remove("unidad");

        }
    }
}