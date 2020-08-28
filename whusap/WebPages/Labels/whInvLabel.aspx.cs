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
    public partial class whInvLabel : System.Web.UI.Page
    {
        public string UrlBaseBarcode = WebConfigurationManager.AppSettings["UrlBaseBarcode"].ToString();

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
            string fecha = Convert.ToDateTime(fila.ItemArray[5]).ToString("MM/dd/yyyy HH:mm");
            string descItem = Session["descItem"].ToString();
            string strMachine = Session["unidad"].ToString();
            string strRollWinder =Session["rollwinder"].ToString().ToUpper();
            
            string siteName = ConfigurationManager.AppSettings["enlaceRet"].ToString();

            rutaServ = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + itemId + "&code=Code128&dpi=96";
            imgItem.Src = !string.IsNullOrEmpty(itemId) ? rutaServ : "";

            rutaServ = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + strSecuen + "&code=Code128&dpi=96";
            imgConsec.Src = !string.IsNullOrEmpty(strSecuen) ? rutaServ : "";

            lblWorkOrderLot.Text = strOrden;
            lblWeight.Text = cantidad ;
            lblRollNumber.Text = strSecuen.Remove(0, strSecuen.Trim().LastIndexOf("-") + 1);
            lblDate.Text = fecha;
            lblUsuario.Text = usuario;
            lblArticulo.Text = descItem;
            lblMachine.Text = strMachine;
            lblRollWinder.Text = strRollWinder;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "printDiv", "javascript:printDiv('Label');", true);
            Session.Remove("descItem");
            Session.Remove("FilaImprimir");
            Session.Remove("descItem");
            Session.Remove("unidad");

        }
    }
}