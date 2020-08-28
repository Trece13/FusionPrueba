using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using System.Web.UI.HtmlControls;
using whusa;
using System.Data;
using Newtonsoft.Json;
using System.Text;
using whusa.Entidades;
using whusa.Interfases;
using System.Threading;
using System.Globalization;
using whusa.Utilidades;
using System.Web.Configuration;

namespace whusap.WebPages.InvMaterial
{
    public partial class whInvMaterialPoOrderReturn : System.Web.UI.Page
    {
        #region Propiedades

        Type csType;
        string strError = string.Empty;
        string Aplicacion = "WEBAPP";

        //Manejo idioma
        public static string PalletWarehousedoesnotmatch = mensajesStatic("PalletWarehousedoesnotmatch");
        public static string PalletItemcodedoesnotmatch = mensajesStatic("PalletItemcodedoesnotmatch");
        public static string ActualquantitypalletIDequalorlessthanorderedquantity = mensajesStatic("ActualquantitypalletIDequalorlessthanorderedquantity");
        public static string Priorinboundnotfound = mensajesStatic("Priorinboundnotfound");

        private static Mensajes _mensajesForm = new Mensajes();
        private static LabelsText _textoLabels = new LabelsText();
        private static string formName;
        private static string globalMessages = "GlobalMessages";
        public static string _idioma;
        public static string _operator = string.Empty;
        public static IntefazDAL_tticol082 Itticol082 = new IntefazDAL_tticol082();
        public static InterfazDAL_twhcol130 twhcol130DAL = new InterfazDAL_twhcol130();
        private static InterfazDAL_ttccol301 _idalttccol301 = new InterfazDAL_ttccol301();
        public static InterfazDAL_twhwmd200 twhwmd200 = new InterfazDAL_twhwmd200();

        public static string UrlBaseBarcode = WebConfigurationManager.AppSettings["UrlBaseBarcode"].ToString();

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            txtReturnOrder.Focus();
            Page.Form.DefaultButton = btnSend.UniqueID;

            var ctrlName = Request.Params[Page.postEventSourceID];
            var args = Request.Params[Page.postEventArgumentID];

            //HandleCustomPostbackEvent(ctrlName, args);
            //lblOrder.Visible = false;

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            base.InitializeCulture();

            if (!IsPostBack)
            {
                formName = Request.Url.AbsoluteUri.Split('/').Last();
                if (formName.Contains('?'))
                {
                    formName = formName.Split('?')[0];
                }

                if (Session["ddlIdioma"] != null)
                {
                    _idioma = Session["ddlIdioma"].ToString();
                }
                else
                {
                    _idioma = "INGLES";
                }

                CargarIdioma();

                _operator = Session["user"].ToString();
                String strTitulo = mensajes("encabezado");

                Label control = (Label)Page.Controls[0].FindControl("lblPageTitle");
                if (control != null) { control.Text = strTitulo; }
            }

            //csType = this.GetType();
            //scriptBlock = Page.ClientScript;

            //StringBuilder script = new StringBuilder();
            //// Crear el script para la ejecucion de la forma
            //script.Append("<script type=\"text/javascript\">function button_click(objTextBox,objBtnID) {");
            //script.Append("if(window.event.keyCode==13)");
            //script.Append("{");
            //script.Append("document.getElementById(objBtnID).focus();");
            //script.Append("document.getElementById(objBtnID).click();");
            //script.Append("}}");
            //script.Append("</script>");     

            //scriptBlock.RegisterClientScriptBlock(csType, "button_click", script.ToString(), false);
            //  this.txtReturnOrder.Attributes.Add("onkeypress", "button_click(this," + this.btnSend.ClientID + ")");
        }


