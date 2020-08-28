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
    public partial class whInvLabelUI : System.Web.UI.Page
    {
        protected static string _idioma;
        public string UrlBaseBarcode = WebConfigurationManager.AppSettings["UrlBaseBarcode"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ddlIdioma"] == null)
            {
                Session["ddlIdioma"] = "INGLES";
            }

            _idioma = Session["ddlIdioma"].ToString();

            if (_idioma == "ESPAÑOL")
            {
                IdiomaEspañol();
            }
            else
            {
                IdiomaIngles();
            }

            string rutaServ = string.Empty;
            DataRow fila = (DataRow)Session["resultado"];
            if (fila == null) 
            {
                return;
            }

            string strUniqueId = fila.ItemArray[0].ToString().Trim();
            string strSourceWareHouse = fila.ItemArray[1].ToString().Trim();
            string strDestinationWareHouse = fila.ItemArray[2].ToString().Trim();
            string strTruckID = fila.ItemArray[5].ToString().Trim();
            string dateUniqueId = strUniqueId.Substring(strUniqueId.Length - 12, 12);

            var month = dateUniqueId.Substring(0, 2);
            var day = dateUniqueId.Substring(2, 2);
            var year = dateUniqueId.Substring(4, 4);
            var hour = dateUniqueId.Substring(8, 2);
            var minute = dateUniqueId.Substring(10, 2);

            dateUniqueId = String.Concat(month,"/",day,"/",year," ",hour,":",minute);

            lblSourceWarehouse.Text = strSourceWareHouse;
            lblDestinationWarehouse.Text = strDestinationWareHouse;
            lblTruckId.Text = strTruckID;
            lblHora.Text = DateTime.Now.ToString();
            lblDateUID.Text = dateUniqueId;

            var tipoFormulario = Session["tipoFormulario"].ToString();

            if (tipoFormulario != null)
            {
                tipoFormulario = tipoFormulario.ToUpper();

                switch (tipoFormulario)
                {
                    case "PRINT":
                        lblReprint.Text = _idioma == "INGLES" ? "PRINTED ON" : "IMPRESO EL";
                        break;
                    case "REPRINT":
                        lblReprint.Text = _idioma == "INGLES" ? "REPRINTED ON" : "RE-IMPRESO EL";
                        break;
                    default:
                        lblReprint.Text = _idioma == "INGLES" ? "PRINTED ON" : "IMPRESO EL";
                        break;
                }
            }
            else 
            {
                lblReprint.Visible = false;
            }

            string siteName = ConfigurationManager.AppSettings["enlaceRet"].ToString();

            rutaServ = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + strUniqueId + "&code=Code128&dpi=96";
            imgUniqueID.Src = !string.IsNullOrEmpty(strUniqueId) ? rutaServ : "";            

            ScriptManager.RegisterStartupScript(this, this.GetType(), "printDiv", "javascript:printDiv('Label');", true);
            Session.Remove("IsPreviousPage");
        }

        protected void IdiomaIngles()
        {
            Session["ddlIdioma"] = "INGLES";
            _idioma = "INGLES";
            lblEncabezado.Text = "UNIQUE IDENTIFIER";
            lblTruck.Text = "Truck ID";
            lblSource.Text = "Source Warehouse";
            lblDestination.Text = "Destination Warehouse";
            lblDateID.Text = "Date of unique ID";
        }

        protected void IdiomaEspañol()
        {
            Session["ddlIdioma"] = "ESPAÑOL";
            _idioma = "ESPAÑOL";
            lblEncabezado.Text = "IDENTIFICADOR UNICO";
            lblTruck.Text = "ID Camión";
            lblSource.Text = "Bodega Origen";
            lblDestination.Text = "Bodega Destino";
            lblDateID.Text = "Fecha de ID Unico";
        }
    }
}