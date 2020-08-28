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
using System.Globalization;
using System.Threading;
using System.Configuration;
using System.Web.Configuration;


namespace whusap.WebPages.Migration
{
    public partial class whInvMrbRejection : System.Web.UI.Page
    {
        #region Propiedades
        private static InterfazDAL_tticst001 _idaltticst001 = new InterfazDAL_tticst001();
        private static InterfazDAL_ttcibd001 _idalttcibd001 = new InterfazDAL_ttcibd001();
        private static InterfazDAL_ttcmcs003 _idalttcmcs003 = new InterfazDAL_ttcmcs003();
        private static InterfazDAL_tticol125 _idaltticol125 = new InterfazDAL_tticol125();
        private static InterfazDAL_tticol022 _idaltticol022 = new InterfazDAL_tticol022();
        private static InterfazDAL_ttisfc001 _idalttisfc001 = new InterfazDAL_ttisfc001();
        private static InterfazDAL_ttcmcs005 _idalttcmcs005 = new InterfazDAL_ttcmcs005();
        private static InterfazDAL_tticol011 _idaltticol011 = new InterfazDAL_tticol011();
        private static InterfazDAL_tticol100 _idaltticol100 = new InterfazDAL_tticol100();
        private static InterfazDAL_twhwmd200 _idaltwhwmd200 = new InterfazDAL_twhwmd200();
        private static InterfazDAL_twhwmd300 _idaltwhwmd300 = new InterfazDAL_twhwmd300();
        private static InterfazDAL_twhltc100 _idaltwhltc100 = new InterfazDAL_twhltc100();
        private static InterfazDAL_twhinr140 _idaltwhinr140 = new InterfazDAL_twhinr140();
        private static InterfazDAL_tticol116 _idaltticol116 = new InterfazDAL_tticol116();
        string strError = string.Empty;
        string Aplicacion = "WEBAPP";
        public string UrlBaseBarcode = WebConfigurationManager.AppSettings["UrlBaseBarcode"].ToString();
        //Manejo idioma
       
        private static LabelsText _textoLabels = new LabelsText();
        private static DataTable _consultaPedido;
        private static DataTable _consultaInformacionPedido;
        private static string formName;
        private static string globalMessages = "GlobalMessages";
        private static Mensajes _mensajesForm = new Mensajes();
        private static DataTable _validaItem;
        private static DataTable _validaWarehouse;
        private static DataTable _validaUbicacion;
        private static DataTable _validaItemLote;
        public static DataTable _validarOrden = new DataTable();
        public static string _idioma;
        public DataTable listaReasons = new DataTable();
        private static string _operator;
        public int CantidadDevuelta = 0;
        public decimal Pqty = 0;
        public string itempallet;
        public string lotepallet;
        public string tableNameSave;
        private static Decimal _stock;
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
            Page.Form.Unload += new EventHandler(Form_Unload);

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
                _operator = Session["user"].ToString();

