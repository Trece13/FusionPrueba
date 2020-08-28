using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Web.Configuration;

namespace whusap.WebPages.Labels
{
    public partial class whInvLabelFinishProduct : System.Web.UI.Page
    {
        public string UrlBaseBarcode = WebConfigurationManager.AppSettings["UrlBaseBarcode"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            string rutaServ = string.Empty;
            string strError = string.Empty;

            string ITEM = HttpContext.Current.Session["ITEM"].ToString();
            string CWAR = HttpContext.Current.Session["CWAR"].ToString();
            string CLOT = HttpContext.Current.Session["CLOT"].ToString();
            string LOCA = HttpContext.Current.Session["LOCA"].ToString();
            string QTYS = HttpContext.Current.Session["QTYS"].ToString();
            string UNIT = HttpContext.Current.Session["UNIT"].ToString();

            string strOrden = HttpContext.Current.Session["strOrden"].ToString();
            string strSecuen = HttpContext.Current.Session["strSecuen"].ToString();
            string itemId = HttpContext.Current.Session["itemId"].ToString();
            string cantidad = HttpContext.Current.Session["cantidad"].ToString();
            string bodega = HttpContext.Current.Session["bodega"].ToString();
            string usuario = HttpContext.Current.Session["usuario"].ToString();
            string fecha = HttpContext.Current.Session["fecha"].ToString();
            string descItem = HttpContext.Current.Session["descItem"].ToString();
            string unidad = HttpContext.Current.Session["unidad"].ToString();
            string strMachine = "";
            string strTagId = HttpContext.Current.Session["strTagId"].ToString();

            string siteName = ConfigurationManager.AppSettings["enlaceRet"].ToString();

            rutaServ = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + itemId + "&code=Code128&dpi=96";
            imgItem.Src = !string.IsNullOrEmpty(itemId) ? rutaServ : "";

            rutaServ = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + strSecuen + "&code=Code128&dpi=96";
            //imgConsec.Src = !string.IsNullOrEmpty(strSecuen) ? rutaServ : "";
            imgConsec.Src = !string.IsNullOrEmpty(strSecuen) ? rutaServ : "";

            rutaServ = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + cantidad + "&code=Code128&dpi=96";
            imgQuantity.Src = !string.IsNullOrEmpty(cantidad) ? rutaServ : "";

            lblWorkOrderLot.Text = strOrden;
            lblCant.Text = cantidad;
            lblUnit.Text = unidad;
            lblDate.Text = fecha;
            lblUsuario.Text = usuario;
            lblArticulo.Text = descItem;
            lblMachine.Text = strMachine;


            ScriptManager.RegisterStartupScript(this, this.GetType(), "printDiv", "javascript:printDiv('Label');", true);


        }
    }
}