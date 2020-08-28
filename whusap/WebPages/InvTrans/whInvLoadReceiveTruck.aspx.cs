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
using System.Data;
using System.Configuration;
using whusa.Utilidades;

namespace whusap.WebPages.InvTrans
{
    public partial class whInvLoadTruc : System.Web.UI.Page
    {
        #region Propiedades
            private static InterfazDAL_twhcol120 _idaltwhcol120 = new InterfazDAL_twhcol120();
            private static InterfazDAL_tticol022 _idtticol022 = new InterfazDAL_tticol022();
            private static InterfazDAL_twhcol119 _idaltwhcol119 = new InterfazDAL_twhcol119();
            private static InterfazDAL_twhwmd300 _idaltwhwmd300 = new InterfazDAL_twhwmd300();
            private static InterfazDAL_twhcol122 _idaltwhcol122 = new InterfazDAL_twhcol122();
            private static InterfazDAL_twhcol124 _idaltwhcol124 = new InterfazDAL_twhcol124();
            private static InterfazDAL_ttccol301 _idalttccol301 = new InterfazDAL_ttccol301();
            private static InterfazDAL_twhcol123 _idaltwhcol123 = new InterfazDAL_twhcol123();
            private static InterfazDAL_twhcol125 _idaltwhcol125 = new InterfazDAL_twhcol125();
            private static Mensajes _mensajesForm = new Mensajes();
            private static LabelsText _textoLabels = new LabelsText();
            private static string _sourceWareHouse;
            private static string _destinationWareHouse;
            private static string tipoFormulario;
            private static string _UNIQUEID;
            public static bool loadLocation;
            string strError = string.Empty;
            protected static string _operator;
            private static string _idioma;
            private static string formName;
            private static string globalMessages = "GlobalMessages";
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
                    string strTitulo = "";
                    lblError.Text = String.Empty;
                    lblConfirm.Text = String.Empty;

                    tipoFormulario = Request.QueryString["tipoFormulario"];

                    if (Session["user"] == null)
                    {
                        if (Request.QueryString["Valor1"] == null || Request.QueryString["Valor1"] == "")
                        {
                            Response.Redirect(ConfigurationManager.AppSettings["UrlBase"] + "/WebPages/Login/whLogIni.aspx");
                        }
                        else
                        {
                            _operator = Request.QueryString["Valor1"];
                            Session["user"] = _operator;
                            Session["logok"] = "OKYes";
                        }
                    }
                    else
                    {
                        _operator = Session["user"].ToString();
                    }


                    if (Session["ddlIdioma"] == null)
                    {
                        Session["ddlIdioma"] = "INGLES";
                    }

                    _idioma = Session["ddlIdioma"].ToString();


                    CargarIdioma();

                    //if (_idioma == "ESPAÑOL")
                    //{
                    //    IdiomaEspañol();
                    //}
                    //else
                    //{
                    //    IdiomaIngles();
                    //}

                    if (tipoFormulario != null)
                    {
                        tipoFormulario = tipoFormulario.ToUpper();

                        switch (tipoFormulario)
                        {
                            case "LOAD":
                                btnConfirmPalletLoad.Visible = true;
                                btnConfirmPalletReceive.Visible = false;
                                trLocation.Visible = true;
                                txtUnit.Visible = true;
                                tdUnit.Visible = true;
                                tdItem.Style.Add("width", "80%");
                                break;
                            case "RECEIVE":
                                btnConfirmPalletLoad.Visible = false;
                                btnConfirmPalletReceive.Visible = true;
                                trLocation.Visible = false;
                                txtUnit.Visible = false;
                                tdUnit.Visible = false;
                                tdItem.Style.Add("width", "100%");
                                break;
                            default:
                                btnConfirmPalletLoad.Visible = true;
                                btnConfirmPalletReceive.Visible = false;
                                tipoFormulario = "LOAD";
                                trLocation.Visible = true;
                                txtUnit.Visible = true;
                                tdUnit.Visible = true;
                                tdItem.Style.Add("width", "80%");
                                break;
                        }
                    }
                    else
                    {
                        btnConfirmPalletLoad.Visible = true;
                        btnConfirmPalletReceive.Visible = false;
                        tipoFormulario = "LOAD";
                        trLocation.Visible = true;
                        txtUnit.Visible = true;
                        tdUnit.Visible = true;
                        tdItem.Style.Add("width", "80%");
                    }

