using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using whusa.Interfases;
using whusa.Utilidades;
using System.Configuration;
using whusa.Entidades;
using System.Threading;
using System.Globalization;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.EnterpriseServices.Internal;
using System.Security.Cryptography.X509Certificates;
using System.Web.Services;
using iTextSharp.text.pdf;
using Org.BouncyCastle.Asn1;
using Newtonsoft.Json;
using System.Web.Configuration;

namespace whusap.WebPages.Migration
{
    public partial class whInvMaterialRejectedWarehouse : System.Web.UI.Page
    {
        #region Propiedades
        private static InterfazDAL_ttcibd001 _idalttcibd001 = new InterfazDAL_ttcibd001();
        private static InterfazDAL_ttcmcs003 _idalttcmcs003 = new InterfazDAL_ttcmcs003();
        private static InterfazDAL_twhwmd200 _idaltwhwmd200 = new InterfazDAL_twhwmd200();
        private static InterfazDAL_twhwmd300 _idaltwhwmd300 = new InterfazDAL_twhwmd300();
        private static InterfazDAL_twhltc100 _idaltwhltc100 = new InterfazDAL_twhltc100();
        private static InterfazDAL_twhinr140 _idaltwhinr140 = new InterfazDAL_twhinr140();
        private static InterfazDAL_ttcmcs005 _idalttcmcs005 = new InterfazDAL_ttcmcs005();
        private static InterfazDAL_tticol116 _idaltticol116 = new InterfazDAL_tticol116();
        private static InterfazDAL_ttccol301 _idalttccol301 = new InterfazDAL_ttccol301();
        private static Mensajes _mensajesForm = new Mensajes();
        private static LabelsText _textoLabels = new LabelsText();
        public string Thelotcantbeempty = string.Empty;
        private static string _operator;
        public static string _idioma;
        private static string strError;
        private static string formName;
        private static string globalMessages = "GlobalMessages";
        private static DataTable _validaItem;
        private static DataTable _validaWarehouse;
        private static DataTable _validaUbicacion;
        private static DataTable _validaItemLote;
        private static Decimal _stock;
        public string lotGlobal = string.Empty;
        public string UrlBaseBarcode = WebConfigurationManager.AppSettings["UrlBaseBarcode"].ToString();
        public static bool ManejolocalizacionAlmacen = true; 

        //Errores Labels

        public string lblitemError = string.Empty;         
        public string lblWarehouseError     = string.Empty;
        public string    lblLocationError   = string.Empty;
        public string lblLotError = string.Empty;

        public int CantidadDevuelta = 0; 

        public string LstItemsJSON = string.Empty;

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            LstItemsJSON = ListaItems();
            // Cambiar cultura para manejo de separador decimal
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-CO");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-CO");
            base.InitializeCulture();

            if (!IsPostBack)
            {
                formName = Request.Url.AbsoluteUri.Split('/').Last();
                if (formName.Contains('?'))
                {
                    formName = formName.Split('?')[0];
                }
                Label control = (Label)Page.Controls[0].FindControl("lblPageTitle");
                lblError.Text = "";
                lblConfirm.Text = "";

                if (Session["user"] == null)
                {
                    Response.Redirect(ConfigurationManager.AppSettings["UrlBase"] + "/WebPages/Login/whLogIni.aspx");
                }

                _operator = Session["user"].ToString();

                try
                {
                    _idioma = Session["ddlIdioma"].ToString();
                }
                catch (Exception)
                {
                    _idioma = "INGLES";
                }

                CargarIdioma();

                string strTitulo = mensajes("encabezado");
                control.Text = strTitulo;

                Ent_ttccol301 data = new Ent_ttccol301()
                {
                    user = _operator,
                    come = strTitulo,
                    refcntd = 0,
                    refcntu = 0
                };

                List<Ent_ttccol301> datalog = new List<Ent_ttccol301>();
                datalog.Add(data);

                _idalttccol301.insertarRegistro(ref datalog, ref strError);
            }
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            lblError.Text = String.Empty;
            var item = txtItem.Text.Trim().ToUpper();
            var warehouse = txtWarehouse.Text.Trim().ToUpper();
            var location = txtLocation.Text.Trim().ToUpper();
            var lot = txtLot.Text.Trim().ToUpper();

            if (string.IsNullOrEmpty(txtLot.Text.Trim().ToUpper())==true)
            {
                Session["Lot"] = string.Empty;
            }
            else
            {
                Session["Lot"] = txtLot.Text.Trim().ToUpper();
            }
            

