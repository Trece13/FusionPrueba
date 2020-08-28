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
using System.Text;
using System.Data;
using System.Web.Configuration;

namespace whusap.WebPages.Migration
{
    public partial class whInvLabelPalletTagsPartial : System.Web.UI.Page
    {
        #region Propiedades
        public string WorkorderhasnotbeeninitiatedPOP = string.Empty;
        private static InterfazDAL_ttccol301 _idalttccol301 = new InterfazDAL_ttccol301();
        private static InterfazDAL_ttisfc001 _idalttisfc001 = new InterfazDAL_ttisfc001();
        private static InterfazDAL_tticol022 _idaltticol022 = new InterfazDAL_tticol022();
        private static InterfazDAL_tticol020 _idaltticol020 = new InterfazDAL_tticol020();
        private static DataTable _consultaInformacionOrden = new DataTable();
        private static Mensajes _mensajesForm = new Mensajes();
        private static LabelsText _textoLabels = new LabelsText();
        private static string _operator;
        public static string _idioma;
        private static string strError;
        private static string formName;
        private static bool _procesoAutomatico = Convert.ToBoolean(ConfigurationManager.AppSettings["anuncioAutomatico"].ToString());
        private static string globalMessages = "GlobalMessages";
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
                    come = mensajes("encabezado"),
                    refcntd = 0,
                    refcntu = 0
                };

                List<Ent_ttccol301> datalog = new List<Ent_ttccol301>();
                datalog.Add(data);

                _idalttccol301.insertarRegistro(ref datalog, ref strError);

