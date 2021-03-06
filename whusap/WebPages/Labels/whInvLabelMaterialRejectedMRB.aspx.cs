﻿using System;
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
    public partial class whInvLabelMaterialRejectedMRB : System.Web.UI.Page
    {
        string strError = string.Empty;
        // string Aplicacion = "WEBAPP";
        public string UrlBaseBarcode = WebConfigurationManager.AppSettings["UrlBaseBarcode"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {

            DataTable DTMyRegrind = (DataTable)Session["DTregrind"];
            string MyRegrind = Session["MyRegrind"].ToString();

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

            //var cnt = Session["cnt"];
            var cnt = Session["TagId"];
            //var regridseq = "-R" + cnt;
            var regridseq = cnt;

            if (fila == null)
                return;
            string strItem = fila.ItemArray[0].ToString().Trim();
            double strQuantity = ((Convert.ToDouble(fila.ItemArray[3]) * Convert.ToDouble(fila.ItemArray[7])) / 2.2046);
            strQuantity = Math.Round(strQuantity, 2);
            string strArticulo = fila.ItemArray[1].ToString().Trim();
            lblArticulo.Text = string.IsNullOrEmpty(MyRegrindDesc) ? strArticulo : MyRegrindDesc;
            lblOrder.Text = fila.ItemArray[9].ToString().Trim().Substring(0,9);
            lblUnit.Text = fila.ItemArray[2].ToString().Trim() == "CJ" ? "KG" : fila.ItemArray[2].ToString().Trim();
            LblComment.Text = fila.ItemArray[6].ToString().Trim();
            lblQuantity.Text = strQuantity.ToString().Trim();
            lblUser.Text = Session["user"].ToString().ToUpper();
            lblDate.Text = fila.ItemArray[8].ToString().Trim();

            string strLot=fila.ItemArray[4].ToString().Trim() ; //LotNO

            string siteName = ConfigurationManager.AppSettings["enlaceRet"].ToString();

            //Logic example “OO0003852-RJ001” “OO0003852-RJ002” – this 
            //strLot= strLot+ "RJ002";
            rutaServ = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + regridseq + "&code=Code128&dpi=96";
            imgLot.Src = !string.IsNullOrEmpty(strLot) ? rutaServ : "";


            rutaServ = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + strItem + "&code=Code128&dpi=96";
            imgItem.Src = !string.IsNullOrEmpty(strItem) ? rutaServ : "";

            rutaServ = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + strQuantity + "&code=Code128&dpi=96";
            imgQuantity.Src = !string.IsNullOrEmpty(strQuantity.ToString().Trim()) ? rutaServ : "";

            //Type csType = this.GetType();
            //ClientScriptManager scriptBlock = Page.ClientScript;

            //string cstext1 = "alert(Imprimir?); printDiv('Label');";
            //scriptBlock.RegisterStartupScript(csType, "printerLabel", cstext1, true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "printDiv", "javascript:printDiv('Label');", true);
            Session.Remove("IsPreviousPage");

        }
    }
}