using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Globalization;
using whusa.Interfases;
using whusa.Entidades;
using System.Configuration;

namespace whusap.WebPages.InvAnunciosProd
{
    public partial class whInvAnuncioOrden : System.Web.UI.Page
    {
        #region Propiedades
            private static string _operator;
            private string strError;
            private static InterfazDAL_ttisfc001 _idaltisfc001 = new InterfazDAL_ttisfc001();
            private static InterfazDAL_tticol020 _idalticol020 = new InterfazDAL_tticol020();
            private static InterfazDAL_tticol025 _idalticol025 = new InterfazDAL_tticol025();
            private static InterfazDAL_ttccol301 _idalttccol301 = new InterfazDAL_ttccol301();
            private static string _idioma = String.Empty;
            private static string _qtdltisfc001;
        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-CO");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-CO");
                base.InitializeCulture();

                if (!IsPostBack)
                {


                    //Obtiene tipo de formulario
                    var tipoFormulario = Request.QueryString["tipoFormulario"];

                    if (tipoFormulario != null)
                    {
                        tipoFormulario = tipoFormulario.ToUpper();

                        //Maneja visibilidad dependiendo del formulario
                        switch (tipoFormulario)
                        {
                            case "CREATE":
                                trPendiente.Visible = true;
                                trAnunciado.Visible = true;
                                trPorConfirmar.Visible = false;
                                trConfirmado.Visible = false;
                                break;
                            case "CONFIRM":
                                trPendiente.Visible = false;
                                trAnunciado.Visible = false;
                                trPorConfirmar.Visible = true;
                                trConfirmado.Visible = true;
                                break;
                            default:
                                trPendiente.Visible = true;
                                trAnunciado.Visible = true;
                                trPorConfirmar.Visible = false;
                                trConfirmado.Visible = false;
                                break;
                        }

                    }

                    //valida inicio de sesion
                    if (Session["user"] == null)
                    {
                        Response.Redirect(ConfigurationManager.AppSettings["UrlBase"] + "/WebPages/Login/whLogIni.aspx");
                    }

                    _operator = Session["user"].ToString();

                    //Valida idioma
                    if (Session["ddlIdioma"] == null)
                    {
                        Session["ddlIdioma"] = "INGLES";
                    }

                    _idioma = Session["ddlIdioma"].ToString();

                    //Carga idiioma
                    if (_idioma == "ESPAÑOL")
                    {
                        IdiomaEspañol();
                    }
                    else
                    {
                        IdiomaIngles();
                    }

                    string strTitulo = tipoFormulario == "CREATE" ? mensajes("encreate") : mensajes("confcreate");
                    Label control = (Label)Page.Controls[0].FindControl("lblPageTitle");
                    control.Text = strTitulo;
                    lblError.Text = String.Empty;

                    trDataAditional.Visible = false;

                    //Guarda log de ingreso
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

        protected void btnFindOrder_Click(object sender, EventArgs e)
            {
                if (txtNumeroOrden.Text != String.Empty)
                {
                    var orderNumber = txtNumeroOrden.Text.ToUpper();

                    //Valida numero de orden
                    var validateOrder = _idaltisfc001.findByOrderNumber(ref orderNumber, ref strError);

                    if (validateOrder.Rows.Count > 0)
                    {
                        var rowOrder = validateOrder.Rows[0];

                        //Valida el estado de la orden
                        if (rowOrder["ESTATUS"].ToString() == "5" || rowOrder["ESTATUS"].ToString() == "7")
                        {
                            //obtiene tipo de formulario
                            var tipoFormulario = Request.QueryString["tipoFormulario"];
                            tipoFormulario = tipoFormulario != null ? tipoFormulario.ToUpper() : "CREATE";

                            //Validacion cantidad cuando el formulario es confirmacion
                            if (tipoFormulario == "CONFIRM" && decimal.Parse(rowOrder["CANTCONF"].ToString()) <= 0)
                            {
                                lblError.Text = mensajes("nocant");
                                return;
                            }

                            //Inserta data en los campos del formulario
                            lblOrdenFabricacion.Text = rowOrder["PDNO"].ToString();
                            lblItem.Text = rowOrder["ITEM"].ToString();
                            lblDescripcionItem.Text = rowOrder["DESCRIPCIONITEM"].ToString();
                            lblBodega.Text = rowOrder["BODEGA"].ToString();
                            lblDescripcionBodega.Text = rowOrder["DESCRIPCIONBODEGA"].ToString();
                            lblTotal.Text = rowOrder["TOTAL"].ToString();
                            lblEntregado.Text = rowOrder["ENTREGADO"].ToString();

                            _qtdltisfc001 = rowOrder["CANTCONF"].ToString();

                            //Inserta data segun formulario
                            if (tipoFormulario == "CONFIRM")
                            {
                                lblPorConfirmar.Text = rowOrder["CANTCONF"].ToString();
                            }
                            else
                            {
                                lblPendiente.Text = rowOrder["PENDIENTE"].ToString();
                            }

                            lblUnidad.Text = rowOrder["UNIDAD"].ToString();

                            lblError.Text = "";
                            trDataAditional.Visible = true;
                        }
                        else
                        {
                            lblError.Text = mensajes("estatusord");
                            return;
                        }
                    }
                    else
                    {
                        lblError.Text = mensajes("ordnotexists");
                        return;
                    }
                }
                else
                {
                    lblError.Text = mensajes("ordBlank");
                    return;
                }
            }

        protected void btnSaveInformation_Click(object sender, EventArgs e)
            {
                var tipoFormulario = Request.QueryString["tipoFormulario"];
                tipoFormulario = tipoFormulario != null ? tipoFormulario.ToUpper() : "CREATE";

                //Valida tipo de formulario y campos en blanco
                if (tipoFormulario == "CONFIRM" ? txtConfirmado.Text.Trim() != String.Empty : txtAnunciado.Text != String.Empty)
                {
                    var validateSave = 0;

                    if (tipoFormulario == "CONFIRM")
                    {
                        var orderNumber = txtNumeroOrden.Text.ToUpper();
                        var consec = 0;
                        var qtdltisfc001 = _qtdltisfc001;
                        var qtdlTotal = 0.0M;
                        //Suma valores QTDL
                        var sumQtdlticol025 = _idalticol025.sumQtdlByOrderNumber(ref orderNumber, ref strError);

                        if (sumQtdlticol025.Rows.Count > 0)
                        {
                            consec = Convert.ToInt32(sumQtdlticol025.Rows[0]["CONSEC"]) + 1;
                            var sum = sumQtdlticol025.Rows[0]["SUMQTDL"].ToString();
                            qtdlTotal = decimal.Parse(qtdltisfc001) + decimal.Parse(sum == "" ? "0" : sum);
                        }

                        //Valida que cantidad de confirmado no sea mayor que QTDL Total
                        if (decimal.Parse(txtConfirmado.Text.Trim().Replace(".", ",")) > qtdlTotal)
                        {
                            lblError.Text = mensajes("largerQuant");
                            return;
                        }

                        //Inserta en tabla ticol025
                        Ent_tticol025 dataticol025 = new Ent_tticol025()
                        {
                            pdno = lblOrdenFabricacion.Text.ToUpper(),
                            sqnb = consec,
                            mitm = lblItem.Text.ToUpper(),
                            dsca = lblDescripcionItem.Text.ToUpper(),
                            qtdl = float.Parse(txtConfirmado.Text.Trim(), CultureInfo.InvariantCulture.NumberFormat),
                            cuni = lblUnidad.Text.ToUpper(),
                            user = _operator
                        };

                        validateSave = _idalticol025.insertarRegistro(ref dataticol025, ref strError);
                    }
                    else
                    {
                        //Inserta en tabl ticol020
                        Ent_tticol020 dataticol020 = new Ent_tticol020()
                        {
                            pdno = lblOrdenFabricacion.Text.ToUpper(),
                            mitm = lblItem.Text.ToUpper().TrimStart(),
                            dsca = lblDescripcionItem.Text.ToUpper(),
                            qtdl = Convert.ToDecimal(txtAnunciado.Text.Trim()),
                            cuni = lblUnidad.Text.ToUpper(),
                            mess = " ",
                            user = _operator
                        };

                        List<Ent_tticol020> listData = new List<Ent_tticol020>();
                        listData.Add(dataticol020);

                        validateSave = _idalticol020.insertarRegistro(ref listData, ref strError);
                    }



                    //Valida que los procesos en BD sean correctos
                    if (validateSave > 0)
                    {
                        lblError.Text = "";
                        lblConfirm.Text = mensajes("save");

                        trDataAditional.Visible = false;
                        txtConfirmado.Text = "";
                        txtAnunciado.Text = "";
                        txtNumeroOrden.Text = "";
                        txtNumeroOrden.Focus();
                        return;
                    }
                    else
                    {
                        lblError.Text = mensajes("error");
                        return;
                    }
                }
                else
                {
                    lblError.Text = mensajes("anunBlank");
                    return;
                }
            }

        #endregion

        #region Metodos

        protected void IdiomaIngles()
        {
            Session["ddlIdioma"] = "INGLES";
            _idioma = "INGLES";
            lblNumeroOrden.Text = "Production Order Number: ";
            lblOrderData.Text = "Production Order Number: ";
            lblItemDesc.Text = "Item: ";
            lblBodegaDesc.Text = "Warehouse: ";
            lblTotalDesc.Text = "Total: ";
            lblEntregadoDesc.Text = "Delivered: ";
            lblPendienteDesc.Text = "Pending: ";
            lblAnunciadoDesc.Text = "Announced: ";
            lblPorConfirmarDesc.Text = "To be confirmed: ";
            lblConfirmadoDesc.Text = "Confirmed: ";
            lblUnidadDesc.Text = "Unit: ";
            btnSaveInformation.Text = "Save";
            btnFindOrder.Text = "Find";
        }

        protected void IdiomaEspañol()
        {
            Session["ddlIdioma"] = "ESPAÑOL";
            _idioma = "ESPAÑOL";
            lblNumeroOrden.Text = "Orden Fabricación No.: ";
            lblOrderData.Text = "Orden Fabricación No.: ";
            lblItemDesc.Text = "Item: ";
            lblBodegaDesc.Text = "Bodega: ";
            lblTotalDesc.Text = "Total: ";
            lblEntregadoDesc.Text = "Entregado: ";
            lblPendienteDesc.Text = "Pendiente: ";
            lblAnunciadoDesc.Text = "Anunciado: ";
            lblPorConfirmarDesc.Text = "Por confirmar: ";
            lblConfirmadoDesc.Text = "Confirmado: ";
            lblUnidadDesc.Text = "Unidad: ";
            btnSaveInformation.Text = "Guardar";
            btnFindOrder.Text = "Buscar";
        }

        protected string mensajes(string tipoMensaje)
        {
            var retorno = String.Empty;

            switch (tipoMensaje)
            {
                case "encreate":
                    retorno = _idioma == "ESPAÑOL" ? "Crear anuncio" : "Create announcement";
                    break;
                case "confcreate":
                    retorno = _idioma == "ESPAÑOL" ? "Confirmar anuncio" : "Confirm announcement";
                    break;
                case "nocant":
                    retorno = _idioma == "ESPAÑOL" ? "No hay cantidades pendientes por confirmar" : "There are not missing quantities to confirm";
                    break;
                case "estatusord":
                    retorno = _idioma == "ESPAÑOL" ? "Estatus de Orden no permite anuncio" : "Order Status doesn't allow announcement";
                    break;
                case "ordnotexists":
                    retorno = _idioma == "ESPAÑOL" ? "Orden no existe" : "Order number not exists";
                    break;
                case "ordBlank":
                    retorno = _idioma == "ESPAÑOL" ? "Por favor diligencie el número de orden" : "Please, enter an order number.";
                    break;
                case "largerQuant":
                    retorno = _idioma == "ESPAÑOL" ? "Cantidad digitada es mayor a la cantidad pendiente por confirmar" : "Digited quantity is higher than pending quantity to confirm";
                    break;
                case "save":
                    retorno = _idioma == "ESPAÑOL" ? "Información guardada correctamente" : "Information saved correctly";
                    break;
                case "anunBlank":
                    retorno = _idioma == "ESPAÑOL" ? "Por favor diligencie el campo Anunciado." : "Please, enter field Announcement";
                    break;
                case "error":
                    tipoMensaje = _idioma == "ESPAÑOL" ? "Ha ocurrido un error mientras se guardaba la información, contacte al Administrador" : "Error occurred while saved information, please contact Administrator";
                    break;
            }

            return retorno;
        }

        #endregion
    }
}
