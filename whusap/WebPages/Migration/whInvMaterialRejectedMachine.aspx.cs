using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using whusa.Interfases;
using whusa.Utilidades;
using System.Threading;
using System.Configuration;
using whusa.Entidades;
using System.Globalization;
using System.Data;
using System.Web.Configuration;

namespace whusap.WebPages.Migration
{
    public partial class whInvMaterialRejected : System.Web.UI.Page
    {
        #region Propiedades

        public string TheItemcantbeempty = string.Empty;
        public string TheReasoncantbeempty = string.Empty;
        public string TheRejectTypecantbeempty = string.Empty;
        public string TheCantcantbeempty = string.Empty;
        public string Thelotcantbeempty = string.Empty;

        private static InterfazDAL_tticst001 _idaltticst001 = new InterfazDAL_tticst001();
        private static InterfazDAL_ttccol301 _idalttccol301 = new InterfazDAL_ttccol301();
        private static InterfazDAL_ttisfc001 _idalttisfc001 = new InterfazDAL_ttisfc001();
        private static InterfazDAL_ttcmcs005 _idalttcmcs005 = new InterfazDAL_ttcmcs005();
        private static InterfazDAL_tticol125 _idaltticol125 = new InterfazDAL_tticol125();
        private static InterfazDAL_twhltc100 _idaltwhltc100 = new InterfazDAL_twhltc100();
        private static InterfazDAL_tticol100 _idaltticol100 = new InterfazDAL_tticol100();
        private static InterfazDAL_tticol011 _idaltticol011 = new InterfazDAL_tticol011();
        private static Mensajes _mensajesForm = new Mensajes();
        private static LabelsText _textoLabels = new LabelsText();
        private static string _operator;
        public static string _idioma;
        private static string strError;
        private static string formName;
        private static string globalMessages = "GlobalMessages";
        private static DataTable _consultaPedido;
        private static DataTable _consultaInformacionPedido;
        public string UrlBaseBarcode = WebConfigurationManager.AppSettings["UrlBaseBarcode"].ToString();
        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
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
            divLabel.Visible = false;
            divBotones.Visible = false;

            slItems.Items.Clear();
            txtDescription.Text = string.Empty;
            txtQty.Text = string.Empty;
            txtUnit.Text = string.Empty;
            txtShift.Text = string.Empty;
            slReason.Items.Clear();
            slRejectType.Items.Clear();
            txtExactReasons.InnerText = string.Empty;


            var pdno = txtWorkOrder.Text.Trim().ToUpper();

