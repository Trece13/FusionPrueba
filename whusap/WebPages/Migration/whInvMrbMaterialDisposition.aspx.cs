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
using System.Configuration;
using System.Drawing;
using System.EnterpriseServices;
using System.Web.Services;
using iTextSharp.text.pdf.interfaces;
using Newtonsoft.Json;
using whusa.Utilidades;
using System.Globalization;
using System.Threading;
using System.Web.Configuration;


namespace whusap.WebPages.Migration
{
    public partial class whInvMrbMaterialDisposition : System.Web.UI.Page
    {
        #region Propiedades
        private static InterfazDAL_tticol125 _idaltticol125 = new InterfazDAL_tticol125();
        protected static InterfazDAL_tticol042 idal042 = new InterfazDAL_tticol042();
        InterfazDAL_tticol127 idal127 = new InterfazDAL_tticol127();
        public List<Ent_tticol127> ListaItems = new List<Ent_tticol127>();
        public List<Ent_tticol127> ListaLotes = new List<Ent_tticol127>();
        Ent_tticol042 obj042 = new Ent_tticol042();
        public string ListaItemsJSON = string.Empty;
        public string ListaLotesJSON = string.Empty;
        private static InterfazDAL_tticol100 _idaltticol100 = new InterfazDAL_tticol100();
        public static IntefazDAL_transfer Transfers = new IntefazDAL_transfer();
        DataTable DTregrind = new DataTable();
        DataTable cantidadRegrind = new DataTable();
        string strError = string.Empty;
        string strOrden = string.Empty;
        string stritem = String.Empty;
        string stockwn = String.Empty;
        private static string formName;
        public string PalletIDdoesntexistsCannotcontinue    = string.Empty;
        public string PalletIDdoesntavailablefordisposition = string.Empty;

        //Manejo idioma
        private static Mensajes _mensajesForm = new Mensajes();
        private static LabelsText _textoLabels = new LabelsText();
       
        private static string globalMessages = "GlobalMessages";
        public static string _idioma;

        public string FactorKG = string.Empty;

        public bool corregible = false;

        string MyRegrind = string.Empty;
        string MyRegrindDesc = string.Empty;


        //Etiquetas Errores

        public string lblitemError = string.Empty;
        public string lblLotError = string.Empty;

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-CO");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-CO");

            txtPalletId.Focus();
            Page.Form.DefaultButton = btnSend.UniqueID;

            var ctrlName = Request.Params[Page.postEventSourceID];
            var args = Request.Params[Page.postEventArgumentID];
          //  Page.Form.Unload += new EventHandler(Form_Unload);

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
                // _operator = Session["user"].ToString();