        [System.Web.Services.WebMethod()]
        public static string vallidatePalletID(string palletID, string position, string returnOrder)
        {

            InterfazDAL_tticol125 idal = new InterfazDAL_tticol125();
            Ent_tticol125 obj = new Ent_tticol125();
            string strError = string.Empty;
            string retorno = string.Empty;
            //TextBox quantity = (TextBox)e.Row.Cells[10].FindControl("palletId");




            obj.paid = palletID.Trim().ToUpperInvariant();

            DataTable resultado = idal.vallidatePalletData(ref obj, ref strError);
            //decimal palletQuantity = 0;
            //decimal status;
            //string tableName = string.Empty;
            //decimal givenQuantity = Convert.ToDecimal(quantity);
            /* select  col131.t$paid "palletid", col131.t$loaa "current_location",
          col131.t$qtya "current_qty", col131.t$item "item", ibd001.t$dsca "description"*/
            string item, description, palletid, current_location, unit, war, stat;
            decimal current_qty;
            if (resultado.Rows.Count < 1)
            {
                return null;

            }
            else
            {
                foreach (DataRow dr in resultado.Rows)
                {
                    palletid = dr.ItemArray[0].ToString();
                    current_location = dr.ItemArray[1].ToString();
                    current_qty = Convert.ToDecimal(dr.ItemArray[2].ToString());
                    item = dr.ItemArray[3].ToString();
                    description = dr.ItemArray[4].ToString();
                    unit = dr.ItemArray[5].ToString();
                    war = dr.ItemArray[6].ToString();
                    stat = dr.ItemArray[7].ToString();

                    // lblItemValue.Text = item;
                    //lblItemDescValue.Text = description;
                    //lblUnitValue.Text = unit;
                    //lbllotValue.Text = lot;


                    string warehouse, itemcode;
                    decimal qty;
                    InterfazDAL_twhcol130 idal2 = new InterfazDAL_twhcol130();
                    DataTable resultado1 = idal2.vallidatePurchaseOrderWithPosition(ref returnOrder, ref position, ref strError);
                    if (resultado1.Rows.Count < 1)
                    {
                        return null;

                    }
                    else
                    {

                        foreach (DataRow dr1 in resultado1.Rows)
                        {
                            itemcode = dr1.ItemArray[0].ToString();
                            warehouse = dr1.ItemArray[1].ToString();
                            qty = Convert.ToDecimal(dr1.ItemArray[2].ToString());
                            if (warehouse.Trim() != war.Trim())
                            {
                                return PalletWarehousedoesnotmatch;
                            }
                            if (itemcode.Trim() != item.Trim())
                            {
                                return PalletItemcodedoesnotmatch;
                            }
                            if (current_qty < qty)
                            {
                                return ActualquantitypalletIDequalorlessthanorderedquantity
;
                            }
                        }
                    }

                }

                return JsonConvert.SerializeObject(resultado);



            }


        }


        protected void btnSend_Click(object sender, EventArgs e)
        {
            // btnEnviar.Visible = false;
            if (string.IsNullOrEmpty(txtReturnOrder.Text.Trim()))
            {
                //RequiredField.Enabled = true;
                //RequiredField.IsValid = false;
                txtReturnOrder.Focus();
                // grdRecords.DataSource = "";
                //  grdRecords.DataBind();
                return;
            }

            tblReturnInfo.Visible = false;
            grdRecords.Visible = false;
            InterfazDAL_twhcol130 idal = new InterfazDAL_twhcol130();

            string strError = string.Empty;
            string retorno = string.Empty;

            string returnorder = string.Empty;

            returnorder = txtReturnOrder.Text.Trim().ToString().ToUpperInvariant();


            DataTable resultado = idal.vallidatePurchaseOrder(ref returnorder, ref strError);


            string tableName = string.Empty;
            string retorder, position, secuence, item, description, supplier, quantity, lot, dsca, STUN, waredesc;
            // decimal order_qty, pending_qty;

            ListItem itemS = null;
            itemS = new ListItem();
            int rowIndex = 0;
            itemS.Text = _idioma == "INGLES" ? "-- Select an Position -- " : " -- Seleccione --";
            itemS.Value = "";
            dropDownPosition.Items.Clear();

            dropDownPosition.Items.Insert(rowIndex, itemS);

            lblError.Text = "";
            if (resultado.Rows.Count < 1)
            { //Purchase Order Return doesn´t exist
                lblError.Text = mensajes("PONotExists");
                return;
            }
            else
            {

                tblReturnInfo.Visible = true;

                /*
                   select pur401.t$orno "ret_order", pur401.t$pono "position", pur401.t$sqnb "secuence",
            pur401.t$oqua "order_qty", pur401.t$bqua "pending_qty", pur401.t$item "item",
            ibd001.T$dsca "description", pur401.t$ddtb "date_pl_receive", inh210.t$oset "set",
            ipu001.t$rtdp "datetol_max", ipu001.t$rtdm "datetol_min", ipu001.t$rtqp "qtytol_max",
            ipu001.t$rtqm "qtytol_min", pur401.t$oltp "type", pur401.t$cwar "warehouse",
            inh210.t$seqn "secuence", inh210.t$clot "lot", ibd001.t$cuni "unit",
            pur400.t$cotp "currency", pur401.t$otbp "supplier", com100.t$nama "name"
                   */
                DataRow dr = resultado.Rows[0];
                retorder = dr.ItemArray[0].ToString();

                position = dr.ItemArray[1].ToString();
                secuence = dr.ItemArray[2].ToString();
                //  order_qty = dr.ItemArray[3].ToString();
                //  pending_qty = dr.ItemArray[4].ToString();
                item = dr.ItemArray[5].ToString();
                description = dr.ItemArray[6].ToString();
                supplier = dr.ItemArray[19].ToString();
                lot = dr.ItemArray[16].ToString();
                STUN = dr.ItemArray[17].ToString();
                //lblItemValue.Text = item;
                //lblItemDescValue.Text = description;
                lblSupplierValue.Text = supplier;
                lblreturnorderValue.Text = retorder;

                lblstun.Value = STUN;
                //lblUnitValue.Text = unit;
                lbllotValue.Text = lot;

                grdRecords.DataSource = resultado;
                grdRecords.DataBind();
                grdRecords.Visible = true;
                itemS = new ListItem();
                // rowIndex = (int)resultado.Rows.IndexOf(dr);
                //itemS.DataSource = resultado;
                //itemS.Value = position;
                //itemS.Text = position;
                //dropDownPosition.Items.Insert(rowIndex + 1, itemS);




                DropDownList listregrind = (DropDownList)dropDownPosition;
                listregrind.DataSource = resultado;
                listregrind.DataTextField = "position";
                listregrind.DataValueField = "position";
                listregrind.DataBind();
                listregrind.Items.Insert(0, new ListItem(_idioma == "INGLES" ? "Select Item here..." : "Seleccione un articulo...", string.Empty));



                //btnSave.Visible = true;



            }

        }