            if (pdno != String.Empty)
            {
                _consultaPedido = _idaltticst001.findByPdno(ref pdno, ref strError);

                if (_consultaPedido.Rows.Count > 0)
                {
                    _consultaInformacionPedido = _idalttisfc001.findByOrderMaterialRejected(ref pdno, ref strError);

                    if (_consultaInformacionPedido.Rows.Count > 0)
                    {
                        lblError.Text = "";
                        lblValueOrder.Text = _consultaPedido.Rows[0]["PDNO"].ToString();

                        ListItem itemSelect = new ListItem() { Text = _idioma == "INGLES" ? "--Select an option--" : "--Seleccione una opción--", Value = "" };

                        slItems.Items.Insert(0, itemSelect);


                        foreach (DataRow item in _consultaInformacionPedido.Rows)
                        {
                            ListItem itemPedido = new ListItem()
                            {
                                Text = String.Concat(item["SITM"].ToString().Trim(), " - ", item["DSCA"].ToString().Trim(), " - ", item["CANTIDAD"].ToString().Trim()),
                                Value = item["SITM"].ToString().Trim()
                            };

                            slItems.Items.Insert(slItems.Items.Count, itemPedido);
                        }

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

                        //<select id='{0}' name='{0}' class='TextBoxBig'><option value='1'>Supplied</option><option value='2'>Internal</option><option value='3'>Return</option></select>

                        ListItem supplied = new ListItem() { Text = "Supplied", Value = "1" };
                        ListItem intern = new ListItem() { Text = "Internal", Value = "2" };
                        ListItem retur = new ListItem() { Text = "Return", Value = "3" };

                        slRejectType.Items.Insert(slRejectType.Items.Count, supplied);
                        slRejectType.Items.Insert(slRejectType.Items.Count, intern);
                        slRejectType.Items.Insert(slRejectType.Items.Count, retur);

                        divTable.Visible = true;
                        divBtnGuardar.Visible = true;
                    }
                    else
                    {
                        lblError.Text = mensajes("notitems");
                        divTable.Visible = true;
                        divBtnGuardar.Visible = true;
                        return;
                    }
                }
                else
                {
                    lblError.Text = mensajes("ordernotexists");
                    return;
                }
            }
            else
            {
                lblError.Text = mensajes("formempty");
                return;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            var pdno = txtWorkOrder.Text.Trim().ToUpper();
            var machine = " ";
            var seqn = 1;
            var item = slItems.SelectedValue.Trim().ToUpper();
            var clot = slLot.SelectedValue.Trim().ToUpper();
            var reason = slReason.SelectedValue.Trim().ToUpper();
            var rejectType = slRejectType.SelectedValue.Trim().ToUpper();
            var qtyr = double.Parse(txtQty.Text.Trim(), CultureInfo.InvariantCulture.NumberFormat);

            if (item == string.Empty)
            {

                lblError.Text = TheItemcantbeempty;
                return;
            }

            if (reason == string.Empty)
            {

                lblError.Text = TheReasoncantbeempty;
                return;
            }

            if (rejectType == string.Empty)
            {

                lblError.Text = TheRejectTypecantbeempty;
                return;
            }

            if (qtyr.ToString() == string.Empty)
            {

                lblError.Text = TheCantcantbeempty;
                return;
            }

            var consultaMaquina = _idaltticol011.findRecordByPdno(ref pdno, ref strError);

            if (consultaMaquina.Rows.Count > 0)
            {
                machine = consultaMaquina.Rows[0]["MCNO"].ToString().Trim();
            }

            var registroItem = _consultaInformacionPedido.AsEnumerable().Where(x => x["SITM"].ToString().Trim() == item).FirstOrDefault();
            var kltc = registroItem["KLTC"].ToString();
            var pono = registroItem["PONO"].ToString();

            if (kltc == "1")
            {
                if (clot == string.Empty)
                {

                    lblError.Text = Thelotcantbeempty;
                    return;
                }

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
                clot = slLot.SelectedValue == "" ? " " : slLot.SelectedValue.Trim().ToUpper(),
                obse = txtExactReasons.InnerText,
                logr = _operator,
                disp = 4,
                proc = 1,
                Unit = txtUnit.Text,
                logn = _operator,
                mess = "0"

            };

            var validaInsert = _idaltticol100.insertRecord(ref dataticol100, ref strError);

            if (validaInsert > 0)
            {

                lblError.Text = String.Empty;
                lblConfirm.Text = mensajes("msjsave");
                divTable.Visible = false;
                divBtnGuardar.Visible = false;
                txtWorkOrder.Text = String.Empty;
                MakeLabel(dataticol100);
                divLabel.Visible = true;
                divBotones.Visible = true;
            }
            else
            {
                lblError.Text = mensajes("errorsave");
                return;
            }
        }

        protected void slItems_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var pdno = txtWorkOrder.Text.Trim().ToUpper();
            var item = slItems.SelectedValue.Trim().ToUpper();
            if (slItems.SelectedValue.Trim() != String.Empty)
            {
                slLot.Items.Clear();
                var lotes = _idaltticol125.consultaPorOrnoItem(ref pdno, ref item, ref strError);

                ListItem itemSeleccione = new ListItem() { Text = _idioma == "INGLES" ? "--Select an option--" : "--Seleccione una opción--", Value = "" };

                slLot.Items.Insert(0, itemSeleccione);

                if (lotes.Rows.Count > 0)
                {
                    foreach (DataRow itemLista in lotes.Rows)
                    {
                        if (itemLista["CLOT"] != null)
                        {
                            ListItem itemLote = new ListItem()
                            {
                                Value = itemLista["CLOT"].ToString(),
                                Text = itemLista["CLOT"].ToString()
                            };

                            slLot.Items.Insert(slLot.Items.Count, itemLote);
                        }
                    }
                }

                var registro = _consultaInformacionPedido.AsEnumerable().Where(x => x["SITM"].ToString().Trim() == item).FirstOrDefault();

                txtDescription.Text = registro["DSCA"].ToString().Trim();
                txtUnit.Text = registro["CUNI"].ToString().Trim();
                hdfQuantity.Value = registro["CANTIDAD"].ToString().Trim();
            }
            else
            {
                txtDescription.Text = String.Empty;
                txtUnit.Text = String.Empty;
                txtQty.Text = String.Empty;
                txtShift.Text = String.Empty;
                slLot.Items.Clear();
            }
        }

        #endregion