                    cargarUID(tipoFormulario);

                    Label control = (Label)Page.Controls[0].FindControl("lblPageTitle");
                    strTitulo = tipoFormulario == "LOAD" ? mensajes("encabezadoLoad") : mensajes("encabezadoReceive");
                    control.Text = strTitulo;
                    trDataAditional.Visible = false;


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

            protected void ddlUniqueID_OnSelectedIndexChanged(object sender, EventArgs e)
            {
                lblError.Text = String.Empty;
                lblConfirm.Text = String.Empty;

                if (txtUniqueID.Text != "")
                {
                    _UNIQUEID = txtUniqueID.Text.Trim().ToUpper();

                    //Validate that exists in twhcol120
                    var validationUI = _idaltwhcol120.validarRegistroByUniqueId(ref _UNIQUEID, ref strError);

                    //No permitir que inserten mas registros si ya se ejecuto el proceso de end picking 
                    if (validationUI.Rows.Count > 0)
                    {
                        var validateEndPicking = validationUI.Rows[0]["ENDPICKING"].ToString() == "1" ? true : false;
                        var validateEndReceive = validationUI.Rows[0]["ENDRECEIVE"].ToString() == "1" ? true : false;

                        var tipoFormulario = Request.QueryString["tipoFormulario"];
                        tipoFormulario = tipoFormulario.ToUpper();

                        if (tipoFormulario == "RECEIVE" && validateEndReceive == true)
                        {
                            trDataAditional.Visible = false;
                            lblError.Text = mensajes("alreadyreceive");
                            return;
                        }

                        if (tipoFormulario == "LOAD" && validateEndPicking == true)
                        {
                            trDataAditional.Visible = false;
                            lblError.Text = mensajes("alreadyload");
                            return;
                        }

                        //                if (validationUI.Rows.Count > 0)
                        //                {
                        //var tipoFormulario = Request.QueryString["tipoFormulario"];
                        //tipoFormulario = tipoFormulario.ToUpper();

                        if (tipoFormulario == "RECEIVE")
                        {
                            var validationEndPicking = validationUI.Rows[0]["ENDPICKING"].ToString() == "1" ? true : false;

                            if (!validationEndPicking)
                            {
                                trDataAditional.Visible = false;
                                lblError.Text = mensajes("wasntconfirmed");
                                return;
                            }

                            _sourceWareHouse = validationUI.Rows[0]["SOURCE"].ToString();
                            _destinationWareHouse = validationUI.Rows[0]["DESTINATION"].ToString();
                            trDataAditional.Visible = true;
                            txtUniqueID.Enabled = false;
                            lblError.Text = "";
                        }
                        else
                        {
                            _sourceWareHouse = validationUI.Rows[0]["SOURCE"].ToString();
                            _destinationWareHouse = validationUI.Rows[0]["DESTINATION"].ToString();
                            trDataAditional.Visible = true;
                            txtUniqueID.Enabled = false;
                            lblError.Text = "";
                        }
                    }
                    else
                    {
                        trDataAditional.Visible = false;
                        lblError.Text = mensajes("uidnotexists");
                        return;
                    }
                }
                else
                {
                    lblError.Text = mensajes("formempty");
                    return;
                }
            }