            Ent_ttcibd001 data001 = new Ent_ttcibd001() { item = item };
            _validaItem = _idalttcibd001.listaRegistro_ObtieneDescripcionUnidad(ref data001, ref strError);

            if (_validaItem.Rows.Count < 1)
            {
                lblError.Text = mensajes("itemnotexists");
                return;
            }

            var kltc = Convert.ToInt32(_validaItem.Rows[0]["KLTC"].ToString());

            _validaWarehouse = _idalttcmcs003.findRecordByCwar(ref warehouse, ref strError);

            if (_validaWarehouse.Rows.Count < 1)
            {
                lblError.Text = mensajes("warehousenotexists");
                return;
            }

            Ent_twhwmd200 data200 = new Ent_twhwmd200() { cwar = warehouse };
            var manejoUbicacionAlmacen = Convert.ToInt32(_idaltwhwmd200.listaRegistro_ObtieneAlmacenLocation(ref data200, ref strError).Rows[0]["LOC"]);

            if (manejoUbicacionAlmacen == 1)
            {
                _validaUbicacion = _idaltwhwmd300.validateExistsLocation(ref location, ref warehouse, ref strError);

                if (_validaUbicacion.Rows.Count < 1)
                {
                    lblError.Text = mensajes("Location doesn't exist.");
                    return;
                }
            }

