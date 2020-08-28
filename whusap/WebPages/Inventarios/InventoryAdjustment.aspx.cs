using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Script.Serialization;
using System.Web.UI.HtmlControls;
using System.Text;
using whusa.Entidades;
using whusa.Interfases;
using whusa.Utilidades;

namespace whusap.WebPages.Inventarios
{
    public partial class InventoryAdjustment : System.Web.UI.Page
    {
        #region Propiedades
        string strError = string.Empty;
        string Aplicacion = "WEBAPP";
        private static string globalMessages = "GlobalMessages";
        //Manejo idioma
        public static string PalletIDdoesntexists = mensajesStatic("PalletIDdoesntexists");
        public static string PalletIDstatusdoesntallowadjustment = mensajesStatic("PalletIDstatusdoesntallowadjustment");
        public static string Adjustmentquantitycannotbezero = mensajesStatic("Adjustmentquantitycannotbezero");
        public static string AdjustmentquantityshouldbelessthanexistingQty = mensajesStatic("AdjustmentquantityshouldbelessthanexistingQty");

        private static Mensajes _mensajesForm = new Mensajes();
        private static LabelsText _textoLabels = new LabelsText();
        private static string formName;
        public static string _idioma;
        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            txtPalletId.Focus();
            Page.Form.DefaultButton = btnSend.UniqueID;

            var ctrlName = Request.Params[Page.postEventSourceID];
            var args = Request.Params[Page.postEventArgumentID];
            //Page.Form.Unload += new EventHandler(Form_Unload);

            //HandleCustomPostbackEvent(ctrlName, args);

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

                String strTitulo = mensajes("encabezado");


