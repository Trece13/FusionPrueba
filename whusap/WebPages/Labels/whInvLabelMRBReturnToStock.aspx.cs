using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Globalization;
using whusa.Entidades;
using whusa.Interfases;
using System.Web.Configuration;


namespace whusap.WebPages.Labels
{
    public partial class whInvLabelMRBReturnToStock : System.Web.UI.Page
    {
        string strError = string.Empty;
        // string Aplicacion = "WEBAPP";
        public string UrlBaseBarcode = WebConfigurationManager.AppSettings["UrlBaseBarcode"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {

            DataTable DTMyRegrind = (DataTable)Session["DTregrind"];
            string MyRegrind = Session["MyRegrind"].ToString();
            string disposition = Session["disposition"].ToString();

            string MyRegrindId = string.Empty;
            string MyRegrindDesc = string.Empty;

            foreach (DataRow item in DTMyRegrind.Rows)
            {
                if (item["item"].ToString().Trim() == MyRegrind)
                {
                    MyRegrindId = item["item"].ToString().Trim();
                    MyRegrindDesc = item["descri"].ToString().Trim();
                }
            }



            string rutaServ = string.Empty;
            DataRow fila = (DataRow)Session["FilaImprimir"];
            if (fila == null)
                return;
            string strItem = fila.ItemArray[0].ToString().Trim() ;
            string strPallet = fila.ItemArray[9].ToString().Trim(); //Pallet
            var war = Session["war"];

            double strQuantity = disposition == "stock" ? Convert.ToDouble(fila.ItemArray[3]) : ((Convert.ToDouble(fila.ItemArray[3]) * Convert.ToDouble(fila.ItemArray[7])) / 2.2046);
            strQuantity = Math.Round(strQuantity, 2);
            string strArticulo = fila.ItemArray[1].ToString().Trim();
            lblArticulo.Text = string.IsNullOrEmpty(MyRegrindDesc) ? strArticulo : MyRegrindDesc;
           // lblOrder.Text = Session["Orden"].ToString();
            lblUnit.Text = disposition == "stock" ? fila.ItemArray[2].ToString().Trim() : (fila.ItemArray[2].ToString().Trim() == "CJ" ? "KG" : fila.ItemArray[2].ToString().Trim());
           // LblComment.Text = fila.ItemArray[6].ToString().Trim();
            lblQuantity.Text = strQuantity.ToString().Trim();
            lblwarehouse.Text = war.ToString().Trim();
            //lblUser.Text = fila.ItemArray[9].ToString().Trim();
            //lblDate.Text = fila.ItemArray[8].ToString().Trim();

            

            string siteName = ConfigurationManager.AppSettings["enlaceRet"].ToString();

            rutaServ = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + strItem + "&code=Code128&dpi=96";
            imgItem.Src = !string.IsNullOrEmpty(strItem) ? rutaServ : "";

            rutaServ = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + strPallet + "&code=Code128&dpi=96";
            imgPallet.Src = !string.IsNullOrEmpty(strPallet.ToString().Trim()) ? rutaServ : "";

            //Type csType = this.GetType();
            //ClientScriptManager scriptBlock = Page.ClientScript;

            //string cstext1 = "alert(Imprimir?); printDiv('Label');";
            //scriptBlock.RegisterStartupScript(csType, "printerLabel", cstext1, true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "printDiv", "javascript:printDiv('Label');", true);
            Session.Remove("IsPreviousPage");

        }
    }
}