                Label control = (Label)Page.Controls[0].FindControl("lblPageTitle");
                if (control != null) { control.Text = strTitulo; }
            }
            generateWarehouseData();
        }



        protected void generateWarehouseData()
        {

            InterfazDAL_ttcmcs003 idal = new InterfazDAL_ttcmcs003();
            //  Ent_ttcmcs003 obj = new Ent_ttcmcs003();
            DataTable resultado = idal.listRecordCwar(ref strError);
            //  dropDownWarehouse.Items.Clear();
            if (dropDownWarehouse.Items.Count <= 0)
            {


                if (resultado.Rows.Count > 0)
                {
                    int rowIndex = 0;
                    ListItem itemS = null;
                    itemS = new ListItem();
                    itemS.Text = _idioma == "INGLES" ? "-- Select an option -- " : " -- Seleccione --";
                    itemS.Value = "";
                    dropDownWarehouse.Items.Insert(rowIndex, itemS);

                    foreach (DataRow dr in resultado.Rows)
                    {
                        itemS = new ListItem();
                        rowIndex = (int)resultado.Rows.IndexOf(dr);
                        itemS.Value = dr.ItemArray[0].ToString();
                        itemS.Text = dr.ItemArray[0].ToString() + "-" + dr.ItemArray[1].ToString();
                        dropDownWarehouse.Items.Insert(rowIndex + 1, itemS);
                    }
                }
            }




        }
       

        //protected void Form_Unload(object sender, EventArgs e)
        //{
        //    //Session["FilaImprimir"] = null;
        //    Session["resultado"] = null;
        //    Session["resultado1"] = null;
        //    Session["WorkOrder"] = null;
        //}
        protected static string mensajes(string tipoMensaje)
        {
            Mensajes mensajesForm = new Mensajes();
            var retorno = mensajesForm.readStatement(formName, _idioma, ref tipoMensaje);

            if (retorno.Trim() == String.Empty)
            {
                retorno = mensajesForm.readStatement(globalMessages, _idioma, ref tipoMensaje);
            }

            return retorno;
        }
        protected void CargarIdioma()
        {
            lblMrbWarehouse.Text = _textoLabels.readStatement(formName, _idioma, "lblMrbWarehouse");
            btnSend.Text = _textoLabels.readStatement(formName, _idioma, "btnSend");
            //  btnGuardar.Text = _textoLabels.readStatement(formName, _idioma, "btnGuardar");
            grdRecords.Font.Size = FontUnit.Medium;
            //  lblDescItem.Text = _textoLabels.readStatement(formName, _idioma, "lblDescItem");
            // lblDescLot.Text = _textoLabels.readStatement(formName, _idioma, "lblDescLot");
            //     btnSend.Text = _textoLabels.readStatement(formName, _idioma, "btnSend");
            //printLabel.Text = _textoLabels.readStatement(formName, _idioma, "printLabel");
            btnSave.Text = _textoLabels.readStatement(formName, _idioma, "btnSave");
            grdRecords.Columns[0].HeaderText = _textoLabels.readStatement(formName, _idioma, "headItem");
            grdRecords.Columns[1].HeaderText = _textoLabels.readStatement(formName, _idioma, "headDescription");
            grdRecords.Columns[2].HeaderText = _textoLabels.readStatement(formName, _idioma, "headWarehouse");
            grdRecords.Columns[5].HeaderText = _textoLabels.readStatement(formName, _idioma, "headLot");
            //grdRecords.Columns[4].HeaderText = _textoLabels.readStatement(formName, _idioma, ".");
            grdRecords.Columns[6].HeaderText = _textoLabels.readStatement(formName, _idioma, "headUnit");
            grdRecords.Columns[7].HeaderText = _textoLabels.readStatement(formName, _idioma, "headToReturn");
            grdRecords.Columns[8].HeaderText = _textoLabels.readStatement(formName, _idioma, "headDisposition");
            grdRecords.Columns[9].HeaderText = _textoLabels.readStatement(formName, _idioma, "headReason");
            grdRecords.Columns[10].HeaderText = _textoLabels.readStatement(formName, _idioma, "headSupplier");
            //grdRecords.Columns[1].HeaderText = _textoLabels.readStatement(formName, _idioma, "headStock");
            grdRecords.Columns[12].HeaderText = _textoLabels.readStatement(formName, _idioma, "headStock");
            grdRecords.Columns[13].HeaderText = _textoLabels.readStatement(formName, _idioma, "headRegrind");
            grdRecords.Columns[14].HeaderText = _textoLabels.readStatement(formName, _idioma, "headComments");

            //grdRecords.Columns[0].HeaderText = _textoLabels.readStatement(formName, _idioma, "headSupplier");
            ////grdRecords.Columns[1].HeaderText = _textoLabels.readStatement(formName, _idioma, "headStock");
            //grdRecords.Columns[2].HeaderText = _textoLabels.readStatement(formName, _idioma, "headStock");
            //grdRecords.Columns[3].HeaderText = _textoLabels.readStatement(formName, _idioma, "headRegrind");
            //grdRecords.Columns[4].HeaderText = _textoLabels.readStatement(formName, _idioma, "headComments");

            lblitemError = _textoLabels.readStatement(formName, _idioma, "lblItemError");
            lblLotError = _textoLabels.readStatement(formName, _idioma, "lblLotError");
            PalletIDdoesntexistsCannotcontinue    = mensajes("PalletIDdoesntexistsCannotcontinue");
            PalletIDdoesntavailablefordisposition = mensajes("PalletIDdoesntavailablefordisposition");

        }

        protected void Dispoid_SelectedIndexChanged(object sender, EventArgs e)
        {
            // dispoc = (GridViewRow)sender;
            DropDownList listdispo = (DropDownList)sender;
            GridViewRow contenedor = (GridViewRow)listdispo.Parent.DataItemContainer;
            DropDownList liststock = (DropDownList)grdRecords.Rows[0].Cells[0].FindControl("Stockwareh");
            DropDownList listregrind = (DropDownList)grdRecords.Rows[0].Cells[1].FindControl("Regrind");
            DropDownList listproveedor = (DropDownList)grdRecords.Rows[0].Cells[2].FindControl("Supplier");

            btnSave.Visible = true;
            btnSave.Enabled = true;

            if (listdispo.SelectedValue == "2")
            {
                listregrind.Enabled = false;
                liststock.Enabled = false;
                listproveedor.Enabled = true;
            }
            if (listdispo.SelectedValue == "3")
            {
                listregrind.Enabled = false;
                listproveedor.Enabled = false;
                liststock.Enabled = true;

            }
            if (listdispo.SelectedValue == "4")
            {
                liststock.Enabled = false;
                listproveedor.Enabled = false;
                listregrind.Enabled = true;


                if (listregrind.Items.Count > 1)
                {
                    btnSave.Visible = true;
                    btnSave.Enabled = true;
                }
                else
                {
                    btnSave.Visible = false;
                    btnSave.Enabled = false;
                }
            }
            if (listdispo.SelectedValue == "5")
            {
                liststock.Enabled = false;
                listproveedor.Enabled = false;
                listregrind.Enabled = false;
            }

            if (listdispo.SelectedItem.Text == "Return to Vendor")
            {
                InterfazDAL_tticol118 idalp = new InterfazDAL_tticol118();
                Ent_tticol118 obj = new Ent_tticol118();
                obj.item = Session["Item"].ToString().Trim();
                DataTable proveedor = idalp.ListaProveedoresProducto(ref obj, ref strError);
                listproveedor.DataSource = proveedor;
                listproveedor.DataBind();
                listproveedor.Items.Insert(0, new ListItem(_idioma == "INGLES" ? "Select Supplier here..." : "Seleccione un proveedor...", string.Empty));
            }
            else
            {
                InterfazDAL_tticol118 idalp = new InterfazDAL_tticol118();
                Ent_tticol118 obj = new Ent_tticol118();
                obj.item = Session["Item"].ToString().Trim();
                listproveedor.DataSource = idalp.listaProveedores_ParamMRB(ref obj, ref strError);
                listproveedor.DataBind();
                listproveedor.Items.Insert(0, new ListItem(_idioma == "INGLES" ? "Select Supplier here..." : "Seleccione un proveedor...", string.Empty));
            }
        }


        protected void btnSend_Click(object sender, EventArgs e)
        {

            // Session["txtItem"] = txtItem.Text.ToUpper();
            // Session["txtLot"] = string.IsNullOrEmpty(txtLot.Text.ToUpper()) || string.IsNullOrWhiteSpace(txtLot.Text.ToUpper()) ? " " : txtLot.Text.ToUpper();

            grdRecords.DataSource = "";
            grdRecords.DataBind();
            grdRecords.Visible = false;
            if (string.IsNullOrEmpty(txtPalletId.Text.Trim()))
            {
                RequiredField.Enabled = true;
                RequiredField.IsValid = false;
                //  txtItem.Focus();
                btnSave.Visible = false;
                grdRecords.DataSource = "";
                grdRecords.DataBind();
                return;
            }
            //Validate Pallet

            InterfazDAL_tticol125 idal1 = new InterfazDAL_tticol125();
            Ent_tticol125 obj = new Ent_tticol125();
            string strError = string.Empty;
            string retorno = string.Empty;
            var palletID = txtPalletId.Text.Trim();
            obj.paid = palletID.Trim().ToUpperInvariant();

            DataTable resultado = idal1.vallidatePalletIDMRB(ref obj, ref strError);
            decimal status;
            string itemName = string.Empty;
            string lot = string.Empty;
            string unit = string.Empty;
            string tableName = string.Empty;
            string qty = string.Empty;
            lblError.Text = string.Empty;
            lblResult.Text = string.Empty;
            if (resultado.Rows.Count < 1)
            {
                lblError.Text = PalletIDdoesntexistsCannotcontinue;
                return;
            }
            else
            {


                foreach (DataRow dr in resultado.Rows)
                {
                    status = Convert.ToDecimal(dr.ItemArray[8].ToString());
                    tableName = dr.ItemArray[0].ToString();
                    itemName = dr.ItemArray[4].ToString().Trim().ToUpper();
                    lot = dr.ItemArray[9].ToString().Trim().ToUpper();
                    unit = dr.ItemArray[5].ToString().Trim().ToUpper();
                    qty = dr.ItemArray[3].ToString();
                    if ((tableName == "whcol131") && (status != 11))
                    {
                        lblError.Text = PalletIDdoesntavailablefordisposition;
                        return;
                    }
                    else if ((tableName == "ticol022") && (status != 3))
                    {
                        lblError.Text = PalletIDdoesntavailablefordisposition;
                        return;
                    }

                    lbltable.Value = tableName;
                 //   With this you bind Theme variable qty to Theme grid, if Theme namespace is correct @resultado will BeachBumCashFactory bound to the grid
                   // grdRecords.DataSource = resultado;
                 //   grdRecords.DataBind();
                   
                }
            }

            // Validar si el usuario tiene almacen MRB asociado
            //Validar si el Item y Lote digitado son correctos
            InterfazDAL_tticol127 idal = new InterfazDAL_tticol127();
            Ent_tticol127 obj1 = new Ent_tticol127();
            //   string strError = string.Empty;

            obj1.user = Session["user"].ToString().ToUpper();

            //OIBPW-01600022  OI0010389
                obj1.item = itemName.ToString();
               obj1.lote = lot.ToString();
               obj1.cwar = dropDownWarehouse.Text;
               Session["Item"] = itemName.ToString();
               Session["txtLot"] = string.IsNullOrEmpty(lot.ToUpper()) || string.IsNullOrWhiteSpace(lot.ToUpper()) ? " " : lot.ToUpper();

          //  obj1.item = "OIBPW-01600022";
          //  obj1.lote = "OI0010389";


            lblResult.Text = string.Empty;
            DataTable resultado1 = idal.listaRegistrosOrden_ParamMRB(ref obj1, ref strError, ref strOrden);

            if (strError != string.Empty)
            {

                //  txtPalletId.Text = "";
                //    txtLot.Text = "";
                //   txtItem.Focus();
                btnSave.Visible = false;
                grdRecords.DataSource = "";
                grdRecords.DataBind();
                lblResult.Text = strError.StartsWith("User") ? mensajes("notwarehouseuser")
                                : strError.StartsWith("Lot") ? mensajes("lotnotexists")
                                : strError.StartsWith("Item") && strError.Contains("MRB") ? mensajes("notstockitem")
                                : mensajes("itemnotexists");


                return;
            }
            else
            {
                // string itemInitials = string.Empty;
                grdRecords.DataSource = resultado1;
                grdRecords.DataBind();
                grdRecords.Visible = true;
                //  toReturn.Text = qty;

                //Do the Vaodation For lblResultIf Items Starts with Letter ‘B’ shows 4th Option “Recycle if not show only 3 options
                Session["Orden"] = strOrden;
                Session["Lote"] = lot.ToString();
                Session["Unit"] = unit.ToString();
                this.HeaderGrid.Visible = true;
                //btnSave.Visible = true;
                lblResult.Text = "";

               
           
              

            }
            var itemInitials = itemName.Trim().ToUpper().Substring(0, 1);
            foreach (GridViewRow row in grdRecords.Rows)
            {
                 ((TextBox)row.Cells[7].FindControl("toReturn")).Text=qty;
                 if (itemInitials != "B")
                 {
                     ((DropDownList)row.Cells[8].FindControl("Dispoid")).Items[4].Enabled = false;
                 }
            }
            //  txtItem.Text = "";
            //   txtLot.Text = "";
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var validUpdate = 0;
            InterfazDAL_tticol118 idal = new InterfazDAL_tticol118();
            List<Ent_tticol118> parameterCollection = new List<Ent_tticol118>();
            Ent_tticol118 obj = new Ent_tticol118();
            List<Ent_tticol042> parameterCollectionRegrind = new List<Ent_tticol042>();
           
            obj042 = new Ent_tticol042();
            Ent_twhcol020 objWhcol020 = new Ent_twhcol020();
            
            DataTable Transferencias = Transfers.ConsultarRegistroTransferir(txtPalletId.Text);
            //Recorrer filas con valores en los textos
            string disposition = String.Empty;
            string reason = String.Empty;
            string stockw = String.Empty;
            string orden = String.Empty;
            string strError = string.Empty;
            //var Regrind;

            foreach (GridViewRow row in grdRecords.Rows)
            {
                string toreturn = ((TextBox)row.Cells[7].FindControl("toReturn")).Text;
              //  string lote = ((TextBox)row.Cells[7].FindControl("toReturn")).Text;
                
                reason = ((DropDownList)row.Cells[9].FindControl("Reasonid")).SelectedValue;
                disposition = ((DropDownList)row.Cells[8].FindControl("Dispoid")).SelectedValue;
              //  stockw = ((DropDownList)grdRecords.Rows[0].Cells[1].FindControl("Stockwareh")).SelectedValue;
                stockw = ((DropDownList)row.Cells[11].FindControl("Stockwareh")).SelectedValue;
                stockwn = ((DropDownList)row.Cells[11].FindControl("Stockwareh")).SelectedItem.Text;
                DataTable DTMyRegrind = new DataTable();

                Session["MyRegrind"] = ((DropDownList)grdRecords.Rows[0].Cells[1].FindControl("Regrind")).SelectedValue.ToString().Trim();
                if (stockw == String.Empty)
                {
                    stockw = "NA";
                }
                string regrind = ((DropDownList)grdRecords.Rows[0].Cells[2].FindControl("Regrind")).SelectedValue;
                if (regrind == String.Empty)
                {
                    regrind = "NA";
                }
                string obse = ((TextBox)grdRecords.Rows[0].Cells[3].FindControl("Comments")).Text;
                if (obse.Length > 255)
                {
                    obse = ((TextBox)grdRecords.Rows[0].Cells[3].FindControl("Comments")).Text.Substring(1, 255);
                }
                string supplier = ((DropDownList)grdRecords.Rows[0].Cells[0].FindControl("Supplier")).SelectedValue;
                if (supplier == string.Empty)
                {
                    supplier = " ";
                }
                if (Session["Lote"].ToString() == "" || Session["Lote"].ToString() == " ")
                {
                    Session["Lote"] = " ";
                }


                if (!toreturn.Equals(string.Empty))
                {
                    obj = new Ent_tticol118();
                    obj.item = "         " + Session["Item"].ToString().Trim().ToUpperInvariant();
                    obj.cwar =dropDownWarehouse.Text.ToUpperInvariant();
                    //obj.clot = row.Cells[3].Text.ToUpperInvariant();
                    obj.clot = Session["Lote"].ToString();
                    obj.qtyr = Double.Parse(toreturn, System.Globalization.CultureInfo.InvariantCulture);//Convert.ToDecimal(toreturn);
                    obj.cdis = reason;
                    obj.obse = obse;
                    obj.logr = Session["user"].ToString();
                    obj.disp = Convert.ToInt32(disposition);
                    obj.stoc = stockw;
                    obj.ritm = regrind;
                    obj.proc = 2;
                    obj.mess = " ";
                    obj.suno = supplier;
                    obj.refcntd = 0;
                    obj.refcntu = 0 ;
                    obj.paid = txtPalletId.Text.ToString();
                    parameterCollection.Add(obj);
                }


                //REgrid Logic
                decimal cantidad;
                string cantidads;
                cantidads = toreturn.Replace(".", ",");
                //bool convert = decimal.TryParse(txtQuantity.Text.Trim(), out cantidad);
                bool convert = decimal.TryParse(cantidads, out cantidad);
                var numberFormatInfo = new NumberFormatInfo { NumberDecimalSeparator = "," };
                var value = Decimal.Parse(cantidads, numberFormatInfo);
                if (Convert.ToInt32(disposition) == 4) //Regrid
                {
                    string strTagId = string.Empty;

                    DataTable resultado = idal.inv_datospesos(ref obj, ref strError);
                    DataRow reg = resultado.Rows[0];

                    //insert a new record on tables ticol042 and ticol242 and whcol020.
                    obj042.pdno = txtPalletId.Text.Substring(0, 9);
                    cantidadRegrind = idal042.listaCantidadRegrind(ref obj042, ref strError);
                    strTagId = txtPalletId.Text.Substring(0, 9) + "-R" + cantidadRegrind.Rows[0]["CANT"].ToString();
                    Session["TagId"] = strTagId;
                    strError = string.Empty;
                    obj042.sqnb = strTagId;
                    obj042.proc = 1;
                    obj042.logn = Session["user"].ToString();
                    obj042.mitm = regrind;
                    obj042.pono = 10;
                    //Convert.ToDouble(fila.ItemArray[3]) * Convert.ToDouble(fila.ItemArray[7])) / 2.2046
                    obj042.qtdl = Math.Round((Convert.ToDecimal((value * Convert.ToDecimal(reg.ItemArray[1])) / Convert.ToDecimal(2.2046))), 2);
                    //obj042.qtdl = Convert.ToDecimal(value) / Convert.ToDecimal(2.2046);
                    obj042.cuni = "Kg";
                    obj042.log1 = Session["user"].ToString();
                    obj042.qtd1 = 0;
                    obj042.pro1 = 1;
                    obj042.log2 = Session["user"].ToString();
                    //obj042.qtd2 = Convert.ToDecimal((Convert.ToDecimal(reg.ItemArray[3]) * Convert.ToDecimal(reg.ItemArray[7])) / Convert.ToDecimal(2.2046));
                    obj042.pro2 = 1;
                    obj042.loca = " ";
                    obj042.norp = 0;
                    obj042.dele = 7; //Located
                    obj042.logd = " ";
                    obj042.refcntd = 0;
                    obj042.refcntu = 0;
                    obj042.drpt = DateTime.Now;
                    obj042.urpt = " ";
                    //obj042.acqt = Convert.ToDouble(value);
                    obj042.acqt = Convert.ToDouble(value) / Convert.ToDouble(2.2046);
                    obj042.cwaf = dropDownWarehouse.Text.ToUpperInvariant();
                    obj042.cwat = stockw;
                    obj042.aclo = " ";
                    obj042.wreg = reg.ItemArray[2].ToString();
                    obj042.allo = 0;
                    parameterCollectionRegrind.Add(obj042);



                    objWhcol020.tbl = lbltable.Value.ToString().Trim();
                    //CLOT = ,
                    objWhcol020.clot = txtPalletId.Text.Substring(1, 9);
                    objWhcol020.sqnb = txtPalletId.Text.ToString().Trim();
                    objWhcol020.mitm = Session["Item"].ToString().Trim().ToUpperInvariant();
                    //DSCA  = "",
                    objWhcol020.dsca = Session["Item"].ToString().Trim().ToUpperInvariant();
                    objWhcol020.cwor = dropDownWarehouse.Text.ToUpperInvariant();
                    objWhcol020.loor = " ";
                    objWhcol020.cwde = stockw;
                    objWhcol020.lode = " ";

                    objWhcol020.qtdl = Convert.ToDouble(value);
                    objWhcol020.cuni = Session["Unit"].ToString();
                    //DATE  = ,
                    //MESS  = ,
                    objWhcol020.user = Session["user"].ToString();



                }
            }



            
            //int actualizar = idal.actualizarRegistro_Param(ref parameterCollection, ref strError);
            //if (actualizar < 1)

            if (string.IsNullOrEmpty(reason) || string.IsNullOrWhiteSpace(reason))
            {
                corregible = true;
                strError = _textoLabels.readStatement(formName, _idioma, "lblReasoNull");
                lblError1.Text = strError;
               return;
            }
            else
            {
                idal.insertarRegistro(ref parameterCollection, ref strError);

                if (Convert.ToInt32(disposition) == 3) //Return to Stock
                {

                    //update whcol131 or ticol022 table: status to “Located” 
                    Ent_tticol100 dataticol100 = new Ent_tticol100()
                    {

                        paid = txtPalletId.Text.ToString(),
                        logr = Session["user"].ToString(),


                    };
                    string updstatus;
                    var tableName = lbltable.Value;
                    if (lbltable.Value == "ticol022")
                    {
                        updstatus = "7";
                        validUpdate = _idaltticol100.ActualizaRegistro_located(ref dataticol100, ref updstatus, ref tableName, ref strError);
                        //warehouse Logic



                    }
                    else if (lbltable.Value == "whcol131")
                    {
                        updstatus = "3";
                        validUpdate = _idaltticol100.ActualizaRegistro_located(ref dataticol100, ref updstatus, ref tableName, ref strError);
                        //warehouse Logic

                    }

                    var stockware = stockw.ToString();
                    var palletId = txtPalletId.Text.ToString();
                    //update actual warehouse field on table ticol222 to MRB Warehouse:
                    var validUpdate1 = _idaltticol100.ActualUpdateStockWarehouse_ticol222(ref tableName, ref stockware, ref palletId, ref strError);



                }

                if (Convert.ToInt32(disposition) == 4) //Regrid
                {
                    string strTagId = string.Empty;

                   


                    //insert a new record on tables ticol042 and ticol242 and whcol020.
                 

                    int retornoRegrind = idal042.insertarRegistro(ref parameterCollectionRegrind, ref strError);
                    bool retornoRegrindTticon242 = idal042.insertarRegistroTticon242(ref parameterCollectionRegrind, ref strError);
                    bool TransferenciasI = Transfers.InsertarTransferencia(objWhcol020);

                }
            }

            


            if (strError != string.Empty)
            {
                lblResult.Text = mensajes("errorsave");
            }
            else
            {
                printResult.Visible = true;
                /*   <asp:ListItem Value="2">Return to Vendor</asp:ListItem>
                                    <asp:ListItem Value="3">Return to Stock</asp:ListItem>
                                    <asp:ListItem Value="4">Regrind</asp:ListItem>
                                    <asp:ListItem Value="5">Recycle</asp:ListItem>
                 */
                if (Convert.ToInt32(disposition) == 4 ||Convert.ToInt32(disposition) == 3)
                {

                    DataTable resultado = idal.invLabel_registroImprimir_Param(ref obj, ref strError);
                    resultado.Columns.Add("USER", typeof(String));
                    DataRow reg = resultado.Rows[0];
                    resultado.Rows[0]["USER"] = Session["user"].ToString().ToUpper();


                    //Generate Unique Regrind unique identifier should  dispot =4 - regrid
                    DataTable resultado1 = idal.invRegrid_Indentifier(ref obj, ref strError);
                    DataRow reg1 = resultado1.Rows[0];
                     var cnt= reg1.ItemArray[0];
                     //int increment = 1;
                     //cnt = Convert.ToInt32(cnt) + increment;
                     //Session["cnt"] = cnt;
                    Session["FilaImprimir"] = reg;
                    //Session["war"] = dropDownWarehouse.SelectedItem.Text;
                    Session["war"] = stockwn;
                    //printLabel.Visible = true;
                }
                lblResult.Text = strError;
                lblResult.Text = mensajes("msjupdt");
                this.HeaderGrid.Visible = false;

            }

            if (corregible == false)
            {
                grdRecords.DataSource = "";
                grdRecords.DataBind();
                grdRecords.DataSource = "";
                grdRecords.DataBind();
            }

            if (strError != string.Empty)
            {
                lblResult.Text = strError;
                //throw new System.InvalidOperationException(strError);
            }
            else
            {


                //Print Label Inmmediatly

                if (Convert.ToInt32(disposition) == 4)
                {

                    obj.item = Session["Item"].ToString().Trim().ToUpperInvariant(); 

                    obj.clot = Session["Lote"].ToString().Trim();

                    lblResult.Text = string.Empty;

                    DataTable resultadop = idal.listaRegistros_Param(ref obj, ref strError);

                    resultadop.Columns.Add("FactorKG", typeof(string));

                    resultadop.Rows[0]["FactorKG"] = FactorKG.Trim();

                    Session["resultadop"] = resultadop;

                    StringBuilder paramurl = new StringBuilder();

                    //paramurl.Append("?");

                    //paramurl.Append("valor1=" + Request.QueryString[0].ToString() + "&");

                    //paramurl.Append("valor2=" + Request.QueryString[1].ToString() + "&");

                    //paramurl.Append("valor3=" + Request.QueryString[2].ToString());

                    Session["IsPreviousPage"] = "";

                    StringBuilder script = new StringBuilder();

                    script.Append("ventanaImp = window.open('../Labels/whInvLabelMaterialRejectedMRB.aspx', ");
                    script.Append("'ventanaImp', 'menubar=0,resizable=0,width=580,height=450');");
                    script.Append("ventanaImp.moveTo(30, 0);");
                    //script.Append("setTimeout (ventanaImp.close(), 20000);");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "printTag", script.ToString(), true);
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "alert('Information Saved Successfully.');", true);
                    lblResult.Text = mensajes("msjupdt");
                    lblResult.Visible = true;
                }
                if (Convert.ToInt32(disposition) ==3 ) //Return To Stock
                {
                    Session["disposition"] = "stock";

                    obj.item = Session["Item"].ToString().Trim().ToUpperInvariant();

                    obj.clot = Session["Lote"].ToString().Trim();

                    lblResult.Text = string.Empty;

                    DataTable resultadop = idal.listaRegistros_Param(ref obj, ref strError);

                    resultadop.Columns.Add("FactorKG", typeof(string));

                    resultadop.Rows[0]["FactorKG"] = FactorKG.Trim();

                    Session["resultadop"] = resultadop;

                    StringBuilder paramurl = new StringBuilder();

                    //paramurl.Append("?");

                    //paramurl.Append("valor1=" + Request.QueryString[0].ToString() + "&");

                    //paramurl.Append("valor2=" + Request.QueryString[1].ToString() + "&");

                    //paramurl.Append("valor3=" + Request.QueryString[2].ToString());

                    Session["IsPreviousPage"] = "";

                    StringBuilder script = new StringBuilder();

                    script.Append("ventanaImp = window.open('../Labels/whInvLabelMRBReturnToStock.aspx', ");
                    script.Append("'ventanaImp', 'menubar=0,resizable=0,width=580,height=450');");
                    script.Append("ventanaImp.moveTo(30, 0);");
                    //script.Append("setTimeout (ventanaImp.close(), 20000);");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "printTag", script.ToString(), true);

                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "alert('Information Saved Successfully.');", true);
                    lblResult.Text = mensajes("msjupdt");
                    lblResult.Visible = true;
                }


            }

        }
        protected void printLabel_Click(object sender, EventArgs e)
        {

            InterfazDAL_tticol118 idal = new InterfazDAL_tticol118();
            Ent_tticol118 obj = new Ent_tticol118();
            string strError = string.Empty;
            obj.item = Session["Item"].ToString();
            obj.clot = Session["Lote"].ToString();
            lblResult.Text = string.Empty;
            DataTable resultado = idal.listaRegistros_Param(ref obj, ref strError);
            resultado.Columns.Add("FactorKG", typeof(string));
            resultado.Rows[0]["FactorKG"] = FactorKG.Trim();
            Session["resultado"] = resultado;

            StringBuilder paramurl = new StringBuilder();
            paramurl.Append("?");
            paramurl.Append("valor1=" + Request.QueryString[0].ToString() + "&");
            paramurl.Append("valor2=" + Request.QueryString[1].ToString() + "&");
            paramurl.Append("valor3=" + Request.QueryString[2].ToString());
            Session["IsPreviousPage"] = "";

            StringBuilder script = new StringBuilder();
            script.Append("ventanaImp = window.open('../Labels/whInvLabelMaterialRejectedDMRB.aspx', ");
            script.Append("'ventanaImp', 'menubar=0,resizable=0,width=580,height=450');");
            script.Append("ventanaImp.moveTo(30, 0);");
            //script.Append("setTimeout (ventanaImp.close(), 20000);");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "printTag", script.ToString(), true);
        }

        protected void grdRecords_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Verificar que si la cantidad es igual a 0, el control "toReturn" se deshabilite
                string stock = ((HiddenField)e.Row.Cells[4].FindControl("actualqty")).Value.Trim();
                stritem = ((DataRowView)e.Row.DataItem).DataView.Table.Rows[e.Row.RowIndex]["Item"].ToString();

                if (Convert.ToDouble(stock) == 0)
                {
                    ((TextBox)e.Row.Cells[6].FindControl("toReturn")).Enabled = false;
                    ((TextBox)e.Row.Cells[6].FindControl("toReturn")).Attributes.Add("onfocus", "limpiar(this);");

                }
                // Verificar que si el lote es nulo o vacio
                string strLote = ((DataRowView)e.Row.DataItem).DataView.Table.Rows[e.Row.RowIndex]["LOT"].ToString();

                DataRow drv = ((DataRowView)e.Row.DataItem).Row;
                var fila = drv.ItemArray.ToArray();

                var serializador = new JavaScriptSerializer();
                var FilaSerializada = serializador.Serialize(fila);

                ((RangeValidator)e.Row.Cells[6].FindControl("validateQuantity")).MinimumValue = "0";
                ((RangeValidator)e.Row.Cells[6].FindControl("validateQuantity")).MaximumValue = ((DataRowView)e.Row.DataItem).DataView.Table.Rows[e.Row.RowIndex]["stock"].ToString();

                //Llenar Stock Warehouse Dropdownlist
                InterfazDAL_tticol118 idalp = new InterfazDAL_tticol118();
                Ent_tticol118 obj = new Ent_tticol118();
                obj.item = stritem;

                //Llenar Reason Dropdownlist
                InterfazDAL_tticol118 idal = new InterfazDAL_tticol118();
                DataTable reason = idal.listaReason_Param(ref strError);
                DropDownList listreason = (DropDownList)e.Row.Cells[9].FindControl("Reasonid");
                listreason.DataSource = reason;
                listreason.DataTextField = "descr";
                listreason.DataValueField = "reason";
                listreason.DataBind();
                listreason.Items.Insert(0, new ListItem(_idioma == "INGLES" ? "Select Reason here..." : "Seleccione una razón...", string.Empty));

                //DataTable proveedor = idalp.listaProveedores_Param(ref obj, ref strError);
                //DropDownList listproveedor = (DropDownList)e.Row.Cells[9].FindControl("Supplier");
                //listproveedor.DataSource = proveedor;
                //listproveedor.DataTextField = "nombre";
                //listproveedor.DataValueField = "proveedor";
                //listproveedor.DataBind();
                //listproveedor.Items.Insert(0, new ListItem("Select Supplier here...", string.Empty));


                ////Llenar Reason Dropdownlist
                //InterfazDAL_tticol118 idal = new InterfazDAL_tticol118();
                //DataTable reason = idal.listaReason_Param(ref strError);
                //DropDownList listreason = (DropDownList) e.Row.Cells[10].FindControl("Reasonid");
                //listreason.DataSource = reason;
                //listreason.DataTextField = "descr";
                //listreason.DataValueField = "reason";
                //listreason.DataBind();
                //listreason.Items.Insert(0, new ListItem("Select Reason here...", string.Empty));               

                ////Llenar Disposition Dropdownlist desde el webconfig
                //////if (ConfigurationManager.AppSettings.AllKeys.Contains("Disposition"))
                //////{
                //////    string listdisposition = ConfigurationManager.AppSettings["Disposition"];

                //////    var dispositionarray = listdisposition.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries).ToList<string>();
                //////    dispositionarray.Insert(0, "-- Select Disposition --");

                //////    DropDownList dispoid = (DropDownList)e.Row.Cells[11].FindControl("Dispositionid");
                //////    dispoid.DataSource = dispositionarray;
                //////    dispoid.DataBind();
                //////}
                //////else { lblResult.Visible = true; lblResult.Text = "Disposition List not found"; }

                ////Llenar Stock Warehouse Dropdownlist
                //InterfazDAL_tticol118 idalsw = new InterfazDAL_tticol118();
                //DataTable stockw = idalsw.listaStockw_Param(ref strError);
                //DropDownList liststockw = (DropDownList) e.Row.Cells[11].FindControl("Stockwareh");
                //liststockw.DataSource = stockw;
                //liststockw.DataTextField = "descrw";
                //liststockw.DataValueField = "warehouse";
                //liststockw.DataBind();
                //liststockw.Items.Insert(0, new ListItem("Select Stock Warehouse here...", string.Empty));

                ////Llenar Stock Warehouse Dropdownlist
                //InterfazDAL_tticol118 idalr = new InterfazDAL_tticol118();
                //DataTable regrind = idalr.listaRegrind_Param(ref strError);
                //DropDownList listregrind = (DropDownList)e.Row.Cells[12].FindControl("Regrind");
                //listregrind.DataSource = regrind;
                //listregrind.DataTextField = "descrc";
                //listregrind.DataValueField = "item";
                //listregrind.DataBind();
                //listregrind.Items.Insert(0, new ListItem("Select Item here...", string.Empty));
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Llenar Stock Warehouse Dropdownlist
                InterfazDAL_tticol118 idalp = new InterfazDAL_tticol118();
                Ent_tticol118 obj = new Ent_tticol118();
                obj.item = stritem;
                DataTable proveedor = idalp.listaProveedores_ParamMRB(ref obj, ref strError);
                DropDownList listproveedor = (DropDownList)e.Row.Cells[0].FindControl("Supplier");
                listproveedor.DataSource = proveedor;
                listproveedor.DataTextField = "nombre";
                listproveedor.DataValueField = "proveedor";
                listproveedor.DataBind();
                listproveedor.Items.Insert(0, new ListItem(_idioma == "INGLES" ? "Select Supplier here..." : "Seleccione un proveedor...", string.Empty));

                //Llenar Disposition Dropdownlist desde el webconfig
                ////if (ConfigurationManager.AppSettings.AllKeys.Contains("Disposition"))
                ////{
                ////    string listdisposition = ConfigurationManager.AppSettings["Disposition"];

                ////    var dispositionarray = listdisposition.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries).ToList<string>();
                ////    dispositionarray.Insert(0, "-- Select Disposition --");

                ////    DropDownList dispoid = (DropDownList)e.Row.Cells[11].FindControl("Dispositionid");
                ////    dispoid.DataSource = dispositionarray;
                ////    dispoid.DataBind();
                ////}
                ////else { lblResult.Visible = true; lblResult.Text = "Disposition List not found"; }

                //Llenar Stock Warehouse Dropdownlist
                InterfazDAL_tticol118 idalsw = new InterfazDAL_tticol118();
                DataTable stockw = idalsw.listaStockw_Param(ref strError);
                DropDownList liststockw = (DropDownList)e.Row.Cells[1].FindControl("Stockwareh");
                liststockw.DataSource = stockw;
                liststockw.DataTextField = "warehouseFullName";
                liststockw.DataValueField = "warehouse";
                liststockw.DataBind();
                liststockw.Items.Insert(0, new ListItem(_idioma == "INGLES" ? "Select Stock Warehouse here..." : "Seleccione inventario almacen...", string.Empty));

                //Llenar Stock Warehouse Dropdownlist
                InterfazDAL_tticol118 idalr = new InterfazDAL_tticol118();
                DTregrind = idalr.listaRegrind_ParamV2(ref stritem, ref strError);
                Session["DTregrind"] = DTregrind;
                //DataTable regrind = idalr.listaRegrind_Param(ref strError);
                DropDownList listregrind = (DropDownList)e.Row.Cells[2].FindControl("Regrind");
                listregrind.DataSource = DTregrind;
                listregrind.DataTextField = "descrc";
                listregrind.DataValueField = "item";
                listregrind.DataBind();
                listregrind.Items.Insert(0, new ListItem(_idioma == "INGLES" ? "Select Item here..." : "Seleccione un articulo...", string.Empty));


            }
        }

        #endregion
    }
}