                if (_procesoAutomatico)
                {
                    lblInfo.Text = mensajes("automaticannounced");
                }
            }
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            lblError.Text = String.Empty;
            lblConfirm.Text = String.Empty;

            if (Convert.ToInt32(txtQuantity.Text.Trim()) == 0)
            {
                lblError.Text = mensajes("amountentered");
                return;
            }

            divTable.Visible = false;
            divBotones.Visible = false;
            var PDNO = txtOrder.Text.Trim().ToUpper();
            var DELE = "2";
            var consultaOrden = _idalttisfc001.findByOrderNumberPalletTags(ref PDNO, ref strError).Rows;
            var qtdlzero = "";
            var enterQuantity = double.Parse(txtQuantity.Text.Trim(), CultureInfo.InvariantCulture.NumberFormat);

            if (consultaOrden.Count > 0)
            {
                if (consultaOrden[0]["STAT"].ToString().Trim() != "2")
                {
                    lblError.Text = WorkorderhasnotbeeninitiatedPOP;
                    return;
                }

                var madein = consultaOrden[0]["NAME"].ToString().ToUpper();
                var item = consultaOrden[0]["MITM"].ToString();
                var descripcion = consultaOrden[0]["DSCA"].ToString();
                var unidad = consultaOrden[0]["CUNI"].ToString();
                var maquina = consultaOrden[0]["MCNO"].ToString();
                var factor = consultaOrden[0]["CONV"].ToString();
                var qtyord = double.Parse(consultaOrden[0]["QRDR"].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                var qtyordo = double.Parse(consultaOrden[0]["QRDR"].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                var qtyanu = double.Parse(consultaOrden[0]["QDLV"].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                var qtyann = double.Parse(consultaOrden[0]["QTYANN"].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                var qtyreh = double.Parse(consultaOrden[0]["QTYREH"].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                var qtypconf = double.Parse(consultaOrden[0]["QTYPCONF"].ToString(), CultureInfo.InvariantCulture.NumberFormat);

                double factorD = double.Parse(factor, CultureInfo.InvariantCulture.NumberFormat);

                factor = factor.Trim() == String.Empty ? "1" : factor;

                //qtyord = ((qtyord - qtyreh) * Convert.ToDouble(ConfigurationManager.AppSettings["calcLabelPalletTag"].ToString())) - qtyanu - qtyann;
                qtyord = qtyord * Convert.ToDouble(ConfigurationManager.AppSettings["calcLabelPalletTag"].ToString());
                qtyord = qtyord == 0 ? 1 : qtyord;

                var qtyordpend = (qtyord * Convert.ToDouble(ConfigurationManager.AppSettings["calcLabelPalletTag"].ToString())) / double.Parse(factor, CultureInfo.InvariantCulture.NumberFormat);
                qtyordpend = qtyordpend > Convert.ToInt32(qtyordpend) ? Convert.ToInt32(qtyordpend) + 1 : qtyordpend;
                
                if (((qtyanu + qtypconf + double.Parse(factor) + qtyann) - qtyreh) > qtyordo)
                {
                    lblError.Text = String.Format(mensajes("palletoverann"), qtyord);
                    return;
                }

                if (enterQuantity > double.Parse(factor, CultureInfo.InvariantCulture.NumberFormat))
                {
                    lblError.Text = mensajes("amountfullpallet");
                    return;
                }
                
                if (enterQuantity > qtyord)
                {
                    lblError.Text = String.Format(mensajes("quantityord"), Math.Truncate(qtyord));
                    return;
                }             

                var numeroOrdenes = _idaltticol022.countRecordsByPdnoAndDele(ref PDNO, ref DELE, ref strError);
                numeroOrdenes = numeroOrdenes > 0 ? numeroOrdenes++ : numeroOrdenes;

                var consultaSecuencia = _idaltticol022.selectMaxSqnbByPdno(ref PDNO, ref qtdlzero, ref strError).Rows;

                var secuencia = "";
                var palletNumber = "";
                var sqnb = "";

                if (consultaSecuencia.Count > 0)
                {
                    var palletActual = consultaSecuencia[0]["SQNB"].ToString().Trim();
                    secuencia = palletActual.Substring(10, 3);
                    var dele = consultaSecuencia[0]["DELE"].ToString();
                    var fecha = consultaSecuencia[0]["FECHA"].ToString();

                    //if (numeroOrdenes > qtyord)
                    //{
                    //    lblError.Text = String.Format(mensajes("palletexceed"), qtyord);
                    //    return;
                    //}

                    var validaSecuencia = Convert.ToInt32(secuencia)+1;

                    if (validaSecuencia < 10)
                    {
                        secuencia = String.Concat("00", validaSecuencia);
                    }
                    else if (validaSecuencia > 9 && validaSecuencia < 99)
                    {
                        secuencia = String.Concat("0", validaSecuencia);
                    }
                    else
                    {
                        secuencia = (validaSecuencia).ToString();
                    }

                    palletNumber = (validaSecuencia).ToString();
                    sqnb = String.Concat(PDNO, "-", secuencia);

                    var mbpl = _idaltticol022.consultambpl(ref strError).Rows[0]["MBPL"];//ConfigurationManager.AppSettings["MBPL"].ToString();

                    var tiempo = Convert.ToInt32(mbpl) * 60;

                    var validaPalletAnterior = _idaltticol022.selectDatesBySqnbPdno(ref PDNO, ref palletActual, ref strError).Rows;

                    if (validaPalletAnterior.Count > 0)
                    {
                        var qtdl = validaPalletAnterior[0]["QTDL"].ToString();
                        var dif_min = validaPalletAnterior[0]["DIF_MIN"].ToString();
                        var fec_hoy = validaPalletAnterior[0]["FEC_HOY"].ToString();
                        var fec_ant = validaPalletAnterior[0]["FEC_ANT"].ToString();

                        if (Convert.ToInt32(dif_min) <= (tiempo / 60))
                        {
                            lblError.Text = String.Format(mensajes("announcedago"), (tiempo / 60));
                            return;
                        }

                        if (Convert.ToInt32(qtdl) == 0)
                        {
                            qtdlzero = "true";
                            var validaRegistroQuantityZero = _idaltticol022.selectMaxSqnbByPdno(ref PDNO, ref qtdlzero, ref strError).Rows;

                            if (validaRegistroQuantityZero[0]["SQNB"].ToString() == String.Empty)
                            {
                                secuencia = "001";
                                palletNumber = "1";
                                sqnb = String.Concat(PDNO, "-" + secuencia);
                            }
                            else
                            {
                                lblError.Text = mensajes("previouspalette");
                                return;
                            }
                        }
                        else
                        {
                            Ent_tticol022 data022 = new Ent_tticol022()
                            {
                                pdno = PDNO,
                                sqnb = sqnb,
                                proc = 2,
                                logn = _operator,
                                mitm = item,
                                qtdl = 0,
                                cuni = unidad,
                                log1 = "NONE",
                                qtd1 = 0,
                                pro1 = 2,
                                log2 = "NONE",
                                qtd2 = 0,
                                pro2 = 2,
                                loca = " ",
                                norp = 1,
                                dele = 2,
                                logd = "NONE",
                                refcntd = 0,
                                refcntu = 0,
                                drpt = DateTime.Now,
                                urpt = " ",
                                acqt = 0,
                                cwaf = _idaltticol022.WharehouseTisfc001(PDNO, ref strError),
                                cwat = " ",
                                aclo = " ",
                                allo = 0
                            };

                            var validateSave = _idaltticol022.insertarRegistroSimple(ref data022, ref strError);
                            if (validateSave < 1)
                            {
                                lblError.Text = mensajes("errorsave");
                                lblConfirm.Text = string.Empty;

                                return;
                            }
                            else
                            {
                                var validateSaveTicol222 = _idaltticol022.InsertarRegistroTicol222(ref data022, ref strError);
                                if (validateSaveTicol222 < 1)
                                {
                                    lblError.Text = mensajes("errorsave Ticol222");
                                    lblConfirm.Text = string.Empty;

                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        lblError.Text = mensajes("notfoundpallet");
                        lblConfirm.Text = string.Empty;

                        return;
                    }
                }
                else
                {
                    secuencia = "001";
                    palletNumber = "1";
                    sqnb = String.Concat(PDNO, "-", secuencia);

                    Ent_tticol022 data022 = new Ent_tticol022()
                    {
                        pdno = PDNO,
                        sqnb = sqnb,
                        proc = 2,
                        logn = _operator,
                        mitm = item,
                        qtdl = 0,
                        cuni = unidad,
                        log1 = "NONE",
                        qtd1 = 0,
                        pro1 = 2,
                        log2 = "NONE",
                        qtd2 = 0,
                        pro2 = 2,
                        loca = " ",
                        norp = 1,
                        dele = 2,
                        logd = "NONE",
                        refcntd = 0,
                        refcntu = 0,
                        drpt = DateTime.Now,
                        urpt = " ",
                        acqt = 0,
                        cwaf = _idaltticol022.WharehouseTisfc001(PDNO, ref strError),
                        cwat = " ",
                        aclo = " ",
                        allo = 0
                    };

                    var validateSave = _idaltticol022.insertarRegistroSimple(ref data022, ref strError);
                    if (validateSave < 1)
                    {
                        lblError.Text = mensajes("errorsave");
                        lblConfirm.Text = string.Empty;

                        return;
                    }
                    else
                    {
                        var validateSaveTicol222 = _idaltticol022.InsertarRegistroTicol222(ref data022, ref strError);
                        if (validateSaveTicol222 < 1)
                        {
                            lblError.Text = mensajes("errorsave Ticol222");
                            lblConfirm.Text = string.Empty;

                            return;
                        }
                    }
                }

                //Codigo barras item
                var rutaServItem = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + item.Trim().ToUpper() + "&code=Code128&dpi=96";
                imgCodeItem.Src = !string.IsNullOrEmpty(item) ? rutaServItem : "";

                //Codigo barras sqnb
                var rutaServSqnb = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + sqnb.Trim().ToUpper() + "&code=Code128&dpi=96";
                imgCodeSqnb.Src = !string.IsNullOrEmpty(sqnb) ? rutaServSqnb : "";

                lblDescItem.Text = descripcion;
                lblItem.Text = item.Trim().ToUpper();
                lblValueWorkOrder.Text = PDNO;
                lblValuePalletNumber.Text = palletNumber;
                lblValueDate.Text = DateTime.Now.ToString();
                lblValueShift.Text = "A,B,C,D";
                lblValueCasePerPallet.Text = enterQuantity.ToString();
                lblValueMadeIn.Text = madein;

                divTable.Visible = true;
                divBotones.Visible = true;

                lblError.Text = String.Empty;

                Session["SqnbAnuncioAutomatico"] = sqnb;
                Session["cantidadAnunPalletTag"] = enterQuantity;

                if (_procesoAutomatico)
                {
                    tdBtnExit.Visible = false;
                    divTable.Visible = true;

                    var validaAnuncio = ConsultaAnuncioAutomatico(sqnb, PDNO, enterQuantity.ToString());

                    if (!validaAnuncio)
                    {
                        lblInfo.Text = mensajes("errorannouncement");
                        divBotones.Visible = true;
                        tdBtnExit.Visible = true;
                    }
                }
            }
            else
            {
                lblError.Text = mensajes("ordernotexists");
                lblConfirm.Text = string.Empty;

                return;
            }
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("whInvAnuncioOrd.aspx?tipoFormulario=quantity");
        }

        #endregion

        #region Metodos

        protected bool ConsultaAnuncioAutomatico(string sqnb, string pdno, string quantannounced)
        {
            var SQNB = sqnb;
            var PDNO = pdno;
            var QTDL = "0";

            _consultaInformacionOrden = _idalttisfc001.findByOrderNumberAnuncioOrd(ref PDNO, ref strError);

            if (_consultaInformacionOrden.Rows.Count > 0)
            {
                var unidad = _consultaInformacionOrden.Rows[0]["CUNI"].ToString();
                var factor = _consultaInformacionOrden.Rows[0]["CONV"].ToString();
                var almacen = _consultaInformacionOrden.Rows[0]["CWAR"].ToString();
                var descalmacen = _consultaInformacionOrden.Rows[0]["DSCACWAR"].ToString();
                var osta = Convert.ToUInt32(_consultaInformacionOrden.Rows[0]["OSTA"].ToString());

                if (unidad.Trim() != "Kg")
                {
                    if (factor.Trim() == String.Empty)
                    {
                        lblError.Text = mensajes("notfactor");
                        lblConfirm.Text = string.Empty;

                        return false;
                    }
                }

                factor = Convert.ToDecimal(factor).ToString("0.000");

                if (osta != 5 && osta != 7)
                {
                    lblError.Text = mensajes("orderinactive");
                    lblConfirm.Text = string.Empty;

                    return false;
                }

                return GuardaAnuncioAutomatico(sqnb, pdno, quantannounced);
            }
            else
            {
                lblError.Text = mensajes("ordernotexists");
                lblConfirm.Text = string.Empty;

                return false;
            }
        }

        protected bool GuardaAnuncioAutomatico(string _sqnb, string _pdno, string quantannounced)
        {
            var item = _consultaInformacionOrden.Rows[0]["MITM"].ToString();
            var descripcion = _consultaInformacionOrden.Rows[0]["DSCAITEM"].ToString();
            var unidad = _consultaInformacionOrden.Rows[0]["CUNI"].ToString();
            var pdno = _pdno;
            var sqnb = _sqnb;
            var qrdr = _consultaInformacionOrden.Rows[0]["QRDR"].ToString();
            var qtyord = Convert.ToDecimal(qrdr.Trim() == String.Empty ? 0.000 : Convert.ToDouble(qrdr) * Convert.ToDouble(ConfigurationManager.AppSettings["calcLabelPalletTag"].ToString()));
            var qtyrec = Convert.ToDecimal(_consultaInformacionOrden.Rows[0]["QTYREC"].ToString());
            decimal enterqty = Convert.ToDecimal(quantannounced);

            if ((qtyrec + Convert.ToDecimal(enterqty)) > qtyord)
            {
                lblError.Text = String.Format(mensajes("exceedquantity"), (qtyrec + enterqty), qtyord);
                lblConfirm.Text = string.Empty;

                return false;
            }

            if (Convert.ToDecimal(enterqty) <= 0)
            {
                lblError.Text = mensajes("quantzero");
                lblConfirm.Text = string.Empty;

                return false;
            }

            var validarRegistro = _idaltticol022.selectDatesBySqnbPdno(ref pdno, ref sqnb, ref strError);

            if (validarRegistro.Rows.Count > 0)
            {
                Ent_tticol022 data022 = new Ent_tticol022()
                {
                    qtdl = Convert.ToDecimal(enterqty),
                    pdno = pdno,
                    sqnb = sqnb,
                    acqt = Convert.ToDecimal(enterqty)
                   
                };

                var validaUpdate = _idaltticol022.actualizaRegistroAnuncioOrd(ref data022, ref strError);
                var validaUpdateCant = _idaltticol022.ActualizarCantidadRegistroTicol222(Convert.ToDecimal(data022.acqt),data022.sqnb);
                if (validaUpdate)
                {
                    var dsca = descripcion;
                    var cuni = unidad;

                    Ent_tticol020 data020 = new Ent_tticol020()
                    {
                        pdno = pdno,
                        mitm = item.Trim(),
                        dsca = dsca,
                        qtdl = enterqty,
                        cuni = cuni,
                        mess = " ",
                        user = _operator,
                        refcntd = 0,
                        refcntu = 0
                    };

                    List<Ent_tticol020> list = new List<Ent_tticol020>();
                    list.Add(data020);

                    var validaSave = _idaltticol020.insertarRegistro(ref list, ref strError);

                    if (validaSave > 0)
                    {
                        lblConfirm.Text = mensajes("msjsave");
                        lblError.Text = string.Empty;
                        return true;
                    }
                    else
                    {
                        lblError.Text = mensajes("errorsave");
                        lblConfirm.Text = string.Empty;

                        return false;
                    }
                }
                else
                {
                    lblError.Text = mensajes("erroupdt");
                    lblConfirm.Text = string.Empty;

                    return false;
                }
            }
            else
            {
                lblError.Text = mensajes("orderproccesed");
                lblConfirm.Text = string.Empty;

                return false;
            }
        }

        protected void CargarIdioma()
        {
            WorkorderhasnotbeeninitiatedPOP = _textoLabels.readStatement(formName, _idioma, "WorkorderhasnotbeeninitiatedPOP");
            lblOrder.Text = _textoLabels.readStatement(formName, _idioma, "lblOrder");
            lblQuantity.Text = _textoLabels.readStatement(formName, _idioma, "lblQuantity");
            btnConsultar.Text = _textoLabels.readStatement(formName, _idioma, "btnConsultar");
            lblMadeIn.Text = _textoLabels.readStatement(formName, _idioma, "lblMadeIn");
            lblWorkOrder.Text = _textoLabels.readStatement(formName, _idioma, "lblWorkOrder");
            lblPalletNumber.Text = _textoLabels.readStatement(formName, _idioma, "lblPalletNumber");
            lblInspectorInitial.Text = _textoLabels.readStatement(formName, _idioma, "lblInspectorInitial");
            lblDate.Text = _textoLabels.readStatement(formName, _idioma, "lblDate");
            lblShift.Text = _textoLabels.readStatement(formName, _idioma, "lblShift");
            lblCasePerPallet.Text = _textoLabels.readStatement(formName, _idioma, "lblCasePerPallet");
            linkPrint.InnerText = _textoLabels.readStatement(formName, _idioma, "linkPrint");
            btnSalir.Text = _textoLabels.readStatement(formName, _idioma, "btnExit");
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
    }
}