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
    public partial class whInvPrintLabelPhysical : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strError = string.Empty;

            string rutaServ = string.Empty;
            DataRow fila = (DataRow)Session["FilaImprimir"];
            if (fila == null)
               return;

            string zonaId = fila.ItemArray[0].ToString().Trim();
            string labelId = fila.ItemArray[1].ToString().Trim();
            string itemId = fila.ItemArray[2].ToString().Trim();
            string loteId = fila.ItemArray[4].ToString().Trim();          
            string cantidad = fila.ItemArray[5].ToString().Trim();
            string usuario = fila.ItemArray[6].ToString().Trim();
            string fecha = fila.ItemArray[7].ToString().Trim();
            string descItem = Session["descItem"].ToString();
            string unidad = Session["unidad"].ToString(); 
            string siteName = ConfigurationManager.AppSettings["enlaceRet"].ToString();

            rutaServ = "http://" + Request.Url.Authority + "/Barcode/BarcodeHandler.ashx?data=" + labelId + "&code=Code128&dpi=96";
            imgLabelid.Src = !string.IsNullOrEmpty(labelId) ? rutaServ : "";

            rutaServ = "http://" + Request.Url.Authority + "/Barcode/BarcodeHandler.ashx?data=" + itemId + "&code=Code128&dpi=96";
            imgItem.Src = !string.IsNullOrEmpty(itemId) ? rutaServ : "";

            rutaServ = "http://" + Request.Url.Authority + "/Barcode/BarcodeHandler.ashx?data=" + loteId + "&code=Code128&dpi=96";
            imgLot.Src = !string.IsNullOrEmpty(loteId) ? rutaServ : "";

            rutaServ = "http://" + Request.Url.Authority + "/Barcode/BarcodeHandler.ashx?data=" + cantidad + "&code=Code128&dpi=96";
            imgCant.Src = !string.IsNullOrEmpty(cantidad) ? rutaServ : "";
            
            lblQuantity.Text = cantidad + " " + unidad;
            lblDate.Text = fecha;
            lblArticulo.Text = descItem;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "printDiv", "javascript:printDiv('Label');", true);
            Session.Remove("descItem");
            Session.Remove("FilaImprimir");
            Session.Remove("descItem");
            Session.Remove("unidad");
        }
    }
}