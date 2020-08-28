using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Configuration;
using whusa.Entidades;
using whusa.Interfases;
using System.Globalization;
using whusa.Utilidades;

namespace whusap.WebPages.InvLogistica
{
    public partial class whInvConfirmReceipt : System.Web.UI.Page
    {
        #region Propiedades
            private static InterfazDAL_ttccol301 _idalttccol301 = new InterfazDAL_ttccol301();
            private static InterfazDAL_ttisfc001 _idalttisfc001 = new InterfazDAL_ttisfc001();
            private static InterfazDAL_tticol025 _idaltticol025 = new InterfazDAL_tticol025();
            private static InterfazDAL_tticol022 _idaltticol022 = new InterfazDAL_tticol022();
            private static Mensajes _mensajesForm = new Mensajes();
            private static LabelsText _textoLabels = new LabelsText();
            private static string _operator;
            public static string _idioma;
            private static string strError;
            private static string formName;
            private static string globalMessages = "GlobalMessages";
            public static string SLOC;
            public static string LOCA;
            public static string CWAR;
        #endregion

        #region Eventos
        //
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
                    catch (Exception ex)
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
                }
            }

            protected void btnConsultar_Click(object sender, EventArgs e)
            {
                divTable.Visible = false;

                if (txtNumeroOrden.Text.Trim() != String.Empty)
                {
                    var numeroOrdenPallet = txtNumeroOrden.Text.Trim().ToUpper();
                    var numeroOrden = numeroOrdenPallet.Substring(0, 9);
                    var pro1 = true;

                    var consultaOrden = _idalttisfc001.findByOrderNumberConfirmRecep(ref numeroOrden, ref numeroOrdenPallet, ref pro1, ref strError);

                    if (consultaOrden.Rows.Count < 1)
                    {
                        pro1 = false;
                        consultaOrden = _idalttisfc001.findByOrderNumberConfirmRecep(ref numeroOrden, ref numeroOrdenPallet, ref pro1, ref strError);
                    }

                    if (consultaOrden.Rows.Count > 0)
                    {
                        var DELE = consultaOrden.Rows[0]["DELE"].ToString();
                        var QTDL = consultaOrden.Rows[0]["QTDL"].ToString();
                        var OSTA = consultaOrden.Rows[0]["OSTA"].ToString();
                        SLOC = consultaOrden.Rows[0]["SLOC"].ToString();
                        LOCA = consultaOrden.Rows[0]["LOCA"].ToString();
                        CWAR = consultaOrden.Rows[0]["CWAR"].ToString();
                        //Validamos si el parametro de validar producción esta activo
                        var consultaPrdVl = _idalttisfc001.findProdValidation_Parameter(ref strError);
                        var PRVL = consultaPrdVl.Rows[0]["PRVL"].ToString();
                        if (PRVL == "1")
                        {
                            if (DELE != "4")
                            {
                                lblError.Text = mensajes("palletwrapped");
                                lblConfirm.Text = "";
                                return;
                            }
                        }
                        if (QTDL == "0")
                        {
                            lblError.Text = mensajes("palletannounced");
                            lblConfirm.Text = "";
                            return;
                        }
                        else if (!pro1)
                        {
                            lblError.Text = mensajes("palletconfirmed");
                            lblConfirm.Text = "";
                            return;
                        }

                        if (OSTA == "5" || OSTA == "7")
                        {   
                        }
                        else
                        {
                            lblError.Text = mensajes("ordernotactive");
                            lblConfirm.Text = "";
                            return;
                        }

                        lblValueOrden.Text = numeroOrden;
                        lblValueArticulo.Text = consultaOrden.Rows[0]["MITM"].ToString()
                                            + " - " + consultaOrden.Rows[0]["DSCA"].ToString();
                        lblValueWareHouse.Text = consultaOrden.Rows[0]["CWAR"].ToString()
                                            + " - " + consultaOrden.Rows[0]["DSCACWAR"].ToString();
                        lblValueTotal.Text = consultaOrden.Rows[0]["QRDR"].ToString();
                        lblValueDelivered.Text = consultaOrden.Rows[0]["TOTQTYENT"].ToString();
                        lblValueToReceive.Text = consultaOrden.Rows[0]["QTYPEND"].ToString();
                        lblValueConfirmed.Text = float.Parse(consultaOrden.Rows[0]["QTDL"].ToString()).ToString("0.000");
                        lblValueUnit.Text = consultaOrden.Rows[0]["CUNI"].ToString();

                        lblError.Text = String.Empty;
                        divTable.Visible = true;
                    }
                    else
                    {
                        lblError.Text = mensajes("palletannounced");
                        lblConfirm.Text = "";
                        return;
                    }
                }
                else
                {
                    lblError.Text = mensajes("numberempty");
                    lblConfirm.Text = "";
                    return;
                }
            }

            protected void btnGuardar_Click(object sender, EventArgs e)
            {
                var sqnb = txtNumeroOrden.Text.Trim().ToUpper();
                var pdno = sqnb.Substring(0, 9);
                var pro1 = "2";

                var consultadata = _idalttisfc001.findByPdnoSqnbAndPro1(ref pdno, ref sqnb, ref pro1, ref strError);

                if (consultadata.Rows.Count > 0)
                {
                    var STRQTY = lblValueConfirmed.Text;
                    var QTYPEND = lblValueToReceive.Text;

                    if (float.Parse(STRQTY) > float.Parse(QTYPEND))
                    {
                        lblError.Text = String.Format(mensajes("confirmedquantity"), STRQTY, QTYPEND);
                        lblConfirm.Text = "";
                        return;
                    }

                    if (float.Parse(STRQTY) < 0)
                    {
                        lblError.Text = mensajes("quantitynegative");
                        lblConfirm.Text = "";
                        return;
                    }

                    if (float.Parse(STRQTY) == 0)
                    {
                        lblError.Text = mensajes("quantityzero");
                        lblConfirm.Text = "";
                        return;
                    }

                    var consecutivo = _idaltticol025.consultarConsecutivoRegistro(ref pdno, ref strError);

                    consecutivo = consecutivo + 1;

                    var item = lblValueArticulo.Text.Split('-')[0].Trim().ToUpper() + "-" + lblValueArticulo.Text.Split('-')[1].Trim().ToUpper();
                    var dsca = lblValueArticulo.Text.Split('-')[2].Trim().ToUpper();
                    var qtdl = float.Parse(STRQTY.Replace(',', '.'), CultureInfo.InvariantCulture.NumberFormat);
                    var cuni = lblValueUnit.Text;

                    Ent_tticol025 data025 = new Ent_tticol025()
                    {
                        pdno = pdno,
                        sqnb = consecutivo,
                        mitm = "         " + item.Trim().ToUpper(),
                        dsca = dsca,
                        qtdl = qtdl,
                        cuni = cuni,
                        mess = " ",
                        user = _operator,
                        refcntd = 0,
                        refcntu = 0
                    };

                    var validSave = _idaltticol025.insertarRegistro(ref data025, ref strError);

                    if (validSave > 0)
                    {

                        Ent_tticol022 data022 = new Ent_tticol022()
                        {
                            log1 = _operator,
                            qtd1 = Convert.ToInt32(data025.qtdl),
                            pdno = pdno,
                            sqnb = sqnb
                        };

                        var validateUpdate = _idaltticol022.actualizaRegistroConfirmReceipt(ref data022, ref strError);

                        if (validateUpdate)
                        {
                            lblConfirm.Text = mensajes("msjsave");
                            lblError.Text = "";
                            lblError.Text = String.Empty;
                            divTable.Visible = false;
                            txtNumeroOrden.Text = String.Empty;
                            _idaltticol022.ActualizarCantidadAlmacenRegistroTicol222(_operator, data022.qtd1, LOCA, CWAR, data022.sqnb);
                            return;
                        }
                        else
                        {
                            lblError.Text = mensajes("errorupdt");
                            lblConfirm.Text = "";
                            return;
                        }
                    }
                    else
                    {
                        lblConfirm.Text = String.Empty;
                        lblError.Text = mensajes("errorsave");
                        lblConfirm.Text = "";
                        return;
                    }
                }
                else
                {
                    lblError.Text = mensajes("orderconfirmed");
                    lblConfirm.Text = "";
                    return;
                }
            }

        #endregion

        #region Metodos

        protected void CargarIdioma()
            {
                lblNumeroOrden.Text = _textoLabels.readStatement(formName, _idioma, "lblNumeroOrden");
                btnConsultar.Text = _textoLabels.readStatement(formName, _idioma, "btnConsultar");
                lblOrden.Text = _textoLabels.readStatement(formName, _idioma, "lblOrden");
                lblArticulo.Text = _textoLabels.readStatement(formName, _idioma, "lblArticulo");
                lblWareHouse.Text = _textoLabels.readStatement(formName, _idioma, "lblWareHouse");
                lblTotal.Text = _textoLabels.readStatement(formName, _idioma, "lblTotal");
                lblDelivered.Text = _textoLabels.readStatement(formName, _idioma, "lblDelivered");
                lblToReceive.Text = _textoLabels.readStatement(formName, _idioma, "lblToReceive");
                lblConfirmed.Text = _textoLabels.readStatement(formName, _idioma, "lblConfirmed");
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