                Label control = (Label)Page.Controls[0].FindControl("lblPageTitle");
                if (control != null) { control.Text = strTitulo; }
            }
            generateWarehouseData();
        }

       
        protected void btnSend_Click(object sender, EventArgs e)
        {
           

            InterfazDAL_tticol125 idal = new InterfazDAL_tticol125();
            Ent_tticol125 obj = new Ent_tticol125();
            string strError = string.Empty;
            string retorno = string.Empty;

            var palletID = txtPalletId.Text.Trim();
            obj.paid = palletID.Trim().ToUpperInvariant();
            lblError.Text = "";
            lblErrorAnnounce.Text = "";
            lblErrorDelivered.Text = "";
            lblErrorLocated.Text = "";
            lblConfirmAnnounce.Text = "";
            lblConfirmDelivered.Text = "";
            lblConfirmLocated.Text = "";
           

            divBtnGuardarDelivered.Visible = false;
            divBtnGuardarLocated.Visible = false;
            divBtnGuardarAnnouce.Visible = false;

            divTableDelivered.Visible = false;
            divTableAnnounce.Visible = false;
            divTableLocated.Visible = false;

            divLabelAnnounce.Visible = false;
            divLabel.Visible = false;
            divLabelDelivered.Visible = false;

            //btnSalirAnnounce.Visible = false;
            //btnSalirDelivered.Visible = false;
            //btnSalirLocate.Visible = false;

            divBotonesDelivered.Visible = false;
            divBotonesAnnounce.Visible = false;
            divBotonesLocated.Visible = false;


            DataTable resultado = idal.vallidatePalletIDMRB(ref obj, ref strError);

      
            decimal status;
            string itemName = string.Empty;
            string lot = string.Empty;
            string loc = string.Empty;
            string tableName = string.Empty;
            string war = string.Empty;
             string orno = string.Empty;
             string statusCheck = string.Empty;
            if (resultado.Rows.Count < 1)
            {
                lblError.Text = mensajes("palletnotexists");
                return;
            }
            else
            {
                foreach (DataRow dr in resultado.Rows)
                {
                    status = Convert.ToDecimal(dr.ItemArray[8].ToString());
                    itemName = dr.ItemArray[4].ToString().Trim().ToUpper();
                    lot = dr.ItemArray[9].ToString().Trim().ToUpper();
                    loc = dr.ItemArray[7].ToString().Trim().ToUpper();
                    war = dr.ItemArray[6].ToString().Trim().ToUpper();
                    orno = dr.ItemArray[1].ToString().Trim().ToUpper();
                    Pqty = Convert.ToDecimal(dr.ItemArray[3].ToString());
                    statusCheck =dr.ItemArray[8].ToString();
                     if (string.IsNullOrEmpty(lot) == true)
                     {
                         Session["Lot"] = string.Empty;
                     }
                     else
                     {
                         Session["Lot"] =lot;
                     }

                     if (string.IsNullOrEmpty(loc) == true)
                     {
                         Session["Location"] = string.Empty;
                     }
                     else
                     {
                         Session["Location"] = loc;
                     }
                     if (string.IsNullOrEmpty(war) == true)
                     {
                         Session["Warehouse"] = string.Empty;
                     }
                     else
                     {
                         Session["Warehouse"] = war;
                     }
                     if (string.IsNullOrEmpty(itemName) == true)
                     {
                         Session["ItemName"] = string.Empty;
                     }
                     else
                     {
                         Session["ItemName"] = itemName;
                     }
                     if (string.IsNullOrEmpty(orno) == true)
                     {
                         Session["OrNo"] = string.Empty;
                     }
                     else
                     {
                         Session["OrNo"] = orno;
                     }
                    tableName = dr.ItemArray[0].ToString();
                    retorno = "";
                    if ((tableName == "whcol131") && (status == 4))
                    {
                        retorno = "Announced";
                        Session["TableNameSave"] = tableName;
                        TxtOrder.Enabled = false;
                        break;
                    }
                    else if ((tableName == "whcol131") && (status == 9))
                    {
                        retorno = "Delivered";
                        TxtOrder.Enabled = true;
                        SetFocus(TxtOrder);
                        itempallet = itemName;
                        lotepallet = lot;
                        Session["TableNameSave"] = tableName;
                        Session["OrNo"] = TxtOrder.Text.Trim();
                        break;
                    }
                    else if ((tableName == "whcol131")  && (status == 3))
                    {
                        retorno = "Located";
                        Session["TableNameSave"] = tableName;
                        TxtOrder.Enabled = false;
                        break;
                    }
                    else if ((tableName == "ticol022") && (status == 2))
                    {
                        retorno = "Announced";
                        Session["TableNameSave"] = tableName;
                        TxtOrder.Enabled = false;
                        break;
                    }
                    else if ((tableName == "ticol022")  && (status == 11))
                    {
                        retorno = "Delivered";
                        TxtOrder.Enabled = true;
                        SetFocus(TxtOrder);
                        itempallet = itemName;
                        lotepallet = lot;
                        Session["TableNameSave"] = tableName;
                        Session["OrNo"] = TxtOrder.Text.Trim();
                        break;
                    }
                    else if ((tableName == "ticol022")  && (status == 7))
                    {
                        retorno = "Located";
                        Session["TableNameSave"] = tableName;
                        TxtOrder.Enabled = false;
                        break;
                    }
                }
            }
          // string inlistring= statusCheck;
            if (retorno != "")
            {
                getData(retorno);
            }
            else
            {
                string inlineStatus = string.Empty;
                if (tableName == "whcol131")
                {
                   // statusCheck = "wer";
                    switch (statusCheck)
                    {
                        case "1":
                            inlineStatus = "Received";
                            break;
                        case "2":
                            inlineStatus = "Picked";
                            break;
                        case "5":
                            inlineStatus = "Validated";
                            break;
                        case "6":
                            inlineStatus = "Requested";
                            break;
                        case "7":
                            inlineStatus = "Picking";
                            break;
                        case "8":
                            inlineStatus = "Validated";
                            break;
                        case "10":
                            inlineStatus = "Blocked";
                            break;
                        case "11":
                            inlineStatus = "Rejected";
                            break;
                        case "12":
                            inlineStatus = "ToDelete";
                            break;
                        case "13":
                            inlineStatus = "Delete";
                            break;
                    }
                }
                 if (tableName == "ticol022")
                {
                    switch (statusCheck)
                    {
                        case "1":
                            inlineStatus="Deleted";
                            break;
                        case "3":
                            inlineStatus = "Rejected";
                            break;
                        case "4":
                            inlineStatus = "Validated";
                            break;
                        case "5":
                            inlineStatus = "Received";
                            break;
                        case "6":
                            inlineStatus = "Picked";
                            break;
                        case "8":
                            inlineStatus = "Requested";
                            break;
                        case "9":
                            inlineStatus = "Picking";
                            break;
                        case "10":
                            inlineStatus = "Dropped CP";
                            break;
                        case "12":
                            inlineStatus = "Blocked";
                            break;
                        case "13":
                            inlineStatus = "ToDelete";
                            break;
                    }
                }
             
                    lblError.Text =   "Pallet ID status doesn´t allow rejection Actual Status " + inlineStatus;
                    return;
            }
            
           // return retorno;
        }

        protected void getData(string retorno)
        {

             if (retorno == "Announced")
             {
                 divBotonesAnnounce.Visible = false;
                 divLabelAnnounce.Visible = false;

                 var order = txtPalletId.Text.Trim().ToUpper();

                 if (order == String.Empty)
                 {
                     lblErrorAnnounce.Text = mensajes("formempty");
                     return;
                 }

                 _validarOrden = _idaltticol022.findRecordBySqnbRejectedPlantMRBRejection(ref order, ref strError);

                 if (_validarOrden.Rows.Count > 0)
                 {
                     lblErrorAnnounce.Text = string.Empty;
                     //var sqnb = _validarOrden.Rows[0]["SQNB"].ToString().Trim();
                     //var mitm = _validarOrden.Rows[0]["MITM"].ToString().Trim();
                     //var dsca = _validarOrden.Rows[0]["DSCA"].ToString().Trim();
                     //var qtdl = _validarOrden.Rows[0]["QTDL"].ToString().Trim();
                     //var pdno = _validarOrden.Rows[0]["PDNO"].ToString().Trim();

                     var dele = Convert.ToInt32(_validarOrden.Rows[0]["DELE"].ToString());
                     var pro1 = Convert.ToInt32(_validarOrden.Rows[0]["PRO1"].ToString());
                     var proc = Convert.ToInt32(_validarOrden.Rows[0]["PROC"].ToString());

                    
                     //if (proc == 2)
                     //{
                     //    lblErrorAnnounce.Text = mensajes("palletnotannounced");
                     //    return;
                     //}

                     if (dele != 2 && dele != 4)
                     {
                         lblErrorAnnounce.Text = mensajes("palletdeleted");
                         return;
                     }

                     if (pro1 == 1)
                     {
                         lblErrorAnnounce.Text = mensajes("palletconfirmed");
                         return;
                     }

                     makeTableAnnounce();
                     divBtnGuardarAnnouce.Visible = true;
                     divTableAnnounce.Visible = true;
                 }
                 else
                 {
                     lblErrorAnnounce.Text = mensajes("palletnotexists");
                     return;
                 } 
             }
             else if (retorno == "Delivered")  //Deliverd
             {
                 var worder = TxtOrder.Text.Trim().ToUpper();
                 var encontrado = false;
                 if (worder != String.Empty)
                 {
                     divLabelDelivered.Visible = false;
                     divBotonesDelivered.Visible = false;
                     //  slItems.Items.Clear();
                     txtDescription.Text = string.Empty;
                     txtQty.Text = string.Empty;
                     txtUnit.Text = string.Empty;
                     txtShift.Text = string.Empty;
                     slReason.Items.Clear();
                     slRejectType.Items.Clear();
                     txtExactReasons.InnerText = string.Empty;
                     // var item = Session["ItemName"].ToString(); 
                     var pdno = Session["OrNo"].ToString();

                     if (pdno != String.Empty)
                     {
                         _consultaPedido = _idaltticst001.findByPdnoMRB(ref pdno, ref strError);

                         if (_consultaPedido.Rows.Count > 0)
                         {
                             _consultaInformacionPedido = _idalttisfc001.findByOrderMaterialRejected(ref worder, ref strError);

                             if (_consultaInformacionPedido.Rows.Count > 0)
                             {
                                 lblError.Text = "";
                                 //lblValueOrder.Text = _consultaPedido.Rows[0]["PDNO"].ToString();
                                 lblValueOrder.Text = worder;
                                 //var itemName = _consultaPedido.Rows[0]["MITM"].ToString();
                                 var itemName = itempallet;

                                 foreach (DataRow item in _consultaInformacionPedido.Rows)
                                 {
                                     var itemorder = item["SITM"].ToString();
                                     if (itemName.Trim() == itemorder.Trim())
                                     {
                                         Ent_ttcibd001 data001 = new Ent_ttcibd001() { item = itemName };
                                         _validaItem = _idalttcibd001.listaRegistro_ObtieneDescripcionUnidad(ref data001, ref strError);

                                         //var lotes = _consultaPedido.Rows[0]["CLOT"].ToString();
                                         var lotes = lotepallet;

                                         slItems.Text = _validaItem.Rows[0]["ITEM"].ToString().Trim().ToUpper();
                                         txtDescription.Text = _validaItem.Rows[0]["DESCRIPCION"].ToString().Trim().ToUpper();
                                         slLot.Text = lotes.ToString();
                                         txtUnit.Text = _validaItem.Rows[0]["UNID"].ToString().Trim().ToUpper();
                                         txtkltc.Value = _validaItem.Rows[0]["KLTC"].ToString().Trim().ToUpper();

                                         txtpono.Value = _consultaInformacionPedido.Rows[0]["PONO"].ToString();
                                         //hdfQuantity.Value = _consultaInformacionPedido.Rows[0]["CANTIDAD"].ToString();
                                         hdfQuantity.Value = Pqty.ToString();


                                         var rstp = "10";
                                         var listaReasons = _idalttcmcs005.findRecords(ref rstp, ref strError);

                                         foreach (DataRow itemReason in listaReasons.Rows)
                                         {
                                             ListItem itemRazon = new ListItem()
                                             {
                                                 Text = itemReason["CDIS"].ToString().Trim() + " - " + itemReason["DSCA"].ToString().Trim(),
                                                 Value = itemReason["CDIS"].ToString().Trim().ToUpper()
                                             };

                                             slReason.Items.Insert(slReason.Items.Count, itemRazon);
                                         }

                                         ListItem supplied = new ListItem() { Text = "Supplied", Value = "1" };
                                         ListItem intern = new ListItem() { Text = "Internal", Value = "2" };
                                         ListItem retur = new ListItem() { Text = "Return", Value = "3" };

                                         slRejectType.Items.Insert(slRejectType.Items.Count, supplied);
                                         slRejectType.Items.Insert(slRejectType.Items.Count, intern);
                                         slRejectType.Items.Insert(slRejectType.Items.Count, retur);

                                         divTableDelivered.Visible = true;
                                         divBtnGuardarDelivered.Visible = true;
                                         lblErrorDelivered.Visible = false;
                                         encontrado = true;

                                     }
                                 }
                                 if (encontrado == false)
                                 {
                                     lblErrorDelivered.Text = mensajes("itemsnotequals");
                                     TxtOrder.Text = string.Empty;
                                     return;
                                 }
                             }
                             else
                             {
                                 lblErrorDelivered.Text = mensajes("notitems");
                                 return;
                             }
                         }
                         else
                         {
                             lblErrorDelivered.Text = mensajes("ordernotexists");
                             return;
                         }
                     }
                     else
                     {
                         lblErrorDelivered.Text = mensajes("formempty");
                         return;
                     }
                 }
             }
             else if (retorno == "Located")  //Located
             {
                 //                 , string itemName, string lotno, string loc,string war


                 lblErrorLocated.Text = String.Empty;
                 var item = Session["itemName"].ToString();
                 var warehouse = Session["Warehouse"].ToString();
                 var location = Session["Location"].ToString(); ;
                 var lot = Session["Lot"].ToString();



                 Ent_ttcibd001 data001 = new Ent_ttcibd001() { item = item };
                 _validaItem = _idalttcibd001.listaRegistro_ObtieneDescripcionUnidad(ref data001, ref strError);

                 if (_validaItem.Rows.Count < 1)
                 {
                     lblErrorLocated.Text = mensajes("itemnotexists");
                     return;
                 }

                 var kltc = Convert.ToInt32(_validaItem.Rows[0]["KLTC"].ToString());

                 _validaWarehouse = _idalttcmcs003.findRecordByCwar(ref warehouse, ref strError);

                 if (_validaWarehouse.Rows.Count < 1)
                 {
                     lblErrorLocated.Text = mensajes("warehousenotexists");
                     return;
                 }

                 Ent_twhwmd200 data200 = new Ent_twhwmd200() { cwar = warehouse };
                 var manejoUbicacionAlmacen = Convert.ToInt32(_idaltwhwmd200.listaRegistro_ObtieneAlmacenLocation(ref data200, ref strError).Rows[0]["LOC"]);

                 if (manejoUbicacionAlmacen == 1)
                 {
                     _validaUbicacion = _idaltwhwmd300.validateExistsLocation(ref location, ref warehouse, ref strError);

                     if (_validaUbicacion.Rows.Count < 1)
                     {
                         lblErrorLocated.Text = mensajes("locationnotexists");
                         return;
                     }
                 }

                 if (kltc == 1)
                 {
                     Ent_twhltc100 data100 = new Ent_twhltc100() { item = item, clot = lot };
                     _validaItemLote = _idaltwhltc100.listaRegistro_Clot(ref data100, ref strError);

                     if (_validaItemLote.Rows.Count < 1)
                     {
                         lblErrorLocated.Text = mensajes("lotnotexists");
                         return;
                     }
                 }

                 var consultaCantidad = new DataTable();

                 if (manejoUbicacionAlmacen == 1)
                 {
                     if (kltc == 1)
                     {
                         consultaCantidad = _idaltwhinr140.consultaPorAlmacenItemUbicacionLote(ref warehouse, ref item, ref location, ref lot, ref strError);
                     }
                     else
                     {
                         consultaCantidad = _idaltwhinr140.consultaPorAlmacenItemUbicacion(ref warehouse, ref item, ref location, ref strError);
                     }
                 }
                 else
                 {
                     if (kltc == 1)
                     {
                         consultaCantidad = _idaltwhinr140.consultaPorAlmacenItemLote(ref warehouse, ref item, ref lot, ref strError);
                     }
                     else
                     {
                         consultaCantidad = _idaltwhinr140.consultaCantidadItemLote(ref warehouse, ref item, ref strError);
                     }
                 }

                 _stock = (Decimal)0;

                 if (consultaCantidad.Rows.Count > 0)
                 {
                     _stock = Convert.ToDecimal(consultaCantidad.Rows[0]["STKS"].ToString());
                 }

                 if (_stock == 0)
                 {
                     lblErrorLocated.Text = mensajes("notstock");
                     return;
                 }

                 divBotonesLocated.Visible = false;
                 // divLabelLocated.Visible = false;
                 divBtnGuardarLocated.Visible = true;
                 makeTableLocated(location, lot);
             }
        }


        protected void makeTableLocated(string loc, string lot)
        {
            var table = String.Empty;
            CantidadDevuelta = _idalttcibd001.CantidadDevueltaStock(_validaItem.Rows[0]["ITEM"].ToString().Trim().ToUpper(), lot.Trim().ToUpper(), _validaWarehouse.Rows[0]["CWAR"].ToString().Trim().ToUpper(), loc.Trim());
            //Fila cwar
            table += String.Format("<hr /><div style='margin-bottom: 100px;'><table class='table table-bordered' style='width:1200px; font-size:13px; border:3px solid; border-style:outset; text-align:center;'>");

            table += String.Format("<tr style='font-weight:bold; background-color:lightgray;'><td>{0}</td><td colspan='6'>{1}</td></tr>"
                , _idioma == "INGLES" ? "Warehouse:" : "Almacen:"
                , String.Concat(_validaWarehouse.Rows[0]["CWAR"].ToString().Trim().ToUpper(), '-', _validaWarehouse.Rows[0]["DSCA"].ToString().Trim().ToUpper()));

            table += String.Format("<tr style='font-weight:bold; background-color:white;'><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td></tr>"
                , _idioma == "INGLES" ? "Item" : "Articulo"
                , _idioma == "INGLES" ? "Description" : "Descripción"
                , _idioma == "INGLES" ? "Unit" : "Unidad"
                , _idioma == "INGLES" ? "Location" : "Ubicación"
                , _idioma == "INGLES" ? "Description" : "Descripción"
                , _idioma == "INGLES" ? "Lot" : "Lote"
                , _idioma == "INGLES" ? "Comments" : "Comentarios");

            table += String.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td rowspan='3'>{6}</td></tr>"
                , _validaItem.Rows[0]["ITEM"].ToString().Trim().ToUpper()
                , _validaItem.Rows[0]["DESCRIPCION"].ToString().Trim().ToUpper()
                , _validaItem.Rows[0]["UNID"].ToString().Trim().ToUpper()
                , loc
                , _validaUbicacion != null ? _validaUbicacion.Rows.Count < 1 ? String.Empty : _validaUbicacion.Rows[0]["DSCA"].ToString().Trim().ToUpper() : String.Empty
                , lot
                , "<textarea id='txtComments' name='txtComments' rows='6'></textarea>");

            hdfDescItem.Value = _validaItem.Rows[0]["DESCRIPCION"].ToString().Trim().ToUpper();

            table += String.Format("<tr style='font-weight:bold; background-color:white;'><td>{0}</td><td>{1}</td><td>{2}</td><td colspan='3'>{3}</td></tr>"
               , _idioma == "INGLES" ? "Quantity" : "Cantidad"
               , _idioma == "INGLES" ? "Reason" : "Razón"
               , _validaItem.Rows[0]["ITEM"].ToString().Trim().ToUpper().Substring(0, 1) == "B" ? (_idioma == "INGLES" ? "" : "Proveedor") : string.Empty
               , _validaItem.Rows[0]["ITEM"].ToString().Trim().ToUpper().Substring(0, 1) == "B" ? (_idioma == "INGLES" ? "" : "Nombre proveedor") : string.Empty);

            var rstp = "10";
            var listaReasons = _idalttcmcs005.findRecords(ref rstp, ref strError);

            var selectReasons = "<select id='slReasons' name='slReasons' class='TextboxBig' onchange='setReason(this);'>";

            foreach (DataRow item in listaReasons.Rows)
            {
                selectReasons += String.Format("<option value='{0}'>{1}</option>", item["CDIS"].ToString().Trim(), item["CDIS"].ToString().Trim() + " - " + item["DSCA"].ToString().Trim());
            }

            table += String.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td colspan='3'>{3}</td></tr>"
                , String.Format(
                    "<input type='number' min='1' pattern='^[0-9]+' step='any' id='txtQuantity' name='txtQuantity' class='TextboxBig' onchange='validarCantidad(this,{0}," + CantidadDevuelta + ")' />",
                    _stock)
                , selectReasons
                , _validaItem.Rows[0]["ITEM"].ToString().Trim().ToUpper().Substring(0, 1) == "B"
                    ? (String.Format(
                        "<input type='text' id='txtSupplier' name='txtSupplier' value='{0}' class='TextboxBig' readonly='true'  style ='display:none'/>",
                        _validaItem.Rows[0]["OTBP"].ToString().Trim().ToUpper()))
                    : (String.Format(
                        "<input type='text' id='txtSupplier' name='txtSupplier' value='{0}' class='TextboxBig' readonly='true' style ='display:none'/>",
                        _validaItem.Rows[0]["OTBP"].ToString().Trim().ToUpper()))
                , _validaItem.Rows[0]["ITEM"].ToString().Trim().ToUpper().Substring(0, 1) == "B" ? (String.Format("<input type='text' id='txtNameSupplier' name='txtNameSupplier' value='{0}' class='TextboxBig'  style ='display:none' />", _validaItem.Rows[0]["NAMA"].ToString().Trim().ToUpper())) : (String.Format("<input type='text' id='txtNameSupplier' name='txtNameSupplier' value='{0}' class='TextboxBig' disabled style ='display:none'/>", _validaItem.Rows[0]["NAMA"].ToString().Trim().ToUpper())));

            table += "</table></div>";
            divTableLocated.InnerHtml = table;
            divTableLocated.Visible = true;
        }

         protected void makeTableAnnounce()
         {
             var rstp = "10";
             listaReasons = _idalttcmcs005.findRecords(ref rstp, ref strError);

             var selectReasons = "<select id='slReasons' name='slReasons' class='TextboxBig'>";

             foreach (DataRow item in listaReasons.Rows)
             {
                 selectReasons += String.Format("<option value='{0}'>{1}</option>", item["CDIS"].ToString().Trim(), item["CDIS"].ToString().Trim() + " - " + item["DSCA"].ToString().Trim());
             }

             selectReasons += "</select>";

             var table = String.Empty;

             //Fila cwar
             table += String.Format("<hr /><table class='table table-bordered' style='width:1200px; font-size:13px; border:3px solid; border-style:outset; text-align:center;'>");

             table += String.Format("<tr style='font-weight:bold; background-color:lightgray;'><td>{0}</td><td colspan='8'>{1}</td></tr>"
                 , _idioma == "INGLES" ? "Order:" : "Orden:"
                 , _validarOrden.Rows[0]["PDNO"].ToString().Trim().ToUpper());

             table += String.Format("<tr style='font-weight:bold; background-color:white;'><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td><td>{7}</td><td>{8}</td></tr>"
                 , _idioma == "INGLES" ? "Pallet Id" : "Id Pallet"
                 , _idioma == "INGLES" ? "Item" : "Articulo"
                 , _idioma == "INGLES" ? "Description" : "Descripción"
                 , _idioma == "INGLES" ? "Qty" : "Cant"
                 , _idioma == "INGLES" ? "Unit" : "Unidad"
                 , _idioma == "INGLES" ? "Shift" : "Cambio"
                 , _idioma == "INGLES" ? "Reason" : "Razón"
                 , _idioma == "INGLES" ? "Reject type" : "Tipo de devolución"
                 , _idioma == "INGLES" ? "Exact Reason" : "Razón exacta");

             for (int i = 0; i < _validarOrden.Rows.Count; i++)
             {
                 table += String.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td><td>{7}</td><td>{8}</td></tr>"
                     , _validarOrden.Rows[i]["SQNB"].ToString().Trim().ToUpper()
                     , _validarOrden.Rows[i]["MITM"].ToString().Trim().ToUpper()
                     , _validarOrden.Rows[i]["DSCA"].ToString().Trim().ToUpper()
                     , _validarOrden.Rows[i]["QTDL"].ToString().Trim().ToUpper()
                     , _validarOrden.Rows[i]["CUNI"].ToString().Trim().ToUpper()
                     , String.Format("<input type='text' id='{0}' name='{0}' class='TextBoxBig' onchange='validarShift(this);' />", "txtShift-" + i)
                     , selectReasons.Replace("id='slReasons'", "id='slReasons-" + i + "'").Replace("name='slReasons'", "name='slReasons-" + i + "'")
                     , String.Format("<select id='{0}' name='{0}' class='TextBoxBig'><option value='1'>Supplied</option><option value='2'>Internal</option><option value='3'>Return</option></select>", "txtRejectType-" + i)
                     , String.Format("<textarea id='{0}' name='{0}' rows='2'></textarea>", "txtExactReason-" + i));
             }

             //foreach (DataRow itemOrder in _validarOrden.Rows)
             //{
             //    table += String.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td><td>{7}</td><td>{8}</td></tr>"
             //        , itemOrder["SQNB"].ToString().Trim().ToUpper()
             //        , itemOrder["MITM"].ToString().Trim().ToUpper()
             //        , itemOrder["DSCA"].ToString().Trim().ToUpper()
             //        , itemOrder["QTDL"].ToString().Trim().ToUpper()
             //        , itemOrder["CUNI"].ToString().Trim().ToUpper()
             //        ,"<input type='text' id='txtShift' name='txtShift' class='TextBoxBig' onchange='validarShift(this);' />"
             //        , selectReasons
             //        ,"<select id='txtRejectType' name='txtRejectType' class='TextBoxBig'><option value='1'>Supplied</option><option value='2'>Internal</option><option value='3'>Return</option></select>"
             //        , "<textarea id='txtExactReason' name='txtExactReason' rows='2'></textarea>");
             //}

             table += "</table>";

             divTableAnnounce.InnerHtml = table;
             divTableAnnounce.Visible = true;
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
            lblMrbWarehouse.Text = _textoLabels.readStatement(formName, _idioma, "lblMrbWarehouse");
            btnSend.Text = _textoLabels.readStatement(formName, _idioma, "btnSend");
            btnSalirLocate.Text = _textoLabels.readStatement(formName, _idioma, "btnSalirLocate");
            btnSalirAnnounce.Text = _textoLabels.readStatement(formName, _idioma, "btnSalirAnnounce");
            btnSalirDelivered.Text = _textoLabels.readStatement(formName, _idioma, "btnSalirDelivered");
            btnGuardar.Text = _textoLabels.readStatement(formName, _idioma, "btnGuardar");
            btnGuardarDelivered.Text = _textoLabels.readStatement(formName, _idioma, "btnGuardarDelivered");
            btnGuardarLocated.Text = _textoLabels.readStatement(formName, _idioma, "btnGuardarLocated");
            lblOrder.Text = _textoLabels.readStatement(formName, _idioma, "lblOrder");
            lblDescription.Text = _textoLabels.readStatement(formName, _idioma, "lblDescription");
            lblDescriptionDelivered.Text = _textoLabels.readStatement(formName, _idioma, "lblDescriptionDelivered");
            lblQty.Text = _textoLabels.readStatement(formName, _idioma, "lblQty");
            lblUnit.Text = _textoLabels.readStatement(formName, _idioma, "lblUnit");
            lblLot.Text = _textoLabels.readStatement(formName, _idioma, "lblLot");
            lblShift.Text = _textoLabels.readStatement(formName, _idioma, "lblShift");
            lblReasonDelivered.Text = _textoLabels.readStatement(formName, _idioma, "lblReasonDelivered");
            lblRejectedType.Text = _textoLabels.readStatement(formName, _idioma, "lblRejectedType");
            lblExactReason.Text = _textoLabels.readStatement(formName, _idioma, "lblExactReason");
            lblItem.Text = _textoLabels.readStatement(formName, _idioma, "lblItem");
          //  btnSave.Text = _textoLabels.readStatement(formName, _idioma, "btnSave");
            //minlenght.ErrorMessage = _textoLabels.readStatement(formName, _idioma, "regularWorkOrder");
            //RequiredField.ErrorMessage = _textoLabels.readStatement(formName, _idioma, "requiredWorkOrder");
            //OrderError.ErrorMessage = _textoLabels.readStatement(formName, _idioma, "customWorkOrder");
            //grdRecords.Columns[0].HeaderText = _textoLabels.readStatement(formName, _idioma, "headPosition");
            //grdRecords.Columns[1].HeaderText = _textoLabels.readStatement(formName, _idioma, "headItem");
            //grdRecords.Columns[2].HeaderText = _textoLabels.readStatement(formName, _idioma, "headDescription");
            //grdRecords.Columns[3].HeaderText = _textoLabels.readStatement(formName, _idioma, "headWarehouse");
            //grdRecords.Columns[4].HeaderText = _textoLabels.readStatement(formName, _idioma, "headLot");
            //grdRecords.Columns[5].HeaderText = _textoLabels.readStatement(formName, _idioma, "headPalletID");
            //grdRecords.Columns[6].HeaderText = _textoLabels.readStatement(formName, _idioma, "headReturnQty");
            //grdRecords.Columns[7].HeaderText = _textoLabels.readStatement(formName, _idioma, "headUnit");
            //grdRecords.Columns[8].HeaderText = _textoLabels.readStatement(formName, _idioma, "headConfirmed");
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
        protected void btnGuardar_Click_announce(object sender, EventArgs e)
        {
            var validInsert = 0;
            var validUpdate = 0;
            lblErrorAnnounce.Text = String.Empty;
            Ent_tticol100 Objdata100 = new Ent_tticol100();
            for (int i = 0; i < _validarOrden.Rows.Count; i++)
            {
                var shift = Request.Form["txtShift-" + i].ToString().Trim();
                var reason = Request.Form["slReasons-" + i].ToString().Trim();
                var rejectType = Request.Form["txtRejectType-" + i].ToString().Trim();
                var exactReasons = Request.Form["txtExactReason-" + i].ToString().Trim();
                var pdno = txtPalletId.Text.Trim().ToUpper().Substring(0, 9);
                var paid = txtPalletId.Text.Trim().ToUpper();
                var cwar = dropDownWarehouse.Text.Trim();
                var machine = " ";
                var consecutivo = 1;


                if (shift != String.Empty && exactReasons != String.Empty)
                {
                    var findMachine = _idaltticol011.findRecordByPdno(ref pdno, ref strError);

                    if (findMachine.Rows.Count > 0)
                    {
                        machine = findMachine.Rows[0]["MCNO"].ToString();
                    }

                    var consultaConsecutivo = _idaltticol100.findMaxSeqnByPdno(ref pdno, ref strError);

                    if (consultaConsecutivo.Rows.Count > 0)
                    {
                        consecutivo = Convert.ToInt32(consultaConsecutivo.Rows[0]["SEQN"]) + 1;
                    }

                    Ent_tticol100 data100 = new Ent_tticol100()
                    {
                        pdno = pdno,
                        seqn = consecutivo,
                        seqnR = _validarOrden.Rows[0][1].ToString().Trim().ToUpper(),
                        mcno = machine,
                        shif = shift,
                        item = _validarOrden.Rows[i]["MITM"].ToString().Trim().ToUpper(),
                        qtyr = double.Parse(_validarOrden.Rows[i]["QTDL"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat),
                        cdis = reason,
                        rejt = Convert.ToInt32(rejectType),
                        clot = pdno,
                        obse = exactReasons,
                        logr = _operator,
                        dsca = _validarOrden.Rows[i]["DSCA"].ToString().Trim().ToUpper(),
                        cwar = cwar,
                        paid = paid

                    };
                    Objdata100 = data100;

                    validInsert += _idaltticol100.insertRecord(ref data100, ref strError);

                    if (validInsert > 0)
                    {

                         validUpdate = _idaltticol100.ActualizaRegistro_ticol022(ref data100, ref strError);
                 
                       
                        //update actual warehouse field on table ticol222 to MRB Warehouse: Announced.
                        
                        tableNameSave = Session["TableNameSave"].ToString();
                        if (tableNameSave == "ticol022")
                        {
                            validUpdate = _idaltticol100.ActualUpdateWarehouse_ticol222(ref data100, ref strError);
                        }
                        else
                        {
                            validUpdate = _idaltticol100.ActualUpdateWarehouse_whcol131(ref data100, ref strError);
                        }
                    }


                }
            }

            if (validInsert > 0)
            {
                lblErrorAnnounce.Text = "";
                lblConfirmAnnounce.Text = mensajes("msjsave");
                divTableAnnounce.InnerHtml = String.Empty;
                divBtnGuardarAnnouce.Visible = false;
                txtPalletId.Text = String.Empty;
                MakeLabel(Objdata100);
                divLabelAnnounce.Visible = true;
                divBotonesAnnounce.Visible = true;
            }
            else
            {
                lblErrorAnnounce.Text = mensajes("errorsave");
                return;
            }
        }


        protected void btnGuardar_Click_located(object sender, EventArgs e)
        {

            var validUpdate = 0;
            lblErrorLocated.Text = String.Empty;
            var item = Session["ItemName"].ToString().Trim().ToUpper();
            var warehouse = Session["Warehouse"].ToString().ToUpper();
            var location = Session["Location"].ToString().ToUpper();
            var lot = Session["Lot"].ToString().ToUpper();

            var cantidad = Convert.ToDouble(Request.Form["txtQuantity"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat);
            var cdis = Request.Form["slReasons"].ToString().Trim();
            var obse = Request.Form["txtComments"].ToString().Trim();
            var VariableAuxSuno = Request.Form["txtSupplier"].ToString().Trim();
            var suno = string.IsNullOrEmpty(VariableAuxSuno) ? string.Empty : Request.Form["txtSupplier"].ToString().Trim();
            var reasondesc = Request.Form["lblReasonDesc"];
            
            var paid = txtPalletId.Text.Trim().ToUpper();
            var cwam = dropDownWarehouse.Text.Trim();

            if (lot == string.Empty)
            {
                lblErrorLocated.Text = "The lot can't be empty";
                return;
            }

            if (reasondesc == null)
            {
                reasondesc = "Adhesion";
            }

            if (cantidad > Convert.ToDouble(_stock, CultureInfo.InvariantCulture.NumberFormat))
            {
                lblErrorLocated.Text = mensajes("quantexceed");
                return;
            }

            Ent_twhwmd300 data = new Ent_twhwmd300() { loca = location };
            var validaLocation = _idaltwhwmd300.listaRegistro_ObtieneAlmacen(ref data, ref strError);
            if (txSloc.Value.ToString().Trim() == "1")
            {
                if (validaLocation.Rows.Count > 0)
                {
                    if (Convert.ToInt32(validaLocation.Rows[0]["LOCT"]) != 5)
                    {
                        lblErrorLocated.Text = mensajes("bulklocation");
                        return;
                    }
                }
                else
                {
                    lblErrorLocated.Text = mensajes("locationnotexists");
                    return;
                }
            }

            var validarRegistro = _idaltticol116.findRecordByWarehouseItemLocation(ref item, ref warehouse, ref location, ref strError);

            if (validarRegistro.Rows.Count > 0)
            {
                //var validarUpdate = _idaltticol116.updateRecordRejectedWarehouse(ref item, ref warehouse, ref location, ref cantidad, ref strError);
                Ent_tticol116 data116 = new Ent_tticol116()
                {
                    item = item,
                    cwar = warehouse,
                    loca = location == String.Empty ? " " : location,
                    clot = lot == String.Empty ? " " : lot,
                    qtyr = cantidad,
                    cdis = cdis,
                    obse = obse == String.Empty ? " " : obse,
                    logr = _operator,
                    suno = suno == String.Empty ? " " : suno,
                    paid = paid,
                    cwam = cwam

                };
                Ent_tticol100 data100 = new Ent_tticol100()
                {

                    paid = paid,
                    cwar = warehouse,
                    logr = _operator,
                };
                var validInsertaux = _idaltticol116.insertarRegistro(ref data116, ref strError);

                if (validInsertaux > 0)
                {

                    //validUpdate = _idaltticol116.UpdatePalletStatus_ticol022(ref data116, ref strError);

                    ////update actual warehouse field on table ticol222 to MRB Warehouse:
                    //validUpdate = _idaltticol116.ActualUpdateWarehouse_ticol222(ref data116, ref strError);


                    //validUpdate = _idaltticol116.UpdatePalletStatus_ticol022(ref data116, ref strError);
                    validUpdate = _idaltticol100.ActualizaRegistro_ticol022(ref data100, ref strError);
                    
                    validUpdate = _idaltticol100.ActualUpdateWarehouse_ticol222(ref data100, ref strError);
                    //update actual warehouse field on table ticol222 to MRB Warehouse:  Located.
                    tableNameSave = Session["TableNameSave"].ToString();
                    if (tableNameSave == "ticol022")
                    {
                        validUpdate = _idaltticol116.ActualUpdateWarehouse_ticol222(ref data116, ref strError);
                    }
                    else
                    {
                        validUpdate = _idaltticol116.ActualUpdateWarehouse_whcol131(ref data116, ref strError);
                    }


                    lblErrorLocated.Text = String.Empty;
                    lblConfirmLocated.Text = mensajes("msjupdate");
                    divTableLocated.InnerHtml = String.Empty;
                    divBtnGuardarLocated.Visible = false;
                   // txtPalletId.Text = String.Empty;
                    lblDefectiveMaterial.Text = "DEFECTIVE MATERIAL-TAG";
                    lblDescRejectQty.Text = "Rejected qty - ";
                    lblDescPrintedBy.Text = "Printed by - ";
                    lblValueLot.Text = "Lot - ";
                    lblDescReason.Text = "Reason"; 
                    lblDescDescription.Text = "Description";
                    lblCommentsLocated.Text = "Comments : ";


                    var rutaServ1 = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + paid.Trim().ToUpper() + "&code=Code128&dpi=96";
                    imgPalletId.Src = !string.IsNullOrEmpty(txtPalletId.Text) ? rutaServ1 : "";

                    var rutaServ = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + item.Trim().ToUpper() + "&code=Code128&dpi=96";
                    imgCodeItem.Src = !string.IsNullOrEmpty(item) ? rutaServ : "";

                    var rutaServQty = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + cantidad.ToString().Trim().ToUpper() + "&code=Code128&dpi=96";
                    imgQty.Src = !string.IsNullOrEmpty(cantidad.ToString()) ? rutaServQty : "";

                    lblValueDescription.Text = hdfDescItem.Value;
                    lblDescRejectQty.Text = String.Concat(_textoLabels.readStatement(formName, _idioma, "lblRejectedQty"), " ", cantidad);
                    lblDescPrintedBy.Text = String.Concat(_textoLabels.readStatement(formName, _idioma, "lblPrintedBy"), " ", _operator);
                    lblValueLot.Text = String.Concat(_textoLabels.readStatement(formName, _idioma, "lblDescLot"), " ", lot);
                    lblCommentsLocated.Text = String.Concat(_textoLabels.readStatement(formName, _idioma, "lblDescComments"), " ", obse);
                    lblValueReasonLocated.Text = reasondesc;

                    //update actual warehouse field on table ticol222 to MRB Warehouse:  Located.
                    tableNameSave = Session["TableNameSave"].ToString();
                    if (tableNameSave == "ticol022")
                    {
                        validUpdate = _idaltticol116.ActualUpdateWarehouse_ticol222(ref data116, ref strError);
                    }
                    else
                    {
                        validUpdate = _idaltticol116.ActualUpdateWarehouse_whcol131(ref data116, ref strError);
                    }

                    divLabel.Visible = true;
                    divBotonesLocated.Visible = true;
                }
                else
                {
                    lblErrorLocated.Text = mensajes("errorupdt");
                    return;
                }
            }
            else
            {
                Ent_tticol116 data116 = new Ent_tticol116()
                {
                    item = item,
                    cwar = warehouse,
                    loca = location == String.Empty ? " " : location,
                    clot = lot == String.Empty ? " " : lot,
                    qtyr = cantidad,
                    cdis = cdis,
                    obse = obse == String.Empty ? " " : obse,
                    logr = _operator,
                    suno = suno == String.Empty ? " " : suno,
                    paid = paid,
                    cwam = cwam
                };
                Ent_tticol100 data100 = new Ent_tticol100()
                {
                   
                    paid = paid,
                    cwar = cwam,
                    logr = _operator,
                };
                var validInsert = _idaltticol116.insertarRegistro(ref data116, ref strError);

                if (validInsert > 0)
                {
                    lblErrorLocated.Text = String.Empty;
                    lblConfirmLocated.Text = mensajes("msjsave");
                    divTableLocated.InnerHtml = String.Empty;
                    divBtnGuardarLocated.Visible = false;
                    //txtPalletId.Text = String.Empty;

                    //validUpdate = _idaltticol116.UpdatePalletStatus_ticol022(ref data116, ref strError);
                    validUpdate = _idaltticol100.ActualizaRegistro_ticol022(ref data100, ref strError);
                        //update actual warehouse field on table ticol222 to MRB Warehouse:
                    //    validUpdate = _idaltticol116.ActualUpdateWarehouse_ticol222(ref data116, ref strError);
                    tableNameSave = Session["TableNameSave"].ToString();
                    if (tableNameSave == "ticol022")
                    {
                        validUpdate = _idaltticol100.ActualUpdateWarehouse_ticol222(ref data100, ref strError);
                    }
                    else
                    {
                        validUpdate = _idaltticol100.ActualUpdateWarehouse_whcol131(ref data100, ref strError);
                    }
                    

                    //var rutaServ = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + item.Trim().ToUpper() + "&code=Code128&dpi=96";
                    //imgCodeItem.Src = !string.IsNullOrEmpty(item) ? rutaServ : "";

                    //var rutaServQty = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + cantidad.ToString().Trim().ToUpper() + "&code=Code128&dpi=96";
                    //imgQty.Src = !string.IsNullOrEmpty(cantidad.ToString()) ? rutaServQty : "";

                    //lblValueDescription.Text = hdfDescItem.Value;
                    //lblDescRejectQty.Text = String.Concat(_textoLabels.readStatement(formName, _idioma, "lblRejectedQty"), " ", cantidad);
                    //lblDescPrintedBy.Text = String.Concat(_textoLabels.readStatement(formName, _idioma, "lblPrintedBy"), " ", _operator);
                    //lblValueLot.Text = String.Concat(_textoLabels.readStatement(formName, _idioma, "lblDescLot"), " ", lot);
                 //   lblComments.Text = String.Concat(_textoLabels.readStatement(formName, _idioma, "lblDescComments"), " ", obse);
                  //  lblValueReason.Text = reasondesc;

                        var rutaServ1 = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + paid.Trim().ToUpper() + "&code=Code128&dpi=96";
                        imgPalletId.Src = !string.IsNullOrEmpty(txtPalletId.Text) ? rutaServ1 : "";

                        var rutaServ = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + item.Trim().ToUpper() + "&code=Code128&dpi=96";
                        imgCodeItem.Src = !string.IsNullOrEmpty(item) ? rutaServ : "";

                        var rutaServQty = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + cantidad.ToString().Trim().ToUpper() + "&code=Code128&dpi=96";
                        imgQty.Src = !string.IsNullOrEmpty(cantidad.ToString()) ? rutaServQty : "";
                    
                        lblDefectiveMaterial.Text = "DEFECTIVE MATERIAL-TAG";
                        lblDescReason.Text = "Reason";
                        lblDescDescription.Text = "Description";

                        lblValueDescription.Text = hdfDescItem.Value;
                        lblDescRejectQty.Text = String.Concat(_textoLabels.readStatement(formName, _idioma, "lblRejectedQty"), " ", cantidad);
                        lblDescPrintedBy.Text = String.Concat(_textoLabels.readStatement(formName, _idioma, "lblPrintedBy"), " ", _operator);
                        lblValueLot.Text = String.Concat(_textoLabels.readStatement(formName, _idioma, "lblDescLot"), " ", lot);
                        lblCommentsLocated.Text = String.Concat(_textoLabels.readStatement(formName, _idioma, "lblDescComments"), " ", obse);
                        lblValueReasonLocated.Text = reasondesc;

                    divLabel.Visible = true;
                    divBotonesLocated.Visible = true;
                }
                else
                {
                    lblErrorLocated.Text = mensajes("errorsave");
                    return;
                }
            }
        }
        protected void MakeLabel(Ent_tticol100 Objtticol100)
        {
            var rstp = "10";
            listaReasons = _idalttcmcs005.findRecords(ref rstp, ref strError);
            IList<Reason> lstReasons = new List<Reason>();
            foreach (DataRow item in listaReasons.Rows)
            {
                Reason reason = new Reason
                {
                    cdis = item["CDIS"].ToString(),
                    dsca = item["DSCA"].ToString()
                };
                lstReasons.Add(reason);
            }

            var ObjReason = lstReasons.Where(x => x.cdis.Trim().ToUpper() == Objtticol100.cdis.Trim().ToUpper()).Single();

            var sqnb = _validarOrden.Rows[0]["SQNB"].ToString().Trim().ToUpper();
            var mitm = _validarOrden.Rows[0]["MITM"].ToString().Trim().ToUpper();
            var dsca = _validarOrden.Rows[0]["DSCA"].ToString().Trim().ToUpper();
            var qtyr = _validarOrden.Rows[0]["QTDL"].ToString().Trim().ToUpper();
            var pdno = _validarOrden.Rows[0]["PDNO"].ToString().Trim().ToUpper();
            //T$SQNB
            lblDmtNumber.Text = "DMT-NUMBER";
            lblDescription.Text = "Description";
            lblRejected.Text = "Rejected Qty";
            Label1.Text = "WorkOrder";
            lblTitleMachine.Text = "Machine";
            Label2.Text = "Disposition - Review";
            lblPrintedBy.Text = "Printed By";
            lblReason.Text = "Reason";
            lblFecha.Text = "Date:";
            lblValueReason.Text = "Adhesion";
            lblComments.Text = "Comments";

            lblDescMachine.Text = Objtticol100.mcno;

            lblValueComments.Text = Objtticol100.obse;
            lblValuePrintedBy.Text = Objtticol100.logr;
            lblValueReason.Text = ObjReason.dsca;
            LblSqnb.Text = sqnb;
            var rutaSqnb = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + sqnb.ToUpper() + "&code=Code128&dpi=96";
            imgCBSqnb.Src = !string.IsNullOrEmpty(sqnb) ? rutaSqnb : String.Empty;
            //T$MITM
            lblDsca.Text = mitm;
            var rutaMitm = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + mitm.ToUpper() + "&code=Code128&dpi=96";
            imgCBMitm.Src = !string.IsNullOrEmpty(mitm) ? rutaMitm : String.Empty;
            //T$DSCA
            lblDsca.Text = dsca;
            //T$QTDL
            lblValueQuantity.Text = Convert.ToString(qtyr);
            lblQtdl.Text = Objtticol100.Unit;
            var rutaQtdl = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + Convert.ToString(qtyr).ToUpper() + "&code=Code128&dpi=96";
            imgBCQtdl.Src = !string.IsNullOrEmpty(Convert.ToString(qtyr)) ? rutaQtdl : String.Empty;
            //T$PDNO
            lblPdno.Text = pdno;
            var rutaPdno = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + pdno.ToUpper() + "&code=Code128&dpi=96";
            imgBCPdno.Src = !string.IsNullOrEmpty(pdno) ? rutaPdno : String.Empty;
        }
        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("whInvMrbRejection.aspx");
        }



       
        protected void btnGuardar_Click_Delivered(object sender, EventArgs e)
        {
              var validUpdate = 0;
                lblErrorDelivered.Text = String.Empty;
            //var pdno = txtPalletId.Text.Trim().ToUpper().Substring(0, 9);
            var pdno = TxtOrder.Text.Trim().ToUpper().ToString();
            var machine = " ";
            var seqn = 1;
            var item = slItems.Text.Trim().ToUpper();
            var clot = slLot.Text.ToString().Trim().ToUpper();
            var reason = slReason.SelectedValue.Trim().ToUpper();
            var rejectType = slRejectType.SelectedValue.Trim().ToUpper();
            var qtyr = double.Parse(txtQty.Text.Trim(), CultureInfo.InvariantCulture.NumberFormat);
            var paid = txtPalletId.Text.Trim().ToUpper();
            var cwar = dropDownWarehouse.Text.Trim();
              
            if (item == string.Empty)
            {
                lblErrorDelivered.Text = "The Item can't be empty";
                return;
            }

            if (clot == string.Empty)
            {
                lblErrorDelivered.Text = "The lot can't be empty";
                return;
            }

            if (reason == string.Empty)
            {
                lblErrorDelivered.Text = "The Reason can't be empty";
                return;
            }

            if (rejectType == string.Empty)
            {
                lblErrorDelivered.Text = "The Reject Type can't be empty";
                return;
            }

            if (qtyr.ToString() == string.Empty)
            {
                lblErrorDelivered.Text = "The quantity can't be empty";
                return;
            }

            var consultaMaquina = _idaltticol011.findRecordByPdno(ref pdno, ref strError);

            if (consultaMaquina.Rows.Count > 0)
            {
                machine = consultaMaquina.Rows[0]["MCNO"].ToString().Trim();
            }

          //  var registroItem = _consultaInformacionPedido.AsEnumerable().Where(x => x["SITM"].ToString().Trim() == item).FirstOrDefault();

          //  var pono = registroItem["PONO"].ToString();

           // txtpono.Value = pono;
           // var registroItem = _consultaInformacionPedido.AsEnumerable().Where(x => x["SITM"].ToString().Trim() == item).FirstOrDefault();
            var kltc = txtkltc.Value;
            var pono = txtpono.Value;

            if (kltc == "1")
            {
                Ent_twhltc100 data100 = new Ent_twhltc100() { item = item, clot = clot };
                var validaLote = _idaltwhltc100.listaRegistro_Clot(ref data100, ref strError);

                if (validaLote.Rows.Count < 0)
                {
                    lblError.Text = mensajes("lotnotexists");
                    return;
                }
            }

            var consultaSecuencia = _idaltticol100.findMaxSeqnByPdnoPono(ref pdno, ref pono, ref strError);

            if (consultaSecuencia.Rows.Count > 0)
            {
                seqn = Convert.ToInt32(consultaSecuencia.Rows[0]["SEQN"]) + 1;
            }

            Ent_tticol100 dataticol100 = new Ent_tticol100()
            {
                //dsca = .Text,
                pdno = pdno,
                pono = Convert.ToInt32(pono),
                seqn = seqn,
                mcno = machine,
                item = item,
                qtyr = qtyr,
                shif = txtShift.Text.Trim().ToUpper(),
                cdis = slReason.SelectedValue.Trim().ToUpper(),
                rejt = Convert.ToInt32(slRejectType.SelectedValue),
                clot = slLot.Text == "" ? " " : slLot.Text.Trim().ToUpper(),
                obse = txtExactReasons.InnerText,
                logr = _operator,
                disp = 4,
                proc = 1,
                Unit = txtUnit.Text,
                logn = _operator,
                mess = "0",
                cwar = cwar,
                paid = paid

            };

            var validaInsert = _idaltticol100.insertRecord(ref dataticol100, ref strError);

            if (validaInsert > 0)
            {

                 validUpdate = _idaltticol100.ActualizaRegistro_ticol022(ref dataticol100, ref strError);

                //update actual warehouse field on table ticol222 to MRB Warehouse: Delivered
                 tableNameSave = Session["TableNameSave"].ToString();
                 if (tableNameSave == "ticol022")
                 {
                     validUpdate = _idaltticol100.ActualUpdateWarehouse_ticol222(ref dataticol100, ref strError);
                 }
                 else
                 {
                     validUpdate = _idaltticol100.ActualUpdateWarehouse_whcol131(ref dataticol100, ref strError);
                 }

                lblErrorDelivered.Text = String.Empty;
                lblConfirmDelivered.Text = mensajes("msjsave");
                divTableDelivered.Visible = false;
                divBtnGuardarDelivered.Visible = false;
                txtPalletId.Text = String.Empty;
                TxtOrder.Text = string.Empty;
                MakeLabelDelivered(dataticol100);
                divLabelDelivered.Visible = true;
                divBotonesDelivered.Visible = true;
            }
            else
            {
                lblErrorDelivered.Text = mensajes("errorsave");
                return;
            }
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
        public class Reason
        {
            public string cdis { get; set; }
            public string dsca { get; set; }
        };

        protected void MakeLabelDelivered(Ent_tticol100 Objtticol100)
        {

            var sqnb = Objtticol100.pdno + "-" + Objtticol100.pono + "-" + Objtticol100.proc;
            var mitm = Objtticol100.item;
            var dsca = txtDescription.Text;
            var qtyr = Objtticol100.qtyr + "-" + Objtticol100.Unit;
            var pdno = Objtticol100.pdno;
            var clot = Objtticol100.clot;

            //T$SQNB
            lblDmtNumberDelivered.Text = "DMT-NUMBER";
            Label1Delivered.Text = "Description";
            lblRejectedDelivered.Text = "Rejected Qty";
            Label11Delivered.Text = "WorkOrder";
            lblDescLot.Text = "Lot";
            //Label2.Text = "Disposition - Review";
            lblPrintedByDelivered.Text = "Printed By";
            lblReason.Text = "Reason";
            lblFechaDelivered.Text = "Date";
            lblCommentsDelivered.Text = "Comments";
            Label3Delivered.Text = "Reason";
            lblValuePrintedByDelivered.Text = Objtticol100.logr;
            lblDscaDelivered.Text = qtyr;
            LblSqnbDElivered.Text = sqnb;
            lblValueReasonDelivered.Text = slReason.SelectedItem.Text.Substring(7);


            var rutaLot = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + clot.ToUpper() + "&code=Code128&dpi=96";
            imgBCClot.Src = string.IsNullOrWhiteSpace(clot) ? string.Empty : rutaLot;

            var rutaSqnb = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + sqnb + "&code=Code128&dpi=96";
            imgCBSqnbDelivered.Src = !string.IsNullOrEmpty(sqnb) ? rutaSqnb : String.Empty;
            //T$MITM
            var rutaMitm = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + mitm.ToUpper() + "&code=Code128&dpi=96";
            imgCBMitmDelivered.Src = !string.IsNullOrEmpty(mitm) ? rutaMitm : String.Empty;
            //T$DSCA
            lblDsca.Text = dsca;
            lblDscaDelivered.Text = dsca;
            //T$QTDL
            lblQtdlDelivered.Text = Convert.ToString(qtyr);
            var rutaQtdl = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + Convert.ToString(Objtticol100.qtyr).ToUpper() + "&code=Code128&dpi=96";
            imgBCQtdlDelivered.Src = !string.IsNullOrEmpty(Convert.ToString(Objtticol100.qtyr)) ? rutaQtdl : String.Empty;
            //T$PDNO
            lblPdnoDelivered.Text = pdno;
            var rutaPdno = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + pdno.ToUpper() + "&code=Code128&dpi=96";
            imgBCPdnoDelivered.Src = !string.IsNullOrEmpty(pdno) ? rutaPdno : String.Empty;

            lblMachinetitle.Text = "Machine";
            lblMachine.Text = Objtticol100.mcno;
            lblValueCommentsDelivered.Text = txtExactReasons.InnerText;
        }

        #endregion

    }
}