            protected void txtPalletID_OnTextChanged(object sender, EventArgs e)
            {
                lblConfirm.Text = String.Empty;
                lblError.Text = String.Empty;

                if (txtPalletID.Text.Trim() != "")
                {
                    limpiarCampos();
                    var validationPalletID = new DataTable();
                    var PALLETID = txtPalletID.Text.Trim().ToUpper();
                    //VALIDAR EL PALLETID CON BODEGA ORIGEN Y DESTINO - 20032019 Javier Castro
                    var buscadatosbodega = new DataTable();
                    buscadatosbodega = _idaltwhcol122.buscarbodegasuid(ref _UNIQUEID, ref strError);
                    var bodegaori = buscadatosbodega.Rows[0]["WARORI"].ToString();
                    var bodegades = buscadatosbodega.Rows[0]["WARDES"].ToString();

                    var tipoFormulario = Request.QueryString["tipoFormulario"];
                    tipoFormulario = tipoFormulario.ToUpper();

                    var validationPalletLocation = _idaltwhcol124.validateLocationByPaid(ref PALLETID, ref strError);

                    if (validationPalletLocation.Rows.Count > 0)
                    {
                        foreach (DataRow item in validationPalletLocation.Rows)
                        {
                            if (Convert.ToInt32(item["LOCS"].ToString()) == 2)
                            {
                                lblError.Text = String.Format(mensajes("paidnolocated"), item["UNID"].ToString());
                                return;
                            }
                        }
                    }

                    if (tipoFormulario == "RECEIVE")
                    {
                        validationPalletID = _idaltwhcol122.validarRegistroByPalledId(ref PALLETID, ref _UNIQUEID, ref bodegaori, ref bodegades, strError);

                        if (validationPalletID.Rows.Count == 0)
                        {
                            validationPalletID = _idaltwhcol123.validarRegistroByPalletId(ref PALLETID, ref _UNIQUEID, ref bodegaori, ref bodegades, ref strError);
                        }
                    }
                    else
                    {
                        validationPalletID = _idtticol022.validarRegistroByPalletId(ref PALLETID, ref bodegaori, ref bodegades, ref strError);
                    }

                    if (validationPalletID.Rows.Count > 0)
                    {
                        lblError.Text = "";

                        if (tipoFormulario == "LOAD")
                        {
                            if (validationPalletID.Rows[0]["PRO2"].ToString() == "2")
                            {
                                lblError.Text = mensajes("paidnolocatedload");
                                return;
                            }

                            txtUnit.Visible = true;
                            trLocation.Visible = true;
                            tdUnit.Visible = true;
                            tdItem.Style.Add("width", "80%");

                            var validatePalletID = _idaltwhcol122.validarRegistroByPalledId(ref PALLETID, ref _UNIQUEID, ref bodegaori, ref bodegades, strError);

                            if (validatePalletID.Rows.Count > 0)
                            {
                                lblError.Text = String.Format(mensajes("palletexist"), validatePalletID.Rows[0]["UNID"]);
                                return;
                            }
                            else
                            {
                                validatePalletID = _idaltwhcol123.validarRegistroByPalletId(ref PALLETID, ref _UNIQUEID, ref bodegaori, ref bodegades, ref strError);

                                if (validatePalletID.Rows.Count > 0)
                                {
                                    lblError.Text = String.Format(mensajes("palletexist"), validatePalletID.Rows[0]["UNID"]);
                                    return;
                                }
                            }


                            var validationLocation = _idaltwhcol119.validateLocation(ref _sourceWareHouse, ref strError);

                            if (validationLocation.Rows.Count > 0)
                            {
                                if (validationLocation.Rows[0]["LOCATION"].ToString().Trim() == String.Empty)
                                {
                                    txtLocation.Enabled = true;
                                    txtLocation.Text = "";
                                    loadLocation = false;
                                    txtLocation.Focus();
                                }
                                else
                                {
                                    txtLocation.Enabled = false;
                                    txtLocation.Text = validationLocation.Rows[0]["LOCATION"].ToString().Trim();
                                    loadLocation = true;
                                }
                            }
                            else
                            {
                                txtLocation.Enabled = true;
                                txtLocation.Text = "";
                                loadLocation = false;
                            }

                            txtUnit.Text = validationPalletID.Rows[0]["UNIDAD"].ToString().Trim();
                        }
                        else
                        {
                            txtUnit.Visible = false;
                            txtUnit.Text = "";
                            trLocation.Visible = false;
                            tdUnit.Visible = false;
                            tdItem.Style.Add("width", "100%");
                        }

                        txtItem.Text = validationPalletID.Rows[0]["ITEM"].ToString();
                        txtLot.Text = validationPalletID.Rows[0]["LOT"].ToString().Trim();
                        txtQuantity.Text = validationPalletID.Rows[0]["QUANTITY"].ToString().Trim();
                        hdfQuantity.Value = txtQuantity.Text;

                        return;
                    }
                    else
                    {
                        limpiarCampos();
                        if (tipoFormulario == "RECEIVE")
                        {
                            lblError.Text = mensajes("noexistloadpallet");
                        }
                        else
                        {
                            lblError.Text = mensajes("noexistbaan");
                        }

                        return;
                    }
                }
                else
                {
                    limpiarCampos();
                    lblError.Text = mensajes("palletblank");
                    return;
                }
            }