                Label control = (Label)Page.Controls[0].FindControl("lblPageTitle");
                if (control != null) { control.Text = strTitulo; }
            }
            generateDropDownReasonCodes();
            generateDropDownCostCenters();
            btnSave.Visible = false;
            tblPalletInfo.Visible = false;
        }

        [System.Web.Services.WebMethod()]
        public static string vallidatePalletID(string palletID)
        {
            //TextBox palletIdTextBox = (TextBox)e.Row.Cells[10].FindControl("palletId");
            //
            //quantityToReturn = "2";

            InterfazDAL_tticol125 idal = new InterfazDAL_tticol125();
            Ent_tticol125 obj = new Ent_tticol125();
            string strError = string.Empty;
            string retorno = string.Empty;
            //TextBox quantity = (TextBox)e.Row.Cells[10].FindControl("palletId");

            obj.paid = palletID.Trim().ToUpperInvariant();
            //obj.reqt = Convert.ToInt32(fila.GetValue(8).ToString().Trim());


            decimal palletQuantity = 0;
            decimal status;
            string tableName = string.Empty;

            DataTable resultado = idal.vallidatePalletID(ref obj, ref strError);

            if (resultado.Rows.Count < 1) { retorno = PalletIDdoesntexists; }
            else
            {
                foreach (DataRow dr in resultado.Rows)
                {
                    status = Convert.ToDecimal(dr.ItemArray[8].ToString());
                    palletQuantity = Convert.ToDecimal(dr.ItemArray[3].ToString());
                    tableName = dr.ItemArray[0].ToString();

                    if (((tableName == "whcol131") || (tableName == "whcol130")) && (status != 3))
                    {
                        retorno = PalletIDstatusdoesntallowadjustment;
                        break;
                    }
                    else if (((tableName == "ticol022") || (tableName == "ticol042")) && (status !=7))
                    {
                        retorno = PalletIDstatusdoesntallowadjustment;
                        break;
                    }
                }
            }
            return retorno;
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtPalletId.Text.Trim()))
            {
                //minlenght.Enabled = true;
                //minlenght.ErrorMessage = mensajes("Please Fill all the Required  Fields.");
                //minlenght.IsValid = false;

                //return;
            }
             lblError.Text = "";
             lblConfirm.Text = "";
            InterfazDAL_tticol125 idal = new InterfazDAL_tticol125();
            Ent_tticol125 obj = new Ent_tticol125();
            string strError = string.Empty;
            obj.paid = txtPalletId.Text.ToUpperInvariant();
            //lblResult.Text = string.Empty;
            DataTable resultado = idal.invGetPalletInfo(ref obj, ref strError);

            if (resultado == null)
            {
                return;
            }

          //  SELECT 'ticol022' AS TBL,ticol022.T$PDNO AS T$ORNO,ticol022.T$SQNB AS T$PAID,ticol222.T$ACQT AS T$qtyc,
          //ticol022.T$MITM AS T$ITEM, tcibd001.t$dsca AS DSCA, ticol022.T$CUNI AS T$CUNI,ticol222.T$CWAF AS T$CWAR,
          //tcmcs003.t$dsca as DESCW, ticol222.T$ACLO AS T$LOCA

          //  SELECT 'ticol022' AS TBL,ticol022.T$PDNO AS T$ORNO,ticol022.T$SQNB AS T$PAID,ticol222.T$ACQT AS T$qtyc,
          //ticol022.T$MITM AS T$ITEM, tcibd001.t$dsca AS DSCA, ticol022.T$CUNI AS T$CUNI,ticol222.T$CWAF AS T$CWAR,
          //tcmcs003.t$dsca as DESCW, ticol222.T$ACLO AS T$LOCA



            string palletId, item, warehouse, lot, location, quantity, dsca, unit, waredesc;

            if (resultado.Rows.Count == 1) 
            {
                DataRow dr = resultado.Rows[0];
                palletId = dr.ItemArray[2].ToString();
                quantity = dr.ItemArray[3].ToString();
                item = dr.ItemArray[4].ToString();
                dsca = dr.ItemArray[5].ToString();
                unit = dr.ItemArray[6].ToString();
                warehouse = dr.ItemArray[7].ToString();
                waredesc = dr.ItemArray[8].ToString();
                lot = dr.ItemArray[10].ToString();
                location = dr.ItemArray[9].ToString();

                lblPalletId1Value.Text = palletId;
                lblItemValue.Text = item;
                lblItemDescValue.Text = dsca;
                lblWarehouseValue.Text = warehouse;
                lblWarehouseDescValue.Text = waredesc;
                lblLotValue.Text = lot;
                lblLocationValue.Text = location;
                lblQuantityValue.Text = quantity;
                lblUnitValue.Text= unit;
                lblUnitValue1.Text = unit;
                tblPalletInfo.Visible = true;
                btnSend.Visible = false;
                btnSave.Visible = true;
            }
        }

        protected void OptionList_value(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            if (ddl.SelectedIndex == 0)
            {
                return;
            }
            else
            {
                return;
            }
        }

        protected void grdRecords_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string prin = ((DataRowView)e.Row.DataItem).DataView.Table.Rows[e.Row.RowIndex]["T$PRIN"].ToString();
                // ((Button)e.Row.Cells[7].FindControl("btnPrint")).OnClientClick = "printTag(" + FilaSerializada.Trim() + ")";
                ((Label)e.Row.Cells[8].FindControl("prin")).Text = prin;
            }
        }

        protected void Form_Unload(object sender, EventArgs e)
        {
            Session["FilaImprimir"] = null;
            Session["resultado"] = null;
            Session["WorkOrder"] = null;
        }

        #endregion

        #region Metodos

        protected void CargarIdioma()
        {
            lblPalletId.Text = _textoLabels.readStatement(formName, _idioma, "lblPalletId");
            btnSend.Text = _textoLabels.readStatement(formName, _idioma, "btnSend");
            //btnSave.Text = _textoLabels.readStatement(formName, _idioma, "btnSave");
          ////  minlenght.ErrorMessage = _textoLabels.readStatement(formName, _idioma, "regularWorkOrder");
          // // RequiredFieldPallet.ErrorMessage = _textoLabels.readStatement(formName, _idioma, "RequiredFieldPallet");
          // // PalletError.ErrorMessage = _textoLabels.readStatement(formName, _idioma, "customWorkOrder");
          //  grdRecords.Columns[0].HeaderText = _textoLabels.readStatement(formName, _idioma, "headPosition");
          //  grdRecords.Columns[1].HeaderText = _textoLabels.readStatement(formName, _idioma, "headItem");
          //  grdRecords.Columns[2].HeaderText = _textoLabels.readStatement(formName, _idioma, "headDescription");
          //  grdRecords.Columns[3].HeaderText = _textoLabels.readStatement(formName, _idioma, "headWarehouse");
          //  grdRecords.Columns[4].HeaderText = _textoLabels.readStatement(formName, _idioma, "headLot");
          //  grdRecords.Columns[5].HeaderText = _textoLabels.readStatement(formName, _idioma, "headPalletID");
          //  grdRecords.Columns[6].HeaderText = _textoLabels.readStatement(formName, _idioma, "headReturnQty");
          //  grdRecords.Columns[7].HeaderText = _textoLabels.readStatement(formName, _idioma, "headUnit");
          //  grdRecords.Columns[8].HeaderText = _textoLabels.readStatement(formName, _idioma, "headConfirmed");
            //grdRecords.Columns[9].HeaderText = _textoLabels.readStatement(formName, _idioma, "headConfirmed");
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


        protected void generateDropDownReasonCodes()
        {
            InterfazDAL_tticol125 idal = new InterfazDAL_tticol125();
            Ent_tticol125 obj = new Ent_tticol125();
            DataTable resultado = idal.getReasonCodes(ref strError);

            if (resultado.Rows.Count > 0) {

                int rowIndex = 0;
                ListItem itemS = null;
                itemS = new ListItem();
                itemS.Text = _idioma == "INGLES" ? "-- Select an option -- " : " -- Seleccione --";
                itemS.Value = "";
                dropDownReasonCodes.Items.Insert(rowIndex, itemS);

                //ListItem itemS = null; 
                foreach (DataRow dr in resultado.Rows)
                {
                    itemS = new ListItem();
                    rowIndex = (int)resultado.Rows.IndexOf(dr);
                    itemS.Value = dr.ItemArray[0].ToString();
                    itemS.Text = dr.ItemArray[1].ToString();
                    dropDownReasonCodes.Items.Insert(rowIndex + 1, itemS);
                }
            }
        }

        protected void generateDropDownCostCenters()
        {

            InterfazDAL_tticol125 idal = new InterfazDAL_tticol125();
            Ent_tticol125 obj = new Ent_tticol125();
            DataTable resultado = idal.getCostCenters(ref strError);

            if (resultado.Rows.Count > 0)
            {
                int rowIndex = 0;
                ListItem itemS = null; 
                itemS = new ListItem();
                itemS.Text = _idioma == "INGLES" ? "-- Select an option -- " : " -- Seleccione --";
                itemS.Value = "";
                dropDownCostCenters.Items.Insert(rowIndex, itemS);

                foreach (DataRow dr in resultado.Rows)
                {
                    itemS = new ListItem();
                    rowIndex = (int) resultado.Rows.IndexOf(dr);
                    itemS.Value = dr.ItemArray[0].ToString();
                    itemS.Text = dr.ItemArray[1].ToString();
                    dropDownCostCenters.Items.Insert(rowIndex + 1, itemS);
                }
            }

           

          
        }
        #endregion

        protected void btnSave_Click(object sender, EventArgs e)
        {

            if (Convert.ToInt32(txtAdjustmentQuantity.Text.Trim()) <= 0)
            {

                lblError.Text = Adjustmentquantitycannotbezero;
                txtPalletId.Enabled = true;
                txtPalletId.Text = String.Empty;
                btnSend.Visible = true;
                return;
            }
            if (Convert.ToInt32(txtAdjustmentQuantity.Text.Trim()) > Convert.ToInt32(lblQuantityValue.Text.Trim()))
            {
                lblError.Text = AdjustmentquantityshouldbelessthanexistingQty;
                txtPalletId.Enabled = true;
                txtPalletId.Text = String.Empty;
                btnSend.Visible = true;
                return;
            }
            InterfazDAL_twhcol025 idal = new InterfazDAL_twhcol025();
            Ent_twhcol025 obj = new Ent_twhcol025();
            string strError = string.Empty;
            List<Ent_twhcol025> parameterCollection025 = new List<Ent_twhcol025>();
            //T$PAID, T$ITEM, T$LOCA, T$CLOT, T$QTYA, T$UNIT, T$DATE, T$LOGN, T$EMNO, T$PROC, T$REFCNTD,T$REFCNTU

            obj.PAID = txtPalletId.Text.ToUpperInvariant();
            obj.ITEM = lblItemValue.Text.Trim().ToUpper();
            //obj.LOCA = lblLocationValue.Text.Trim().ToUpper();
            //obj.CLOT = lblLot.Text.Trim().ToUpper();
            obj.LOCA = lblLocationValue.Text;
            obj.CLOT = lblLotValue.Text;
            obj.QTYA = Convert.ToInt32(txtAdjustmentQuantity.Text.Trim());
            obj.UNIT = lblUnitValue.Text.Trim().ToUpper();
            obj.LOGN = Session["user"].ToString(); ;
            obj.CDIS = dropDownReasonCodes.SelectedItem.Value;
            obj.EMNO = dropDownCostCenters.SelectedItem.Value; 
         
            obj.PROC = 2;
            obj.REFCNTD = 0;
            obj.REFCNTU = 0;
            parameterCollection025.Add(obj);

            var validSave = idal.insertRegistrItemAdjustment(ref parameterCollection025, ref strError);

            if (validSave > 0)
            {
                lblConfirm.Text = mensajes("msjsave");
                //divTable.Visible = false;
                txtPalletId.Enabled = true;
                txtPalletId.Text = String.Empty;
                btnSend.Visible = true;
                return;
            }
            else
            {
                lblError.Text = mensajes("errorsave");
                return;
            }
        }

        protected static string mensajesStatic(string tipoMensaje)
        {
            Mensajes mensajesForm = new Mensajes();
            string idioma = "INGLES";
            var retorno = mensajesForm.readStatement("InventoryAdjustment.aspx", idioma, ref tipoMensaje);

            if (retorno.Trim() == String.Empty)
            {
                retorno = mensajesForm.readStatement(globalMessages, idioma, ref tipoMensaje);
            }

            return retorno;
        }

    }
}