using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using whusa.Entidades;
using whusa.Interfases;
using whusa.Utilidades;
using System.Threading;
using System.Globalization;
using System.Configuration;
using System.Data;

namespace whusap.WebPages.Migration
{
    public partial class whInvAnuncioOrd : System.Web.UI.Page
    {
        #region Propiedades
            private static InterfazDAL_ttccol301 _idalttccol301 = new InterfazDAL_ttccol301();
            private static InterfazDAL_tticol022 _idaltticol022 = new InterfazDAL_tticol022();
            private static InterfazDAL_ttisfc001 _idalttisfc001 = new InterfazDAL_ttisfc001();
            private static InterfazDAL_tticol020 _idaltticol020 = new InterfazDAL_tticol020();
            private static Mensajes _mensajesForm = new Mensajes();
            private static LabelsText _textoLabels = new LabelsText();
            private static string _operator;
            public static string _idioma;
            private static string strError;
            private static string formName;
            private static string globalMessages = "GlobalMessages";
            private static string _tipoFormulario = String.Empty;
            private static DataTable _consultaInformacionOrden = new DataTable();
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


                    if (Request.QueryString["tipoFormulario"] != null)
                    {
                        _tipoFormulario = Request.QueryString["tipoFormulario"].ToString().ToUpper().Trim();
                    }
                    else
                    {
                        _tipoFormulario = "PALLET";
                    }

                    txtOrderNumber.Text = Session["SqnbAnuncioAutomatico"] == null ? String.Empty : Session["SqnbAnuncioAutomatico"].ToString();

                    //if (!_procesoAutomatico)
                    //{
                    //    if (Session["SqnbAnuncioAutomatico"] == null)
                    //    {
                    //        lblError.Text = mensajes("No se pudo correr el proceso automatico, no se inicializo la variable de sesión sqnb");
                    //        return;
                    //    }

