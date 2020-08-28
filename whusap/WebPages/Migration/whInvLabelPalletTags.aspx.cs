using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using whusa.Interfases;
using whusa.Utilidades;
using System.Threading;
using System.Globalization;
using System.Configuration;
using whusa.Entidades;
using System.Data;
using System.Web.Configuration;

namespace whusap.WebPages.Migration
{
    public partial class whInvLabelPalletTags : System.Web.UI.Page
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
        private static double calcAnuncioOrd = Convert.ToDouble(ConfigurationManager.AppSettings["calcAnuncioOrd"].ToString());
        private static string globalMessages = "GlobalMessages";
        public static string _tipoFormulario;
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

                if (Request.QueryString["tipoFormulario"] != null)
                {
                    _tipoFormulario = Request.QueryString["tipoFormulario"].ToString().ToUpper();
                }
                else
                {
                    _tipoFormulario = "PRINT";
                }

                if (_tipoFormulario == "REPRINT")
                {
                    trSecuence.Visible = true;
                }
                else
                {
                    trSecuence.Visible = false;
                }

                string strTitulo = _tipoFormulario == "PRINT" ? mensajes("encabezado") : mensajes("encabezadoreprint");
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

                if (_procesoAutomatico && _tipoFormulario != "REPRINT")
                {
                    lblInfo.Text = mensajes("automaticannounced");
                }
            }
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            divTable.Visible = false;
            divBotones.Visible = false;

            if (_tipoFormulario == "REPRINT")
            {
                formRePrintLabel();
                return;
            }

            var PDNO = txtOrder.Text.Trim().ToUpper();
            var DELE = "2";
            var consultaOrden = _idalttisfc001.findByOrderNumberPalletTags(ref PDNO, ref strError).Rows;
            var qtdlzero = "";

            if (consultaOrden.Count > 0)
            {
                if (consultaOrden[0]["STAT"].ToString().Trim() != "2")
                {
                  
                    lblError.Text = WorkorderhasnotbeeninitiatedPOP;
                    lblConfirm.Text = string.Empty;
                    return;
                }

                var madein = consultaOrden[0]["NAME"].ToString().ToUpper();
                var item = consultaOrden[0]["MITM"].ToString();
                var descripcion = consultaOrden[0]["DSCA"].ToString();
                var unidad = consultaOrden[0]["CUNI"].ToString();
                var maquina = consultaOrden[0]["MCNO"].ToString();
                var factor = consultaOrden[0]["CONV"].ToString();
                var qtyord = double.Parse(consultaOrden[0]["QRDR"].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                var qtyreh = double.Parse(consultaOrden[0]["QTYREH"].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                var qtyanu = double.Parse(consultaOrden[0]["QDLV"].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                var qtypconf = double.Parse(consultaOrden[0]["QTYPCONF"].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                var qtyann = double.Parse(consultaOrden[0]["QTYANN"].ToString(), CultureInfo.InvariantCulture.NumberFormat);

                factor = factor.Trim() == String.Empty ? "1" : factor;

                //qtyord = (qtyord * Convert.ToDouble(ConfigurationManager.AppSettings["calcLabelPalletTag"].ToString())) / double.Parse(factor, CultureInfo.InvariantCulture.NumberFormat);
                qtyord = qtyord * Convert.ToDouble(ConfigurationManager.AppSettings["calcLabelPalletTag"].ToString());
                qtyord = qtyord == 0 ? 1 : qtyord;

                if (((qtyanu + qtypconf + double.Parse(factor) + qtyann) - qtyreh) > qtyord)
                {
                    lblError.Text = String.Format(mensajes("palletoverann"), qtyord);
                    lblConfirm.Text = string.Empty;
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

                    if (numeroOrdenes + 1  > qtyord)
                    {
                        lblError.Text = String.Format(mensajes("palletexceed"), qtyord);
                        lblConfirm.Text = string.Empty;
                        return;
                    }

                    var validaSecuencia = Convert.ToInt32(secuencia)+1;

                    if (validaSecuencia < 10)
                    {
                        secuencia = String.Concat("00", validaSecuencia);
                    }
                    else if (validaSecuencia> 9 && validaSecuencia < 99)
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
                        //if (Convert.ToInt32(dif_min) <= (tiempo / 60))
                        if (Convert.ToInt32(dif_min) <= (tiempo / 60))
                        {
                            lblError.Text = String.Format(mensajes("announcedago"), (tiempo / 60));
                            lblConfirm.Text = string.Empty;
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
                                lblConfirm.Text = string.Empty;
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
                                acqt = _procesoAutomatico == true ? Convert.ToDecimal(factor) : 0,
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
                        acqt = 0,//_procesoAutomatico == true ? enterQuantity : 0,
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
                            return;
                        }
                    }

                }

                //Codigo barras item
                var rutaServ = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + item.Trim().ToUpper() + "&code=Code128&dpi=96";
                imgCodeItem.Src = !string.IsNullOrEmpty(item) ? rutaServ : "";

                //Codigo barras sqnb
                rutaServ = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + sqnb.Trim().ToUpper() + "&code=Code128&dpi=96";
                imgCodeSqnb.Src = !string.IsNullOrEmpty(sqnb) ? rutaServ : "";

                lblDescItem.Text = descripcion;
                lblItem.Text = item.Trim().ToUpper();
                lblValueWorkOrder.Text = PDNO;
                lblValuePalletNumber.Text = palletNumber;
                lblValueDate.Text = "Date";
                lblValueShift.Text = "A,B,C,D";
                lblValueCasePerPallet.Text = factor;
                lblValueMadeIn.Text = madein;

                divTable.Visible = true;
                divBotones.Visible = true;

                lblError.Text = String.Empty;

                Session["SqnbAnuncioAutomatico"] = sqnb;

                if (_procesoAutomatico)
                {
                    tdBtnExit.Visible = false;
                    divTable.Visible = true;


                    var validaAnuncio = ConsultaAnuncioAutomatico(sqnb, PDNO);

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
            Response.Redirect("whInvAnuncioOrd.aspx?tipoFormulario=pallet");
        }

        #endregion

        #region Metodos

        protected bool ConsultaAnuncioAutomatico(string sqnb, string pdno)
        {
            var SQNB = sqnb;
            var PDNO = pdno;
            var QTDL = "0";

            _consultaInformacionOrden = _idalttisfc001.findByOrderNumberAnuncioOrd(ref PDNO, ref strError);

            if (_consultaInformacionOrden.Rows.Count > 0)
            {
                var unidad = _consultaInformacionOrden.Rows[0]["CUNI"].ToString();
                var factor = _consultaInformacionOrden.Rows[0]["CONV"].ToString();
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

                return GuardaAnuncioAutomatico(sqnb, pdno, factor);
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
            var qtyreh = Convert.ToDecimal(_consultaInformacionOrden.Rows[0]["QTYREH"].ToString());
            var qtyord = Convert.ToDecimal(qrdr.Trim() == String.Empty ? 0.000 : Convert.ToDouble(qrdr) * calcAnuncioOrd);
            var qtyrec = Convert.ToDecimal(_consultaInformacionOrden.Rows[0]["QTYREC"].ToString());
            var enterqty = Convert.ToDecimal(quantannounced);

            if (((qtyrec + enterqty) - qtyreh) > qtyord)
            {
                lblError.Text = String.Format(mensajes("exceedquantity"), (qtyrec + enterqty), qtyord);
                lblConfirm.Text = string.Empty;
                return false;
            }

            if (enterqty <= 0)
            {
                lblError.Text = mensajes("quantzero");
                lblConfirm.Text = string.Empty;
                return false;
            }

            var qtdl = "0";
            var validarRegistro = _idaltticol022.findBySqnbPdnoAndQtdl(ref pdno, ref sqnb, ref qtdl, ref strError);

            if (validarRegistro.Rows.Count > 0)
            {
                Ent_tticol022 data022 = new Ent_tticol022()
                {
                    qtdl = enterqty,
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
            lblSecuence.Text = _textoLabels.readStatement(formName, _idioma, "lblSecuence");
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

        protected void formRePrintLabel()
        {

            var pdno = txtOrder.Text.Trim().ToUpper();
            var sec = txtSecuence.Text.Trim().ToUpper();
            var sqnb = String.Concat(pdno, '-', sec);

            if (pdno == String.Empty || sec == String.Empty)
            {
                lblError.Text = mensajes("");
                lblConfirm.Text = string.Empty;
                return;
            }

            var consultaInformacion = _idaltticol022.findBySqnbPdnoLabelPallet(ref pdno, ref sqnb, ref strError).Rows;

            if (consultaInformacion.Count > 0)
            {
                var dele = consultaInformacion[0]["DELE"].ToString();

                if (dele == "1")
                {
                    lblError.Text = mensajes("tagdeleted");
                    lblConfirm.Text = string.Empty;
                    return;
                }

                var madein = consultaInformacion[0]["NAME"].ToString().Trim().ToUpper();
                var qtdl = consultaInformacion[0]["QTDL"].ToString().Trim().ToUpper();
                var item = consultaInformacion[0]["MITM"].ToString().Trim().ToUpper();
                var desc = consultaInformacion[0]["DSCA"].ToString().Trim().ToUpper();
                var unit = consultaInformacion[0]["CUNI"].ToString().Trim().ToUpper();
                var maq = consultaInformacion[0]["MCNO"].ToString().Trim().ToUpper();
                var fecha = consultaInformacion[0]["FECHA"].ToString().Trim().ToUpper();
                var user = consultaInformacion[0]["LOGN"].ToString().Trim().ToUpper();
                var norp = Convert.ToInt32(consultaInformacion[0]["NORP"].ToString().Trim().ToUpper());

                var rutaServ = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + item.Trim().ToUpper() + "&code=Code128&dpi=96";
                imgCodeItem.Src = !string.IsNullOrEmpty(item) ? rutaServ : "";

                //Codigo barras sqnb
                rutaServ = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + sqnb.Trim().ToUpper() + "&code=Code128&dpi=96";
                imgCodeSqnb.Src = !string.IsNullOrEmpty(sqnb) ? rutaServ : "";

                lblDescItem.Text = desc;
                lblItem.Text = item.Trim().ToUpper();
                lblValueWorkOrder.Text = pdno;
                lblValuePalletNumber.Text = sec;
                lblValueDate.Text = fecha;
                lblValueShift.Text = "A,B,C,D";
                lblValueCasePerPallet.Text = qtdl;
                lblValueMadeIn.Text = madein;
                lblValueInspectorInitial.Text = "REPRINT by: "+_operator;
                divTable.Visible = true;
                tdBtnExit.Visible = false;
                divBotones.Visible = true;

                //MODIFICCACIONES JC
                _idaltticol022.ActualizarRegistroTicol222(Session["user"].ToString(), pdno,sqnb);

                Ent_tticol022 Obj_tticol022 = new Ent_tticol022
                {
                    sqnb = sqnb,
                    norp = ++norp
                };
                bool ActualizacionTticol022 = _idaltticol022.ActualizarNorpTicol022( Obj_tticol022);
         
            }
            else
            {
                divTable.Visible = false;
                tdBtnExit.Visible = true;
                divBotones.Visible = false;
                lblError.Text = mensajes("ordenocreated");
                lblConfirm.Text = string.Empty;
                return;
            }
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
