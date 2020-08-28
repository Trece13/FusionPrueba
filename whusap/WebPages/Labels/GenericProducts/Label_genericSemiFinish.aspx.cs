using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Web.Configuration;


namespace whusap.WebPages.Labels.GenericProducts
{
    public partial class Label_genericSemiFinish : System.Web.UI.Page
    {
        public string UrlBaseBarcode = WebConfigurationManager.AppSettings["UrlBaseBarcode"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            string strError = string.Empty;

            string rutaServ = string.Empty;
            DataRow fila = (DataRow)Session["FilaImprimir"];
            if (fila == null)
                return;

            string strOrden = fila["ORDEN"].ToString(); //fila.ItemArray[0].ToString();
            string strSecuen = Session["Consec"].ToString();
            string itemId = fila["ITEM"].ToString();//fila.ItemArray[2].ToString().Trim();
            string cantidad = fila["FACTOR"].ToString();//fila.ItemArray[3].ToString().Trim().Replace('-',' ');
            string bodega = fila.ItemArray[4].ToString().Trim();
            string usuario = fila.ItemArray[5].ToString().Trim();
            string fecha = DateTime.Now.ToShortDateString().Replace('/','.');
            string time = DateTime.Now.ToShortTimeString();
            string descItem = fila["DESCI"].ToString();  //Session["descItem"].ToString();
            //string unidad = Session["unidad"].ToString();
            //string strMachine = Session["machineItem"].ToString();
            //string strTagId = Session["strTagid"].ToString();

            string siteName = ConfigurationManager.AppSettings["enlaceRet"].ToString();

            rutaServ = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + itemId + "&code=Code128&dpi=96";
            imgItem.Src = !string.IsNullOrEmpty(itemId) ? rutaServ : "";

            rutaServ = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + strOrden + "&code=Code128&dpi=96";
            imgOrden.Src = !string.IsNullOrEmpty(strOrden) ? rutaServ : "";



            //lblWorkOrderLot.Text = strOrden;
            lblConsec.Text = strSecuen;
            lblUnit.Text = cantidad;
            lblDate.Text = fecha;
            lblFecha.Text = fecha;
            lblTime.Text = time;
            lblArticulo.Text = descItem;
            //lblMachine.Text = strMachine;


            ScriptManager.RegisterStartupScript(this, this.GetType(), "printDiv", "javascript:printDiv('Label');", true);
            Session.Remove("Consec");
            Session.Remove("FilaImprimir");
        }
    }

}