                    //    txtOrderNumber.Text = Session["SqnbAnuncioAutomatico"].ToString();
                    //    //btnConsultar_Click(btnConsultar, null);
                    //}
                }
            }

        protected void btnConsultar_Click(object sender, EventArgs e)
            {
                if (txtOrderNumber.Text.Trim() != String.Empty)
                {
                    var SQNB = txtOrderNumber.Text.Trim().ToUpper();
                    hdfSqnb.Value = SQNB;
                    var PDNO = SQNB.Substring(0, 9);
                    var QTDL = "0";
                    divTable.Visible = false;

                    var consultaOrden = _idaltticol022.findBySqnbPdnoAndQtdl(ref PDNO, ref SQNB, ref QTDL, ref strError).Rows;

                    if (consultaOrden.Count > 0)
                    {
                        //Llamar metodo de consulta
                        _consultaInformacionOrden = _idalttisfc001.findByOrderNumberAnuncioOrd(ref PDNO, ref strError);

                        if (_consultaInformacionOrden.Rows.Count > 0)
                        {
                            var item = _consultaInformacionOrden.Rows[0]["MITM"].ToString();
                            var descripcion = _consultaInformacionOrden.Rows[0]["DSCAITEM"].ToString();
                            var factor = _consultaInformacionOrden.Rows[0]["CONV"].ToString();
                            var unidad = _consultaInformacionOrden.Rows[0]["CUNI"].ToString();
                            var almacen = _consultaInformacionOrden.Rows[0]["CWAR"].ToString();
                            var descalmacen = _consultaInformacionOrden.Rows[0]["DSCACWAR"].ToString();
                            var osta = Convert.ToUInt32(_consultaInformacionOrden.Rows[0]["OSTA"].ToString());

                            if (unidad.Trim() != "Kg")
                            {
                                if (factor.Trim() == String.Empty)
                                {
                                    lblError.Text = mensajes("notfactor");
                                    return;
                                }
                            }

                            factor = Convert.ToDecimal(factor == "" ? "0" : factor).ToString("0.000");
                            var totqty = Convert.ToDecimal(_consultaInformacionOrden.Rows[0]["QRDR"].ToString()).ToString("0.000");
                            var totqtyent = Convert.ToDecimal(_consultaInformacionOrden.Rows[0]["TOTQTYEND"].ToString()).ToString("0.000");
                            var qtypend = Convert.ToDecimal(_consultaInformacionOrden.Rows[0]["QTYPEND"].ToString()).ToString("0.000");

                            if (osta != 5 && osta != 7)
                            {
                                lblError.Text = mensajes("orderinactive");
                                return;
                            }

                            if (_tipoFormulario == "QUANTITY")
                            {
                                lblValueAnounced.Visible = false;
                                txtAnounced.Visible = true;
                                txtAnounced.Text = Session["cantidadAnunPalletTag"] == null ? String.Empty : Session["cantidadAnunPalletTag"].ToString();
                            }
                            else
                            {
                                lblValueAnounced.Visible = true;
                                txtAnounced.Visible = false;
                            }

                            lblValueOrden.Text = PDNO;
                            lblValueArticulo.Text = String.Concat(item, " - ", descripcion);
                            lblValueWareHouse.Text = String.Concat(almacen, " - ", descalmacen);
                            lblValueTotal.Text = totqty;
                            lblValueDelivered.Text = totqtyent;
                            lblValueAnounced.Text = factor;
                            lblValueUnit.Text = unidad;

                            divTable.Visible = true;
                            lblError.Text = String.Empty;

                            //if (!_procesoAutomatico)
                            //{
                            //    btnGuardar_Click(btnGuardar, null);
                            //}
                        }
                        else
                        {
                            lblError.Text = mensajes("ordernotexists");
                            return;
                        }
                    }
                    else
                    {
                        lblError.Text = mensajes("palletannounced");
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
                if (_tipoFormulario == "QUANTITY" && txtAnounced.Text.Trim() == String.Empty)
                {
                    lblError.Text = mensajes("formempty");
                    return;
                }

                var pdno = lblValueOrden.Text.Trim().ToUpper();
                var sqnb = hdfSqnb.Value;
                var qrdr = _consultaInformacionOrden.Rows[0]["QRDR"].ToString();
                var qtyord = Convert.ToDecimal(qrdr.Trim() == String.Empty ? 0.000 : Convert.ToDouble(qrdr) * Convert.ToDouble(ConfigurationManager.AppSettings["calcAnuncioOrd"].ToString()));
                var qtyrec = Convert.ToDecimal(_consultaInformacionOrden.Rows[0]["QTYREC"].ToString());
                var enterqty = Convert.ToDecimal(_tipoFormulario == "QUANTITY" ? txtAnounced.Text.Trim() : lblValueAnounced.Text);

                if ((qtyrec + enterqty) > qtyord)
                {
                    lblError.Text = String.Format(mensajes("exceedquantity"), (qtyrec + enterqty), qtyord);
                    return;
                }

                if (enterqty <= 0)
                {
                    lblError.Text = mensajes("quantzero");
                    return;
                }

                var qtdl = "0";
                var validarRegistro = _tipoFormulario == "QUANTITY" ? _idaltticol022.selectDatesBySqnbPdno(ref pdno, ref sqnb, ref strError)
                        : _idaltticol022.findBySqnbPdnoAndQtdl(ref pdno, ref sqnb, ref qtdl, ref strError);

                if (validarRegistro.Rows.Count > 0)
                {
                    Ent_tticol022 data022 = new Ent_tticol022()
                    {
                        qtdl = enterqty,
                        pdno = pdno,
                        sqnb = sqnb
                    };

                    var validaUpdate = _idaltticol022.actualizaRegistroAnuncioOrd(ref data022, ref strError);
                    var validaUpdateCantidad = _idaltticol022.ActualizarCantidadRegistroTicol222(enterqty, sqnb);
                    if (validaUpdate)
                    {
                        var item = lblValueArticulo.Text.Split('-')[0].Trim().ToUpper() + "-" + lblValueArticulo.Text.Split('-')[1].Trim().ToUpper();
                        var dsca = lblValueArticulo.Text.Split('-')[2].Trim().ToUpper();
                        var cuni = lblValueUnit.Text;

                        Ent_tticol020 data020 = new Ent_tticol020()
                        {
                            pdno = pdno,
                            mitm = item,
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
                            divTable.Visible = false;
                            return;
                        }
                        else
                        {
                            lblError.Text = mensajes("errorsave");
                            return;
                        }
                    }
                    else
                    {
                        lblError.Text = mensajes("erroupdt");
                        return;
                    }
                }
                else
                {
                    lblError.Text = mensajes("orderproccesed");
                    return;
                }
            }

        #endregion

        #region Metodos

        protected void CargarIdioma()
            {
                lblOrderNumber.Text = _textoLabels.readStatement(formName, _idioma, "lblOrderNumber");
                btnConsultar.Text = _textoLabels.readStatement(formName, _idioma, "btnConsultar");
                lblOrden.Text = _textoLabels.readStatement(formName, _idioma, "lblOrden");
                lblArticulo.Text = _textoLabels.readStatement(formName, _idioma, "lblArticulo");
                lblWareHouse.Text = _textoLabels.readStatement(formName, _idioma, "lblWareHouse");
                lblTotal.Text = _textoLabels.readStatement(formName, _idioma, "lblTotal");
                lblDelivered.Text = _textoLabels.readStatement(formName, _idioma, "lblDelivered");
                lblPending.Text = _textoLabels.readStatement(formName, _idioma, "lblPending");
                lblAnounced.Text = _textoLabels.readStatement(formName, _idioma, "lblAnounced");
                lblUnit.Text = _textoLabels.readStatement(formName, _idioma, "lblUnit");
                btnGuardar.Text = _textoLabels.readStatement(formName, _idioma, "btnGuardar");
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