        [System.Web.Services.WebMethod()]
        public static string InsertarReseiptRawMaterial(string OORG, string ORNO, string ITEM, string PONO, string LOT, decimal QUANTITY, string STUN, string PALLET, string UNIT)
        {
            //if (string.IsNullOrEmpty(ORNO.Trim()))
            //{
            //    //RequiredField.Enabled = true;
            //    //RequiredField.IsValid = false;
            //    txtPalletIDValue.Focus();
            //    lblError.Text = mensajes("Palletnotpresent");
            //    // grdRecords.DataSource = "";
            //    //  grdRecords.DataBind();
            //    return;
            //}


            //var ORNO = lblreturnorderValue.Text.Trim().ToUpper();
            //var PONO = dropDownPosition.Text;
            //var QUANTITYAUX = Convert.ToInt32(lblqtyValue1.Value.ToString());

            //var ITEM = lblItemValue1.Value.Trim().ToString().ToUpper();
            decimal QUANTITYAUX = QUANTITY;
            LOT = LOT.Trim() == string.Empty ? " " : LOT.Trim();

            string Retrono = "El Registro no se ha insertado";
            DataTable DTOrdencompra = ConsultaOrdencompra(ORNO, PONO, QUANTITYAUX, ITEM, "");
            if (DTOrdencompra.Rows.Count > 0) //Changes
            {
                bool OrdenImportacion = false;
                OrdenImportacion = twhcol130DAL.ConsultaOrdenImportacion(DTOrdencompra.Rows[0]["T$COTP"].ToString()).Rows.Count > 0 ? true : false;

                string LOCAL = string.Empty;
                string PRIORIDAD = string.Empty;
                Ent_twhcol130131 MyObjError = new Ent_twhcol130131();
                try
                {
                    string strError = string.Empty;
                    Ent_twhwmd200 OBJ200 = new Ent_twhwmd200 { cwar = DTOrdencompra.Rows[0]["T$CWAR"].ToString().Trim() };
                    DataTable Dttwhwmd200 = twhwmd200.listaRegistro_ObtieneAlmacenLocation(ref OBJ200, ref strError);
                    if (Dttwhwmd200.Rows.Count > 0)
                    {
                        if (Dttwhwmd200.Rows[0]["LOC"].ToString().Trim() == "1")
                        {
                            PRIORIDAD = twhcol130DAL.ConsultarPrioridadNativa(DTOrdencompra.Rows[0]["T$CWAR"].ToString().Trim()).Rows[0]["T$PRIO"].ToString().Trim();
                            LOCAL = twhcol130DAL.ConsultarLocationNativa(DTOrdencompra.Rows[0]["T$CWAR"].ToString().Trim(), PRIORIDAD).Rows[0]["T$LOCA"].ToString().Trim();
                        }
                    }

                }
                catch (Exception ex)
                {
                    //LOCAL = " ";
                    MyObjError.error = true;
                    MyObjError.errorMsg = Priorinboundnotfound;
                    //return MyObjError;
                }
                // var OORG = 2;
                var FIRE = 2;

                Ent_twhcol130131 MyObj = new Ent_twhcol130131
                {
                    OORG = OORG.ToString(),// Order type escaneada view 
                    ORNO = DTOrdencompra.Rows[0]["T$ORNO"].ToString(),
                    ITEM = DTOrdencompra.Rows[0]["T$ITEM"].ToString().Trim(),
                    PAID = PALLET,
                    PONO = DTOrdencompra.Rows[0]["T$PONO"].ToString(),
                    SEQN = DTOrdencompra.Rows[0]["T$SQNBR"].ToString(),
                    CLOT = LOT,// lote VIEW
                    CWAR = DTOrdencompra.Rows[0]["T$CWAR"].ToString(),
                    QTYS = QUANTITYAUX.ToString("0.00"),// cantidad escaneada view 
                    UNIT = STUN,//unit escaneada view
                    QTYC = QUANTITYAUX.ToString("0.00"),//cantidad escaneada view aplicando factor
                    UNIC = UNIT,//unidad view stock
                    DATE = DateTime.Now.ToString("dd/MM/yyyy").ToString(),//fecha de confirmacion 
                    CONF = "1",
                    RCNO = " ",//llena baan
                    DATR = "01/01/70",//llena baan
                    LOCA = LOCAL,// enviamos vacio
                    DATL = DateTime.Now.ToString("dd/MM/yyyy").ToString(),//llenar con fecha de hoy
                    PRNT = "1",// llenar en 1
                    DATP = DateTime.Now.ToString("dd/MM/yyyy").ToString(),//llena baan
                    NPRT = "1",//conteo de reimpresiones 
                    LOGN = _operator,// nombre de ususario de la session
                    LOGT = " ",//llena baan
                    STAT = "0",// LLENAR EN 1  
                    DSCA = DTOrdencompra.Rows[0]["DSCA"].ToString(),
                    COTP = DTOrdencompra.Rows[0]["T$COTP"].ToString(),
                    FIRE = FIRE.ToString(),
                    PSLIP = "",
                    ALLO = "0",
                    PAID_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + PALLET.ToString() + "&code=Code128&dpi=96",
                    ORNO_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + DTOrdencompra.Rows[0]["T$ORNO"].ToString() + "&code=Code128&dpi=96",
                    ITEM_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + DTOrdencompra.Rows[0]["T$ITEM"].ToString().Trim().ToUpper() + "&code=Code128&dpi=96",
                    CLOT_URL = LOT.ToString().Trim() != "" ? UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + LOT.ToString().Trim().ToUpper() + "&code=Code128&dpi=96" : "",
                    QTYC_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + QUANTITYAUX.ToString("0.00").Trim().ToUpper() + "&code=Code128&dpi=96",
                    UNIC_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + UNIT.ToString().Trim().ToUpper() + "&code=Code128&dpi=96"
                };

                //if (OrdenImportacion)
                //{
                var i = 1;
                int PRIO = 1;
                Ent_tticol082 Objtticol082 = new Ent_tticol082
                {
                    OORG = OORG.ToString(),
                    ORNO = MyObj.ORNO,
                    OSET = DTOrdencompra.Rows[0]["T$OSET"].ToString(),
                    PONO = MyObj.PONO,
                    SQNB = MyObj.SEQN,
                    ADVS = i.ToString(),
                    ITEM = "         " + MyObj.ITEM,
                    STAT = "2",
                    QTYT = QUANTITYAUX.ToString("0.00"),
                    CWAR = DTOrdencompra.Rows[0]["T$CWAR"].ToString(),
                    UNIT = MyObj.UNIT,
                    PRIO = PRIO.ToString(),
                    PAID = PALLET,
                    LOGN = _operator,
                };
                bool InsertSuccess = Itticol082.InsertarregistroItticol082(Objtticol082);
                if (InsertSuccess)
                {
                    //Update pallet ID status to “Picked” (whcol131.stat).

                    bool updatesuccess = twhcol130DAL.UpdateStatusPicked(MyObj);


                    Retrono = JsonConvert.SerializeObject(MyObj);
                }
                else
                {
                    MyObj.error = true;
                    MyObj.errorMsg = "la insercion fue: " + InsertSuccess.ToString();
                    Retrono = JsonConvert.SerializeObject(MyObj);
                }


                // }

            }
            return Retrono;

        }

