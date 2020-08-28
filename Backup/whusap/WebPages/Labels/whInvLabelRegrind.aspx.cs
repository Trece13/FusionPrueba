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
    public partial class whInvLabelRegrind : System.Web.UI.Page
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
            string cantidad = fila.ItemArray[3].ToString().Trim().Replace('-', ' ');
            string bodega = fila.ItemArray[4].ToString().Trim();
            string usuario = fila.ItemArray[5].ToString().Trim();
            string fecha = fila.ItemArray[6].ToString().Trim();
            string descItem = Session["descItem"].ToString();
            string unidad = Session["unidad"].ToString();
            string strMachine = Session["machineItem"].ToString();
            string strTagId = Session["strTagid"].ToString();

            string Reprinted = (string)(Session["reprinted"]);
            //if (fila.ItemArray.Count() >= 15)
            //{
            //string DateReprinted = fila.ItemArray[16].ToString().Trim();
            //}


            string siteName = ConfigurationManager.AppSettings["enlaceRet"].ToString();

            rutaServ = "http://" + Request.Url.Authority + "/Barcode/BarcodeHandler.ashx?data=" + itemId + "&code=Code128&dpi=96";
            imgItem.Src = !string.IsNullOrEmpty(itemId) ? rutaServ : "";

            rutaServ = "http://" + Request.Url.Authority + "/Barcode/BarcodeHandler.ashx?data=" + strTagId + "&code=Code128&dpi=96";
            //imgConsec.Src = !string.IsNullOrEmpty(strSecuen) ? rutaServ : "";
            imgConsec.Src = !string.IsNullOrEmpty(strTagId) ? rutaServ : "";

            rutaServ = "http://" + Request.Url.Authority + "/Barcode/BarcodeHandler.ashx?data=" + cantidad + "&code=Code128&dpi=96";
            imgQuantity.Src = !string.IsNullOrEmpty(cantidad) ? rutaServ : "";


            if (Session["reprinted"] == null)
            {
                lblTitleReprinted.Visible = false;
                lblReprinted.Visible = false;
                lblTitleReprinted.Text = "";
            }
            else
            {
                string DateReprinted = fila.ItemArray[16].ToString().Trim();
                lblTitleReprinted.Visible = true;
                lblReprinted.Visible = true;
                lblReprinted.Text = DateReprinted;
            }


            lblWorkOrderLot.Text = strOrden;
            lblCant.Text = cantidad;
            lblUnit.Text = unidad;
            lblDate.Text = fecha;
            lblUsuario.Text = usuario;
            lblArticulo.Text = descItem;
            lblMachine.Text = strMachine;


            ScriptManager.RegisterStartupScript(this, this.GetType(), "printDiv", "javascript:printDiv('Label');", true);
            Session.Remove("descItem");
            Session.Remove("FilaImprimir");
            Session.Remove("machineItem");
            Session.Remove("unidad");
            Session.Remove("strTagid");
            Session.Remove("reprinted");


        }
    }
}