            protected void btnConfirmPalletLoad_Click(object sender, EventArgs e)
            {
                lblError.Text = String.Empty;
                lblConfirm.Text = String.Empty;

                if (txtPalletID.Text.Trim() != String.Empty)
                {
                    lblError.Text = "";

                    var strLocation = txtLocation.Text.Trim().ToUpper();
                    var palletId = txtPalletID.Text.Trim().ToUpper();
                    //VALIDAR EL PALLETID CON BODEGA ORIGEN Y DESTINO - 20032019 Javier Castro
                    var PALLETID = txtPalletID.Text.Trim().ToUpper();
                    var UNIQUEID = txtUniqueID.Text.Trim().ToUpper();
                    var bodegaori = _sourceWareHouse.Trim().ToUpper();
                    var bodegades = _destinationWareHouse.Trim().ToUpper();

                    if (strLocation != String.Empty)
                    {
                        if (loadLocation == false)
                        {
                            var location = _idaltwhwmd300.validateExistsLocation(ref strLocation, ref _sourceWareHouse, ref strError);

                            if (location.Rows.Count < 1)
                            {
                                lblError.Text = mensajes("invalidlocation");
                                txtLocation.Text = "";
                                txtLocation.Focus();
                                return;
                            }
                            else
                            {
                                var validateBlocking = location.Rows[0]["BOUT"].ToString();

                                if (validateBlocking == "1")
                                {
                                    lblError.Text = mensajes("locationblocked");
                                    return;
                                }

                                if (float.Parse(txtQuantity.Text) > float.Parse(hdfQuantity.Value))
                                {
                                    lblError.Text = mensajes("quantityhigher");
                                    return;
                                }

                                var validatePalletID = _idaltwhcol122.validarRegistroByPalledId(ref PALLETID, ref _UNIQUEID, ref bodegaori, ref bodegades, strError);

                                if (validatePalletID.Rows.Count > 0)
                                {
                                    lblError.Text = String.Format(mensajes("palletexist"), validatePalletID.Rows[0]["UNID"]);
                                    return;
                                }
                                else
                                {
                                    validatePalletID = _idaltwhcol123.validarRegistroByPalletId(ref PALLETID, ref _UNIQUEID, ref bodegaori, ref bodegades, ref strError);

                                    if (validatePalletID.Rows.Count > 0)
                                    {
                                        lblError.Text = String.Format(mensajes("palletexist"), validatePalletID.Rows[0]["UNID"]);
                                        return;
                                    }
                                }

                                Ent_twhcol122 _datathwcol122 = new Ent_twhcol122()
                                {
                                    unid = txtUniqueID.Text.Trim(),
                                    logn = _operator,
                                    loso = txtLocation.Text.Trim().ToUpper(),
                                    paid = txtPalletID.Text.Trim().ToUpper(),
                                    item = txtItem.Text == String.Empty ? " " : txtItem.Text.ToUpper(),
                                    clot = txtLot.Text.Trim().ToUpper() == String.Empty ? " " : txtLot.Text.Trim().ToUpper(),
                                    qtdt = float.Parse(txtQuantity.Text.Trim().Contains(",") == true ? txtQuantity.Text.Trim().Replace(",", ".") : txtQuantity.Text.Trim().Replace(".", ",")),
                                    proc = 1,
                                    mes1 = " ",
                                    refcntd = 0,
                                    refcntu = 0
                                };

                                var validateSave = _idaltwhcol122.insertarRegistro(ref _datathwcol122, ref strError);

                                if (validateSave)
                                {
                                    limpiarCampos();
                                    lblConfirm.Text = String.Format(mensajes("palletconfirm"), txtPalletID.Text.Trim().ToUpper());
                                    txtPalletID.Text = "";
                                    txtPalletID.Focus();
                                    return;
                                }
                                else
                                {
                                    lblError.Text = mensajes("errorsave"); ;
                                    return;
                                }
                            }
                        }
                        else
                        {
                            var validatePalletID = _idaltwhcol122.validarRegistroByPalledId(ref PALLETID, ref _UNIQUEID, ref bodegaori, ref bodegades, strError);

                            if (validatePalletID.Rows.Count > 0)
                            {
                                lblError.Text = String.Format(mensajes("palletexist"), validatePalletID.Rows[0]["UNID"]);
                                return;
                            }
                            else
                            {
                                validatePalletID = _idaltwhcol123.validarRegistroByPalletId(ref PALLETID, ref _UNIQUEID, ref bodegaori, ref bodegades, ref strError);

                                if (validatePalletID.Rows.Count > 0)
                                {
                                    lblError.Text = String.Format(mensajes("palletexist"), validatePalletID.Rows[0]["UNID"]);
                                    return;
                                }
                            }

                            Ent_twhcol122 _datathwcol122 = new Ent_twhcol122()
                            {
                                unid = txtUniqueID.Text.Trim().ToUpper(),
                                logn = _operator,
                                loso = txtLocation.Text.Trim().ToUpper(),
                                paid = txtPalletID.Text.Trim().ToUpper(),
                                item = txtItem.Text == String.Empty ? " " : txtItem.Text.ToUpper(),
                                clot = txtLot.Text.Trim().ToUpper(),
                                qtdt = float.Parse(txtQuantity.Text.Trim(), CultureInfo.InvariantCulture.NumberFormat),
                                proc = 1,
                                mes1 = " ",
                                refcntd = 0,
                                refcntu = 0
                            };

                            var validateSave = _idaltwhcol122.insertarRegistro(ref _datathwcol122, ref strError);

                            if (validateSave)
                            {
                                limpiarCampos();
                                lblConfirm.Text = String.Format(mensajes("palletconfirm"), txtPalletID.Text.Trim().ToUpper());
                                txtPalletID.Text = "";
                                txtPalletID.Focus();
                                return;
                            }
                            else
                            {
                                lblError.Text = mensajes("errorsave");
                                return;
                            }
                        }
                    }
                    else
                    {
                        lblError.Text = mensajes("locationblank");
                        return;
                    }

                }
                else
                {
                    lblError.Text = mensajes("palletblank");
                    return;
                }
            }

