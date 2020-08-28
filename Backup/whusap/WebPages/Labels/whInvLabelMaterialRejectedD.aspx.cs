using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using whusa.Entidades;
using whusa.Interfases;

namespace whusap.WebPages.Labels
{
    public partial class whInvLabelMaterialRejectedD : System.Web.UI.Page
    {
        string strError = string.Empty;
        // string Aplicacion = "WEBAPP";

        protected void Page_Load(object sender, EventArgs e)
        {
            string rutaServ = string.Empty;
            DataRow fila = (DataRow)Session["FilaImprimir"];
            if (fila == null)
                return;
            string strItem = fila.ItemArray[0].ToString().Trim();
            string strQuantity = fila.ItemArray[3].ToString().Trim();
            string strArticulo = fila.ItemArray[1].ToString().Trim();
            lblArticulo.Text = strArticulo;
            lblOrder.Text = Session["Orden"].ToString();
            lblUnit.Text = fila.ItemArray[2].ToString().Trim();
            lblQuantity.Text = strQuantity;
            lblUser.Text = fila.ItemArray[6].ToString().Trim();
            lblDate.Text = fila.ItemArray[8].ToString().Trim();
            string siteName = ConfigurationManager.AppSettings["enlaceRet"].ToString();
                                 
            rutaServ = "http://" + Request.Url.Authority + "/Barcode/BarcodeHandler.ashx?data=" + strItem + "&code=Code128&dpi=96";
            imgItem.Src = !string.IsNullOrEmpty(strItem) ? rutaServ : "";

            rutaServ = "http://" + Request.Url.Authority + "/Barcode/BarcodeHandler.ashx?data=" + strQuantity + "&code=Code128&dpi=96";
            imgQuantity.Src = !string.IsNullOrEmpty(strQuantity) ? rutaServ : "";

            //Type csType = this.GetType();
            //ClientScriptManager scriptBlock = Page.ClientScript;

            //string cstext1 = "alert(Imprimir?); printDiv('Label');";
            //scriptBlock.RegisterStartupScript(csType, "printerLabel", cstext1, true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "printDiv", "javascript:printDiv('Label');", true);
            Session.Remove("IsPreviousPage");

        }
    }
}