        #region Metodos
        protected void MakeLabel(Ent_tticol100 Objtticol100)
        {

            var sqnb = Objtticol100.pdno + "-" + Objtticol100.pono + "-" + Objtticol100.proc;
            var mitm = Objtticol100.item;
            var dsca = txtDescription.Text;
            var qtyr = Objtticol100.qtyr + "-" + Objtticol100.Unit;
            var pdno = Objtticol100.pdno;
            var clot = Objtticol100.clot;

            //T$SQNB
            lblDmtNumber.Text = "DMT-NUMBER";
            Label1.Text = "Description";
            lblRejected.Text = "Rejected Qty";
            Label11.Text = "WorkOrder";
            lblDescLot.Text = "Lot";
            //Label2.Text = "Disposition - Review";
            lblPrintedBy.Text = "Printed By";
            lblReason.Text = "Reason";
            lblFecha.Text = "Date";
            lblComments.Text = "Comments";
            Label3.Text = "Reason";
            lblValuePrintedBy.Text = Objtticol100.logr;
            lblDsca.Text = qtyr;
            LblSqnb.Text = sqnb;
            lblValueReason.Text = slReason.SelectedItem.Text.Substring(7);


            var rutaLot = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + clot.ToUpper() + "&code=Code128&dpi=96";
            imgBCClot.Src = string.IsNullOrWhiteSpace(clot) ? string.Empty : rutaLot;

            var rutaSqnb = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + sqnb + "&code=Code128&dpi=96";
            imgCBSqnb.Src = !string.IsNullOrEmpty(sqnb) ? rutaSqnb : String.Empty;
            //T$MITM
            var rutaMitm = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + mitm.ToUpper() + "&code=Code128&dpi=96";
            imgCBMitm.Src = !string.IsNullOrEmpty(mitm) ? rutaMitm : String.Empty;
            //T$DSCA
            lblDsca.Text = dsca;
            //T$QTDL
            lblQtdl.Text = Convert.ToString(qtyr);
            var rutaQtdl = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + Convert.ToString(Objtticol100.qtyr).ToUpper() + "&code=Code128&dpi=96";
            imgBCQtdl.Src = !string.IsNullOrEmpty(Convert.ToString(Objtticol100.qtyr)) ? rutaQtdl : String.Empty;
            //T$PDNO
            lblPdno.Text = pdno;
            var rutaPdno = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + pdno.ToUpper() + "&code=Code128&dpi=96";
            imgBCPdno.Src = !string.IsNullOrEmpty(pdno) ? rutaPdno : String.Empty;

            lblMachinetitle.Text = "Machine";
            lblMachine.Text = Objtticol100.mcno;
            lblValueComments.Text = txtExactReasons.InnerText;
        }
        protected void CargarIdioma()
        {
            lblWorkOrder.Text = _textoLabels.readStatement(formName, _idioma, "lblWorkOrder");
            btnConsultar.Text = _textoLabels.readStatement(formName, _idioma, "btnConsultar");
            btnGuardar.Text = _textoLabels.readStatement(formName, _idioma, "btnGuardar");
            lblOrder.Text = _textoLabels.readStatement(formName, _idioma, "lblOrder");
            lblDescription.Text = _textoLabels.readStatement(formName, _idioma, "lblDescription");
            lblQty.Text = _textoLabels.readStatement(formName, _idioma, "lblQty");
            lblUnit.Text = _textoLabels.readStatement(formName, _idioma, "lblUnit");
            lblLot.Text = _textoLabels.readStatement(formName, _idioma, "lblLot");
            lblShift.Text = _textoLabels.readStatement(formName, _idioma, "lblShift");
            lblReason.Text = _textoLabels.readStatement(formName, _idioma, "lblReason");
            lblRejectedType.Text = _textoLabels.readStatement(formName, _idioma, "lblRejectedType");
            lblExactReason.Text = _textoLabels.readStatement(formName, _idioma, "lblExactReason");
            lblItem.Text = _textoLabels.readStatement(formName, _idioma, "lblItem");
            TheItemcantbeempty = mensajes("TheItemcantbeempty");
            TheReasoncantbeempty = mensajes("TheReasoncantbeempty");
            TheRejectTypecantbeempty = mensajes("TheRejectTypecantbeempty");
            TheCantcantbeempty = mensajes("TheCantcantbeempty");
            Thelotcantbeempty = mensajes("Thelotcantbeempty");
        }

        protected static string mensajes(string tipoMensaje)
        {
            string idioma = "INGLES";
            var retorno = _mensajesForm.readStatement("whInvMaterialRejected.aspx", idioma, ref tipoMensaje);

            if (retorno.Trim() == String.Empty)
            {
                retorno = _mensajesForm.readStatement(globalMessages, idioma, ref tipoMensaje);
            }

            return retorno;
        }

        #endregion
    }
}