            protected void btnConfirmPalletReceive_Click(object sender, EventArgs e)
            {
                lblError.Text = String.Empty;
                lblConfirm.Text = String.Empty;

                if (txtPalletID.Text.Trim() != String.Empty)
                {
                    var palletId = txtPalletID.Text.Trim().ToUpper();

                    if (float.Parse(txtQuantity.Text) > float.Parse(hdfQuantity.Value))
                    {
                        lblError.Text = mensajes("quantityhigher");
                        return;
                    }

                    var buscadatosbodega = new DataTable();
                    buscadatosbodega = _idaltwhcol122.buscarbodegasuid(ref _UNIQUEID, ref strError);
                    var bodegaori = buscadatosbodega.Rows[0]["WARORI"].ToString();
                    var bodegades = buscadatosbodega.Rows[0]["WARDES"].ToString();

                    var validatePalletID = _idaltwhcol124.validateExistsPalletId(ref palletId, ref _UNIQUEID, ref bodegaori, ref bodegades, ref strError);

                    if (validatePalletID.Rows.Count > 0)
                    {
                        lblError.Text = String.Format(mensajes("palletexist"), validatePalletID.Rows[0]["UNID"]);
                        return;
                    }
                    else
                    {
                        validatePalletID = _idaltwhcol125.validateExistsPalletId(ref palletId, ref _UNIQUEID, ref bodegaori, ref bodegades, ref strError);

                        if (validatePalletID.Rows.Count > 0)
                        {
                            lblError.Text = String.Format(mensajes("palletexist"), validatePalletID.Rows[0]["UNID"]);
                            return;
                        }
                    }

                    Ent_twhcol124 datawhcol124 = new Ent_twhcol124()
                    {
                        unid = txtUniqueID.Text.Trim().ToUpper(),
                        logn = _operator,
                        paid = txtPalletID.Text.Trim().ToUpper(),
                        item = txtItem.Text == String.Empty ? " " : txtItem.Text.ToUpper(),
                        clot = txtLot.Text.Trim(),
                        qtdt = float.Parse(txtQuantity.Text.Trim(), CultureInfo.InvariantCulture.NumberFormat),
                        proc = 1,
                        mes1 = " ",
                        refcntd = 0,
                        refcntu = 0
                    };

                    var validateSave = _idaltwhcol124.insertarRegistro(ref datawhcol124, ref strError);

                    if (validateSave > 0)
                    {
                        limpiarCampos();
                        lblConfirm.Text = String.Format(mensajes("palletsavedrec"), txtPalletID.Text.Trim().ToUpper());
                        txtPalletID.Text = "";
                        txtPalletID.Focus();
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
                    lblError.Text = mensajes("palletblank");
                    return;
                }
            }

        #endregion

        #region Metodos

            private void limpiarCampos()
            {
                txtItem.Text = "";
                txtLot.Text = "";
                txtQuantity.Text = "";
                txtLocation.Text = "";
                txtUnit.Text = "";
                lblError.Text = String.Empty;
                lblConfirm.Text = String.Empty;
            }

            private void cargarUID(string tipoFormulario)
            {
                //var data = new DataTable();

                //ddlUniqueID.Items.Clear();

                //if (tipoFormulario == "LOAD")
                //{
                //    data = _idaltwhcol120.listFalseEndPickingRecords(ref strError);
                //}
                //else 
                //{
                //    data = _idaltwhcol120.listFalseEndReceiveRecords(ref strError);
                //}

                //ddlUniqueID.DataSource = data;
                //ddlUniqueID.DataTextField = "UNID";
                //ddlUniqueID.DataValueField = "UNID";
                //ddlUniqueID.DataBind();

                //ddlUniqueID.Items.Insert(0 , new ListItem("--Seleccione--",""));
            }

            protected void CargarIdioma()
            {
                lblNameForm.Text = _textoLabels.readStatement(formName, _idioma, "lblNameForm");
                lblScan.Text = _textoLabels.readStatement(formName, _idioma, "lblScan");
                lblPallet.Text = _textoLabels.readStatement(formName, _idioma, "lblPallet");
                lblLocation.Text = _textoLabels.readStatement(formName, _idioma, "lblLocation");
                lblItem.Text = _textoLabels.readStatement(formName, _idioma, "lblItem");
                lblLot.Text = _textoLabels.readStatement(formName, _idioma, "lblLot");
                lblQuantity.Text = _textoLabels.readStatement(formName, _idioma, "lblQuantity");
                btnConfirmPalletLoad.Text = _textoLabels.readStatement(formName, _idioma, "btnConfirmPalletLoad");
                btnConfirmPalletReceive.Text = _textoLabels.readStatement(formName, _idioma, "btnConfirmPalletReceive");
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