            if (kltc == 1)
            {
                Ent_twhltc100 data100 = new Ent_twhltc100() { item = item, clot = lot };
                _validaItemLote = _idaltwhltc100.listaRegistro_Clot(ref data100, ref strError);

                if (_validaItemLote.Rows.Count < 1)
                {
                    lblError.Text = mensajes("lotnotexists");
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
                lblError.Text = mensajes("notstock");
                return;
            }

            divBotones.Visible = false;
            divLabel.Visible = false;
            divBtnGuardar.Visible = true;
            makeTable();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
             lblError.Text = String.Empty;
            var item = txtItem.Text.Trim().ToUpper();
            var warehouse = txtWarehouse.Text.Trim().ToUpper();
            var location = txtLocation.Text.Trim().ToUpper();
            var lot = Session["Lot"].ToString().ToUpper();

            var cantidad = Convert.ToDouble(Request.Form["txtQuantity"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat);
            var cdis = Request.Form["slReasons"].ToString().Trim();
            var obse = Request.Form["txtComments"].ToString().Trim();
            var VariableAuxSuno = Request.Form["txtSupplier"].ToString().Trim();
            var suno = string.IsNullOrEmpty(VariableAuxSuno) ? string.Empty : Request.Form["txtSupplier"].ToString().Trim();
            var reasondesc = Request.Form["lblReasonDesc"];

            if (lot == string.Empty)
            {
                lblError.Text = Thelotcantbeempty;
                return;
            }

            if (reasondesc == null)
            {
                reasondesc = "Adhesion";
            }

            if (cantidad > Convert.ToDouble(_stock, CultureInfo.InvariantCulture.NumberFormat))
            {
                lblError.Text = mensajes("quantexceed");
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
                        lblError.Text = mensajes("bulklocation");
                        return;
                    }
                }
                else
                {
                    lblError.Text = mensajes("locationnotexists");
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
                    suno = suno == String.Empty ? " " : suno
                };
                var validInsertaux = _idaltticol116.insertarRegistro(ref data116, ref strError);

                if (validInsertaux > 0)
                {
                    lblError.Text = String.Empty;
                    lblConfirm.Text = mensajes("msjupdate");
                    divTable.InnerHtml = String.Empty;
                    divBtnGuardar.Visible = false;
                    txtItem.Text = String.Empty;
                    txtWarehouse.Text = String.Empty;
                    txtLocation.Text = String.Empty;
                    txtLot.Text = String.Empty;

                    var rutaServ = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + item.Trim().ToUpper() + "&code=Code128&dpi=96";
                    imgCodeItem.Src = !string.IsNullOrEmpty(item) ? rutaServ : "";

                    var rutaServQty = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + cantidad.ToString().Trim().ToUpper() + "&code=Code128&dpi=96";
                    imgQty.Src = !string.IsNullOrEmpty(cantidad.ToString()) ? rutaServQty : "";

                    lblValueDescription.Text = hdfDescItem.Value;
                    lblDescRejectQty.Text = String.Concat(_textoLabels.readStatement(formName, _idioma, "lblRejectedQty"), " ", cantidad);
                    lblDescPrintedBy.Text = String.Concat(_textoLabels.readStatement(formName, _idioma, "lblPrintedBy"), " ", _operator);
                    lblValueLot.Text = String.Concat(_textoLabels.readStatement(formName, _idioma, "lblDescLot"), " ", lot);
                    lblComments.Text = String.Concat(_textoLabels.readStatement(formName, _idioma, "lblDescComments"), " ", obse);
                    lblValueReason.Text = reasondesc;

                    divLabel.Visible = true;
                    divBotones.Visible = true;
                }
                else
                {
                    lblError.Text = mensajes("errorupdt");
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
                    suno = suno == String.Empty ? " " : suno
                };

                var validInsert = _idaltticol116.insertarRegistro(ref data116, ref strError);

                if (validInsert > 0)
                {
                    lblError.Text = String.Empty;
                    lblConfirm.Text = mensajes("msjsave");
                    divTable.InnerHtml = String.Empty;
                    divBtnGuardar.Visible = false;
                    txtItem.Text = String.Empty;
                    txtWarehouse.Text = String.Empty;
                    txtLocation.Text = String.Empty;
                    txtLot.Text = String.Empty;

                    var rutaServ = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + item.Trim().ToUpper() + "&code=Code128&dpi=96";
                    imgCodeItem.Src = !string.IsNullOrEmpty(item) ? rutaServ : "";

                    var rutaServQty = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + cantidad.ToString().Trim().ToUpper() + "&code=Code128&dpi=96";
                    imgQty.Src = !string.IsNullOrEmpty(cantidad.ToString()) ? rutaServQty : "";

                    lblValueDescription.Text = hdfDescItem.Value;
                    lblDescRejectQty.Text = String.Concat(_textoLabels.readStatement(formName, _idioma, "lblRejectedQty"), " ", cantidad);
                    lblDescPrintedBy.Text = String.Concat(_textoLabels.readStatement(formName, _idioma, "lblPrintedBy"), " ", _operator);
                    lblValueLot.Text = String.Concat(_textoLabels.readStatement(formName, _idioma, "lblDescLot"), " ", lot);
                    lblComments.Text = String.Concat(_textoLabels.readStatement(formName, _idioma, "lblDescComments"), " ", obse);
                    lblValueReason.Text = reasondesc;

                    divLabel.Visible = true;
                    divBotones.Visible = true;
                }
                else
                {
                    lblError.Text = mensajes("errorsave");
                    return;
                }
            }
        }

        protected void txtLocation_OnTextChanged(object sender, EventArgs e)
        {
            lblError.Text = String.Empty;
            var location = txtLocation.Text.Trim().ToUpper();

            if (location != String.Empty)
            {
                Ent_twhwmd300 data = new Ent_twhwmd300() { loca = location };
                var validaLocation = _idaltwhwmd300.listaRegistro_ObtieneAlmacen(ref data, ref strError);

                if (validaLocation.Rows.Count > 0)
                {
                    if (Convert.ToInt32(validaLocation.Rows[0]["LOCT"]) != 5)
                    {
                        lblError.Text = mensajes("bulklocation");
                        return;
                    }
                }
                else
                {
                    lblError.Text = mensajes("locationnotexists");
                    return;
                }
            }
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("whInvMaterialRejectedWarehouse.aspx");
        }

        protected void txtItem_OnTextChanged(object sender, EventArgs e)
        {
            lblError.Text = String.Empty;

            var item = txtItem.Text.Trim().ToUpper();

            if (item != String.Empty)
            {
                Ent_ttcibd001 data001 = new Ent_ttcibd001() { item = item };
                _validaItem = _idalttcibd001.listaRegistro_ObtieneDescripcionUnidad(ref data001, ref strError);

                if (_validaItem.Rows.Count < 1)
                {
                    lblError.Text = mensajes("itemnotexists");
                    return;
                }
            }

            var kltc = Convert.ToInt32(_validaItem.Rows[0]["KLTC"].ToString());

            if (kltc != 1)
            {
                txtLot.Text = String.Empty;
                txtLot.Enabled = false;
            }
        }

        #endregion

        #region Metodos

        protected void makeTable()
        {
            var table = String.Empty;
            CantidadDevuelta = _idalttcibd001.CantidadDevueltaStock(_validaItem.Rows[0]["ITEM"].ToString().Trim().ToUpper(), Session["Lot"].ToString().Trim().ToUpper(), _validaWarehouse.Rows[0]["CWAR"].ToString().Trim().ToUpper(),txtLocation.Text.ToUpper().Trim());
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
                , txtLocation.Text.Trim().ToUpper()
                , _validaUbicacion != null ? _validaUbicacion.Rows.Count < 1 ? String.Empty : _validaUbicacion.Rows[0]["DSCA"].ToString().Trim().ToUpper() : String.Empty
                , txtLot.Text.Trim().ToUpper()
                , "<textarea id='txtComments' name='txtComments' rows='6'></textarea>");

            hdfDescItem.Value = _validaItem.Rows[0]["DESCRIPCION"].ToString().Trim().ToUpper();

            table += String.Format("<tr style='font-weight:bold; background-color:white;'><td>{0}</td><td>{1}</td><td>{2}</td><td colspan='3'>{3}</td></tr>"
               , _idioma == "INGLES" ? "Quantity" : "Cantidad"
               , _idioma == "INGLES" ? "Reason" : "Razón"
               , _validaItem.Rows[0]["ITEM"].ToString().Trim().ToUpper().Substring(0, 1) == "B" ? (_idioma == "INGLES" ? "Supplier" : "Proveedor") : string.Empty
               , _validaItem.Rows[0]["ITEM"].ToString().Trim().ToUpper().Substring(0, 1) == "B" ? (_idioma == "INGLES" ? "Name Supplier" : "Nombre proveedor") : string.Empty);

            var rstp = "10";
            var listaReasons = _idalttcmcs005.findRecords(ref rstp, ref strError);

            var selectReasons = "<select id='slReasons' name='slReasons' class='TextboxBig' onchange='setReason(this);'>";

            foreach (DataRow item in listaReasons.Rows)
            {
                selectReasons += String.Format("<option value='{0}'>{1}</option>", item["CDIS"].ToString().Trim(), item["CDIS"].ToString().Trim() + " - " + item["DSCA"].ToString().Trim());
            }

            table += String.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td colspan='3'>{3}</td></tr>"
                , String.Format(
                    "<input type='number' min='1' pattern='^[0-9]+' step='any' id='txtQuantity' name='txtQuantity' class='TextboxBig' onchange='validarCantidad(this,{0}," + CantidadDevuelta+ ")' />",
                    _stock)
                , selectReasons
                , _validaItem.Rows[0]["ITEM"].ToString().Trim().ToUpper().Substring(0, 1) == "B"
                    ? (String.Format(
                        "<input type='text' id='txtSupplier' name='txtSupplier' value='{0}' class='TextboxBig' readonly='true'/>",
                        _validaItem.Rows[0]["OTBP"].ToString().Trim().ToUpper()))
                    : (String.Format(
                        "<input type='text' id='txtSupplier' name='txtSupplier' value='{0}' class='TextboxBig' readonly='true' style ='display:none'/>",
                        _validaItem.Rows[0]["OTBP"].ToString().Trim().ToUpper()))
                , _validaItem.Rows[0]["ITEM"].ToString().Trim().ToUpper().Substring(0, 1) == "B" ? (String.Format("<input type='text' id='txtNameSupplier' name='txtNameSupplier' value='{0}' class='TextboxBig' disabled />", _validaItem.Rows[0]["NAMA"].ToString().Trim().ToUpper())) : (String.Format("<input type='text' id='txtNameSupplier' name='txtNameSupplier' value='{0}' class='TextboxBig' disabled style ='display:none'/>", _validaItem.Rows[0]["NAMA"].ToString().Trim().ToUpper())));

            table += "</table></div>";

            divTable.InnerHtml = table;
            
        }

        protected void CargarIdioma()
        {
            Thelotcantbeempty = _textoLabels.readStatement(formName, _idioma, "Thelotcantbeempty");
            lblItem.Text = _textoLabels.readStatement(formName, _idioma, "lblItem");
            lblWarehouse.Text = _textoLabels.readStatement(formName, _idioma, "lblWarehouse");
            lblLocation.Text = _textoLabels.readStatement(formName, _idioma, "lblLocation");
            lblLot.Text = _textoLabels.readStatement(formName, _idioma, "lblLot");
            btnConsultar.Text = _textoLabels.readStatement(formName, _idioma, "btnConsultar");
            btnGuardar.Text = _textoLabels.readStatement(formName, _idioma, "btnGuardar");
            lblDefectiveMaterial.Text = _textoLabels.readStatement(formName, _idioma, "lblDefectiveMaterial");
            lblDescDescription.Text = _textoLabels.readStatement(formName, _idioma, "lblDescDescription");
            lblDescReason.Text = _textoLabels.readStatement(formName, _idioma, "lblDescReason");
            btnSalir.Text = _textoLabels.readStatement(formName, _idioma, "btnSalir");
            //btnSalir.InnerText = _textoLabels.readStatement(formName, _idioma, "linkPrint");
            //linkPrint.InnerText = _textoLabels.readStatement(formName, _idioma, "linkPrint");
            //linkPrint.Text = _textoLabels.readStatement(formName, _idioma, "linkPrint");
            lblDate.Text = _textoLabels.readStatement(formName, _idioma, "lblDate");

            lblitemError      =       _textoLabels.readStatement(formName, _idioma, "lblitemError");
            lblWarehouseError =       _textoLabels.readStatement(formName, _idioma, "lblWarehouseError");
            lblLocationError  =       _textoLabels.readStatement(formName, _idioma, "lblLocationError");
            lblLotError       =       _textoLabels.readStatement(formName, _idioma, "lblLotError");
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

        #endregion


        [WebMethod]
        public static string WareHouseLotesPorItem(string Item)
        {
            DataItem DataReturnItem = new DataItem();

            List<string> lstWarehouse = new List<string>();
            List<string> lstLotes = new List<string>();
            DataTable DtWareHouseLotesPorItem = _idalttcibd001.listaWarehouseLotePorItem(Item);
            if (DtWareHouseLotesPorItem.Rows.Count > 0)
            {

                foreach (DataRow row in DtWareHouseLotesPorItem.Rows)
                {
                    DataReturnItem.Item = row["ITEM"].ToString().Trim();
                    lstWarehouse.Add(row["CLOT"].ToString().Trim());
                    lstLotes.Add(row["CWAR"].ToString().Trim());
                }

                DataReturnItem.lstWarehouse = lstWarehouse;
                DataReturnItem.lstLotes = lstLotes;

            }
            return JsonConvert.SerializeObject(DtWareHouseLotesPorItem);
        }

        [WebMethod]
        public static string LocalizacionesPorWarehouses(string cwar)
        {
            List<string> LstLocation = new List<string>();
            DataTable DtLotesPorWarehouse = _idalttcibd001.listaLocalizacionesPorWarehouses(cwar);
            foreach (DataRow row in DtLotesPorWarehouse.Rows)
            {
                LstLocation.Add(row["LOCA"].ToString().Trim().ToUpper());
            }
            return JsonConvert.SerializeObject(LstLocation);
        }

        
        public string ListaItems()
        {
            
            DataTable DtItems = _idalttcibd001.ListaItems();
            int cont = 0;
            List<ItemEsct> LstItems = new List<ItemEsct>();
            foreach (DataRow row in DtItems.Rows)
            {
                ItemEsct objItem = new ItemEsct(); 
                objItem.Item = (row["ITEM"].ToString().Trim().ToUpper());
                objItem.Ktlc = (row["KLTC"].ToString().Trim().ToUpper());
                LstItems.Add(objItem);

            }
            return  JsonConvert.SerializeObject(LstItems);
        }

        [WebMethod]
        public static string ListaWarehouses()
        {
            List<Warehouse> LstWarehouses = new List<Warehouse>();
            DataTable DtWarehouses = _idalttcibd001.ListaWarehouses();
            foreach (DataRow row in DtWarehouses.Rows)
            {
                Warehouse objWare = new Warehouse();
                objWare.CWAR = row["CWAR"].ToString().Trim().ToUpper();
                objWare.SLOC = row["SLOC"].ToString().Trim().ToUpper();
                LstWarehouses.Add(objWare);
            }
            return JsonConvert.SerializeObject(LstWarehouses);
        }

        [WebMethod]
        public static string listaLotesPorItem(string Item)
        {
            List<string> LstLotesPorItem = new List<string>();
            DataTable DtLotesPorItem = _idalttcibd001.listaLotesPorItem(Item);
            foreach (DataRow row in DtLotesPorItem.Rows)
            {
                LstLotesPorItem.Add(row["CLOT"].ToString().Trim().ToUpper());
            }
            return JsonConvert.SerializeObject(LstLotesPorItem);
        }

    }

    public class ItemEsct
    {
        public ItemEsct()
        {
            this.Item = string.Empty;
            this.Ktlc = string.Empty;
        }
        public string Item { get; set; }
        public string Ktlc { get; set; }
    }

    public class DataItem
    {
        public DataItem()
        {
            this.Item = string.Empty;
        }
        public string Item { get; set; }
        public List<string> lstWarehouse { get; set; }
        public List<string> lstLotes { get; set; }
    }

    public class Warehouse
    {
        public Warehouse()
        {
            this.CWAR = String.Empty;
            this.SLOC = String.Empty;
        }
        public string CWAR { get; set; }
        public string SLOC { get; set; }
    }



}
