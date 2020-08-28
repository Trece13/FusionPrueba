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
    public partial class whInvPrintLabel : System.Web.UI.Page
    {
        string strError = string.Empty;
        string Aplicacion = "WEBAPP";

        protected void Page_Load(object sender, EventArgs e)
        {
            string rutaServ = string.Empty;
            DataRow fila = (DataRow)Session["FilaImprimir"];
            if (fila == null)
                return;


            // string id = 
            LblWorkOrder.Text = fila.ItemArray[0].ToString().Trim();
            lblLotCode.Text = fila.ItemArray[4].ToString().Trim();
            lblItem.Text = fila.ItemArray[2].ToString().Trim();
            lblArticulo.Text = fila.ItemArray[14].ToString().Trim();
            lblQuantity.Text = fila.ItemArray[5].ToString().Trim();
            lblUnit.Text = fila.ItemArray[13].ToString().Trim();
            lblUser.Text = fila.ItemArray[6].ToString().Trim();
            lblDate.Text = fila.ItemArray[7].ToString().Trim();
            string siteName = ConfigurationManager.AppSettings["enlaceRet"].ToString();
            
            rutaServ = "http://" + Request.Url.Authority + "/Barcode/BarcodeHandler.ashx?data=" + LblWorkOrder.Text + "&code=Code128&dpi=96";
            imgWorkOrder.Src = !string.IsNullOrEmpty(LblWorkOrder.Text) ? rutaServ : "";
            
            rutaServ = "http://" + Request.Url.Authority + "/Barcode/BarcodeHandler.ashx?data=" + lblLotCode.Text + "&code=Code128&dpi=96";
            imgLotCode.Src = !string.IsNullOrEmpty(lblLotCode.Text) ? rutaServ : "";
            
            rutaServ = "http://" + Request.Url.Authority + "/Barcode/BarcodeHandler.ashx?data=" + lblItem.Text + "&code=Code128&dpi=96";
            imgItem.Src = !string.IsNullOrEmpty(lblItem.Text) ? rutaServ : "";

            Type csType = this.GetType();
            ClientScriptManager scriptBlock = Page.ClientScript;

            string cstext1 = "printDiv('Label');";
            scriptBlock.RegisterStartupScript(csType, "printerLabel", cstext1, true);

            if (Session["update"] != null)
                return;

            List<Ent_tticol125> parameterCollection = new List<Ent_tticol125>();
            Ent_tticol125 obj = new Ent_tticol125();

            //Actualizar estado de impresion
            obj = new Ent_tticol125();
            obj.pdno = fila.ItemArray[0].ToString().Trim();
            obj.pono = Convert.ToInt32(fila.ItemArray[1].ToString().Trim());
            obj.item = fila.ItemArray[2].ToString();
            obj.cwar = fila.ItemArray[3].ToString().Trim();
            obj.clot = string.IsNullOrEmpty(fila.ItemArray[4].ToString().Trim()) ? " " : fila.ItemArray[4].ToString().Trim();
            obj.reqt = Convert.ToDecimal(fila.ItemArray[5].ToString().Trim());
            obj.refcntd = "0";
            obj.refcntu = "0";
            obj.mess = " ";
            obj.conf = Convert.ToInt32(fila.ItemArray[9].ToString().Trim()) ;
            obj.prin = 1;           
            obj.idrecord = fila.ItemArray[15].ToString().Trim();
            parameterCollection.Add(obj);
            InterfazDAL_tticol125 idal = new InterfazDAL_tticol125();
            if (fila.ItemArray.Count() > 16)
            {
                if (fila.ItemArray[16].ToString().Trim() == "126")
                {
                    idal.actualizarRegistro_Param(ref parameterCollection, ref strError, Aplicacion, true);
                    return;
                }
            }
            idal.actualizarRegistro_Param(ref parameterCollection, ref strError, Aplicacion);
            Session.Remove("IsPreviousPage");

            ScriptManager.RegisterStartupScript(this, this.GetType(), "printDiv", "javascript:printDiv('Label');", true);
        }
    }
}