        public static DataTable ConsultaOrdencompra(string ORNO, string PONO, decimal CANT, string ITEM, string CLOT)
        {
            DataTable consulta = new DataTable();
            consulta = twhcol130DAL.ConsultaOrdencompra(ORNO, PONO, CANT, ITEM, CLOT);
            return consulta;
        }
        public void validaBackend(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            String str = txt.Text;
        }

        //protected void btnExit_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("whInvMaterialPoOrderReturn.aspx");
        //}


        #endregion

        #region Metodos






        protected void CargarIdioma()
        {
            lblreturnorder.Text = _textoLabels.readStatement(formName, _idioma, "lblreturnorder");
            btnSend.Text = _textoLabels.readStatement(formName, _idioma, "btnSend");
            // btnSalir.Text = _textoLabels.readStatement(formName, _idioma, "btnSalir");
            ////printLabel.Text = _textoLabels.readStatement(formName, _idioma, "printLabel");
            ////btnSave.Text = _textoLabels.readStatement(formName, _idioma, "btnSave");
            ////minlenght.ErrorMessage = _textoLabels.readStatement(formName, _idioma, "regularWorkOrder");
            ////RequiredField.ErrorMessage = _textoLabels.readStatement(formName, _idioma, "requiredWorkOrder");
            ////OrderError.ErrorMessage = _textoLabels.readStatement(formName, _idioma, "customWorkOrder");
            //////validateReturn.ErrorMessage = _textoLabels.readStatement(formName, _idioma, "btnSave");
            //////validateQuantity.ErrorMessage = _textoLabels.readStatement(formName, _idioma, "btnSave");
            ////grdRecords.Columns[0].HeaderText = _textoLabels.readStatement(formName, _idioma, "headPosition");
            ////grdRecords.Columns[1].HeaderText = _textoLabels.readStatement(formName, _idioma, "headItem");
            ////grdRecords.Columns[2].HeaderText = _textoLabels.readStatement(formName, _idioma, "headDescription");
            ////grdRecords.Columns[3].HeaderText = _textoLabels.readStatement(formName, _idioma, "headWarehouse");
            ////grdRecords.Columns[4].HeaderText = _textoLabels.readStatement(formName, _idioma, "headActualQty");
            ////grdRecords.Columns[5].HeaderText = _textoLabels.readStatement(formName, _idioma, "headUnit");
            ////grdRecords.Columns[6].HeaderText = _textoLabels.readStatement(formName, _idioma, "headToReturn");
            ////grdRecords.Columns[7].HeaderText = _textoLabels.readStatement(formName, _idioma, "headLot");
            ////grdRecords.Columns[8].HeaderText = _textoLabels.readStatement(formName, _idioma, "headLot");
        }




        protected string mensajes(string tipoMensaje)
        {
            var retorno = _mensajesForm.readStatement(formName, _idioma, ref tipoMensaje);

            if (retorno.Trim() == String.Empty)
            {
                retorno = _mensajesForm.readStatement(globalMessages, _idioma, ref tipoMensaje);
            }

            return retorno;
        }

        protected static string mensajesStatic(string tipoMensaje)
        {
            Mensajes mensajesForm = new Mensajes();
            string idioma = "INGLES";
            var retorno = mensajesForm.readStatement("whInvMaterialPoOrderReturn.aspx", idioma, ref tipoMensaje);

            if (retorno.Trim() == String.Empty)
            {
                retorno = mensajesForm.readStatement(globalMessages, idioma, ref tipoMensaje);
            }

            return retorno;
        }
        #endregion
    }
}//
//